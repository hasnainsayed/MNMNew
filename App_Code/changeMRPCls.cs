using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for changeMRPCls
/// </summary>
public class changeMRPCls
{
    public changeMRPCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable getBarcodeDetails(string searchFields)
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
        transaction = connection.BeginTransaction("getBarDet");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select s.*,i.Title,col.C1Name,c.ItemCategory from StockUpInward s inner join ItemStyle i on i.StyleID=s.StyleID inner join Column1 col on col.Col1ID=i.Col1 inner join ItemCategory c on c.ItemCategoryID=i.ItemCatID where BarcodeNo = @BarcodeNo";
            command.Parameters.AddWithValue("@BarcodeNo", searchFields);
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

    public int updateMRP(string StockupID, string MRP)
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
        transaction = connection.BeginTransaction("updateMRP");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {

            command.CommandText = "update StockUpInward set mrp=@mrp where StockupID=@StockupID";
            command.Parameters.AddWithValue("@mrp",Convert.ToDecimal(MRP));
            command.Parameters.AddWithValue("@StockupID", Convert.ToInt32(StockupID));
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
}