using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for labelCls
/// </summary>
public class labelCls
{
    string userId = HttpContext.Current.Session["login"].ToString();//id of logged in admin
    public labelCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable getLabels(string brand,string user)
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
            command.CommandText = "select s.StockupID,s.BarcodeNo,0 as ArchiveStockupID from StockUpInward s inner join ItemStyle i on i.StyleID=s.StyleID and i.Col1=@brand and s.labels='Inprocess' and s.labelUserId=@labelUserId " +
                "union all " +
                "select s1.StockupID,s1.BarcodeNo,s1.ArchiveStockupID from ArchiveStockUpInward s1 inner join ItemStyle i1 on i1.StyleID = s1.StyleID and i1.Col1=@brand1 and s1.labels = 'Inprocess' and s1.labelUserId=@labelUserId1";
            command.Parameters.AddWithValue("@brand", brand);
            command.Parameters.AddWithValue("@brand1", brand);
            command.Parameters.AddWithValue("@labelUserId", user);
            command.Parameters.AddWithValue("@labelUserId1", user);
            invTable.Load(command.ExecuteReader());
            transaction.Commit();          
        }
        catch (Exception ex)
        {          
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        return invTable;
    }

    public DataTable getLabelDetails(DataTable dt,string brand)
    {
        DataTable merge = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("getLblDets");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            string stock = string.Empty;
            foreach(DataRow rows in dt.Rows)
            {
                stock += rows["StockupID"] + ",";
            }
            string stock1 = stock.Remove(stock.Length - 1, 1);
            stock1 = " and StockupID in (" + stock1 + ")" ;

            command.CommandText = "select s.StyleID,s.SizeID,s.BarcodeNo,c2.C2Name as Name,CASE WHEN i.Col1=1 then SUBSTRING(i.Control3,0,len(i.Control3)-1)+' '+SUBSTRING(i.Control3,len(i.Control3)-1,len(i.Control3)) " +
                "when i.Col1 = 2 then i.Control3 + (case when i.Col3 = 1 then ' M'  when i.Col3 = 2 then ' W'  when i.Col3 = 3 then ' U' else ' K' end)+' ' + (sz.Size1) else i.Control3 end  as Article_Code, " +
                "case when i.Col1 = 1 then sz.Size1 + ' ' + 'UK - ' + (select concat(lengths,' cm') from sizeLength sl where sl.sizeId = s.SizeID and sl.brandId = 1) when i.Col1 = 2 then 'Length: ' + (select concat(lengths,' cm') from sizeLength sl where sl.sizeId = s.SizeID and sl.brandId = 2) else '' end as Sizes, " +
                "case when i.Col1 = 1 then '2N' when i.Col1 = 2 then '2N      (1 Pair)' else '' end as Qnty, " +
                "case when i.Col1 = 1 then '1' when i.Col1 = 2 then '' else '' end as Pair, " +
                "case when i.Col1 = 1 then replace(concat(s.mrp,'/-'),'.00','') when i.Col1 = 2 then concat(s.mrp,'') else '' end as MRP, case when mfgDate!='' then datename(m,mfgDate)+' '+cast(datepart(yyyy,mfgDate) as varchar) else '' end as MFG," +
                "i.Control6 as Style,i.Control1 as Color,(select e.EAN from EAN e where e.styleid = s.StyleID and e.sizeid = s.SizeID) as Barcode,'' as Company," +
                "case when i.Col1 = 1 then 'Regd. Office: No 509, CMH Road, Indiranagar, Bangalore - 560 038' when i.Col1 = 2 then 'adidas India Marketing Private Limited, Office No. 6, 2nd Floor, Sector-B, Pocket No. 7, Plot No. 11 Vasant Kunj, New Delhi - 110070' else '' end as Address," +
                "case when i.Col1 = 1 then 'Tel: 080-40852319  Toll Free: 1800-102-7862' when i.Col1 = 2 then 'Toll Free No.  1800-120-3300' else '' end as Telephone, " +
                "case when i.Col1 = 1 then 'E-mail: customercareindia@puma.com' when i.Col1 = 2 then 'E-mail: care@adidas.com' else '' end as Email," +
                "case when i.Col1 = 1 then 'DAOXIAN BUILDYET SHOES CO.,LTD' when i.Col1 = 2 then '' else '' end as MFg_Name," +
                "case when i.Col1 = 1 then 'DAOXIANINDUSTRIAL DISTRICT, YONGZHOU CITY, HUNAN PROVINCE, CHINA' when i.Col1 = 2 then '' else '' end as MFg_Address," +
                "case when i.Col1 = 1 then 'EAN 13' when i.Col1 = 2 then '' else '' end as EAN," +
                "case when i.Col1 = 1 then 'Made In China' when i.Col1 = 2 then '' else '' end as Made," +
                "CASE WHEN i.Col1 = 1 then(case when i.Col3 = 1 then ' Mens'  when i.Col3 = 2 then ' Womens'  when i.Col3 = 3 then ' Kids' else ' K' end) when i.Col1 = 2 then '' else '' end as Gender " +
                "from StockUpInward s inner join ItemStyle i on i.StyleID = s.StyleID "+ stock1 + " and i.Col1 in (1, 2) and s.labels = 'Inprocess' inner join Category2 c2 on c2.Cat2ID = i.Cat2ID " +
                "inner join Size sz on sz.SizeID = s.SizeID";

            DataTable invTable = new DataTable();
            invTable.Load(command.ExecuteReader());

            command.CommandText = "select s.StyleID,s.SizeID,s.BarcodeNo,c2.C2Name as Name,CASE WHEN i.Col1=1 then SUBSTRING(i.Control3,0,len(i.Control3)-1)+' '+SUBSTRING(i.Control3,len(i.Control3)-1,len(i.Control3)) " +
                "when i.Col1 = 2 then i.Control3 + (case when i.Col3 = 1 then ' M'  when i.Col3 = 2 then ' W'  when i.Col3 = 3 then ' U' else ' K' end)+' ' + (sz.Size1) else i.Control3 end  as Article_Code, " +
                "case when i.Col1 = 1 then sz.Size1 + ' ' + 'UK - ' + (select concat(lengths,' cm') from sizeLength sl where sl.sizeId = s.SizeID and sl.brandId = 1)  when i.Col1 = 2 then 'Length: ' + (select concat(lengths,' cm') from sizeLength sl where sl.sizeId = s.SizeID and sl.brandId = 2) else '' end as Sizes, " +
                "case when i.Col1 = 1 then '2N' when i.Col1 = 2 then '2N      (1 Pair)' else '' end as Qnty, " +
                "case when i.Col1 = 1 then '1' when i.Col1 = 2 then '' else '' end as Pair, " +
                "case when i.Col1 = 1 then replace(concat(s.mrp,'/-'),'.00','') when i.Col1 = 2 then concat(s.mrp,'') else '' end as MRP, case when mfgDate!='' then datename(m,mfgDate)+' '+cast(datepart(yyyy,mfgDate) as varchar) else '' end as MFG," +
                "i.Control6 as Style,i.Control1 as Color,(select e.EAN from EAN e where e.styleid = s.StyleID and e.sizeid = s.SizeID) as Barcode,'' as Company," +
                "case when i.Col1 = 1 then 'Regd. Office: No 509, CMH Road, Indiranagar, Bangalore - 560 038' when i.Col1 = 2 then 'adidas India Marketing Private Limited, Office No. 6, 2nd Floor, Sector-B, Pocket No. 7, Plot No. 11 Vasant Kunj, New Delhi - 110070' else '' end as Address," +
                "case when i.Col1 = 1 then 'Tel: 080-40852319  Toll Free: 1800-102-7862' when i.Col1 = 2 then 'Toll Free No.  1800-120-3300' else '' end as Telephone, " +
                "case when i.Col1 = 1 then 'E-mail: customercareindia@puma.com' when i.Col1 = 2 then 'E-mail: care@adidas.com' else '' end as Email," +
                "case when i.Col1 = 1 then 'DAOXIAN BUILDYET SHOES CO.,LTD' when i.Col1 = 2 then '' else '' end as MFg_Name," +
                "case when i.Col1 = 1 then 'DAOXIANINDUSTRIAL DISTRICT, YONGZHOU CITY, HUNAN PROVINCE, CHINA' when i.Col1 = 2 then '' else '' end as MFg_Address," +
                "case when i.Col1 = 1 then 'EAN 13' when i.Col1 = 2 then '' else '' end as EAN," +
                "case when i.Col1 = 1 then 'Made In China' when i.Col1 = 2 then '' else '' end as Made," +
                "CASE WHEN i.Col1 = 1 then(case when i.Col3 = 1 then ' Mens'  when i.Col3 = 2 then ' Womens'  when i.Col3 = 3 then ' Kids' else ' K' end) when i.Col1 = 2 then '' else '' end as Gender " +
                "from ArchiveStockUpInward s inner join ItemStyle i on i.StyleID = s.StyleID " + stock1 + " and i.Col1 in (1, 2) and s.labels = 'Inprocess' inner join Category2 c2 on c2.Cat2ID = i.Cat2ID " +
                "inner join Size sz on sz.SizeID = s.SizeID";

            DataTable invTable1 = new DataTable();
            invTable1.Load(command.ExecuteReader());
            invTable.Merge(invTable1);
            merge = invTable;
            transaction.Commit();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        return merge;
    }

    public int updateStockLabel(DataTable dt)
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
        transaction = connection.BeginTransaction("uStockLbl");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            foreach(DataRow rows in dt.Rows)
            {
                command.Parameters.AddWithValue("@labels", "Printed");
                command.Parameters.AddWithValue("@labelUserId", userId);
                command.Parameters.AddWithValue("@StockupID", rows["StockupID"]);
                if (rows["ArchiveStockupID"].Equals("0"))
                {
                    command.CommandText = "update StockUpInward set labels=@labels,labelUserId=@labelUserId where StockupID=@StockupID";
                    command.ExecuteNonQuery();
                }
                else
                {
                    command.CommandText = "update ArchiveStockUpInward set labels=@labels,labelUserId=@labelUserId where StockupID=@StockupID";
                    command.ExecuteNonQuery();
                }                
                command.Parameters.Clear();
            }
            
            transaction.Commit();
            

            if (connection.State == ConnectionState.Open)
                connection.Close();
            return 1;
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