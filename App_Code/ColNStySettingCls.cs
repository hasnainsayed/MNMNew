using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

/// <summary>
/// Summary description for ColNStySettingCls
/// </summary>
public class ColNStySettingCls
{
    public ColNStySettingCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int addEditcolnstysetting(DataTable dt)
    {
        DataTable Table = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }
        int result = 0;
        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("addEditcolnstysetting");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["cmgfrom"].ToString().Equals("-1"))
                {
                    command.CommandText = "insert into checkColSetting (catid,mandatory,optinal,Na,colid,tablename) values(@catid,@mandatory,@optinal,@Na,@colid,@tablename)";

                }
                else
                {
                    command.CommandText = "update checkColSetting set mandatory=@mandatory,optinal=@optinal,Na=@Na where colid=@colid";
                    
                }
                command.Parameters.AddWithValue("@catid", dr["itemcatid"]);
                
                command.Parameters.AddWithValue("@mandatory", dr["mandatory"]);
                command.Parameters.AddWithValue("@optinal", dr["optinal"]);
                command.Parameters.AddWithValue("@Na", dr["Na"]);
                command.Parameters.AddWithValue("@colid", dr["colid"]);
                command.Parameters.AddWithValue("@tablename", dr["TableName"]);
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }

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
    public DataTable BindColSetting(string catid)
    {
        DataTable stock = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("BindColSetting");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select s.*,c.mandatory,c.optinal,c.Na,c.colsetid from ColTableSetting s left join checkColSetting c on c.colid=s.CTSettingID where  c.catid=@catid";
            command.Parameters.AddWithValue("@catid", catid);
            stock.Load(command.ExecuteReader());


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
        return stock;
    }
    public DataTable BindStycolSetting(string catid)
    {
        DataTable stock = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("BindStycolSetting");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select s.*,c.mandatory,c.optinal,c.Na,c.stycolid from StyleColumnSettings s left join checkStycolSetting c on c.colid=s.StyleCSID where  c.catid=@catid";
            command.Parameters.AddWithValue("@catid", catid);
           
            stock.Load(command.ExecuteReader());


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
        return stock;
    }
    public DataTable Bind()
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
        transaction = connection.BeginTransaction("Bind");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select Distinct c.catid,i.ItemCategory  from checkColSetting c inner join ItemCategory i on i.ItemCategoryID=c.catid";
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
    public DataTable BindCategoryforStylecol()
    {
        DataTable stock = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("BindCategoryforStylecol");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select Distinct c.catid,i.ItemCategory  from checkStycolSetting c inner join ItemCategory i on i.ItemCategoryID=c.catid";
           

            stock.Load(command.ExecuteReader());


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
        return stock;
    }
    public DataTable GetStyleColumnbycatid(string catid)
    {
        DataTable stock = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("GetStyleColumnbycatid");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select c.*,s.SettingName from checkStycolSetting c inner join StyleColumnSettings s on s.StyleCSID=c.colid   where c.catid=@catid";
            command.Parameters.AddWithValue("@catid", catid);
            stock.Load(command.ExecuteReader());


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
        return stock;
    }
    public DataTable Getcolcumnsettingbycatid(string catid)
    {
        DataTable stock = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("Getcolcumnsettingbycatid");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select c.*,s.SettingName from checkColSetting c inner join ColTableSetting s on s.CTSettingID=c.colid   where c.catid=@catid";
            command.Parameters.AddWithValue("@catid", catid);
            stock.Load(command.ExecuteReader());


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
        return stock;
    }
    public int addEditstycolumnsetting(DataTable dt2)
    {
        DataTable Table = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }
        int result = 0;
        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("addEditstycolumnsetting");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {

            foreach (DataRow dr in dt2.Rows)
            {
                if (dr["cmgfrom"].ToString().Equals("-1"))
                {
                    command.CommandText = "insert into checkStycolSetting (catid,mandatory,optinal,Na,colid,colname) values(@catid,@mandatory,@optinal,@Na,@colid,@colname)";

                }
                else
                {
                    command.CommandText = "update checkStycolSetting set mandatory=@mandatory,optinal=@optinal,Na=@Na where colid=@colid";
                    
                }
                command.Parameters.AddWithValue("@catid", dr["itemcatid"]);
                
                command.Parameters.AddWithValue("@mandatory", dr["mandatory"]);
                command.Parameters.AddWithValue("@optinal", dr["optinal"]);
                command.Parameters.AddWithValue("@Na", dr["Na"]);
                command.Parameters.AddWithValue("@colid", dr["stycolid"]);
                command.Parameters.AddWithValue("@colname", dr["colname"]); 
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }



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
    public DataTable gettable(string tablename)
    {
        DataTable stock = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("gettable");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select  * from "+tablename+"";
            stock.Load(command.ExecuteReader());


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
        return stock;
    }
    public DataTable Check(string catid, string tablename)
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
        transaction = connection.BeginTransaction("Check");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select * from " + tablename + " s where s.catid=@catid";
            command.Parameters.AddWithValue("@catid", catid);
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
    public DataTable Checkcategory(string catid)
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
        transaction = connection.BeginTransaction("Check");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select Distinct c.catid,s.catid from checkColSetting c inner join checkStycolSetting s on s.catid=c.catid where c.catid=@catid1 and s.catid=@catid2";
            command.Parameters.AddWithValue("@catid1", catid);
            command.Parameters.AddWithValue("@catid2", catid);
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
    public DataTable GetStyleSetting(string catid,string colid)
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
        transaction = connection.BeginTransaction("GetStyleSetting");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select * from checkStycolSetting c where c.colid=@colid and c.catid=@catid";
            command.Parameters.AddWithValue("@catid", catid);
            command.Parameters.AddWithValue("@colid", colid);
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

    public DataTable GetColumnSetting(string catid, string colid)
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
        transaction = connection.BeginTransaction("GetColumnSetting");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select * from checkColSetting c where c.colid=@colid and c.catid=@catid";
            command.Parameters.AddWithValue("@catid", catid);
            command.Parameters.AddWithValue("@colid", colid);
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


}