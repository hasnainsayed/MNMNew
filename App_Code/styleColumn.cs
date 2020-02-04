using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for stylColumn
/// </summary>
public class styleColumn
{
    public styleColumn()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable getTableColwithID(string table, string column, string val, string getColumns)
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
        transaction = connection.BeginTransaction("getTable");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select " + getColumns + " from " + table + " where " + column + "=@column";
            command.Parameters.AddWithValue("column", val);
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

    public int addEditShip(string shipAmountApp, string shipAmountAppTwo, string shipCharge, string shipId)
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
        transaction = connection.BeginTransaction("addEditShip");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            int result = 0;
            command.Parameters.AddWithValue("@shipAmountApp", shipAmountApp);
            command.Parameters.AddWithValue("@shipCharge", shipCharge);
            command.Parameters.AddWithValue("@shipAmountAppTwo", shipAmountAppTwo);
            if (shipId.Equals("0"))
            {
                command.CommandText = "insert into shipping (shipAmountApp,shipCharge,shipAmountAppTwo) values (@shipAmountApp,@shipCharge,@shipAmountAppTwo)";
            }
            else
            {
                command.CommandText = "update shipping set shipAmountApp=@shipAmountApp,shipCharge=@shipCharge,shipAmountAppTwo=@shipAmountAppTwo where shipId=@shipId";
                command.Parameters.AddWithValue("@shipId", shipId);
            }
            
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

            return result;
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

    public int addEditCol(string table, string idCol , string nameCol , string name , string id)
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
        transaction = connection.BeginTransaction("addEditCol");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            int result = 0;

            if(id.Equals("0"))
            {
                command.CommandText = "INSERT INTO "+table+" ("+ nameCol + ") " +
                              " Values (@nameCol)";
            }
            else
            {
                command.CommandText = "update " + table + " set " + nameCol + "=@nameCol where " + idCol + "=@idCol";
                command.Parameters.AddWithValue("@idCol", id);
            }
            command.Parameters.AddWithValue("@nameCol", name);
            

            command.ExecuteNonQuery();
            command.Parameters.Clear();
            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

            return result;
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