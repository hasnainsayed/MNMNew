using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for invoiceCls
/// </summary>
public class invoiceCls
{
    string userId = HttpContext.Current.Session["login"].ToString();//id of logged in admin
    public invoiceCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable getInvoiceList()
    {
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("invList");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            /*command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "spTest";
            invTable.Load(command.ExecuteReader());*/

            command.CommandText = "select * from invoice i inner join stateCode s on s.stateid=i.state";
            invTable.Load(command.ExecuteReader());

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        return invTable;
    }

    public DataTable getInvoiceListSearch(DataTable search)
    {
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("invListS");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            if (search.Rows[0]["SearchBy"].ToString().Equals("All"))
            {
                /*command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spTest";
                invTable.Load(command.ExecuteReader());*/
                command.CommandText = "select * from invoice i inner join stateCode s on s.stateid=i.state order by invid desc";
                invTable.Load(command.ExecuteReader());
            }
            else if (search.Rows[0]["SearchBy"].ToString().Equals("1")) // by barcode
            {
                invTable = invoiceSearchByBarcode(search.Rows[0]["searchField"].ToString());
            }
            else if (search.Rows[0]["SearchBy"].ToString().Equals("2")) // by salesid
            {
                invTable = invoiceSearchBySalesID(search.Rows[0]["searchField"].ToString());
            }
            else if (search.Rows[0]["SearchBy"].ToString().Equals("3")) // by virtual location
            {
                invTable = invoiceSearchByVirtualLoc(search.Rows[0]["Vloc"].ToString());
            }
            else if (search.Rows[0]["SearchBy"].ToString().Equals("4")) // by Date
            {
                invTable = invoiceSearchByDate(search.Rows[0]["fromDate"].ToString(), search.Rows[0]["toDate"].ToString());
            }
            else if (search.Rows[0]["SearchBy"].ToString().Equals("5")) // by Customer
            {
                invTable = invoiceSearchByCustomer(search.Rows[0]["searchField"].ToString());
            }
            else if (search.Rows[0]["SearchBy"].ToString().Equals("6")) // by Status
            {
                invTable = invoiceSearchByStatus(search.Rows[0]["searchField"].ToString());
            }



            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        return invTable;
    }

    public DataTable invoiceSearchByStatus(string statuss)
    {
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("invbarc");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select i.invid, i.custname, i.total, s.statename,phoneNo,i.invoiceStatus,paymentStatus from invoice i inner " +
                "join stateCode s on s.stateid = i.state " +
                " where i.invoiceStatus LIKE @BarcodeNo1 group by i.invid, i.custname, i.total, s.statename,phoneNo,i.invoiceStatus,paymentStatus";
            command.Parameters.AddWithValue("@BarcodeNo1", "%"+statuss+"%");

            invTable.Load(command.ExecuteReader());

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        return invTable;
    }

    public DataTable getInvoiceDetsByStockupID(string StockUpId)
    {
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("invById");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select * from invoice i inner join salesrecord s on s.invoiceid=i.invid inner join Location l on l.LocationID=s.saleschannelvlocid where s.itemid = @StockUpId and s.status = @status";
            command.Parameters.AddWithValue("@StockUpId", StockUpId);
            command.Parameters.AddWithValue("@status", "ND");
            invTable.Load(command.ExecuteReader());

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        return invTable;
    }

    public DataTable getInvoicebyID(string invoice)
    {
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("invById");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select * from invoice i inner join stateCode s on s.stateid=i.state where invid = @invid";
            command.Parameters.AddWithValue("@invid", invoice);

            invTable.Load(command.ExecuteReader());

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        return invTable;
    }

    public DataTable getSalesbyInvID(string invoice)
    {
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("saleByInv");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select ROW_NUMBER() OVER(ORDER BY sid ASC) AS srNo,s.*,c.ItemCategory,c.HSNCode,i.Control3,si.BarcodeNo,co.courierName,l.Location,i.Title,si.checkNo,si.piecePerPacket from salesrecord s inner join StockUpInward si on si.StockupID=s.itemid inner join ItemStyle i on i.StyleID=si.StyleID inner join ItemCategory c on c.ItemCategoryID=i.ItemCatID inner join Location l on l.LocationID=s.saleschannelvlocid and l.LocationTypeID=2 left join courier co on co.courierId=s.salesCourier where invoiceid = @invid";
            command.Parameters.AddWithValue("@invid", invoice);

            invTable.Load(command.ExecuteReader());

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        return invTable;
    }

    public DataTable getSalesbyArchiveInvID(string invoice)
    {
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("saleArcByInv");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select ROW_NUMBER() OVER(ORDER BY sid ASC) AS srNo,s.*,c.ItemCategory,c.HSNCode,i.Control3,si.BarcodeNo,co.courierName,l.Location,i.Title,si.checkNo,si.piecePerPacket from salesrecord s inner join ArchiveStockUpInward si on si.ArchiveStockupID=s.archiveid inner join ItemStyle i on i.StyleID=si.StyleID inner join ItemCategory c on c.ItemCategoryID=i.ItemCatID inner join Location l on l.LocationID=s.saleschannelvlocid and l.LocationTypeID=2 left join courier co on co.courierId=s.salesCourier where invoiceid = @invid " +
                " union all " +
                "select ROW_NUMBER() OVER(ORDER BY sid ASC) AS srNo,s.*,c.ItemCategory,c.HSNCode,i.Control3,si.BarcodeNo,co.courierName,l.Location,i.Title,si.checkNo,si.piecePerPacket from salesrecord s inner join ArchiveStockUpInward si on si.StockupID=s.itemid inner join ItemStyle i on i.StyleID=si.StyleID inner join ItemCategory c on c.ItemCategoryID=i.ItemCatID inner join Location l on l.LocationID=s.saleschannelvlocid and l.LocationTypeID=2 left join courier co on co.courierId=s.salesCourier where invoiceid = @invid1";
            command.Parameters.AddWithValue("@invid", invoice);
            command.Parameters.AddWithValue("@invid1", invoice);

            invTable.Load(command.ExecuteReader());

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        return invTable;
    }

    public DataTable invoiceSearchByBarcode(string Barcode)
    {
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("invbarc");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select * from " +
                "(select i.invid, i.custname, i.total, s.statename,phoneNo,i.invoiceStatus from invoice i inner join stateCode s on s.stateid = i.state inner join salesrecord sl on sl.invoiceid = i.invid inner join StockUpInward st on st.StockupID = sl.itemid where st.BarcodeNo = @BarcodeNo1 group by i.invid, i.custname, i.total, s.statename,phoneNo,invoiceStatus " +
                "union all " +
                "select i1.invid, i1.custname, i1.total, s1.statename,phoneNo,i.invoiceStatus from invoice i1 inner join stateCode s1 on s1.stateid = i1.state inner join salesrecord sl1 on sl1.invoiceid = i1.invid inner join ArchiveStockUpInward st1 on st1.StockupID = sl1.itemid where st1.BarcodeNo = @BarcodeNo2 group by i1.invid, i1.custname, i1.total, s1.statename,phoneNo,invoiceStatus) a " +
                "group by a.invid,a.custname,a.total,a.statename,phoneNo,invoiceStatus";
            command.Parameters.AddWithValue("@BarcodeNo1", Barcode);
            command.Parameters.AddWithValue("@BarcodeNo2", Barcode);
            invTable.Load(command.ExecuteReader());

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        return invTable;
    }

    public int cancelInvoice(string invoiceId)
    {
        int result = 1;
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("invByCust");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            // change invoice status
            command.CommandText = "update invoice set invoiceStatus=@invoiceStatus where invid = @invid";
            command.Parameters.AddWithValue("@invoiceStatus", "Cancelled");
            command.Parameters.AddWithValue("@invid", invoiceId);
            command.ExecuteNonQuery();
            command.Parameters.Clear();

            // delete from sales and add into cancel
            DataTable dt = new DataTable();
            command.CommandText = "select * from salesrecord where invoiceid=@invid1";
            command.Parameters.AddWithValue("@invid1", invoiceId);
            dt.Load(command.ExecuteReader());

            if (!dt.Rows.Count.Equals(0))
            {
                foreach (DataRow dRow in dt.Rows)
                {
                    //update stockupinward status
                    command.CommandText = "update StockUpInward set status=@status where StockupID=@StockupID";
                    command.Parameters.AddWithValue("@StockupID", dRow["itemid"]);
                    command.Parameters.AddWithValue("@status", "RFL");
                    command.ExecuteNonQuery();

                    //insert into cancelTrans
                    command.CommandText = "insert into cancelTrans (sid,invoiceid,saleschannelvlocid,salesidgivenbyvloc,sellingprice,status,itemid,archiveid,recordtimestamp,dispatchtimestamp,dispatchuserid,gstpercent," +
                "taxableamount,cgstamnt,sgstamnt,igstamnt,salesCourier,salesAbwno,returntimestamp,returnuserid,returnCourier,returnAbwno,reasons,remarks,salesDateTime,salesUserId,canReason,cancelId," +
                "cancleReason,changeStatus,rImage1,rImage2,rImage3,rImage4,rImage5,rImage6,rImage7,rImage8,rImage9,rImage10,rVideo1,rVideo2,discountPer)" +
                    " values " +
                    "(@sid1,@invoiceid1,@saleschannelvlocid,@salesidgivenbyvloc,@sellingprice,@status2,@itemid,@archiveid," +
                    "@recordtimestamp,@dispatchtimestamp,@dispatchuserid,@gstpercent," +
                    "@taxableamount,@cgstamnt,@sgstamnt,@igstamnt,@salesCourier,@salesAbwno,@returntimestamp," +
                    "@returnuserid,@returnCourier,@returnAbwno,@reasons,@remarks,@salesDateTime,@salesUserId," +
                    "@canReason,@cancelId,@cancleReason,@changeStatus,@rImage1,@rImage2,@rImage3,@rImage4,@rImage5,@rImage6,@rImage7,@rImage8,@rImage9,@rImage10,@rVideo1,@rVideo2,@discountPer)";
                    command.Parameters.AddWithValue("@sid1", dRow["sid"]);
                    command.Parameters.AddWithValue("@saleschannelvlocid", dRow["saleschannelvlocid"].ToString());
                    command.Parameters.AddWithValue("@invoiceid1", dRow["invoiceid"].ToString());
                    command.Parameters.AddWithValue("@salesidgivenbyvloc", dRow["salesidgivenbyvloc"].ToString());
                    command.Parameters.AddWithValue("@sellingprice", Convert.ToDecimal(dRow["sellingprice"].ToString()));
                    command.Parameters.AddWithValue("@status2", dRow["status"].ToString());
                    command.Parameters.AddWithValue("@itemid", dRow["itemid"].ToString());
                    command.Parameters.AddWithValue("@archiveid", dRow["archiveid"].ToString());
                    command.Parameters.AddWithValue("@recordtimestamp", Convert.ToDateTime(dRow["recordtimestamp"]).ToString("yyyy-MM-dd HH:mm:ss.mmm"));
                    command.Parameters.AddWithValue("@dispatchtimestamp", dRow["dispatchtimestamp"]);
                    command.Parameters.AddWithValue("@dispatchuserid", dRow["dispatchuserid"].ToString());
                    command.Parameters.AddWithValue("@gstpercent", dRow["gstpercent"].ToString());
                    command.Parameters.AddWithValue("@taxableamount", dRow["taxableamount"].ToString());
                    command.Parameters.AddWithValue("@cgstamnt", dRow["cgstamnt"].ToString());
                    command.Parameters.AddWithValue("@sgstamnt", dRow["sgstamnt"].ToString());
                    command.Parameters.AddWithValue("@igstamnt", dRow["igstamnt"].ToString());
                    command.Parameters.AddWithValue("@salesCourier", dRow["salesCourier"].ToString());
                    command.Parameters.AddWithValue("@salesAbwno", dRow["salesAbwno"].ToString());
                    command.Parameters.AddWithValue("@returntimestamp", dRow["returntimestamp"]);
                    command.Parameters.AddWithValue("@returnuserid", dRow["returnuserid"].ToString());
                    command.Parameters.AddWithValue("@returnCourier", dRow["returnCourier"].ToString());
                    command.Parameters.AddWithValue("@returnAbwno", dRow["returnAbwno"].ToString());
                    command.Parameters.AddWithValue("@reasons", dRow["reasons"].ToString());
                    command.Parameters.AddWithValue("@remarks", dRow["remarks"].ToString());
                    command.Parameters.AddWithValue("@salesDateTime", Convert.ToDateTime(dRow["salesDateTime"]).ToString("yyyy-MM-dd HH:mm:ss.mmm"));
                    command.Parameters.AddWithValue("@salesUserId", dt.Rows[0]["salesUserId"].ToString());
                    command.Parameters.AddWithValue("@canReason", "Merchant Cancelled");
                    command.Parameters.AddWithValue("@cancelId", userId);
                    command.Parameters.AddWithValue("@cancleReason", "Self Cancellation");
                    command.Parameters.AddWithValue("@changeStatus", "RFL");
                    command.Parameters.AddWithValue("@rImage1", dRow["rImage1"].ToString());
                    command.Parameters.AddWithValue("@rImage2", dRow["rImage2"].ToString());
                    command.Parameters.AddWithValue("@rImage3", dRow["rImage3"].ToString());
                    command.Parameters.AddWithValue("@rImage4", dRow["rImage4"].ToString());
                    command.Parameters.AddWithValue("@rImage5", dRow["rImage5"].ToString());
                    command.Parameters.AddWithValue("@rImage6", dRow["rImage6"].ToString());
                    command.Parameters.AddWithValue("@rImage7", dRow["rImage7"].ToString());
                    command.Parameters.AddWithValue("@rImage8", dRow["rImage8"].ToString());
                    command.Parameters.AddWithValue("@rImage9", dRow["rImage9"].ToString());
                    command.Parameters.AddWithValue("@rImage10", dRow["rImage10"].ToString());
                    command.Parameters.AddWithValue("@rVideo1", dRow["rVideo1"].ToString());
                    command.Parameters.AddWithValue("@rVideo2", dRow["rVideo2"].ToString());
                    command.Parameters.AddWithValue("@discountPer", dRow["discountPer"].ToString());
                    command.ExecuteNonQuery();

                    // delete from salesrecord
                    command.CommandText = "DELETE FROM salesrecord WHERE sid=@sid";
                    command.Parameters.AddWithValue("@sid", dRow["sid"]);
                    command.ExecuteNonQuery();

                    command.Parameters.Clear();
                }
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
                result = -1;
            }
            catch (Exception ex2)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex2);
                result = -1;

            }
        }
        return result;
    }

    public int markInvoicePaid(string invoiceId)
    {
        int result = 1;
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("invByCust");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            // change invoice status
            command.CommandText = "update invoice set paymentStatus=@paymentStatus where invid = @invid";
            command.Parameters.AddWithValue("@paymentStatus", "paid");
            command.Parameters.AddWithValue("@invid", invoiceId);
            command.ExecuteNonQuery();
            command.Parameters.Clear();

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
                result = -1;
            }
            catch (Exception ex2)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex2);
                result = -1;

            }
        }
        return result;
    }

    public DataTable invoiceSearchBySalesID(string salesID)
    {
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("invsales");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select i.invid,i.custname,i.total,s.statename,phoneNo,invoicetStatus from invoice i inner join stateCode s on s.stateid=i.state inner join salesrecord sl on sl.invoiceid=i.invid where sl.salesidgivenbyvloc = @salesID group by i.invid,i.custname,i.total,s.statename,phoneNo";
            command.Parameters.AddWithValue("@salesID", salesID);
            invTable.Load(command.ExecuteReader());

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        return invTable;
    }

    public DataTable getTokenGenList()
    {
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("genToken");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "SELECT i.invid,i.custname,i.phoneNo,(SELECT l.username FROM login l WHERE l.userid=i.tokenGenBy) AS tokenGenByName,i.tokenGenBy FROM invoice i WHERE invoiceStatus in ('tokenGen','TokenMaking') order by invid desc";

            invTable.Load(command.ExecuteReader());

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        return invTable;
    }

    public DataTable getTokenMadeList()
    {
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("TokenMade");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "SELECT i.invid,i.custname,i.phoneNo,(SELECT l.username FROM login l WHERE l.userid=i.tokenMadeBy) AS tokenMadeByName,i.tokenMadeBy FROM invoice i WHERE invoiceStatus in ('TokenMade','Tellying') order by invid desc";
            invTable.Load(command.ExecuteReader());

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        return invTable;
    }


    public DataTable invoiceSearchByVirtualLoc(string saleschannelvlocid)
    {
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("invsales");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select i.invid,i.custname,i.total,s.statename,phoneNo,invoicetStatus from invoice i inner join stateCode s on s.stateid=i.state inner join salesrecord sl on sl.invoiceid=i.invid where sl.saleschannelvlocid = @saleschannelvlocid group by i.invid,i.custname,i.total,s.statename,phoneNo";
            command.Parameters.AddWithValue("@saleschannelvlocid", saleschannelvlocid);
            invTable.Load(command.ExecuteReader());

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        return invTable;
    }

    public string saveToken(string invoiceId, DataTable barcodeDt, string tokenMadeId, string status)
    {
        string result = string.Empty;
        int res = 0;
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("invByCust");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            // remove all token id from stockupinward
            command.CommandText = "update StockUpInward set tokenId=@tokenId where tokenId=@tokenId1";
            command.Parameters.AddWithValue("@tokenId", DBNull.Value);
            command.Parameters.AddWithValue("@tokenId1", invoiceId);
            command.ExecuteNonQuery();

            // change status and add to transaction table
            command.CommandText = "update invoice set invoiceStatus=@invoiceStatus,tokenMadeBy=@makerId where invid = @invid";
            command.Parameters.AddWithValue("@invoiceStatus", status);
            command.Parameters.AddWithValue("@makerId", tokenMadeId);
            command.Parameters.AddWithValue("@datetime", DateTime.Now);
            command.Parameters.AddWithValue("@invid", invoiceId);
            command.ExecuteNonQuery();

            command.CommandText = "insert into invoiceTrans (invoiceStatus,makerId,invoiceId) values (@invoiceStatus,@makerId,@invid)";
            command.ExecuteNonQuery();

            // update token id in stock up 
            foreach (DataRow dRow in barcodeDt.Rows)
            {
                DataTable dt = new DataTable();
                string barcode = dRow["BarcodeNo"].ToString();
                command.CommandText = "select BarcodeNo from StockUpInward where BarcodeNo=@BarcodeNo1";
                command.Parameters.AddWithValue("@BarcodeNo1", barcode);
                dt.Load(command.ExecuteReader());

                if (dt.Rows.Count.Equals(0))
                {
                    // check archive
                    DataTable archive = new DataTable();
                    command.CommandText = "select BarcodeNo from ArchiveStockUpInward where BarcodeNo=@BarcodeNo1";
                    archive.Load(command.ExecuteReader());
                    if (archive.Rows.Count.Equals(0))
                    {
                        result += "Barcode is not Available-"+ barcode+",";
                        res = 1;
                    }
                    else
                    {
                        // bring back to stockup and set the token id
                        command.CommandText = "SET IDENTITY_INSERT StockUpInward ON;" +
                            " INSERT INTO StockUpInward(StyleID, UserID, BagID, SizeID, DateTime, LastBarcode, StyleCode, BarcodeNo, RFLQty, RejectQty, Status, LocationID, ExpiredDate, ListingDate, ModeOfPayment, SoldAmount, DispatchedDate, SalesDate, RackBarcode, RackDate, CancelReason, CancelDate, ListedUserID, Picked, BulkID, BulkDate, MoneyIn, MoneyOut, ItemID, SalesID, RecordNo, CourierTransactionID, mrp, StockupID,SystemDate,printed,oldBarcode,initialStatus,labels,mfgDate,labelUserId,piecePerPacket,isSample) SELECT StyleID, UserID, BagID, SizeID, DateTime, LastBarcode, StyleCode, BarcodeNo, RFLQty, RejectQty, Status, LocationID, ExpiredDate, ListingDate, ModeOfPayment, SoldAmount, DispatchedDate, SalesDate, RackBarcode, RackDate, CancelReason, CancelDate, ListedUserID, Picked, BulkID, BulkDate, MoneyIn, MoneyOut, ItemID, SalesID, RecordNo, CourierTransactionID, mrp, StockupID,SystemDate,printed,oldBarcode,initialStatus,labels,mfgDate,labelUserId,piecePerPacket,isSample FROM ArchiveStockUpInward WHERE BarcodeNo = @BarcodeNo;SET IDENTITY_INSERT StockUpInward OFF; ";
                        //SELECT StyleID, UserID, BagID, SizeID, DateTime, LastBarcode, StyleCode, BarcodeNo, RFLQty, RejectQty, Status, LocationID, ExpiredDate, ListingDate, ModeOfPayment, SoldAmount, DispatchedDate, SalesDate, RackBarcode, RackDate, CancelReason, CancelDate, ListedUserID, Picked, BulkID, BulkDate, MoneyIn, MoneyOut, ItemID, SalesID, RecordNo, CourierTransactionID, mrp, StockupID, SystemDate, printed
                        command.Parameters.AddWithValue("@BarcodeNo", barcode);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (!rowsAffected.Equals(1))
                        {
                            result += "update failed because of trasfer from archive to stock-" + barcode + ",";
                            res = 1;
                        }
                        else
                        {
                            DataTable newDt = new DataTable();
                            command.CommandText = "select * from StockUpInward where BarcodeNo=@BarcodeNo";
                            newDt.Load(command.ExecuteReader());
                            command.CommandText = "update StockUpInward set Status=@Ustatus,RackBarcode=@RackBarcode,printed=@printed123,tokenId=@tokenId2,checkNo=@checkNo2 where BarcodeNo=@BarcodeNo";
                            command.Parameters.AddWithValue("@tokenId2", invoiceId);
                            command.Parameters.AddWithValue("@checkNo2", dRow["checkNo"].ToString());
                            command.Parameters.AddWithValue("@Ustatus", newDt.Rows[0]["initialStatus"].ToString());
                            command.Parameters.AddWithValue("@RackBarcode", "");
                            command.Parameters.AddWithValue("@printed123", "No");
                            command.ExecuteNonQuery();

                            // get the archive id
                            DataTable archiveNew = new DataTable();
                            command.CommandText = "select ArchiveStockupID FROM ArchiveStockUpInward WHERE BarcodeNo=@BarcodeNo ";
                            archiveNew.Load(command.ExecuteReader());

                            //Delete from ArchiveStockUpInward
                            command.CommandText = "DELETE FROM ArchiveStockUpInward WHERE BarcodeNo=@BarcodeNo ";                            
                            int rowsAffectedDel = command.ExecuteNonQuery();
                            if (rowsAffectedDel.Equals(1))
                            {
                                command.CommandText = "update salesrecord set status=@status,itemid=@itemid,archiveid=@archiveid,returntimestamp=@returntimestamp," +
                        "returnuserid=@returnuserid,returnCourier=@returnCourier,reasons=@reasons,remarks=@remarks,changeStatus=@changeStatus" +
                        " WHERE archiveid=@archiveid1 ";

                                command.Parameters.AddWithValue("@status", "RETURN");
                                command.Parameters.AddWithValue("@itemid", newDt.Rows[0]["StockupID"].ToString());
                                command.Parameters.AddWithValue("@archiveid", -1);
                                command.Parameters.AddWithValue("@returntimestamp", DateTime.Now);
                                command.Parameters.AddWithValue("@returnuserid", tokenMadeId);
                                command.Parameters.AddWithValue("@returnCourier", DBNull.Value);                               
                                command.Parameters.AddWithValue("@reasons", "9");
                                command.Parameters.AddWithValue("@remarks", "Return marked by System");
                                command.Parameters.AddWithValue("@archiveid1", archiveNew.Rows[0]["ArchiveStockupID"].ToString());
                                command.Parameters.AddWithValue("@changeStatus", newDt.Rows[0]["initialStatus"].ToString());
                                
                                command.ExecuteNonQuery();
                            }
                            else
                            {
                                result += "delete from archive failed-"+ barcode + ",";
                                res = 1;
                            }
                        }
                            
                    }
                }
                else
                {
                    command.CommandText = "update StockUpInward set tokenId=@tokenId2,checkNo=@checkNo where BarcodeNo=@BarcodeNo1";
                    command.Parameters.AddWithValue("@tokenId2", invoiceId);
                    command.Parameters.AddWithValue("@checkNo", dRow["checkNo"].ToString());
                    command.ExecuteNonQuery();
                }

                command.Parameters.Clear();
            }


            if(res.Equals(0))
            {
                transaction.Commit();
                result = "Succesfully Token Made";
            }
            else
            {
                transaction.Rollback();

            }
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
                result = "RollBack";
            }
            catch (Exception ex2)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex2);
                result = "RollBack";

            }
        }
        return result;
    }

    public DataTable invoiceSearchByDate(string fromDate, string toDate)
    {
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("invsales");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select i.invid,i.custname,i.total,s.statename,phoneNo,invoicetStatus from invoice i inner join stateCode s on s.stateid=i.state where (i.salesDate between @fromdate and @todate) group by i.invid,i.custname,i.total,s.statename,phoneNo";
            command.Parameters.AddWithValue("@fromdate", Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@todate", Convert.ToDateTime(toDate).ToString("yyyy-MM-dd"));
            invTable.Load(command.ExecuteReader());

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        return invTable;
    }

    public DataTable invoiceSearchByCustomer(string customer)
    {
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("invByCust");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select * from invoice i inner join stateCode s on s.stateid=i.state where custname like '%" + customer + "%'";
            //command.Parameters.AddWithValue("@customer", customer);

            invTable.Load(command.ExecuteReader());

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        return invTable;
    }

    public int generateToken(string customerId, string makerId)
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
        transaction = connection.BeginTransaction("genTok");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            DataTable dt = new DataTable();
            command.CommandText = "select * from websiteCustomer where webCustId=@webCustId";
            command.Parameters.AddWithValue("@webCustId", customerId);
            dt.Load(command.ExecuteReader());

            command.CommandText = "insert into invoice (webCustomer,userid,invoiceStatus,custname,address1,city,state,phoneNo,tokenGenBy,salesDate) values " +
                "(@webCustomer,@userid,@invoiceStatus,@custname,@address1,@city,@state,@phoneNo,@tokenGenBy,@salesDate)SELECT CAST(scope_identity() AS int)";
            command.Parameters.AddWithValue("@webCustomer", customerId);
            command.Parameters.AddWithValue("@userid", makerId);
            command.Parameters.AddWithValue("@invoiceStatus", "TokenGen");
            command.Parameters.AddWithValue("@custname", dt.Rows[0]["custFirstName"].ToString() + " " + dt.Rows[0]["custLastName"].ToString());
            command.Parameters.AddWithValue("@address1", dt.Rows[0]["address"].ToString());
            command.Parameters.AddWithValue("@city", dt.Rows[0]["city"].ToString());
            command.Parameters.AddWithValue("@state", dt.Rows[0]["state"].ToString());
            command.Parameters.AddWithValue("@phoneNo", dt.Rows[0]["phoneNo"].ToString());
            command.Parameters.AddWithValue("@tokenGenBy", makerId);
            command.Parameters.AddWithValue("@salesDate", DateTime.Now.ToString("yyyy-MM-dd"));
            int invoiceid = (Int32)command.ExecuteScalar();
            command.Parameters.Clear();

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();
            return invoiceid;

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

    public int changeInvoiceStatus(string invoiceId, string status, string makerId, string makerColumn, string datetimeColumn)
    {
        int result = 1;
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("invStat");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            // change invoice status
            command.CommandText = "update invoice set invoiceStatus=@invoiceStatus," + makerColumn + "=@makerId," + datetimeColumn + "=@datetime where invid = @invid";
            command.Parameters.AddWithValue("@invoiceStatus", status);
            command.Parameters.AddWithValue("@makerId", makerId);
            command.Parameters.AddWithValue("@datetime", DateTime.Now);
            command.Parameters.AddWithValue("@invid", invoiceId);
            command.ExecuteNonQuery();

            command.CommandText = "insert into invoiceTrans (invoiceStatus,makerId,invoiceId) values (@invoiceStatus,@makerId,@invid)";
            command.ExecuteNonQuery();

            command.Parameters.Clear();

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
                result = -1;
            }
            catch (Exception ex2)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex2);
                result = -1;

            }
        }
        return result;
    }

    public string saveInvoice(string invoiceid, DataTable barcodeDt, string tokenMadeId, string remarks)
    {
        string result = string.Empty;
        int res = 0;
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("invByCust");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            // GET CUSTOMER STATES  
            DataTable cusDt = new DataTable();
            command.CommandText = "SELECT c.state,c.webCustId FROM websiteCustomer c INNER JOIN invoice i ON i.webCustomer=c.webCustId AND i.invid=@invid";
            command.Parameters.AddWithValue("@invid", invoiceid);
            cusDt.Load(command.ExecuteReader());

            decimal totSum = Convert.ToDecimal(0.0);

            // in one loop
            foreach (DataRow row in barcodeDt.Rows)
            {
                // FETCH HSN CODE
                DataTable hsnDt = new DataTable();
                command.CommandText = "select h.*,si.StockupID,si.mrp from hsnmaster h inner join ItemCategory c on h.hsnid=c.hsnid inner join ItemStyle s on s.ItemCatID=c.ItemCategoryID inner join StockUpInward si on si.StyleID=s.StyleID where si.BarcodeNo = @BarcodeNo and si.Status!=@checkStatus";
                command.Parameters.AddWithValue("@BarcodeNo", row["BarcodeNo"].ToString());
                command.Parameters.AddWithValue("@checkStatus", "SOLD");
                hsnDt.Load(command.ExecuteReader());

                // total sum
                totSum += Convert.ToDecimal(row["mrp"]);

                //calculate hsn
                decimal lowhighpt = Convert.ToDecimal(hsnDt.Rows[0]["lowhighpt"]);
                decimal ligst = Convert.ToDecimal(hsnDt.Rows[0]["ligst"]);
                decimal higst = Convert.ToDecimal(hsnDt.Rows[0]["higst"]);
                decimal taxableamnt = 0;
                decimal igst = 0;
                decimal cgst = 0;
                decimal sgst = 0;
                decimal gst = 0;
                decimal amnt = ((Convert.ToDecimal(row["mrp"])) - ((Convert.ToDecimal(row["mrp"]) * ligst) / 100));
                if (amnt <= lowhighpt)
                {
                    taxableamnt = amnt;
                    igst = ((Convert.ToDecimal(row["mrp"]) * ligst) / 100);
                    gst = ligst;
                }
                else
                {
                    taxableamnt = ((Convert.ToDecimal(row["mrp"])) - ((Convert.ToDecimal(row["mrp"]) * higst) / 100));
                    igst = ((Convert.ToDecimal(row["mrp"]) * higst) / 100);
                    gst = higst;
                }
                cgst = igst / 2;
                sgst = igst / 2;
                if (cusDt.Rows[0]["state"].ToString().Equals("27"))
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


command.CommandText = "INSERT INTO ArchiveStockUpInward (StyleID, UserID, BagID, SizeID, DateTime, LastBarcode, StyleCode, BarcodeNo, RFLQty, RejectQty, Status, LocationID, ExpiredDate, ListingDate, ModeOfPayment, SoldAmount, DispatchedDate, SalesDate, RackBarcode, RackDate, CancelReason, CancelDate, ListedUserID, Picked, BulkID, BulkDate, MoneyIn, MoneyOut, ItemID, SalesID, RecordNo, CourierTransactionID, mrp, StockupID,SystemDate,printed,oldBarcode,initialStatus,labels,mfgDate,labelUserId,physicalId,subLocId,rackId,stackId,piecePerPacket,isSample,purchaseRate,checkNo,tokenId,travelCost)" +
                                                " SELECT StyleID, UserID, BagID, SizeID, DateTime, LastBarcode, StyleCode, BarcodeNo, RFLQty, RejectQty, Status, LocationID, ExpiredDate, ListingDate, ModeOfPayment, SoldAmount, DispatchedDate, SalesDate, RackBarcode, RackDate, CancelReason, CancelDate, ListedUserID, Picked, BulkID, BulkDate, MoneyIn, MoneyOut, ItemID, SalesID, RecordNo, CourierTransactionID, mrp, StockupID,SystemDate,printed,oldBarcode,initialStatus,labels,mfgDate,labelUserId,physicalId,subLocId,rackId,stackId,piecePerPacket,isSample,purchaseRate,checkNo,tokenId,travelCost FROM StockUpInward WHERE StockupID=@StockupID " +
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
                    "@sellingprice,@status,@itemid,@gstpercent,@taxableamount," +
                    "@cgstamnt,@sgstamnt,@igstamnt,@salesUserId,@dispatchtimestamp,@dispatchuserid,@archiveid" +
                    ")";
                command.Parameters.AddWithValue("@invoiceid", invoiceid);
                command.Parameters.AddWithValue("@saleschannelvlocid", "2");
                command.Parameters.AddWithValue("@salesidgivenbyvloc", "");
                command.Parameters.AddWithValue("@sellingprice", Convert.ToDecimal(row["mrp"]));
                command.Parameters.AddWithValue("@status", "DISPATCHED");
                command.Parameters.AddWithValue("@itemid", "-1");
                command.Parameters.AddWithValue("@archiveid", archiveid);
                command.Parameters.AddWithValue("@gstpercent", gst);
                command.Parameters.AddWithValue("@taxableamount", taxableamnt);
                command.Parameters.AddWithValue("@igstamnt", igst);
                command.Parameters.AddWithValue("@cgstamnt", cgst);
                command.Parameters.AddWithValue("@sgstamnt", sgst);
                command.Parameters.AddWithValue("@salesUserId", tokenMadeId);
                command.Parameters.AddWithValue("@dispatchtimestamp", DateTime.Now);                
                command.Parameters.AddWithValue("@dispatchuserid", tokenMadeId);
                command.ExecuteNonQuery();
                
                command.Parameters.Clear();

            }

            // update invoice with total SUM
            command.CommandText = "update invoice set total=@itotal where invid=@iinvid";
            command.Parameters.AddWithValue("@itotal", totSum);
            command.Parameters.AddWithValue("@iinvid", invoiceid);
            command.ExecuteNonQuery();

             // change invoice status
            command.CommandText = "update invoice set invoiceStatus=@invoiceStatus,telliedById=@makerId,tellyDateTime=@datetime, rmks=@remarks where invid = @invid";
            command.Parameters.AddWithValue("@invoiceStatus", "Invoiced");
            command.Parameters.AddWithValue("@makerId", tokenMadeId);
            command.Parameters.AddWithValue("@datetime", DateTime.Now);
            command.Parameters.AddWithValue("@invid", invoiceid); 
            command.Parameters.AddWithValue("@remarks", remarks); 
            command.ExecuteNonQuery();

            // add in invoice transaction
            command.CommandText = "insert into invoiceTrans (invoiceStatus,makerId,invoiceId) values (@invoiceStatus,@makerId,@invid)";
            command.ExecuteNonQuery();


            //int lot = (Int32)command.ExecuteScalar();

            // check for suspense amount and adjust it in ghost entry
            DataTable suspenseAmount = new DataTable();
            command.CommandText = "SELECT ISNULL(SUM(paymentAmount),0) AS suspenseAmount FROM invoicePayment WHERE customerId=@customerId AND invoiceId IS NULL ";
            command.Parameters.AddWithValue("customerId", cusDt.Rows[0]["webCustId"].ToString());
            suspenseAmount.Load(command.ExecuteReader());

            // get ghost amount 
            DataTable ghostAmount = new DataTable();
            command.CommandText = "SELECT ISNULL(SUM(ghostAmount),0) AS ghostAmount FROM ghostInvoicePayment WHERE invoiceId IN (SELECT invoiceId FROM invoicePayment WHERE customerId=@customerId) ";
            ghostAmount.Load(command.ExecuteReader());

            if (!Convert.ToDecimal(suspenseAmount.Rows[0]["suspenseAmount"]).Equals(0))
            {
                decimal suspense = (Convert.ToDecimal(suspenseAmount.Rows[0]["suspenseAmount"])) - (Convert.ToDecimal(ghostAmount.Rows[0]["ghostAmount"]));

                // is suspense amount is less or equal to lot amount
                if (suspense >= Convert.ToDecimal(totSum))
                {
                    suspense = Convert.ToDecimal(totSum);

                    // mark paidStatus as paid
                    command.CommandText = "update invoice set paymentStatus=@paidStatus where invid=@invid";
                    command.Parameters.AddWithValue("@paidStatus", "paid");
                    command.Parameters.AddWithValue("@invid", invoiceid);
                    command.ExecuteNonQuery();
                }

                // insert in ghost entry
                command.CommandText = "insert into ghostInvoicePayment (ghostAmount,invoiceId,ghostDate,makerId) values (@ghostAmount,@lotId,@ghostDate,@makerId)";
                command.Parameters.AddWithValue("@ghostAmount", suspense);
                command.Parameters.AddWithValue("@lotId", invoiceid);
                command.Parameters.AddWithValue("@ghostDate", DateTime.Now.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@makerId", tokenMadeId);
                command.ExecuteNonQuery();
            }

            transaction.Commit();

            result = "Invoice Saved Successfully";
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
                result = "RollBack";
            }
            catch (Exception ex2)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex2);
                result = "RollBack";

            }
        }
        return result;
    }

}