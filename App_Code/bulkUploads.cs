using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

/// <summary>
/// Summary description for bulkUploads
/// </summary>
public class bulkUploads
{
    string userId = HttpContext.Current.Session["login"].ToString();//id of logged in admin
    string userName = HttpContext.Current.Session["userName"].ToString();//name of logged in user
    public bulkUploads()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable doBulkListing(DataTable dt, string saleschannelvlocid)
    {
        string error = string.Empty;
        string insert = string.Empty;
        string update = string.Empty;
        DataTable succDt = new DataTable();
        DataTable newDT = dt;
        newDT.Columns.Add("Status");
        newDT.Columns.Add("Reason");
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;


        try
        {
            dt.Rows[0].Delete();
            int k = 0;
            int listid = 0;
            foreach (DataRow dRow in dt.Rows)
            {
                // Start a local transaction.
                transaction = connection.BeginTransaction("BulkListing");
                command.Connection = connection;
                command.Transaction = transaction;
                try
                {

                    string sku = dRow["SKU"].ToString();
                    string listidgivenbyvloc = dRow["ListID"].ToString();
                    decimal listprice = Convert.ToDecimal(dRow["Price"].ToString());
                    if (!sku.Equals(""))
                    {
                        if (sku.Contains("-"))
                        {
                            //get style id and cat id
                            command.CommandText = "select StyleID,ItemCatID from ItemStyle where StyleCode = @StyleCode";
                            command.Parameters.AddWithValue("@StyleCode", sku.Split('-')[0]);
                            DataTable style = new DataTable();
                            style.Load(command.ExecuteReader());

                            if (!style.Rows.Count.Equals(0))
                            {
                                //get size id
                                command.CommandText = "select SizeID from Size s where s.ItemCategoryID = @ItemCategoryID and s.Size1 = @Size1";
                                command.Parameters.AddWithValue("@ItemCategoryID", Convert.ToInt32(style.Rows[0]["ItemCatID"].ToString()));
                                command.Parameters.AddWithValue("@Size1", sku.Split('-')[1]);
                                DataTable size = new DataTable();
                                size.Load(command.ExecuteReader());

                                if (!size.Rows.Count.Equals(0))
                                {
                                    //int retSucc = bulkInsertUpload(saleschannelvlocid, listidgivenbyvloc, listprice, Convert.ToInt32(style.Rows[0]["StyleID"].ToString()), Convert.ToInt32(size.Rows[0]["SizeID"].ToString()), userId);
                                    DataTable listDT = new DataTable();
                                    command.CommandText = "select * from listchannelrecord l where l.styleId = @LstyleId and l.sizeId = @LsizeId and l.saleschannelvlocid = @saleschannelvlocid";
                                    command.Parameters.AddWithValue("@LstyleId", Convert.ToInt32(style.Rows[0]["StyleID"].ToString()));
                                    command.Parameters.AddWithValue("@LsizeId", Convert.ToInt32(size.Rows[0]["SizeID"].ToString()));
                                    command.Parameters.AddWithValue("@saleschannelvlocid", Convert.ToInt32(saleschannelvlocid));
                                    listDT.Load(command.ExecuteReader());

                                    if (listDT.Rows.Count.Equals(0))
                                    {
                                        // insert
                                        command.CommandText = "insert into listchannelrecord (saleschannelvlocid,itemid,listidgivenbyvloc,listprice,userId,styleId,sizeId) " +
                                " values (@isaleschannelvlocid,@iitemid,@ilistidgivenbyvloc,@ilistprice,@iuserId,@istyleId,@isizeId) "+
                                "SELECT CAST(scope_identity() AS int)"; 
                                        command.Parameters.AddWithValue("@isaleschannelvlocid", Convert.ToInt32(saleschannelvlocid));
                                        command.Parameters.AddWithValue("@iitemid", 0);
                                        command.Parameters.AddWithValue("@ilistidgivenbyvloc", listidgivenbyvloc);
                                        command.Parameters.AddWithValue("@ilistprice", listprice);
                                        command.Parameters.AddWithValue("@iuserId", userId);
                                        command.Parameters.AddWithValue("@istyleId", Convert.ToInt32(style.Rows[0]["StyleID"].ToString()));
                                        command.Parameters.AddWithValue("@isizeId", Convert.ToInt32(size.Rows[0]["SizeID"].ToString()));
                                        listid = (Int32)command.ExecuteScalar();
                                        command.Parameters.Clear();

                                        // update item style for sooperbuy website
                                        if (saleschannelvlocid.ToString().Equals("20"))
                                        {
                                            command.CommandText = "update ItemStyle set onWebsite=1 where StyleID=@weStyleId";
                                            command.Parameters.AddWithValue("@weStyleId", style.Rows[0]["StyleID"].ToString());
                                            command.ExecuteNonQuery();
                                            command.Parameters.Clear();
                                        }

                                        string newsku = sku.Replace("-", "/");

                                        //list log
                                        command.CommandText = "insert into list_log (sku,locid,userid,type,datetime,styleid,sizeid,listid) values(@sku,@locid,@userid,@type,@datetime,@styleid,@sizeid,@listid)";
                                        command.Parameters.AddWithValue("@sku", newsku);
                                        command.Parameters.AddWithValue("@locid", saleschannelvlocid);
                                        command.Parameters.AddWithValue("@userid", userId);
                                        command.Parameters.AddWithValue("@type", "Bulk");
                                        command.Parameters.AddWithValue("@datetime", DateTime.Now);
                                        command.Parameters.AddWithValue("@styleid", Convert.ToInt32(style.Rows[0]["StyleID"].ToString()));
                                        command.Parameters.AddWithValue("@sizeid", Convert.ToInt32(size.Rows[0]["SizeID"].ToString()));
                                        command.Parameters.AddWithValue("@listid", listid);
                                        command.ExecuteNonQuery();

                                        insert += sku + Environment.NewLine;
                                        newDT.Rows[k]["Status"] = "Success";
                                        newDT.Rows[k]["Reason"] = "Inserted SKU";
                                        
                                        transaction.Commit();
                                        command.Parameters.Clear();
                                    }
                                    else
                                    {
                                        //update
                                        command.CommandText = "update listchannelrecord set listidgivenbyvloc=@ulistidgivenbyvloc,listprice=@ulistprice,userId=@uuserId WHERE listid=@ulistid ";
                                        command.Parameters.AddWithValue("@ulistid", Convert.ToInt32(listDT.Rows[0]["listid"].ToString()));
                                        command.Parameters.AddWithValue("@ulistidgivenbyvloc", listidgivenbyvloc);
                                        command.Parameters.AddWithValue("@ulistprice", listprice);
                                        command.Parameters.AddWithValue("@uuserId", userId);
                                        command.ExecuteNonQuery();
                                        command.Parameters.Clear();

                                        string newsku = sku.Replace("-", "/");

                                        // update item style for sooperbuy website
                                        if (saleschannelvlocid.ToString().Equals("20"))
                                        {
                                            command.CommandText = "update ItemStyle set onWebsite=1 where StyleID=@uweStyleId";
                                            command.Parameters.AddWithValue("@uweStyleId", style.Rows[0]["StyleID"].ToString());
                                            command.ExecuteNonQuery();
                                            command.Parameters.Clear();
                                        }

                                        //list log
                                        command.CommandText = "insert into list_log (sku,locid,userid,type,datetime,styleid,sizeid,listid) values(@sku,@locid,@userid,@type,@datetime,@styleid,@sizeid,@listid)";
                                        command.Parameters.AddWithValue("@sku", newsku);
                                        command.Parameters.AddWithValue("@locid", saleschannelvlocid);
                                        command.Parameters.AddWithValue("@userid", userId);
                                        command.Parameters.AddWithValue("@type", "Bulk");
                                        command.Parameters.AddWithValue("@datetime", DateTime.Now);
                                        command.Parameters.AddWithValue("@styleid", Convert.ToInt32(style.Rows[0]["StyleID"].ToString()));
                                        command.Parameters.AddWithValue("@sizeid", Convert.ToInt32(size.Rows[0]["SizeID"].ToString()));
                                        command.Parameters.AddWithValue("@listid", Convert.ToInt32(listDT.Rows[0]["listid"].ToString()));
                                        command.ExecuteNonQuery();
                                        
                                        update += sku + Environment.NewLine;
                                        newDT.Rows[k]["Status"] = "Success";
                                        newDT.Rows[k]["Reason"] = "Updated SKU";
                                        
                                        transaction.Commit();
                                        command.Parameters.Clear();
                                    }
                                }
                                else
                                {
                                    transaction.Rollback();
                                    error += "Size not Found-" + sku + Environment.NewLine;
                                    newDT.Rows[k]["Status"] = "Failed";
                                    newDT.Rows[k]["Reason"] = "Size not Found";
                                }
                            }
                            else
                            {
                                transaction.Rollback();
                                error += "SKU not Found-" + sku + Environment.NewLine;
                                newDT.Rows[k]["Status"] = "Failed";
                                newDT.Rows[k]["Reason"] = "SKU not Found";
                            }
                        }
                        else
                        {
                            transaction.Rollback();
                            newDT.Rows[k]["Status"] = "Failed";
                            newDT.Rows[k]["Reason"] = "Incorrect SKU Format";
                        }

                        command.Parameters.Clear();
                    }
                    else
                    {
                        transaction.Rollback();
                        newDT.Rows[k]["Status"] = "Failed";
                        newDT.Rows[k]["Reason"] = "Empty SKU";
                    }
                    
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    RecordExceptionCls rex = new RecordExceptionCls();
                    rex.recordException(ex);
                    error += "Transaction Rolled Back";
                    newDT.Rows[k]["Status"] = "Failed";
                    newDT.Rows[k]["Reason"] = "Transaction Rolled Back";
                }
                k++;
            }

            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        catch (Exception ex)
        {
            try
            {
                //transaction.Rollback();
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex);
                error += "Transaction Rolled Back";

            }
            catch (Exception ex2)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex2);
                error += "Some error Occured";

            }
        }

        succDt.Columns.Add("Error");
        succDt.Columns.Add("Insert");
        succDt.Columns.Add("Update");
        succDt.Rows.Add(error, insert, update);
        return newDT;
    }

    public int bulkInsertUpload(string saleschannelvlocid, string listidgivenbyvloc, decimal listprice, int v1, int v2, string userId)
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
        transaction = connection.BeginTransaction("BulkListing");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            return 2;
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
                return 3;

            }
            catch (Exception ex2)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex2);
                return 3;

            }
        }
    }

    public DataTable getSoldBarcode(DataTable dtBarcode)
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
        transaction = connection.BeginTransaction("getSldBarc");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            string soldMarked = string.Empty;
            string noStock = string.Empty;
            DataTable newDt = new DataTable();
            newDt.Columns.Add("Barcode");
            newDt.Columns.Add("Status");

            /*string barcode = "'"+string.Join("','", dtBarcode.AsEnumerable().Select(r => r.Field<string>("barcode")).ToArray())+ "'";
            command.CommandText = "select * from StockUpInward s where s.Status = @Status and s.BarcodeNo in (@BarcodeNo)";
            command.Parameters.AddWithValue("@Status","SOLD");
            command.Parameters.AddWithValue("@BarcodeNo", barcode);
            succDt.Load(command.ExecuteReader());*/
            
            foreach (DataRow row in dtBarcode.Rows)
            {
                DataTable succDt = new DataTable();
                command.CommandText = "select * from StockUpInward s where s.Status = @Status and s.BarcodeNo = @BarcodeNo";
                command.Parameters.AddWithValue("@Status", "SOLD");
                command.Parameters.AddWithValue("@BarcodeNo", row["barcode"].ToString());
                succDt.Load(command.ExecuteReader());
                if (!succDt.Rows.Count.Equals(0))
                {
                    soldMarked += row["barcode"].ToString() + ",";
                    newDt.Rows.Add(row["barcode"].ToString(), "Barcode Marked SOLD");
                    /*newDt.Rows[k]["Barcode"] = row["barcode"].ToString();
                    newDt.Rows[k]["Status"] = "Barcode Marked SOLD";*/
                }

                DataTable stockDt = new DataTable();
                command.CommandText = "select * from StockUpInward s where s.BarcodeNo = @BarcodeNo1";
                command.Parameters.AddWithValue("@BarcodeNo1", row["barcode"].ToString());
                stockDt.Load(command.ExecuteReader());
                if (stockDt.Rows.Count.Equals(0))
                {
                    noStock += row["barcode"].ToString() + ",";
                    newDt.Rows.Add(row["barcode"].ToString(), "Barcode Not Available in STOCK");
                    /*newDt.Rows[k]["Barcode"] = row["barcode"].ToString();
                    newDt.Rows[k]["Status"] = "Barcode are Not Available in STOCK";*/
                }

                command.Parameters.Clear();
               
            }
            /*string err = string.Empty;
            if (!soldMarked.Equals(""))
            {
                err += "Following Barcodes are Already Marked SOLD : <br>" + soldMarked + "<br>";
            }
            if (!noStock.Equals(""))
            {
                err += "Following Barcodes are Not Available in STOCK : <br>" + noStock;
            }*/
            transaction.Commit();

            if (connection.State == ConnectionState.Open)
                connection.Close();
            return newDt;
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
                return null;

            }
            catch (Exception ex2)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex2);
                return null;

            }
        }


    }

    public int doBulkSales(DataTable field, string salesId, string custname,
        string address1, string address2, string city, string stateID,
        string virtualLocation, string paymentMode, string salesDate,string phoneNo)
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
        transaction = connection.BeginTransaction("bulkSales");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            int stats = 1;
            command.CommandText = "select Status from StockUpInward where BarcodeNo=@checkBarcodeNo";
            command.Parameters.AddWithValue("@checkBarcodeNo", field.Rows[0]["barcode"].ToString());
            DataTable check = new DataTable();
            check.Load(command.ExecuteReader());
            if (check.Rows[0]["Status"].Equals("SOLD"))
            {
                transaction.Rollback();
                stats = -2;
            }
            else
            {
                int invoiceid = 0;
                // insert in invoice
                command.CommandText = "insert into invoice (custname,address1,address2,city,state,paymentMode,salesDate,userId,invType,phoneNo,physicalID)" +
                    "values (@custname,@address1,@address2,@city,@state,@paymentMode,@salesDate,@userId,@invType,@phoneNo,@physicalID)" +
                    "SELECT CAST(scope_identity() AS int)";
                command.Parameters.AddWithValue("@custname", custname);
                command.Parameters.AddWithValue("@address1", address1);
                command.Parameters.AddWithValue("@address2", address2);
                command.Parameters.AddWithValue("@city", city);
                command.Parameters.AddWithValue("@state", stateID);

                command.Parameters.AddWithValue("@paymentMode", paymentMode);
                command.Parameters.AddWithValue("@salesDate", Convert.ToDateTime(salesDate).ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@userid", userId);
                command.Parameters.AddWithValue("@invType", "Bulk");
                command.Parameters.AddWithValue("@phoneNo", phoneNo);                
                command.Parameters.AddWithValue("@physicalID", Convert.ToInt32(HttpContext.Current.Session["physicalLocation"].ToString()));
                invoiceid = (Int32)command.ExecuteScalar();
                decimal totSum = Convert.ToDecimal(0.0);

                // in one loop
                foreach (DataRow row in field.Rows)
                {
                    // FETCH HSN CODE
                    DataTable hsnDt = new DataTable();
                    command.CommandText = "select h.*,si.StockupID,si.mrp from hsnmaster h inner join ItemCategory c on h.hsnid=c.hsnid inner join ItemStyle s on s.ItemCatID=c.ItemCategoryID inner join StockUpInward si on si.StyleID=s.StyleID where si.BarcodeNo = @BarcodeNo and si.Status!=@checkStatus";
                    command.Parameters.AddWithValue("@BarcodeNo", row["barcode"].ToString());
                    command.Parameters.AddWithValue("@checkStatus", "SOLD");
                    hsnDt.Load(command.ExecuteReader());

                    // total sum
                    totSum += Convert.ToDecimal(hsnDt.Rows[0]["mrp"]);

                    //calculate hsn
                    decimal lowhighpt = Convert.ToDecimal(hsnDt.Rows[0]["lowhighpt"]);
                    decimal ligst = Convert.ToDecimal(hsnDt.Rows[0]["ligst"]);
                    decimal higst = Convert.ToDecimal(hsnDt.Rows[0]["higst"]);
                    decimal taxableamnt = 0;
                    decimal igst = 0;
                    decimal cgst = 0;
                    decimal sgst = 0;
                    decimal gst = 0;
                    decimal amnt = ((Convert.ToDecimal(hsnDt.Rows[0]["mrp"])) - ((Convert.ToDecimal(hsnDt.Rows[0]["mrp"]) * ligst) / 100));
                    if (amnt <= lowhighpt)
                    {
                        taxableamnt = amnt;
                        igst = ((Convert.ToDecimal(hsnDt.Rows[0]["mrp"]) * ligst) / 100);
                        gst = ligst;
                    }
                    else
                    {
                        taxableamnt = ((Convert.ToDecimal(hsnDt.Rows[0]["mrp"])) - ((Convert.ToDecimal(hsnDt.Rows[0]["mrp"]) * higst) / 100));
                        igst = ((Convert.ToDecimal(hsnDt.Rows[0]["mrp"]) * higst) / 100);
                        gst = higst;
                    }
                    cgst = igst / 2;
                    sgst = igst / 2;
                    if (stateID.Equals("27"))
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
                    // insert in sales
                    command.CommandText = "insert into salesrecord" +
                        " (invoiceid,saleschannelvlocid,salesidgivenbyvloc," +
                        "sellingprice,status,itemid,gstpercent,taxableamount,cgstamnt," +
                        "sgstamnt,igstamnt,salesUserId) values " +
                        "(@invoiceid,@saleschannelvlocid,@salesidgivenbyvloc," +
                        "@sellingprice,@status,@itemid,@gstpercent,@taxableamount,@cgstamnt,@sgstamnt,@igstamnt,@salesUserId" +
                        ")";
                    command.Parameters.AddWithValue("@invoiceid", invoiceid);
                    command.Parameters.AddWithValue("@saleschannelvlocid", virtualLocation);
                    command.Parameters.AddWithValue("@salesidgivenbyvloc", salesId);
                    command.Parameters.AddWithValue("@sellingprice", Convert.ToDecimal(hsnDt.Rows[0]["mrp"]));
                    command.Parameters.AddWithValue("@status", "ND");
                    command.Parameters.AddWithValue("@itemid", hsnDt.Rows[0]["StockupId"]);
                    command.Parameters.AddWithValue("@gstpercent", gst);
                    command.Parameters.AddWithValue("@taxableamount", taxableamnt);
                    command.Parameters.AddWithValue("@igstamnt", igst);
                    command.Parameters.AddWithValue("@cgstamnt", cgst);
                    command.Parameters.AddWithValue("@sgstamnt", sgst);

                    command.Parameters.AddWithValue("@salesUserId", userId);
                    command.ExecuteNonQuery();
                    // update sold status in stockupinward
                    command.CommandText = "update StockUpInward set Status=@Status12 where StockUpID=@UStockUpID";
                    command.Parameters.AddWithValue("@Status12", "SOLD");
                    command.Parameters.AddWithValue("@UStockUpID", hsnDt.Rows[0]["StockupId"]);

                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                }

                // update invoice with total SUM
                command.CommandText = "update invoice set total=@itotal where invid=@iinvid";
                command.Parameters.AddWithValue("@itotal", totSum);
                command.Parameters.AddWithValue("@iinvid", invoiceid);
                command.ExecuteNonQuery();

                transaction.Commit();
                stats = 1;
            }



            if (connection.State == ConnectionState.Open)
                connection.Close();
            return stats;
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

    public DataTable getBulkInvoice()
    {
        DataTable dt = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("BInv");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            string conditions = string.Empty;
            //command.CommandText = "select invid,concat(invid,'-',custname,'-',total) as dropName from invoice i where invType='Bulk' order by invid desc";
            if(HttpContext.Current.Session["uType"].ToString().Equals("2"))
            {
                conditions += " and physicalID="+ HttpContext.Current.Session["physicalLocation"].ToString();
            }
            command.CommandText = "select invid,concat(invid,'-',custname,'-',total) as dropName from invoice i inner join salesrecord s on s.status='ND' and s.invoiceid=i.invid where invType='Bulk'"+ conditions + " group by invid,custname,total order by invid desc";
            dt.Load(command.ExecuteReader());
            transaction.Commit();

            if (connection.State == ConnectionState.Open)
                connection.Close();
            return dt;
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
                return dt;

            }
            catch (Exception ex2)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex2);
                return dt;

            }
        }


    }

    public DataTable getSoldBarcodeByInvoice(string invoiceid)
    {
        DataTable dt = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("BarcInv");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select si.BarcodeNo,si.StockupID,i.Title,c.ItemCategory,s.sid,s.salesidgivenbyvloc,s.sellingprice,s.sellingprice as actualSP from salesrecord s inner join StockUpInward si on si.StockupID=s.itemid inner join ItemStyle i on i.StyleID=si.StyleID inner join ItemCategory c on c.ItemCategoryID=i.ItemCatID where s.status = @status and invoiceid = @invoiceid";
            command.Parameters.AddWithValue("@status", "ND");
            command.Parameters.AddWithValue("@invoiceid", invoiceid);
            dt.Load(command.ExecuteReader());
            transaction.Commit();

            if (connection.State == ConnectionState.Open)
                connection.Close();
            return dt;
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
                return dt;

            }
            catch (Exception ex2)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex2);
                return dt;

            }
        }


    }

    public int doBulkDispatch(string invid, string salesabwno, string courier, DataTable dt, string custname, string address1, string address2, string city, string stateID,string phoneNo)
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
        transaction = connection.BeginTransaction("dispatchB");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            foreach (DataRow rows in dt.Rows)
            {
                //insert into ArchiveStockUpInward
                command.CommandText = "INSERT INTO ArchiveStockUpInward (StyleID, UserID, BagID, SizeID, DateTime, LastBarcode, StyleCode, BarcodeNo, RFLQty, RejectQty, Status, LocationID, ExpiredDate, ListingDate, ModeOfPayment, SoldAmount, DispatchedDate, SalesDate, RackBarcode, RackDate, CancelReason, CancelDate, ListedUserID, Picked, BulkID, BulkDate, MoneyIn, MoneyOut, ItemID, SalesID, RecordNo, CourierTransactionID, mrp, StockupID,SystemDate,printed,oldBarcode,initialStatus,labels,mfgDate,labelUserId,physicalId,subLocId,rackId,stackId)" +
                    " SELECT StyleID, UserID, BagID, SizeID, DateTime, LastBarcode, StyleCode, BarcodeNo, RFLQty, RejectQty, Status, LocationID, ExpiredDate, ListingDate, ModeOfPayment, SoldAmount, DispatchedDate, SalesDate, RackBarcode, RackDate, CancelReason, CancelDate, ListedUserID, Picked, BulkID, BulkDate, MoneyIn, MoneyOut, ItemID, SalesID, RecordNo, CourierTransactionID, mrp, StockupID,SystemDate,printed,oldBarcode,initialStatus,labels,mfgDate,labelUserId,physicalId,subLocId,rackId,stackId FROM StockUpInward WHERE StockupID=@StockupID " +
                    " SELECT CAST(scope_identity() AS int)";
                command.Parameters.AddWithValue("@StockupID", Convert.ToInt32(rows["StockupID"]));
                int archiveid = (Int32)command.ExecuteScalar();


                // FETCH HSN CODE
                DataTable hsnDt = new DataTable();
                command.CommandText = "select h.*,si.StockupID,si.mrp from hsnmaster h inner join ItemCategory c on h.hsnid=c.hsnid inner join ItemStyle s on s.ItemCatID=c.ItemCategoryID inner join StockUpInward si on si.StyleID=s.StyleID where si.BarcodeNo = @BarcodeNo";
                command.Parameters.AddWithValue("@BarcodeNo", rows["barcode"].ToString());
                hsnDt.Load(command.ExecuteReader());

                //calculate hsn
                decimal lowhighpt = Convert.ToDecimal(hsnDt.Rows[0]["lowhighpt"]);
                decimal ligst = Convert.ToDecimal(hsnDt.Rows[0]["ligst"]);
                decimal higst = Convert.ToDecimal(hsnDt.Rows[0]["higst"]);
                decimal taxableamnt = 0;
                decimal igst = 0;
                decimal cgst = 0;
                decimal sgst = 0;
                decimal gst = 0;
                decimal amnt = ((Convert.ToDecimal(rows["sellingprice"])) - ((Convert.ToDecimal(rows["sellingprice"]) * ligst) / 100));
                if (amnt <= lowhighpt)
                {
                    taxableamnt = amnt;
                    igst = ((Convert.ToDecimal(rows["sellingprice"]) * ligst) / 100);
                    gst = ligst;
                }
                else
                {
                    taxableamnt = ((Convert.ToDecimal(rows["sellingprice"])) - ((Convert.ToDecimal(rows["sellingprice"]) * higst) / 100));
                    igst = ((Convert.ToDecimal(rows["sellingprice"]) * higst) / 100);
                    gst = higst;
                }
                cgst = igst / 2;
                sgst = igst / 2;
                if (stateID.Equals("27"))
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



                // get gst amount.. 
                /*command.CommandText = "select * from salesrecord where sid=@gSalesid";
                command.Parameters.AddWithValue("@gSalesid", Convert.ToInt32(rows["sid"]));
                DataTable salesDT = new DataTable();
                salesDT.Load(command.ExecuteReader());
                decimal igst = Convert.ToDecimal(salesDT.Rows[0]["igstamnt"]);
                decimal cgst = Convert.ToDecimal(salesDT.Rows[0]["cgstamnt"]);
                decimal sgst = Convert.ToDecimal(salesDT.Rows[0]["sgstamnt"]);
                if (stateID.Equals("27"))
                {
                    cgst = igst / 2;
                    sgst = igst / 2;
                    igst = Convert.ToDecimal(0.0);

                }
                else
                {
                    igst = igst + cgst + sgst;
                    cgst = Convert.ToDecimal(0.0);
                    sgst = Convert.ToDecimal(0.0);
                }*/

                //update sales record
                command.CommandText = "update salesrecord set status=@status,itemid=@itemid,archiveid=@archiveid,dispatchtimestamp=@dispatchtimestamp,follwUpTime=@follwUpTime,dispatchuserid=@dispatchuserid,salesCourier=@salesCourier,salesAbwno=@salesAbwno,igstamnt=@igstamnt,cgstamnt=@cgstamnt,sgstamnt=@sgstamnt,sellingprice=@SaleSp,taxableamount=@taxableamount WHERE itemid=@StockupID and status='ND'";

                command.Parameters.AddWithValue("@status", "DISPATCHED");
                command.Parameters.AddWithValue("@itemid", -1);
                command.Parameters.AddWithValue("@archiveid", archiveid);
                command.Parameters.AddWithValue("@dispatchtimestamp", DateTime.Now);
                command.Parameters.AddWithValue("@follwUpTime", DateTime.Now);
                command.Parameters.AddWithValue("@dispatchuserid", userId);
                command.Parameters.AddWithValue("@salesCourier", courier);
                command.Parameters.AddWithValue("@salesAbwno", salesabwno);
                command.Parameters.AddWithValue("@igstamnt", igst);
                command.Parameters.AddWithValue("@cgstamnt", cgst);
                command.Parameters.AddWithValue("@sgstamnt", sgst);
                command.Parameters.AddWithValue("@SaleSp", Convert.ToDecimal(rows["sellingprice"]));
                command.Parameters.AddWithValue("@taxableamount", taxableamnt);
                command.ExecuteNonQuery();


                //Delete from StockUpInward
                command.CommandText = "DELETE FROM StockUpInward WHERE StockupID=@StockupID ";
                command.ExecuteNonQuery();



                command.Parameters.Clear();
            }

            // get all salesrecord
            command.CommandText = "select * from salesrecord where invoiceid=@Sinvoiceid";
            command.Parameters.AddWithValue("@Sinvoiceid", invid);
            DataTable salesrecordDt = new DataTable();
            salesrecordDt.Load(command.ExecuteReader());
            decimal totInvAmnt = Convert.ToDecimal(0.0);
            // loop through to set gst of unchecked items (State can be changed again n again)
            foreach (DataRow salerows in salesrecordDt.Rows)
            {
                totInvAmnt += Convert.ToDecimal(salerows["sellingprice"]);
                decimal igst1 = Convert.ToDecimal(salerows["igstamnt"]);
                decimal cgst1 = Convert.ToDecimal(salerows["cgstamnt"]);
                decimal sgst1 = Convert.ToDecimal(salerows["sgstamnt"]);
                decimal gstt = (igst1 + cgst1 + sgst1);
                if (stateID.Equals("27"))
                {
                    cgst1 = gstt / 2;
                    sgst1 = gstt / 2;
                    igst1 = Convert.ToDecimal(0.0);

                }
                else
                {
                    igst1 = gstt;
                    cgst1 = Convert.ToDecimal(0.0);
                    sgst1 = Convert.ToDecimal(0.0);
                }

                //update sales record
                command.CommandText = "update salesrecord set igstamnt=@igstamnt,cgstamnt=@cgstamnt,sgstamnt=@sgstamnt WHERE sid=@Ssid";
                command.Parameters.AddWithValue("@igstamnt", igst1);
                command.Parameters.AddWithValue("@cgstamnt", cgst1);
                command.Parameters.AddWithValue("@sgstamnt", sgst1);
                command.Parameters.AddWithValue("@Ssid", salerows["sid"]);
                command.ExecuteNonQuery();

                command.Parameters.Clear();
            }

            //update invoice table
            command.CommandText = "update invoice set custname=@custname," +
            "address1=@address1,address2=@address2,city=@city,state=@state,total=@totalss,phoneNo=@phoneNo where invid=@invid";
            command.Parameters.AddWithValue("@custname", custname);
            command.Parameters.AddWithValue("@address1", address1);
            command.Parameters.AddWithValue("@address2", address2);
            command.Parameters.AddWithValue("@city", city);
            command.Parameters.AddWithValue("@state", stateID);
            command.Parameters.AddWithValue("@invid", invid);
            command.Parameters.AddWithValue("@totalss", totInvAmnt);
            command.Parameters.AddWithValue("@phoneNo", phoneNo);
            command.ExecuteNonQuery();
            command.Parameters.Clear();


            transaction.Commit();
            command.Parameters.Clear();
            if (connection.State == ConnectionState.Open)
                connection.Close();
            return 0;
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

    public DataTable doBulkSalesExcel(DataTable dt)
    {
        string error = string.Empty;

        DataTable newDT = dt;
        newDT.Columns.Add("Barcode");
        newDT.Columns.Add("RackBarcode"); 
        newDT.Columns.Add("Status");
        newDT.Columns.Add("Reason");
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;


        try
        {
            //dt.Rows[0].Delete();
            int k = 0;
            foreach (DataRow dRow in dt.Rows)
            {
                // Start a local transaction.
                transaction = connection.BeginTransaction("bulkSalesEX");
                command.Connection = connection;
                command.Transaction = transaction;
                try
                {

                    string sku = dRow["SKU"].ToString();
                    decimal sp = Convert.ToDecimal(dRow["SP"].ToString());
                    string customerName = dRow["customerName"].ToString();
                    string salesDate = dRow["salesDate"].ToString();
                    string salesID = dRow["salesID"].ToString();
                    string paymentMode = dRow["paymentMode"].ToString();
                    string phoneNo = dRow["ContactNo"].ToString();
                    string saleschannelvlocid = dRow["VirtualLocation"].ToString();
                    command.CommandText = "select * from Location where LocationID=@vlocid";
                    command.Parameters.AddWithValue("@vlocid", saleschannelvlocid);
                    DataTable vloc = new DataTable();
                    vloc.Load(command.ExecuteReader());
                    if (!sku.Equals("") && !vloc.Rows.Count.Equals(0))
                    {
                        if (sku.Contains("-"))
                        {
                            string newsku = sku.Replace("-", "/");
                            newsku = newsku + "/";
                            //get style id and cat id
                            command.CommandText = "select top 1 * from StockUpInward where Status = 'RFL' and BarcodeNo like '" + newsku + "%' order by StockupID asc";
                            DataTable stockup = new DataTable();
                            stockup.Load(command.ExecuteReader());

                            if (!stockup.Rows.Count.Equals(0))
                            {
                                string stateID = "1";
                                int invoiceid = 0;
                                // insert in invoice
                                command.CommandText = "insert into invoice (custname,total,state,paymentMode,salesDate,userId,invType,phoneNo)" +
                                    "values (@custname,@total,@state,@paymentMode,@salesDate,@userId,@invType,@phoneNo)" +
                                    "SELECT CAST(scope_identity() AS int)";
                                command.Parameters.AddWithValue("@custname", customerName);
                                command.Parameters.AddWithValue("@state", stateID);
                                command.Parameters.AddWithValue("@paymentMode", paymentMode);
                                command.Parameters.AddWithValue("@salesDate", Convert.ToDateTime(salesDate).ToString("yyyy-MM-dd"));
                                command.Parameters.AddWithValue("@userid", userId);
                                command.Parameters.AddWithValue("@invType", "BulkEx");
                                command.Parameters.AddWithValue("@total", Convert.ToDecimal(sp));
                                command.Parameters.AddWithValue("@phoneNo", phoneNo);
                                invoiceid = (Int32)command.ExecuteScalar();

                                // FETCH HSN CODE
                                DataTable hsnDt = new DataTable();
                                command.CommandText = "select h.*,si.StockupID,si.mrp from hsnmaster h inner join ItemCategory c on h.hsnid=c.hsnid inner join ItemStyle s on s.ItemCatID=c.ItemCategoryID inner join StockUpInward si on si.StyleID=s.StyleID where si.StockupID = @hStockupID and si.Status='RFL'";
                                command.Parameters.AddWithValue("@hStockupID", Convert.ToInt32(stockup.Rows[0]["StockupID"]));
                                hsnDt.Load(command.ExecuteReader());

                                //calculate hsn
                                decimal lowhighpt = Convert.ToDecimal(hsnDt.Rows[0]["lowhighpt"]);
                                decimal ligst = Convert.ToDecimal(hsnDt.Rows[0]["ligst"]);
                                decimal higst = Convert.ToDecimal(hsnDt.Rows[0]["higst"]);
                                decimal taxableamnt = 0;
                                decimal igst = 0;
                                decimal cgst = 0;
                                decimal sgst = 0;
                                decimal gst = 0;
                                decimal amnt = ((Convert.ToDecimal(sp)) - ((Convert.ToDecimal(sp) * ligst) / 100));
                                if (amnt <= lowhighpt)
                                {
                                    taxableamnt = amnt;
                                    igst = ((Convert.ToDecimal(sp) * ligst) / 100);
                                    gst = ligst;
                                }
                                else
                                {
                                    taxableamnt = ((Convert.ToDecimal(sp)) - ((Convert.ToDecimal(sp) * higst) / 100));
                                    igst = ((Convert.ToDecimal(sp) * higst) / 100);
                                    gst = higst;
                                }
                                cgst = igst / 2;
                                sgst = igst / 2;

                                if (stateID.Equals("27"))
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

                                // insert in sales
                                command.CommandText = "insert into salesrecord" +
                                    " (invoiceid,saleschannelvlocid,salesidgivenbyvloc," +
                                    "sellingprice,status,itemid,gstpercent,taxableamount,cgstamnt," +
                                    "sgstamnt,igstamnt,salesUserId) values " +
                                    "(@invoiceid,@saleschannelvlocid,@salesidgivenbyvloc," +
                                    "@sellingprice,@status,@itemid,@gstpercent,@taxableamount,@cgstamnt,@sgstamnt,@igstamnt,@salesUserId" +
                                    ")";
                                command.Parameters.AddWithValue("@invoiceid", invoiceid);
                                command.Parameters.AddWithValue("@saleschannelvlocid", saleschannelvlocid);
                                command.Parameters.AddWithValue("@salesidgivenbyvloc", salesID);
                                command.Parameters.AddWithValue("@sellingprice", Convert.ToDecimal(sp));
                                command.Parameters.AddWithValue("@status", "ND");
                                command.Parameters.AddWithValue("@itemid", stockup.Rows[0]["StockupId"]);
                                command.Parameters.AddWithValue("@gstpercent", gst);
                                command.Parameters.AddWithValue("@taxableamount", taxableamnt);
                                command.Parameters.AddWithValue("@igstamnt", igst);
                                command.Parameters.AddWithValue("@cgstamnt", cgst);
                                command.Parameters.AddWithValue("@sgstamnt", sgst);

                                command.Parameters.AddWithValue("@salesUserId", userId);
                                command.ExecuteNonQuery();
                                // update sold status in stockupinward
                                command.CommandText = "update StockUpInward set Status=@Status12 where StockUpID=@UStockUpID";
                                command.Parameters.AddWithValue("@Status12", "SOLD");
                                command.Parameters.AddWithValue("@UStockUpID", stockup.Rows[0]["StockupId"]);

                                command.ExecuteNonQuery();

                                transaction.Commit();
                                newDT.Rows[k]["Barcode"] = stockup.Rows[0]["BarcodeNo"];
                                newDT.Rows[k]["RackBarcode"] = stockup.Rows[0]["RackBarcode"];
                                newDT.Rows[k]["Status"] = "Success";
                                newDT.Rows[k]["Reason"] = "Invoice Generated";
                                command.Parameters.Clear();
                            }
                            else
                            {
                                transaction.Rollback();
                                error += "SKU not Found-" + sku + Environment.NewLine;
                                newDT.Rows[k]["Barcode"] = "";
                                newDT.Rows[k]["RackBarcode"] = "";
                                newDT.Rows[k]["Status"] = "Failed";
                                newDT.Rows[k]["Reason"] = "SKU not in Stock";
                            }
                        }
                        else
                        {
                            transaction.Rollback();
                            newDT.Rows[k]["Status"] = "Failed";
                            newDT.Rows[k]["Reason"] = "Incorrect SKU Format";
                        }
                    }
                    else
                    {
                        transaction.Rollback();
                        newDT.Rows[k]["Status"] = "Failed";
                        newDT.Rows[k]["Reason"] = "SKU Empty or Virtual Location Not Found";
                    }

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    RecordExceptionCls rex = new RecordExceptionCls();
                    rex.recordException(ex);
                    error += "Transaction Rolled Back";
                    newDT.Rows[k]["Barcode"] = "";
                    newDT.Rows[k]["RackBarcode"] = "";
                    newDT.Rows[k]["Status"] = "Failed";
                    newDT.Rows[k]["Reason"] = "Transaction Rolled Back";
                }
                k++;
                command.Parameters.Clear();
            }

            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        catch (Exception ex)
        {
            try
            {
                //transaction.Rollback();
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex);
                error += "Transaction Rolled Back";

            }
            catch (Exception ex2)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex2);
                error += "Some error Occured";

            }
        }


        return newDT;
    }

    public DataTable doBulkDispatchExcel(DataTable dt, string saleschannelvlocid)
    {
        string error = string.Empty;

        DataTable newDT = dt;
        newDT.Columns.Add("Barcode");
        newDT.Columns.Add("RackBarcode");
        newDT.Columns.Add("Status");
        newDT.Columns.Add("Reason");
        newDT.Columns.Add("SMS");
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;


        try
        {
            //dt.Rows[0].Delete();
            int k = 0;
            foreach (DataRow dRow in dt.Rows)
            {
                // Start a local transaction.
                transaction = connection.BeginTransaction("bulkSalesEX");
                command.Connection = connection;
                command.Transaction = transaction;
                try
                {
                    string salesID = dRow["salesID"].ToString();
                    string sku = dRow["SKU"].ToString();
                    string customerName = dRow["CustomerName"].ToString();
                    string address1 = dRow["Address1"].ToString();
                    string address2 = dRow["Address2"].ToString();
                    string city = dRow["AddressCity"].ToString();
                    string stateName = dRow["AddressState"].ToString();
                    string couriername = dRow["Courier"].ToString();
                    string abwno = dRow["ABWNo"].ToString();
                    string phoneNo = dRow["ContactNo"].ToString();

                    if (!sku.Equals(""))
                    {
                        if (sku.Contains("-"))
                        {
                            string newsku = sku.Replace("-", "/");
                            newsku = newsku + "/";

                            //get stockup , sales , invoice details
                            command.CommandText = "select * from salesrecord s inner join StockUpInward si on si.StockupID=s.itemid inner join invoice i on i.invid=s.invoiceid" +
                                " where BarcodeNo like '" + newsku + "%' and si.status = 'SOLD' and s.status = 'ND' and s.salesidgivenbyvloc = @salesidgivenbyvloc and i.custname = @custname";
                            command.Parameters.AddWithValue("@salesidgivenbyvloc", salesID);
                            command.Parameters.AddWithValue("@custname", customerName);
                            DataTable stockup = new DataTable();
                            stockup.Load(command.ExecuteReader());

                            if (!stockup.Rows.Count.Equals(0))
                            {
                                // check if count is greater than 1
                                if (stockup.Rows.Count > 1)
                                {
                                    transaction.Rollback();
                                    error += "SKU not Found-" + sku + Environment.NewLine;
                                    newDT.Rows[k]["Barcode"] = "";
                                    newDT.Rows[k]["RackBarcode"] = "";
                                    newDT.Rows[k]["Status"] = "Failed";
                                    newDT.Rows[k]["Reason"] = "Total records Found -" + stockup.Rows.Count;
                                    newDT.Rows[k]["SMS"] = "";
                                }
                                else
                                {
                                    // get stateID
                                    command.CommandText = "select * from stateCode where statename like '% - " + stateName + "'";
                                    DataTable stateDt = new DataTable();
                                    stateDt.Load(command.ExecuteReader());
                                    if (stateDt.Rows.Count.Equals(0) || stateDt.Rows.Count > 1)
                                    {
                                        transaction.Rollback();
                                        error += "State not Found-" + sku + Environment.NewLine;
                                        newDT.Rows[k]["Barcode"] = "";
                                        newDT.Rows[k]["RackBarcode"] = "";
                                        newDT.Rows[k]["Status"] = "Failed";
                                        newDT.Rows[k]["Reason"] = "State not Found ";
                                        newDT.Rows[k]["SMS"] = "";
                                    }
                                    else
                                    {
                                        // check if courier available 
                                        command.CommandText = "select * from courier where courierName=@courierName";
                                        command.Parameters.AddWithValue("@courierName", couriername);
                                        DataTable courierDt = new DataTable();
                                        courierDt.Load(command.ExecuteReader());
                                        if (courierDt.Rows.Count.Equals(0))
                                        {
                                            transaction.Rollback();
                                            error += "Courier not Found-" + sku + Environment.NewLine;
                                            newDT.Rows[k]["Barcode"] = "";
                                            newDT.Rows[k]["RackBarcode"] = "";
                                            newDT.Rows[k]["Status"] = "Failed";
                                            newDT.Rows[k]["Reason"] = "Courier not Found ";
                                            newDT.Rows[k]["SMS"] = "";
                                        }
                                        else
                                        {
                                            //insert into ArchiveStockUpInward
                                            command.CommandText = "INSERT INTO ArchiveStockUpInward (StyleID, UserID, BagID, SizeID, DateTime, LastBarcode, StyleCode, BarcodeNo, RFLQty, RejectQty, Status, LocationID, ExpiredDate, ListingDate, ModeOfPayment, SoldAmount, DispatchedDate, SalesDate, RackBarcode, RackDate, CancelReason, CancelDate, ListedUserID, Picked, BulkID, BulkDate, MoneyIn, MoneyOut, ItemID, SalesID, RecordNo, CourierTransactionID, mrp, StockupID,SystemDate,printed,oldBarcode,initialStatus,labels,mfgDate,labelUserId,physicalId,subLocId,rackId,stackId)" +
                                                " SELECT StyleID, UserID, BagID, SizeID, DateTime, LastBarcode, StyleCode, BarcodeNo, RFLQty, RejectQty, Status, LocationID, ExpiredDate, ListingDate, ModeOfPayment, SoldAmount, DispatchedDate, SalesDate, RackBarcode, RackDate, CancelReason, CancelDate, ListedUserID, Picked, BulkID, BulkDate, MoneyIn, MoneyOut, ItemID, SalesID, RecordNo, CourierTransactionID, mrp, StockupID,SystemDate,printed,oldBarcode,initialStatus,labels,mfgDate,labelUserId,physicalId,subLocId,rackId,stackId FROM StockUpInward WHERE StockupID=@StockupID " +
                                                " SELECT CAST(scope_identity() AS int)";
                                            command.Parameters.AddWithValue("@StockupID", Convert.ToInt32(stockup.Rows[0]["StockupID"]));
                                            int archiveid = (Int32)command.ExecuteScalar();

                                            //Delete from StockUpInward
                                            command.CommandText = "DELETE FROM StockUpInward WHERE StockupID=@StockupID ";
                                            command.ExecuteNonQuery();

                                            // get gst amount.. 
                                            command.CommandText = "select * from salesrecord where sid=@gSalesid";
                                            command.Parameters.AddWithValue("@gSalesid", Convert.ToInt32(stockup.Rows[0]["sid"]));
                                            DataTable salesDT = new DataTable();
                                            salesDT.Load(command.ExecuteReader());
                                            decimal igst = Convert.ToDecimal(salesDT.Rows[0]["igstamnt"]);
                                            decimal cgst = Convert.ToDecimal(salesDT.Rows[0]["cgstamnt"]);
                                            decimal sgst = Convert.ToDecimal(salesDT.Rows[0]["sgstamnt"]);
                                            decimal gstt = (igst + cgst + sgst);
                                            if (Convert.ToInt32(stateDt.Rows[0]["stateid"]).Equals(Convert.ToInt32(27)))
                                            {
                                                cgst = (gstt) / 2;
                                                sgst = (gstt) / 2;
                                                igst = Convert.ToDecimal(0.0);

                                            }
                                            else
                                            {
                                                igst = gstt;
                                                cgst = Convert.ToDecimal(0.0);
                                                sgst = Convert.ToDecimal(0.0);
                                            }

                                            //update sales record
                                            command.CommandText = "update salesrecord set status=@status,itemid=@itemid,archiveid=@archiveid,dispatchtimestamp=@dispatchtimestamp,follwUpTime=@follwUpTime,dispatchuserid=@dispatchuserid,salesCourier=@salesCourier,salesAbwno=@salesAbwno,igstamnt=@igstamnt,cgstamnt=@cgstamnt,sgstamnt=@sgstamnt WHERE itemid=@StockupID and status='ND'";

                                            command.Parameters.AddWithValue("@status", "DISPATCHED");
                                            command.Parameters.AddWithValue("@itemid", -1);
                                            command.Parameters.AddWithValue("@archiveid", archiveid);
                                            command.Parameters.AddWithValue("@dispatchtimestamp", DateTime.Now);
                                            command.Parameters.AddWithValue("@follwUpTime", DateTime.Now);
                                            command.Parameters.AddWithValue("@dispatchuserid", userId);
                                            command.Parameters.AddWithValue("@salesCourier", courierDt.Rows[0]["courierId"]);
                                            command.Parameters.AddWithValue("@salesAbwno", abwno);
                                            command.Parameters.AddWithValue("@igstamnt", igst);
                                            command.Parameters.AddWithValue("@cgstamnt", cgst);
                                            command.Parameters.AddWithValue("@sgstamnt", sgst);
                                            command.ExecuteNonQuery();

                                            //update invoice table
                                            command.CommandText = "update invoice set custname=@custname1," +
                                                "address1=@address11,address2=@address21,city=@city1,state=@state1,phoneNo=@phoneNo12 where invid=@invid1";
                                            command.Parameters.AddWithValue("@custname1", customerName);
                                            command.Parameters.AddWithValue("@address11", address1);
                                            command.Parameters.AddWithValue("@address21", address2);
                                            command.Parameters.AddWithValue("@city1", city);
                                            command.Parameters.AddWithValue("@state1", stateDt.Rows[0]["stateid"]);
                                            command.Parameters.AddWithValue("@invid1", stockup.Rows[0]["invoiceid"]);
                                            command.Parameters.AddWithValue("@phoneNo12", phoneNo);
                                            command.ExecuteNonQuery();

                                            // get location details
                                            DataTable locationDets = new DataTable();
                                            command.CommandText = "select sendSMS,Location from Location where LocationID=@LocationIDs";
                                            command.Parameters.AddWithValue("@LocationIDs", salesDT.Rows[0]["saleschannelvlocid"].ToString());
                                            locationDets.Load(command.ExecuteReader());
                                            // sms service
                                            DataTable sms = new DataTable();
                                            command.CommandText = "select * from smsService where smsid = @smsid";
                                            command.Parameters.AddWithValue("@smsid", 1);
                                            sms.Load(command.ExecuteReader());

                                            transaction.Commit();
                                            
                                            if (locationDets.Rows[0]["sendSMS"].ToString().Equals("Yes"))
                                            {
                                               
                                                string msg = sms.Rows[0]["smsMessage"].ToString().Replace("{NAME}", customerName);
                                                msg = msg.Replace("PORTAL", locationDets.Rows[0]["Location"].ToString());
                                                msg = msg.Replace("{SALESID}", salesDT.Rows[0]["salesidgivenbyvloc"].ToString());
                                                msg = msg.Replace("{ABWNO}", abwno);
                                                dispatchCls dObj = new dispatchCls();
                                                string res = dObj.sendSMS(phoneNo, msg, sms.Rows[0]["apikey"].ToString(), sms.Rows[0]["smsSender"].ToString());

                                                //string newres = "[" + res + "]";
                                                //DataTable retSms = JsonConvert.DeserializeObject<DataTable>(newres);

                                                //DataTable barcodes = JsonConvert.DeserializeObject<DataTable>(empObj.Rows[0]["Barcode"].ToString());
                                                
                                                string smsStats = "Yes";
                                               

                                                if (res.Contains("failure"))
                                                {
                                                    smsStats = "No";
                                                    newDT.Rows[k]["SMS"] = "Failure";
                                                }
                                                else
                                                {
                                                    newDT.Rows[k]["SMS"] = "Success";
                                                }

                                                // Start a local transaction.
                                                transaction = connection.BeginTransaction("sendSMS");
                                                command.Connection = connection;
                                                command.Transaction = transaction;
                                                command.CommandText = "update salesrecord set smsStatus=@smsStatus WHERE archiveid=@archiveIDsms and status='DISPATCHED'";
                                                command.Parameters.AddWithValue("@archiveIDsms", archiveid);
                                                command.Parameters.AddWithValue("@smsStatus", smsStats);
                                                command.ExecuteNonQuery();
                                                transaction.Commit();
                                            }
                                            else
                                            {
                                                newDT.Rows[k]["SMS"] = "";
                                            }
                                                                                        
                                            newDT.Rows[k]["Barcode"] = stockup.Rows[0]["BarcodeNo"];
                                            newDT.Rows[k]["RackBarcode"] = stockup.Rows[0]["RackBarcode"];
                                            newDT.Rows[k]["Status"] = "Success";
                                            newDT.Rows[k]["Reason"] = "Dispatched";
                                            command.Parameters.Clear();
                                        }
                                    }
                                }

                                command.Parameters.Clear();
                            }
                            else
                            {
                                transaction.Rollback();
                                error += "SKU not Found-" + sku + Environment.NewLine;
                                newDT.Rows[k]["Barcode"] = "";
                                newDT.Rows[k]["RackBarcode"] = "";
                                newDT.Rows[k]["Status"] = "Failed";
                                newDT.Rows[k]["Reason"] = "SKU not in Stock";
                                newDT.Rows[k]["SMS"] = "";
                            }
                            command.Parameters.Clear();
                        }
                        else
                        {
                            transaction.Rollback();
                            newDT.Rows[k]["Status"] = "Failed";
                            newDT.Rows[k]["Reason"] = "Incorrect SKU Format";
                            newDT.Rows[k]["SMS"] = "";
                        }

                    }
                    else
                    {
                        transaction.Rollback();
                        newDT.Rows[k]["Status"] = "Failed";
                        newDT.Rows[k]["Reason"] = "SKU Empty";
                        newDT.Rows[k]["SMS"] = "";
                    }

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    RecordExceptionCls rex = new RecordExceptionCls();
                    rex.recordException(ex);
                    error += "Transaction Rolled Back";
                    newDT.Rows[k]["Barcode"] = "";
                    newDT.Rows[k]["RackBarcode"] = "";
                    newDT.Rows[k]["Status"] = "Failed";
                    newDT.Rows[k]["Reason"] = "Transaction Rolled Back";
                    newDT.Rows[k]["SMS"] = "";
                }
                k++;
                command.Parameters.Clear();
            }

            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        catch (Exception ex)
        {
            try
            {
                //transaction.Rollback();
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex);
                error += "Transaction Rolled Back";

            }
            catch (Exception ex2)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex2);
                error += "Some error Occured";

            }
        }


        return newDT;
    }

    public Boolean? checkBarcodeLocation(string barcode, string physicalLocation)
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
        transaction = connection.BeginTransaction("chckbarcode");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            Boolean? check = false;
            command.CommandText = "select * from StockUpInward where BarcodeNo=@BarcodeNo and physicalID=@physicalID and status!='SOLD'";
            command.Parameters.AddWithValue("@BarcodeNo", barcode);
            command.Parameters.AddWithValue("@physicalID", physicalLocation);
            DataTable dt = new DataTable();
            dt.Load(command.ExecuteReader());
            if(dt.Rows.Count.Equals(1))
            {
                check = true;
            }
            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();
            return check;
        }
        catch (Exception ex)
        {
            Boolean? check1 = null;
            try
            {
                transaction.Rollback();
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex);
                return check1;

            }
            catch (Exception ex2)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex2);
                return check1;

            }
        }
    }


}