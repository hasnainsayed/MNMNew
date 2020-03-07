using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for LrListingCls
/// </summary>
public class LrListingCls
{
    public LrListingCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable getLr(string commingfrom)
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
        transaction = connection.BeginTransaction("getLr");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            string where = "";
            if (commingfrom.Equals("Active"))
            {
                where = "where status=0";
            }
            else if (commingfrom.Equals("InActive"))
            {
                where = "where status=1";
            }
            command.CommandText = "select * from lrListing " + where + " ";
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

    public DataTable getLRById(string id)
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
        transaction = connection.BeginTransaction("getLRById");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select * from lrListing where id = @id";
            command.Parameters.AddWithValue("@id", id);
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

    public int addLR(string lrno, string description)
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
        transaction = connection.BeginTransaction("addLR");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            int result = 0;


            command.CommandText = "INSERT INTO lrListing (lrno,description) " + " Values (@lrno,@description)";
            command.Parameters.AddWithValue("@lrno", lrno);
            command.Parameters.AddWithValue("@description", description);
            
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

    public int updateLR(int id, string lrno, string description)
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
        transaction = connection.BeginTransaction("updateLR");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            int result = 0;

            command.CommandText = "update lrListing set lrno=@lrno," + "description=@description" + " where id=@id";

            command.Parameters.AddWithValue("@lrno", lrno);
            command.Parameters.AddWithValue("@description", description);
            command.Parameters.AddWithValue("@id", id);
            
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

    public int changeLRStatus(int id, string status)
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
        transaction = connection.BeginTransaction("chgLotSts");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            int result = 0;

            command.CommandText = "Update lrListing set status=@status where id=@id ";
            command.Parameters.AddWithValue("@status", status);
            command.Parameters.AddWithValue("@id", id);
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

    public int getDuplicate1(string lrno)
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
        transaction = connection.BeginTransaction("getDuplicate");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            int result = 0;

            command.CommandText = "select count(*) from lrListing where lrno = @lrno";
            command.Parameters.AddWithValue("@lrno", lrno);
            //command.Parameters.AddWithValue("@id", id);
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

    public DataTable getDuplicate(string lrno)
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
        transaction = connection.BeginTransaction("getDuplicate");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select lrno from lrListing where lrno = @lrno";
            command.Parameters.AddWithValue("@lrno", lrno);
            catTable.Load(command.ExecuteReader());

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
        return catTable;
    }

    public DataTable getDuplicateById(string lrno, string id)
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
        transaction = connection.BeginTransaction("getDuplicate");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select lrno from lrListing where lrno = @lrno and id!=@id";
            command.Parameters.AddWithValue("@lrno", lrno);
            command.Parameters.AddWithValue("@id", id);
            catTable.Load(command.ExecuteReader());

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
        return catTable;
    }

    public DataTable getRecords(string lrno)
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
        transaction = connection.BeginTransaction("getRecords");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "SELECT IsActive FROM Lot where lrno=@lrno";
            command.Parameters.AddWithValue("@lrno", lrno);
            dt.Load(command.ExecuteReader());

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
        return dt;
    }

}