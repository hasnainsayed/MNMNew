using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for styleCls
/// </summary>
public class accountingCentreCls
{
    string userId = HttpContext.Current.Session["login"].ToString();//id of logged in admin
    string userName = HttpContext.Current.Session["userName"].ToString();
    public accountingCentreCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable getAccoutingCentre()
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
        transaction = connection.BeginTransaction("getAC");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "SELECT a.centreId,a.centreName,c.currencyName FROM accountingCentre a INNER JOIN currencyMaster c ON a.currency=c.currencyId";
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

    public int addUpdateCentre(string currency, string centreName , string centreId,string makerId, string logDet )
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
        transaction = connection.BeginTransaction("addEditCen");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            int result = 0;

            if(centreId.Equals("0"))
            {
                command.CommandText = "insert into accountingCentre (centreName,currency,makerId,logDet) values (@centreName,@currency,@makerId,@logDet)";
                command.Parameters.AddWithValue("@makerId", makerId);
            }
            else
            {
                command.CommandText = "update accountingCentre set centreName=@centreName,currency=@currency,logDet+=@logDet where centreId=@centreId";
                command.Parameters.AddWithValue("@centreId", centreId);
            }
            command.Parameters.AddWithValue("@centreName", centreName);
            command.Parameters.AddWithValue("@currency", currency);
            command.Parameters.AddWithValue("@logDet", ","+logDet+":"+DateTime.Now);

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