using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for pickListCls
/// </summary>
public class pickListCls
{
    string userId = HttpContext.Current.Session["login"].ToString();//id of logged in admin
    public pickListCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable getPickList()
    {
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("genPick");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "SELECT p.*,concat(w.custFirstName,'',w.custLastName) as custName FROM pickList p INNER JOIN websiteCustomer w ON w.webCustId=p.customerId order by pickListId desc";

            invTable.Load(command.ExecuteReader());

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
        return invTable;
    }

    public int generatePickList(string customerId, string makerId)
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
        transaction = connection.BeginTransaction("genPick");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {            
            command.CommandText = "insert into pickList (customerId,makerId) values " +
                "(@customerId,@makerId)SELECT CAST(scope_identity() AS int)";
            command.Parameters.AddWithValue("@customerId", customerId);
            command.Parameters.AddWithValue("@makerId", makerId);            
            int invoiceid = (Int32)command.ExecuteScalar();
            command.Parameters.Clear();

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();
            return invoiceid;

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

    internal DataTable getPickListData(string stylecode, string pcppkt)
    {
        DataTable finalreturn = new DataTable();
        DataTable forprice = new DataTable();
        DataTable forlocation = new DataTable();

        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("genPick");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "SELECT count(si.mrp) as countMrp, MAX(si.mrp) as maxMrp FROM itemstyle ist INNER JOIN StockUpInward si ON si.StyleID = ist.StyleID WHERE ist.stylecode = @stylecode AND si.piecePerPacket = @pcppkt";
            command.Parameters.AddWithValue("@stylecode", stylecode);
            command.Parameters.AddWithValue("@pcppkt", pcppkt);
            forprice.Load(command.ExecuteReader());
            forprice.Columns.Add("racks");

            command.CommandText = "SELECT String_AGG(cnt,CHAR(13)) AS racks FROM (SELECT concat(si.RackBarcode, ': ',COUNT(si.RackBarcode),' pkts, ') AS cnt FROM itemstyle ist " +
                " INNER JOIN StockUpInward si ON si.StyleID = ist.StyleID " +
                " WHERE ist.stylecode = @stylecode AND si.piecePerPacket = @pcppkt group by si.RackBarcode) a";

            forlocation.Load(command.ExecuteReader());
            forprice.Rows[0]["racks"] = forlocation.Rows[0]["racks"].ToString();
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
        return forprice;
    }

    public string savePickList(DataTable barcodeDet, string pickListId, string userName, string userId)
    {
        string res = string.Empty;
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("savePick");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            // remove all the pickTrans data for that pick list
            command.CommandText = "delete from pickListTrans where pickListFId=@pickListFId";
            command.Parameters.AddWithValue("@pickListFId", pickListId);
            command.ExecuteNonQuery();
            
            foreach(DataRow dRow in barcodeDet.Rows)
            {
                string[] barcodeArr = dRow["SKU"].ToString().Split('/');
                command.CommandText = "select StyleID from ItemStyle where StyleCode=@StyleCode";
                command.Parameters.AddWithValue("@StyleCode", barcodeArr[0].ToString());
                string styleIDS = command.ExecuteScalar().ToString();

                command.CommandText = "select SizeID from Size where Size1=@Size1";
                command.Parameters.AddWithValue("@Size1", barcodeArr[1].ToString());
                string SizeIDs = command.ExecuteScalar().ToString();

                command.CommandText = "insert into pickListTrans (pickListFId,styleId,sizeId,quantity,quotedPrice,sku) " +
                    "values (@pickListFId1,@styleId,@sizeId,@quantity,@quotedPrice,@sku)";
                command.Parameters.AddWithValue("@pickListFId1", pickListId);
                command.Parameters.AddWithValue("@styleId", Convert.ToInt32(styleIDS));
                command.Parameters.AddWithValue("@sizeId", Convert.ToInt32(SizeIDs));
                command.Parameters.AddWithValue("@quantity", Convert.ToInt32(dRow["Qty"].ToString()));
                command.Parameters.AddWithValue("@quotedPrice", Convert.ToDecimal(dRow["mrp"].ToString()));
                command.Parameters.AddWithValue("@sku", dRow["SKU"].ToString());
                command.ExecuteNonQuery();

                command.Parameters.Clear();
            }

            // update log
            command.CommandText = "update pickList set logs+=@logs";            
            command.Parameters.AddWithValue("@logs", userName+":"+DateTime.Now.ToString());
            command.ExecuteNonQuery();

            command.Parameters.Clear();

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();
            res = "updated successfully";

        }
        catch (Exception ex)
        {
            res = "Exception Occured";
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
        return res;
    }
}