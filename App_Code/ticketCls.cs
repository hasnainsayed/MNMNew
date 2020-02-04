using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for ticketCls
/// </summary>
public class ticketCls
{
    string userId = HttpContext.Current.Session["login"].ToString();//id of logged in admin
    public ticketCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable getTickets(string fromDate,string toDate, string vLoc,string salesid,string barcode,string ticket, bool ticketCheck, bool vLocCheck, bool salesCheck, bool barcodeCheck, bool dateRange)
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
        transaction = connection.BeginTransaction("getTickets");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            string dateR = string.Empty;
            string dateRA = string.Empty;
            if (dateRange.Equals(true))
            {
                dateR = " where (issueDate between @fromDate and @toDate)";
                dateRA = " where (issueDate between @fromDate1 and @toDate1)";
                command.Parameters.AddWithValue("@fromDate", fromDate);
                command.Parameters.AddWithValue("@toDate", toDate);
                command.Parameters.AddWithValue("@fromDate1", fromDate);
                command.Parameters.AddWithValue("@toDate1", toDate);
            }
            string query = string.Empty;
            //query += "select * from (select st.BarcodeNo,s.salesidgivenbyvloc,s.itemid,l.Location,t.*,s.saleschannelvlocid from (select * from ticket_master "+ dateR + " ) t inner join salesrecord s on s.sid=t.salesid " +
                //"inner join StockUpInward st on s.itemid != -1 and st.StockupID = s.itemid inner join Location l on l.LocationID = s.saleschannelvlocid " +
                //"union all " +
               // "select st1.BarcodeNo,s1.salesidgivenbyvloc,s1.itemid,l1.Location,t1.*,s1.saleschannelvlocid from (select * from ticket_master  " + dateRA + " ) t1 inner join salesrecord s1 on s1.sid=t1.salesid " +
                //"inner join ArchiveStockUpInward st1 on s1.archiveid != -1 and st1.ArchiveStockupID = s1.archiveid inner join Location l1 on l1.LocationID = s1.saleschannelvlocid ) a";

            query += "select * from (select st.BarcodeNo,s.salesidgivenbyvloc,s.itemid,l.Location,t.*,s.saleschannelvlocid,case when s.rImage1 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',s.rImage1) else '' end as rImage1," +
                "case when s.rImage2 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',s.rImage2) else '' end as rImage2," +
                "case when s.rImage3 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',s.rImage3) else '' end as rImage3," +
                "case when s.rImage4 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',s.rImage4) else '' end as rImage4," +
                "case when s.rImage5 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',s.rImage5) else '' end as rImage5," +
                "case when s.rImage6 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',s.rImage6) else '' end as rImage6," +
                "case when s.rImage7 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',s.rImage7) else '' end as rImage7," +
                "case when s.rImage8 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',s.rImage8) else '' end as rImage8," +
                "case when s.rImage9 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',s.rImage9) else '' end as rImage9," +
                "case when s.rImage10 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',s.rImage10) else '' end as rImage10,rVideo1,rVideo2 from (select * from ticket_master " + dateR + " ) t inner join salesrecord s on s.status=@status and s.sid=t.salesid " +
                "inner join StockUpInward st on st.StockupID = s.itemid inner join Location l on l.LocationID = s.saleschannelvlocid " +
                "union all " +
                "select st1.BarcodeNo,s1.salesidgivenbyvloc,s1.itemid,l1.Location,t1.*,s1.saleschannelvlocid,case when s1.rImage1 != '' then concat('http://sbuyimages1.dzvdesk.com/Uploads/',s1.rImage1) else '' end as rImage1," +
                "case when s1.rImage2 != '' then concat('http://sbuyimages1.dzvdesk.com/Uploads/',s1.rImage2) else '' end as rImage2," +
                "case when s1.rImage3 != '' then concat('http://sbuyimages1.dzvdesk.com/Uploads/',s1.rImage3) else '' end as rImage3," +
                "case when s1.rImage4 != '' then concat('http://sbuyimages1.dzvdesk.com/Uploads/',s1.rImage4) else '' end as rImage4," +
                "case when s1.rImage5 != '' then concat('http://sbuyimages1.dzvdesk.com/Uploads/',s1.rImage5) else '' end as rImage5," +
                "case when s1.rImage6 != '' then concat('http://sbuyimages1.dzvdesk.com/Uploads/',s1.rImage6) else '' end as rImage6," +
                "case when s1.rImage7 != '' then concat('http://sbuyimages1.dzvdesk.com/Uploads/',s1.rImage7) else '' end as rImage7," +
                "case when s1.rImage8 != '' then concat('http://sbuyimages1.dzvdesk.com/Uploads/',s1.rImage8) else '' end as rImage8," +
                "case when s1.rImage9 != '' then concat('http://sbuyimages1.dzvdesk.com/Uploads/',s1.rImage9) else '' end as rImage9," +
                "case when s1.rImage10 != '' then concat('http://sbuyimages1.dzvdesk.com/Uploads/',s1.rImage10) else '' end as rImage10,rVideo1,rVideo2 from (select * from ticket_master  " + dateRA + " ) t1 inner join salesrecord s1 on s1.status=@status1 and  s1.sid=t1.salesid " +
                "inner join ArchiveStockUpInward st1 on st1.StockupID = s1.itemid inner join Location l1 on l1.LocationID = s1.saleschannelvlocid ) a";
            command.Parameters.AddWithValue("@status", "RETURN");
            command.Parameters.AddWithValue("@status1", "RETURN");
            string conV = string.Empty;
            string where = "0";
            if (!vLoc.Equals("-1") && vLocCheck.Equals(true))
            {
                if(where.Equals("0"))
                {
                    conV += " where a.saleschannelvlocid=@vLoc";
                    where = "1";
                }
                else
                {
                    conV += " and a.saleschannelvlocid=@vLoc";
                }
                command.Parameters.AddWithValue("@vLoc", vLoc);
            }

            if (!salesid.Equals("") && salesCheck.Equals(true))
            {
                if (where.Equals("0"))
                {
                    conV += " where a.salesidgivenbyvloc=@salesid";
                    where = "1";
                }
                else
                {
                    conV += " and a.salesidgivenbyvloc=@salesid";
                }
                command.Parameters.AddWithValue("@salesid", salesid);
            }

            if (!barcode.Equals("") && barcodeCheck.Equals(true))
            {
                if (where.Equals("0"))
                {
                    conV += " where a.BarcodeNo=@barcode";
                    where = "1";
                }
                else
                {
                    conV += " and a.BarcodeNo=@barcode";
                }
                command.Parameters.AddWithValue("@barcode", barcode);
            }

            if (!ticket.Equals("") && ticketCheck.Equals(true))
            {
                if (where.Equals("0"))
                {
                    conV += " where a.ticketNo=@ticket";
                    where = "1";
                }
                else
                {
                    conV += " and a.ticketNo=@ticket";
                }
                command.Parameters.AddWithValue("@ticket", ticket);
            }


            command.CommandText = query + conV;
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

    public DataTable returnByBarcodeSales(string searchFields, string column)
    {
        DataTable stock = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("rByBarcode");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            // search stockupinward
            command.CommandText = " select c.ItemCategory,a.StockupID,a.BarcodeNo,s.sid,s.salesidgivenbyvloc,s.salesAbwno,co.courierName,s.salesDateTime,lo.username as soldBy, " +
                "s.dispatchtimestamp,loD.username as dispatchedBy,inv.custname,inv.invid,inv.paymentMode,col.C1Name,loc.Location,si.image1,loR.username as returnedBy,s.returntimestamp,s.remarks,s.returnAbwno,ro.courierName as retCourier " +
                "from StockUpInward a inner join salesrecord s on s.status = @status and " + column + "=@column and s.itemid = a.StockupID " +
                " inner join ItemStyle si on si.StyleID = a.StyleID" +
                " inner join ItemCategory c on c.ItemCategoryID = si.ItemCatID" +
                " inner join courier co on co.courierId = s.salesCourier" +
                " inner join courier ro on ro.courierId = s.returnCourier" +
                " inner join login lo on lo.userid = s.salesUserId" +
                " inner join login loD on loD.userid = s.dispatchuserid" +
                " inner join login loR on loR.userid = s.returnuserid" +
                " inner join invoice inv on inv.invid = s.invoiceid" +
                " inner join Column1 col on col.Col1ID = si.Col1" +
                " inner join Location loc on loc.LocationID = s.saleschannelvlocid";

            /*command.CommandText = "select c.ItemCategory,a.ArchiveStockupID,a.StockupID,a.BarcodeNo,s.sid,s.salesidgivenbyvloc,s.salesAbwno,co.courierName,s.salesDateTime,lo.username as soldBy,s.dispatchtimestamp,loD.username as dispatchedBy," +
                "inv.custname,inv.invid,inv.paymentMode,col.C1Name,loc.Location,si.image1 from ArchiveStockUpInward a inner join salesrecord s on s.archiveid=a.ArchiveStockupID  inner join ItemStyle si on si.StyleID=a.StyleID" +
                "  inner join ItemCategory c on c.ItemCategoryID=si.ItemCatID inner join courier co on co.courierId=s.salesCourier inner join login lo on lo.userid=s.salesUserId inner join login loD on loD.userid=s.dispatchuserid " +
                "inner join invoice inv on inv.invid=s.invoiceid inner join Column1 col on col.Col1ID=si.Col1 inner join Location loc on loc.LocationID=s.saleschannelvlocid where " +
                " s.status=@status  and " +
                column + "=@column";*/
           
            command.Parameters.AddWithValue("@status", "RETURN");
            command.Parameters.AddWithValue("@column", searchFields);
            stock.Load(command.ExecuteReader());

            DataTable aStock = new DataTable();
                command.CommandText = " select c.ItemCategory,a.StockupID,a.BarcodeNo,s.sid,s.salesidgivenbyvloc,s.salesAbwno,co.courierName,s.salesDateTime,lo.username as soldBy, " +
                "s.dispatchtimestamp,loD.username as dispatchedBy,inv.custname,inv.invid,inv.paymentMode,col.C1Name,loc.Location,si.image1,loR.username as returnedBy,s.returntimestamp,s.remarks,s.returnAbwno,ro.courierName as retCourier " +
                "from ArchiveStockUpInward a inner join salesrecord s on s.status = @status1 and " + column + "=@column1 and s.itemid = a.StockupID "+
                " inner join ItemStyle si on si.StyleID = a.StyleID" +
                " inner join ItemCategory c on c.ItemCategoryID = si.ItemCatID" +
                " inner join courier co on co.courierId = s.salesCourier" +
                " inner join courier ro on ro.courierId = s.returnCourier" +
                " inner join login lo on lo.userid = s.salesUserId" +
                " inner join login loD on loD.userid = s.dispatchuserid" +
                " inner join login loR on loR.userid = s.returnuserid" +
                " inner join invoice inv on inv.invid = s.invoiceid" +
                " inner join Column1 col on col.Col1ID = si.Col1" +
                " inner join Location loc on loc.LocationID = s.saleschannelvlocid";
                command.Parameters.AddWithValue("@status1", "RETURN");
                command.Parameters.AddWithValue("@column1", searchFields);
            aStock.Load(command.ExecuteReader());

            stock.Merge(aStock);

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        return stock;
    }

    public DataTable getReturnImages(string salesid)
    {
        DataTable stock = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("retImages");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            // search stockupinward
            command.CommandText = "select rImage1,rImage2,rImage3,rImage4,rImage5,rImage6,rImage7,rImage8,rImage9,rImage10,rVideo1,rVideo2 from salesrecord where sid=@salesid";
                   
            command.Parameters.AddWithValue("@salesid", salesid);
          
            stock.Load(command.ExecuteReader());
                        
            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        return stock;
    }

    public int saveTicket(string displaysalesid, string ticketNo, string issueDate, string description)
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
        transaction = connection.BeginTransaction("saveTicket");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            command.CommandText = "INSERT INTO ticket_master (salesid,ticketNo,issueDate,description,uid) values (@salesid,@ticketNo,@issueDate,@description,@uid)";
            command.Parameters.AddWithValue("@salesid", displaysalesid);
            command.Parameters.AddWithValue("@ticketNo", ticketNo);
            command.Parameters.AddWithValue("@issueDate", Convert.ToDateTime(issueDate).ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@description", description);
            command.Parameters.AddWithValue("@uid", userId);

            command.ExecuteNonQuery();
            transaction.Commit();         
            command.Parameters.Clear();

            if (connection.State == ConnectionState.Open)
                connection.Close();
            return 1;
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

    public int saveTicktSuccess(string tid, string replyDate, string remittanceRecDate, string remittance)
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
        transaction = connection.BeginTransaction("saveTicketS");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            command.CommandText = "update ticket_master set replyDate=@replyDate,remittance=@remittance,remittanceRecDate=@remittanceRecDate,replyDateTime=@replyDateTime,replyUid=@replyUid,ticketStatus=@ticketStatus where tid=@tid";
            command.Parameters.AddWithValue("@replyDate", Convert.ToDateTime(replyDate).ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@remittance", Convert.ToDecimal(remittance));
            if(remittanceRecDate.Equals(""))
            {
                command.Parameters.AddWithValue("@remittanceRecDate",DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@remittanceRecDate", Convert.ToDateTime(remittanceRecDate).ToString("yyyy-MM-dd"));
            }
            
            command.Parameters.AddWithValue("@replyDateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ff"));
            command.Parameters.AddWithValue("@replyUid", userId);
            command.Parameters.AddWithValue("@ticketStatus", "Success");
            command.Parameters.AddWithValue("@tid", tid);
            command.ExecuteNonQuery();
            transaction.Commit();
            command.Parameters.Clear();

            if (connection.State == ConnectionState.Open)
                connection.Close();
            return 1;
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

    public int saveTicktReject(string tid, string replyDate, string reasons)
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
        transaction = connection.BeginTransaction("saveTicketR");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            command.CommandText = "update ticket_master set replyDate=@replyDate,reasons=@reasons,replyDateTime=@replyDateTime,replyUid=@replyUid,ticketStatus=@ticketStatus where tid=@tid";
            command.Parameters.AddWithValue("@replyDate", Convert.ToDateTime(replyDate).ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@reasons", reasons);        

            command.Parameters.AddWithValue("@replyDateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ff"));
            command.Parameters.AddWithValue("@replyUid", userId);
            command.Parameters.AddWithValue("@ticketStatus", "Rejected");
            command.Parameters.AddWithValue("@tid", tid);
            command.ExecuteNonQuery();
            transaction.Commit();
            command.Parameters.Clear();

            if (connection.State == ConnectionState.Open)
                connection.Close();
            return 1;
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

    public int saveVideo(string tid, string rVideo1, string rVideo2)
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
        transaction = connection.BeginTransaction("saveVideo");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            command.CommandText = "select * from ticket_master where tid=@tid";
            command.Parameters.AddWithValue("@tid", tid);
            DataTable ticket = new DataTable();
            ticket.Load(command.ExecuteReader());

            command.CommandText = "update salesrecord set rVideo1=@rVideo1,rVideo2=@rVideo2 where sid=@sid";           
            command.Parameters.AddWithValue("@rVideo1", rVideo1);           
            command.Parameters.AddWithValue("@rVideo2", rVideo2);           
            command.Parameters.AddWithValue("@sid", Convert.ToInt32(ticket.Rows[0]["salesid"].ToString()));
            command.ExecuteNonQuery();
            transaction.Commit();
            command.Parameters.Clear();

            if (connection.State == ConnectionState.Open)
                connection.Close();
            return 1;
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

    public int updateReturnImages(string tid, string image1, string image2, string image3, string image4,
        string image5, string image6, string image7, string image8, string image9, string image10)
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
        transaction = connection.BeginTransaction("updateImg");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            command.CommandText = "select * from ticket_master where tid=@tid";
            command.Parameters.AddWithValue("@tid", tid);
            DataTable ticket = new DataTable();
            ticket.Load(command.ExecuteReader());

            string query = string.Empty;
            if(!image1.Equals(""))
            {
                query += " rImage1=@rImage1";
                command.Parameters.AddWithValue("@rImage1", image1);
            }
            if (!image2.Equals(""))
            {
                query += " ,rImage2=@rImage2";
                command.Parameters.AddWithValue("@rImage2", image2);
            }
            if (!image3.Equals(""))
            {
                query += " ,rImage3=@rImage3";
                command.Parameters.AddWithValue("@rImage3", image3);
            }
            if (!image4.Equals(""))
            {
                query += " ,rImage4=@rImage4";
                command.Parameters.AddWithValue("@rImage4", image4);
            }
            if (!image5.Equals(""))
            {
                query += " ,rImage5=@rImage5";
                command.Parameters.AddWithValue("@rImage5", image5);
            }
            if (!image6.Equals(""))
            {
                query += " ,rImage6=@rImage6";
                command.Parameters.AddWithValue("@rImage6", image6);
            }
            if (!image7.Equals(""))
            {
                query += " ,rImage7=@rImage7";
                command.Parameters.AddWithValue("@rImage7", image7);
            }
            if (!image8.Equals(""))
            {
                query += " ,rImage8=@rImage8";
                command.Parameters.AddWithValue("@rImage8", image8);
            }
            if (!image9.Equals(""))
            {
                query += " ,rImage9=@rImage9";
                command.Parameters.AddWithValue("@rImage9", image9);
            }
            if (!image10.Equals(""))
            {
                query += " ,rImage10=@rImage10";
                command.Parameters.AddWithValue("@rImage10", image10);
            }
            
            if (!query.Equals(""))
            {
                query.Substring(1);
                command.CommandText = "update salesrecord set "+ query + " where sid=@sid";
                command.Parameters.AddWithValue("@sid", Convert.ToInt32(ticket.Rows[0]["salesid"].ToString()));
                command.ExecuteNonQuery();
            }
           
            
           
            transaction.Commit();
            command.Parameters.Clear();

            if (connection.State == ConnectionState.Open)
                connection.Close();
            return 1;
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