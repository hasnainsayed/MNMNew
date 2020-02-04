using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for stockUpCls
/// </summary>
public class stockUpCls
{
    public stockUpCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable getSoldList()
    {
        DataTable itemTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("soldList");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            //command.CommandText = "select si.StockupID,si.StyleID,s.StyleCode,sz.Size1,si.mrp,si.LastBarcode,concat(s.StyleCode,'/',sz.Size1,'/',si.LastBarcode) as fullBarcode,si.RackBarcode,c.ItemCategory,col.C1Name,sale.salesidgivenbyvloc,sale.sid from StockUpInward si inner join ItemStyle s on s.StyleID = si.StyleID inner join Size sz on sz.SizeID = si.SizeID inner join ItemCategory c on c.ItemCategoryID = s.ItemCatID inner join Column1 col on col.Col1ID = s.Col1 inner join salesrecord sale on sale.itemid = si.StockupID and sale.status = 'ND' where si.status = @status";
            command.CommandText = "select si.StockupID,si.StyleID,s.StyleCode,sz.Size1,si.mrp,si.LastBarcode,si.BarcodeNo as fullBarcode,si.RackBarcode,c.ItemCategory,col.C1Name,sale.salesidgivenbyvloc,sale.sid,c2.C2Name,loc.Location,s.image1 from StockUpInward si inner join ItemStyle s on s.StyleID = si.StyleID inner join Size sz on sz.SizeID = si.SizeID inner join ItemCategory c on c.ItemCategoryID = s.ItemCatID inner join Column1 col on col.Col1ID = s.Col1 inner join salesrecord sale on sale.itemid = si.StockupID and sale.status = 'ND' left join Category2 c2 on c2.Cat2ID=s.Cat2ID inner join Location loc on loc.LocationID=sale.saleschannelvlocid where si.status = @status";
            command.Parameters.AddWithValue("@status", "SOLD");
            itemTable.Load(command.ExecuteReader());

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
        return itemTable;
    }
}