using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for returnItemCls
/// </summary>
public class returnItemCls
{
    string userId = HttpContext.Current.Session["login"].ToString();//id of logged in admin
    public returnItemCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable dispatchByBarcodeSales(string searchFields,string column)
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
        transaction = connection.BeginTransaction("dByBarcode");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText= "select c.ItemCategory,a.ArchiveStockupID,a.StockupID,a.BarcodeNo,s.sid,s.salesidgivenbyvloc,s.salesAbwno,co.courierName,s.salesDateTime,lo.username as soldBy,s.dispatchtimestamp,loD.username as dispatchedBy,inv.custname,inv.invid,inv.paymentMode,col.C1Name,loc.Location,si.image1 from ArchiveStockUpInward a inner join salesrecord s on s.archiveid=a.ArchiveStockupID  inner join ItemStyle si on si.StyleID=a.StyleID  inner join ItemCategory c on c.ItemCategoryID=si.ItemCatID left join courier co on co.courierId=s.salesCourier inner join login lo on lo.userid=s.salesUserId inner join login loD on loD.userid=s.dispatchuserid inner join invoice inv on inv.invid=s.invoiceid inner join Column1 col on col.Col1ID=si.Col1 inner join Location loc on loc.LocationID=s.saleschannelvlocid where " +
                " s.status=@status  and " +
                column + "=@column";
            /*command.CommandText = "select c.ItemCategory,a.ArchiveStockupID,a.StockupID,a.BarcodeNo,s.sid " +
                " from ArchiveStockUpInward a inner join salesrecord s on s.archiveid=a.ArchiveStockupID" +
                " inner join ItemStyle si on si.StyleID=a.StyleID " +
                " inner join ItemCategory c on c.ItemCategoryID=si.ItemCatID where " +
                " s.status=@status  and " +
                column +"=@column";*/

            command.Parameters.AddWithValue("@status", "DISPATCHED");
            command.Parameters.AddWithValue("@column", searchFields);
            stock.Load(command.ExecuteReader());


            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        return stock;
    }

    public DataTable websiteSales(string searchFields, string column)
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
        transaction = connection.BeginTransaction("websiteSales");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select c.ItemCategory,a.ArchiveStockupID,a.StockupID,a.BarcodeNo,s.sid,s.salesidgivenbyvloc,s.salesAbwno,co.courierName,s.salesDateTime,lo.username as soldBy,s.dispatchtimestamp,loD.username as dispatchedBy,inv.custname,inv.invid,inv.paymentMode,col.C1Name,loc.Location,si.image1 from ArchiveStockUpInward a inner join salesrecord s on s.archiveid=a.ArchiveStockupID  inner join ItemStyle si on si.StyleID=a.StyleID  inner join ItemCategory c on c.ItemCategoryID=si.ItemCatID inner join courier co on co.courierId=s.salesCourier inner join login lo on lo.userid=s.salesUserId inner join login loD on loD.userid=s.dispatchuserid inner join invoice inv on inv.invid=s.invoiceid inner join Column1 col on col.Col1ID=si.Col1 inner join Location loc on loc.LocationID=s.saleschannelvlocid where " +
                " s.status=@status and custReasonStats=@custReasonStats and " +
                column + "=@column";

            command.Parameters.AddWithValue("@custReasonStats", 1);
            command.Parameters.AddWithValue("@status", "DISPATCHED");
            command.Parameters.AddWithValue("@column", searchFields);
            stock.Load(command.ExecuteReader());


            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        return stock;
    }

    public DataTable getLatestReturns()
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
        transaction = connection.BeginTransaction("dByBarcode");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select s.BarcodeNo,sales.returntimestamp,sales.salesidgivenbyvloc,l.username,c.courierName,sales.returnAbwno,sales.remarks,loc.Location,case when (sales.reasons='1') then 'COD Return' when (sales.reasons='2') then 'Customer Return' when (sales.reasons='3') then 'Customer Cancellation' when (sales.reasons='4') then 'Others' ELSE '' END as reasons from (select top 20 * from salesrecord where status = 'RETURN' order by sid desc) sales " +
                "inner join StockUpInward s on s.StockupID = sales.itemid inner join login l on l.userid = sales.returnuserid inner join courier c on c.courierId = sales.returnCourier inner join Location loc on loc.LocationID=sales.saleschannelvlocid " +
                "union all " +
                "select sa.BarcodeNo,salesa.returntimestamp,salesa.salesidgivenbyvloc,la.username,ca.courierName,salesa.returnAbwno,salesa.remarks,loca.Location,case when(salesa.reasons = '1') then 'COD Return' when(salesa.reasons = '2') then 'Customer Return' when(salesa.reasons = '3') then 'Customer Cancellation' when(salesa.reasons = '4') then 'Others' ELSE '' END as reasons from(select top 20 * from salesrecord where status = 'RETURN' order by sid desc) salesa " +
                "inner join ArchiveStockUpInward sa on sa.StockupID = salesa.itemid inner join login la on la.userid = salesa.returnuserid inner join courier ca on ca.courierId = salesa.returnCourier inner join Location loca on loca.LocationID=salesa.saleschannelvlocid";
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

    public int markReturn(string displayStockupID, string displayArchiveStockupID, string displaysalesid, string returnStatus, 
        string courier, string awbNo,string reasons,string remarks, string video1, string video2, string image1, string image2, string image3, string image4,
        string image5, string image6, string image7, string image8, string image9,string image10)
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
        transaction = connection.BeginTransaction("markReturn");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            //insert into StockUpInward
            // command.CommandText = "SET IDENTITY_INSERT StockUpInward ON; INSERT INTO StockUpInward(StyleID, UserID, BagID, SizeID, DateTime, LastBarcode, StyleCode, BarcodeNo, RFLQty, RejectQty, Status, LocationID, ExpiredDate, ListingDate, ModeOfPayment, SoldAmount, DispatchedDate, SalesDate, RackBarcode, RackDate, CancelReason, CancelDate, ListedUserID, Picked, BulkID, BulkDate, MoneyIn, MoneyOut, ItemID, SalesID, RecordNo, CourierTransactionID, mrp, StockupID) SELECT StyleID, UserID, BagID, SizeID, DateTime, LastBarcode, StyleCode, BarcodeNo, RFLQty, RejectQty, Status, LocationID, ExpiredDate, ListingDate, ModeOfPayment, SoldAmount, DispatchedDate, SalesDate, RackBarcode, RackDate, CancelReason, CancelDate, ListedUserID, Picked, BulkID, BulkDate, MoneyIn, MoneyOut, ItemID, SalesID, RecordNo, CourierTransactionID, mrp, StockupID,SystemDate,printed FROM ArchiveStockUpInward WHERE ArchiveStockupID = @ArchiveStockupID;SET IDENTITY_INSERT StockUpInward OFF; ";
                        command.CommandText = "SET IDENTITY_INSERT StockUpInward ON; INSERT INTO StockUpInward(StyleID, UserID, BagID, SizeID, DateTime, LastBarcode, StyleCode, BarcodeNo, RFLQty, RejectQty, Status, LocationID, ExpiredDate, ListingDate, ModeOfPayment, SoldAmount, DispatchedDate, SalesDate, RackBarcode, RackDate, CancelReason, CancelDate, ListedUserID, Picked, BulkID, BulkDate, MoneyIn, MoneyOut, ItemID, SalesID, RecordNo, CourierTransactionID, mrp, StockupID,SystemDate,printed,oldBarcode,initialStatus,labels,mfgDate,labelUserId,piecePerPacket,isSample,purchaseRate,travelCost) SELECT StyleID, UserID, BagID, SizeID, DateTime, LastBarcode, StyleCode, BarcodeNo, RFLQty, RejectQty, Status, LocationID, ExpiredDate, ListingDate, ModeOfPayment, SoldAmount, DispatchedDate, SalesDate, RackBarcode, RackDate, CancelReason, CancelDate, ListedUserID, Picked, BulkID, BulkDate, MoneyIn, MoneyOut, ItemID, SalesID, RecordNo, CourierTransactionID, mrp, StockupID,SystemDate,printed,oldBarcode,initialStatus,labels,mfgDate,labelUserId,piecePerPacket,isSample,purchaseRate,travelCost FROM ArchiveStockUpInward WHERE ArchiveStockupID = @ArchiveStockupID;SET IDENTITY_INSERT StockUpInward OFF; ";

            //SELECT StyleID, UserID, BagID, SizeID, DateTime, LastBarcode, StyleCode, BarcodeNo, RFLQty, RejectQty, Status, LocationID, ExpiredDate, ListingDate, ModeOfPayment, SoldAmount, DispatchedDate, SalesDate, RackBarcode, RackDate, CancelReason, CancelDate, ListedUserID, Picked, BulkID, BulkDate, MoneyIn, MoneyOut, ItemID, SalesID, RecordNo, CourierTransactionID, mrp, StockupID, SystemDate, printed
            command.Parameters.AddWithValue("@ArchiveStockupID", displayArchiveStockupID);
            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected.Equals(1))
            {
                // update stock up with status
                command.CommandText = "update StockUpInward set Status=@Ustatus,RackBarcode=@RackBarcode,printed=@printed123 where StockupID=@updateStockupID";
                command.Parameters.AddWithValue("@Ustatus", returnStatus);
                command.Parameters.AddWithValue("@RackBarcode", "");
                command.Parameters.AddWithValue("@printed123", "No");
                command.Parameters.AddWithValue("@updateStockupID", displayStockupID);
                command.ExecuteNonQuery();


                //Delete from ArchiveStockUpInward
                command.CommandText = "DELETE FROM ArchiveStockUpInward WHERE ArchiveStockupID=@delArchiveStockupID ";
                command.Parameters.AddWithValue("@delArchiveStockupID", displayArchiveStockupID);
                int rowsAffectedDel = command.ExecuteNonQuery();

                if(rowsAffectedDel.Equals(1))
                {
                    //update sales record
                    command.CommandText = "update salesrecord set status=@status,itemid=@itemid,archiveid=@archiveid,returntimestamp=@returntimestamp," +
                        "returnuserid=@returnuserid,returnCourier=@returnCourier,returnAbwno=@returnAbwno,reasons=@reasons,remarks=@remarks,changeStatus=@changeStatus" +
                        ",rImage1=@rImage1,rImage2=@rImage2,rImage3=@rImage3,rImage4=@rImage4,rImage5=@rImage5," +
                        "rImage6=@rImage6,rImage7=@rImage7,rImage8=@rImage8,rImage9=@rImage9,rImage10=@rImage10,rVideo1=@rVideo1,rVideo2=@rVideo2 WHERE sid=@sid ";

                    command.Parameters.AddWithValue("@status", "RETURN");
                    command.Parameters.AddWithValue("@itemid", displayStockupID);
                    command.Parameters.AddWithValue("@archiveid", -1);
                    command.Parameters.AddWithValue("@returntimestamp", DateTime.Now);
                    command.Parameters.AddWithValue("@returnuserid", userId);
                    command.Parameters.AddWithValue("@returnCourier", courier);
                    command.Parameters.AddWithValue("@returnAbwno", awbNo);
                    command.Parameters.AddWithValue("@reasons", reasons);
                    command.Parameters.AddWithValue("@remarks", remarks);
                    command.Parameters.AddWithValue("@sid", displaysalesid);
                    command.Parameters.AddWithValue("@changeStatus", returnStatus);
                    command.Parameters.AddWithValue("@rImage1", image1);
                    command.Parameters.AddWithValue("@rImage2", image2);
                    command.Parameters.AddWithValue("@rImage3", image3);
                    command.Parameters.AddWithValue("@rImage4", image4);
                    command.Parameters.AddWithValue("@rImage5", image5);
                    command.Parameters.AddWithValue("@rImage6", image6);
                    command.Parameters.AddWithValue("@rImage7", image7);
                    command.Parameters.AddWithValue("@rImage8", image8);
                    command.Parameters.AddWithValue("@rImage9", image9);
                    command.Parameters.AddWithValue("@rImage10", image10);
                    command.Parameters.AddWithValue("@rVideo1", video1);
                    command.Parameters.AddWithValue("@rVideo2", video2);
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                    rowsAffected = -2;
                }
                

            }
            else
            {
                transaction.Rollback();
                rowsAffected = -2;
            }
            
            command.Parameters.Clear();

            if (connection.State == ConnectionState.Open)
                connection.Close();
            return rowsAffected;
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