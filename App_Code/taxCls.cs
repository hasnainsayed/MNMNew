using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for taxCls
/// </summary>
public class taxCls
{
    string userId = HttpContext.Current.Session["login"].ToString();//id of logged in admin
    string username = HttpContext.Current.Session["userName"].ToString();//id of logged in admin
    public taxCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int addedittax(string taxid, string tax)
    {
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);

        connection.Open();

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("addedittax");

        // Must assign both transaction object and connection 
        // to Command object for a pending local transaction
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            string logDets = System.DateTime.Now + ": (" + userId + ") " + username;
            if (taxid.Equals("0"))
            {

                command.CommandText = "Insert into tax (tax,makerid,logdet) values (@tax,@makerid,@logdet)";
            }
            else
            {
                command.CommandText = "update tax set tax=@tax,makerid=@makerid,logdet+=@logdet where taxid=@taxid";
               
            }

            command.Parameters.AddWithValue("@tax", tax);
            command.Parameters.AddWithValue("@taxid", taxid);
            command.Parameters.AddWithValue("@makerid", Convert.ToInt32(userId));
            command.Parameters.AddWithValue("@logdet", logDets);
            command.ExecuteNonQuery();
            // Attempt to commit the transaction.
            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

            return 1;


        }
        catch (Exception ex)
        {

            // Attempt to roll back the transaction. 
            try
            {
                transaction.Rollback();
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                return 2;
            }
            catch (Exception ex2)
            {
                // This catch block will handle any errors that may have occurred 
                // on the server that would cause the rollback to fail, such as 
                // a closed connection.
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                return 3;
            }

        }
    }
}