using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for newLotCls
/// </summary>
public class newLotCls
{
    public newLotCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable getLot()
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
        transaction = connection.BeginTransaction("getLot");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select l.*,v.VendorName from Lot l left join Vendor v on v.VendorID = l.VendorID";
            catTable.Load(command.ExecuteReader());

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
        return catTable;
    }

    public int addLot(string VendorID,string totalAmount, string BagDescription, string invoiceNo, string invoiceDate, string totalPiece,
        string makerId,string lotImage, string lrno,string travelCost)
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
            int result = 0;


            command.CommandText = "INSERT INTO Lot (VendorID,totalAmount,BagDescription,invoiceNo,invoiceDate,totalPiece,lotImage,lrno,travelCost) " +
                " Values (@VendorID,@totalAmount,@BagDescription,@invoiceNo,@invoiceDate,@totalPiece,@lotImage,@lrno,@travelCost) SELECT CAST(scope_identity() AS int)";
            command.Parameters.AddWithValue("@VendorID", VendorID);
            command.Parameters.AddWithValue("@totalAmount", totalAmount);
            command.Parameters.AddWithValue("@BagDescription", BagDescription);
            command.Parameters.AddWithValue("@invoiceNo", invoiceNo);
            command.Parameters.AddWithValue("@invoiceDate", Convert.ToDateTime(invoiceDate).ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@totalPiece", totalPiece);
            command.Parameters.AddWithValue("@lotImage", lotImage);
            command.Parameters.AddWithValue("@lrno", lrno);
            command.Parameters.AddWithValue("@travelCost", travelCost);
            //command.ExecuteNonQuery();
            int lot = (Int32)command.ExecuteScalar();

            // check for suspense amount and adjust it in ghost entry
            DataTable suspenseAmount = new DataTable();
            command.CommandText = "SELECT ISNULL(SUM(paymentAmount),0) AS suspenseAmount FROM lotPayment WHERE VendorID=@VendorID AND lotId IS NULL ";
            suspenseAmount.Load(command.ExecuteReader());

            // get ghost amount 
            DataTable ghostAmount = new DataTable();
            command.CommandText = "SELECT ISNULL(SUM(ghostAmount),0) AS ghostAmount FROM ghostPayment WHERE lotId IN (SELECT lotId FROM lotPayment WHERE vendorId=@VendorID) ";
            ghostAmount.Load(command.ExecuteReader());

            if (!Convert.ToDecimal(suspenseAmount.Rows[0]["suspenseAmount"]).Equals(0))
            {
                decimal suspense = (Convert.ToDecimal(suspenseAmount.Rows[0]["suspenseAmount"]))- (Convert.ToDecimal(ghostAmount.Rows[0]["ghostAmount"]));

                // is suspense amount is less or equal to lot amount
                if(suspense >= Convert.ToDecimal(totalAmount))
                {
                    suspense = Convert.ToDecimal(totalAmount);

                    // mark paidStatus as paid
                    command.CommandText = "update Lot set paidStatus=@paidStatus where BagId=@BagId";
                    command.Parameters.AddWithValue("@paidStatus", 1);
                    command.Parameters.AddWithValue("@BagId", lot);
                    command.ExecuteNonQuery();
                }

                // insert in ghost entry
                command.CommandText = "insert into ghostPayment (ghostAmount,lotId,ghostDate,makerId) values (@ghostAmount,@lotId,@ghostDate,@makerId)";
                command.Parameters.AddWithValue("@ghostAmount", suspense);
                command.Parameters.AddWithValue("@lotId", lot);
                command.Parameters.AddWithValue("@ghostDate", DateTime.Now.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@makerId", makerId);
                command.ExecuteNonQuery();

                
            }

            command.Parameters.Clear();
            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

            return lot;
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

    public int updateLot(int BagID, string VendorID, string totalAmount, string BagDescription, string invoiceNo, string invoiceDate, string totalPiece, string lotImage, string lrno, string travelCost)
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
        transaction = connection.BeginTransaction("updateLot");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            int result = 0;

            command.CommandText = "Update Lot set VendorID=@VendorID,totalAmount=@totalAmount,BagDescription=@BagDescription,invoiceNo=@invoiceNo,invoiceDate=@invoiceDate," +
                "totalPiece=@totalPiece,lrno=@lrno,travelCost=@travelCost,isActive=@isActive where BagId=@BagID ";
            command.Parameters.AddWithValue("@VendorID", VendorID);
            command.Parameters.AddWithValue("@totalAmount", totalAmount);
            command.Parameters.AddWithValue("@BagDescription", BagDescription);
            command.Parameters.AddWithValue("@invoiceNo", invoiceNo);
            command.Parameters.AddWithValue("@invoiceDate", Convert.ToDateTime(invoiceDate).ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@totalPiece", totalPiece);
            command.Parameters.AddWithValue("@BagID", BagID);
            command.Parameters.AddWithValue("@lrno", lrno);
            command.Parameters.AddWithValue("@travelCost", travelCost);
            command.Parameters.AddWithValue("@isActive", 3);
            command.ExecuteNonQuery();

            if(!lotImage.Equals(string.Empty))
            {
                command.CommandText = "update Lot set lotImage=@lotImage where BagId=@BagID";
                command.Parameters.AddWithValue("@lotImage", lotImage);
                command.ExecuteNonQuery();
            }
            command.Parameters.Clear();
            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

            return BagID;
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

    public DataTable getLotById(string BagID)
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
        transaction = connection.BeginTransaction("getLotById");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select * from Lot where BagId=@BagID";
            command.Parameters.AddWithValue("@BagID", BagID);
            catTable.Load(command.ExecuteReader());

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
        return catTable;
    }

    public int changeLotStatus(int BagID, string status)
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
        transaction = connection.BeginTransaction("chgLotSts");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            int result = 0;

            command.CommandText = "Update Lot set IsActive=@IsActive where BagId=@BagID ";
            command.Parameters.AddWithValue("@IsActive", status);
            command.Parameters.AddWithValue("@BagID", BagID);            
            command.ExecuteNonQuery();

            // if inactive set values
            if(status.Equals("0"))
            {
                // get lot details
                command.CommandText = "select * from lot where BagID=@BagID";
                DataTable lotDt = new DataTable();
                lotDt.Load(command.ExecuteReader());

                //get total pieces count
                command.CommandText = "SELECT isnull(SUM(piecePerPacket),0) AS totalPieces FROM (SELECT piecePerPacket FROM StockUpInward WHERE BagID=@BagID " +
                    "union ALL " +
                    "SELECT piecePerPacket FROM ArchiveStockUpInward WHERE BagID = @BagID1) a";
                command.Parameters.AddWithValue("@BagID1", BagID);
                DataTable pieceDt = new DataTable();
                pieceDt.Load(command.ExecuteReader());

                int pieceCnt = (Int32)pieceDt.Rows[0]["totalPieces"];
                decimal pieceAmount = 0;
                if (!pieceCnt.Equals(0))
                {
                    pieceAmount = (Decimal)lotDt.Rows[0]["travelCost"] / pieceCnt;
                }

                command.Parameters.AddWithValue("@pieceAmount", pieceAmount);
                command.CommandText = "UPDATE StockUpInward SET travelCost = piecePerPacket*@pieceAmount WHERE BagID=@BagID";
                command.ExecuteNonQuery();
                command.CommandText = "UPDATE ArchiveStockUpInward SET travelCost = piecePerPacket*@pieceAmount WHERE BagID=@BagID";
                command.ExecuteNonQuery();
            }
            else // if active set value to 0
            {
                command.Parameters.AddWithValue("@BagID1", BagID);
                command.Parameters.AddWithValue("@pieceAmount", 0);
                command.CommandText = "UPDATE StockUpInward SET travelCost = piecePerPacket*@pieceAmount WHERE BagID=@BagID";
                command.ExecuteNonQuery();
                command.CommandText = "UPDATE ArchiveStockUpInward SET travelCost = piecePerPacket*@pieceAmount WHERE BagID=@BagID";
                command.ExecuteNonQuery();
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

    public string saveBora(string years, string months, string vendorId, string noOfBora,string makerId)
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
        transaction = connection.BeginTransaction("chgLotSts");
        command.Connection = connection;
        command.Transaction = transaction;
        string res = string.Empty;
        try
        {
            command.CommandText = "select bagcode from Lot where years = @years and months = @months order by BagId desc";
            command.Parameters.AddWithValue("@years", years);
            command.Parameters.AddWithValue("@months", months);
            DataTable lotDt = new DataTable();
            lotDt.Load(command.ExecuteReader());
            int code = 0;
            if (!lotDt.Rows.Count.Equals(0))
            {
                code = 1 + Convert.ToInt32(lotDt.Rows[0]["bagcode"]);
            }

            for (int i = 0;i< Convert.ToInt32(noOfBora);i++)
            {
                command.CommandText = "insert into Lot (VendorID,years,months,bagcode,BagDescription,makerId) " +
                    "values (@VendorID,@years1,@months1,@bagcode,@Description,@makerId)";
                command.Parameters.AddWithValue("@VendorID", vendorId);
                command.Parameters.AddWithValue("@years1", years);
                command.Parameters.AddWithValue("@months1", months);
                command.Parameters.AddWithValue("@bagcode",code);
                command.Parameters.AddWithValue("@Description", years+ months+ code);
                command.Parameters.AddWithValue("@makerId", makerId);
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                code = code + 1;
            }
            
            
            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();
            res = "Bora Added";
            
        }

        catch (Exception ex)
        {
            res = "Bora Adding FAILED";
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

    public string deleteLot(string LotId)
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
        transaction = connection.BeginTransaction("chgLotSts");
        command.Connection = connection;
        command.Transaction = transaction;
        string res = string.Empty;
        try
        {
            command.CommandText = "SELECT COUNT(*) AS cnt FROM (SELECT styleID FROM StockUpInward WHERE BagID=@BagID union ALL " +
                "SELECT styleID FROM ArchiveStockUpInward WHERE BagID = @BagID1) a";
            command.Parameters.AddWithValue("@BagID", LotId);
            command.Parameters.AddWithValue("@BagID1", LotId);
            DataTable lotDt = new DataTable();
            lotDt.Load(command.ExecuteReader());
            int counts = (Int32)lotDt.Rows[0]["cnt"];
            if (!counts.Equals(0))
            {
                res = "Cannot Delete the Bora";
            }
            else
            {
                command.CommandText = "INSERT INTO delLot " +
                    "SELECT* FROM Lot " +
                    "WHERE BagID = @BagID; ";
                command.ExecuteNonQuery();

                command.CommandText = "delete from lot where BagID=@BagID";
                command.ExecuteNonQuery();

                res = "Bora Deleted";
            }
            
            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();
           

        }

        catch (Exception ex)
        {
            res = "Bora Deleting FAILED";
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