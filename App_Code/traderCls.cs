using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


/// <summary>
/// Summary description for traderCls
/// </summary>
public class traderCls
{
    private SequenceType sequenceType = SequenceType.NumericToAlpha;
    public traderCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int saveTraderNote(string customerId, string lotId,
        DataTable dtProgLang, string userId, string userName)
    {
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("tradeNote");
        command.Connection = connection;
        command.Transaction = transaction;      
        try
        {
            decimal totSum = Convert.ToDecimal(0.0);

            // add in invoice
            DataTable dt = new DataTable();
            command.CommandText = "select * from websiteCustomer where webCustId=@webCustId";
            command.Parameters.AddWithValue("@webCustId", customerId);
            dt.Load(command.ExecuteReader());

            command.CommandText = "insert into invoice (invType,webCustomer,userid,invoiceStatus,custname,address1,city,state,phoneNo,tokenGenBy,salesDate) values " +
                "(@invType,@webCustomer,@userid,@invoiceStatus,@custname,@address1,@city,@state,@phoneNo,@tokenGenBy,@salesDate)SELECT CAST(scope_identity() AS int)";
            command.Parameters.AddWithValue("@webCustomer", customerId);
            command.Parameters.AddWithValue("@userid", userId);
            command.Parameters.AddWithValue("@invoiceStatus", "Invoiced");
            command.Parameters.AddWithValue("@invType", "Trader Note");
            command.Parameters.AddWithValue("@custname", dt.Rows[0]["custFirstName"].ToString() + " " + dt.Rows[0]["custLastName"].ToString());
            command.Parameters.AddWithValue("@address1", dt.Rows[0]["address"].ToString());
            command.Parameters.AddWithValue("@city", dt.Rows[0]["city"].ToString());
            command.Parameters.AddWithValue("@state", dt.Rows[0]["state"].ToString());
            command.Parameters.AddWithValue("@phoneNo", dt.Rows[0]["phoneNo"].ToString());
            command.Parameters.AddWithValue("@tokenGenBy", userId);
            command.Parameters.AddWithValue("@salesDate", DateTime.Now.ToString("yyyy-MM-dd"));
            int invoiceid = (Int32)command.ExecuteScalar();
            command.Parameters.Clear();

            int res = 1;
            // add to stockup and move to archive as well
            foreach (DataRow dRow in dtProgLang.Rows)
            {
                // get size(pieceperpacket) 
                command.CommandText = "select Size1,SizeCode from Size where SizeID=@getSize";
                command.Parameters.AddWithValue("@getSize", dRow["Size"].ToString());
                DataTable sizeDt = new DataTable();
                sizeDt.Load(command.ExecuteReader());

                // get style 
                command.CommandText = "select StyleCode from ItemStyle where StyleID=@getStyleID";
                command.Parameters.AddWithValue("@getStyleID", dRow["styleDrp"].ToString());
                DataTable styleDt = new DataTable();
                styleDt.Load(command.ExecuteReader());
                
                for (int i = 0; i < Convert.ToInt32(dRow["quantity"].ToString()); i++)
                {
                   
                    DataTable stockUp = new DataTable();
                    command.CommandText = "select Top 1 LastBarcode from " +
                    "(select LastBarcode, StockupID from StockUpInward where StyleID = @StyleID and SizeID = @sizeID1 " +
                    "union all " +
                    "select LastBarcode, StockupID from ArchiveStockUpInward where StyleID = @StyleIDArch and SizeID = @sizeID1Arch  ) a  " +
                    "order by StockupID desc";
                    command.Parameters.AddWithValue("@StyleID", dRow["styleDrp"].ToString());
                    command.Parameters.AddWithValue("@sizeID1", dRow["Size"].ToString());
                    command.Parameters.AddWithValue("@StyleIDArch", dRow["styleDrp"].ToString());
                    command.Parameters.AddWithValue("@sizeID1Arch", dRow["Size"].ToString());

                    stockUp.Load(command.ExecuteReader());
                    string LastBarcode = "000";
                    if (!stockUp.Rows.Count.Equals(0))
                    {
                        AlphaNumeric.RequiredLength = Convert.ToInt32("3");  //000 - no. of digits needed
                        LastBarcode = AlphaNumeric.NextKeyCode(stockUp.Rows[0]["LastBarcode"].ToString(), sequenceType);

                    }

                    string BarcodeNo = styleDt.Rows[0]["StyleCode"].ToString() + "/" + sizeDt.Rows[0]["Size1"].ToString() + "/" + LastBarcode;
                    command.CommandText = "select BarcodeNo from StockUpInward where BarcodeNo = @sBarcodeNo union all select BarcodeNo from ArchiveStockUpInward where BarcodeNo = @aBarcodeNo";
                    command.Parameters.AddWithValue("@sBarcodeNo", BarcodeNo);
                    command.Parameters.AddWithValue("@aBarcodeNo", BarcodeNo);
                    DataTable barcodeCheck = new DataTable();
                    barcodeCheck.Load(command.ExecuteReader());


                    if (barcodeCheck.Rows.Count.Equals(0))
                    {
                        command.CommandText = "insert into StockUpInward (StyleID,BagID,SizeID,LastBarcode,Status,mrp,userId,BarcodeNo,oldBarcode,initialStatus,mfgDate,piecePerPacket,isSample,purchaseRate) " +
                        " values (@StyleID1,@BagID,@SizeID,@LastBarcode,@Status,@mrp,@userId,@BarcodeNo,@oldBarcode,@initialStatus,@mfgDate,@piecePerPacket,@isSample,@purchaseRate) SELECT CAST(scope_identity() AS int)";
                        command.Parameters.AddWithValue("@StyleID1", dRow["styleDrp"].ToString());
                        command.Parameters.AddWithValue("@BagID", lotId);
                        command.Parameters.AddWithValue("@SizeID", dRow["Size"].ToString());
                        command.Parameters.AddWithValue("@LastBarcode", LastBarcode);
                        command.Parameters.AddWithValue("@Status", "RFL");
                        command.Parameters.AddWithValue("@mrp", Convert.ToDecimal(dRow["mrp"].ToString()) * Convert.ToDecimal(sizeDt.Rows[0]["Size1"].ToString()));
                        //command.Parameters.AddWithValue("@mrp", Convert.ToDecimal(dRow["mrp"].ToString()) * Convert.ToDecimal(dRow["quantity"].ToString()));
                        command.Parameters.AddWithValue("@userId", userId);
                        command.Parameters.AddWithValue("@BarcodeNo", BarcodeNo);
                        command.Parameters.AddWithValue("@oldBarcode", string.Empty);
                        command.Parameters.AddWithValue("@initialStatus", "RFL");
                        command.Parameters.AddWithValue("@mfgDate", DateTime.Now.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@piecePerPacket", sizeDt.Rows[0]["Size1"].ToString());
                        command.Parameters.AddWithValue("@isSample", "0");
                        command.Parameters.AddWithValue("@purchaseRate", Convert.ToDecimal(dRow["purchaseRate"].ToString()) * Convert.ToDecimal(sizeDt.Rows[0]["Size1"].ToString()));
                        //command.Parameters.AddWithValue("@purchaseRate", Convert.ToDecimal(dRow["purchaseRate"].ToString()) * Convert.ToDecimal(dRow["quantity"].ToString()));
                        int stockId = (Int32)command.ExecuteScalar();

                        //calculate gst percent
                        // FETCH HSN CODE
                        DataTable hsnDt = new DataTable();
                        command.CommandText = "select h.*,si.StockupID,si.mrp from hsnmaster h inner join ItemCategory c on h.hsnid=c.hsnid inner join ItemStyle s on s.ItemCatID=c.ItemCategoryID inner join StockUpInward si on si.StyleID=s.StyleID where si.BarcodeNo = @BarcodeNo and si.Status!=@checkStatus";
                        
                        command.Parameters.AddWithValue("@checkStatus", "SOLD");
                        hsnDt.Load(command.ExecuteReader());

                        // total sum
                        totSum += Convert.ToDecimal(dRow["sp"].ToString());

                        //calculate hsn
                        decimal lowhighpt = Convert.ToDecimal(hsnDt.Rows[0]["lowhighpt"]);
                        decimal ligst = Convert.ToDecimal(hsnDt.Rows[0]["ligst"]);
                        decimal higst = Convert.ToDecimal(hsnDt.Rows[0]["higst"]);
                        decimal taxableamnt = 0;
                        decimal igst = 0;
                        decimal cgst = 0;
                        decimal sgst = 0;
                        decimal gst = 0;
                        decimal amnt = ((Convert.ToDecimal(dRow["sp"].ToString())) - ((Convert.ToDecimal(dRow["sp"].ToString()) * ligst) / 100));
                        if (amnt <= lowhighpt)
                        {
                            taxableamnt = amnt;
                            igst = ((Convert.ToDecimal(dRow["sp"].ToString()) * ligst) / 100);
                            gst = ligst;
                        }
                        else
                        {
                            taxableamnt = ((Convert.ToDecimal(dRow["sp"].ToString())) - ((Convert.ToDecimal(dRow["sp"].ToString()) * higst) / 100));
                            igst = ((Convert.ToDecimal(dRow["sp"].ToString()) * higst) / 100);
                            gst = higst;
                        }
                        cgst = igst / 2;
                        sgst = igst / 2;
                        if (dt.Rows[0]["state"].ToString().Equals("27"))
                        {
                            igst = 0;
                            cgst = cgst;
                            sgst = sgst;
                        }
                        else
                        {
                            igst = igst;
                            cgst = 0;
                            sgst = 0;
                        }

                        // insert into archive
                        command.CommandText = "INSERT INTO ArchiveStockUpInward (StyleID, UserID, BagID, SizeID, DateTime, LastBarcode, StyleCode, BarcodeNo, RFLQty, RejectQty, Status, LocationID, ExpiredDate, ListingDate, ModeOfPayment, SoldAmount, DispatchedDate, SalesDate, RackBarcode, RackDate, CancelReason, CancelDate, ListedUserID, Picked, BulkID, BulkDate, MoneyIn, MoneyOut, ItemID, SalesID, RecordNo, CourierTransactionID, mrp, StockupID,SystemDate,printed,oldBarcode,initialStatus,labels,mfgDate,labelUserId,physicalId,subLocId,rackId,stackId,piecePerPacket,isSample,purchaseRate,checkNo)" +
                                                        " SELECT StyleID, UserID, BagID, SizeID, DateTime, LastBarcode, StyleCode, BarcodeNo, RFLQty, RejectQty, Status, LocationID, ExpiredDate, ListingDate, ModeOfPayment, SoldAmount, DispatchedDate, SalesDate, RackBarcode, RackDate, CancelReason, CancelDate, ListedUserID, Picked, BulkID, BulkDate, MoneyIn, MoneyOut, ItemID, SalesID, RecordNo, CourierTransactionID, mrp, StockupID,SystemDate,printed,oldBarcode,initialStatus,labels,mfgDate,labelUserId,physicalId,subLocId,rackId,stackId,piecePerPacket,isSample,purchaseRate,checkNo FROM StockUpInward WHERE StockupID=@StockupID " +
                                                        " SELECT CAST(scope_identity() AS int)";
                        command.Parameters.AddWithValue("@StockupID", Convert.ToInt32(hsnDt.Rows[0]["StockupID"]));
                        int archiveid = (Int32)command.ExecuteScalar();

                        // delete from stock up
                        command.CommandText = "DELETE FROM StockUpInward WHERE StockupID=@StockupID ";
                        command.ExecuteNonQuery();

                        // insert in sales
                        command.CommandText = "insert into salesrecord" +
                            " (invoiceid,saleschannelvlocid,salesidgivenbyvloc," +
                            "sellingprice,status,itemid,gstpercent,taxableamount,cgstamnt," +
                            "sgstamnt,igstamnt,salesUserId,dispatchtimestamp,dispatchuserid,archiveid) values " +
                            "(@invoiceid,@saleschannelvlocid,@salesidgivenbyvloc," +
                            "@sellingprice,@status11,@itemid,@gstpercent,@taxableamount," +
                            "@cgstamnt,@sgstamnt,@igstamnt,@salesUserId,@dispatchtimestamp,@dispatchuserid,@archiveid" +
                            ")";
                        command.Parameters.AddWithValue("@invoiceid", invoiceid);
                        command.Parameters.AddWithValue("@saleschannelvlocid", "2");
                        command.Parameters.AddWithValue("@salesidgivenbyvloc", "");
                        command.Parameters.AddWithValue("@sellingprice", Convert.ToDecimal(dRow["sp"].ToString()));
                        command.Parameters.AddWithValue("@status11", "DISPATCHED");
                        command.Parameters.AddWithValue("@itemid", "-1");
                        command.Parameters.AddWithValue("@archiveid", archiveid);
                        command.Parameters.AddWithValue("@gstpercent", gst);
                        command.Parameters.AddWithValue("@taxableamount", taxableamnt);
                        command.Parameters.AddWithValue("@igstamnt", igst);
                        command.Parameters.AddWithValue("@cgstamnt", cgst);
                        command.Parameters.AddWithValue("@sgstamnt", sgst);
                        command.Parameters.AddWithValue("@salesUserId", userId);
                        command.Parameters.AddWithValue("@dispatchtimestamp", DateTime.Now);
                        command.Parameters.AddWithValue("@dispatchuserid", userId);
                        command.ExecuteNonQuery();

                        command.Parameters.Clear();
                    }
                    else
                    {                       
                        res = 0;
                        break;
                    }
                    command.Parameters.Clear();

                }
            }

            // update invoice with total SUM
            command.CommandText = "update invoice set total=@itotal where invid=@iinvid";
            command.Parameters.AddWithValue("@itotal", totSum);
            command.Parameters.AddWithValue("@iinvid", invoiceid);
            command.ExecuteNonQuery();

            if (res.Equals(1)) // commit transaction
            {
                transaction.Commit();
                command.Parameters.Clear();
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                return 0;
            }
            else
            {
                transaction.Rollback();
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                return -1;
            }



        }
        catch (Exception ex)
        {
            try
            {
                transaction.Rollback();
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex);
                return -1;
            }
            catch (Exception ex2)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex2);
                return -1;

            }
        }
    }

   
}