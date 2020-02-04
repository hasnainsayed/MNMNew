using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for reportCls
/// </summary>
public class reportCls
{
    
    public reportCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable getNotListedSKU(string virtualLoc)
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
        transaction = connection.BeginTransaction("NotListSKU");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            //command.CommandText = "select si.StyleID,s.StyleCode,s.Title,s.mrp,ic.ItemCategory,sz.Size1,concat(s.StyleCode,'/',sz.Size1) as sku,col.C1Name from StockUpInward si inner join ItemStyle s on s.StyleID=si.StyleID inner join ItemCategory ic on ic.ItemCategoryID=s.ItemCatID inner join Size sz on si.SizeID=sz.SizeID inner join Column1 col on col.Col1ID=s.Col1 where si.status IN('RFL') and (concat(si.StyleID,'-',si.SizeID)) not in (select concat(l.styleId,'-',l.sizeId) from listchannelrecord l where l.saleschannelvlocid = @saleschannelvlocid)  group by si.StyleID,s.StyleCode,s.Title,s.mrp,ic.ItemCategory,sz.Size1,col.C1Name";
            //optimise query
            command.CommandText = "SELECT i.Title,concat(i.StyleCode,'-',sz.Size1) as sku,sz.Size1 as Size,sz.Size2 as UKSize,col.C1Name,c.ItemCategory,t.mrp,t.mrp as sp,case when i.image1 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image1) else '' end as image1" +
                ",case when i.image2 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image2) else '' end as image2," +
                "case when i.image3 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image3) else '' end as image3," +
                "case when i.image4 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image4) else '' end as image4," +
                "case when i.image5 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image5) else '' end as image5," +
                "case when i.image6 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image6) else '' end as image6 ,col2.C2Name,col3.C3Name,col4.C4Name,col5.C5Name,col6.C6Name,col7.C7Name,col8.C8Name,col9.C9Name,col10.C10Name,col11.C11Name,col12.C12Name,col13.C13Name,i.Control1,i.Control2,i.Control3,i.Control4,i.Control5,i.Control6,i.Control7,i.Control8,cat2.C2Name as cat2Name,cat3.C3Name as cat3Name" +
" FROM( " +
"select s.StyleID,s.SizeID,s.mrp from StockUpInward s where s.Status = 'RFL' and (concat(s.StyleID,'-',s.SizeID)) not in (select concat(l.styleId,'-',l.sizeId) from listchannelrecord l where l.saleschannelvlocid = @saleschannelvlocid) group by s.StyleID,s.SizeID,s.mrp " +
") t " +
"inner join ItemStyle i on i.StyleID = t.StyleID " +
"inner join Size sz on sz.SizeID = t.SizeID " +
"inner join ItemCategory c on c.ItemCategoryID = i.ItemCatID " +
" left join Category2 cat2 on cat2.Cat2ID = i.Cat2ID" +
" left join Category3 cat3 on cat3.Cat3ID = i.Cat3ID" +
" left join Column1 col on col.Col1ID = i.Col1 " +
            " left join Column2 col2 on col2.Col2ID = i.Col2 " +
            " left join Column3 col3 on col3.Col3ID = i.Col3 " +
            " left join Column4 col4 on col4.Col4ID = i.Col4 " +
            " left join Column5 col5 on col5.Col5ID = i.Col5 " +
            " left join Column6 col6 on col6.Col6ID = i.Col6 " +
            " left join Column7 col7 on col7.Col7ID = i.Col7 " +
            " left join Column8 col8 on col8.Col8ID = i.Col8 " +
            " left join Column9 col9 on col9.Col9ID = i.Col9 " +
            " left join Column10 col10 on col10.Col10ID = i.Col10 " +
            " left join Column11 col11 on col11.Col11ID = i.Col11 " +
            " left join Column12 col12 on col12.Col12ID = i.Col12 " +
            " left join Column13 col13 on col13.Col13ID = i.Col13 ";
            command.Parameters.AddWithValue("@saleschannelvlocid", virtualLoc);
            locTable.Load(command.ExecuteReader());

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
        return locTable;
    }

    public DataTable getSKUInventory()
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
        transaction = connection.BeginTransaction("SKUInv");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {

            //command.CommandText = "select si.StyleID,s.StyleCode,s.Title,s.mrp,ic.ItemCategory,sz.Size1,concat(s.StyleCode,'/',sz.Size1) as sku,col.C1Name,isnull(count(si.StockupID),0) as qnty from StockUpInward si inner join ItemStyle s on s.StyleID=si.StyleID inner join ItemCategory ic on ic.ItemCategoryID=s.ItemCatID inner join Size sz on si.SizeID=sz.SizeID inner join Column1 col on col.Col1ID=s.Col1 where si.status IN('RFL') group by si.StyleID,s.StyleCode,s.Title,s.mrp,ic.ItemCategory,sz.Size1,col.C1Name";

            //optimise query
            command.CommandText = "SELECT i.Title,concat(i.StyleCode,'/',sz.Size1) as sku,col.C1Name,t.qnty,c.ItemCategory,t.mrp "+
"FROM( "+
"select isnull(count(s.StockupID), 0) as qnty, s.StyleID, s.SizeID,s.mrp from StockUpInward s where s.Status = 'RFL' group by s.StyleID, s.SizeID,s.mrp " +
") t "+
"inner join ItemStyle i on i.StyleID = t.StyleID "+
"inner join Size sz on sz.SizeID = t.SizeID "+
"inner join ItemCategory c on c.ItemCategoryID = i.ItemCatID "+
"left join Column1 col on col.Col1ID = i.Col1 ";
            locTable.Load(command.ExecuteReader());

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
        return locTable;
    }

    public DataTable getDispatched()
    {
        // dispatched and not marked as return...
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
        transaction = connection.BeginTransaction("SKUInv");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {

            //command.CommandText = "select si.StyleID,s.StyleCode,s.Title,s.mrp,ic.ItemCategory,sz.Size1,concat(s.StyleCode,'/',sz.Size1) as sku,col.C1Name,isnull(count(si.StockupID),0) as qnty from StockUpInward si inner join ItemStyle s on s.StyleID=si.StyleID inner join ItemCategory ic on ic.ItemCategoryID=s.ItemCatID inner join Size sz on si.SizeID=sz.SizeID inner join Column1 col on col.Col1ID=s.Col1 where si.status IN('RFL') group by si.StyleID,s.StyleCode,s.Title,s.mrp,ic.ItemCategory,sz.Size1,col.C1Name";

            //optimise query
            command.CommandText = "select s.BarcodeNo,s.SystemDate,s.Status,i.StyleCode,concat(i.StyleCode,'-',sz.Size1) as SKU,s.mrp,l.BagDescription as LotNo,col1.C1Name as Brand,i.Title,sz.Size1 as Size,c.ItemCategory,cat2.C2Name as Cat02,cat3.C3Name as Cat03,cat4.C4Name as Cat04,cat5.C5Name as Cat05,s.RackBarcode,s.RackDate,s.printed,i.Control1 as Colour,i.Control2 as FSN_No,i.Control3 as Article_No,i.Control4 as Search_Image_URL,i.Control5 as EAN,i.Control6 as Model_Name,i.Control7 as Description,i.Control8 as Comments,col2.C2Name Colour, col3.C3Name Gender, col4.C4Name Upper_Material, col5.C5Name Sole_Material,  col6.C6Name Closure, col7.C7Name Occasion,   col8.C8Name Pattern, col9.C9Name Ankle_Height, col10.C10Name Toe_Shape,     col11.C11Name Heel_Shape, col12.C12Name Heel_Height, col13.C13Name Back_Closure       , sales.salesidgivenbyvloc,sales.sellingprice,sales.salesDateTime,sales.salesAbwno,sales.dispatchtimestamp,sales.returntimestamp,sales.returnAbwno from ArchiveStockUpInward s inner join ItemStyle i on i.StyleID = s.StyleID inner join Size sz on sz.SizeID = s.SizeID left join Lot l on l.BagId = s.BagID left join ItemCategory c on c.ItemCategoryID = i.ItemCatID left join Category2 cat2 on cat2.Cat2ID = i.Cat2ID left join Category3 cat3 on cat3.Cat3ID = i.Cat3ID left join Category4 cat4 on cat4.Cat4ID = i.Cat4ID left join Category5 cat5 on cat5.Cat5ID = i.Cat5ID left join Column1 col1 on col1.Col1ID = i.Col1 left join Column2 col2 on col2.Col2ID = i.Col2 left join Column3 col3 on col3.Col3ID = i.Col3 left join Column4 col4 on col4.Col4ID = i.Col4 left join Column5 col5 on col5.Col5ID = i.Col5 left join Column6 col6 on col6.Col6ID = i.Col6 left join Column7 col7 on col7.Col7ID = i.Col7 left join Column8 col8 on col8.Col8ID = i.Col8 left join Column9 col9 on col9.Col9ID = i.Col9 left join Column10 col10 on col10.Col10ID = i.Col10 left join Column11 col11 on col11.Col11ID = i.Col11 left join Column12 col12 on col12.Col12ID = i.Col12 left join Column13 col13 on col13.Col13ID = i.Col13 left join salesrecord sales on sales.archiveid = s.ArchiveStockupID and sales.status = 'DISPATCHED'";
            locTable.Load(command.ExecuteReader());

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
        return locTable;
    }

    public DataTable getDump(string Barcodeno)
    {
        // return and available in stock (see salesrecord condition)
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
        transaction = connection.BeginTransaction("getDump");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            string barcodeCond = string.Empty;
            if(!Barcodeno.Equals(""))
            {
                barcodeCond = " and BarcodeNo = '"+ Barcodeno+"'";
            }
            //get control select
            DataTable ColTableSetting = new DataTable();
            command.CommandText = "select * from ColTableSetting";
            ColTableSetting.Load(command.ExecuteReader());

            //get datafiled select
            DataTable StyleColumnSettings = new DataTable();
            command.CommandText = "select * from StyleColumnSettings";
            StyleColumnSettings.Load(command.ExecuteReader());

            string select = "select s.BarcodeNo,s.SystemDate,s.Status,i.StyleCode,concat(i.StyleCode,'-',sz.Size1) as SKU,s.mrp, s.purchaseRate,l.BagDescription as LotNo";

            string joins = " from (select * from StockUpInward where Status!='SOLD'"+ barcodeCond + ") s inner join ItemStyle i on i.StyleID = s.StyleID inner join Size sz on sz.SizeID=s.SizeID  left join salesrecord sale on sale.sid = (SELECT top 1 sid FROM salesrecord where status='RETURN' and itemid = s.StockupID order by sid desc) left join invoice inv on inv.invid=sale.invoiceid left join Location loc on loc.LocationID=sale.saleschannelvlocid " +
                "left join login saleLogin on sale.salesUserId = saleLogin.userid " +
                "left join login disLogin on sale.dispatchuserid = disLogin.userid " +
                "left join login retLogin on sale.returnuserid = retLogin.userid left join Lot l on l.BagId = s.BagID left join login lo on lo.userid=s.UserID left join Location phy on phy.LocationID = s.physicalID" +
" left join ItemCategory c on c.ItemCategoryID = i.ItemCatID" +
" left join Category2 cat2 on cat2.Cat2ID = i.Cat2ID" +
" left join Category3 cat3 on cat3.Cat3ID = i.Cat3ID" +
" left join Category4 cat4 on cat4.Cat4ID = i.Cat4ID" +
" left join Category5 cat5 on cat5.Cat5ID = i.Cat5ID " +
"left join EAN e on e.styleid = s.StyleID and e.sizeid = s.SizeID " +
"left join stateCode stc on stc.stateid=inv.state ";
            if (ColTableSetting.Rows[0]["IsAssigned"].ToString().Equals("True"))
            {
                select += ",col1.C1Name as Brand";
                joins += " left join Column1 col1 on col1.Col1ID = i.Col1";
            }

            select += ",i.Title,sz.Size1 as Size,c.ItemCategory,cat2.C2Name as Cat02,cat3.C3Name as Cat03,cat4.C4Name as Cat04,cat5.C5Name as Cat05,s.RackBarcode,s.RackDate,s.printed";
            int j = 1;
            for(int i=0;i<20;i++)
            {                
                if (StyleColumnSettings.Rows[i]["IsAssigned"].ToString().Equals("True"))
                {                    
                    select += ",i.Control"+j+" as " + StyleColumnSettings.Rows[i]["SettingName"].ToString().Replace(' ', '_');
                }
                j++;
            }

            // starting from 2 because brand was one and needed on first position as per client requirement
            int t = 2;
            for (int i = 1; i < 20; i++)
            {
                if (ColTableSetting.Rows[i]["IsAssigned"].ToString().Equals("True"))
                {
                    select += ",col"+t+".C"+t+ "Name " + ColTableSetting.Rows[i]["SettingName"].ToString().Replace(' ', '_');
                    joins += " left join Column"+t+ " col" + t + " on col" + t + ".Col" + t + "ID = i.Col" + t + "";
                }
                t++;
            }

            select += ",case when i.image1 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image1) else '' end as image1" +
                ",case when i.image2 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image2) else '' end as image2," +
                "case when i.image3 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image3) else '' end as image3," +
                "case when i.image4 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image4) else '' end as image4," +
                "case when i.image5 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image5) else '' end as image5," +
                "case when i.image6 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image6) else '' end as image6,lo.username as Barcode_Maker";
            select += ",sale.salesidgivenbyvloc,loc.Location as SoldFrom,sale.sellingprice,sale.status as salesStaus,inv.salesDate as ChannelSalesDate,inv.invType,sale.salesAbwno,sale.salesDateTime,saleLogin.username as SoldBy,sale.dispatchtimestamp,disLogin.username as Dispatchedby,sale.returntimestamp,retLogin.username as Returnby,sale.returnAbwno,(select STRING_AGG(concat(lo.Location,'-',listidgivenbyvloc,':',listprice),CHAR(13)+CHAR(10)) from (select l1.listidgivenbyvloc,l1.listprice,l1.saleschannelvlocid  from listchannelrecord  l1 where l1.styleId=s.StyleID and l1.sizeId=s.SizeID ) l inner join location lo on l.saleschannelvlocid=lo.locationID ) listDets,phy.Location as Physical_Location,DATEDIFF ( d, s.SystemDate , getDate() ) as stockAge,DATEDIFF ( d, l.invoiceDate , getDate() ) as lotAge,e.EAN,inv.invid as invoice_number,inv.custname as customer_name,inv.city,inv.paymentMode,stc.statename,convert(varchar, s.SystemDate, 4) as System_Date,convert(varchar, sale.salesDateTime, 4) as Sales_Date,convert(varchar, sale.dispatchtimestamp, 4) as Dispatch_Date,convert(varchar,sale.returntimestamp, 4) as Return_Date ";
       
            command.CommandText = select + joins;
            locTable.Load(command.ExecuteReader());

            /*command.CommandText = "select i.Title,c.ItemCategory,s.BarcodeNo,sz.Size1,sz.SizeID,s.SizeID as stockSize,cat2.C2Name as Cat02,cat3.C3Name as Cat03,cat4.C4Name as Cat04,cat5.C5Name as Cat05,l.BagDescription as LotNo,s.StockupID,s.Status,s.SystemDate,s.RackBarcode,s.RackDate,s.mrp,s.printed," +
                "i.Control1 as Colour,i.Control2 as FSN_No,i.Control3 as Article_No,i.Control4 as Search_Image_URL,i.Control5 as EAN," +
                "i.Control6 as Model_Name,i.Control7 as Description,i.Control8 as Comments,i.Control9,i.Control10,i.Control11,i.Control12,i.Control13,i.Control14,i.Control15,i.Control16,i.Control17,i.Control18,i.Control19,i.Control20," +
                "col1.C1Name as Brand,col2.C2Name as Colour,col3.C3Name as Gender,col4.C4Name as Upper_Material,col5.C5Name as Sole_Material,col6.C6Name as Closure,col7.C7Name as Occasion,col8.C8Name as Pattern,col9.C9Name as Ankle_Height,col10.C10Name as Toe_Shape," +
                "case when i.image1 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image1) else '' end as image1" +
                ",case when i.image2 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image2) else '' end as image2," +
                "case when i.image3 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image3) else '' end as image3," +
                "case when i.image4 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image4) else '' end as image4," +
                "case when i.image5 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image5) else '' end as image5," +
                "case when i.image6 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image6) else '' end as image6" +
 " from" +
" StockUpInward s inner" +
 " join ItemStyle i on i.StyleID = s.StyleID inner join Size sz on sz.SizeID=s.SizeID" +
" left" +
 " join Lot l on l.BagId = s.BagID" +
" left join ItemCategory c on c.ItemCategoryID = i.ItemCatID" +
" left join Category2 cat2 on cat2.Cat2ID = i.Cat2ID" +
" left join Category3 cat3 on cat3.Cat3ID = i.Cat3ID" +
" left join Category4 cat4 on cat4.Cat4ID = i.Cat4ID" +
" left join Category5 cat5 on cat5.Cat5ID = i.Cat5ID" +
" left join Column1 col1 on col1.Col1ID = i.Col1" +
" left join Column2 col2 on col2.Col2ID = i.Col2" +
" left join Column3 col3 on col3.Col3ID = i.Col3" +
" left join Column4 col4 on col4.Col4ID = i.Col4" +
" left join Column5 col5 on col5.Col5ID = i.Col5" +
" left join Column6 col6 on col6.Col6ID = i.Col6" +
" left join Column7 col7 on col7.Col7ID = i.Col7" +
" left join Column8 col8 on col8.Col8ID = i.Col8" +
" left join Column9 col9 on col9.Col9ID = i.Col9" +
" left join Column10 col10 on col10.Col10ID = i.Col10";*/
            //locTable.Load(command.ExecuteReader());

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
        return locTable;
    }

    public DataTable getSoldDump(string Barcodeno)
    {
        // sold but not dispatched...
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
        transaction = connection.BeginTransaction("soldDump");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            string barcodeCond = string.Empty;
            if (!Barcodeno.Equals(""))
            {
                barcodeCond = " and BarcodeNo = '" + Barcodeno + "'";
            }

            //get control select
            DataTable ColTableSetting = new DataTable();
            command.CommandText = "select * from ColTableSetting";
            ColTableSetting.Load(command.ExecuteReader());

            //get datafiled select
            DataTable StyleColumnSettings = new DataTable();
            command.CommandText = "select * from StyleColumnSettings";
            StyleColumnSettings.Load(command.ExecuteReader());

            string select = "select s.BarcodeNo,s.SystemDate,s.Status,i.StyleCode,concat(i.StyleCode,'-',sz.Size1) as SKU,s.mrp, s.purchaseRate,l.BagDescription as LotNo";

            string joins = " from (select * from StockUpInward where Status='SOLD'"+ barcodeCond + ") s inner join ItemStyle i on i.StyleID = s.StyleID inner join Size sz on sz.SizeID=s.SizeID left join salesrecord sale on sale.itemid=s.StockupID and sale.status = 'ND' left join invoice inv on inv.invid=sale.invoiceid" +
                " left join Location loc on loc.LocationID=sale.saleschannelvlocid " +
                "left join login saleLogin on sale.salesUserId = saleLogin.userid " +
                "left join login disLogin on sale.dispatchuserid = disLogin.userid " +
                "left join login retLogin on sale.returnuserid = retLogin.userid left join Lot l on l.BagId = s.BagID left join login lo on lo.userid=s.UserID left join Location phy on phy.LocationID = s.physicalID" +
" left join ItemCategory c on c.ItemCategoryID = i.ItemCatID" +
" left join Category2 cat2 on cat2.Cat2ID = i.Cat2ID" +
" left join Category3 cat3 on cat3.Cat3ID = i.Cat3ID" +
" left join Category4 cat4 on cat4.Cat4ID = i.Cat4ID" +
" left join Category5 cat5 on cat5.Cat5ID = i.Cat5ID "+
"left join EAN e on e.styleid = s.StyleID and e.sizeid = s.SizeID " +
"left join stateCode stc on stc.stateid=inv.state ";
            if (ColTableSetting.Rows[0]["IsAssigned"].ToString().Equals("True"))
            {
                select += ",col1.C1Name as Brand";
                joins += " left join Column1 col1 on col1.Col1ID = i.Col1";
            }

            select += ",i.Title,sz.Size1 as Size,c.ItemCategory,cat2.C2Name as Cat02,cat3.C3Name as Cat03,cat4.C4Name as Cat04,cat5.C5Name as Cat05,s.RackBarcode,s.RackDate,s.printed";
            int j = 1;
            for (int i = 0; i < 20; i++)
            {
                if (StyleColumnSettings.Rows[i]["IsAssigned"].ToString().Equals("True"))
                {
                    select += ",i.Control" + j + " as " + StyleColumnSettings.Rows[i]["SettingName"].ToString().Replace(' ', '_');
                }
                j++;
            }

            // starting from 2 because brand was one and needed on first position as per client requirement
            int t = 2;
            for (int i = 1; i < 20; i++)
            {
                if (ColTableSetting.Rows[i]["IsAssigned"].ToString().Equals("True"))
                {
                    select += ",col" + t + ".C" + t + "Name " + ColTableSetting.Rows[i]["SettingName"].ToString().Replace(' ', '_');
                    joins += " left join Column" + t + " col" + t + " on col" + t + ".Col" + t + "ID = i.Col" + t + "";
                }
                t++;
            }

            select += ",case when i.image1 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image1) else '' end as image1" +
                ",case when i.image2 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image2) else '' end as image2," +
                "case when i.image3 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image3) else '' end as image3," +
                "case when i.image4 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image4) else '' end as image4," +
                "case when i.image5 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image5) else '' end as image5," +
                "case when i.image6 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image6) else '' end as image6,lo.username as Barcode_Maker";
            select += ",sale.salesidgivenbyvloc,loc.Location as SoldFrom,sale.sellingprice,sale.status as salesStaus,inv.salesDate as ChannelSalesDate,inv.invType,sale.salesAbwno,sale.salesDateTime,saleLogin.username as SoldBy,sale.dispatchtimestamp,disLogin.username as Dispatchedby,sale.returntimestamp,retLogin.username as Returnby,sale.returnAbwno,(select STRING_AGG(concat(lo.Location,'-',listidgivenbyvloc,':',listprice),CHAR(13)+CHAR(10)) from (select l1.listidgivenbyvloc,l1.listprice,l1.saleschannelvlocid  from listchannelrecord  l1 where l1.styleId=s.StyleID and l1.sizeId=s.SizeID ) l inner join location lo on l.saleschannelvlocid=lo.locationID ) listDets,phy.Location as Physical_Location,DATEDIFF ( d, s.SystemDate , getDate() ) as stockAge,DATEDIFF ( d, l.invoiceDate , getDate() ) as lotAge,e.EAN,inv.invid as invoice_number,inv.custname as customer_name,inv.city,inv.paymentMode,stc.statename,convert(varchar, s.SystemDate, 4) as System_Date,convert(varchar, sale.salesDateTime, 4) as Sales_Date,convert(varchar, sale.dispatchtimestamp, 4) as Dispatch_Date,convert(varchar,sale.returntimestamp, 4) as Return_Date ";

            command.CommandText = select + joins;
            locTable.Load(command.ExecuteReader());

            /*command.CommandText = "select i.Title,c.ItemCategory,s.BarcodeNo,sz.Size1,sz.SizeID,s.SizeID as stockSize,cat2.C2Name as Cat02,cat3.C3Name as Cat03,cat4.C4Name as Cat04,cat5.C5Name as Cat05,l.BagDescription as LotNo,s.StockupID,s.Status,s.SystemDate,s.RackBarcode,s.RackDate,s.mrp,s.printed," +
                "i.Control1 as Colour,i.Control2 as FSN_No,i.Control3 as Article_No,i.Control4 as Search_Image_URL,i.Control5 as EAN," +
                "i.Control6 as Model_Name,i.Control7 as Description,i.Control8 as Comments,i.Control9,i.Control10,i.Control11,i.Control12,i.Control13,i.Control14,i.Control15,i.Control16,i.Control17,i.Control18,i.Control19,i.Control20," +
                "col1.C1Name as Brand,col2.C2Name as Colour,col3.C3Name as Gender,col4.C4Name as Upper_Material,col5.C5Name as Sole_Material,col6.C6Name as Closure,col7.C7Name as Occasion,col8.C8Name as Pattern,col9.C9Name as Ankle_Height,col10.C10Name as Toe_Shape," +
                "case when i.image1 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image1) else '' end as image1" +
                ",case when i.image2 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image2) else '' end as image2," +
                "case when i.image3 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image3) else '' end as image3," +
                "case when i.image4 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image4) else '' end as image4," +
                "case when i.image5 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image5) else '' end as image5," +
                "case when i.image6 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image6) else '' end as image6" +
 " from" +
" StockUpInward s inner" +
 " join ItemStyle i on i.StyleID = s.StyleID inner join Size sz on sz.SizeID=s.SizeID" +
" left" +
 " join Lot l on l.BagId = s.BagID" +
" left join ItemCategory c on c.ItemCategoryID = i.ItemCatID" +
" left join Category2 cat2 on cat2.Cat2ID = i.Cat2ID" +
" left join Category3 cat3 on cat3.Cat3ID = i.Cat3ID" +
" left join Category4 cat4 on cat4.Cat4ID = i.Cat4ID" +
" left join Category5 cat5 on cat5.Cat5ID = i.Cat5ID" +
" left join Column1 col1 on col1.Col1ID = i.Col1" +
" left join Column2 col2 on col2.Col2ID = i.Col2" +
" left join Column3 col3 on col3.Col3ID = i.Col3" +
" left join Column4 col4 on col4.Col4ID = i.Col4" +
" left join Column5 col5 on col5.Col5ID = i.Col5" +
" left join Column6 col6 on col6.Col6ID = i.Col6" +
" left join Column7 col7 on col7.Col7ID = i.Col7" +
" left join Column8 col8 on col8.Col8ID = i.Col8" +
" left join Column9 col9 on col9.Col9ID = i.Col9" +
" left join Column10 col10 on col10.Col10ID = i.Col10";*/
            //locTable.Load(command.ExecuteReader());

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
        return locTable;
    }

    public DataTable getSalesDump()
    {
        // not dispatched data.... but status ND or return both will come...
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
        transaction = connection.BeginTransaction("salesDump");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            //get control select
            DataTable ColTableSetting = new DataTable();
            command.CommandText = "select * from ColTableSetting";
            ColTableSetting.Load(command.ExecuteReader());

            //get datafiled select
            DataTable StyleColumnSettings = new DataTable();
            command.CommandText = "select * from StyleColumnSettings";
            StyleColumnSettings.Load(command.ExecuteReader());

            string select = "select s.BarcodeNo,s.SystemDate,s.Status,i.StyleCode,concat(i.StyleCode,'-',sz.Size1) as SKU,s.mrp,l.BagDescription as LotNo";

            string joins = " from (select * from salesrecord where itemid != -1) sale inner join StockUpInward s on s.StockupID=sale.itemid inner join ItemStyle i on i.StyleID = s.StyleID inner join Size sz on sz.SizeID=s.SizeID inner join Location loc on loc.LocationID=sale.saleschannelvlocid " +
                "left join invoice inv on inv.invid=sale.invoiceid " +
                "left join login saleLogin on sale.salesUserId = saleLogin.userid " +
                "left join login disLogin on sale.dispatchuserid = disLogin.userid " +
                "left join login retLogin on sale.returnuserid = retLogin.userid left join Lot l on l.BagId = s.BagID left join login lo on lo.userid=s.UserID left join Location phy on phy.LocationID = s.physicalID" +
" left join ItemCategory c on c.ItemCategoryID = i.ItemCatID" +
" left join Category2 cat2 on cat2.Cat2ID = i.Cat2ID" +
" left join Category3 cat3 on cat3.Cat3ID = i.Cat3ID" +
" left join Category4 cat4 on cat4.Cat4ID = i.Cat4ID" +
" left join Category5 cat5 on cat5.Cat5ID = i.Cat5ID "+
"left join EAN e on e.styleid = s.StyleID and e.sizeid = s.SizeID " +
"left join stateCode stc on stc.stateid=inv.state ";
            if (ColTableSetting.Rows[0]["IsAssigned"].ToString().Equals("True"))
            {
                select += ",col1.C1Name as Brand";
                joins += " left join Column1 col1 on col1.Col1ID = i.Col1";
            }

            select += ",i.Title,sz.Size1 as Size,c.ItemCategory,cat2.C2Name as Cat02,cat3.C3Name as Cat03,cat4.C4Name as Cat04,cat5.C5Name as Cat05,s.RackBarcode,s.RackDate,s.printed";
            int j = 1;
            for (int i = 0; i < 20; i++)
            {
                if (StyleColumnSettings.Rows[i]["IsAssigned"].ToString().Equals("True"))
                {
                    select += ",i.Control" + j + " as " + StyleColumnSettings.Rows[i]["SettingName"].ToString().Replace(' ', '_');
                }
                j++;
            }

            // starting from 2 because brand was one and needed on first position as per client requirement
            int t = 2;
            for (int i = 1; i < 20; i++)
            {
                if (ColTableSetting.Rows[i]["IsAssigned"].ToString().Equals("True"))
                {
                    select += ",col" + t + ".C" + t + "Name " + ColTableSetting.Rows[i]["SettingName"].ToString().Replace(' ', '_');
                    joins += " left join Column" + t + " col" + t + " on col" + t + ".Col" + t + "ID = i.Col" + t + "";
                }
                t++;
            }

            select += ",case when i.image1 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image1) else '' end as image1" +
                ",case when i.image2 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image2) else '' end as image2," +
                "case when i.image3 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image3) else '' end as image3," +
                "case when i.image4 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image4) else '' end as image4," +
                "case when i.image5 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image5) else '' end as image5," +
                "case when i.image6 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image6) else '' end as image6,lo.username as Barcode_Maker";

            select += ",sale.salesidgivenbyvloc,loc.Location as SoldFrom,sale.sellingprice,sale.status as salesStaus,inv.salesDate as ChannelSalesDate,inv.invType,sale.salesAbwno,sale.salesDateTime,saleLogin.username as SoldBy,sale.dispatchtimestamp,disLogin.username as Dispatchedby,sale.returntimestamp,retLogin.username as Returnby,sale.returnAbwno,(select STRING_AGG(concat(lo.Location,'-',listidgivenbyvloc,':',listprice),CHAR(13)+CHAR(10)) from (select l1.listidgivenbyvloc,l1.listprice,l1.saleschannelvlocid  from listchannelrecord  l1 where l1.styleId=s.StyleID and l1.sizeId=s.SizeID ) l inner join location lo on l.saleschannelvlocid=lo.locationID ) listDets,phy.Location as Physical_Location,DATEDIFF ( d, s.SystemDate , getDate() ) as stockAge,DATEDIFF ( d, l.invoiceDate , getDate() ) as lotAge,e.EAN,inv.invid as invoice_number,inv.custname as customer_name,inv.city,inv.paymentMode,stc.statename,convert(varchar, s.SystemDate, 4) as System_Date,convert(varchar, sale.salesDateTime, 4) as Sales_Date,convert(varchar, sale.dispatchtimestamp, 4) as Dispatch_Date,convert(varchar,sale.returntimestamp, 4) as Return_Date";
            command.CommandText = select + joins;
            locTable.Load(command.ExecuteReader());

            
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
        return locTable;
    }

    public DataTable getSalesDumpFromArchive()
    {
        // marked return and again dispatched...
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
        transaction = connection.BeginTransaction("salesDump");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            //get control select
            DataTable ColTableSetting = new DataTable();
            command.CommandText = "select * from ColTableSetting";
            ColTableSetting.Load(command.ExecuteReader());

            //get datafiled select
            DataTable StyleColumnSettings = new DataTable();
            command.CommandText = "select * from StyleColumnSettings";
            StyleColumnSettings.Load(command.ExecuteReader());

            string select = "select s.BarcodeNo,s.SystemDate,s.Status,i.StyleCode,concat(i.StyleCode,'-',sz.Size1) as SKU,s.mrp,l.BagDescription as LotNo";

            string joins = " from (select * from salesrecord where itemid != -1) sale inner join ArchiveStockUpInward s on s.StockupID=sale.itemid inner join ItemStyle i on i.StyleID = s.StyleID inner join Size sz on sz.SizeID=s.SizeID inner join Location loc on loc.LocationID=sale.saleschannelvlocid " +
                "left join invoice inv on inv.invid=sale.invoiceid " +
                "left join login saleLogin on sale.salesUserId = saleLogin.userid " +
                "left join login disLogin on sale.dispatchuserid = disLogin.userid " +
                "left join login retLogin on sale.returnuserid = retLogin.userid left join Lot l on l.BagId = s.BagID left join login lo on lo.userid=s.UserID left join Location phy on phy.LocationID = s.physicalID" +
" left join ItemCategory c on c.ItemCategoryID = i.ItemCatID" +
" left join Category2 cat2 on cat2.Cat2ID = i.Cat2ID" +
" left join Category3 cat3 on cat3.Cat3ID = i.Cat3ID" +
" left join Category4 cat4 on cat4.Cat4ID = i.Cat4ID" +
" left join Category5 cat5 on cat5.Cat5ID = i.Cat5ID "+
"left join EAN e on e.styleid = s.StyleID and e.sizeid = s.SizeID " +
"left join stateCode stc on stc.stateid=inv.state ";
            if (ColTableSetting.Rows[0]["IsAssigned"].ToString().Equals("True"))
            {
                select += ",col1.C1Name as Brand";
                joins += " left join Column1 col1 on col1.Col1ID = i.Col1";
            }

            select += ",i.Title,sz.Size1 as Size,c.ItemCategory,cat2.C2Name as Cat02,cat3.C3Name as Cat03,cat4.C4Name as Cat04,cat5.C5Name as Cat05,s.RackBarcode,s.RackDate,s.printed";
            int j = 1;
            for (int i = 0; i < 20; i++)
            {
                if (StyleColumnSettings.Rows[i]["IsAssigned"].ToString().Equals("True"))
                {
                    select += ",i.Control" + j + " as " + StyleColumnSettings.Rows[i]["SettingName"].ToString().Replace(' ', '_');
                }
                j++;
            }

            // starting from 2 because brand was one and needed on first position as per client requirement
            int t = 2;
            for (int i = 1; i < 20; i++)
            {
                if (ColTableSetting.Rows[i]["IsAssigned"].ToString().Equals("True"))
                {
                    select += ",col" + t + ".C" + t + "Name " + ColTableSetting.Rows[i]["SettingName"].ToString().Replace(' ', '_');
                    joins += " left join Column" + t + " col" + t + " on col" + t + ".Col" + t + "ID = i.Col" + t + "";
                }
                t++;
            }

            select += ",case when i.image1 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image1) else '' end as image1" +
                ",case when i.image2 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image2) else '' end as image2," +
                "case when i.image3 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image3) else '' end as image3," +
                "case when i.image4 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image4) else '' end as image4," +
                "case when i.image5 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image5) else '' end as image5," +
                "case when i.image6 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image6) else '' end as image6,lo.username as Barcode_Maker";

            select += ",sale.salesidgivenbyvloc,loc.Location as SoldFrom,sale.sellingprice,sale.status as salesStaus,inv.salesDate as ChannelSalesDate,inv.invType,sale.salesAbwno,sale.salesDateTime,saleLogin.username as SoldBy,sale.dispatchtimestamp,disLogin.username as Dispatchedby,sale.returntimestamp,retLogin.username as Returnby,sale.returnAbwno,(select STRING_AGG(concat(lo.Location,'-',listidgivenbyvloc,':',listprice),CHAR(13)+CHAR(10)) from (select l1.listidgivenbyvloc,l1.listprice,l1.saleschannelvlocid  from listchannelrecord  l1 where l1.styleId=s.StyleID and l1.sizeId=s.SizeID ) l inner join location lo on l.saleschannelvlocid=lo.locationID ) listDets,phy.Location as Physical_Location,DATEDIFF ( d, s.SystemDate , getDate() ) as stockAge,DATEDIFF ( d, l.invoiceDate , getDate() ) as lotAge,e.EAN,inv.invid as invoice_number,inv.custname as customer_name,inv.city,inv.paymentMode,stc.statename,convert(varchar, s.SystemDate, 4) as System_Date,convert(varchar, sale.salesDateTime, 4) as Sales_Date,convert(varchar, sale.dispatchtimestamp, 4) as Dispatch_Date,convert(varchar,sale.returntimestamp, 4) as Return_Date ";
            command.CommandText = select + joins;
            locTable.Load(command.ExecuteReader());


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
        return locTable;
    }

    public DataTable getSalesDumpArchive(string Barcodeno)
    {
        // all dispatched data
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
        transaction = connection.BeginTransaction("salesDump");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            
            string barcodeCond = string.Empty;
            if (!Barcodeno.Equals(""))
            {
                command.CommandText = "select * from ArchiveStockUpInward where BarcodeNo=@barcodeNo";
                command.Parameters.AddWithValue("@barcodeNo", Barcodeno);
                DataTable newDT = new DataTable();
                newDT.Load(command.ExecuteReader());
                barcodeCond = " and archiveid = " + newDT.Rows[0]["ArchiveStockupID"].ToString();
            }
            //get control select
            DataTable ColTableSetting = new DataTable();
            command.CommandText = "select * from ColTableSetting";
            ColTableSetting.Load(command.ExecuteReader());

            //get datafiled select
            DataTable StyleColumnSettings = new DataTable();
            command.CommandText = "select * from StyleColumnSettings";
            StyleColumnSettings.Load(command.ExecuteReader());

            string select = "select s.BarcodeNo,s.SystemDate,s.Status,i.StyleCode,concat(i.StyleCode,'-',sz.Size1) as SKU,s.mrp,s.purchaseRate,l.BagDescription as LotNo";

            string joins = " from (select * from salesrecord where archiveid != -1"+ barcodeCond + ") sale inner join ArchiveStockUpInward s on s.ArchiveStockupID=sale.archiveid inner join ItemStyle i on i.StyleID = s.StyleID inner join Size sz on sz.SizeID=s.SizeID  inner join Location loc on loc.LocationID=sale.saleschannelvlocid " +
                "left join invoice inv on inv.invid=sale.invoiceid " +
                "left join login saleLogin on sale.salesUserId = saleLogin.userid " +
                "left join login disLogin on sale.dispatchuserid = disLogin.userid " +
                "left join login retLogin on sale.returnuserid = retLogin.userid left join Lot l on l.BagId = s.BagID left join login lo on lo.userid=s.UserID left join Location phy on phy.LocationID = s.physicalID" +
" left join ItemCategory c on c.ItemCategoryID = i.ItemCatID" +
" left join Category2 cat2 on cat2.Cat2ID = i.Cat2ID" +
" left join Category3 cat3 on cat3.Cat3ID = i.Cat3ID" +
" left join Category4 cat4 on cat4.Cat4ID = i.Cat4ID" +
" left join Category5 cat5 on cat5.Cat5ID = i.Cat5ID "+
"left join EAN e on e.styleid = s.StyleID and e.sizeid = s.SizeID " +
"left join stateCode stc on stc.stateid=inv.state ";
            if (ColTableSetting.Rows[0]["IsAssigned"].ToString().Equals("True"))
            {
                select += ",col1.C1Name as Brand";
                joins += " left join Column1 col1 on col1.Col1ID = i.Col1";
            }

            select += ",i.Title,sz.Size1 as Size,c.ItemCategory,cat2.C2Name as Cat02,cat3.C3Name as Cat03,cat4.C4Name as Cat04,cat5.C5Name as Cat05,s.RackBarcode,s.RackDate,s.printed";
            int j = 1;
            for (int i = 0; i < 20; i++)
            {
                if (StyleColumnSettings.Rows[i]["IsAssigned"].ToString().Equals("True"))
                {
                    select += ",i.Control" + j + " as " + StyleColumnSettings.Rows[i]["SettingName"].ToString().Replace(' ', '_');
                }
                j++;
            }

            // starting from 2 because brand was one and needed on first position as per client requirement
            int t = 2;
            for (int i = 1; i < 20; i++)
            {
                if (ColTableSetting.Rows[i]["IsAssigned"].ToString().Equals("True"))
                {
                    select += ",col" + t + ".C" + t + "Name " + ColTableSetting.Rows[i]["SettingName"].ToString().Replace(' ', '_');
                    joins += " left join Column" + t + " col" + t + " on col" + t + ".Col" + t + "ID = i.Col" + t + "";
                }
                t++;
            }

            select += ",case when i.image1 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image1) else '' end as image1" +
                ",case when i.image2 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image2) else '' end as image2," +
                "case when i.image3 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image3) else '' end as image3," +
                "case when i.image4 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image4) else '' end as image4," +
                "case when i.image5 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image5) else '' end as image5," +
                "case when i.image6 != '' then concat('http://finetouchimages.dzvdesk.com/Uploads/',i.image6) else '' end as image6,lo.username as Barcode_Maker";

            select += ",sale.salesidgivenbyvloc,loc.Location as SoldFrom,sale.sellingprice,sale.status as salesStaus,inv.salesDate as ChannelSalesDate,inv.invType,sale.salesAbwno,sale.salesDateTime,saleLogin.username as SoldBy,sale.dispatchtimestamp,disLogin.username as Dispatchedby,sale.returntimestamp,retLogin.username as Returnby,sale.returnAbwno,(select STRING_AGG(concat(lo.Location,'-',listidgivenbyvloc,':',listprice),CHAR(13)+CHAR(10)) from (select l1.listidgivenbyvloc,l1.listprice,l1.saleschannelvlocid  from listchannelrecord  l1 where l1.styleId=s.StyleID and l1.sizeId=s.SizeID ) l inner join location lo on l.saleschannelvlocid=lo.locationID ) listDets,phy.Location as Physical_Location,DATEDIFF ( d, s.SystemDate , getDate() ) as stockAge,DATEDIFF ( d, l.invoiceDate , getDate() ) as lotAge,e.EAN,inv.invid as invoice_number,inv.custname as customer_name,inv.city,inv.paymentMode,stc.statename,convert(varchar, s.SystemDate, 4) as System_Date,convert(varchar, sale.salesDateTime, 4) as Sales_Date,convert(varchar, sale.dispatchtimestamp, 4) as Dispatch_Date,convert(varchar,sale.returntimestamp, 4) as Return_Date";
            command.CommandText = select + joins;
            locTable.Load(command.ExecuteReader());


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
        return locTable;
    }

    public DataTable getRackMaster()
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
        transaction = connection.BeginTransaction("RackMaster");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "SELECT isnull(count(r.RackNo),0) as racked,RackNo,r.RackLimitation,isnull(r.RackLimitation-count(r.RackNo),0) as spaceLeft from rackmaster r left join StockUpInward s on SUBSTRING(RackBarcode,3,1)=r.RackNo group by RackNo,r.RackLimitation ";
            locTable.Load(command.ExecuteReader());

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
        return locTable;
    }

    public DataTable getStocked(string userid,string dates)
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
        transaction = connection.BeginTransaction("RackMaster");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select BarcodeNo,s.SystemDate,s.RackDate,s.RackBarcode,col1.C1Name from StockUpInward s inner join ItemStyle i on i.StyleID=s.StyleID inner join Column1 col1 on col1.Col1ID=i.Col1 where CONVERT (date, s.SystemDate) = @dates and s.userid = @userid" +
                " union all"+
                " select BarcodeNo,sa.SystemDate,sa.RackDate,sa.RackBarcode,col1a.C1Name from ArchiveStockUpInward sa inner join ItemStyle ia on ia.StyleID=sa.StyleID inner join Column1 col1a on col1a.Col1ID=ia.Col1 where CONVERT (date, sa.SystemDate) = @dates1 and sa.userid = @userid1";
            command.Parameters.AddWithValue("@dates", Convert.ToDateTime(dates).ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@userid", userid);
            command.Parameters.AddWithValue("@dates1", Convert.ToDateTime(dates).ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@userid1", userid);
            locTable.Load(command.ExecuteReader());

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
        return locTable;
    }

    public DataTable getList(string userid, string dates)
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
        transaction = connection.BeginTransaction("RackMaster");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select concat(i.StyleCode,'-',s.Size1) as sku,l.recordtimestamp,l.listidgivenbyvloc,loc.Location from listchannelrecord l inner join Size s on s.SizeID=l.sizeId inner join ItemStyle i on i.StyleID=l.styleId inner join Location loc on loc.LocationID=l.saleschannelvlocid where CONVERT (date, recordtimestamp) = @dates and userId=@userid";
            command.Parameters.AddWithValue("@dates", Convert.ToDateTime(dates).ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@userid", userid);
            locTable.Load(command.ExecuteReader());

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
        return locTable;
    }

    public DataTable getSoldByUser(string userid, string dates)
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
        transaction = connection.BeginTransaction("RackMaster");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select si.BarcodeNo,s.salesDateTime from salesrecord s inner join StockUpInward si on s.itemid = si.StockupID where CONVERT (date, s.salesDateTime) = @dates and s.salesUserId=@userid" +
                " union all" +
                " select a.BarcodeNo,s1.salesDateTime from salesrecord s1 inner join ArchiveStockUpInward a on s1.archiveid = a.ArchiveStockupID where CONVERT(date, s1.salesDateTime) = @dates1 and s1.salesUserId=@userid1 " +
                " union all" +
                " select a5.BarcodeNo,s15.salesDateTime from salesrecord s15 inner join ArchiveStockUpInward a5 on s15.itemid = a5.StockupID where CONVERT(date, s15.salesDateTime) = @dates5 and s15.salesUserId=@userid5 " +
                " union all " +
                "select si1.BarcodeNo,c.salesDateTime from cancelTrans c inner join StockUpInward si1 on c.itemid = si1.StockupID where CONVERT (date, c.salesDateTime) = @dates3 and c.salesUserId=@userid3 " +
                "union all " +
                "select a1.BarcodeNo,c1.salesDateTime from cancelTrans c1 inner join ArchiveStockUpInward a1 on c1.archiveid = a1.ArchiveStockupID where CONVERT(date, c1.salesDateTime) = @dates4 and c1.salesUserId = @userid4"+
                " union all " +
                "select a16.BarcodeNo,c16.salesDateTime from cancelTrans c16 inner join ArchiveStockUpInward a16 on c16.itemid = a16.StockupID where CONVERT(date, c16.salesDateTime) = @dates6 and c16.salesUserId = @userid6";

            command.Parameters.AddWithValue("@dates", Convert.ToDateTime(dates).ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@userid", userid);
            command.Parameters.AddWithValue("@dates1", Convert.ToDateTime(dates).ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@userid1", userid);
            command.Parameters.AddWithValue("@dates3", Convert.ToDateTime(dates).ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@userid3", userid);
            command.Parameters.AddWithValue("@dates4", Convert.ToDateTime(dates).ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@userid4", userid);
            command.Parameters.AddWithValue("@dates5", Convert.ToDateTime(dates).ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@userid5", userid);
            command.Parameters.AddWithValue("@dates6", Convert.ToDateTime(dates).ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@userid6", userid);
            locTable.Load(command.ExecuteReader());

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
        return locTable;
    }

    public DataTable getDispatched(string userid, string dates)
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
        transaction = connection.BeginTransaction("RackMaster");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select a.BarcodeNo,s1.dispatchtimestamp from salesrecord s1 inner join ArchiveStockUpInward a on s1.archiveid = a.ArchiveStockupID where CONVERT (date, s1.dispatchtimestamp) = @dates and s1.dispatchuserid=@userid" +
                " union all" +
                " select aa.BarcodeNo,s1a.dispatchtimestamp from salesrecord s1a inner join StockUpInward aa on s1a.itemid = aa.StockupID where CONVERT(date, s1a.dispatchtimestamp) = @dates1 and s1a.dispatchuserid = @userid1" +
                " union all " +
                " select a3.BarcodeNo,s13.dispatchtimestamp from salesrecord s13 inner join ArchiveStockUpInward a3 on s13.itemid = a3.StockupID where CONVERT (date, s13.dispatchtimestamp) = @dates3 and s13.dispatchuserid=@userid3";
            command.Parameters.AddWithValue("@dates", Convert.ToDateTime(dates).ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@userid", userid);
            command.Parameters.AddWithValue("@dates1", Convert.ToDateTime(dates).ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@userid1", userid);
            command.Parameters.AddWithValue("@dates3", Convert.ToDateTime(dates).ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@userid3", userid);
            locTable.Load(command.ExecuteReader());

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
        return locTable;
    }

    public DataTable getReturned(string userid, string dates)
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
        transaction = connection.BeginTransaction("RackMaster");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select si.BarcodeNo,s.returntimestamp,si.RackBarcode,si.RackDate,loc.Location from salesrecord s inner join StockUpInward si on s.itemid = si.StockupID inner join Location loc on loc.LocationID=s.saleschannelvlocid where CONVERT (date, s.returntimestamp) = @dates and s.returnuserid=@userid" +
                " union all"+
                " select sia.BarcodeNo,sa.returntimestamp,sia.RackBarcode,sia.RackDate,loca.Location from salesrecord sa inner join ArchiveStockUpInward sia on sa.itemid = sia.StockupID inner join Location loca on loca.LocationID=sa.saleschannelvlocid where CONVERT (date, sa.returntimestamp) = @dates1 and sa.returnuserid=@userid1";
            command.Parameters.AddWithValue("@dates", Convert.ToDateTime(dates).ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@userid", userid);
            command.Parameters.AddWithValue("@dates1", Convert.ToDateTime(dates).ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@userid1", userid);
            locTable.Load(command.ExecuteReader());

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
        return locTable;
    }

    public DataTable getCancelled(string userid, string dates)
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
        transaction = connection.BeginTransaction("RackMaster");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select si1.BarcodeNo,c.cancelTimeStamp from cancelTrans c inner join StockUpInward si1 on c.itemid = si1.StockupID where CONVERT (date, c.cancelTimeStamp) = @dates3 and c.cancelId=@userid3 " +
                "union all " +
                "select a1.BarcodeNo,c1.cancelTimeStamp from cancelTrans c1 inner join ArchiveStockUpInward a1 on c1.itemid = a1.StockupID where CONVERT(date, c1.cancelTimeStamp) = @dates4 and c1.cancelId = @userid4";
            command.Parameters.AddWithValue("@dates3", Convert.ToDateTime(dates).ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@userid3", userid);
            command.Parameters.AddWithValue("@dates4", Convert.ToDateTime(dates).ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@userid4", userid);
            locTable.Load(command.ExecuteReader());

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
        return locTable;
    }

    public DataTable getStyle(string userid, string dates)
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
        transaction = connection.BeginTransaction("getStyle");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            //get username ... 
            command.CommandText = "select * from login where userid=@userid9";
            command.Parameters.AddWithValue("@userid9", userid);
            DataTable dt = new DataTable();
            dt.Load(command.ExecuteReader());
            
            command.CommandText = "select StyleCode,SystemDate from ItemStyle i where CONVERT (date, i.SystemDate) = @dates3 and i.LoginID=@userid3" +
                " union all"+
                " select StyleCode,'"+ Convert.ToDateTime(dates).ToString("yyyy-MM-dd") + "' as SystemDate from ItemStyle where Lower(logs) like '%," + dt.Rows[0]["username"].ToString().ToLower() + "#"+ Convert.ToDateTime(dates).ToString("M/dd/yyyy").Replace("-","/") + "%'";
            command.Parameters.AddWithValue("@dates3", Convert.ToDateTime(dates).ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@userid3", userid);
           
            locTable.Load(command.ExecuteReader());

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
        return locTable;
    }

    public DataSet barcodeStatusHistory(string BarcodeNo)
    {
        DataSet ds = new DataSet();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("barHis");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            //created by
            DataTable createdby = new DataTable();
            command.CommandText = "select s.SystemDate,l.username,initialStatus from StockUpInward s inner join login l on l.userid=s.UserID where BarcodeNo = @BarcodeNo1 " +
                "union all " +
                "select s1.SystemDate,l1.username,initialStatus from ArchiveStockUpInward s1 inner join login l1 on l1.userid = s1.UserID where BarcodeNo = @BarcodeNo2";
            command.Parameters.AddWithValue("@BarcodeNo1", BarcodeNo);
            command.Parameters.AddWithValue("@BarcodeNo2", BarcodeNo);
            createdby.Load(command.ExecuteReader());

            // from salesrecord
            DataTable locTable = new DataTable();
            command.CommandText = "select s.salesDateTime,l.username as salesUser,s.dispatchtimestamp,lD.username as dispatchUser,s.returntimestamp,lR.username as retUser,changeStatus from salesrecord s inner join StockUpInward si on si.StockupID=s.itemid left join login l on l.userid=s.salesUserId left join login lD on lD.userid=s.dispatchuserid left join login lR on lR.userid=s.returnuserid where BarcodeNo=@BarcodeNo3 " +
                "union all " +
                "select s1.salesDateTime,l1.username as salesUser,s1.dispatchtimestamp,lD1.username as dispatchUser,s1.returntimestamp,lR1.username as retUser,changeStatus from salesrecord s1 inner join ArchiveStockUpInward si1 on si1.StockupID = s1.itemid left join login l1 on l1.userid = s1.salesUserId left join login lD1 on lD1.userid = s1.dispatchuserid left join login lR1 on lR1.userid = s1.returnuserid where BarcodeNo = @BarcodeNo4 " +
                "union all " +
                "select s11.salesDateTime,l11.username as salesUser,s11.dispatchtimestamp,lD11.username as dispatchUser,s11.returntimestamp,lR11.username as retUser,changeStatus from salesrecord s11 inner join ArchiveStockUpInward si11 on si11.ArchiveStockupID = s11.archiveid left join login l11 on l11.userid = s11.salesUserId left join login lD11 on lD11.userid = s11.dispatchuserid left join login lR11 on lR11.userid = s11.returnuserid where BarcodeNo = @BarcodeNo5";
            command.Parameters.AddWithValue("@BarcodeNo3", BarcodeNo);
            command.Parameters.AddWithValue("@BarcodeNo4", BarcodeNo);
            command.Parameters.AddWithValue("@BarcodeNo5", BarcodeNo);
            locTable.Load(command.ExecuteReader());

            // cancle details
            DataTable cancle = new DataTable();
            command.CommandText = "select s.salesDateTime,l.username as salesUser,s.cancelTimeStamp,lD.username as cancelUser,changeStatus from cancelTrans s inner join StockUpInward si on si.StockupID=s.itemid left join login l on l.userid=s.salesUserId left join login lD on lD.userid=s.cancelId where BarcodeNo=@BarcodeNo6 " +
                "union all " +
                "select s1.salesDateTime,l1.username as salesUser,s1.cancelTimeStamp,lD1.username as cancelUser ,changeStatus from cancelTrans s1 inner join ArchiveStockUpInward si1 on si1.StockupID = s1.itemid left join login l1 on l1.userid = s1.salesUserId left join login lD1 on lD1.userid = s1.cancelId where BarcodeNo=@BarcodeNo7 " +
                "union all " +
                "select s11.salesDateTime,l11.username as salesUser,s11.cancelTimeStamp,lD11.username as cancelUser ,changeStatus from cancelTrans s11 inner join ArchiveStockUpInward si11 on si11.ArchiveStockupID = s11.archiveid left join login l11 on l11.userid = s11.salesUserId left join login lD11 on lD11.userid = s11.cancelId where BarcodeNo=@BarcodeNo8";
            command.Parameters.AddWithValue("@BarcodeNo6", BarcodeNo);
            command.Parameters.AddWithValue("@BarcodeNo7", BarcodeNo);
            command.Parameters.AddWithValue("@BarcodeNo8", BarcodeNo);


            cancle.Load(command.ExecuteReader());

            //labels
            DataTable labels = new DataTable();
            command.CommandText = "select l.barcodeNo,l.labelStatus,l.entryDate,u.username from labelLogs l inner join login u on u.userid=l.labelUserId where barcodeNo=@barcodeNo9";
            command.Parameters.AddWithValue("barcodeNo9", BarcodeNo);
            labels.Load(command.ExecuteReader());

            //labels
            DataTable statusChange = new DataTable();
            command.CommandText = "select l.barcodenewstus,l.datetime,u.username from status_log l inner join login u on u.userid=l.userid where l.barcodeno=@barcodeNo10";
            command.Parameters.AddWithValue("barcodeNo10", BarcodeNo);
            statusChange.Load(command.ExecuteReader());

            // names to datatable table
            createdby.TableName = "createdby";
            locTable.TableName = "locTable";
            cancle.TableName = "cancle";
            labels.TableName = "labels";
            statusChange.TableName = "statusChange";

            ds.Tables.Add(createdby);
            ds.Tables.Add(locTable);
            ds.Tables.Add(cancle);
            ds.Tables.Add(labels);
            ds.Tables.Add(statusChange);

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
        return ds;
    }

    public DataTable getStyleDets(string styleCode)
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
        transaction = connection.BeginTransaction("getStyle");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select i.SystemDate,l.username,i.logs,i.Title,i.mrp,c.ItemCategory,col.C1Name as brand,c2.C2Name,c3.C3Name,i.image1 from ItemStyle i inner join login l on l.userid=i.loginID inner join Column1 col on col.Col1ID=i.Col1 inner join ItemCategory c on c.ItemCategoryID=i.ItemCatID left join Category2 c2 on c2.Cat2ID=i.Cat2ID left join Category3 c3 on c3.Cat3ID=i.Cat3ID where StyleCode=@styleCode";
            command.Parameters.AddWithValue("@styleCode", styleCode);

            locTable.Load(command.ExecuteReader());

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
        return locTable;
    }

    public DataTable getBarcodes(string styleCode)
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
        transaction = connection.BeginTransaction("getStyle");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select BarcodeNo,Status,SystemDate from StockUpInward where BarcodeNo like '"+styleCode+"/%' " +
                "union all " +
                "select BarcodeNo,'DISPATCHED' as Status,SystemDate from ArchiveStockUpInward where BarcodeNo like '" + styleCode + "/%'";
            

            locTable.Load(command.ExecuteReader());

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
        return locTable;
    }

    public DataTable getListedStyles(string styleCode)
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
        transaction = connection.BeginTransaction("getStyle");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "SELECT l.sku,l.type,l.DATETIME,loc.Location,lo.username,(SELECT lr.listidgivenbyvloc FROM listchannelrecord lr WHERE l.listid=lr.listid) AS listID FROM list_log l INNER JOIN ItemStyle i ON i.StyleID=l.styleid AND i.StyleCode=@styleCode INNER JOIN Location loc ON loc.LocationID=l.locid AND loc.LocationTypeID=2 INNER JOIN login lo ON lo.userid=l.userid ORDER BY l.DATETIME desc";
            command.Parameters.AddWithValue("@styleCode", styleCode);
            locTable.Load(command.ExecuteReader());

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
        return locTable;
    }

    public DataTable getstatuscontrol(string userid, string dates)
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
        transaction = connection.BeginTransaction("getstatuscontrol");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select * from status_log s where  s.userid=@userid and   CONVERT(date, s.datetime)=@datetime ";
            command.Parameters.AddWithValue("userid", userid);
            command.Parameters.AddWithValue("datetime", dates);
            locTable.Load(command.ExecuteReader());

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
        return locTable;
    }

    public DataTable getListed(string userid, string dates)
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
        transaction = connection.BeginTransaction("getListed");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select s.*,l.Location,lr.listidgivenbyvloc from list_log s inner join Location l on l.LocationID=s.locid inner join listchannelrecord lr on lr.listid=s.listid where  s.userid=@userid and   CONVERT(date, s.datetime) =@datetime";
            command.Parameters.AddWithValue("userid", userid);
            command.Parameters.AddWithValue("datetime", dates);
            locTable.Load(command.ExecuteReader());

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
        return locTable;
    }

    public DataTable getLabels(string userid, string dates)
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
        transaction = connection.BeginTransaction("getListed");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select l.barcodeNo,l.labelStatus,l.entryDate,u.username from labelLogs l inner join login u on u.userid=l.labelUserId where u.userid=@userid and CONVERT(date, l.entryDate)=@datetime";
            command.Parameters.AddWithValue("userid", userid);
            command.Parameters.AddWithValue("datetime", dates);
            locTable.Load(command.ExecuteReader());

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
        return locTable;
    }

}