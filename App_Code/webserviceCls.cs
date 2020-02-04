using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for webserviceCls
/// </summary>
public class webserviceCls
{
    public webserviceCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string updateRack(string rackCode, DataTable barcodes)
    {
        string result = string.Empty;
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("updateRack");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            // check if pack or rack
            if(rackCode.StartsWith("///"))
            {
                // packaging
                foreach (DataRow row in barcodes.Rows)
                {
                    string dgvBarcodes = row["Barcode"].ToString();
                    command.CommandText = "insert into packaging (packBarcode,entryDate,rackCode) values (@packBarcode,@entryDate,@rackCode)";
                    command.Parameters.AddWithValue("@packBarcode", dgvBarcodes);
                    command.Parameters.AddWithValue("@rackCode", rackCode);
                    command.Parameters.AddWithValue("@entryDate", DateTime.Now);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
            else if(rackCode.StartsWith("//"))
            {
                command.CommandText = "select s.LocationID,s.SublocationID,r.RackID,st.StackID from Sublocation s inner join Rack r on s.SublocationID=r.SublocationID inner join Stack st on r.RackID=st.RackID where s.Sublocation = substring('" + rackCode + "',3,1) and r.Rack=substring('" + rackCode + "',5,1) and st.Stack=substring('" + rackCode + "',6,1)";
                DataTable rack = new DataTable();
                rack.Load(command.ExecuteReader());

                foreach (DataRow row in barcodes.Rows)
                {
                    command.Parameters.AddWithValue("@BarcodeNo", row["Barcode"].ToString());
                    command.Parameters.AddWithValue("@RackBarcode", rackCode);
                    command.Parameters.AddWithValue("@RackDate", DateTime.Now);
                    command.Parameters.AddWithValue("@physicalId", Convert.ToInt32(rack.Rows[0]["LocationID"].ToString()));
                    command.Parameters.AddWithValue("@subLocId", Convert.ToInt32(rack.Rows[0]["SublocationID"].ToString()));
                    command.Parameters.AddWithValue("@rackId", Convert.ToInt32(rack.Rows[0]["RackID"].ToString()));
                    command.Parameters.AddWithValue("@stackId", Convert.ToInt32(rack.Rows[0]["StackID"].ToString()));
                    command.CommandText = "update StockUpInward set RackBarcode = @RackBarcode , RackDate = @RackDate," +
                    " physicalId = @physicalId, subLocId = @subLocId, rackId = @rackId, stackId = @stackId where BarcodeNo = @BarcodeNo ";
                    command.ExecuteNonQuery();

                    command.CommandText = "update ArchiveStockUpInward set RackBarcode = @RackBarcode , RackDate = @RackDate," +
                    " physicalId = @physicalId, subLocId = @subLocId, rackId = @rackId, stackId = @stackId where BarcodeNo = @BarcodeNo ";
                    command.ExecuteNonQuery(); command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            
            }
            
            transaction.Commit();
            connection.Close();
            result = "Success";
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            if (connection.State != ConnectionState.Open)
            {
                connection.Close();
            }
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
            result = "Failure";
        }
        return result;
    }
}