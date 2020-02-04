using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for communicationCls
/// </summary>
public class communicationCls
{
    string userId = HttpContext.Current.Session["login"].ToString();//id of logged in admin
    string userName = HttpContext.Current.Session["userName"].ToString();//id of logged in admin
    public communicationCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int editSmsService(string smsMessage, string apikey, string smsSender, string logs, string smsid)
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
        transaction = connection.BeginTransaction("editSms");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            string logs1 = userName + "#" + DateTime.Now + ",";
            int result = 0;
              command.CommandText = "update smsService set smsMessage=@smsMessage,apikey=@apikey,smsSender=@smsSender,logs+=@logs where smsid=@smsid";
              command.Parameters.AddWithValue("@smsMessage", smsMessage);   
              command.Parameters.AddWithValue("@apikey", apikey);
            command.Parameters.AddWithValue("@smsSender", smsSender);
            command.Parameters.AddWithValue("@logs",logs1);
            command.Parameters.AddWithValue("@smsid", smsid);


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

    public int editFlashData(string flashText, string flashId)
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
        transaction = connection.BeginTransaction("editFlash");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            string logs1 = userName + "#" + DateTime.Now + ",";
            int result = 0;
            command.CommandText = "update flashTable set flashText=@flashText,logs+=@logs where flashId=@flashId";
            command.Parameters.AddWithValue("@flashText", flashText);            
            command.Parameters.AddWithValue("@logs", logs1);
            command.Parameters.AddWithValue("@flashId", flashId);


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

    public int editHeaderSettings(string deals, string latest, string headerId)
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
        transaction = connection.BeginTransaction("editHeader");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            string logs1 = userName + "#" + DateTime.Now + ",";
            int result = 0;
            command.Parameters.AddWithValue("@deals", deals);
            command.Parameters.AddWithValue("@logs", logs1);
            command.Parameters.AddWithValue("@latest", latest);
            if (headerId.Equals("0"))
            {
                command.CommandText = "insert into headerSettings(deals,latest,logs) values (@deals,@latest,@logs)";
            }
            else
            {
                command.CommandText = "update headerSettings set deals=@deals,latest=@latest,logs+=@logs where headerId=@headerId";
                command.Parameters.AddWithValue("@headerId", headerId);
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

    public int editShippingSettings(string amounts, string shipDefaultId)
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
        transaction = connection.BeginTransaction("editHeader");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            string logs1 = userName + "#" + DateTime.Now + ",";
            int result = 0;
            command.Parameters.AddWithValue("@amounts", amounts);
            command.Parameters.AddWithValue("@logs", logs1);
            if (shipDefaultId.Equals("0"))
            {
                command.CommandText = "insert into shipSetting(amounts,logs) values (@amounts,@logs)";
            }
            else
            {
                command.CommandText = "update shipSetting set amounts=@amounts,logs+=@logs where shipDefaultId=@shipDefaultId";
                command.Parameters.AddWithValue("@shipDefaultId", shipDefaultId);
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
}