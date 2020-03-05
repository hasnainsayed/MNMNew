using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for StockLocationCls
/// </summary>
public class StockLocationCls
{
    public StockLocationCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable getStockLocation()
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
        transaction = connection.BeginTransaction("getStockLocation");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "SELECT concat(i.StyleCode,'-',sz.Size1) AS sku ,s.RackBarcode,s.mrp,COUNT(s.StyleID) cnt FROM StockUpInward s INNER JOIN ItemStyle i ON i.StyleID=s.StyleID INNER JOIN Size sz ON sz.SizeID=s.SizeID WHERE s.RackBarcode!='' GROUP BY s.StyleID,s.SizeID,s.mrp,s.RackBarcode,i.StyleCode,sz.Size1 ORDER BY s.RackBarcode";
            
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