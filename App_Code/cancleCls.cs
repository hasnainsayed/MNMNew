using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for cancleCls
/// </summary>
public class cancleCls
{
    string userId = HttpContext.Current.Session["login"].ToString();//id of logged in admin
    public cancleCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string deleteBarcode(string barcode,string reasons)
    {
        string succFailure = string.Empty;
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("delBarcode");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            //get stockupId (not there in stock)
            command.CommandText = "select * from StockUpInward where BarcodeNo=@barcode";
            command.Parameters.AddWithValue("@barcode", barcode);
            DataTable stock = new DataTable();
            stock.Load(command.ExecuteReader());

            // check in ticket master 
            command.CommandText = "select * from StockUpInward s inner join salesrecord sr on s.BarcodeNo=@barcodeTicket and sr.itemid=s.StockupID inner join ticket_master t on t.salesid=sr.sid";
            command.Parameters.AddWithValue("@barcodeTicket", barcode);
            DataTable ticket = new DataTable();
            ticket.Load(command.ExecuteReader());

            // check if exist
            if (!stock.Rows.Count.Equals(0) && ticket.Rows.Count.Equals(0))
            {        
                // check for status (not sold)
                if (stock.Rows[0]["Status"].ToString().Equals("SOLD"))
                {
                    succFailure = "Barcode marked as Sold. Cannot Cancel Barcode";
                }
                else
                {
                    //check if there in salesrecord(stockupid if marked as return or something)
                    command.CommandText = "select * from salesrecord where itemid =@StockupID";
                    command.Parameters.AddWithValue("@StockupID", Convert.ToInt32(stock.Rows[0]["StockupID"].ToString()));
                    DataTable sales = new DataTable();
                    sales.Load(command.ExecuteReader());
                    if (sales.Rows.Count.Equals(0))
                    {
                        command.CommandText = "INSERT INTO deleteStockUpInward (StyleID, UserID, BagID, SizeID, DateTime, LastBarcode, StyleCode, BarcodeNo, RFLQty, RejectQty, Status, LocationID, ExpiredDate, ListingDate, ModeOfPayment, SoldAmount, DispatchedDate, SalesDate, RackBarcode, RackDate, CancelReason, CancelDate, ListedUserID, Picked, BulkID, BulkDate, MoneyIn, MoneyOut, ItemID, SalesID, RecordNo, CourierTransactionID, mrp, StockupID,SystemDate,printed,oldBarcode,initialStatus,labels,mfgDate,labelUserId,piecePerPacket,isSample,purchaseRate)" +
                        " SELECT StyleID, UserID, BagID, SizeID, DateTime, LastBarcode, StyleCode, BarcodeNo, RFLQty, RejectQty, Status, LocationID, ExpiredDate, ListingDate, ModeOfPayment, SoldAmount, DispatchedDate, SalesDate, RackBarcode, RackDate, CancelReason, CancelDate, ListedUserID, Picked, BulkID, BulkDate, MoneyIn, MoneyOut, ItemID, SalesID, RecordNo, CourierTransactionID, mrp, StockupID,SystemDate,printed,oldBarcode,initialStatus,labels,mfgDate,labelUserId,piecePerPacket,isSample,purchaseRate FROM StockUpInward WHERE StockupID=@delStockupID " +
                        " SELECT CAST(scope_identity() AS int)";
                        command.Parameters.AddWithValue("@delStockupID", Convert.ToInt32(stock.Rows[0]["StockupID"].ToString()));
                        int deleteStockupID = (Int32)command.ExecuteScalar();

                        // update userid
                        command.CommandText = "update deleteStockUpInward set reasons=@reasons,delUserId=@delUserId where deleteStockupID=@deleteStockupID";
                        command.Parameters.AddWithValue("@reasons", reasons);
                        command.Parameters.AddWithValue("@deleteStockupID", deleteStockupID);
                        command.Parameters.AddWithValue("@delUserId", userId);
                        command.ExecuteNonQuery();

                        //delete barcode
                        command.CommandText = "delete from StockUpInward where StockupID=@stckID";
                        command.Parameters.AddWithValue("@stckID", Convert.ToInt32(stock.Rows[0]["StockupID"].ToString()));
                        command.ExecuteNonQuery();
                        succFailure = "Barcode Deleted";
                    }
                    else
                    {
                        succFailure = "Barcode was Sold earlier. Cannot Delete Barcode";
                    }

                }
            }
            else
            {
                succFailure = "Barcode Does Not exist or Ticket is raised against this Barcode. Cannot Delete Barcode";
            }

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

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

            }
            catch (Exception ex2)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex2);


            }
        }
        return succFailure;
    }

    public string changePrintStatus(string barcode)
    {
        string succFailure = string.Empty;
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("printStat");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            //get stockupId (not there in stock)
            command.CommandText = "select * from StockUpInward where BarcodeNo=@barcode";
            command.Parameters.AddWithValue("@barcode", barcode);
            DataTable stock = new DataTable();
            stock.Load(command.ExecuteReader());
            // check if exist
            if (!stock.Rows.Count.Equals(0))
            {
                command.CommandText = "update StockUpInward set printed=@printed,RackBarcode=@RackBarcode,RackDate=@RackDate where BarcodeNo=@BarcodeNo12";
                command.Parameters.AddWithValue("@printed","No");
                command.Parameters.AddWithValue("@RackBarcode", "");
                command.Parameters.AddWithValue("@RackDate", DBNull.Value);
                command.Parameters.AddWithValue("@BarcodeNo12", barcode);
                command.ExecuteNonQuery();
                succFailure = "Barcode Print Status Changed ";
            }
            else
            {
                succFailure = "Barcode Does Not exist ";
            }

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            succFailure = "Exception Occured";
            try
            {
                transaction.Rollback();
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex);

            }
            catch (Exception ex2)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex2);


            }
        }
        return succFailure;
    }

    public string deleteWrongBarcode(string barcode, string reasons)
    {
        string succFailure = string.Empty;
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("delWBarcode");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            //get stockupId (not there in stock)
            command.CommandText = "select * from StockUpInward where BarcodeNo=@barcode";
            command.Parameters.AddWithValue("@barcode", barcode);
            DataTable stock = new DataTable();
            stock.Load(command.ExecuteReader());

            // check in ticket master 
            command.CommandText = "select * from StockUpInward s inner join salesrecord sr on s.BarcodeNo=@barcodeTicket and sr.itemid=s.StockupID inner join ticket_master t on t.salesid=sr.sid";
            command.Parameters.AddWithValue("@barcodeTicket", barcode);
            DataTable ticket = new DataTable();
            ticket.Load(command.ExecuteReader());

            // check if exist
            if (!stock.Rows.Count.Equals(0) && ticket.Rows.Count.Equals(0))
            {

                //check if there in salesrecord(stockupid if marked as return or something)
                command.CommandText = "select * from salesrecord where itemid =@StockupID";
                command.Parameters.AddWithValue("@StockupID", Convert.ToInt32(stock.Rows[0]["StockupID"].ToString()));
                DataTable sales = new DataTable();
                sales.Load(command.ExecuteReader());
                if(sales.Rows.Count.Equals(0))
                {
                    succFailure = "No Sales Entry. Please Use Cancle Barcode Option if you want delete this Barcode";
                }
                else
                {
                    foreach (DataRow rows in sales.Rows)
                    {
                        command.CommandText = "select * from salesrecord WHERE sid=@sid12";
                        command.Parameters.AddWithValue("@sid12", rows["sid"].ToString());
                        DataTable dt = new DataTable();
                        dt.Load(command.ExecuteReader());

                        // select data from sales id and add to transaction
                        command.CommandText = "insert into cancelTrans (sid,invoiceid,saleschannelvlocid," +
                            "salesidgivenbyvloc,sellingprice,status,itemid,archiveid,recordtimestamp,dispatchtimestamp," +
                            "dispatchuserid,gstpercent,taxableamount,cgstamnt,sgstamnt,igstamnt,salesCourier,salesAbwno," +
                            "returntimestamp,returnuserid,returnCourier,returnAbwno,reasons,remarks,salesDateTime," +
                            "salesUserId,canReason,cancelId,cancleReason,changeStatus,rImage1,rImage2,rImage3," +
                            "rImage4,rImage5,rImage6,rImage7,rImage8,rImage9,rImage10,rVideo1,rVideo2" +
                            ",smsStatus,whatsappStatus,deliveryStatus,deliveryDate,callOneStatus,callOneDateime,callOneUserId,callTwoStatus,callTwoDatetime,callTwoUserId,callThreeStatus,callThreeDatetime,callThreeUserId" +
                            ",callFourStatus,callFourDatetime,callFourUserId,callFiveStatus,callFiveDatetime," +
                            "callFiveUserId,customerStatus,callingStatus,contactStatus,callingCount,delStatus,follwUpTime)" +
                                " values " +
                                "(@sid1,@invoiceid1,@saleschannelvlocid,@salesidgivenbyvloc," +
                                "@sellingprice,@status1,@itemid,@archiveid," +
                                "@recordtimestamp,@dispatchtimestamp,@dispatchuserid,@gstpercent," +
                                "@taxableamount,@cgstamnt,@sgstamnt,@igstamnt,@salesCourier,@salesAbwno,@returntimestamp," +
                                "@returnuserid,@returnCourier,@returnAbwno,@reasons,@remarks,@salesDateTime," +
                                "@salesUserId,@canReason,@cancelId,@cancleReason,@changeStatus,@rImage1,@rImage2," +
                                "@rImage3,@rImage4,@rImage5,@rImage6,@rImage7,@rImage8,@rImage9,@rImage10,@rVideo1," +
                                "@rVideo2" +
                                ",@smsStatus,@whatsappStatus,@deliveryStatus,@deliveryDate,@callOneStatus,@callOneDateime,@callOneUserId,@callTwoStatus,@callTwoDatetime,@callTwoUserId,@callThreeStatus,@callThreeDatetime,@callThreeUserId" +
                                ",@callFourStatus,@callFourDatetime,@callFourUserId,@callFiveStatus,@callFiveDatetime," +
                                "@callFiveUserId,@customerStatus,@callingStatus,@contactStatus,@callingCount,@delStatus,@follwUpTime)";
                        command.Parameters.AddWithValue("@sid1", rows["sid"].ToString());
                        command.Parameters.AddWithValue("@saleschannelvlocid", dt.Rows[0]["saleschannelvlocid"].ToString());
                        command.Parameters.AddWithValue("@invoiceid1", dt.Rows[0]["invoiceid"].ToString());
                        command.Parameters.AddWithValue("@salesidgivenbyvloc", dt.Rows[0]["salesidgivenbyvloc"].ToString());
                        command.Parameters.AddWithValue("@sellingprice", Convert.ToDecimal(dt.Rows[0]["sellingprice"].ToString()));
                        command.Parameters.AddWithValue("@status1", dt.Rows[0]["status"].ToString());
                        command.Parameters.AddWithValue("@itemid", dt.Rows[0]["itemid"].ToString());
                        command.Parameters.AddWithValue("@archiveid", dt.Rows[0]["archiveid"].ToString());
                        command.Parameters.AddWithValue("@recordtimestamp", Convert.ToDateTime(dt.Rows[0]["recordtimestamp"]).ToString("yyyy-MM-dd HH:mm:ss.mmm"));
                        command.Parameters.AddWithValue("@dispatchtimestamp", dt.Rows[0]["dispatchtimestamp"]);
                        command.Parameters.AddWithValue("@dispatchuserid", dt.Rows[0]["dispatchuserid"].ToString());
                        command.Parameters.AddWithValue("@gstpercent", dt.Rows[0]["gstpercent"].ToString());
                        command.Parameters.AddWithValue("@taxableamount", dt.Rows[0]["taxableamount"].ToString());
                        command.Parameters.AddWithValue("@cgstamnt", dt.Rows[0]["cgstamnt"].ToString());
                        command.Parameters.AddWithValue("@sgstamnt", dt.Rows[0]["sgstamnt"].ToString());
                        command.Parameters.AddWithValue("@igstamnt", dt.Rows[0]["igstamnt"].ToString());
                        command.Parameters.AddWithValue("@salesCourier", dt.Rows[0]["salesCourier"].ToString());
                        command.Parameters.AddWithValue("@salesAbwno", dt.Rows[0]["salesAbwno"].ToString());
                        command.Parameters.AddWithValue("@returntimestamp", dt.Rows[0]["returntimestamp"]);
                        command.Parameters.AddWithValue("@returnuserid", dt.Rows[0]["returnuserid"].ToString());
                        command.Parameters.AddWithValue("@returnCourier", dt.Rows[0]["returnCourier"].ToString());
                        command.Parameters.AddWithValue("@returnAbwno", dt.Rows[0]["returnAbwno"].ToString());
                        command.Parameters.AddWithValue("@reasons", dt.Rows[0]["reasons"].ToString());
                        command.Parameters.AddWithValue("@remarks", dt.Rows[0]["remarks"].ToString());
                        command.Parameters.AddWithValue("@salesDateTime", Convert.ToDateTime(dt.Rows[0]["salesDateTime"]).ToString("yyyy-MM-dd HH:mm:ss.mmm"));
                        command.Parameters.AddWithValue("@salesUserId", dt.Rows[0]["salesUserId"].ToString());
                        command.Parameters.AddWithValue("@canReason", reasons);
                        command.Parameters.AddWithValue("@cancelId", userId);
                        command.Parameters.AddWithValue("@cancleReason", "Wrong Barcode");
                        command.Parameters.AddWithValue("@changeStatus", "DELETED");
                        command.Parameters.AddWithValue("@rImage1", dt.Rows[0]["rImage1"].ToString());
                        command.Parameters.AddWithValue("@rImage2", dt.Rows[0]["rImage2"].ToString());
                        command.Parameters.AddWithValue("@rImage3", dt.Rows[0]["rImage3"].ToString());
                        command.Parameters.AddWithValue("@rImage4", dt.Rows[0]["rImage4"].ToString());
                        command.Parameters.AddWithValue("@rImage5", dt.Rows[0]["rImage5"].ToString());
                        command.Parameters.AddWithValue("@rImage6", dt.Rows[0]["rImage6"].ToString());
                        command.Parameters.AddWithValue("@rImage7", dt.Rows[0]["rImage7"].ToString());
                        command.Parameters.AddWithValue("@rImage8", dt.Rows[0]["rImage8"].ToString());
                        command.Parameters.AddWithValue("@rImage9", dt.Rows[0]["rImage9"].ToString());
                        command.Parameters.AddWithValue("@rImage10", dt.Rows[0]["rImage10"].ToString());
                        command.Parameters.AddWithValue("@rVideo1", dt.Rows[0]["rVideo1"].ToString());
                        command.Parameters.AddWithValue("@rVideo2", dt.Rows[0]["rVideo2"].ToString());

                        command.Parameters.AddWithValue("@smsStatus", dt.Rows[0]["smsStatus"].ToString());
                        command.Parameters.AddWithValue("@whatsappStatus", dt.Rows[0]["whatsappStatus"].ToString());
                        command.Parameters.AddWithValue("@deliveryDate", dt.Rows[0]["deliveryDate"].ToString());
                        command.Parameters.AddWithValue("@callOneStatus", dt.Rows[0]["callOneStatus"].ToString());
                        command.Parameters.AddWithValue("@callOneDateime",dt.Rows[0]["callOneDateime"].ToString());
                        command.Parameters.AddWithValue("@callOneUserId", dt.Rows[0]["callOneUserId"].ToString());
                        command.Parameters.AddWithValue("@callTwoStatus", dt.Rows[0]["callTwoStatus"].ToString());
                        command.Parameters.AddWithValue("@callTwoDatetime", dt.Rows[0]["callTwoDatetime"].ToString());
                        command.Parameters.AddWithValue("@callTwoUserId", dt.Rows[0]["callTwoUserId"].ToString());
                        command.Parameters.AddWithValue("@callThreeStatus", dt.Rows[0]["callThreeStatus"].ToString());
                        command.Parameters.AddWithValue("@callThreeDatetime", dt.Rows[0]["callThreeDatetime"].ToString());
                        command.Parameters.AddWithValue("@callThreeUserId", dt.Rows[0]["callThreeUserId"].ToString());
                        command.Parameters.AddWithValue("@callFourStatus", dt.Rows[0]["callFourStatus"].ToString());
                        command.Parameters.AddWithValue("@callFourDatetime", dt.Rows[0]["callFourDatetime"].ToString());
                        command.Parameters.AddWithValue("@callFourUserId", dt.Rows[0]["callFourUserId"].ToString());
                        command.Parameters.AddWithValue("@callFiveStatus", dt.Rows[0]["callFiveStatus"].ToString());
                        command.Parameters.AddWithValue("@callFiveDatetime", dt.Rows[0]["callFiveDatetime"].ToString());
                        command.Parameters.AddWithValue("@callFiveUserId", dt.Rows[0]["callFiveUserId"].ToString());
                        command.Parameters.AddWithValue("@customerStatus", dt.Rows[0]["customerStatus"].ToString());
                        command.Parameters.AddWithValue("@callingStatus", dt.Rows[0]["callingStatus"].ToString());
                        command.Parameters.AddWithValue("@contactStatus", dt.Rows[0]["contactStatus"].ToString());
                        command.Parameters.AddWithValue("@callingCount", dt.Rows[0]["callingCount"].ToString());
                        command.Parameters.AddWithValue("@delStatus", dt.Rows[0]["delStatus"].ToString());
                        command.Parameters.AddWithValue("@deliveryStatus", dt.Rows[0]["deliveryStatus"].ToString());
                        command.Parameters.AddWithValue("@follwUpTime", dt.Rows[0]["follwUpTime"].ToString());

                        command.ExecuteNonQuery();

                        // delete from salesrecord
                        command.CommandText = "DELETE FROM salesrecord WHERE sid=@sid";
                        command.Parameters.AddWithValue("@sid", rows["sid"].ToString());
                        command.ExecuteNonQuery();

                        //  find current total cost and total records
                        command.CommandText = "select isnull(count(sid),0) as cnt,isnull(sum(sellingprice),0) as totAmnt from salesrecord where invoiceid=@invoiceid";
                        command.Parameters.AddWithValue("@invoiceid", rows["invoiceid"].ToString());
                        DataTable dt1 = new DataTable();
                        dt1.Load(command.ExecuteReader());

                        command.CommandText = "update invoice set total=@total where invid=@invid1";
                        command.Parameters.AddWithValue("@total", Convert.ToDecimal(dt1.Rows[0]["totAmnt"]));
                        command.Parameters.AddWithValue("@invid1", rows["invoiceid"].ToString());
                        command.ExecuteNonQuery();

                        /*if (Convert.ToInt32(dt1.Rows[0]["cnt"]).Equals(Convert.ToInt32(0)))
                        {
                            // if total record is 0 than delete invoice

                            command.CommandText = "DELETE FROM invoice WHERE invid=@invid";
                            command.Parameters.AddWithValue("@invid", rows["invoiceid"].ToString());
                            command.ExecuteNonQuery();
                        }
                        else
                        {
                            command.CommandText = "update invoice set total=@total where invid=@invid1";
                            command.Parameters.AddWithValue("@total", Convert.ToDecimal(dt1.Rows[0]["totAmnt"]));
                            command.Parameters.AddWithValue("@invid1", rows["invoiceid"].ToString());
                            command.ExecuteNonQuery();
                        }*/
                        command.Parameters.Clear();

                    }

                    command.CommandText = "INSERT INTO deleteStockUpInward (StyleID, UserID, BagID, SizeID, DateTime, LastBarcode, StyleCode, BarcodeNo, RFLQty, RejectQty, Status, LocationID, ExpiredDate, ListingDate, ModeOfPayment, SoldAmount, DispatchedDate, SalesDate, RackBarcode, RackDate, CancelReason, CancelDate, ListedUserID, Picked, BulkID, BulkDate, MoneyIn, MoneyOut, ItemID, SalesID, RecordNo, CourierTransactionID, mrp, StockupID,SystemDate,printed,oldBarcode,initialStatus,labels,mfgDate,labelUserId,piecePerPacket,isSample,purchaseRate)" +
                    " SELECT StyleID, UserID, BagID, SizeID, DateTime, LastBarcode, StyleCode, BarcodeNo, RFLQty, RejectQty, Status, LocationID, ExpiredDate, ListingDate, ModeOfPayment, SoldAmount, DispatchedDate, SalesDate, RackBarcode, RackDate, CancelReason, CancelDate, ListedUserID, Picked, BulkID, BulkDate, MoneyIn, MoneyOut, ItemID, SalesID, RecordNo, CourierTransactionID, mrp, StockupID,SystemDate,printed,oldBarcode,initialStatus,labels,mfgDate,labelUserId,piecePerPacket,isSample,purchaseRate FROM StockUpInward WHERE StockupID=@delStockupID " +
                    " SELECT CAST(scope_identity() AS int)";
                    command.Parameters.AddWithValue("@delStockupID", Convert.ToInt32(stock.Rows[0]["StockupID"].ToString()));
                    int deleteStockupID = (Int32)command.ExecuteScalar();

                    // update delete reason
                    command.CommandText = "update deleteStockUpInward set reasons=@reasons,delUserId=@delUserId where deleteStockupID=@deleteStockupIDss";
                    command.Parameters.AddWithValue("@reasons", reasons);
                    command.Parameters.AddWithValue("@delUserId", userId);
                    command.Parameters.AddWithValue("@deleteStockupIDss", deleteStockupID);
                    command.ExecuteNonQuery();
                    
                    //delete barcode
                    command.CommandText = "delete from StockUpInward where StockupID=@stckID";
                    command.Parameters.AddWithValue("@stckID", Convert.ToInt32(stock.Rows[0]["StockupID"].ToString()));
                    command.ExecuteNonQuery();
                    succFailure = "Barcode Deleted";
                }
                
            }
            else
            {
                succFailure = "Barcode Does Not exist or Ticket is raised against this Barcode. Cannot Cancel Barcode";
            }

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

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

            }
            catch (Exception ex2)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex2);


            }
        }
        return succFailure;
    }

    public DataTable getDeletedBarcodes(string fromDate, string toDate, string barcode, string salesid,  bool salesCheck, bool barcodeCheck, bool dateRange)
    {
        DataTable mergeTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("getDelBarc");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            string dateR = string.Empty;
            string dateRA = string.Empty;
            int where = 0;
            if (dateRange.Equals(true))
            {
                dateR = " and (delDate between @fromDate and @toDate)";               
                command.Parameters.AddWithValue("@fromDate", fromDate);
                command.Parameters.AddWithValue("@toDate", toDate);
               
              
            }
            string conV = string.Empty;
            string conV1 = string.Empty;
            string conVA = string.Empty;
            if (!salesid.Equals("") && salesCheck.Equals(true))
            {
                conV1 += " where salesidgivenbyvloc=@salesid";
                command.Parameters.AddWithValue("@salesid", salesid);
                
            }

            if (!barcode.Equals("") && barcodeCheck.Equals(true))
            {
                conV += " and BarcodeNo=@barcode";
                command.Parameters.AddWithValue("@barcode", barcode);
                
            }

            string query = string.Empty;
            string query1 = string.Empty;

            query += "select tab.BarcodeNo,i.Title,c.ItemCategory,col1.C1Name,l.username,'delete barcode' as msg,null as salesidgivenbyvloc,null as Location,null as salesCourier,null as salesDateTime," +
                "null as salesUser,null as retUser,null as retCourier,null as dispatchtimestamp,null as dispatchUser,null as creasons,null as returntimestamp,null as remarks,null as salesAbwno,tab.delDate as deltimestamp,tab.reasons as delreason,null as returnAbwno" +
                " from (select * from deleteStockUpInward d where d.StockupID not in (select distinct(itemid) from cancelTrans " + conV1 + ") " + dateR + "" + conV + ") tab" +
                " inner join ItemStyle i on i.StyleID = tab.StyleID " +
                "inner join ItemCategory c on c.ItemCategoryID = i.ItemCatID " +
                "inner join Column1 col1 on col1.Col1ID = i.Col1 " +
                "inner join login l on l.userid = tab.delUserId ";
                /*"left join Location loc on loc.LocationID = tab.saleschannelvlocid " +
                "left join courier salesc on salesc.courierId = tab.salesCourier " +
                "left join login salesLog on salesLog.userid = tab.salesUserId " +
                "left join courier retc on retc.courierId = tab.returnCourier " +
                "left join login retLog on retLog.userid = tab.returnuserid";*/
          

            command.CommandText = query;
            DataTable dt1 = new DataTable();
            dt1.Load(command.ExecuteReader());

            //int cnt = dt1.Rows.Count;

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();
            return dt1;
        }
        catch (Exception ex)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
            return mergeTable;
        }
        
    }
    public DataTable getDeletedBarcodesCan(string fromDate, string toDate, string barcode, string salesid, bool salesCheck, bool barcodeCheck, bool dateRange)
    {
        DataTable mergeTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("getDelBarc");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            string dateR = string.Empty;
            string dateRA = string.Empty;
            int where = 0;
            if (dateRange.Equals(true))
            {
                
                command.Parameters.AddWithValue("@fromDate", fromDate);
                command.Parameters.AddWithValue("@toDate", toDate);
                if (where.Equals(0))
                {
                    dateRA = " where (delDate between @fromDate and @toDate)";
                    where = 1;
                }
                else
                {
                    dateRA = " and (delDate between @fromDate and @toDate)";
                }

            }
            string conV = string.Empty;
            string conV1 = string.Empty;
            string conVA = string.Empty;
            if (!salesid.Equals("") && salesCheck.Equals(true))
            {
                
                command.Parameters.AddWithValue("@salesid", salesid);
                if (where.Equals(0))
                {
                    conVA = " where salesidgivenbyvloc=@salesid";
                    where = 1;
                }
                else
                {
                    conVA = " and salesidgivenbyvloc=@salesid";
                }
            }

            if (!barcode.Equals("") && barcodeCheck.Equals(true))
            {
                
                command.Parameters.AddWithValue("@barcode", barcode);
                if (where.Equals(0))
                {
                    conVA = " where BarcodeNo=@barcode";
                    where = 1;
                }
                else
                {
                    conVA = " and BarcodeNo=@barcode";
                }
            }

            string query = string.Empty;
            string query1 = string.Empty;

            
            query1 += "select tab.BarcodeNo,i.Title,c.ItemCategory,col1.C1Name,l.username,'delete after sold' as msg,salesidgivenbyvloc,loc.Location,salesc.courierName as salesCourier,salesDateTime," +
                " salesLog.username as salesUser,retLog.username as retUser,retc.courierName as retCourier,tab.dispatchtimestamp,disLog.username as dispatchUser,tab.creasons,tab.returntimestamp,tab.remarks,tab.salesAbwno,tab.delDate as deltimestamp,tab.reasons as delreason,tab.returnAbwno " +
                "from (select d.*, c.status as canstatus, c.salesidgivenbyvloc, c.saleschannelvlocid, c.salesCourier, c.salesDateTime," +
                "c.salesUserId, c.returnCourier, c.returnAbwno, c.returnuserid, c.dispatchtimestamp,c.dispatchuserid, c.reasons as creasons, c.remarks, c.returntimestamp, c.salesAbwno from deleteStockUpInward d " +
                "inner join cancelTrans c on c.itemid = d.StockupID" + dateRA + "" + conVA + ") tab  inner join ItemStyle i on i.StyleID = tab.StyleID " +
                "inner join ItemCategory c on c.ItemCategoryID = i.ItemCatID " +
                "inner join Column1 col1 on col1.Col1ID = i.Col1 " +
                "inner join login l on l.userid = tab.delUserId " +
                "left join Location loc on loc.LocationID = tab.saleschannelvlocid " +
                "left join courier salesc on salesc.courierId = tab.salesCourier " +
                "left join login salesLog on salesLog.userid = tab.salesUserId " +
                "left join login disLog on disLog.userid = tab.dispatchUserId " +
                "left join courier retc on retc.courierId = tab.returnCourier " +
                "left join login retLog on retLog.userid = tab.returnuserid";


            command.CommandText = query1;
            DataTable dt2 = new DataTable();
            dt2.Load(command.ExecuteReader());

            //int cnt = dt2.Rows.Count;

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();
            return dt2;
        }
        catch (Exception ex)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
            return mergeTable;
        }

    }

    public DataTable getDeletedpick(string fromDate, string toDate, string barcode, string salesid, bool salesCheck, bool barcodeCheck, bool dateRange)
    {
        DataTable mergeTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("getDelBarc");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            string dateR = string.Empty;
            string dateRA = string.Empty;
            int where = 0;
            if (dateRange.Equals(true))
            {

                command.Parameters.AddWithValue("@fromDate", fromDate);
                command.Parameters.AddWithValue("@toDate", toDate);
                if (where.Equals(0))
                {
                    dateRA = " where (cancelTimeStamp between @fromDate and @toDate)";
                    where = 1;
                }
                else
                {
                    dateRA = " and (cancelTimeStamp between @fromDate and @toDate)";
                }

            }
            string conV = string.Empty;
            string conV1 = string.Empty;
            string conVA = string.Empty;
            if (!salesid.Equals("") && salesCheck.Equals(true))
            {

                command.Parameters.AddWithValue("@salesid", salesid);
                if (where.Equals(0))
                {
                    conVA = " where salesidgivenbyvloc=@salesid";
                    where = 1;
                }
                else
                {
                    conVA = " and salesidgivenbyvloc=@salesid";
                }
            }

            if (!barcode.Equals("") && barcodeCheck.Equals(true))
            {

                command.Parameters.AddWithValue("@barcode", barcode);
                if (where.Equals(0))
                {
                    conVA = " where BarcodeNo=@barcode";
                    where = 1;
                }
                else
                {
                    conVA = " and BarcodeNo=@barcode";
                }
            }

            string query = string.Empty;
            string query1 = string.Empty;


            query1 += "select tab.BarcodeNo,i.Title,c.ItemCategory,col1.C1Name,l.username,'delete from picklist' as msg,salesidgivenbyvloc,loc.Location,salesc.courierName as salesCourier,salesDateTime," +
                "salesLog.username as salesUser,null as retUser,null as retCourier,tab.dispatchtimestamp,null as dispatchUser,tab.creasons,tab.returntimestamp,tab.remarks,tab.salesAbwno,tab.cancelTimeStamp as deltimestamp,tab.cancleReason as delreason,tab.returnAbwno from " +
                "(select d.*, c.status as canstatus, c.salesidgivenbyvloc, c.saleschannelvlocid, c.salesCourier, c.salesDateTime," +
                "c.salesUserId, c.returnCourier, c.returnAbwno, c.returnuserid, c.dispatchtimestamp, c.dispatchuserid, c.reasons as creasons, c.remarks, c.returntimestamp, c.salesAbwno, c.cancelId, c.cancelTimeStamp, c.cancleReason from ArchiveStockUpInward d inner join cancelTrans c on c.itemid = d.StockupID " + dateRA + "" + conVA + ") tab " +
                "inner join ItemStyle i on i.StyleID = tab.StyleID " +
                "inner join ItemCategory c on c.ItemCategoryID = i.ItemCatID " +
                "inner join Column1 col1 on col1.Col1ID = i.Col1 " +
                "inner join login l on l.userid = tab.cancelId " +
                "left join Location loc on loc.LocationID = tab.saleschannelvlocid " +
                "left join courier salesc on salesc.courierId = tab.salesCourier " +
                "left join login salesLog on salesLog.userid = tab.salesUserId";


            command.CommandText = query1;
            DataTable dt2 = new DataTable();
            dt2.Load(command.ExecuteReader());
            int cnt = dt2.Rows.Count;
            query += "select tab.BarcodeNo,i.Title,c.ItemCategory,col1.C1Name,l.username,'delete from picklist' as msg,salesidgivenbyvloc,loc.Location,salesc.courierName as salesCourier,salesDateTime," +
              "salesLog.username as salesUser,null as retUser,null as retCourier,tab.dispatchtimestamp,null as dispatchUser,tab.creasons,tab.returntimestamp,tab.remarks,tab.salesAbwno,tab.cancelTimeStamp as deltimestamp,tab.cancleReason as delreason,tab.returnAbwno from " +
              "(select d.*, c.status as canstatus, c.salesidgivenbyvloc, c.saleschannelvlocid, c.salesCourier, c.salesDateTime," +
              "c.salesUserId, c.returnCourier, c.returnAbwno, c.returnuserid, c.dispatchtimestamp, c.dispatchuserid, c.reasons as creasons, c.remarks, c.returntimestamp, c.salesAbwno, c.cancelId, c.cancelTimeStamp, c.cancleReason from StockUpInward d inner join cancelTrans c on c.itemid = d.StockupID " + dateRA + "" + conVA + ") tab " +
              "inner join ItemStyle i on i.StyleID = tab.StyleID " +
              "inner join ItemCategory c on c.ItemCategoryID = i.ItemCatID " +
              "inner join Column1 col1 on col1.Col1ID = i.Col1 " +
              "inner join login l on l.userid = tab.cancelId " +
              "left join Location loc on loc.LocationID = tab.saleschannelvlocid " +
              "left join courier salesc on salesc.courierId = tab.salesCourier " +
              "left join login salesLog on salesLog.userid = tab.salesUserId";


            command.CommandText = query;
            DataTable dt1 = new DataTable();
            dt1.Load(command.ExecuteReader());
            //int cnt1 = dt1.Rows.Count;
            dt1.Merge(dt2);
            //int cnt2 = dt1.Rows.Count;
            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();
            return dt1;
        }
        catch (Exception ex)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
            return mergeTable;
        }

    }
}