using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// Summary description for currencyCls
/// </summary>
public class currencyCls
{
    public currencyCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int addUpdateCurrency(string currencyName, string currencyCode, string currencyId, string makerId, string logDet)
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
        transaction = connection.BeginTransaction("addEditCur");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            int result = 0;

            if (currencyId.Equals("0"))
            {
                command.CommandText = "insert into currencyMaster (currencyName,currencyCode,makerId,logDets) values (@currencyName,@currencyCode,@makerId,@logDet)";
                command.Parameters.AddWithValue("@makerId", makerId);
            }
            else
            {
                command.CommandText = "update currencyMaster set currencyName=@currencyName,currencyCode=@currencyCode,logDets+=@logDet where currencyId=@currencyId";
                command.Parameters.AddWithValue("@currencyId", currencyId);
            }
            command.Parameters.AddWithValue("@currencyName", currencyName);
            command.Parameters.AddWithValue("@currencyCode", currencyCode);
            command.Parameters.AddWithValue("@logDet", "," + logDet + ":" + DateTime.Now);

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