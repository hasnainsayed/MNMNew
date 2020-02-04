using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for storedProcedureCls
/// </summary>
public class storedProcedureCls
{
    string userId = HttpContext.Current.Session["login"].ToString();//id of logged in admin

    public storedProcedureCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable getTable(string tableName,string columnOrder, string orderBy)
    {
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;       
        command.Connection = connection;
       
        try
        {
            command.CommandText = "getTable";
            command.Parameters.AddWithValue("@tableName", tableName);
            command.Parameters.AddWithValue("@columnOrder", columnOrder);
            command.Parameters.AddWithValue("@orderBy", orderBy);
            invTable.Load(command.ExecuteReader());
            command.Parameters.Clear();
        }
        catch (Exception ex)
        {
           
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        return invTable;
    }

    public DataTable getTableWithCondition(string tableName, string columnName, string columnValue, string columnOrder, string orderBy)
    {
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.Connection = connection;

        try
        {
            command.CommandText = "getTableWithCondition";
            command.Parameters.AddWithValue("@tableName", tableName);
            command.Parameters.AddWithValue("@columnName", columnName);
            command.Parameters.AddWithValue("@columnValue", columnValue);
            command.Parameters.AddWithValue("@columnOrder", columnOrder);
            command.Parameters.AddWithValue("@orderBy", orderBy);
            invTable.Load(command.ExecuteReader());
            command.Parameters.Clear();
        }
        catch (Exception ex)
        {

            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        return invTable;
    }

    public DataTable getWebRefund(string fromDate, string toDate, bool checks, string type)
    {
        DataTable catTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("WebRefund");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            string sql1 = string.Empty; string sql2 = string.Empty; string sql3 = string.Empty; string sql4 = string.Empty; string sql5 = string.Empty;
            if (checks.Equals(true))
            {
                sql1 =" AND (Cast(s.custRetTime AS Date) BETWEEN '"+ fromDate + "' AND  '"+ toDate + "' )";
                sql2 = " AND (Cast(s1.custRetTime AS Date) BETWEEN '" + fromDate + "' AND  '" + toDate + "' )";
                sql3 = " AND (Cast(s2.custRetTime AS Date) BETWEEN '" + fromDate + "' AND  '" + toDate + "' )";
                sql4 = " AND (Cast(s3.custRetTime AS Date) BETWEEN '" + fromDate + "' AND  '" + toDate + "' )";
                sql5 = " AND (Cast(s4.custRetTime AS Date) BETWEEN '" + fromDate + "' AND  '" + toDate + "' )";


            }
            command.CommandText = "SELECT sales.sid,sales.invoiceid,sales.canReason as customerRetReason,sales.custRetTime,'C' as returnFrom,a.BarcodeNo FROM (SELECT * FROM cancelTrans s WHERE s.custReasonStats = 1 AND s.sid NOT IN (SELECT sid FROM webRefund) "+ sql1 + ") sales INNER JOIN ArchiveStockUpInward a ON a.StockupID=sales.itemid " +
                "union ALL " +
                "SELECT sales1.sid,sales1.invoiceid,sales1.canReason as customerRetReason,sales1.custRetTime,'C' as returnFrom,a1.BarcodeNo FROM(SELECT* FROM cancelTrans s1 WHERE s1.custReasonStats = 1 AND s1.sid NOT IN (SELECT sid FROM webRefund) " + sql2 + ") sales1 INNER JOIN StockUpInward a1 ON a1.StockupID = sales1.itemid " +
                "UNION ALL " +
                "SELECT sales2.sid,sales2.invoiceid,sales2.canReason as customerRetReason,sales2.custRetTime,'R' as returnFrom,a2.BarcodeNo FROM(SELECT* FROM cancelTrans s2 WHERE s2.custReasonStats = 1 AND s2.sid NOT IN (SELECT sid FROM webRefund) " + sql3 + ") sales2 INNER JOIN StockUpInward a2 ON a2.StockupID = sales2.itemid " +
                "UNION ALL " +
                "SELECT sales3.sid,sales3.invoiceid,sales3.canReason as customerRetReason,sales3.custRetTime,'R' as returnFrom,a3.BarcodeNo FROM(SELECT* FROM cancelTrans s3 WHERE s3.custReasonStats = 1 AND s3.sid NOT IN (SELECT sid FROM webRefund) " + sql4 + ") sales3 INNER JOIN ArchiveStockUpInward a3 ON a3.StockupID = sales3.itemid " +
                "UNION ALL " +
                "SELECT sales4.sid,sales4.invoiceid,sales4.canReason as customerRetReason,sales4.custRetTime,'R' as returnFrom,a4.BarcodeNo FROM(SELECT* FROM cancelTrans s4 WHERE s4.custReasonStats = 1 AND s4.sid NOT IN (SELECT sid FROM webRefund) " + sql5 + ") sales4 INNER JOIN ArchiveStockUpInward a4 ON a4.ArchiveStockupID = sales4.archiveid";
            catTable.Load(command.ExecuteReader());
            transaction.Commit();            

        }
        catch (Exception ex)
        {
            transaction.Rollback();
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        return catTable;
    }

    public DataTable getWebApproved(string fromDate, string toDate, bool checks, string type)
    {
        DataTable catTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("ltstDealSty");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            string sql1 = string.Empty; string sql2 = string.Empty; string sql3 = string.Empty; string sql4 = string.Empty; string sql5 = string.Empty;
            if (checks.Equals(true))
            {
                sql1 = " AND (Cast(r.entrydatetime AS Date) BETWEEN '" + fromDate + "' AND  '" + toDate + "' )";
                sql2 = " AND (Cast(r1.entrydatetime AS Date) BETWEEN '" + fromDate + "' AND  '" + toDate + "' )";
                sql3 = " AND (Cast(r2.entrydatetime AS Date) BETWEEN '" + fromDate + "' AND  '" + toDate + "' )";
                sql4 = " AND (Cast(r3.entrydatetime AS Date) BETWEEN '" + fromDate + "' AND  '" + toDate + "' )";
                sql5 = " AND (Cast(r4.entrydatetime AS Date) BETWEEN '" + fromDate + "' AND  '" + toDate + "' )";
            }
            command.CommandText = "SELECT wr.transferDets,wr.refundDets,s.sid,s.invoiceid,wr.entrydatetime FROM (SELECT * FROM webRefund r WHERE refundStatus=2 AND returnFrom='C' "+ sql1 + ") wr INNER JOIN cancelTrans s ON s.sid=wr.salesid INNER JOIN ArchiveStockUpInward a ON a.StockupID=s.itemid " +
                "union ALL " +
                "SELECT wr1.transferDets,wr1.refundDets,s1.sid,s1.invoiceid,wr1.entrydatetime FROM(SELECT* FROM webRefund r1 WHERE r1.refundStatus= 2 AND r1.returnFrom= 'C' " + sql2 + ") wr1 INNER JOIN cancelTrans s1 ON s1.sid = wr1.salesid INNER JOIN StockUpInward a1 ON a1.StockupID = s1.itemid " +
                "UNION ALL " +
                "SELECT wr2.transferDets,wr2.refundDets,s2.sid,s2.invoiceid,wr2.entrydatetime FROM(SELECT* FROM webRefund r2 WHERE r2.refundStatus= 2 AND r2.returnFrom= 'R' " + sql3 + ") wr2 INNER JOIN salesrecord s2 ON s2.sid = wr2.salesid INNER JOIN StockUpInward a2 ON a2.StockupID = s2.itemid " +
                "UNION ALL " +
                "SELECT wr3.transferDets,wr3.refundDets,s3.sid,s3.invoiceid,wr3.entrydatetime FROM(SELECT* FROM webRefund r3 WHERE r3.refundStatus= 2 AND r3.returnFrom= 'R' " + sql4 + ") wr3 INNER JOIN salesrecord s3 ON s3.sid = wr3.salesid INNER JOIN ArchiveStockUpInward a3 ON a3.StockupID = s3.itemid " +
                "UNION ALL " +
                "SELECT wr4.transferDets,wr4.refundDets,s4.sid,s4.invoiceid,wr4.entrydatetime FROM(SELECT* FROM webRefund r4 WHERE r4.refundStatus= 2 AND r4.returnFrom= 'R' " + sql5 + ") wr4 INNER JOIN salesrecord s4 ON s4.sid = wr4.salesid INNER JOIN ArchiveStockUpInward a4 ON a4.ArchiveStockupID = s4.archiveid";
            catTable.Load(command.ExecuteReader());
            transaction.Commit();

        }
        catch (Exception ex)
        {
            transaction.Rollback();
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        return catTable;
    }

    public DataTable getWebRejected(string fromDate, string toDate, bool checks, string type)
    {
        DataTable catTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("ltstDealSty");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            string sql1 = string.Empty; string sql2 = string.Empty; string sql3 = string.Empty; string sql4 = string.Empty; string sql5 = string.Empty;
            if (checks.Equals(true))
            {
                sql1 = " AND (Cast(r.entrydatetime AS Date) BETWEEN '" + fromDate + "' AND  '" + toDate + "' )";
                sql2 = " AND (Cast(r1.entrydatetime AS Date) BETWEEN '" + fromDate + "' AND  '" + toDate + "' )";
                sql3 = " AND (Cast(r2.entrydatetime AS Date) BETWEEN '" + fromDate + "' AND  '" + toDate + "' )";
                sql4 = " AND (Cast(r3.entrydatetime AS Date) BETWEEN '" + fromDate + "' AND  '" + toDate + "' )";
                sql5 = " AND (Cast(r4.entrydatetime AS Date) BETWEEN '" + fromDate + "' AND  '" + toDate + "' )";
            }
            command.CommandText = "SELECT wr.transferDets,wr.refundDets,s.sid,s.invoiceid,wr.entrydatetime FROM (SELECT * FROM webRefund r WHERE refundStatus=3 AND returnFrom='C' " + sql1 + ") wr INNER JOIN cancelTrans s ON s.sid=wr.salesid INNER JOIN ArchiveStockUpInward a ON a.StockupID=s.itemid " +
                "union ALL " +
                "SELECT wr1.transferDets,wr1.refundDets,s1.sid,s1.invoiceid,wr1.entrydatetime FROM(SELECT* FROM webRefund r1 WHERE r1.refundStatus= 3 AND r1.returnFrom= 'C' " + sql2 + ") wr1 INNER JOIN cancelTrans s1 ON s1.sid = wr1.salesid INNER JOIN StockUpInward a1 ON a1.StockupID = s1.itemid " +
                "UNION ALL " +
                "SELECT wr2.transferDets,wr2.refundDets,s2.sid,s2.invoiceid,wr2.entrydatetime FROM(SELECT* FROM webRefund r2 WHERE r2.refundStatus= 3 AND r2.returnFrom= 'R' " + sql3 + ") wr2 INNER JOIN salesrecord s2 ON s2.sid = wr2.salesid INNER JOIN StockUpInward a2 ON a2.StockupID = s2.itemid " +
                "UNION ALL " +
                "SELECT wr3.transferDets,wr3.refundDets,s3.sid,s3.invoiceid,wr3.entrydatetime FROM(SELECT* FROM webRefund r3 WHERE r3.refundStatus= 3 AND r3.returnFrom= 'R' " + sql4 + ") wr3 INNER JOIN salesrecord s3 ON s3.sid = wr3.salesid INNER JOIN ArchiveStockUpInward a3 ON a3.StockupID = s3.itemid " +
                "UNION ALL " +
                "SELECT wr4.transferDets,wr4.refundDets,s4.sid,s4.invoiceid,wr4.entrydatetime FROM(SELECT* FROM webRefund r4 WHERE r4.refundStatus= 3 AND r4.returnFrom= 'R' " + sql5 + ") wr4 INNER JOIN salesrecord s4 ON s4.sid = wr4.salesid INNER JOIN ArchiveStockUpInward a4 ON a4.ArchiveStockupID = s4.archiveid";
            catTable.Load(command.ExecuteReader());
            transaction.Commit();

        }
        catch (Exception ex)
        {
            transaction.Rollback();
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        return catTable;
    }

    public DataTable getGenderforWeb(string sellID)
    {
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.Connection = connection;

        try
        {
            command.CommandText = "getGendersforWeb";
            command.Parameters.AddWithValue("@webDisplayId", sellID);
            invTable.Load(command.ExecuteReader());
            command.Parameters.Clear();
        }
        catch (Exception ex)
        {

            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        return invTable;
    }

    public DataTable getVerticalforWeb(string sellID)
    {
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.Connection = connection;

        try
        {
            command.CommandText = "getVerticalforWeb";
            command.Parameters.AddWithValue("@webDisplayId", sellID);
            invTable.Load(command.ExecuteReader());
            command.Parameters.Clear();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        return invTable;
    }
        
    public DataTable getCategoryforWeb(string wvId,string cat1ID)
    {
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.Connection = connection;

        try
        {
            command.CommandText = "getCategoryforWeb";
            command.Parameters.AddWithValue("@webVerticalID", wvId);
            command.Parameters.AddWithValue("@cat1ID", cat1ID);
            invTable.Load(command.ExecuteReader());
            command.Parameters.Clear();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        return invTable;
    }

    public string saveRefundSuccess(string displaySalesId, string refundType, string transferDets, string refundDets,string displayReturnFrom,string type)
    {
        string success = string.Empty;
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.Connection = connection;

        try
        {
            command.CommandText = "webRefunds";
            command.Parameters.AddWithValue("@sid", Convert.ToInt32(displaySalesId));
            command.Parameters.AddWithValue("@refundType", refundType);
            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@transferDets", transferDets);
            command.Parameters.AddWithValue("@refundDets", refundDets);
            command.Parameters.AddWithValue("@userId", userId);
            command.Parameters.AddWithValue("@returnFrom", displayReturnFrom);
            command.Parameters.AddWithValue("@entrydatetime", DateTime.Now);
            command.Parameters.Add("@result", SqlDbType.Char, 100);
            command.Parameters["@result"].Direction = ParameterDirection.Output;
            command.ExecuteNonQuery();
            success = command.Parameters["@result"].Value.ToString();
            command.Parameters.Clear();
        }
        catch (Exception ex)
        {

            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
            
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        return success;
    }

    public DataTable getGenderforBanner(string bannerId)
    {
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.Connection = connection;

        try
        {
            command.CommandText = "getGendersforBanner";
            command.Parameters.AddWithValue("@bannerDisplayId", bannerId);
            invTable.Load(command.ExecuteReader());
            command.Parameters.Clear();
        }
        catch (Exception ex)
        {

            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        return invTable;
    }

    public DataTable getVerticalforBanner(string bannerId)
    {
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.Connection = connection;

        try
        {
            command.CommandText = "getVerticalforBanner";
            command.Parameters.AddWithValue("@bannerDisplayId", bannerId);
            invTable.Load(command.ExecuteReader());
            command.Parameters.Clear();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        return invTable;
    }

    public DataTable getCategoryforBanner(string bvId, string cat1ID)
    {
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.Connection = connection;

        try
        {
            command.CommandText = "getCategoryforBanner";
            command.Parameters.AddWithValue("@bannerVerticalID", bvId);
            command.Parameters.AddWithValue("@cat1ID", cat1ID);
            invTable.Load(command.ExecuteReader());
            command.Parameters.Clear();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        return invTable;
    }

    public DataTable getDropdowns(string bannerId,string type)
    {
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.Connection = connection;

        try
        {
            command.CommandText = "getBannerDropValue";
            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@bannerId", bannerId);
            command.Parameters.AddWithValue("@columns", "");
            command.Parameters.AddWithValue("@table", "");
            invTable.Load(command.ExecuteReader());
            command.Parameters.Clear();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        return invTable;
    }

    public DataTable getSubDropdown(string table,string Num)
    {
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.Connection = connection;

        try
        {
            command.CommandText = "getBannerDropValue";
            command.Parameters.AddWithValue("@type", "fetchSubDropdowns");
            command.Parameters.AddWithValue("@bannerId", "");
            command.Parameters.AddWithValue("@columns", "*");
            command.Parameters.AddWithValue("@table", table);
            invTable.Load(command.ExecuteReader());
            command.Parameters.Clear();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        return invTable;
    }

    public string saveWebDetaiils(string displayTitle, string displayStatus, DataTable gender, DataTable vertical, DataTable category, string sellID,string logs,string menuBannerId,string priorities)
    {
        string success = "0";
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.Connection = connection;

        try
        {
            command.CommandText = "saveWebDisplay";
            command.Parameters.AddWithValue("@displayTitle", displayTitle);
            command.Parameters.AddWithValue("@displayStatus", displayStatus);
            command.Parameters.AddWithValue("@gender", gender);
            command.Parameters.AddWithValue("@vertical", vertical);
            command.Parameters.AddWithValue("@category", category);
            command.Parameters.AddWithValue("@sellId", sellID);
            command.Parameters.AddWithValue("@logs", logs);
            command.Parameters.AddWithValue("@menuBannerId", menuBannerId);
            command.Parameters.AddWithValue("@priorities", priorities);
            command.Parameters.Add("@result", SqlDbType.Char, 50);
            command.Parameters["@result"].Direction = ParameterDirection.Output;
            command.ExecuteNonQuery();
            success = command.Parameters["@result"].Value.ToString();
            command.Parameters.Clear();
            
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);            
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        return success;
    }

    public string saveEmailSettings(string tableName, string sender, string subject, string body, string emailId,string logs)
    {
       
        string success = "0";
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.Connection = connection;

        try
        {
            command.CommandText = "emailSettings";
            command.Parameters.AddWithValue("@tableName", tableName);
            command.Parameters.AddWithValue("@sender", sender);
            command.Parameters.AddWithValue("@subject", subject);
            command.Parameters.AddWithValue("@body", body);
            command.Parameters.AddWithValue("@logs", logs);
            command.Parameters.AddWithValue("@emailid", emailId);            
            command.Parameters.Add("@result", SqlDbType.Char, 100);
            command.Parameters["@result"].Direction = ParameterDirection.Output;
            command.ExecuteNonQuery();
            success = command.Parameters["@result"].Value.ToString();
            command.Parameters.Clear();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        return success;
    }

    public string saveBannerDetaiils(string bannerName, string bannerImageName, DataTable gender, DataTable vertical, DataTable category, DataTable drops, string bannerId, string logs,string BannerType)
    {
        string success = "0";
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.Connection = connection;

        try
        {
            command.CommandText = "saveBannerDisplay";
            command.Parameters.AddWithValue("@bannerName", bannerName);
            command.Parameters.AddWithValue("@bannerImageName", bannerImageName);
            command.Parameters.AddWithValue("@gender", gender);
            command.Parameters.AddWithValue("@vertical", vertical);
            command.Parameters.AddWithValue("@category", category);
            command.Parameters.AddWithValue("@drops", drops);
            command.Parameters.AddWithValue("@bannerId", bannerId);
            command.Parameters.AddWithValue("@logs", logs);
            command.Parameters.AddWithValue("@BannerType", BannerType);            
            command.Parameters.Add("@result", SqlDbType.Char, 50);
            command.Parameters["@result"].Direction = ParameterDirection.Output;
            command.ExecuteNonQuery();
            success = command.Parameters["@result"].Value.ToString();
            command.Parameters.Clear();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        return success;
    }

    public string modifySingleCol(string tableName, string columnName,string columnValue,string whereColumnName,string whereColumnValue, string logs)
    {
        string success = "0";
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.Connection = connection;

        try
        {
            command.CommandText = "modifySingleCol";
            command.Parameters.AddWithValue("@tableName", tableName);
            command.Parameters.AddWithValue("@columnName", columnName);
            command.Parameters.AddWithValue("@columnValue", columnValue);
            command.Parameters.AddWithValue("@whereColumnName", whereColumnName);
            command.Parameters.AddWithValue("@whereColumnValue", whereColumnValue);
            command.Parameters.AddWithValue("@logs", logs);
            command.Parameters.Add("@result", SqlDbType.Char, 100);
            command.Parameters["@result"].Direction = ParameterDirection.Output;
            command.ExecuteNonQuery();
            success = command.Parameters["@result"].Value.ToString();
            command.Parameters.Clear();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        return success;
    }

    public DataTable getMenuBanners(string tableName, string column1, string column1Value, string column2, string column2Value)
    {
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.Connection = connection;

        try
        {
            command.CommandText = "getTableMultipleCon";
            command.Parameters.AddWithValue("@tableName", tableName);
            command.Parameters.AddWithValue("@column1", column1);
            command.Parameters.AddWithValue("@column1Value", column1Value);
            command.Parameters.AddWithValue("@column2", column2);
            command.Parameters.AddWithValue("@column2Value", column2Value);
            invTable.Load(command.ExecuteReader());
            command.Parameters.Clear();
        }
        catch (Exception ex)
        {

            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        return invTable;
    }

    public string addLatestProducts(DataTable dtProgLang,string type)
    {
        string success = "0";
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.Connection = connection;

        try
        {
            command.CommandText = "saveIndexProduct";
            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@dataTable", dtProgLang);           
            command.Parameters.AddWithValue("@userId", userId);
            command.Parameters.Add("@result", SqlDbType.Char, 50);
            command.Parameters["@result"].Direction = ParameterDirection.Output;
            command.ExecuteNonQuery();
            success = command.Parameters["@result"].Value.ToString();
            command.Parameters.Clear();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        return success;
    }

    

}