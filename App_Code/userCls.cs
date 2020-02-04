using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for userCls
/// </summary>
public class userCls
{
    string userName = HttpContext.Current.Session["userName"].ToString();//id of logged in admin
    public userCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int addEditUsers(string username, string password, string id,string userrole,string uImage,string hasImage,string uType, string physicalLocation)
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
        transaction = connection.BeginTransaction("users");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            int result = 0;
            command.CommandText = "select * from login where password=@password";
            command.Parameters.AddWithValue("@password", password);
            DataTable dup = new DataTable();
            dup.Load(command.ExecuteReader());
            string logs = "," + userName + ":" + DateTime.Now;
            if (id.Equals("0"))
            {                
                if(dup.Rows.Count.Equals(0))
                {
                    command.CommandText = "INSERT INTO login (username,password,userrole,uImage,logs,uType,physicalLocation) values (@iusername,@ipassword,@iuserrole,@iuImage,@logs,@uType,@physicalLocation)";
                    command.Parameters.AddWithValue("@iusername", username);
                    command.Parameters.AddWithValue("@ipassword", password);
                    command.Parameters.AddWithValue("@iuserrole", userrole);
                    command.Parameters.AddWithValue("@iuImage", uImage);
                    command.Parameters.AddWithValue("@logs", logs);
                    command.Parameters.AddWithValue("@uType", uType);
                    command.Parameters.AddWithValue("@physicalLocation", physicalLocation);
                    command.ExecuteNonQuery();
                    
                }
                else
                {
                    result = 1;
                }
            }
            else
            {
                string images = "";
                if(hasImage.Equals("1"))
                {
                    images = "uImage=@uuImage,";
                    command.Parameters.AddWithValue("@uuImage", uImage);
                }
                if (dup.Rows.Count.Equals(0))
                {
                    command.CommandText = "update login set username=@uusername,password=@upassword,userrole=@uuserrole,"+ images + "logs=logs+'" + logs + "',uType=@uuType,physicalLocation=@uphysicalLocation where userid=@userid";
                    command.Parameters.AddWithValue("@uusername", username);
                    command.Parameters.AddWithValue("@upassword", password);
                    command.Parameters.AddWithValue("@uuserrole", userrole);
                    command.Parameters.AddWithValue("@uuType", uType);
                    command.Parameters.AddWithValue("@uphysicalLocation", physicalLocation);

                    command.Parameters.AddWithValue("@userid", id);
                    
                    command.ExecuteNonQuery();

                }else if(dup.Rows.Count.Equals(1))
                {
                    if(dup.Rows[0]["userid"].ToString().Equals(id))
                    {
                        command.CommandText = "update login set username=@uusername,password=@upassword,userrole=@uuserrole," + images + "logs=logs+'" + logs + "',uType=@uuType,physicalLocation=@uphysicalLocation where userid=@userid";
                        command.Parameters.AddWithValue("@uusername", username);
                        command.Parameters.AddWithValue("@upassword", password);
                        command.Parameters.AddWithValue("@uuserrole", userrole);
                        command.Parameters.AddWithValue("@uuType", uType);
                        command.Parameters.AddWithValue("@uphysicalLocation", physicalLocation);
                        command.Parameters.AddWithValue("@userid", id);
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        result = 1;
                    }
                }
                else
                {
                    result = 1;
                }
            }
                        

           
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