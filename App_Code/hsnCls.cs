using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for hsnCls
/// </summary>
public class hsnCls
{
    public hsnCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable getHsn()
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
        transaction = connection.BeginTransaction("getHsn");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select * from hsnmaster";
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

    public DataTable getHSNById(string hsnid)
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
        transaction = connection.BeginTransaction("getHSNById");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select * from hsnmaster where hsnid = @hsnid";
            command.Parameters.AddWithValue("@hsnid",hsnid);
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

    public int addHSN(string hsncode, string lowhighpt, string higst,
                    string hcgst, string hsgst, string ligst, string lcgst, string lsgst)
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
        transaction = connection.BeginTransaction("addHSN");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            int result = 0;


            command.CommandText = "INSERT INTO hsnmaster (hsncode,lowhighpt," +
                "higst,hcgst,hsgst,ligst,lcgst,lsgst) " +
                " Values (@hsncode,@lowhighpt,@higst,@hcgst,@hsgst,@ligst,@lcgst,@lsgst)";
            command.Parameters.AddWithValue("@hsncode", hsncode);
            command.Parameters.AddWithValue("@lowhighpt", lowhighpt);
            command.Parameters.AddWithValue("@higst", higst);
            command.Parameters.AddWithValue("@hcgst", 0);
            command.Parameters.AddWithValue("@hsgst", 0);
            command.Parameters.AddWithValue("@ligst", ligst);
            command.Parameters.AddWithValue("@lcgst", 0);
            command.Parameters.AddWithValue("@lsgst", 0);
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

    public int updateHSN(int hsnid, string hsncode, string lowhighpt, string higst,
                    string hcgst, string hsgst, string ligst, string lcgst, string lsgst)
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
        transaction = connection.BeginTransaction("updateLot");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            int result = 0;

            command.CommandText = "update hsnmaster set hsncode=@hsncode," +
                "lowhighpt=@lowhighpt," +
                "higst=@higst,hcgst=@hcgst,hsgst=@hsgst,ligst=@ligst,lcgst=@lcgst,lsgst=@lsgst where hsnid=@hsnid";
             
            command.Parameters.AddWithValue("@hsncode", hsncode);
            command.Parameters.AddWithValue("@lowhighpt", lowhighpt);
            command.Parameters.AddWithValue("@higst", higst);
            command.Parameters.AddWithValue("@hcgst", 0);
            command.Parameters.AddWithValue("@hsgst", 0);
            command.Parameters.AddWithValue("@ligst", ligst);
            command.Parameters.AddWithValue("@lcgst", 0);
            command.Parameters.AddWithValue("@lsgst", 0);
            command.Parameters.AddWithValue("@hsnid", hsnid);
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