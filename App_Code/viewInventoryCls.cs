
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for viewInventoryCls
/// </summary>
public class viewInventoryCls
{

	string userId = HttpContext.Current.Session["login"].ToString();//id of logged in user
    string userName = HttpContext.Current.Session["userName"].ToString();//name of logged in user
    public viewInventoryCls()
	{
		//
		// TODO: Add constructor logic here
		//
	}

	public DataTable bindInventory()
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
		transaction = connection.BeginTransaction("bindInventory");
		command.Connection = connection;
		command.Transaction = transaction;
		try
		{
			command.CommandText = "select si.StyleID,s.StyleCode,s.Title,s.mrp,ic.ItemCategory from StockUpInward si inner join ItemStyle s on s.StyleID=si.StyleID inner join ItemCategory ic on ic.ItemCategoryID=s.ItemCatID where si.status IN('RFL','NRM','NRD','NRR','SOLD') GROUP BY si.StyleID,s.StyleCode,s.Title,s.mrp,ic.ItemCategory";
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

    public DataTable bindInventorySearch(DataTable cat, DataTable field, DataTable drp)
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
        transaction = connection.BeginTransaction("invSearch");
        command.Connection = connection;
        command.Transaction = transaction;
        DataTable dt = new DataTable();
        try
        {

            command.CommandText = "select si.StyleID,s.StyleCode,s.Title,s.mrp,ic.ItemCategory from StockUpInward si inner join ItemStyle s on s.StyleID=si.StyleID inner join ItemCategory ic on ic.ItemCategoryID=s.ItemCatID where si.status IN('RFL','NRM','NRD','NRR','SOLD') ";
            string subQuery = string.Empty;
            foreach (DataRow row in cat.Rows)
            {
                subQuery += " and " + row["Column"] + "=" + row["Search"];
            }

            foreach (DataRow row in drp.Rows)
            {
                subQuery += " and " + row["Column"] + "=" + row["Search"];
            }

            foreach (DataRow row in field.Rows)
            {
                subQuery += " and " + row["Column"] + " LIKE '%" + row["Search"] + "%'";
            }
            if (!subQuery.Equals(""))
            {
                command.CommandText += subQuery;
            }

            command.CommandText += "  GROUP BY si.StyleID,s.StyleCode,s.Title,s.mrp,ic.ItemCategory";
            dt.Load(command.ExecuteReader());
            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();
            return dt;
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
                return dt;
            }
            catch (Exception ex2)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex2);
                return dt;

            }
        }
    }

    public DataTable bindInventorySizeDet(string StyleID)
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
		transaction = connection.BeginTransaction("bindInvenSize");
		command.Connection = connection;
		command.Transaction = transaction;
		try
		{
			command.CommandText = "SELECT STRING_AGG(sizedet,', ') sizedet from (select CONCAT('<b>',sz.Size1,'</b>',': RFL(#',count(CASE WHEN si.status = 'RFL' THEN 1 END),'), NRM(#',count(CASE WHEN si.status = 'NRM' THEN 1 END),'), NRD(#',count(CASE WHEN si.status = 'NRD' THEN 1 END),'), NRR(#',count(CASE WHEN si.status = 'NRR' THEN 1 END),'), SOLD(#',count(CASE WHEN si.status = 'SOLD' THEN 1 END),')') sizedet from StockUpInward si inner join Size sz on sz.SizeID = si.SizeID where si.status IN('RFL','NRM','NRD','NRR','SOLD') and si.StyleID = @StyleID GROUP By si.SizeID,sz.Size1) a";
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

	public DataTable bindInventoryListDet(string StyleID)
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
		transaction = connection.BeginTransaction("bindInvenList");
		command.Connection = connection;
		command.Transaction = transaction;
		try
		{
			command.CommandText = "SELECT STRING_AGG(Location,', ') listdet from (select loc.Location from StockUpInward si inner join listchannelrecord ls on ls.itemid = si.StockupID inner join Location loc on loc.LocationID = ls.saleschannelvlocid where si.status IN('RFL','NRM','NRD','NRR','SOLD') and si.StyleID = @StyleID group by ls.saleschannelvlocid, loc.Location )a";
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

	public DataTable bindItemsToList(string StyleID,string saleschannelvlocid)
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
		transaction = connection.BeginTransaction("bindInvenList");
		command.Connection = connection;
		command.Transaction = transaction;
		try
		{
            // query in basis of item
            //command.CommandText = "select si.StockupID,si.StyleID,s.StyleCode,sz.Size1,si.mrp,si.LastBarcode,concat(s.StyleCode, '/', sz.Size1, '/', si.LastBarcode) as fullBarcode,l.listprice,l.listid,CASE WHEN l.listid is null THEN 'false' ELSE 'true' END chkItem,l.listidgivenbyvloc from StockUpInward si inner join ItemStyle s on s.StyleID = si.StyleID inner join Size sz on sz.SizeID = si.SizeID left join listchannelrecord l on l.itemid = si.StockupID and l.saleschannelvlocid = @saleschannelvlocid where si.status IN('RFL') and si.StyleID = @StyleID ";

            // query on basis of style and size
            command.CommandText = "select si.StyleID,s.StyleCode,sz.Size1," +
                " concat(s.StyleCode, '/', sz.Size1) as fullBarcode," +
                " CASE WHEN l.listid is null THEN 'false' ELSE 'true' END chkItem," +
                " l.listidgivenbyvloc,l.listid,l.listprice,sz.SizeID from StockUpInward si inner join" +
                " ItemStyle s on s.StyleID = si.StyleID" +
" inner join Size sz on sz.SizeID = si.SizeID" +
" left join listchannelrecord l on l.styleId = si.StyleID and l.sizeId = si.SizeID and" +
" l.saleschannelvlocid = @saleschannelvlocid" +
" where si.status IN('RFL') and si.StyleID = @StyleID" +
" group by si.StyleID,s.StyleCode,sz.Size1," +
" l.listidgivenbyvloc,l.sizeId,l.listid,l.listprice,sz.SizeID ";
            command.Parameters.AddWithValue("@StyleID", StyleID);
			command.Parameters.AddWithValue("@saleschannelvlocid", saleschannelvlocid);
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

	public int ListItems(DataTable dtItems)
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
		transaction = connection.BeginTransaction("listItems");
		command.Connection = connection;
		command.Transaction = transaction;
	
		try
		{
            int listid = 0;
			foreach (DataRow drow in dtItems.Rows)
			{
				if (drow["listid"].ToString().Equals(""))
				{
					if (Convert.ToBoolean(drow["chkItem"]).Equals(true))
					{
						//insert new listing
						command.CommandText = "insert into listchannelrecord (saleschannelvlocid,itemid,listidgivenbyvloc,listprice,userId,styleId,sizeId) " +
                " values (@saleschannelvlocid,@itemid,@listidgivenbyvloc,@listprice,@userId,@styleId,@sizeId) "+
                "SELECT CAST(scope_identity() AS int)";
						command.Parameters.AddWithValue("@saleschannelvlocid", drow["saleschannelvlocid"].ToString());
						command.Parameters.AddWithValue("@itemid", drow["StockUpID"].ToString());
						command.Parameters.AddWithValue("@listidgivenbyvloc", drow["listidgivenbyvloc"].ToString());
						command.Parameters.AddWithValue("@listprice", drow["listprice"].ToString());
						command.Parameters.AddWithValue("@userId", userId);
                        command.Parameters.AddWithValue("@styleId", drow["styleId"].ToString());
                        command.Parameters.AddWithValue("@sizeId", drow["sizeId"].ToString());
                        listid = (Int32)command.ExecuteScalar();
                        command.Parameters.Clear();

                        // update item style for sooperbuy website
                        if(drow["saleschannelvlocid"].ToString().Equals("20"))
                        {
                            command.CommandText = "update ItemStyle set onWebsite=1 where StyleID=@weStyleId";
                            command.Parameters.AddWithValue("@weStyleId", drow["styleId"].ToString());
                            command.ExecuteScalar();
                            command.Parameters.Clear();
                        }

                                               
                        //list log
                        command.CommandText = "insert into list_log (sku,locid,userid,type,datetime,styleid,sizeid,listid) values(@sku,@locid,@userid,@type,@datetime,@styleid,@sizeid,@listid)";
                        command.Parameters.AddWithValue("@sku", drow["sku"].ToString());
                        command.Parameters.AddWithValue("@locid", drow["saleschannelvlocid"].ToString());
                        command.Parameters.AddWithValue("@userid", userId);
                        command.Parameters.AddWithValue("@type", "Single");
                        command.Parameters.AddWithValue("@datetime",DateTime.Now);
                        command.Parameters.AddWithValue("@styleid", drow["styleId"].ToString());
                        command.Parameters.AddWithValue("@sizeid", drow["sizeId"].ToString());
                        command.Parameters.AddWithValue("@listid", listid);
                        command.ExecuteNonQuery();
                    }


				}
				else
				{
					/*if (Convert.ToBoolean(drow["chkItem"]).Equals(false))
					{
						//Delete listing removed
						command.CommandText = "DELETE FROM listchannelrecord WHERE listid=@listid ";
					}
					else
					{
						//update listidgivenbyvloc
						command.CommandText = "update listchannelrecord set listidgivenbyvloc=@listidgivenbyvloc,listprice=@listprice WHERE listid=@listid ";
					}*/
                    //update listidgivenbyvloc
                    command.CommandText = "update listchannelrecord set listidgivenbyvloc=@listidgivenbyvloc,listprice=@listprice WHERE listid=@listid ";
                    command.Parameters.AddWithValue("@listid", drow["listid"].ToString());
					command.Parameters.AddWithValue("@saleschannelvlocid", drow["saleschannelvlocid"].ToString());
					command.Parameters.AddWithValue("@itemid", drow["StockUpID"].ToString());
					command.Parameters.AddWithValue("@listidgivenbyvloc", drow["listidgivenbyvloc"].ToString());
					command.Parameters.AddWithValue("@listprice", drow["listprice"].ToString());
					command.Parameters.AddWithValue("@userId", userId);
					command.ExecuteNonQuery();

                    command.Parameters.Clear();

                    // update item style for sooperbuy website
                    if (drow["saleschannelvlocid"].ToString().Equals("20"))
                    {
                        command.CommandText = "update ItemStyle set onWebsite=1 where StyleID=@weStyleId";
                        command.Parameters.AddWithValue("@weStyleId", drow["styleId"].ToString());
                        command.ExecuteScalar();
                        command.Parameters.Clear();
                    }

                    //list log
                    command.CommandText = "insert into list_log (sku,locid,userid,type,datetime,styleid,sizeid,listid) values(@sku,@locid,@userid,@type,@datetime,@styleid,@sizeid,@listid)";
                    command.Parameters.AddWithValue("@sku", drow["sku"].ToString());
                    command.Parameters.AddWithValue("@locid", drow["saleschannelvlocid"].ToString());
                    command.Parameters.AddWithValue("@userid", userId);
                    command.Parameters.AddWithValue("@type", "Single");
                    command.Parameters.AddWithValue("@datetime", DateTime.Now);
                    command.Parameters.AddWithValue("@styleid", drow["styleId"].ToString());
                    command.Parameters.AddWithValue("@sizeid", drow["sizeId"].ToString());
                    command.Parameters.AddWithValue("@listid", drow["listid"].ToString());
                    command.ExecuteNonQuery();
                }
			
				command.Parameters.Clear();
			}

		
			transaction.Commit();
			command.Parameters.Clear();
			if (connection.State == ConnectionState.Open)
				connection.Close();
			return 0;
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