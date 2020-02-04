using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for statusControl
/// </summary>
public class statusControlCls
{
    string userId = HttpContext.Current.Session["login"].ToString();//id of logged in admin
    string userName = HttpContext.Current.Session["userName"].ToString();//id of logged in admin

    public statusControlCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable getStockupInv()
    {
        DataTable dt = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("SInv");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select StockupID,BarcodeNo,a.Status,a.mrp,s.Title,c.ItemCategory,col.C1Name from (select StockupID,BarcodeNo,Status,mrp,StyleID from StockUpInward where Status!=@status) a " +
                "inner join ItemStyle s on s.StyleID = a.StyleID " +
                "inner join ItemCategory c on c.ItemCategoryID = s.ItemCatID " +
                "left join Column1 col on col.Col1ID = s.Col1 ";
            command.Parameters.AddWithValue("@status", "SOLD");
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

    public int changeBarcodeStatus(string StockupID, string Status,string oldstatus,string barcode)
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
        transaction = connection.BeginTransaction("BulkListing");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "update StockUpInward set Status=@status,printed=@printed,physicalId=@physicalId,subLocId=@subLocId,rackId=@rackId,stackId=@stackId where StockupID=@StockupID";
            command.Parameters.AddWithValue("@status", Status);
            command.Parameters.AddWithValue("@printed", "No");
            command.Parameters.AddWithValue("@physicalId", DBNull.Value);
            command.Parameters.AddWithValue("@subLocId", DBNull.Value);
            command.Parameters.AddWithValue("@rackId", DBNull.Value);
            command.Parameters.AddWithValue("@stackId", DBNull.Value);
            command.Parameters.AddWithValue("@StockupID", StockupID);
            command.ExecuteNonQuery();
            command.Parameters.Clear();

            DataTable dtlogin = new DataTable();
            command.CommandText = "select l.userrole from login l where l.userid=@userid";
            command.Parameters.AddWithValue("@userid", userId);
            dtlogin.Load(command.ExecuteReader());
            command.Parameters.Clear();

            command.CommandText = "insert into status_log (userrole,userid,barcodeoldstus,barcodenewstus,barcodeno,datetime) values(@userrole,@userid,@barcodeoldstus,@barcodenewstus,@barcodeno,@datetime)";
            command.Parameters.AddWithValue("@userrole", dtlogin.Rows[0]["userrole"].ToString());
            command.Parameters.AddWithValue("@userid", userId);
           
            command.Parameters.AddWithValue("@barcodeoldstus", oldstatus);
            command.Parameters.AddWithValue("@barcodenewstus", Status);
            command.Parameters.AddWithValue("@barcodeno", barcode);
            command.Parameters.AddWithValue("@datetime",DateTime.Now);
            command.ExecuteNonQuery();


            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();
            return 1;
        }
        catch (Exception ex)
        {
            try
            {
                //transaction.Rollback();
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex);
                return 0;

            }
            catch (Exception ex2)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex2);
                return 0;

            }
        }
    }

    public DataTable getStockupInvByBarcode(string BarcodeNo)
    {
        DataTable dt = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("SInvBarc");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select StockupID,BarcodeNo,a.Status,a.mrp,s.Title,c.ItemCategory,col.C1Name from (select StockupID,BarcodeNo,Status,mrp,StyleID from StockUpInward where Status!=@status and BarcodeNo=@BarcodeNo) a " +
                "inner join ItemStyle s on s.StyleID = a.StyleID " +
                "inner join ItemCategory c on c.ItemCategoryID = s.ItemCatID " +
                "left join Column1 col on col.Col1ID = s.Col1 ";
            command.Parameters.AddWithValue("@status", "SOLD");
            command.Parameters.AddWithValue("@BarcodeNo", BarcodeNo);
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

    public DataTable getCurrentStatusOfBarcode(string BarcodeNo)
    {
        DataTable dt = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("CurStats");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select * from StockUpInward where BarcodeNo=@BarcodeNo";
            command.Parameters.AddWithValue("@BarcodeNo", BarcodeNo);
            DataTable checkStatus = new DataTable();
            checkStatus.Load(command.ExecuteReader());
            reportCls obj = new reportCls();
            if(checkStatus.Rows.Count.Equals(0))
            {
                //archived
                dt = obj.getSalesDumpArchive(BarcodeNo);
            }
            else
            {
                if(checkStatus.Rows[0]["Status"].ToString().Equals("SOLD"))
                {
                    // marked sold in Stockupinward

                    dt = obj.getSoldDump(BarcodeNo);
                }
                else
                {
                    dt = obj.getDump(BarcodeNo);
                }
            }

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
        return dt;

    }

    public DataTable getcommonDetailsOfBarcode(string BarcodeNo)
    {
        DataTable dt = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("barcDets");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select displayStatus,a.*,i.Title,i.StyleCode,i.mrp,i.Control1,i.Control2,i.Control3,i.Control4," +
                "i.Control5,i.Control6,i.Control7,i.Control8,case when i.image1 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image1) else '' end as image1,case when i.image2 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image2) else '' end as image2,case when i.image3 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image3) else '' end as image3," +
                "case when i.image4 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image4) else '' end as image4," +
 "case when i.image5 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image5) else '' end as image5," +
 "case when i.image6 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image6) else '' end as image6,c.ItemCategory,col1.C1Name,lo.BagDescription," +
 "cat2.C2Name,cat3.C3Name,cat4.C4Name,cat5.C5Name,col2.C2Name as col2Name,col3.C3Name as col3Name,col4.C4Name as col4Name" +
 ",col5.C5Name as col5Name,col6.C6Name,col7.C7Name,col8.C8Name,col9.C9Name,col10.C10Name,col11.C11Name,col12.C12Name,col13.C13Name,col14.C14Name," +
 "DATEDIFF(d, a.SystemDate, getDate()) as stockAge,DATEDIFF(d, lo.invoiceDate, getDate()) as lotAge," +
 "loc.Location,sz.Size1,concat(i.StyleCode, '-', sz.Size1) as SKU," +
 "(select STRING_AGG(concat(lo.Location, '-', listidgivenbyvloc, ':', listprice), CHAR(13) + CHAR(10)) from(select l1.listidgivenbyvloc, l1.listprice, l1.saleschannelvlocid  from listchannelrecord l1 where l1.styleId = a.StyleID and l1.sizeId = sz.SizeID) l inner join location lo on l.saleschannelvlocid = lo.locationID ) listDets" +
  " from (select s.Status as displayStatus,s.printed,s.BagID, s.SystemDate, l.username, s.initialStatus, s.StockupID, s.StyleID, s.SizeID, s.RackBarcode, s.RackDate, s.physicalId,e.EAN from StockUpInward s inner" +
  " join login l on l.userid = s.UserID left join EAN e on e.styleid=s.StyleID and e.sizeid=s.SizeID where BarcodeNo = @BarcodeNo" +
 " union all" +
 " select sa.Status as displayStatus,sa.printed,sa.BagID, sa.SystemDate, la.username, sa.initialStatus, sa.StockupID, sa.StyleID, sa.SizeID, RackBarcode, RackDate, physicalId,e.EAN from ArchiveStockUpInward sa inner join login la on la.userid = sa.UserID left join EAN e on e.styleid=sa.StyleID and e.sizeid=sa.SizeID where BarcodeNo = @BarcodeNo1) a" +
           " inner join ItemStyle i on i.StyleID = a.StyleID" +
 " inner join Size sz on sz.SizeID = a.SizeID" +
 " inner join ItemCategory c on c.ItemCategoryID = i.ItemCatID" +
 " inner join Column1 col1 on col1.Col1ID = i.Col1" +
 " inner join Lot lo on lo.BagId = a.BagID" +
 " left join Category2 cat2 on cat2.Cat2ID = i.Cat2ID" +
 " left join Category3 cat3 on cat3.Cat3ID = i.Cat3ID" +
 " left join Category4 cat4 on cat4.Cat4ID = i.Cat4ID" +
 " left join Category5 cat5 on cat5.Cat5ID = i.Cat5ID" +
 " left join Column2 col2 on col2.Col2ID = i.Col2" +
 " left join Column3 col3 on col3.Col3ID = i.Col3" +
 " left join Column4 col4 on col4.Col4ID = i.Col4" +
 " left join Column5 col5 on col5.Col5ID = i.Col5" +
 " left join Column6 col6 on col6.Col6ID = i.Col6" +
 " left join Column7 col7 on col7.Col7ID = i.Col7" +
 " left join Column8 col8 on col8.Col8ID = i.Col8" +
 " left join Column9 col9 on col9.Col9ID = i.Col9" +
 " left join Column10 col10 on col10.Col10ID = i.Col10" +
 " left join Column11 col11 on col11.Col11ID = i.Col11" +
 " left join Column12 col12 on col12.Col12ID = i.Col12" +
 " left join Column13 col13 on col13.Col13ID = i.Col13" +
 " left join Column14 col14 on col14.Col14ID = i.Col14" +
 " left join Location loc on loc.LocationID = a.physicalId";
            command.Parameters.AddWithValue("@BarcodeNo", BarcodeNo);
            command.Parameters.AddWithValue("@BarcodeNo1", BarcodeNo);
            dt.Load(command.ExecuteReader());

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
        return dt;

    }

    public DataTable getSalesFullDetails(string BarcodeNo)
    {
        DataTable dt = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("CurStats");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select s.status,s.salesDateTime,l.username as salesUser,s.salesidgivenbyvloc,s.dispatchtimestamp,lD.username as dispatchUser,s.returntimestamp,lR.username as retUser,changeStatus,vloc.Location as vlocs,s.salesAbwno,sco.courierName,rco.courierName as rcourierName,s.returnAbwno,s.remarks,case when s.reasons = '1' then 'COD Return' when s.reasons = '2' then 'Customer Return' when s.reasons = '3' then 'Customer Cancellation' when s.reasons = '4' then 'Others' else '' end as reasons,null as cancleUser,null as cancelTimeStamp,null as canReason,null as canclereason from salesrecord s inner join StockUpInward si on si.StockupID=s.itemid and BarcodeNo = @BarcodeNo inner join Location vloc on vloc.LocationID=s.saleschannelvlocid left join courier sco on sco.courierId=s.salesCourier left join courier rco on rco.courierId=s.returnCourier left join login l on l.userid=s.salesUserId left join login lD on lD.userid=s.dispatchuserid left join login lR on lR.userid=s.returnuserid " +
                "union all " +
                "select s1.status,s1.salesDateTime,l1.username as salesUser,s1.salesidgivenbyvloc,s1.dispatchtimestamp,lD1.username as dispatchUser,s1.returntimestamp,lR1.username as retUser,changeStatus,vloc1.Location as vlocs,s1.salesAbwno,sco1.courierName,rco1.courierName as rcourierName,s1.returnAbwno,s1.remarks,case when s1.reasons = '1' then 'COD Return' when s1.reasons = '2' then 'Customer Return' when s1.reasons = '3' then 'Customer Cancellation' when s1.reasons = '4' then 'Others' else '' end as reasons,null as cancleUser,null as cancelTimeStamp,null as canReason,null as canclereason from salesrecord s1 inner join ArchiveStockUpInward si1 on si1.StockupID = s1.itemid and BarcodeNo = @BarcodeNo1 inner join Location vloc1 on vloc1.LocationID = s1.saleschannelvlocid left join courier sco1 on sco1.courierId = s1.salesCourier left join courier rco1 on rco1.courierId = s1.returnCourier left join login l1 on l1.userid = s1.salesUserId left join login lD1 on lD1.userid = s1.dispatchuserid left join login lR1 on lR1.userid = s1.returnuserid " +
                "union all select s11.status,s11.salesDateTime,l11.username as salesUser,s11.salesidgivenbyvloc,s11.dispatchtimestamp,lD11.username as dispatchUser,s11.returntimestamp,lR11.username as retUser,changeStatus,vloc11.Location as vlocs,s11.salesAbwno,sco11.courierName,rco11.courierName as rcourierName,s11.returnAbwno,s11.remarks,case when s11.reasons = '1' then 'COD Return' when s11.reasons = '2' then 'Customer Return' when s11.reasons = '3' then 'Customer Cancellation' when s11.reasons = '4' then 'Others' else '' end as reasons,null as cancleUser,null as cancelTimeStamp,null as canReason,null as canclereason from salesrecord s11 inner join ArchiveStockUpInward si11 on si11.ArchiveStockupID = s11.archiveid and BarcodeNo = @BarcodeNo2 inner join Location vloc11 on vloc11.LocationID = s11.saleschannelvlocid left join courier sco11 on sco11.courierId = s11.salesCourier left join courier rco11 on rco11.courierId = s11.returnCourier left join login l11 on l11.userid = s11.salesUserId left join login lD11 on lD11.userid = s11.dispatchuserid left join login lR11 on lR11.userid = s11.returnuserid " +
            "union all " +
"select s4.status,s4.salesDateTime,l4.username as salesUser,s4.salesidgivenbyvloc,s4.dispatchtimestamp,'' as dispatchUser,s4.returntimestamp,'' as retUser,changeStatus,vloc4.Location as vlocs,s4.salesAbwno,sco4.courierName,'' as rcourierName,s4.returnAbwno,'' as remarks,'' as reasons,lc4.username as cancleUser,s4.cancelTimeStamp,canReason,canclereason from cancelTrans s4 inner join StockUpInward si4 on si4.StockupID = s4.itemid and BarcodeNo = @BarcodeNo4 inner join Location vloc4 on vloc4.LocationID = s4.saleschannelvlocid left join courier sco4 on sco4.courierId = s4.salesCourier left join login l4 on l4.userid = s4.salesUserId left join login lc4 on lc4.userid = s4.cancelId " +
"union all " +
"select s5.status,s5.salesDateTime,l5.username as salesUser,s5.salesidgivenbyvloc,s5.dispatchtimestamp,'' as dispatchUser,s5.returntimestamp,'' as retUser,changeStatus,vloc5.Location as vlocs,s5.salesAbwno,sco5.courierName,'' as rcourierName,s5.returnAbwno,'' as remarks,'' as reasons,lc5.username as cancleUser,s5.cancelTimeStamp,canReason,canclereason from cancelTrans s5 inner join ArchiveStockUpInward si5 on si5.StockupID = s5.itemid and BarcodeNo = @BarcodeNo5 inner join Location vloc5 on vloc5.LocationID = s5.saleschannelvlocid left join courier sco5 on sco5.courierId = s5.salesCourier left join login l5 on l5.userid = s5.salesUserId left join login lc5 on lc5.userid = s5.cancelId " +
"union all " +
"select s6.status,s6.salesDateTime,l6.username as salesUser,s6.salesidgivenbyvloc,s6.dispatchtimestamp,'' as dispatchUser,s6.returntimestamp,'' as retUser,changeStatus,vloc6.Location as vlocs,s6.salesAbwno,sco6.courierName,'' as rcourierName,s6.returnAbwno,'' as remarks,'' as reasons,lc6.username as cancleUser,s6.cancelTimeStamp,canReason,canclereason from cancelTrans s6 inner join ArchiveStockUpInward si6 on si6.ArchiveStockupID = s6.archiveid and BarcodeNo = @BarcodeNo6 inner join Location vloc6 on vloc6.LocationID = s6.saleschannelvlocid left join courier sco6 on sco6.courierId = s6.salesCourier left join login l6 on l6.userid = s6.salesUserId left join login lc6 on lc6.userid = s6.cancelId" +
" union all " +
"select st.barcodenewstus,st.datetime as salesDateTime,l.username as salesUser,null as salesidgivenbyvloc,null as dispatchtimestamp,null as dispatchUser,null as returntimestamp,null as  retUser,st.barcodenewstus as changeStatus,null as vlocs,null as salesAbwno,null as courierName,null as rcourierName,null as returnAbwno,null as remarks, 'changeStatus' as reasons,null as cancleUser,null as cancelTimeStamp, null as canReason,null as canclereason from status_log st  inner join login l on l.userid=st.userid and st.barcodeno =@BarcodeNo8 ";

            //"select lab.labelStatus,lab.entryDate as salesDateTime,l.username as salesUser,null as salesidgivenbyvloc,null as dispatchtimestamp,null as dispatchUser,null as returntimestamp,null as retUser,null as changeStatus,null as vlocs,null as salesAbwno,null as courierName,null as rcourierName,null as returnAbwno,null as remarks,'labelsPrint' as reasons,null as cancleUser,null as cancelTimeStamp,null as canReason,null as canclereason from labelLogs lab inner join login l on l.userid = lab.labelUserId and lab.barcodeNo =@BarcodeNo7 " +
//" union all " +
            command.Parameters.AddWithValue("@BarcodeNo", BarcodeNo);
            command.Parameters.AddWithValue("@BarcodeNo1", BarcodeNo);
            command.Parameters.AddWithValue("@BarcodeNo2", BarcodeNo);
            command.Parameters.AddWithValue("@BarcodeNo4", BarcodeNo);
            command.Parameters.AddWithValue("@BarcodeNo5", BarcodeNo);
            command.Parameters.AddWithValue("@BarcodeNo6", BarcodeNo);
            command.Parameters.AddWithValue("@BarcodeNo7", BarcodeNo);
            command.Parameters.AddWithValue("@BarcodeNo8", BarcodeNo);

            dt.Load(command.ExecuteReader());

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
        return dt;

    }

    public DataTable getCancleSalesFullDets(string BarcodeNo)
    {
        DataTable dt = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("CanStats");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select s.salesDateTime,l.username as salesUser,s.salesidgivenbyvloc,s.dispatchtimestamp,'' as dispatchUser,s.returntimestamp,'' as retUser,changeStatus,vloc.Location as vlocs,s.salesAbwno,sco.courierName,'' as rcourierName,s.returnAbwno,'' as remarks,'' as reasons,lc.username as cancleUser,Convert(nvarchar(max),s.cancelTimeStamp,1) as cancelTimeStamp,canReason,canclereason from cancelTrans s inner join StockUpInward si on si.StockupID=s.itemid and BarcodeNo = @BarcodeNo inner join Location vloc on vloc.LocationID=s.saleschannelvlocid left join courier sco on sco.courierId=s.salesCourier left join login l on l.userid=s.salesUserId left join login lc on lc.userid=s.cancelId " +
                "union all " +
                "select s1.salesDateTime,l1.username as salesUser,s1.salesidgivenbyvloc,s1.dispatchtimestamp,'' as dispatchUser,s1.returntimestamp,'' as retUser,changeStatus,vloc1.Location as vlocs,s1.salesAbwno,sco1.courierName,'' as rcourierName,s1.returnAbwno,'' as remarks,'' as reasons,lc1.username as cancleUser,Convert(nvarchar(max),s1.cancelTimeStamp,1) as cancelTimeStamp,canReason,canclereason from cancelTrans s1 inner join ArchiveStockUpInward si1 on si1.StockupID = s1.itemid and BarcodeNo = @BarcodeNo1 inner join Location vloc1 on vloc1.LocationID = s1.saleschannelvlocid left join courier sco1 on sco1.courierId = s1.salesCourier left join login l1 on l1.userid = s1.salesUserId left join login lc1 on lc1.userid = s1.cancelId " +
                "union all select s11.salesDateTime,l11.username as salesUser,s11.salesidgivenbyvloc,s11.dispatchtimestamp,'' as dispatchUser,s11.returntimestamp,'' as retUser,changeStatus,vloc11.Location as vlocs,s11.salesAbwno,sco11.courierName,'' as rcourierName,s11.returnAbwno,'' as remarks,'' as reasons,lc11.username as cancleUser,Convert(nvarchar(max),s11.cancelTimeStamp,1) as cancelTimeStamp,canReason,canclereason from cancelTrans s11 inner join ArchiveStockUpInward si11 on si11.ArchiveStockupID = s11.archiveid and BarcodeNo = @BarcodeNo2 inner join Location vloc11 on vloc11.LocationID = s11.saleschannelvlocid left join courier sco11 on sco11.courierId = s11.salesCourier left join login l11 on l11.userid = s11.salesUserId left join login lc11 on lc11.userid = s11.cancelId";
            command.Parameters.AddWithValue("@BarcodeNo", BarcodeNo);
            command.Parameters.AddWithValue("@BarcodeNo1", BarcodeNo);
            command.Parameters.AddWithValue("@BarcodeNo2", BarcodeNo);
            dt.Load(command.ExecuteReader());

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
        return dt;

    }
}