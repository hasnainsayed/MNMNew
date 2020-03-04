using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for LRReports
/// </summary>
public class LRReportsCls
{
    public LRReportsCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable getLRReport(string id)
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
        transaction = connection.BeginTransaction("getLRReport");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "SELECT inv.invType,lo.BagDescription FROM lrListing l INNER JOIN Lot lo ON lo.lrno=l.id AND l.id=@id INNER JOIN ArchiveStockUpInward a ON a.BagID=lo.BagId INNER JOIN salesrecord s ON s.archiveid=a.ArchiveStockupID INNER JOIN invoice inv ON inv.invid=s.invoiceid GROUP BY inv.invType,lo.BagDescription ORDER BY lo.BagDescription";
            command.Parameters.AddWithValue("@id", id);
            catTable.Load(command.ExecuteReader());

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        return catTable;
    }
}