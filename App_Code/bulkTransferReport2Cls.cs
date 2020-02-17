using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for bulkTransferReport2Cls
/// </summary>
public class bulkTransferReport2Cls
{
    public bulkTransferReport2Cls()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable getRecords(string frmDate, string toDate)
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
        transaction = connection.BeginTransaction("getRecords");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "SELECT CONCAT(i.StyleCode,'/',z.Size1) As Barcode,COUNT(i.StyleCode) As Count FROM StockUpInward s INNER JOIN ItemStyle i ON s.StyleID=i.StyleID INNER JOIN size z ON z.SizeID=s.SizeID " +
                        "WHERE s.DateTime BETWEEN @frmDate AND @toDate " +
                        "GROUP BY i.StyleCode,z.Size1 " +
                        "UNION ALL " +
                        "SELECT CONCAT(i.StyleCode, '/', z.Size1) As Barcode,COUNT(i.StyleCode) As Count FROM ArchiveStockUpInward s INNER JOIN ItemStyle i ON s.StyleID = i.StyleID INNER JOIN size z ON z.SizeID = s.SizeID " +
                        "WHERE s.DateTime BETWEEN @frmDate AND @toDate " +
                        "GROUP BY i.StyleCode,z.Size1";
            command.Parameters.AddWithValue("@frmDate", frmDate);
            command.Parameters.AddWithValue("@toDate", toDate);
            catTable.Load(command.ExecuteReader());

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
        return catTable;
    }
}