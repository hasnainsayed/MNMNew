using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for VendorsCls
/// </summary>
public class VendorsCls
{
    public VendorsCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int addUpdateVendor(int VendorID, string VendorName, string Contact, string Email, string City, string vAddress, string gstin)
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
        transaction = connection.BeginTransaction("AUVendor");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            int result = 0;
            command.Parameters.AddWithValue("@VendorName", VendorName);
            command.Parameters.AddWithValue("@Contact", Contact);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@City", City);
            command.Parameters.AddWithValue("@vAddress", vAddress);
            command.Parameters.AddWithValue("@gstin", gstin);
           
            if (VendorID.Equals(0))
            {
                command.CommandText = "insert into Vendor (VendorName,Contact,Email,City,vAddress,gstin) values (@VendorName,@Contact,@Email,@City,@vAddress,@gstin)";
            }
            else
            {
                command.CommandText = "update Vendor set VendorName=@VendorName," +
                    "Contact=@Contact,Email=@Email,City=@City,vAddress=@vAddress,gstin=@gstin where VendorID=@VendorID";
                command.Parameters.AddWithValue("@VendorID", VendorID);
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