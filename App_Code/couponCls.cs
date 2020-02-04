using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


/// <summary>
/// Summary description for couponCls
/// </summary>
public class couponCls
{
    string userId = HttpContext.Current.Session["login"].ToString();//id of logged in admin
    string userName = HttpContext.Current.Session["userName"].ToString();//name of logged in user
    public couponCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable getCouponList()
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
        transaction = connection.BeginTransaction("getCoupon");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "SELECT c.*,i.ItemCategory FROM couponMaster c left JOIN ItemCategory i ON c.couponCategory=i.ItemCategoryID";
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

    public int addEditCoupon(string couponId, string couponName, string couponCategory, string validFrom, string validTo, string couponType,
        string couponDiscount, string discountOn,DataTable dropdown, string applicableOnAmount, string applicableAmount)
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
        transaction = connection.BeginTransaction("addLot");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            string logs = ","+userName + ":" + DateTime.Now;
            int result = 0;
            command.Parameters.AddWithValue("@couponName", couponName);
            command.Parameters.AddWithValue("@couponId", couponId);
            command.Parameters.AddWithValue("@couponCategory", couponCategory);
            command.Parameters.AddWithValue("@validFrom", Convert.ToDateTime(validFrom).ToString("yyyy-MM-dd H:mm:ss"));
            command.Parameters.AddWithValue("@validTo", Convert.ToDateTime(validTo).ToString("yyyy-MM-dd H:mm:ss"));
            command.Parameters.AddWithValue("@couponType", couponType);            
            command.Parameters.AddWithValue("@couponDiscount", Convert.ToDecimal(couponDiscount));
            command.Parameters.AddWithValue("@discountOn", discountOn);
            command.Parameters.AddWithValue("@applicableOnAmount", applicableOnAmount);
            command.Parameters.AddWithValue("@applicableAmount", Convert.ToDecimal(applicableAmount));
            command.Parameters.AddWithValue("@logs", logs);
            
            DataTable checkDuplicate = new DataTable();
            if (couponId.Equals("0"))
            {
                command.CommandText = "select * from couponMaster where couponName=@couponName";                
                checkDuplicate.Load(command.ExecuteReader());
            }
            else
            {
                command.CommandText = "select * from couponMaster where couponName=@couponName and couponId!=@couponId";                
                checkDuplicate.Load(command.ExecuteReader());
            }

            if(!checkDuplicate.Rows.Count.Equals(0))
            {
                result = 1;
            }
            else
            {
                int id = 0;
                if (couponId.Equals("0"))
                {
                    // insert
                    command.CommandText = "insert into couponMaster (couponName,couponCategory,validFrom,validTo,couponType,couponDiscount,discountOn,logs,applicableOnAmount,applicableAmount) values " +
                        "(@couponName,@couponCategory,@validFrom,@validTo,@couponType,@couponDiscount,@discountOn,@logs,@applicableOnAmount,@applicableAmount) SELECT CAST(scope_identity() AS int)";
                    id = (Int32)command.ExecuteScalar();
                }
                else
                {
                    // update
                    command.CommandText = "update couponMaster set couponName=@couponName,couponCategory=@couponCategory,validFrom=@validFrom" +
                        ",validTo=@validTo,couponType=@couponType,couponDiscount=@couponDiscount,discountOn=@discountOn,logs+=@logs,applicableOnAmount=@applicableOnAmount,applicableAmount=@applicableAmount where couponId=@couponId";
                    command.ExecuteNonQuery();
                    id = Convert.ToInt32(couponId);
                }

                //delete from bannerDropdown and re - insert

                command.CommandText = "delete from couponDropdown where bannerId = @id";
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                command.Parameters.Clear();

                foreach (DataRow row in dropdown.Rows)
                {
                    command.CommandText = "INSERT INTO couponDropdown(dropId, subDropId, bannerId) values (@dropId1, @subDropId1, @bannerId1)";
                    command.Parameters.AddWithValue("@dropId1", row["dropId"]);
                    command.Parameters.AddWithValue("@subDropId1", row["subDropId"]);
                    command.Parameters.AddWithValue("@bannerId1", id);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
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