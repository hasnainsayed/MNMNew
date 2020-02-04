using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for sizeCls
/// </summary>
public class sizeCls
{
    string userId = HttpContext.Current.Session["login"].ToString();//id of logged in admin
    string userName = HttpContext.Current.Session["userName"].ToString();//id of logged in admin
    public sizeCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable getSizeByStyle(string StyleID)
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
        transaction = connection.BeginTransaction("getSizeByS");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select s.SizeID,s.Size1 from size s inner join ItemCategory c on c.ItemCategoryID = s.ItemCategoryID inner join ItemStyle sty on sty.ItemCatID = c.ItemCategoryID where sty.StyleID = @StyleID";
            command.Parameters.AddWithValue("@StyleID", StyleID);
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

    public int addEditSize(string ItemCategoryID, string Size1, string Size2, string Size3, string Size4, string SizeCode, string hdnId, DataTable dt)
    {
        int result = -1;
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("latestStyle");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            int sizeId = 0;
            command.Parameters.AddWithValue("@ItemCategoryID", ItemCategoryID);
            command.Parameters.AddWithValue("@Size1", Size1);
            command.Parameters.AddWithValue("@Size2", Size2);
            command.Parameters.AddWithValue("@Size3", Size3);
            command.Parameters.AddWithValue("@Size4", Size4);
            

            if (hdnId.Equals("0"))
            {
                command.Parameters.AddWithValue("@SizeCode", SizeCode);
                command.CommandText = "insert into Size (ItemCategoryID,Size1,Size2,Size3,Size4,SizeCode)" +
                   "values (@ItemCategoryID,@Size1,@Size2,@Size3,@Size4,@SizeCode)" +
                   "SELECT CAST(scope_identity() AS int)";
                sizeId = (Int32)command.ExecuteScalar();

                // generate hexa value and store
            }
            else{
                command.CommandText = "update Size set ItemCategoryID=@ItemCategoryID,Size1=@Size1,Size2=@Size2," +
                    "Size3=@Size3,Size4=@Size4 where SizeID=@SizeID";
                command.Parameters.AddWithValue("@SizeID", hdnId);
                command.ExecuteNonQuery();
                sizeId = Convert.ToInt32(hdnId);
            }
            foreach(DataRow rows in dt.Rows)
            {
                command.Parameters.AddWithValue("@logs", ","+userName + "" + DateTime.Now);
                command.Parameters.AddWithValue("@brandId", rows["brandId"]);
                command.Parameters.AddWithValue("@sizeId1", sizeId);
                command.Parameters.AddWithValue("@lengths", rows["lengths"]);
                if (rows["pId"].Equals("0"))
                {
                    command.CommandText = "insert into sizeLength (sizeId,brandId,lengths,logs) values (@sizeId1,@brandId,@lengths,@logs)";
                }
                else
                {
                    command.CommandText = "update sizeLength set sizeId=@sizeId1,brandId=@brandId,lengths=@lengths,logs+=@logs where pId=@pId";
                    command.Parameters.AddWithValue("@pId", rows["pId"]);
                }
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }
            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();
            result = 1;
        }
        catch(Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
        return result;
    }
}