using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for LotRepoCls
/// </summary>
public class LotRepoCls
{
    public LotRepoCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable BindLotRepo()
    {
        DataTable dtfetch = new DataTable();
        dtfetch.Columns.Add("Lot#",typeof(string));
        dtfetch.Columns.Add("Lot_Qty", typeof(Int64));
        dtfetch.Columns.Add("Lot_Amnt", typeof(decimal));
        //dtfetch.Columns.Add("Barcode");
        dtfetch.Columns.Add("Sold_Qty", typeof(Int64));
        dtfetch.Columns.Add("Avail_Qty", typeof(decimal));
        dtfetch.Columns.Add("Amnt_Earned", typeof(decimal));
        

        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("BindLotRepo");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
             //DataTable dtsp = new DataTable();
            // command.CommandText = "select s.BagID,l.NoOfBags,l.amount, count(s.StockUpId) as avail  from StockUpInward s  right join Lot l on l.BagId=s.BagID where s.BagID=l.BagId group by s.BagID,l.NoOfBags,l.amount order by s.BagID asc";
            // dtavilqty.Load(command.ExecuteReader());
            // command.Parameters.Clear();

            // DataTable dtsoldqty = new DataTable(); 
            // command.CommandText = "select l.BagID,count(s.StyleID) as soldqty    from ArchiveStockUpInward s  right join Lot l on l.BagId=s.BagID where l.BagId=s.BagID group by l.BagID order by l.BagID";
            // dtsoldqty.Load(command.ExecuteReader());
            // command.Parameters.Clear();

            //foreach(DataRow dr in dtavilqty.Rows)
            // {
            //     DataRow drfetch = dtfetch.NewRow();

            //     drfetch["id"] = dr["BagID"];
            //     drfetch["lotqty"] = dr["NoOfBags"];
            //     drfetch["lotamnt"] = dr["BagID"];
            //     drfetch["availqty"] = dr["avail"];



            //     //foreach(DataRow dr )
            // }

            //DataTable dtsp = new DataTable();
            //command.CommandText = "select lo.BagId, sum(sr.sellingprice) as totalam   from salesrecord sr  left join  ArchiveStockUpInward ar on  sr.archiveid=ar.ArchiveStockupID right join Lot lo on lo.BagId=ar.BagID  where sr.status='DISPATCHED' and sr.archiveid=ar.ArchiveStockupID and ar.BagID=lo.BagId group by lo.BagId order by lo.BagId asc";
            //dtsp.Load(command.ExecuteReader());
            //command.Parameters.Clear();


            DataTable dt = new DataTable();
            command.CommandText = "select s.BagID,l.BagDescription,l.NoOfBags,l.amount,l.oldsftqty,l.oldsftamt, count(s.StockUpId) as avail,(select count(ar.ArchiveStockupID) as sold  from ArchiveStockUpInward ar right join Lot l  on l.BagId=s.BagID where ar.BagID=l.BagId  ) as sold  from StockUpInward s   right join Lot l on l.BagId=s.BagID  where s.BagID=l.BagId group by s.BagID,l.NoOfBags,l.amount,l.oldsftqty,l.oldsftamt,l.BagDescription order by s.BagID asc";
            dt.Load(command.ExecuteReader());
            command.Parameters.Clear();


            foreach(DataRow dr in dt.Rows)
            {
                DataRow drfetch = dtfetch.NewRow();

                drfetch["Lot#"] = dr["BagDescription"];
                drfetch["Lot_Qty"] = dr["NoOfBags"];
                drfetch["Lot_Amnt"] = dr["amount"];

                double totalqty;
                double oldsoftqty;
                if (dr["oldsftqty"].Equals(DBNull.Value))
                {
                    oldsoftqty = 0;
                }
                else
                {
                    oldsoftqty= Convert.ToDouble(dr["oldsftqty"]);
                }
                totalqty = Convert.ToDouble(dr["avail"]) + oldsoftqty;

                drfetch["Avail_Qty"] = totalqty;
                drfetch["Sold_Qty"] = dr["sold"];

                DataTable dtsp = new DataTable();
                command.CommandText = "select ar.BagId, sum(sr.sellingprice) as totalam    from salesrecord sr  left join  ArchiveStockUpInward ar on  sr.archiveid=ar.ArchiveStockupID  where sr.status='DISPATCHED' and sr.archiveid=ar.ArchiveStockupID and ar.BagID=@BagID group by ar.BagId ";
                command.Parameters.AddWithValue("@BagID", dr["BagID"]);
                dtsp.Load(command.ExecuteReader());
                command.Parameters.Clear();
                double sp = 00;
                if(dtsp.Rows.Count.Equals(0))
                {
                    sp = 00;
                }
                else
                {
                    sp = Convert.ToDouble(dtsp.Rows[0]["totalam"].ToString());
                }
                double oldsoftamt;
                if (dr["oldsftamt"].Equals(DBNull.Value))
                {
                    oldsoftamt = 0;
                }
                else
                {
                    oldsoftamt = Convert.ToDouble(dr["oldsftqty"]);
                }



                double totalsp;
                totalsp = sp + oldsoftamt;
                double amount;
                if (dr["amount"].Equals(DBNull.Value))
                {
                    amount = 00;
                }
                else
                {
                    amount = Convert.ToDouble(dr["amount"]);
                }
                double amtenerd;
                
                //double amount= Convert.ToDouble(dr["amount"]);

                amtenerd = amount - totalsp ;

                drfetch["Amnt_Earned"] = amtenerd;
                dtfetch.Rows.Add(drfetch);
            }



            

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
            return null;
        }
        return dtfetch;
    }

    public DataTable BindReturnRepo()
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
        transaction = connection.BeginTransaction("BindReturnRepo");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select s.sid,count(s.sid) as cunt,s.reasons,s.returntimestamp,CONVERT(CHAR(4), returntimestamp, 100) as Month , CONVERT(CHAR(4), returntimestamp, 120)  as year,l.Location,s.itemid from salesrecord s  left join Location l on l.LocationID=s.saleschannelvlocid where s.status='RETURN' group by s.sid,s.returntimestamp,s.reasons,l.Location,s.itemid";

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
    public DataTable BindRemittencerepo()
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
        transaction = connection.BeginTransaction("BindReturnRepo");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText= "select t.replyDate, CONVERT(CHAR(4), replyDate, 100) as Month , CONVERT(CHAR(4), replyDate, 120)  as year,L.Location,s.salesidgivenbyvloc,p.curr_item_status,t.ticketStatus,st.BagID,col.C1Name,item.Title, "+
                                  "concat(sz.Size1,'-',sz.ItemCategoryID) as size,item.Control3, DATEDIFF(day, o.systemdate, GETDATE()) as lotbucket, DATEDIFF(day, st.SystemDate, GETDATE()) as barcodebucket,s.sellingprice as MRP,p.prod_price as Selling_Price,p.merket_place_comm as VLcommision,  " +
                                  "p.pymt_gtwy_fee as gatewayfee,p.mrkt_logist_chrgs as logisticfee,  "+
                                  "p.merkt_plc_comm_igst + p.pymt_gtwy_fee_igst + p.mrkt_logist_chrgs_igst as totalIGST,  "+
                                  "p.merkt_plc_comm_cgst + p.pymt_gtwy_fee_cgst + p.mrkt_logist_chrgs_cgst as TotalCGST,  "+
                                  "p.merkt_plc_comm_sgst + p.pymt_gtwy_fee_sgst + p.mrkt_logist_chrgs_sgst as TotalSGST,s.invoiceid  " +
                                  "from ticket_master t  "+
                                  "inner join salesrecord s on s.sid = t.salesid  "+
                                  "left join payment_transaction p on p.order_id = s.salesidgivenbyvloc  "+
                                  "inner join invoice i on i.invid = s.invoiceid  "+
                                  "inner join Location L on L.LocationID = s.saleschannelvlocid  "+
                                  "inner join StockUpInward st on st.StockupID = s.itemid  "+
                                  "inner join ItemStyle item on item.StyleID = st.StyleID  " +
                                  "inner join Column1 col on col.Col1ID = item.Col1  " +
                                  "inner join Size sz on sz.SizeID = st.SizeID  " +
                                  "inner join Lot o on o.BagId = st.BagID ";



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



    public DataTable BindConsolidatedRepobyVloc(string id,string status)
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
        transaction = connection.BeginTransaction("BindConsolidatedRepobyVloc");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select s.recordtimestamp as date,CONVERT(CHAR(4), s.recordtimestamp, 100) as Month , CONVERT(CHAR(4), s.recordtimestamp, 120)  as year,l.Location,s.invoiceid,s.salesidgivenbyvloc, "+
                                  "p.pymt_status,s.status,st.BagID as lot,col.C1Name,item.Title,concat(sz.Size1, '-', sz.ItemCategoryID) as size,item.Control3, " +
                                  "DATEDIFF(day, o.systemdate, GETDATE()) as lotbucket, DATEDIFF(day, st.SystemDate, GETDATE()) as barcodebucket,s.sellingprice as MRP,p.prod_price as Selling_Price,p.merket_place_comm as VLcommision, " +
                                  "p.pymt_gtwy_fee as gatewayfee,p.mrkt_logist_chrgs as logisticfee,   " +
                                  "p.merkt_plc_comm_igst + p.pymt_gtwy_fee_igst + p.mrkt_logist_chrgs_igst as totalIGST,  " +
                                  "p.merkt_plc_comm_cgst + p.pymt_gtwy_fee_cgst + p.mrkt_logist_chrgs_cgst as TotalCGST,   " +
                                  "p.merkt_plc_comm_sgst + p.pymt_gtwy_fee_sgst + p.mrkt_logist_chrgs_sgst as TotalSGST  " +
                                  "From salesrecord s " +
                                  "inner join Location l on l.LocationID = s.saleschannelvlocid " +
                                  "left join payment_transaction p on p.order_id = s.salesidgivenbyvloc " +
                                  "left join StockUpInward st on st.SalesID = s.itemid " +
                                  "left join ItemStyle item on item.StyleID = st.StyleID " +
                                  "left join Column1 col on col.Col1ID = item.Col1 " +
                                  "left join Size sz on sz.SizeID = st.SizeID " +
                                  "left join Lot o on o.BagId = st.BagID " +
                                  "where l.LocationID = s.saleschannelvlocid  and l.LocationID = @LocationID and s.status=@status ";
            command.Parameters.AddWithValue("@LocationID", id);
            command.Parameters.AddWithValue("@status", status);



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