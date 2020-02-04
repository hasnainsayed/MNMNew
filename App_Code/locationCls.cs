using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for locationCls
/// </summary>
public class locationCls
{
    public locationCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable getVirtualLocation(string LTypeID2)
    {
        DataTable locTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("getVLoc");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select l.LocationID,l.Location from Location l where l.LTypeID2 =@LTypeID2";
            command.Parameters.AddWithValue("@LTypeID2", LTypeID2);
            locTable.Load(command.ExecuteReader());

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        return locTable;
    }

    public DataTable getpackageLocation()
    {
        DataTable locTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("getPackLoc");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "SELECT DISTINCT(p.rackcode) FROM packaging p WHERE rackcode IS not NULL ORDER BY rackcode asc";
           
            locTable.Load(command.ExecuteReader());

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        return locTable;
    }

    public DataTable getPackaging(string fromDate, string toDate, string vLoc, string barcode, bool vLocCheck, bool barcodeCheck, bool dateRange)
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
        transaction = connection.BeginTransaction("getTickets");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {           
            string query = string.Empty;
            query += "select * from packaging";
            
            string conV = string.Empty;
            string where = "0";
            if (!vLoc.Equals("-1") && vLocCheck.Equals(true))
            {
                if (where.Equals("0"))
                {
                    conV += " where rackcode=@vLoc";
                    where = "1";
                }
                else
                {
                    conV += " and rackcode=@vLoc";
                }
                command.Parameters.AddWithValue("@vLoc", vLoc);
            }

            
            if (!barcode.Equals("") && barcodeCheck.Equals(true))
            {
                if (where.Equals("0"))
                {
                    conV += " where packBarcode=@barcode";
                    where = "1";
                }
                else
                {
                    conV += " and packBarcode=@barcode";
                }
                command.Parameters.AddWithValue("@barcode", barcode);
            }

            if (dateRange.Equals(true))
            {
                if (where.Equals("0"))
                {
                    conV += " where (CAST (entryDate AS date) between @fromDate and @toDate)";
                    where = "1";
                }
                else
                {
                    conV += " and (CAST (entryDate AS date) between @fromDate and @toDate)";
                   
                }
                command.Parameters.AddWithValue("@fromDate", fromDate);
                command.Parameters.AddWithValue("@toDate",  toDate);
            }


            command.CommandText = query + conV;
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

    public DataTable getPhysicalLocation()
    {
        DataTable locTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("getPLoc");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select l.*,t.Name as LocationTypeName,isnull(count(s.StockupID),0) as stockCnt from Location l inner join LocationType t on l.LTypeID2 = 1 and l.LocationTypeID=t.LocationTypeID left join StockUpInward s on s.physicalId = l.LocationID group by l.LocationID,l.LocationTypeID,l.LTypeID2,l.Location,l.Contact,l.Address,t.Name,l.sendSMS";            
            locTable.Load(command.ExecuteReader());

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        return locTable;
    }

    public int getPhysicalOccupancy(string LocationID)
    {
        int count = 0;
        
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("getPLoc");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            DataTable dt = new DataTable();
            command.CommandText = "SELECT isnull(sum(st.stackQnty),0) AS totalSpace FROM Location l INNER JOIN Sublocation s ON l.LocationID=s.LocationID AND l.LocationID=@LocationID INNER JOIN Rack r ON r.SublocationID=s.SublocationID INNER JOIN Stack st ON st.RackID=r.RackID";
            command.Parameters.AddWithValue("@LocationID", LocationID);
            dt.Load(command.ExecuteReader());
            if (!dt.Rows.Count.Equals(0))
            {
                count = Convert.ToInt32(dt.Rows[0]["totalSpace"]);
            }
            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        return count;
    }

    public int getSubLocationOccupancy(string SublocationID)
    {
        int count = 0;

        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("getPLoc");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            DataTable dt = new DataTable();
            command.CommandText = "SELECT isnull(sum(st.stackQnty),0) AS totalSpace FROM Sublocation s INNER JOIN Rack r ON r.SublocationID=s.SublocationID AND s.SublocationID=@SublocationID INNER JOIN Stack st ON st.RackID=r.RackID";
            command.Parameters.AddWithValue("@SublocationID", SublocationID);
            dt.Load(command.ExecuteReader());
            if (!dt.Rows.Count.Equals(0))
            {
                count = Convert.ToInt32(dt.Rows[0]["totalSpace"]);
            }
            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        return count;
    }

    public int getRackOccupancy(string RackID)
    {
        int count = 0;

        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("getPLoc");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            DataTable dt = new DataTable();
            command.CommandText = "SELECT isnull(sum(st.stackQnty),0) AS totalSpace FROM Rack r INNER JOIN Stack st ON st.RackID=r.RackID AND r.RackID=@RackID";
            command.Parameters.AddWithValue("@RackID", RackID);
            dt.Load(command.ExecuteReader());
            if (!dt.Rows.Count.Equals(0))
            {
                count = Convert.ToInt32(dt.Rows[0]["totalSpace"]);
            }
            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        return count;
    }

    public int addUpdatePhysical(int LocationID, string LocationTypeID, string Location, string Contact, string Address)
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
        transaction = connection.BeginTransaction("AUPhysical");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            int result = 0;
            command.Parameters.AddWithValue("@LocationTypeID", LocationTypeID);
            command.Parameters.AddWithValue("@Location", Location);
            command.Parameters.AddWithValue("@Contact", Contact);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@LTypeID2", "1");

            if (LocationID.Equals(0))
            {
                command.CommandText = "insert into Location " +
                    "(LocationTypeID,Location,Contact,Address,LTypeID2) values (@LocationTypeID," +
                    "@Location,@Contact,@Address,@LTypeID2)";
            }
            else
            {
                command.CommandText = "update Location set LocationTypeID=@LocationTypeID," +
                    "Location=@Location,Contact=@Contact,Address=@Address,LTypeID2=@LTypeID2 where LocationID=@LocationID";
                command.Parameters.AddWithValue("@LocationID", LocationID);
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

    public DataTable getSubLocation(string physicalLocationID)
    {
        DataTable locTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("getSLoc");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select s.SublocationID,s.LocationID,s.Sublocation,isnull(count(st.StockupID),0) as stockCnt from Sublocation s left join StockUpInward st on s.SublocationID=st.subLocId where s.LocationID = @LocationID group by s.SublocationID,s.LocationID,s.Sublocation ";
            command.Parameters.AddWithValue("@LocationID", physicalLocationID);
            locTable.Load(command.ExecuteReader());

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        return locTable;
    }

    public int addUpdateSubLocation(int LocationID, int subLocationID, string subLocation)
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
        transaction = connection.BeginTransaction("AUSubPhy");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            int result = 0;
            command.Parameters.AddWithValue("@LocationID", LocationID);
            command.Parameters.AddWithValue("@subLocation1", subLocation);
          
            if (subLocationID.Equals(0))
            {
                command.CommandText = "select * from Sublocation where Sublocation=@Sublocation";
                command.Parameters.AddWithValue("@Sublocation", subLocation);
                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                if(!dt.Rows.Count.Equals(0))
                {
                    result = 2;
                }
                else
                {
                    command.CommandText = "insert into Sublocation (LocationID,Sublocation) values (@LocationID,@subLocation1)";
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    transaction.Commit();
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }

                
            }
            else
            {
                command.CommandText = "select * from Sublocation where Sublocation=@Sublocation and SublocationID!=@SublocationID";
                command.Parameters.AddWithValue("@Sublocation", subLocation);
                command.Parameters.AddWithValue("@SublocationID", subLocationID);
                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                if (!dt.Rows.Count.Equals(0))
                {
                    result = 2;
                }
                else
                {
                    command.CommandText = "update Sublocation set LocationID=@LocationID,Sublocation=@subLocation1 where subLocationID=@subLocationID1";
                    command.Parameters.AddWithValue("@subLocationID1", subLocationID);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    transaction.Commit();
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                    
                }
                              
            }

            

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

    public DataTable getRack(string subLocationID)
    {
        DataTable locTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("getRack");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select r.RackID,r.SublocationID,r.Rack,isnull(count(st.StockupID),0) as stockCnt from Rack r left join StockUpInward st on r.RackID=st.rackId where r.SublocationID=@SublocationID group by r.RackID,r.SublocationID,r.Rack";
            command.Parameters.AddWithValue("@SublocationID", subLocationID);
            locTable.Load(command.ExecuteReader());

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        return locTable;
    }

    public int addUpdateRack(int subLocationID, int rackID, string rack)
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
        transaction = connection.BeginTransaction("AURack");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            int result = 0;
            command.Parameters.AddWithValue("@subLocationID", subLocationID);
            command.Parameters.AddWithValue("@rack", rack);

            if (rackID.Equals(0))
            {
                command.CommandText = "select * from Rack where Rack=@rack and SublocationID=@subLocationID";
                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                if (!dt.Rows.Count.Equals(0))
                {
                    result = 2;
                }
                else
                {
                    command.CommandText = "insert into Rack (SublocationID,Rack) values (@subLocationID,@rack)";
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    transaction.Commit();
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }


            }
            else
            {
                command.CommandText = "select * from Rack where Rack=@rack and SublocationID=@subLocationID and RackID!=@rackID";
                command.Parameters.AddWithValue("@rackID", rackID);                
                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                if (!dt.Rows.Count.Equals(0))
                {
                    result = 2;
                }
                else
                {
                    command.CommandText = "update Rack set Rack=@rack,SublocationID=@subLocationID where RackID=@rackID";
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    transaction.Commit();
                    if (connection.State == ConnectionState.Open)
                        connection.Close();

                }

            }



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

    public DataTable getStack(string rackID)
    {
        DataTable locTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("getStack");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select s.StackID,s.RackID,s.Stack,s.stackQnty,isnull(count(st.StockupID),0) as stockCnt from Stack s left join StockUpInward st on s.StackID=st.stackId where s.RackID=@rackID group by s.StackID,s.RackID,s.Stack,s.stackQnty";
            command.Parameters.AddWithValue("@rackID", rackID);
            locTable.Load(command.ExecuteReader());

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        return locTable;
    }

    public int addUpdateStack(int rackID, int StackID, string stack,string occupancy)
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
        transaction = connection.BeginTransaction("AUStack");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            int result = 0;
            command.Parameters.AddWithValue("@rackID", rackID);
            command.Parameters.AddWithValue("@stack", stack);
            command.Parameters.AddWithValue("@occupancy", occupancy);
            if (StackID.Equals(0))
            {
                command.CommandText = "select * from Stack where RackID=@rackID and Stack=@stack";
                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                if (!dt.Rows.Count.Equals(0))
                {
                    result = 2;
                }
                else
                {
                    command.CommandText = "insert into Stack (RackID,Stack,stackQnty) values (@rackID,@stack,@occupancy)";
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    transaction.Commit();
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }


            }
            else
            {
                command.CommandText = "select * from Stack where RackID=@rackID and Stack=@stack and StackID!=@stackID";
                command.Parameters.AddWithValue("@stackID", StackID);
                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                if (!dt.Rows.Count.Equals(0))
                {
                    result = 2;
                }
                else
                {
                    command.CommandText = "update Stack set RackID=@rackID,Stack=@stack,stackQnty=@occupancy where StackID=@stackID";
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    transaction.Commit();
                    if (connection.State == ConnectionState.Open)
                        connection.Close();

                }

            }



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