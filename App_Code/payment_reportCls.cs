using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

/// <summary>
/// Summary description for payment_reportCls
/// </summary>
public class payment_reportCls
{
    public payment_reportCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable BindRemittance(string vlocid)
    {
        DataTable Table = new DataTable();
        DataTable finaldt = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("Bind");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {

            /*command.CommandText = "select * from(select p.salesid, st.BarcodeNo, p.stockupId,convert(varchar, st.SystemDate, 104) as date, CONVERT(CHAR(4), st.SystemDate, 100) as Month, CONVERT(CHAR(4), st.SystemDate, 120) as year, sales.Status as sys_status, i.StyleCode, concat(i.StyleCode, '-', sz.Size1) as SKU, " +
                                  "st.mrp, lot.BagDescription as LotNo, col1.C1Name as Brand, i.Title, sz.Size1 as Size, cat.ItemCategory , loc.Location, sales.invoiceid, p.Payment_Status,i.Control3 as articel,p.TCS_CGST+p.TCS_IGST+p.TCS_GST as utax,p.percentage,p.Payable_Amoun, " +
                                  "DATEDIFF(m, st.SystemDate, sales.recordtimestamp) as stockAge, DATEDIFF(m, lot.invoiceDate, sales.recordtimestamp) as lotAge,p.pmt_sp_inc_gst as spincgst,p.pmt_sp_ex_gst as spexcgst, " +
                                  "p.channel_commsion, p.Channel_Gateway, p.VL_Logistics, p.VLPenalty, (ISNULL(p.Pack_Charges_VL_Misc, 0) + ISNULL(p.Spcl_pack_chrgs_vlMics, 0) + ISNULL(p.Misc_charge, 0)) as misccharges, " +
                                 "(p.pmt_scomm_inc_cgst + p.pmt_cgate_inc_cgst + p.pmt_vllogi_inc_cgst + p.pmt_mic1_inc_cgst + p.pmt_mic2_inc_cgst + p.pmt_chnpent_inc_cgst) - (p.pmt_scomm_ex_cgst + p.pmt_cgate_ex_cgst + p.pmt_vllogi_ex_cgst + p.pmt_mic1_ex_cgst + p.pmt_mic2_ex_cgst + p.pmt_chnpent_exc_cgst) as totCGST, "+
                                 "(p.pmt_scomm_inc_sgst + p.pmt_cgate_inc_sgst + p.pmt_vllogi_inc_sgst + p.pmt_mic1_inc_sgst + p.pmt_mic2_inc_sgst + p.pmt_chnpent_inc_sgst) - (p.pmt_scomm_ex_sgst + p.pmt_cgate_ex_sgst + p.pmt_vllogi_ex_sgst + p.pmt_mic1_ex_sgst + p.pmt_mic2_ex_cgst + p.pmt_chnpent_exc_sgst) as totSGST, "+
                                 "(p.pmt_cgate_inc_gst + p.pmt_scomm_inc_gst + p.pmt_vllogi_inc_gst + p.pmt_mic1_inc_gst + p.pmt_vlpenl_inc_gst + p.pmt_mic1_inc_gst + p.pmt_mic2_inc_gst) - (p.pmt_cgate_ex_gst + p.pmt_scomm_ex_gst + p.pmt_vllogi_ex_gst + p.pmt_mic1_ex_gst + p.pmt_vlpenl_ex_gst + p.pmt_mic1_ex_gst + p.pmt_mic2_ex_gst) as totIGST, "+
                                  "(ISNULL(p.mp_commission_cgst, 0) + ISNULL(p.mp_commission_sgst, 0) + ISNULL(p.mp_commission_igst, 0) + ISNULL(p.logistics_cgst, 0) + ISNULL(p.logistics_sgst, 0) + ISNULL(p.logistics_igst, 0)) as totDeduction, " +
                                  "st.mrp - (ISNULL(p.mp_commission_cgst, 0) + ISNULL(p.mp_commission_sgst, 0) + ISNULL(p.mp_commission_igst, 0) + ISNULL(p.logistics_cgst, 0) + ISNULL(p.logistics_sgst, 0) + ISNULL(p.logistics_igst, 0)) - 0 as setamt, " +
                                  "(st.mrp - (ISNULL(p.mp_commission_cgst, 0) + ISNULL(p.mp_commission_sgst, 0) + ISNULL(p.mp_commission_igst, 0) + ISNULL(p.logistics_cgst, 0) + ISNULL(p.logistics_sgst, 0) + ISNULL(p.logistics_igst, 0))) / NULLIF(st.mrp, 0) * 100 as setper, " +
                                  "st.mrp - (ISNULL(p.channel_commsion, 0) + ISNULL(p.Channel_Gateway, 0)) - 0 as netpro, " +
                                  "(st.mrp - (ISNULL(p.channel_commsion, 0) + ISNULL(p.Channel_Gateway, 0))) / NULLIF(st.mrp, 0) * 100 as netper, " +
                                  "convert(varchar, p.order_date, 104) as pmt_date,CONVERT(CHAR(4), p.order_date, 100) as pmt_Month , CONVERT(CHAR(4),p.order_date, 120)  as pmt_year,p.type as pmt_effecting_status,sales.sellingprice as sys_sp,p.sp as pmt_sp,cast(((st.mrp*isnull(lot.Percentage,0))/100) as decimal(10,2)) as cp " +
                                  "from(select * from payment_trans where stockupId != -1 and stockupId != -2) p " +
                                  "inner join salesrecord sales on p.salesid = sales.salesidgivenbyvloc and p.vlocid = sales.saleschannelvlocid and sales.itemid != -1 " +
                                  "inner join StockUpInward st on st.StockupID = p.stockupId " +
                                  "inner join ItemStyle i on i.StyleID = st.StyleID " +
                                  "inner join Size sz on sz.SizeID = st.SizeID " +
                                  "inner join ItemCategory cat on cat.ItemCategoryID = i.ItemCatID " +
                                  "inner join Lot lot on lot.BagId = st.BagID " +
                                  "left join invoice inv on inv.invid = sales.invoiceid " +
                                  "left join Location loc on loc.LocationID = sales.saleschannelvlocid " +
                                  "left join Column1 col1 on col1.Col1ID = i.Col1 " +

                                  "union all " +
                                  "select p.salesid, st.BarcodeNo,p.stockupId, convert(varchar, st.SystemDate, 104) as date, CONVERT(CHAR(4), st.SystemDate, 100) as Month, CONVERT(CHAR(4), st.SystemDate, 120) as year, sales.Status as sys_status, i.StyleCode, concat(i.StyleCode, '-', sz.Size1) as SKU, " +
                                  "st.mrp, lot.BagDescription as LotNo, col1.C1Name as Brand, i.Title, sz.Size1 as Size, cat.ItemCategory , loc.Location, sales.invoiceid, p.Payment_Status,i.Control3 as articel,p.TCS_CGST+p.TCS_IGST+p.TCS_GST as utax,p.percentage,p.Payable_Amoun, " +
                                  "DATEDIFF(m, st.SystemDate, sales.recordtimestamp) as stockAge, DATEDIFF(m, lot.invoiceDate, sales.recordtimestamp) as lotAge,p.pmt_sp_inc_gst as spincgst,p.pmt_sp_ex_gst as spexcgst, " +
                                  "p.channel_commsion, p.Channel_Gateway, p.VL_Logistics, p.VLPenalty, (ISNULL(p.Pack_Charges_VL_Misc, 0) + ISNULL(p.Spcl_pack_chrgs_vlMics, 0) + ISNULL(p.Misc_charge, 0)) as misccharges, " +
                                  "(p.pmt_scomm_inc_cgst + p.pmt_cgate_inc_cgst + p.pmt_vllogi_inc_cgst + p.pmt_mic1_inc_cgst + p.pmt_mic2_inc_cgst + p.pmt_chnpent_inc_cgst) - (p.pmt_scomm_ex_cgst + p.pmt_cgate_ex_cgst + p.pmt_vllogi_ex_cgst + p.pmt_mic1_ex_cgst + p.pmt_mic2_ex_cgst + p.pmt_chnpent_exc_cgst) as totCGST, "+
                                  "(p.pmt_scomm_inc_sgst + p.pmt_cgate_inc_sgst + p.pmt_vllogi_inc_sgst + p.pmt_mic1_inc_sgst + p.pmt_mic2_inc_sgst + p.pmt_chnpent_inc_sgst) - (p.pmt_scomm_ex_sgst + p.pmt_cgate_ex_sgst + p.pmt_vllogi_ex_sgst + p.pmt_mic1_ex_sgst + p.pmt_mic2_ex_cgst + p.pmt_chnpent_exc_sgst) as totSGST, "+
                                  "(p.pmt_cgate_inc_gst + p.pmt_scomm_inc_gst + p.pmt_vllogi_inc_gst + p.pmt_mic1_inc_gst + p.pmt_vlpenl_inc_gst + p.pmt_mic1_inc_gst + p.pmt_mic2_inc_gst) - (p.pmt_cgate_ex_gst + p.pmt_scomm_ex_gst + p.pmt_vllogi_ex_gst + p.pmt_mic1_ex_gst + p.pmt_vlpenl_ex_gst + p.pmt_mic1_ex_gst + p.pmt_mic2_ex_gst) as totIGST, "+
                                  "(ISNULL(p.mp_commission_cgst, 0) + ISNULL(p.mp_commission_sgst, 0) + ISNULL(p.mp_commission_igst, 0) + ISNULL(p.logistics_cgst, 0) + ISNULL(p.logistics_sgst, 0) + ISNULL(p.logistics_igst, 0)) as totDeduction, " +
                                  "st.mrp - (ISNULL(p.mp_commission_cgst, 0) + ISNULL(p.mp_commission_sgst, 0) + ISNULL(p.mp_commission_igst, 0) + ISNULL(p.logistics_cgst, 0) + ISNULL(p.logistics_sgst, 0) + ISNULL(p.logistics_igst, 0)) - 0 as setamt, " +
                                  "(st.mrp - (ISNULL(p.mp_commission_cgst, 0) + ISNULL(p.mp_commission_sgst, 0) + ISNULL(p.mp_commission_igst, 0) + ISNULL(p.logistics_cgst, 0) + ISNULL(p.logistics_sgst, 0) + ISNULL(p.logistics_igst, 0))) / NULLIF(st.mrp, 0) * 100 as setper, " +
                                  "st.mrp - (ISNULL(p.channel_commsion, 0) + ISNULL(p.Channel_Gateway, 0)) - 0 as netpro, " +
                                  "(st.mrp - (ISNULL(p.channel_commsion, 0) + ISNULL(p.Channel_Gateway, 0))) / NULLIF(st.mrp, 0) * 100 as netper," +
                                  "convert(varchar, p.order_date, 104) as pmt_date,CONVERT(CHAR(4), p.order_date, 100) as pmt_Month , CONVERT(CHAR(4),p.order_date, 120)  as pmt_year,p.type as pmt_effecting_status,sales.sellingprice as sys_sp,p.sp as pmt_sp ,cast(((st.mrp*isnull(lot.Percentage,0))/100) as decimal(10,2)) as cp " +
                                  "from(select * from payment_trans where stockupId != -1  and stockupId != -2) p  " +
                                  "inner join salesrecord sales on p.salesid = sales.salesidgivenbyvloc and p.vlocid = sales.saleschannelvlocid and sales.archiveid != -1 " +
                                  "inner join ArchiveStockUpInward st on st.StockupID = p.stockupId " +
                                  "inner join ItemStyle i on i.StyleID = st.StyleID " +
                                  "inner join Size sz on sz.SizeID = st.SizeID " +
                                  "inner join ItemCategory cat on cat.ItemCategoryID = i.ItemCatID " +
                                  "inner join Lot lot on lot.BagId = st.BagID " +
                                  "left join invoice inv on inv.invid = sales.invoiceid " +
                                  "left join Location loc on loc.LocationID = sales.saleschannelvlocid " +
                                  "left join Column1 col1 on col1.Col1ID = i.Col1 " +
                                  "union all " +
                                  "select p.salesid, st.BarcodeNo,p.stockupId, convert(varchar, st.SystemDate, 104) as date, CONVERT(CHAR(4), st.SystemDate, 100) as Month, CONVERT(CHAR(4), st.SystemDate, 120) as year, sales.Status as sys_status, i.StyleCode, concat(i.StyleCode, '-', sz.Size1) as SKU, " +
                                  "st.mrp, lot.BagDescription as LotNo, col1.C1Name as Brand, i.Title, sz.Size1 as Size, cat.ItemCategory , loc.Location, sales.invoiceid, p.Payment_Status, i.Control3 as articel,p.TCS_CGST+p.TCS_IGST+p.TCS_GST as utax,p.percentage,p.Payable_Amoun," +
                                  "DATEDIFF(m, st.SystemDate, sales.recordtimestamp) as stockAge, DATEDIFF(m, lot.invoiceDate, sales.recordtimestamp) as lotAge, p.pmt_sp_inc_gst as spincgst,p.pmt_sp_ex_gst as spexcgst," +
                                  "p.channel_commsion, p.Channel_Gateway, p.VL_Logistics, p.VLPenalty, (ISNULL(p.Pack_Charges_VL_Misc, 0) + ISNULL(p.Spcl_pack_chrgs_vlMics, 0) + ISNULL(p.Misc_charge, 0)) as misccharges, " +
                                  "(p.pmt_scomm_inc_cgst + p.pmt_cgate_inc_cgst + p.pmt_vllogi_inc_cgst + p.pmt_mic1_inc_cgst + p.pmt_mic2_inc_cgst + p.pmt_chnpent_inc_cgst) - (p.pmt_scomm_ex_cgst + p.pmt_cgate_ex_cgst + p.pmt_vllogi_ex_cgst + p.pmt_mic1_ex_cgst + p.pmt_mic2_ex_cgst + p.pmt_chnpent_exc_cgst) as totCGST, "+
                                  "(p.pmt_scomm_inc_sgst + p.pmt_cgate_inc_sgst + p.pmt_vllogi_inc_sgst + p.pmt_mic1_inc_sgst + p.pmt_mic2_inc_sgst + p.pmt_chnpent_inc_sgst) - (p.pmt_scomm_ex_sgst + p.pmt_cgate_ex_sgst + p.pmt_vllogi_ex_sgst + p.pmt_mic1_ex_sgst + p.pmt_mic2_ex_cgst + p.pmt_chnpent_exc_sgst) as totSGST, "+
                                  "(p.pmt_cgate_inc_gst + p.pmt_scomm_inc_gst + p.pmt_vllogi_inc_gst + p.pmt_mic1_inc_gst + p.pmt_vlpenl_inc_gst + p.pmt_mic1_inc_gst + p.pmt_mic2_inc_gst) - (p.pmt_cgate_ex_gst + p.pmt_scomm_ex_gst + p.pmt_vllogi_ex_gst + p.pmt_mic1_ex_gst + p.pmt_vlpenl_ex_gst + p.pmt_mic1_ex_gst + p.pmt_mic2_ex_gst) as totIGST, "+
                                  "(ISNULL(p.mp_commission_cgst, 0) + ISNULL(p.mp_commission_sgst, 0) + ISNULL(p.mp_commission_igst, 0) + ISNULL(p.logistics_cgst, 0) + ISNULL(p.logistics_sgst, 0) + ISNULL(p.logistics_igst, 0)) as totDeduction, " +
                                  "st.mrp - (ISNULL(p.mp_commission_cgst, 0) + ISNULL(p.mp_commission_sgst, 0) + ISNULL(p.mp_commission_igst, 0) + ISNULL(p.logistics_cgst, 0) + ISNULL(p.logistics_sgst, 0) + ISNULL(p.logistics_igst, 0)) - 0 as setamt, " +
                                  "(st.mrp - (ISNULL(p.mp_commission_cgst, 0) + ISNULL(p.mp_commission_sgst, 0) + ISNULL(p.mp_commission_igst, 0) + ISNULL(p.logistics_cgst, 0) + ISNULL(p.logistics_sgst, 0) + ISNULL(p.logistics_igst, 0))) / NULLIF(st.mrp, 0) * 100 as setper, " +
                                  "st.mrp - (ISNULL(p.channel_commsion, 0) + ISNULL(p.Channel_Gateway, 0)) - 0 as netpro, " +
                                  "(st.mrp - (ISNULL(p.channel_commsion, 0) + ISNULL(p.Channel_Gateway, 0))) / NULLIF(st.mrp, 0) * 100 as netper," +
                                  "convert(varchar, p.order_date, 104) as pmt_date,CONVERT(CHAR(4), p.order_date, 100) as pmt_Month , CONVERT(CHAR(4),p.order_date, 120)  as pmt_year,p.type as pmt_effecting_status,sales.sellingprice as sys_sp,p.sp as pmt_sp,cast(((st.mrp*isnull(lot.Percentage,0))/100) as decimal(10,2)) as cp " +
                                  "from(select * from payment_trans where stockupId != -1 and stockupId != -2) p " +
                                  "inner join cancelTrans sales on p.salesid = sales.salesidgivenbyvloc and p.vlocid = sales.saleschannelvlocid and sales.itemid != -1 " +
                                  "inner join StockUpInward st on st.StockupID = p.stockupId " +
                                  "inner join ItemStyle i on i.StyleID = st.StyleID " +
                                  "inner join Size sz on sz.SizeID = st.SizeID " +
                                  "inner join ItemCategory cat on cat.ItemCategoryID = i.ItemCatID " +
                                  "inner join Lot lot on lot.BagId = st.BagID " +
                                  "left join invoice inv on inv.invid = sales.invoiceid " +
                                  "left join Location loc on loc.LocationID = sales.saleschannelvlocid " +
                                  "left join Column1 col1 on col1.Col1ID = i.Col1 " +
                                  "union all " +
                                  "select p.salesid, st.BarcodeNo,p.stockupId, convert(varchar, st.SystemDate, 104) as date, CONVERT(CHAR(4), st.SystemDate, 100) as Month, CONVERT(CHAR(4), st.SystemDate, 120) as year, sales.Status as sys_status, i.StyleCode, concat(i.StyleCode, '-', sz.Size1) as SKU, " +
                                  "st.mrp, lot.BagDescription as LotNo, col1.C1Name as Brand, i.Title, sz.Size1 as Size, cat.ItemCategory, loc.Location, sales.invoiceid, p.Payment_Status, i.Control3 as articel,p.TCS_CGST+p.TCS_IGST+p.TCS_GST as utax,p.percentage,p.Payable_Amoun," +
                                  "DATEDIFF(m, st.SystemDate, sales.recordtimestamp) as stockAge, DATEDIFF(m, lot.invoiceDate, sales.recordtimestamp) as lotAge,p.pmt_sp_inc_gst as spincgst,p.pmt_sp_ex_gst as spexcgst," +
                                  "p.channel_commsion, p.Channel_Gateway, p.VL_Logistics, p.VLPenalty, (ISNULL(p.Pack_Charges_VL_Misc, 0) + ISNULL(p.Spcl_pack_chrgs_vlMics, 0) + ISNULL(p.Misc_charge, 0)) as misccharges, " +
                                  "(p.pmt_scomm_inc_cgst + p.pmt_cgate_inc_cgst + p.pmt_vllogi_inc_cgst + p.pmt_mic1_inc_cgst + p.pmt_mic2_inc_cgst + p.pmt_chnpent_inc_cgst) - (p.pmt_scomm_ex_cgst + p.pmt_cgate_ex_cgst + p.pmt_vllogi_ex_cgst + p.pmt_mic1_ex_cgst + p.pmt_mic2_ex_cgst + p.pmt_chnpent_exc_cgst) as totCGST, "+
                                  "(p.pmt_scomm_inc_sgst + p.pmt_cgate_inc_sgst + p.pmt_vllogi_inc_sgst + p.pmt_mic1_inc_sgst + p.pmt_mic2_inc_sgst + p.pmt_chnpent_inc_sgst) - (p.pmt_scomm_ex_sgst + p.pmt_cgate_ex_sgst + p.pmt_vllogi_ex_sgst + p.pmt_mic1_ex_sgst + p.pmt_mic2_ex_cgst + p.pmt_chnpent_exc_sgst) as totSGST, "+
                                  "(p.pmt_cgate_inc_gst + p.pmt_scomm_inc_gst + p.pmt_vllogi_inc_gst + p.pmt_mic1_inc_gst + p.pmt_vlpenl_inc_gst + p.pmt_mic1_inc_gst + p.pmt_mic2_inc_gst) - (p.pmt_cgate_ex_gst + p.pmt_scomm_ex_gst + p.pmt_vllogi_ex_gst + p.pmt_mic1_ex_gst + p.pmt_vlpenl_ex_gst + p.pmt_mic1_ex_gst + p.pmt_mic2_ex_gst) as totIGST, "+
                                  "(ISNULL(p.mp_commission_cgst, 0) + ISNULL(p.mp_commission_sgst, 0) + ISNULL(p.mp_commission_igst, 0) + ISNULL(p.logistics_cgst, 0) + ISNULL(p.logistics_sgst, 0) + ISNULL(p.logistics_igst, 0)) as totDeduction, " +
                                  "st.mrp - (ISNULL(p.mp_commission_cgst, 0) + ISNULL(p.mp_commission_sgst, 0) + ISNULL(p.mp_commission_igst, 0) + ISNULL(p.logistics_cgst, 0) + ISNULL(p.logistics_sgst, 0) + ISNULL(p.logistics_igst, 0)) - 0 as setamt, " +
                                  "(st.mrp - (ISNULL(p.mp_commission_cgst, 0) + ISNULL(p.mp_commission_sgst, 0) + ISNULL(p.mp_commission_igst, 0) + ISNULL(p.logistics_cgst, 0) + ISNULL(p.logistics_sgst, 0) + ISNULL(p.logistics_igst, 0))) / NULLIF(st.mrp, 0) * 100 as setper, " +
                                  "st.mrp - (ISNULL(p.channel_commsion, 0) + ISNULL(p.Channel_Gateway, 0)) - 0 as netpro," +
                                  "(st.mrp - (ISNULL(p.channel_commsion, 0) + ISNULL(p.Channel_Gateway, 0))) / NULLIF(st.mrp, 0) * 100 as netper," +
                                  "convert(varchar, p.order_date, 104) as pmt_date,CONVERT(CHAR(4), p.order_date, 100) as pmt_Month , CONVERT(CHAR(4),p.order_date, 120)  as pmt_year,p.type as pmt_effecting_status,sales.sellingprice as sys_sp,p.sp as pmt_sp,cast(((st.mrp*isnull(lot.Percentage,0))/100) as decimal(10,2)) as cp " +

                                  "from(select * from payment_trans where stockupId != -1 and stockupId != -2) p " +
                                  "inner join cancelTrans sales on p.salesid = sales.salesidgivenbyvloc and p.vlocid = sales.saleschannelvlocid and sales.archiveid != -1 " +
                                  "inner join ArchiveStockUpInward st on st.StockupID = p.stockupId " +
                                  "inner join ItemStyle i on i.StyleID = st.StyleID  " +
                                  "inner join Size sz on sz.SizeID = st.SizeID " +
                                  "inner join ItemCategory cat on cat.ItemCategoryID = i.ItemCatID " +
                                  "inner join Lot lot on lot.BagId = st.BagID " +
                                  "left join invoice inv on inv.invid = sales.invoiceid " +
                                  "left join Location loc on loc.LocationID = sales.saleschannelvlocid " +
                                  "left join Column1 col1 on col1.Col1ID = i.Col1) newTable ";*/

              command.CommandText= "select * from(select p.salesid, st.BarcodeNo, p.stockupId, convert(varchar, st.SystemDate, 104) as date, CONVERT(CHAR(4), st.SystemDate, 100) as Month, CONVERT(CHAR(4), st.SystemDate, 120) as year, sales.Status as sys_status, i.StyleCode, concat(i.StyleCode, '-', sz.Size1) as SKU, st.mrp, lot.BagDescription as LotNo, col1.C1Name as Brand, i.Title, sz.Size1 as Size, cat.ItemCategory, loc.Location, sales.invoiceid, p.Payment_Status, i.Control3 as articel, p.TCS_CGST + p.TCS_IGST + p.TCS_GST as utax, lot.percentage, p.Payable_Amoun, DATEDIFF(m, st.SystemDate, sales.recordtimestamp) as stockAge, DATEDIFF(m, lot.invoiceDate, sales.recordtimestamp) as lotAge, p.pmt_sp_inc_gst as spincgst, p.pmt_sp_ex_gst as spexcgst, p.channel_commsion, p.Channel_Gateway, p.VL_Logistics, p.VLPenalty, (ISNULL(p.Pack_Charges_VL_Misc, 0) + ISNULL(p.Spcl_pack_chrgs_vlMics, 0) + ISNULL(p.Misc_charge, 0)) as misccharges, (p.pmt_scomm_inc_cgst + p.pmt_cgate_inc_cgst + p.pmt_vllogi_inc_cgst + p.pmt_mic1_inc_cgst + p.pmt_mic2_inc_cgst + p.pmt_chnpent_inc_cgst) - (p.pmt_scomm_ex_cgst + p.pmt_cgate_ex_cgst + p.pmt_vllogi_ex_cgst + p.pmt_mic1_ex_cgst + p.pmt_mic2_ex_cgst + p.pmt_chnpent_exc_cgst) as totCGST, (p.pmt_scomm_inc_sgst + p.pmt_cgate_inc_sgst + p.pmt_vllogi_inc_sgst + p.pmt_mic1_inc_sgst + p.pmt_mic2_inc_sgst + p.pmt_chnpent_inc_sgst) - (p.pmt_scomm_ex_sgst + p.pmt_cgate_ex_sgst + p.pmt_vllogi_ex_sgst + p.pmt_mic1_ex_sgst + p.pmt_mic2_ex_cgst + p.pmt_chnpent_exc_sgst) as totSGST, (p.pmt_cgate_inc_gst + p.pmt_scomm_inc_gst + p.pmt_vllogi_inc_gst + p.pmt_mic1_inc_gst + p.pmt_vlpenl_inc_gst + p.pmt_mic1_inc_gst + p.pmt_mic2_inc_gst) - (p.pmt_cgate_ex_gst + p.pmt_scomm_ex_gst + p.pmt_vllogi_ex_gst + p.pmt_mic1_ex_gst + p.pmt_vlpenl_ex_gst + p.pmt_mic1_ex_gst + p.pmt_mic2_ex_gst) as totIGST, (ISNULL(p.mp_commission_cgst, 0) + ISNULL(p.mp_commission_sgst, 0) + ISNULL(p.mp_commission_igst, 0) + ISNULL(p.logistics_cgst, 0) + ISNULL(p.logistics_sgst, 0) + ISNULL(p.logistics_igst, 0)) as totDeduction, st.mrp - (ISNULL(p.mp_commission_cgst, 0) + ISNULL(p.mp_commission_sgst, 0) + ISNULL(p.mp_commission_igst, 0) + ISNULL(p.logistics_cgst, 0) + ISNULL(p.logistics_sgst, 0) + ISNULL(p.logistics_igst, 0)) - 0 as setamt, (st.mrp - (ISNULL(p.mp_commission_cgst, 0) + ISNULL(p.mp_commission_sgst, 0) + ISNULL(p.mp_commission_igst, 0) + ISNULL(p.logistics_cgst, 0) + ISNULL(p.logistics_sgst, 0) + ISNULL(p.logistics_igst, 0))) / NULLIF(st.mrp, 0) * 100 as setper, st.mrp - (ISNULL(p.channel_commsion, 0) + ISNULL(p.Channel_Gateway, 0)) - 0 as netpro, (st.mrp - (ISNULL(p.channel_commsion, 0) + ISNULL(p.Channel_Gateway, 0))) / NULLIF(st.mrp, 0) * 100 as netper, convert(varchar, p.order_date, 104) as pmt_date, CONVERT(CHAR(4), p.order_date, 100) as pmt_Month, CONVERT(CHAR(4), p.order_date, 120) as pmt_year, p.type as pmt_effecting_status, sales.sellingprice as sys_sp, p.sp as pmt_sp, lot.Percentage as cp from(select * from payment_trans where stockupId != -1) p "+
                                   "inner join salesrecord sales on p.salesid = sales.salesidgivenbyvloc and p.vlocid = sales.saleschannelvlocid " +
                                   "inner join StockUpInward st on st.StockupID = p.stockupId and st.StockupID = sales.itemid and sales.itemid != -1 " +
                                   "inner join ItemStyle i on i.StyleID = st.StyleID " +
                                   "inner join Size sz on sz.SizeID = st.SizeID " +
                                   "inner join ItemCategory cat on cat.ItemCategoryID = i.ItemCatID " +
                                   "inner join Lot lot on lot.BagId = st.BagID " +
                                   "left join invoice inv on inv.invid = sales.invoiceid left join Location loc on loc.LocationID = sales.saleschannelvlocid " +
                                   "left join Column1 col1 on col1.Col1ID = i.Col1 where p.vlocid=@vlocid1 " +
                                   " union all " +
                                   " select p.salesid, st.BarcodeNo, p.stockupId, convert(varchar, st.SystemDate, 104) as date, CONVERT(CHAR(4), st.SystemDate, 100) as Month, CONVERT(CHAR(4), st.SystemDate, 120) as year, sales.Status as sys_status, i.StyleCode, concat(i.StyleCode, '-', sz.Size1) as SKU, st.mrp, lot.BagDescription as LotNo, col1.C1Name as Brand, i.Title, sz.Size1 as Size, cat.ItemCategory, loc.Location, sales.invoiceid, p.Payment_Status, i.Control3 as articel, p.TCS_CGST + p.TCS_IGST + p.TCS_GST as utax, lot.percentage, p.Payable_Amoun, DATEDIFF(m, st.SystemDate, sales.recordtimestamp) as stockAge, DATEDIFF(m, lot.invoiceDate, sales.recordtimestamp) as lotAge, p.pmt_sp_inc_gst as spincgst, p.pmt_sp_ex_gst as spexcgst, p.channel_commsion, p.Channel_Gateway, p.VL_Logistics, p.VLPenalty, (ISNULL(p.Pack_Charges_VL_Misc, 0) + ISNULL(p.Spcl_pack_chrgs_vlMics, 0) + ISNULL(p.Misc_charge, 0)) as misccharges, (p.pmt_scomm_inc_cgst + p.pmt_cgate_inc_cgst + p.pmt_vllogi_inc_cgst + p.pmt_mic1_inc_cgst + p.pmt_mic2_inc_cgst + p.pmt_chnpent_inc_cgst) - (p.pmt_scomm_ex_cgst + p.pmt_cgate_ex_cgst + p.pmt_vllogi_ex_cgst + p.pmt_mic1_ex_cgst + p.pmt_mic2_ex_cgst + p.pmt_chnpent_exc_cgst) as totCGST, (p.pmt_scomm_inc_sgst + p.pmt_cgate_inc_sgst + p.pmt_vllogi_inc_sgst + p.pmt_mic1_inc_sgst + p.pmt_mic2_inc_sgst + p.pmt_chnpent_inc_sgst) - (p.pmt_scomm_ex_sgst + p.pmt_cgate_ex_sgst + p.pmt_vllogi_ex_sgst + p.pmt_mic1_ex_sgst + p.pmt_mic2_ex_cgst + p.pmt_chnpent_exc_sgst) as totSGST, (p.pmt_cgate_inc_gst + p.pmt_scomm_inc_gst + p.pmt_vllogi_inc_gst + p.pmt_mic1_inc_gst + p.pmt_vlpenl_inc_gst + p.pmt_mic1_inc_gst + p.pmt_mic2_inc_gst) - (p.pmt_cgate_ex_gst + p.pmt_scomm_ex_gst + p.pmt_vllogi_ex_gst + p.pmt_mic1_ex_gst + p.pmt_vlpenl_ex_gst + p.pmt_mic1_ex_gst + p.pmt_mic2_ex_gst) as totIGST, (ISNULL(p.mp_commission_cgst, 0) + ISNULL(p.mp_commission_sgst, 0) + ISNULL(p.mp_commission_igst, 0) + ISNULL(p.logistics_cgst, 0) + ISNULL(p.logistics_sgst, 0) + ISNULL(p.logistics_igst, 0)) as totDeduction, st.mrp - (ISNULL(p.mp_commission_cgst, 0) + ISNULL(p.mp_commission_sgst, 0) + ISNULL(p.mp_commission_igst, 0) + ISNULL(p.logistics_cgst, 0) + ISNULL(p.logistics_sgst, 0) + ISNULL(p.logistics_igst, 0)) - 0 as setamt, (st.mrp - (ISNULL(p.mp_commission_cgst, 0) + ISNULL(p.mp_commission_sgst, 0) + ISNULL(p.mp_commission_igst, 0) + ISNULL(p.logistics_cgst, 0) + ISNULL(p.logistics_sgst, 0) + ISNULL(p.logistics_igst, 0))) / NULLIF(st.mrp, 0) * 100 as setper, st.mrp - (ISNULL(p.channel_commsion, 0) + ISNULL(p.Channel_Gateway, 0)) - 0 as netpro, (st.mrp - (ISNULL(p.channel_commsion, 0) + ISNULL(p.Channel_Gateway, 0))) / NULLIF(st.mrp, 0) * 100 as netper, convert(varchar, p.order_date, 104) as pmt_date, CONVERT(CHAR(4), p.order_date, 100) as pmt_Month, CONVERT(CHAR(4), p.order_date, 120) as pmt_year, p.type as pmt_effecting_status, sales.sellingprice as sys_sp, p.sp as pmt_sp, lot.Percentage as cp from(select * from payment_trans where stockupId != -1) p " +
                                   " inner join salesrecord sales on p.salesid = sales.salesidgivenbyvloc and p.vlocid = sales.saleschannelvlocid " +
                                   " inner join ArchiveStockUpInward st on st.StockupID = p.stockupId and st.StockupID = sales.itemid " +
                                   " inner join ItemStyle i on i.StyleID = st.StyleID " +
                                   " inner join Size sz on sz.SizeID = st.SizeID " +
                                   " inner join ItemCategory cat on cat.ItemCategoryID = i.ItemCatID " +
                                   " inner join Lot lot on lot.BagId = st.BagID " +
                                   " left join invoice inv on inv.invid = sales.invoiceid " +
                                   " left join Location loc on loc.LocationID = sales.saleschannelvlocid " +
                                   " left join Column1 col1 on col1.Col1ID = i.Col1 where p.vlocid=@vlocid2 " +
                                   " union all " +
                                   " select p.salesid, st.BarcodeNo, p.stockupId, convert(varchar, st.SystemDate, 104) as date, CONVERT(CHAR(4), st.SystemDate, 100) as Month, CONVERT(CHAR(4), st.SystemDate, 120) as year, sales.Status as sys_status, i.StyleCode, concat(i.StyleCode, '-', sz.Size1) as SKU, st.mrp, lot.BagDescription as LotNo, col1.C1Name as Brand, i.Title, sz.Size1 as Size, cat.ItemCategory, loc.Location, sales.invoiceid, p.Payment_Status, i.Control3 as articel, p.TCS_CGST + p.TCS_IGST + p.TCS_GST as utax,lot.percentage, p.Payable_Amoun, DATEDIFF(m, st.SystemDate, sales.recordtimestamp) as stockAge, DATEDIFF(m, lot.invoiceDate, sales.recordtimestamp) as lotAge, p.pmt_sp_inc_gst as spincgst, p.pmt_sp_ex_gst as spexcgst, p.channel_commsion, p.Channel_Gateway, p.VL_Logistics, p.VLPenalty, (ISNULL(p.Pack_Charges_VL_Misc, 0) + ISNULL(p.Spcl_pack_chrgs_vlMics, 0) + ISNULL(p.Misc_charge, 0)) as misccharges, (p.pmt_scomm_inc_cgst + p.pmt_cgate_inc_cgst + p.pmt_vllogi_inc_cgst + p.pmt_mic1_inc_cgst + p.pmt_mic2_inc_cgst + p.pmt_chnpent_inc_cgst) - (p.pmt_scomm_ex_cgst + p.pmt_cgate_ex_cgst + p.pmt_vllogi_ex_cgst + p.pmt_mic1_ex_cgst + p.pmt_mic2_ex_cgst + p.pmt_chnpent_exc_cgst) as totCGST, (p.pmt_scomm_inc_sgst + p.pmt_cgate_inc_sgst + p.pmt_vllogi_inc_sgst + p.pmt_mic1_inc_sgst + p.pmt_mic2_inc_sgst + p.pmt_chnpent_inc_sgst) - (p.pmt_scomm_ex_sgst + p.pmt_cgate_ex_sgst + p.pmt_vllogi_ex_sgst + p.pmt_mic1_ex_sgst + p.pmt_mic2_ex_cgst + p.pmt_chnpent_exc_sgst) as totSGST, (p.pmt_cgate_inc_gst + p.pmt_scomm_inc_gst + p.pmt_vllogi_inc_gst + p.pmt_mic1_inc_gst + p.pmt_vlpenl_inc_gst + p.pmt_mic1_inc_gst + p.pmt_mic2_inc_gst) - (p.pmt_cgate_ex_gst + p.pmt_scomm_ex_gst + p.pmt_vllogi_ex_gst + p.pmt_mic1_ex_gst + p.pmt_vlpenl_ex_gst + p.pmt_mic1_ex_gst + p.pmt_mic2_ex_gst) as totIGST, (ISNULL(p.mp_commission_cgst, 0) + ISNULL(p.mp_commission_sgst, 0) + ISNULL(p.mp_commission_igst, 0) + ISNULL(p.logistics_cgst, 0) + ISNULL(p.logistics_sgst, 0) + ISNULL(p.logistics_igst, 0)) as totDeduction, st.mrp - (ISNULL(p.mp_commission_cgst, 0) + ISNULL(p.mp_commission_sgst, 0) + ISNULL(p.mp_commission_igst, 0) + ISNULL(p.logistics_cgst, 0) + ISNULL(p.logistics_sgst, 0) + ISNULL(p.logistics_igst, 0)) - 0 as setamt, (st.mrp - (ISNULL(p.mp_commission_cgst, 0) + ISNULL(p.mp_commission_sgst, 0) + ISNULL(p.mp_commission_igst, 0) + ISNULL(p.logistics_cgst, 0) + ISNULL(p.logistics_sgst, 0) + ISNULL(p.logistics_igst, 0))) / NULLIF(st.mrp, 0) * 100 as setper, st.mrp - (ISNULL(p.channel_commsion, 0) + ISNULL(p.Channel_Gateway, 0)) - 0 as netpro, (st.mrp - (ISNULL(p.channel_commsion, 0) + ISNULL(p.Channel_Gateway, 0))) / NULLIF(st.mrp, 0) * 100 as netper, convert(varchar, p.order_date, 104) as pmt_date, CONVERT(CHAR(4), p.order_date, 100) as pmt_Month, CONVERT(CHAR(4), p.order_date, 120) as pmt_year, p.type as pmt_effecting_status, sales.sellingprice as sys_sp, p.sp as pmt_sp, lot.Percentage as cp from(select * from payment_trans where stockupId != -1) p " +
                                   " inner join salesrecord sales on p.salesid = sales.salesidgivenbyvloc and p.vlocid = sales.saleschannelvlocid " +
                                   " inner join ArchiveStockUpInward st on st.StockupID = p.stockupId and st.ArchiveStockupID = sales.archiveid and sales.archiveid != -1 " +
                                   " inner join ItemStyle i on i.StyleID = st.StyleID " +
                                   " inner join Size sz on sz.SizeID = st.SizeID " +
                                   " inner join ItemCategory cat on cat.ItemCategoryID = i.ItemCatID " +
                                   " inner join Lot lot on lot.BagId = st.BagID " +
                                   " left join invoice inv on inv.invid = sales.invoiceid " +
                                   " left join Location loc on loc.LocationID = sales.saleschannelvlocid " +
                                   " left join Column1 col1 on col1.Col1ID = i.Col1  where p.vlocid=@vlocid3 " +
                                   " union all " +
                                   " select p.salesid, st.BarcodeNo, p.stockupId, convert(varchar, st.SystemDate, 104) as date, CONVERT(CHAR(4), st.SystemDate, 100) as Month, CONVERT(CHAR(4), st.SystemDate, 120) as year, sales.Status as sys_status, i.StyleCode, concat(i.StyleCode, '-', sz.Size1) as SKU, st.mrp, lot.BagDescription as LotNo, col1.C1Name as Brand, i.Title, sz.Size1 as Size, cat.ItemCategory, loc.Location, sales.invoiceid, p.Payment_Status, i.Control3 as articel, p.TCS_CGST + p.TCS_IGST + p.TCS_GST as utax, lot.percentage, p.Payable_Amoun, DATEDIFF(m, st.SystemDate, sales.recordtimestamp) as stockAge, DATEDIFF(m, lot.invoiceDate, sales.recordtimestamp) as lotAge, p.pmt_sp_inc_gst as spincgst, p.pmt_sp_ex_gst as spexcgst, p.channel_commsion, p.Channel_Gateway, p.VL_Logistics, p.VLPenalty, (ISNULL(p.Pack_Charges_VL_Misc, 0) + ISNULL(p.Spcl_pack_chrgs_vlMics, 0) + ISNULL(p.Misc_charge, 0)) as misccharges, (p.pmt_scomm_inc_cgst + p.pmt_cgate_inc_cgst + p.pmt_vllogi_inc_cgst + p.pmt_mic1_inc_cgst + p.pmt_mic2_inc_cgst + p.pmt_chnpent_inc_cgst) - (p.pmt_scomm_ex_cgst + p.pmt_cgate_ex_cgst + p.pmt_vllogi_ex_cgst + p.pmt_mic1_ex_cgst + p.pmt_mic2_ex_cgst + p.pmt_chnpent_exc_cgst) as totCGST, (p.pmt_scomm_inc_sgst + p.pmt_cgate_inc_sgst + p.pmt_vllogi_inc_sgst + p.pmt_mic1_inc_sgst + p.pmt_mic2_inc_sgst + p.pmt_chnpent_inc_sgst) - (p.pmt_scomm_ex_sgst + p.pmt_cgate_ex_sgst + p.pmt_vllogi_ex_sgst + p.pmt_mic1_ex_sgst + p.pmt_mic2_ex_cgst + p.pmt_chnpent_exc_sgst) as totSGST, (p.pmt_cgate_inc_gst + p.pmt_scomm_inc_gst + p.pmt_vllogi_inc_gst + p.pmt_mic1_inc_gst + p.pmt_vlpenl_inc_gst + p.pmt_mic1_inc_gst + p.pmt_mic2_inc_gst) - (p.pmt_cgate_ex_gst + p.pmt_scomm_ex_gst + p.pmt_vllogi_ex_gst + p.pmt_mic1_ex_gst + p.pmt_vlpenl_ex_gst + p.pmt_mic1_ex_gst + p.pmt_mic2_ex_gst) as totIGST, (ISNULL(p.mp_commission_cgst, 0) + ISNULL(p.mp_commission_sgst, 0) + ISNULL(p.mp_commission_igst, 0) + ISNULL(p.logistics_cgst, 0) + ISNULL(p.logistics_sgst, 0) + ISNULL(p.logistics_igst, 0)) as totDeduction, st.mrp - (ISNULL(p.mp_commission_cgst, 0) + ISNULL(p.mp_commission_sgst, 0) + ISNULL(p.mp_commission_igst, 0) + ISNULL(p.logistics_cgst, 0) + ISNULL(p.logistics_sgst, 0) + ISNULL(p.logistics_igst, 0)) - 0 as setamt, (st.mrp - (ISNULL(p.mp_commission_cgst, 0) + ISNULL(p.mp_commission_sgst, 0) + ISNULL(p.mp_commission_igst, 0) + ISNULL(p.logistics_cgst, 0) + ISNULL(p.logistics_sgst, 0) + ISNULL(p.logistics_igst, 0))) / NULLIF(st.mrp, 0) * 100 as setper, st.mrp - (ISNULL(p.channel_commsion, 0) + ISNULL(p.Channel_Gateway, 0)) - 0 as netpro, (st.mrp - (ISNULL(p.channel_commsion, 0) + ISNULL(p.Channel_Gateway, 0))) / NULLIF(st.mrp, 0) * 100 as netper, convert(varchar, p.order_date, 104) as pmt_date, CONVERT(CHAR(4), p.order_date, 100) as pmt_Month, CONVERT(CHAR(4), p.order_date, 120) as pmt_year, p.type as pmt_effecting_status, sales.sellingprice as sys_sp, p.sp as pmt_sp, lot.Percentage as cp from(select * from payment_trans where stockupId != -1) p " +
                                   " inner join cancelTrans sales on p.salesid = sales.salesidgivenbyvloc and p.vlocid = sales.saleschannelvlocid " +
                                   " inner join StockUpInward st on st.StockupID = p.stockupId and st.StockupID = sales.itemid and sales.itemid != -1 " +
                                   " inner join ItemStyle i on i.StyleID = st.StyleID " +
                                   " inner join Size sz on sz.SizeID = st.SizeID " +
                                   " inner join ItemCategory cat on cat.ItemCategoryID = i.ItemCatID " +
                                   " inner join Lot lot on lot.BagId = st.BagID " +
                                   " left join invoice inv on inv.invid = sales.invoiceid " +
                                   " left join Location loc on loc.LocationID = sales.saleschannelvlocid " +
                                   " left join Column1 col1 on col1.Col1ID = i.Col1 where p.vlocid=@vlocid4 " +
                                   " union all " +
                                   " select p.salesid, st.BarcodeNo, p.stockupId, convert(varchar, st.SystemDate, 104) as date, CONVERT(CHAR(4), st.SystemDate, 100) as Month, CONVERT(CHAR(4), st.SystemDate, 120) as year, sales.Status as sys_status, i.StyleCode, concat(i.StyleCode, '-', sz.Size1) as SKU, st.mrp, lot.BagDescription as LotNo, col1.C1Name as Brand, i.Title, sz.Size1 as Size, cat.ItemCategory, loc.Location, sales.invoiceid, p.Payment_Status, i.Control3 as articel, p.TCS_CGST + p.TCS_IGST + p.TCS_GST as utax, lot.percentage, p.Payable_Amoun, DATEDIFF(m, st.SystemDate, sales.recordtimestamp) as stockAge, DATEDIFF(m, lot.invoiceDate, sales.recordtimestamp) as lotAge, p.pmt_sp_inc_gst as spincgst, p.pmt_sp_ex_gst as spexcgst, p.channel_commsion, p.Channel_Gateway, p.VL_Logistics, p.VLPenalty, (ISNULL(p.Pack_Charges_VL_Misc, 0) + ISNULL(p.Spcl_pack_chrgs_vlMics, 0) + ISNULL(p.Misc_charge, 0)) as misccharges, (p.pmt_scomm_inc_cgst + p.pmt_cgate_inc_cgst + p.pmt_vllogi_inc_cgst + p.pmt_mic1_inc_cgst + p.pmt_mic2_inc_cgst + p.pmt_chnpent_inc_cgst) - (p.pmt_scomm_ex_cgst + p.pmt_cgate_ex_cgst + p.pmt_vllogi_ex_cgst + p.pmt_mic1_ex_cgst + p.pmt_mic2_ex_cgst + p.pmt_chnpent_exc_cgst) as totCGST, (p.pmt_scomm_inc_sgst + p.pmt_cgate_inc_sgst + p.pmt_vllogi_inc_sgst + p.pmt_mic1_inc_sgst + p.pmt_mic2_inc_sgst + p.pmt_chnpent_inc_sgst) - (p.pmt_scomm_ex_sgst + p.pmt_cgate_ex_sgst + p.pmt_vllogi_ex_sgst + p.pmt_mic1_ex_sgst + p.pmt_mic2_ex_cgst + p.pmt_chnpent_exc_sgst) as totSGST, (p.pmt_cgate_inc_gst + p.pmt_scomm_inc_gst + p.pmt_vllogi_inc_gst + p.pmt_mic1_inc_gst + p.pmt_vlpenl_inc_gst + p.pmt_mic1_inc_gst + p.pmt_mic2_inc_gst) - (p.pmt_cgate_ex_gst + p.pmt_scomm_ex_gst + p.pmt_vllogi_ex_gst + p.pmt_mic1_ex_gst + p.pmt_vlpenl_ex_gst + p.pmt_mic1_ex_gst + p.pmt_mic2_ex_gst) as totIGST, (ISNULL(p.mp_commission_cgst, 0) + ISNULL(p.mp_commission_sgst, 0) + ISNULL(p.mp_commission_igst, 0) + ISNULL(p.logistics_cgst, 0) + ISNULL(p.logistics_sgst, 0) + ISNULL(p.logistics_igst, 0)) as totDeduction, st.mrp - (ISNULL(p.mp_commission_cgst, 0) + ISNULL(p.mp_commission_sgst, 0) + ISNULL(p.mp_commission_igst, 0) + ISNULL(p.logistics_cgst, 0) + ISNULL(p.logistics_sgst, 0) + ISNULL(p.logistics_igst, 0)) - 0 as setamt, (st.mrp - (ISNULL(p.mp_commission_cgst, 0) + ISNULL(p.mp_commission_sgst, 0) + ISNULL(p.mp_commission_igst, 0) + ISNULL(p.logistics_cgst, 0) + ISNULL(p.logistics_sgst, 0) + ISNULL(p.logistics_igst, 0))) / NULLIF(st.mrp, 0) * 100 as setper, st.mrp - (ISNULL(p.channel_commsion, 0) + ISNULL(p.Channel_Gateway, 0)) - 0 as netpro, (st.mrp - (ISNULL(p.channel_commsion, 0) + ISNULL(p.Channel_Gateway, 0))) / NULLIF(st.mrp, 0) * 100 as netper, convert(varchar, p.order_date, 104) as pmt_date, CONVERT(CHAR(4), p.order_date, 100) as pmt_Month, CONVERT(CHAR(4), p.order_date, 120) as pmt_year, p.type as pmt_effecting_status, sales.sellingprice as sys_sp, p.sp as pmt_sp, lot.Percentage as cp from(select * from payment_trans where stockupId != -1) p " +
                                   " inner join cancelTrans sales on p.salesid = sales.salesidgivenbyvloc and p.vlocid = sales.saleschannelvlocid " +
                                   " inner join ArchiveStockUpInward st on st.StockupID = p.stockupId and st.StockupID = sales.itemid " +
                                   " inner join ItemStyle i on i.StyleID = st.StyleID " +
                                   " inner join Size sz on sz.SizeID = st.SizeID " +
                                   " inner join ItemCategory cat on cat.ItemCategoryID = i.ItemCatID " +
                                   " inner join Lot lot on lot.BagId = st.BagID " +
                                   " left join invoice inv on inv.invid = sales.invoiceid " +
                                   " left join Location loc on loc.LocationID = sales.saleschannelvlocid " +
                                   " left join Column1 col1 on col1.Col1ID = i.Col1  where p.vlocid=@vlocid5 " +
                                   " union all " +
                                   " select p.salesid, st.BarcodeNo, p.stockupId, convert(varchar, st.SystemDate, 104) as date, CONVERT(CHAR(4), st.SystemDate, 100) as Month, CONVERT(CHAR(4), st.SystemDate, 120) as year, sales.Status as sys_status, i.StyleCode, concat(i.StyleCode, '-', sz.Size1) as SKU, st.mrp, lot.BagDescription as LotNo, col1.C1Name as Brand, i.Title, sz.Size1 as Size, cat.ItemCategory, loc.Location, sales.invoiceid, p.Payment_Status, i.Control3 as articel, p.TCS_CGST + p.TCS_IGST + p.TCS_GST as utax, lot.percentage, p.Payable_Amoun, DATEDIFF(m, st.SystemDate, sales.recordtimestamp) as stockAge, DATEDIFF(m, lot.invoiceDate, sales.recordtimestamp) as lotAge, p.pmt_sp_inc_gst as spincgst, p.pmt_sp_ex_gst as spexcgst, p.channel_commsion, p.Channel_Gateway, p.VL_Logistics, p.VLPenalty, (ISNULL(p.Pack_Charges_VL_Misc, 0) + ISNULL(p.Spcl_pack_chrgs_vlMics, 0) + ISNULL(p.Misc_charge, 0)) as misccharges, (p.pmt_scomm_inc_cgst + p.pmt_cgate_inc_cgst + p.pmt_vllogi_inc_cgst + p.pmt_mic1_inc_cgst + p.pmt_mic2_inc_cgst + p.pmt_chnpent_inc_cgst) - (p.pmt_scomm_ex_cgst + p.pmt_cgate_ex_cgst + p.pmt_vllogi_ex_cgst + p.pmt_mic1_ex_cgst + p.pmt_mic2_ex_cgst + p.pmt_chnpent_exc_cgst) as totCGST, (p.pmt_scomm_inc_sgst + p.pmt_cgate_inc_sgst + p.pmt_vllogi_inc_sgst + p.pmt_mic1_inc_sgst + p.pmt_mic2_inc_sgst + p.pmt_chnpent_inc_sgst) - (p.pmt_scomm_ex_sgst + p.pmt_cgate_ex_sgst + p.pmt_vllogi_ex_sgst + p.pmt_mic1_ex_sgst + p.pmt_mic2_ex_cgst + p.pmt_chnpent_exc_sgst) as totSGST, (p.pmt_cgate_inc_gst + p.pmt_scomm_inc_gst + p.pmt_vllogi_inc_gst + p.pmt_mic1_inc_gst + p.pmt_vlpenl_inc_gst + p.pmt_mic1_inc_gst + p.pmt_mic2_inc_gst) - (p.pmt_cgate_ex_gst + p.pmt_scomm_ex_gst + p.pmt_vllogi_ex_gst + p.pmt_mic1_ex_gst + p.pmt_vlpenl_ex_gst + p.pmt_mic1_ex_gst + p.pmt_mic2_ex_gst) as totIGST, (ISNULL(p.mp_commission_cgst, 0) + ISNULL(p.mp_commission_sgst, 0) + ISNULL(p.mp_commission_igst, 0) + ISNULL(p.logistics_cgst, 0) + ISNULL(p.logistics_sgst, 0) + ISNULL(p.logistics_igst, 0)) as totDeduction, st.mrp - (ISNULL(p.mp_commission_cgst, 0) + ISNULL(p.mp_commission_sgst, 0) + ISNULL(p.mp_commission_igst, 0) + ISNULL(p.logistics_cgst, 0) + ISNULL(p.logistics_sgst, 0) + ISNULL(p.logistics_igst, 0)) - 0 as setamt, (st.mrp - (ISNULL(p.mp_commission_cgst, 0) + ISNULL(p.mp_commission_sgst, 0) + ISNULL(p.mp_commission_igst, 0) + ISNULL(p.logistics_cgst, 0) + ISNULL(p.logistics_sgst, 0) + ISNULL(p.logistics_igst, 0))) / NULLIF(st.mrp, 0) * 100 as setper, st.mrp - (ISNULL(p.channel_commsion, 0) + ISNULL(p.Channel_Gateway, 0)) - 0 as netpro, (st.mrp - (ISNULL(p.channel_commsion, 0) + ISNULL(p.Channel_Gateway, 0))) / NULLIF(st.mrp, 0) * 100 as netper, convert(varchar, p.order_date, 104) as pmt_date, CONVERT(CHAR(4), p.order_date, 100) as pmt_Month, CONVERT(CHAR(4), p.order_date, 120) as pmt_year, p.type as pmt_effecting_status, sales.sellingprice as sys_sp, p.sp as pmt_sp, lot.Percentage as cp from(select * from payment_trans where stockupId != -1) p " +
                                   " inner join cancelTrans sales on p.salesid = sales.salesidgivenbyvloc and p.vlocid = sales.saleschannelvlocid " +
                                   " inner join ArchiveStockUpInward st on st.StockupID = p.stockupId and st.ArchiveStockupID = sales.archiveid and sales.archiveid != -1 " +
                                   " inner join ItemStyle i on i.StyleID = st.StyleID " +
                                   " inner join Size sz on sz.SizeID = st.SizeID " +
                                   " inner join ItemCategory cat on cat.ItemCategoryID = i.ItemCatID " +
                                   " inner join Lot lot on lot.BagId = st.BagID " +
                                   " left join invoice inv on inv.invid = sales.invoiceid " +
                                   " left join Location loc on loc.LocationID = sales.saleschannelvlocid " +
                                   " left join Column1 col1 on col1.Col1ID = i.Col1 where p.vlocid=@vlocid6 " +
                                   " ) newTable ";
            command.Parameters.AddWithValue("@vlocid1", vlocid);
            command.Parameters.AddWithValue("@vlocid2", vlocid);
            command.Parameters.AddWithValue("@vlocid3", vlocid);
            command.Parameters.AddWithValue("@vlocid4", vlocid);
            command.Parameters.AddWithValue("@vlocid5", vlocid);
            command.Parameters.AddWithValue("@vlocid6", vlocid);
            Table.Load(command.ExecuteReader());
            command.Parameters.Clear();

            DataTable dtsecond = new DataTable();
            command.CommandText= "select p.salesid, null as BarcodeNo, p.stockupId,null as date, null as Month, null as year, null as sys_status, null as StyleCode, null as SKU, "+
                                 "null as mrp, null as LotNo, null as Brand, null as Title, null as Size, null as ItemCategory , null as Location, null as invoiceid, p.Payment_Status,null as articel,p.TCS_CGST + p.TCS_IGST + p.TCS_GST as utax,p.percentage,p.Payable_Amoun,  "+
                                 "null as stockAge, null aslotAge,p.pmt_sp_inc_gst as spincgst,p.pmt_sp_ex_gst as spexcgst,  " +
                                 " p.channel_commsion, p.Channel_Gateway, p.VL_Logistics, p.VLPenalty, (ISNULL(p.Pack_Charges_VL_Misc, 0) + ISNULL(p.Spcl_pack_chrgs_vlMics, 0) + ISNULL(p.Misc_charge, 0)) as misccharges,  " +
                                 "(abs(p.mp_commission_cgst)+abs(p.pg_commission_cgst)+abs(p.logistics_cgst)+abs(p.TCS_CGST)) as  totCGST,  " +
                                 "(abs(p.mp_commission_sgst)+abs(p.pg_commission_sgst)+abs(p.logistics_sgst)+abs(+p.TCS_GST)) as totSGST,  " +
                                 "(abs(p.mp_commission_igst)+abs(p.pg_commission_igst)+abs(p.logistics_igst)+abs(p.TCS_IGST)) as totIGST,  " +
                                  "(ISNULL(p.mp_commission_cgst, 0) + ISNULL(p.mp_commission_sgst, 0) + ISNULL(p.mp_commission_igst, 0) + ISNULL(p.logistics_cgst, 0) + ISNULL(p.logistics_sgst, 0) + ISNULL(p.logistics_igst, 0)) as totDeduction,  " +
                                  "null as setamt, null as setper, null as netpro,null asnetper, convert(varchar, p.order_date, 104) as pmt_date,CONVERT(CHAR(4), p.order_date, 100) as pmt_Month , CONVERT(CHAR(4), p.order_date, 120) as pmt_year,p.type as pmt_effecting_status,null as sys_sp,p.sp as pmt_sp,null as cp  "+
                                  " from payment_trans p where p.stockupId = -2 and p.vlocid=@vlocid ";
            command.Parameters.AddWithValue("@vlocid", vlocid);
            dtsecond.Load(command.ExecuteReader());
            DataTable dtnew = Table.Clone();
            foreach (DataRow dr in dtsecond.Rows)
            {
                DataRow row = dtnew.NewRow();
                row["salesid"] = dr["salesid"];//
                row["BarcodeNo"] = dr["BarcodeNo"];
                row["stockupId"] = dr["stockupId"];
                row["date"] = dr["date"];//
                row["Month"] = dr["Month"];//
                row["year"] = dr["year"];//
                row["sys_status"] = "NA";//
                row["StyleCode"] = dr["StyleCode"];//
                row["SKU"] = "NA";//
                row["mrp"] = dr["mrp"];//
                row["LotNo"] = dr["LotNo"];//
                row["Brand"] = dr["Brand"];
                row["Title"] = dr["Title"];//
                row["Size"] = dr["Size"];//
                row["ItemCategory"] = dr["ItemCategory"]; //
                row["Location"] = dr["Location"];//
                row["invoiceid"] = 0;//
                row["Payment_Status"] = dr["Payment_Status"];//
                row["articel"] = dr["articel"];//
                row["utax"] = dr["utax"];//
                row["percentage"] = dr["percentage"];//
                row["Payable_Amoun"] =dr["Payable_Amoun"];//
                row["channel_commsion"] = dr["channel_commsion"];//
                row["Channel_Gateway"] =dr["Channel_Gateway"];//
                row["VL_Logistics"] =dr["VL_Logistics"];//
                row["VLPenalty"] =dr["VLPenalty"];//
                row["misccharges"] =dr["misccharges"];//
                row["totCGST"] =dr["totCGST"];//
                row["totSGST"] =dr["totSGST"];//
                row["totIGST"] =dr["totIGST"];//
                row["totDeduction"] = dr["totDeduction"];//
                row["setamt"] = dr["setamt"];
                row["setper"] = dr["setper"];//
                row["netpro"] = DBNull.Value;//
                row["netper"] = DBNull.Value;//
                row["pmt_date"] = dr["pmt_date"];//
                row["pmt_Month"] = dr["pmt_Month"];//
                row["pmt_year"] = dr["pmt_year"]; ;
                row["pmt_effecting_status"] = dr["pmt_effecting_status"];
                row["sys_sp"] = dr["sys_sp"];//
                row["pmt_sp"] = dr["pmt_sp"];//
                row["cp"] = "-1";



                dtnew.Rows.Add(row);
               
            }
            finaldt = Table.Copy();
            finaldt.Merge(dtnew);


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
        return finaldt;
    }
    //public DataTable BindReturn(string type)
    //{
    //    DataTable tab = new DataTable();
    //    DataTable Table = new DataTable();
    //    string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
    //    SqlConnection connection = new SqlConnection(connectionString);
    //    if (connection.State != ConnectionState.Open)
    //    {
    //        connection.Open();
    //    }

    //    SqlCommand command = connection.CreateCommand();
    //    SqlTransaction transaction;

    //    // Start a local transaction.
    //    transaction = connection.BeginTransaction("BindReturn");
    //    command.Connection = connection;
    //    command.Transaction = transaction;
    //    try
    //    {
           
    //        tab.Columns.Add("Date",typeof(string));
    //        tab.Columns.Add("Month", typeof(string));
    //        tab.Columns.Add("Year", typeof(string));
    //        tab.Columns.Add("Channel", typeof(string));
    //        tab.Columns.Add("TotalOrder", typeof(decimal));
    //        tab.Columns.Add("ReturnOrder", typeof(decimal));
    //        tab.Columns.Add("Amount Charged", typeof(decimal));

    //        if (type.Equals("Date"))
    //        {
    //            //command.CommandText = "select  convert(varchar, p.order_date, 104) as date,CONVERT(CHAR(4), p.order_date, 100) as Month , CONVERT(CHAR(4), p.order_date, 120)  as year,count(p.Pt_id) as totalreturn,l.Location,sum(p.Payable_Amoun) as amount from payment_trans p left join salesrecord s on s.salesidgivenbyvloc=p.salesid left join Location l on l.LocationID=p.vlocid where s.status='RETURN' and p.stockupId!=-1 group by p.order_date,l.Location ";
    //            command.CommandText = "select * from " +
    //                                  "(select p.order_date, " +
    //                                  "convert(varchar, p.order_date, 104) as date, CONVERT(CHAR(4), p.order_date, 100) as Month, CONVERT(CHAR(4), p.order_date, 120) as year, count(p.Pt_id) as totalreturn, l.Location, sum(p.Payable_Amoun) as amount " +
    //                                  "from payment_trans p inner join salesrecord s on s.salesidgivenbyvloc = p.salesid and s.saleschannelvlocid = p.vlocid " +
    //                                  "left join Location l on l.LocationID = p.vlocid " +
    //                                  "where s.status = 'RETURN' and p.stockupId != -1 group by p.order_date, l.Location " +
    //                                  "union all " +
    //                                  "select p.order_date, " +
    //                                  "convert(varchar, p.order_date, 104) as date, CONVERT(CHAR(4), p.order_date, 100) as Month, CONVERT(CHAR(4), p.order_date, 120) as year, count(p.Pt_id) as totalreturn, l.Location, sum(p.Payable_Amoun) as amount " +
    //                                  "from payment_trans p " +
    //                                  "inner join cancelTrans c on c.saleschannelvlocid = p.vlocid and c.salesidgivenbyvloc = p.salesid " +
    //                                  "left join Location l on l.LocationID = c.saleschannelvlocid and p.vlocid = l.LocationID " +
    //                                  "where  c.status = 'RETURN' and p.stockupId != -1 group by p.order_date, l.Location) y ";

    //            Table.Load(command.ExecuteReader());

    //            foreach (DataRow dr in Table.Rows)
    //            {
    //                DataTable exce = new DataTable();
    //                command.CommandText = "select Distinct count(Pt_id) as cnt,sum(p.Payable_Amoun) as pay from payment_trans p inner join cancelTrans s on s.saleschannelvlocid=p.vlocid and s.salesidgivenbyvloc=p.salesid left join Location l on l.LocationID=p.vlocid  where p.order_date=@order_date1  and l.Location=@Location1  " +
    //                                       " union all " +
    //                                       "select Distinct  count(Pt_id) as cnt,sum(p.Payable_Amoun) as pay from payment_trans p inner join salesrecord s on s.saleschannelvlocid=p.vlocid and s.salesidgivenbyvloc=p.salesid left join Location l on l.LocationID=p.vlocid  where p.order_date=@order_date2  and l.Location=@Location2 ";
    //                command.Parameters.AddWithValue("@Location1", dr["Location"]);
    //                command.Parameters.AddWithValue("@Location2", dr["Location"]);
    //                command.Parameters.AddWithValue("@order_date1", dr["order_date"]);
    //                command.Parameters.AddWithValue("@order_date2", dr["order_date"]);
    //                exce.Load(command.ExecuteReader());
    //                command.Parameters.Clear();

    //                object count = exce.Compute("Sum(cnt)", string.Empty);
                    
    //                DataRow row = tab.NewRow();


    //                row["Date"] = dr["date"];
    //                row["Month"] = dr["Month"];
    //                row["Year"] = dr["year"];
    //                row["Channel"] = dr["Location"];
    //                row["TotalOrder"] = count.ToString();
    //                row["ReturnOrder"] = dr["totalreturn"];
    //                row["Amount Charged"] = dr["amount"];
    //                tab.Rows.Add(row);
    //            }
    //        }
    //        else if(type.Equals("Month"))
    //        {
    //            command.CommandText = "select sum(Amount_charged) as Amount_charged ,sum(totalreturn) as totalreturn,u.YEAR,u.Month,u.MMM,u.location from( " +
    //                                  "SELECT YEAR = YEAR(order_date), Month = Month(order_date), MMM = UPPER(left(DATENAME(MONTH, order_date), 3)), Amount_charged = convert(decimal, floor(sum(Payable_Amoun)), 1), totalreturn = count(* ), l.Location  FROM     payment_trans p inner join salesrecord s on s.salesidgivenbyvloc = p.salesid and s.saleschannelvlocid = p.vlocid    left join Location l on l.LocationID = p.vlocid  where s.status = 'RETURN' and p.stockupId != -1   GROUP BY YEAR(order_date), Month(order_date), DATENAME(MONTH, order_date), l.Location " +
    //                                  "union all " +
    //                                  "SELECT YEAR = YEAR(order_date), Month = Month(order_date), MMM = UPPER(left(DATENAME(MONTH, order_date), 3)), Amount_charged = convert(decimal, floor(sum(Payable_Amoun)), 1), totalreturn = count(* ), l.Location  FROM     payment_trans p inner join cancelTrans s on s.salesidgivenbyvloc = p.salesid and s.saleschannelvlocid = p.vlocid    left join Location l on l.LocationID = p.vlocid  where s.status = 'RETURN' and p.stockupId != -1   GROUP BY YEAR(order_date), Month(order_date), DATENAME(MONTH, order_date), l.Location) u   group by u.YEAR,u.Month,u.MMM,u.location order by u.YEAR";

    //            Table.Load(command.ExecuteReader());
    //            foreach(DataRow dr in Table.Rows)
    //            {
    //                DataTable exce = new DataTable();
    //                command.CommandText = "select * from( " +
    //                                     "SELECT YEAR = YEAR(order_date), Month = Month(order_date), MMM = UPPER(left(DATENAME(MONTH, order_date), 3)), Amount_charged = convert(VARCHAR, floor(sum(Payable_Amoun)), 1), total= count(* ), l.Location  FROM     payment_trans p inner join salesrecord s on s.salesidgivenbyvloc = p.salesid and s.saleschannelvlocid = p.vlocid    left join Location l on l.LocationID = p.vlocid  where  p.stockupId != -1 and Month(order_date) =@Month1 and YEAR(order_date)=@YEAR1 and l.Location =@Location1  GROUP BY YEAR(order_date), Month(order_date), DATENAME(MONTH, order_date), l.Location " +
    //                                     "union all " +
    //                                     "SELECT YEAR = YEAR(order_date), Month = Month(order_date), MMM = UPPER(left(DATENAME(MONTH, order_date), 3)), Amount_charged = convert(VARCHAR, floor(sum(Payable_Amoun)), 1), total= count(* ), l.Location  FROM     payment_trans p inner join cancelTrans s on s.salesidgivenbyvloc = p.salesid and s.saleschannelvlocid = p.vlocid    left join Location l on l.LocationID = p.vlocid  where  p.stockupId != -1  and Month(order_date) =@Month2 and YEAR(order_date)=@YEAR2 and l.Location =@Location2 GROUP BY YEAR(order_date), Month(order_date), DATENAME(MONTH, order_date), l.Location) u ";
    //                command.Parameters.AddWithValue("@Location1", dr["Location"]);
    //                command.Parameters.AddWithValue("@Location2", dr["Location"]);
    //                command.Parameters.AddWithValue("@Month1", dr["Month"]);
    //                command.Parameters.AddWithValue("@Month2", dr["Month"]);
    //                command.Parameters.AddWithValue("@YEAR1", dr["YEAR"]);
    //                command.Parameters.AddWithValue("@YEAR2", dr["YEAR"]);
    //                exce.Load(command.ExecuteReader());
    //                command.Parameters.Clear();

    //                object count = exce.Compute("Sum(total)", string.Empty);
                    
                    
    //                DataRow row = tab.NewRow();


    //                row["Date"] = "";
    //                row["Month"] = dr["MMM"];
    //                row["Year"] = dr["YEAR"];
    //                row["Channel"] = dr["Location"];
    //                row["TotalOrder"] = count.ToString();
    //                row["ReturnOrder"] = dr["totalreturn"];
    //                row["Amount Charged"] = dr["Amount_charged"];
    //                tab.Rows.Add(row);
    //            }
    //        }





    //        transaction.Commit();
    //        if (connection.State == ConnectionState.Open)
    //            connection.Close();
    //        return tab;
    //    }
    //    catch (Exception ex)
    //    {
    //        if (connection.State == ConnectionState.Open)
    //            connection.Close();
    //        RecordExceptionCls rec = new RecordExceptionCls();
    //        rec.recordException(ex);
    //        return null;
    //    }
       
    //}
    public DataTable BindConsoledate(string vlocid)
    {
        DataTable Table = new DataTable();
        DataTable finaldt = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("BindConsoledate");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            /*command.CommandText = " select distinct p.[salesid],convert(varchar, arc.SystemDate, 104) as sys_date,CONVERT(CHAR(4), arc.SystemDate, 100) as sys_Month , CONVERT(CHAR(4), arc.SystemDate, 120)  as sys_year,  i.Title,cast(((arc.mrp*isnull(lot.Percentage,0))/100) as decimal(10,2)) as cp,i.Control3 as Articel,  sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,sales.status as System_status,sales.invoiceid,l.Location,p.stockupId,DATEDIFF(m, arc.SystemDate, sales.recordtimestamp) as stockAge, DATEDIFF(m, lot.invoiceDate, sales.recordtimestamp) as lotAge, arc.mrp ,sales.sellingprice as sys_sp ,STUFF((SELECT distinct ', ' + convert(varchar, n.order_date, 104)          from payment_trans n          where p.[salesid] = n.[salesid] and  n.[stockupId]!=-1 and n.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_date ,STUFF((SELECT distinct ', ' + cast(sp.sp as NVARCHAR(MAX))          from payment_trans sp          where p.[salesid] = sp.[salesid] and  sp.[stockupId]!=-1 and  sp.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_sp,   STUFF((SELECT distinct ', ' +cast(s.sellingprice as NVARCHAR(MAX))          from payment_trans t1          inner join salesrecord s on s.salesidgivenbyvloc=t1.salesid and s.saleschannelvlocid=t1.vlocid and s.itemid!=-1          where p.[salesid] = t1.[salesid] and t1.[stockupId]!=-1 and t1.[stockupId]!=-2              FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,2,'') sm,        STUFF((SELECT distinct ', ' + cast(pybamt.Payable_Amoun as NVARCHAR(MAX))          from payment_trans pybamt          where p.[salesid] = pybamt.[salesid] and pybamt.[stockupId]!=-1  and pybamt.[stockupId]!=-2              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') Payableamt,  STUFF((SELECT distinct ', ' + t7.type          from payment_trans t7          where p.[salesid] = t7.[salesid] and t7.[stockupId]!=-1 and t7.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_effecting_status,   STUFF((SELECT distinct ', ' + t2.Merchant_SKU          from payment_trans t2          where p.[salesid] = t2.[salesid] and t2.[stockupId]!=-1 and t2.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') sku ,   STUFF((SELECT distinct ', ' + tp.Payment_Status           from payment_trans tp          where p.[salesid] = tp.[salesid] and tp.[stockupId]!=-1             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') payment_status,       (SELECT distinct sum(chn.channel_commsion)          from payment_trans chn          where p.[salesid] = chn.[salesid] and  chn.[stockupId]!=-1 and  chn.[stockupId]!=-2            ) chennelcommision,       (SELECT distinct sum(gate.Channel_Gateway)          from payment_trans gate          where p.[salesid] = gate.[salesid] and gate.[stockupId]!=-1 and gate.[stockupId]!=-2            ) gatewaycommission,          (SELECT distinct sum(logic.VL_Logistics)          from payment_trans logic          where p.[salesid] = logic.[salesid] and logic.[stockupId]!=-1 and logic.[stockupId]!=-2            ) logistic,          (SELECT distinct sum(pent.VLPenalty)          from payment_trans pent          where p.[salesid] = pent.[salesid] and pent.[stockupId]!=-1 and pent.[stockupId]!=-2            ) Vlpenelty,             (SELECT distinct sum((k.pmt_cgate_inc_gst+k.pmt_scomm_inc_gst+k.pmt_vllogi_inc_gst+k.pmt_vlpenl_inc_gst+k.pmt_mic1_inc_gst+k.pmt_mic2_inc_gst)-(k.pmt_cgate_ex_gst+k.pmt_scomm_ex_gst+k.pmt_vllogi_ex_gst+k.pmt_vlpenl_ex_gst+k.pmt_mic1_ex_gst+k.pmt_mic2_ex_gst) )          from payment_trans k          where p.[salesid] = k.[salesid] and  k.[stockupId]!=-1 and  k.[stockupId]!=-2            ) IGST,             (SELECT distinct sum(k3.Payable_Amoun)          from payment_trans k3          where p.[salesid] = k3.[salesid] and  k3.[stockupId]!=-1 and  k3.[stockupId]!=-2            ) totaldec,             (SELECT distinct sum((k4.pmt_scomm_inc_cgst+k4.pmt_cgate_inc_cgst+k4.pmt_vllogi_inc_cgst+k4.pmt_mic1_inc_cgst+k4.pmt_mic2_inc_cgst+k4.pmt_chnpent_inc_cgst+k4.pmt_chnpent_inc_cgst)-(k4.pmt_scomm_ex_cgst+k4.pmt_cgate_ex_cgst+k4.pmt_vllogi_ex_cgst+k4.pmt_mic1_ex_cgst+k4.pmt_mic2_ex_cgst+k4.pmt_chnpent_exc_cgst+k4.pmt_chnpent_exc_cgst) )          from payment_trans k4          where p.[salesid] = k4.[salesid] and  k4.[stockupId]!=-1 and  k4.[stockupId]!=-2            ) CGST , (SELECT distinct sum(x.Misc_charge) from payment_trans x  where p.[salesid] = x.[salesid] and x.[stockupId]!=-1 AND x.stockupId!=-2 ) Mis ,   ( SELECT distinct sum((k5.pmt_scomm_inc_sgst+k5.pmt_cgate_inc_sgst+k5.pmt_vllogi_inc_sgst+k5.pmt_mic1_inc_sgst+k5.pmt_mic2_inc_sgst+k5.pmt_chnpent_inc_sgst+k5.pmt_chnpent_inc_sgst)-(k5.pmt_scomm_ex_sgst+k5.pmt_cgate_ex_sgst+k5.pmt_vllogi_ex_sgst+k5.pmt_mic1_ex_sgst+k5.pmt_mic2_ex_cgst+k5.pmt_chnpent_exc_sgst+k5.pmt_chnpent_exc_sgst) )          from payment_trans k5  where p.[salesid] = k5.[salesid] and  k5.[stockupId]!=-1  and  k5.[stockupId]!=-2 ) SGST,h.ligst,h.higst,h.lowhighpt, (SELECT distinct sum(g1.TCS_IGST) from payment_trans g1  where p.[salesid] = g1.[salesid] and g1.[stockupId]!=-1 AND g1.stockupId!=-2 ) TCS_IGST, (SELECT distinct sum(g2.TCS_CGST) from payment_trans g2  where p.[salesid] = g2.[salesid] and g2.[stockupId]!=-1 AND g2.stockupId!=-2 ) TCS_CGST, (SELECT distinct sum(g3.TCS_GST) from payment_trans g3  where p.[salesid] = g3.[salesid] and g3.[stockupId]!=-1 AND g3.stockupId!=-2 ) TCS_SGST, (SELECT distinct count(cunt.Pt_id) from payment_trans cunt  where p.[salesid] = cunt.[salesid] and cunt.[stockupId]!=-1 AND cunt.stockupId!=-2 ) cunt         from payment_trans p         inner join StockUpInward arc on arc.StockupID=p.stockupId          inner join salesrecord sales on p.salesid= sales.salesidgivenbyvloc and p.vlocid= sales.saleschannelvlocid and sales.itemid!=-1            left join Location l on l.LocationID= p.vlocid            inner join ItemStyle i on i.StyleID = arc.StyleID          inner join Size sz on sz.SizeID= arc.SizeID          inner join Lot lot on lot.BagId= arc.BagID          left join Column1 col1 on col1.Col1ID= i.Col1 inner join ItemCategory cat on cat.ItemCategoryID=i.ItemCatID inner join  hsnmaster h on h.hsnid = cat.hsnid           where p.stockupId!=-1 and p.stockupId!=-2  " +
          "union all  " +
          "select distinct p.[salesid],convert(varchar, arc.SystemDate, 104) as sys_date,CONVERT(CHAR(4), arc.SystemDate, 100) as sys_Month , CONVERT(CHAR(4), arc.SystemDate, 120)  as sys_year,  i.Title,cast(((arc.mrp*isnull(lot.Percentage,0))/100) as decimal(10,2)) as cp, i.Control3 as Articel, sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,sales.status as System_status,sales.invoiceid,l.Location,p.stockupId,DATEDIFF(m, arc.SystemDate, sales.recordtimestamp) as stockAge, DATEDIFF(m, lot.invoiceDate, sales.recordtimestamp) as lotAge, arc.mrp ,sales.sellingprice as sys_sp ,STUFF((SELECT distinct ', ' + convert(varchar, n.order_date, 104)          from payment_trans n          where p.[salesid] = n.[salesid] and  n.[stockupId]!=-1 and  n.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_date ,STUFF((SELECT distinct ', ' + cast(sp.sp as NVARCHAR(MAX))          from payment_trans sp          where p.[salesid] = sp.[salesid] and  sp.[stockupId]!=-1 and  sp.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_sp,   STUFF((SELECT distinct ', ' +cast(s.sellingprice as NVARCHAR(MAX))          from payment_trans t1          inner join salesrecord s on s.salesidgivenbyvloc=t1.salesid and s.saleschannelvlocid=t1.vlocid and s.itemid!=-1          where p.[salesid] = t1.[salesid] and t1.[stockupId]!=-1 and t1.[stockupId]!=-2               FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,2,'') sm,        STUFF((SELECT distinct ', ' + cast(pybamt.Payable_Amoun as NVARCHAR(MAX))          from payment_trans pybamt          where p.[salesid] = pybamt.[salesid] and pybamt.[stockupId]!=-1  and pybamt.[stockupId]!=-2              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') Payableamt,  STUFF((SELECT distinct ', ' + t7.type          from payment_trans t7          where p.[salesid] = t7.[salesid] and t7.[stockupId]!=-1 and t7.[stockupId]!=-2              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_effecting_status,   STUFF((SELECT distinct ', ' + t2.Merchant_SKU          from payment_trans t2          where p.[salesid] = t2.[salesid] and t2.[stockupId]!=-1 and t2.[stockupId]!=-2              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') sku ,   STUFF((SELECT distinct ', ' + tp.Payment_Status           from payment_trans tp          where p.[salesid] = tp.[salesid] and tp.[stockupId]!=-1 and tp.[stockupId]!=-2              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') payment_status,       (SELECT distinct sum(chn.channel_commsion)          from payment_trans chn          where p.[salesid] = chn.[salesid] and  chn.[stockupId]!=-1 and  chn.[stockupId]!=-2            ) chennelcommision,       (SELECT distinct sum(gate.Channel_Gateway)          from payment_trans gate          where p.[salesid] = gate.[salesid] and gate.[stockupId]!=-1 and gate.[stockupId]!=-2             ) gatewaycommission,          (SELECT distinct sum(logic.VL_Logistics)          from payment_trans logic          where p.[salesid] = logic.[salesid] and logic.[stockupId]!=-1 and logic.[stockupId]!=-2             ) logistic,          (SELECT distinct sum(pent.VLPenalty)          from payment_trans pent          where p.[salesid] = pent.[salesid] and pent.[stockupId]!=-1 and pent.[stockupId]!=-2             ) Vlpenelty,             (SELECT distinct sum((k.pmt_cgate_inc_gst+k.pmt_scomm_inc_gst+k.pmt_vllogi_inc_gst+k.pmt_vlpenl_inc_gst+k.pmt_mic1_inc_gst+k.pmt_mic2_inc_gst)-(k.pmt_cgate_ex_gst+k.pmt_scomm_ex_gst+k.pmt_vllogi_ex_gst+k.pmt_mic1_ex_gst+k.pmt_vlpenl_ex_gst+k.pmt_mic2_ex_gst) )          from payment_trans k          where p.[salesid] = k.[salesid] and  k.[stockupId]!=-1 and  k.[stockupId]!=-2            ) IGST,             (SELECT distinct sum(k3.Payable_Amoun)          from payment_trans k3          where p.[salesid] = k3.[salesid] and  k3.[stockupId]!=-1 and  k3.[stockupId]!=-2            ) totaldec,             (SELECT distinct sum((k4.pmt_scomm_inc_cgst+k4.pmt_cgate_inc_cgst+k4.pmt_vllogi_inc_cgst+k4.pmt_mic1_inc_cgst+k4.pmt_mic2_inc_cgst+k4.pmt_chnpent_inc_cgst)-(k4.pmt_scomm_ex_cgst+k4.pmt_cgate_ex_cgst+k4.pmt_vllogi_ex_cgst+k4.pmt_mic1_ex_cgst+k4.pmt_mic2_ex_cgst+k4.pmt_chnpent_exc_cgst) )          from payment_trans k4          where p.[salesid] = k4.[salesid] and  k4.[stockupId]!=-1 and  k4.[stockupId]!=-2            ) CGST , (SELECT distinct sum(x.Misc_charge) from payment_trans x  where p.[salesid] = x.[salesid] and x.[stockupId]!=-1 AND p.stockupId!=-2) Mis,      ( SELECT distinct sum((k5.pmt_scomm_inc_sgst+k5.pmt_cgate_inc_sgst+k5.pmt_vllogi_inc_sgst+k5.pmt_mic1_inc_sgst+k5.pmt_mic2_inc_sgst+k5.pmt_chnpent_inc_sgst)-(k5.pmt_scomm_ex_sgst+k5.pmt_cgate_ex_sgst+k5.pmt_vllogi_ex_sgst+k5.pmt_mic1_ex_sgst+k5.pmt_mic2_ex_cgst+k5.pmt_chnpent_exc_sgst) )          from payment_trans k5   where p.[salesid] = k5.[salesid] and  k5.[stockupId]!=-1  and  k5.[stockupId]!=-2  ) SGST ,h.ligst,h.higst,h.lowhighpt, (SELECT distinct sum(g1.TCS_IGST) from payment_trans g1  where p.[salesid] = g1.[salesid] and g1.[stockupId]!=-1 AND g1.stockupId!=-2 ) TCS_IGST, (SELECT distinct sum(g2.TCS_CGST) from payment_trans g2  where p.[salesid] = g2.[salesid] and g2.[stockupId]!=-1 AND g2.stockupId!=-2 ) TCS_CGST, (SELECT distinct sum(g3.TCS_GST) from payment_trans g3  where p.[salesid] = g3.[salesid] and g3.[stockupId]!=-1 AND g3.stockupId!=-2 ) TCS_SGST, (SELECT distinct count(cunt.Pt_id) from payment_trans cunt  where p.[salesid] = cunt.[salesid] and cunt.[stockupId]!=-1 AND cunt.stockupId!=-2 ) cunt from payment_trans p  inner join ArchiveStockUpInward arc on arc.StockupID=p.stockupId   inner join salesrecord sales on p.salesid=sales.salesidgivenbyvloc and p.vlocid=sales.saleschannelvlocid and sales.archiveid!=-1  left join Location l on l.LocationID=p.vlocid  inner join ItemStyle i on i.StyleID = arc.StyleID   inner join Size sz on sz.SizeID=arc.SizeID   inner join Lot lot on lot.BagId=arc.BagID   left join Column1 col1 on col1.Col1ID=i.Col1  inner join ItemCategory cat on cat.ItemCategoryID=i.ItemCatID inner join  hsnmaster h on h.hsnid = cat.hsnid  where p.stockupId!=-1 and p.stockupId!=-2 " +
         " union all  " +
         " select distinct p.[salesid],convert(varchar, arc.SystemDate, 104) as sys_date,CONVERT(CHAR(4), arc.SystemDate, 100) as sys_Month , CONVERT(CHAR(4), arc.SystemDate, 120)  as sys_year,  i.Title,cast(((arc.mrp*isnull(lot.Percentage,0))/100) as decimal(10,2)) as cp,i.Control3 as Articel,  sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,sales.status as System_status,sales.invoiceid,l.Location,p.stockupId,DATEDIFF(m, arc.SystemDate, sales.recordtimestamp) as stockAge, DATEDIFF(m, lot.invoiceDate, sales.recordtimestamp) as lotAge, arc.mrp ,sales.sellingprice as sys_sp ,STUFF((SELECT distinct ', ' + convert(varchar, n.order_date, 104)          from payment_trans n          where p.[salesid] = n.[salesid] and  n.[stockupId]!=-1 and  n.[stockupId]!=-2              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_date ,STUFF((SELECT distinct ', ' + cast(sp.sp as NVARCHAR(MAX))          from payment_trans sp          where p.[salesid] = sp.[salesid] and  sp.[stockupId]!=-1 and  sp.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_sp,   STUFF((SELECT distinct ', ' +cast(s.sellingprice as NVARCHAR(MAX))          from payment_trans t1          inner join cancelTrans s on s.salesidgivenbyvloc=t1.salesid and s.saleschannelvlocid=t1.vlocid and s.itemid!=-1          where p.[salesid] = t1.[salesid] and t1.[stockupId]!=-1 and t1.[stockupId]!=-2               FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,2,'') sm,        STUFF((SELECT distinct ', ' + cast(pybamt.Payable_Amoun as NVARCHAR(MAX))          from payment_trans pybamt          where p.[salesid] = pybamt.[salesid] and pybamt.[stockupId]!=-1  and pybamt.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') Payableamt,  STUFF((SELECT distinct ', ' + t7.type          from payment_trans t7          where p.[salesid] = t7.[salesid] and t7.[stockupId]!=-1 and t7.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_effecting_status,   STUFF((SELECT distinct ', ' + t2.Merchant_SKU          from payment_trans t2          where p.[salesid] = t2.[salesid] and t2.[stockupId]!=-1 and t2.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') sku ,   STUFF((SELECT distinct ', ' + tp.Payment_Status           from payment_trans tp          where p.[salesid] = tp.[salesid] and tp.[stockupId]!=-1 and tp.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') payment_status,       (SELECT distinct sum(chn.channel_commsion)          from payment_trans chn          where p.[salesid] = chn.[salesid] and  chn.[stockupId]!=-1 and  chn.[stockupId]!=-2            ) chennelcommision,       (SELECT distinct sum(gate.Channel_Gateway)          from payment_trans gate          where p.[salesid] = gate.[salesid] and gate.[stockupId]!=-1 and gate.[stockupId]!=-2            ) gatewaycommission,          (SELECT distinct sum(logic.VL_Logistics)          from payment_trans logic          where p.[salesid] = logic.[salesid] and logic.[stockupId]!=-1  and logic.[stockupId]!=-2             ) logistic,          (SELECT distinct sum(pent.VLPenalty)          from payment_trans pent          where p.[salesid] = pent.[salesid] and pent.[stockupId]!=-1 and pent.[stockupId]!=-2             ) Vlpenelty,             (SELECT distinct sum((k.pmt_cgate_inc_gst+k.pmt_scomm_inc_gst+k.pmt_vllogi_inc_gst+k.pmt_vlpenl_inc_gst+k.pmt_mic1_inc_gst+k.pmt_mic2_inc_gst)-(k.pmt_cgate_ex_gst+k.pmt_scomm_ex_gst+k.pmt_vllogi_ex_gst+k.pmt_mic1_ex_gst+k.pmt_vlpenl_ex_gst+k.pmt_mic2_ex_gst) )          from payment_trans k          where p.[salesid] = k.[salesid] and  k.[stockupId]!=-1 and  k.[stockupId]!=-2            ) IGST,             (SELECT distinct sum(k3.Payable_Amoun)          from payment_trans k3          where p.[salesid] = k3.[salesid] and  k3.[stockupId]!=-1 and  k3.[stockupId]!=-2            ) totaldec,             (SELECT distinct sum((k4.pmt_scomm_inc_cgst+k4.pmt_cgate_inc_cgst+k4.pmt_vllogi_inc_cgst+k4.pmt_mic1_inc_cgst+k4.pmt_mic2_inc_cgst+k4.pmt_chnpent_inc_cgst)-(k4.pmt_scomm_ex_cgst+k4.pmt_cgate_ex_cgst+k4.pmt_vllogi_ex_cgst+k4.pmt_mic1_ex_cgst+k4.pmt_mic2_ex_cgst+k4.pmt_chnpent_exc_cgst) )          from payment_trans k4          where p.[salesid] = k4.[salesid] and  k4.[stockupId]!=-1 and  k4.[stockupId]!=-2             ) CGST,  (SELECT distinct sum(x.Misc_charge) from payment_trans x  where p.[salesid] = x.[salesid] and x.[stockupId]!=-1 AND p.stockupId!=-2) Mis,    ( SELECT distinct sum((k5.pmt_scomm_inc_sgst+k5.pmt_cgate_inc_sgst+k5.pmt_vllogi_inc_sgst+k5.pmt_mic1_inc_sgst+k5.pmt_mic2_inc_sgst+k5.pmt_chnpent_inc_sgst)-(k5.pmt_scomm_ex_sgst+k5.pmt_cgate_ex_sgst+k5.pmt_vllogi_ex_sgst+k5.pmt_mic1_ex_sgst+k5.pmt_mic2_ex_cgst+k5.pmt_chnpent_exc_sgst) )          from payment_trans k5   where p.[salesid] = k5.[salesid] and  k5.[stockupId]!=-1 and  k5.[stockupId]!=-2 )  SGST,h.ligst,h.higst,h.lowhighpt,  (SELECT distinct sum(g1.TCS_IGST) from payment_trans g1  where p.[salesid] = g1.[salesid] and g1.[stockupId]!=-1 AND g1.stockupId!=-2 ) TCS_IGST, (SELECT distinct sum(g2.TCS_CGST) from payment_trans g2  where p.[salesid] = g2.[salesid] and g2.[stockupId]!=-1 AND g2.stockupId!=-2 ) TCS_CGST, (SELECT distinct sum(g3.TCS_GST) from payment_trans g3  where p.[salesid] = g3.[salesid] and g3.[stockupId]!=-1 AND g3.stockupId!=-2 ) TCS_SGST, (SELECT distinct count(cunt.Pt_id) from payment_trans cunt  where p.[salesid] = cunt.[salesid] and cunt.[stockupId]!=-1 AND cunt.stockupId!=-2 ) cunt from payment_trans p  inner join StockUpInward arc on arc.StockupID=p.stockupId   inner join cancelTrans sales on p.salesid=sales.salesidgivenbyvloc and p.vlocid=sales.saleschannelvlocid and sales.itemid!=-1     left join Location l on l.LocationID=p.vlocid    inner join ItemStyle i on i.StyleID = arc.StyleID  inner join Size sz on sz.SizeID=arc.SizeID  inner join Lot lot on lot.BagId=arc.BagID  left join Column1 col1 on col1.Col1ID=i.Col1 inner join ItemCategory cat on cat.ItemCategoryID=i.ItemCatID inner join  hsnmaster h on h.hsnid = cat.hsnid   where p.stockupId!=-1 and p.stockupId!=-2 " +
          "union all " +
          "select distinct p.[salesid],convert(varchar, arc.SystemDate, 104) as sys_date,CONVERT(CHAR(4), arc.SystemDate, 100) as sys_Month , CONVERT(CHAR(4), arc.SystemDate, 120)  as sys_year,  i.Title,cast(((arc.mrp*isnull(lot.Percentage,0))/100) as decimal(10,2)) as cp, i.Control3 as Articel, sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,sales.status as System_status,sales.invoiceid,l.Location,p.stockupId,DATEDIFF(m, arc.SystemDate, sales.recordtimestamp) as stockAge, DATEDIFF(m, lot.invoiceDate, sales.recordtimestamp) as lotAge, arc.mrp ,sales.sellingprice as sys_sp ,STUFF((SELECT distinct ', ' + convert(varchar, n.order_date, 104)          from payment_trans n          where p.[salesid] = n.[salesid] and  n.[stockupId]!=-1  and  n.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_date ,STUFF((SELECT distinct ', ' + cast(sp.sp as NVARCHAR(MAX))          from payment_trans sp          where p.[salesid] = sp.[salesid] and  sp.[stockupId]!=-1 and  sp.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_sp,   STUFF((SELECT distinct ', ' +cast(s.sellingprice as NVARCHAR(MAX))          from payment_trans t1          inner join cancelTrans s on s.salesidgivenbyvloc=t1.salesid and s.saleschannelvlocid=t1.vlocid and s.itemid!=-1          where p.[salesid] = t1.[salesid] and t1.[stockupId]!=-1 and t1.[stockupId]!=-2              FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,2,'') sm,        STUFF((SELECT distinct ', ' + cast(pybamt.Payable_Amoun as NVARCHAR(MAX))          from payment_trans pybamt          where p.[salesid] = pybamt.[salesid] and pybamt.[stockupId]!=-1 and pybamt.[stockupId]!=-2              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') Payableamt,  STUFF((SELECT distinct ', ' + t7.type          from payment_trans t7          where p.[salesid] = t7.[salesid] and t7.[stockupId]!=-1 and t7.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_effecting_status,   STUFF((SELECT distinct ', ' + t2.Merchant_SKU          from payment_trans t2          where p.[salesid] = t2.[salesid] and t2.[stockupId]!=-1 and t2.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') sku ,   STUFF((SELECT distinct ', ' + tp.Payment_Status           from payment_trans tp          where p.[salesid] = tp.[salesid] and tp.[stockupId]!=-1 and tp.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') payment_status,       (SELECT distinct sum(chn.channel_commsion)          from payment_trans chn          where p.[salesid] = chn.[salesid] and  chn.[stockupId]!=-1 and  chn.[stockupId]!=-2            ) chennelcommision,       (SELECT distinct sum(gate.Channel_Gateway)          from payment_trans gate          where p.[salesid] = gate.[salesid] and gate.[stockupId]!=-1 and gate.[stockupId]!=-2            ) gatewaycommission,          (SELECT distinct sum(logic.VL_Logistics)          from payment_trans logic          where p.[salesid] = logic.[salesid] and logic.[stockupId]!=-1 and logic.[stockupId]!=-2            ) logistic,          (SELECT distinct sum(pent.VLPenalty)          from payment_trans pent          where p.[salesid] = pent.[salesid] and pent.[stockupId]!=-1 and pent.[stockupId]!=-2            ) Vlpenelty,             (SELECT distinct sum((k.pmt_cgate_inc_gst+k.pmt_scomm_inc_gst+k.pmt_vllogi_inc_gst+k.pmt_mic1_inc_gst+k.pmt_vlpenl_inc_gst+k.pmt_mic2_inc_gst)-(k.pmt_cgate_ex_gst+k.pmt_scomm_ex_gst+k.pmt_vllogi_ex_gst+k.pmt_vlpenl_ex_gst+k.pmt_mic1_ex_gst+k.pmt_mic2_ex_gst) )          from payment_trans k          where p.[salesid] = k.[salesid] and  k.[stockupId]!=-1 and  k.[stockupId]!=-2            ) IGST,             (SELECT distinct sum(k3.Payable_Amoun)          from payment_trans k3          where p.[salesid] = k3.[salesid] and  k3.[stockupId]!=-1 and  k3.[stockupId]!=-2            ) totaldec,             (SELECT distinct sum((k4.pmt_scomm_inc_cgst+k4.pmt_cgate_inc_cgst+k4.pmt_vllogi_inc_cgst+k4.pmt_mic1_inc_cgst+k4.pmt_mic2_inc_cgst+k4.pmt_chnpent_inc_cgst)-(k4.pmt_scomm_ex_cgst+k4.pmt_cgate_ex_cgst+k4.pmt_vllogi_ex_cgst+k4.pmt_mic1_ex_cgst+k4.pmt_mic2_ex_cgst+k4.pmt_chnpent_exc_cgst) )          from payment_trans k4          where p.[salesid] = k4.[salesid] and  k4.[stockupId]!=-1 and  k4.[stockupId]!=-2            ) CGST,  (SELECT distinct sum(x.Misc_charge) from payment_trans x  where p.[salesid] = x.[salesid] and x.[stockupId]!=-1 AND p.stockupId!=-2) Mis,   ( SELECT distinct sum((k5.pmt_scomm_inc_sgst+k5.pmt_cgate_inc_sgst+k5.pmt_vllogi_inc_sgst+k5.pmt_mic1_inc_sgst+k5.pmt_mic2_inc_sgst+k5.pmt_chnpent_inc_sgst)-(k5.pmt_scomm_ex_sgst+k5.pmt_cgate_ex_sgst+k5.pmt_vllogi_ex_sgst+k5.pmt_mic1_ex_sgst+k5.pmt_mic2_ex_cgst+k5.pmt_chnpent_exc_sgst) )          from payment_trans k5  where p.[salesid] = k5.[salesid] and  k5.[stockupId]!=-1 and  k5.[stockupId]!=-2 ) SGST,h.ligst,h.higst,h.lowhighpt, (SELECT distinct sum(g1.TCS_IGST) from payment_trans g1  where p.[salesid] = g1.[salesid] and g1.[stockupId]!=-1 AND g1.stockupId!=-2 ) TCS_IGST, (SELECT distinct sum(g2.TCS_CGST) from payment_trans g2  where p.[salesid] = g2.[salesid] and g2.[stockupId]!=-1 AND g2.stockupId!=-2 ) TCS_CGST, (SELECT distinct sum(g3.TCS_GST) from payment_trans g3  where p.[salesid] = g3.[salesid] and g3.[stockupId]!=-1 AND g3.stockupId!=-2 ) TCS_SGST, (SELECT distinct count(cunt.Pt_id) from payment_trans cunt  where p.[salesid] = cunt.[salesid] and cunt.[stockupId]!=-1 AND cunt.stockupId!=-2 ) cunt from payment_trans p  inner join ArchiveStockUpInward arc on arc.StockupID=p.stockupId   inner join cancelTrans sales on p.salesid=sales.salesidgivenbyvloc and p.vlocid=sales.saleschannelvlocid and sales.archiveid!=-1      left join Location l on l.LocationID=p.vlocid    inner join ItemStyle i on i.StyleID = arc.StyleID  inner join Size sz on sz.SizeID=arc.SizeID  inner join Lot lot on lot.BagId=arc.BagID  left join Column1 col1 on col1.Col1ID=i.Col1 inner join ItemCategory cat on cat.ItemCategoryID=i.ItemCatID inner join  hsnmaster h on h.hsnid = cat.hsnid   where p.stockupId!=-1  and p.stockupId!=-2  ";*/
            command.CommandTimeout = 0;
            command.CommandText = "select Distinct p.[salesid],convert(varchar, arc.SystemDate, 104) as sys_date,CONVERT(CHAR(4), arc.SystemDate, 100) as sys_Month , CONVERT(CHAR(4), arc.SystemDate, 120)  as sys_year, i.Title,lot.Percentage as cp,i.Control3 as Articel,  sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,sales.status as System_status,sales.invoiceid,l.Location,p.stockupId,DATEDIFF(m, arc.SystemDate, sales.recordtimestamp) as stockAge, DATEDIFF(m, lot.invoiceDate, sales.recordtimestamp) as lotAge ,(SELECT  sum(tt1.other_charges) from payment_trans tt1 where p.[salesid] = tt1.[salesid] and tt1.[stockupId]!=-1 AND tt1.stockupId!=-2 ) otherchrg, arc.mrp ,sales.sellingprice as sys_sp ,STUFF((SELECT  ', ' + convert(varchar, n.order_date, 104)          from payment_trans n where p.[salesid] = n.[salesid] and  n.[stockupId]!=-1 and n.[stockupId]!=-2           FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_date ,STUFF((SELECT  ', ' + cast(sp.sp as NVARCHAR(MAX))          from payment_trans sp where p.[salesid] = sp.[salesid] and  sp.[stockupId]!=-1 and sp.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_sp,            STUFF((SELECT  ', ' + cast(pybamt.Payable_Amoun as NVARCHAR(MAX))          from payment_trans pybamt where p.[salesid] = pybamt.[salesid] and pybamt.[stockupId]!=-1  and pybamt.[stockupId]!=-2              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') Payableamt,  STUFF((SELECT  ', ' + t7.type from payment_trans t7          where p.[salesid] = t7.[salesid] and t7.[stockupId]!=-1 and t7.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_effecting_status,   STUFF((SELECT  ', ' + t2.Merchant_SKU from payment_trans t2          where p.[salesid] = t2.[salesid] and t2.[stockupId]!=-1 and t2.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') sku ,   STUFF((SELECT  ', ' + tp.Payment_Status from payment_trans tp          where p.[salesid] = tp.[salesid] and tp.[stockupId]!=-1             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') payment_status,       (SELECT  sum(chn.channel_commsion)          from payment_trans chn where p.[salesid] = chn.[salesid] and  chn.[stockupId]!=-1 and chn.[stockupId]!=-2            ) chennelcommision,       (SELECT  sum(gate.Channel_Gateway)          from payment_trans gate where p.[salesid] = gate.[salesid] and gate.[stockupId]!=-1 and gate.[stockupId]!=-2            ) gatewaycommission,          (SELECT  sum(logic.VL_Logistics)          from payment_trans logic where p.[salesid] = logic.[salesid] and logic.[stockupId]!=-1 and logic.[stockupId]!=-2            ) logistic,          (SELECT  sum(pent.VLPenalty)          from payment_trans pent where p.[salesid] = pent.[salesid] and pent.[stockupId]!=-1 and pent.[stockupId]!=-2            ) Vlpenelty,             (SELECT  sum((k.pmt_cgate_inc_gst+k.pmt_scomm_inc_gst+k.pmt_vllogi_inc_gst+k.pmt_vlpenl_inc_gst+k.pmt_mic1_inc_gst+k.pmt_mic2_inc_gst)-(k.pmt_cgate_ex_gst+k.pmt_scomm_ex_gst+k.pmt_vllogi_ex_gst+k.pmt_vlpenl_ex_gst+k.pmt_mic1_ex_gst+k.pmt_mic2_ex_gst) )          from payment_trans k where p.[salesid] = k.[salesid] and  k.[stockupId]!=-1 and k.[stockupId]!=-2            ) IGST,                                             (SELECT  sum(k3.Payable_Amoun)          from payment_trans k3 where p.[salesid] = k3.[salesid] and  k3.[stockupId]!=-1 and k3.[stockupId]!=-2            ) totaldec,             (SELECT  sum((k4.pmt_scomm_inc_cgst+k4.pmt_cgate_inc_cgst+k4.pmt_vllogi_inc_cgst+k4.pmt_mic1_inc_cgst+k4.pmt_mic2_inc_cgst+k4.pmt_chnpent_inc_cgst+k4.pmt_chnpent_inc_cgst)-(k4.pmt_scomm_ex_cgst+k4.pmt_cgate_ex_cgst+k4.pmt_vllogi_ex_cgst+k4.pmt_mic1_ex_cgst+k4.pmt_mic2_ex_cgst+k4.pmt_chnpent_exc_cgst+k4.pmt_chnpent_exc_cgst) )          from payment_trans k4 where p.[salesid] = k4.[salesid] and  k4.[stockupId]!=-1 and k4.[stockupId]!=-2            ) CGST , (SELECT  sum(x.Misc_charge) from payment_trans x where p.[salesid] = x.[salesid] and x.[stockupId]!=-1 AND x.stockupId!=-2 ) Mis ,   (SELECT  sum((k5.pmt_scomm_inc_sgst+k5.pmt_cgate_inc_sgst+k5.pmt_vllogi_inc_sgst+k5.pmt_mic1_inc_sgst+k5.pmt_mic2_inc_sgst+k5.pmt_chnpent_inc_sgst+k5.pmt_chnpent_inc_sgst)-(k5.pmt_scomm_ex_sgst+k5.pmt_cgate_ex_sgst+k5.pmt_vllogi_ex_sgst+k5.pmt_mic1_ex_sgst+k5.pmt_mic2_ex_cgst+k5.pmt_chnpent_exc_sgst+k5.pmt_chnpent_exc_sgst) )          from payment_trans k5 where p.[salesid] = k5.[salesid] and  k5.[stockupId]!=-1  and k5.[stockupId]!=-2 ) SGST,h.ligst,h.higst,h.lowhighpt, (SELECT  sum(g1.TCS_IGST) from payment_trans g1 where p.[salesid] = g1.[salesid] and g1.[stockupId]!=-1 AND g1.stockupId!=-2 ) TCS_IGST, (SELECT  sum(g2.TCS_CGST) from payment_trans g2 where p.[salesid] = g2.[salesid] and g2.[stockupId]!=-1 AND g2.stockupId!=-2 ) TCS_CGST, (SELECT  sum(g3.TCS_GST) from payment_trans g3 where p.[salesid] = g3.[salesid] and g3.[stockupId]!=-1 AND g3.stockupId!=-2 ) TCS_SGST, (SELECT  count(cunt.Pt_id) from payment_trans cunt where p.[salesid] = cunt.[salesid] and cunt.[stockupId]!=-1 AND cunt.stockupId!=-2 ) cunt, (SELECT  sum(g3.tcsincgst) from payment_trans g3 where p.[salesid] = g3.[salesid] and g3.[stockupId]!=-1 AND g3.stockupId!=-2 ) tcsincgst, (SELECT  sum(g4.tcsexcgst) from payment_trans g4 where p.[salesid] = g4.[salesid] and g4.[stockupId]!=-1 AND g4.stockupId!=-2 ) tcsexcgst, (SELECT  sum(cc1.mp_commission_igst) from payment_trans cc1 where p.[salesid] = cc1.[salesid] and cc1.[stockupId]!=-1 AND cc1.stockupId!=-2 ) Ichgategst, (SELECT  sum(cc2.pg_commission_igst) from payment_trans cc2 where p.[salesid] = cc2.[salesid] and cc2.[stockupId]!=-1 AND cc2.stockupId!=-2 ) Ichncomm, (SELECT  sum(cc3.logistics_igst) from payment_trans cc3 where p.[salesid] = cc3.[salesid] and cc3.[stockupId]!=-1 AND cc3.stockupId!=-2 ) Ilogicgst, (SELECT  sum(cc4.Total_Igst) from payment_trans cc4 where p.[salesid] = cc4.[salesid] and cc4.[stockupId]!=-1 AND cc4.stockupId!=-2 ) Itotgst, (SELECT  sum(cc5.TCS_IGST) from payment_trans cc5 where p.[salesid] = cc5.[salesid] and cc5.[stockupId]!=-1 AND cc5.stockupId!=-2 ) Itcsgst, (SELECT  sum(cc6.mp_commission_cgst) from payment_trans cc6 where p.[salesid] = cc6.[salesid] and cc6.[stockupId]!=-1 AND cc6.stockupId!=-2 ) Cchgategst, (SELECT  sum(cc7.pg_commission_cgst) from payment_trans cc7 where p.[salesid] = cc7.[salesid] and cc7.[stockupId]!=-1 AND cc7.stockupId!=-2 ) Cchncomm, (SELECT  sum(cc8.logistics_cgst) from payment_trans cc8 where p.[salesid] = cc8.[salesid] and cc8.[stockupId]!=-1 AND cc8.stockupId!=-2 ) Clogicgst, (SELECT  sum(cc9.TCS_CGST) from payment_trans cc9 where p.[salesid] = cc9.[salesid] and cc9.[stockupId]!=-1 AND cc9.stockupId!=-2 ) Ctcsgst, (SELECT  sum(cc10.Total_Cgst) from payment_trans cc10 where p.[salesid] = cc10.[salesid] and cc10.[stockupId]!=-1 AND cc10.stockupId!=-2 ) Ctotgst, (SELECT  sum(cc11.mp_commission_sgst) from payment_trans cc11 where p.[salesid] = cc11.[salesid] and cc11.[stockupId]!=-1 AND cc11.stockupId!=-2 ) Schgategst, (SELECT  sum(cc12.pg_commission_sgst) from payment_trans cc12 where p.[salesid] = cc12.[salesid] and cc12.[stockupId]!=-1 AND cc12.stockupId!=-2 ) Schncomm, (SELECT  sum(cc13.logistics_sgst) from payment_trans cc13 where p.[salesid] = cc13.[salesid] and cc13.[stockupId]!=-1 AND cc13.stockupId!=-2 ) Slogicgst, (SELECT  sum(cc14.TCS_GST) from payment_trans cc14 where p.[salesid] = cc14.[salesid] and cc14.[stockupId]!=-1 AND cc14.stockupId!=-2 ) Stcsgst, (SELECT  sum(cc15.Total_Sgst) from payment_trans cc15 where p.[salesid] = cc15.[salesid] and cc15.[stockupId]!=-1 AND cc15.stockupId!=-2 ) Stotgst, (SELECT  sum(cc16.penltyigst) from payment_trans cc16 where p.[salesid] = cc16.[salesid] and cc16.stockupId!=-2 and cc16.stockupId!=-1  ) Ipengst, (SELECT  sum(cc17.penltysgst) from payment_trans cc17 where p.[salesid] = cc17.[salesid] and cc17.stockupId!=-2 and cc17.stockupId!=-1 ) Spengst, (SELECT  sum(cc18.penltycgst) from payment_trans cc18 where p.[salesid] = cc18.[salesid]  AND cc18.stockupId!=-2  and cc18.stockupId!=-1) Cpengst from payment_trans p  inner join salesrecord sales on p.salesid= sales.salesidgivenbyvloc and p.vlocid= sales.saleschannelvlocid  inner join StockUpInward arc on arc.StockupID= p.stockupId   and sales.itemid!=-1   left join Location l on l.LocationID= p.vlocid            inner join ItemStyle i on i.StyleID = arc.StyleID          inner join Size sz on sz.SizeID= arc.SizeID          inner join Lot lot on lot.BagId= arc.BagID          left join Column1 col1 on col1.Col1ID= i.Col1 inner join ItemCategory cat on cat.ItemCategoryID= i.ItemCatID inner join  hsnmaster h on h.hsnid = cat.hsnid           where p.stockupId!=-1 and p.stockupId!=-2 and p.vlocid=@vlocid1   " +
                                " union all " +
                                "select Distinct  p.[salesid], convert(varchar, arc.SystemDate, 104) as sys_date,CONVERT(CHAR(4), arc.SystemDate, 100) as sys_Month , CONVERT(CHAR(4), arc.SystemDate, 120)  as sys_year,  i.Title,lot.Percentage as cp, i.Control3 as Articel, sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,sales.status as System_status,sales.invoiceid,l.Location,p.stockupId,DATEDIFF(m, arc.SystemDate, sales.recordtimestamp) as stockAge, DATEDIFF(m, lot.invoiceDate, sales.recordtimestamp) as lotAge,(SELECT  sum(tt1.other_charges) from payment_trans tt1 where p.[salesid] = tt1.[salesid] and tt1.[stockupId]!=-1 AND tt1.stockupId!=-2 ) otherchrg,  arc.mrp ,sales.sellingprice as sys_sp ,STUFF((SELECT  ', ' + convert(varchar, n.order_date, 104)          from payment_trans n where p.[salesid] = n.[salesid] and  n.[stockupId]!=-1 and n.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_date ,STUFF((SELECT  ', ' + cast(sp.sp as NVARCHAR(MAX))          from payment_trans sp where p.[salesid] = sp.[salesid] and  sp.[stockupId]!=-1 and sp.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_sp,       STUFF((SELECT  ', ' + cast(pybamt.Payable_Amoun as NVARCHAR(MAX))          from payment_trans pybamt where p.[salesid] = pybamt.[salesid] and pybamt.[stockupId]!=-1  and pybamt.[stockupId]!=-2              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') Payableamt,  STUFF((SELECT  ', ' + t7.type from payment_trans t7          where p.[salesid] = t7.[salesid] and t7.[stockupId]!=-1 and t7.[stockupId]!=-2              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_effecting_status,   STUFF((SELECT  ', ' + t2.Merchant_SKU from payment_trans t2          where p.[salesid] = t2.[salesid] and t2.[stockupId]!=-1 and t2.[stockupId]!=-2              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') sku ,   STUFF((SELECT  ', ' + tp.Payment_Status from payment_trans tp          where p.[salesid] = tp.[salesid] and tp.[stockupId]!=-1 and tp.[stockupId]!=-2              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') payment_status,       (SELECT  sum(chn.channel_commsion)          from payment_trans chn where p.[salesid] = chn.[salesid] and  chn.[stockupId]!=-1 and chn.[stockupId]!=-2            ) chennelcommision,       (SELECT  sum(gate.Channel_Gateway)          from payment_trans gate where p.[salesid] = gate.[salesid] and gate.[stockupId]!=-1 and gate.[stockupId]!=-2             ) gatewaycommission,          (SELECT  sum(logic.VL_Logistics)          from payment_trans logic where p.[salesid] = logic.[salesid] and logic.[stockupId]!=-1 and logic.[stockupId]!=-2             ) logistic,          (SELECT  sum(pent.VLPenalty)          from payment_trans pent where p.[salesid] = pent.[salesid] and pent.[stockupId]!=-1 and pent.[stockupId]!=-2             ) Vlpenelty,             (SELECT  sum((k.pmt_cgate_inc_gst+k.pmt_scomm_inc_gst+k.pmt_vllogi_inc_gst+k.pmt_vlpenl_inc_gst+k.pmt_mic1_inc_gst+k.pmt_mic2_inc_gst)-(k.pmt_cgate_ex_gst+k.pmt_scomm_ex_gst+k.pmt_vllogi_ex_gst+k.pmt_mic1_ex_gst+k.pmt_vlpenl_ex_gst+k.pmt_mic2_ex_gst) )          from payment_trans k where p.[salesid] = k.[salesid] and  k.[stockupId]!=-1 and k.[stockupId]!=-2            ) IGST,             (SELECT  sum(k3.Payable_Amoun)          from payment_trans k3 where p.[salesid] = k3.[salesid] and  k3.[stockupId]!=-1 and k3.[stockupId]!=-2            ) totaldec,             (SELECT  sum((k4.pmt_scomm_inc_cgst+k4.pmt_cgate_inc_cgst+k4.pmt_vllogi_inc_cgst+k4.pmt_mic1_inc_cgst+k4.pmt_mic2_inc_cgst+k4.pmt_chnpent_inc_cgst)-(k4.pmt_scomm_ex_cgst+k4.pmt_cgate_ex_cgst+k4.pmt_vllogi_ex_cgst+k4.pmt_mic1_ex_cgst+k4.pmt_mic2_ex_cgst+k4.pmt_chnpent_exc_cgst) )          from payment_trans k4 where p.[salesid] = k4.[salesid] and  k4.[stockupId]!=-1 and k4.[stockupId]!=-2            ) CGST , (SELECT  sum(x.Misc_charge) from payment_trans x where p.[salesid] = x.[salesid] and x.[stockupId]!=-1 AND p.stockupId!=-2) Mis,      (SELECT  sum((k5.pmt_scomm_inc_sgst+k5.pmt_cgate_inc_sgst+k5.pmt_vllogi_inc_sgst+k5.pmt_mic1_inc_sgst+k5.pmt_mic2_inc_sgst+k5.pmt_chnpent_inc_sgst)-(k5.pmt_scomm_ex_sgst+k5.pmt_cgate_ex_sgst+k5.pmt_vllogi_ex_sgst+k5.pmt_mic1_ex_sgst+k5.pmt_mic2_ex_cgst+k5.pmt_chnpent_exc_sgst) )          from payment_trans k5 where p.[salesid] = k5.[salesid] and  k5.[stockupId]!=-1  and k5.[stockupId]!=-2  ) SGST ,h.ligst,h.higst,h.lowhighpt, (SELECT  sum(g1.TCS_IGST) from payment_trans g1 where p.[salesid] = g1.[salesid] and g1.[stockupId]!=-1 AND g1.stockupId!=-2 ) TCS_IGST, (SELECT  sum(g2.TCS_CGST) from payment_trans g2 where p.[salesid] = g2.[salesid] and g2.[stockupId]!=-1 AND g2.stockupId!=-2 ) TCS_CGST, (SELECT  sum(g3.TCS_GST) from payment_trans g3 where p.[salesid] = g3.[salesid] and g3.[stockupId]!=-1 AND g3.stockupId!=-2 ) TCS_SGST, (SELECT  count(cunt.Pt_id) from payment_trans cunt where p.[salesid] = cunt.[salesid] and cunt.[stockupId]!=-1 AND cunt.stockupId!=-2 ) cunt, (SELECT  sum(g3.tcsincgst) from payment_trans g3 where p.[salesid] = g3.[salesid] and g3.[stockupId]!=-1 AND g3.stockupId!=-2 ) tcsincgst, (SELECT  sum(g4.tcsexcgst) from payment_trans g4 where p.[salesid] = g4.[salesid] and g4.[stockupId]!=-1 AND g4.stockupId!=-2 ) tcsexcgst, (SELECT  sum(cc1.mp_commission_igst) from payment_trans cc1 where p.[salesid] = cc1.[salesid] and cc1.[stockupId]!=-1 AND cc1.stockupId!=-2 ) Ichgategst, (SELECT  sum(cc2.pg_commission_igst) from payment_trans cc2 where p.[salesid] = cc2.[salesid] and cc2.[stockupId]!=-1 AND cc2.stockupId!=-2 ) Ichncomm, (SELECT  sum(cc3.logistics_igst) from payment_trans cc3 where p.[salesid] = cc3.[salesid] and cc3.[stockupId]!=-1 AND cc3.stockupId!=-2 ) Ilogicgst, (SELECT  sum(cc4.Total_Igst) from payment_trans cc4 where p.[salesid] = cc4.[salesid] and cc4.[stockupId]!=-1 AND cc4.stockupId!=-2 ) Itotgst, (SELECT  sum(cc5.TCS_IGST) from payment_trans cc5 where p.[salesid] = cc5.[salesid] and cc5.[stockupId]!=-1 AND cc5.stockupId!=-2 ) Itcsgst, (SELECT  sum(cc6.mp_commission_cgst) from payment_trans cc6 where p.[salesid] = cc6.[salesid] and cc6.[stockupId]!=-1 AND cc6.stockupId!=-2 ) Cchgategst, (SELECT  sum(cc7.pg_commission_cgst) from payment_trans cc7 where p.[salesid] = cc7.[salesid] and cc7.[stockupId]!=-1 AND cc7.stockupId!=-2 ) Cchncomm, (SELECT  sum(cc8.logistics_cgst) from payment_trans cc8 where p.[salesid] = cc8.[salesid] and cc8.[stockupId]!=-1 AND cc8.stockupId!=-2 ) Clogicgst, (SELECT  sum(cc9.TCS_CGST) from payment_trans cc9 where p.[salesid] = cc9.[salesid] and cc9.[stockupId]!=-1 AND cc9.stockupId!=-2 ) Ctcsgst, (SELECT  sum(cc10.Total_Cgst) from payment_trans cc10 where p.[salesid] = cc10.[salesid] and cc10.[stockupId]!=-1 AND cc10.stockupId!=-2 ) Ctotgst, (SELECT  sum(cc11.mp_commission_sgst) from payment_trans cc11 where p.[salesid] = cc11.[salesid] and cc11.[stockupId]!=-1 AND cc11.stockupId!=-2 ) Schgategst, (SELECT  sum(cc12.pg_commission_sgst) from payment_trans cc12 where p.[salesid] = cc12.[salesid] and cc12.[stockupId]!=-1 AND cc12.stockupId!=-2 ) Schncomm, (SELECT  sum(cc13.logistics_sgst) from payment_trans cc13 where p.[salesid] = cc13.[salesid] and cc13.[stockupId]!=-1 AND cc13.stockupId!=-2 ) Slogicgst, (SELECT  sum(cc14.TCS_GST) from payment_trans cc14 where p.[salesid] = cc14.[salesid] and cc14.[stockupId]!=-1 AND cc14.stockupId!=-2 ) Stcsgst, (SELECT  sum(cc15.Total_Sgst) from payment_trans cc15 where p.[salesid] = cc15.[salesid] and cc15.[stockupId]!=-1 AND cc15.stockupId!=-2 ) Stotgst, (SELECT  sum(cc16.penltyigst) from payment_trans cc16 where p.[salesid] = cc16.[salesid] and cc16.stockupId!=-2 and cc16.stockupId!=-1  ) Ipengst, (SELECT  sum(cc17.penltysgst) from payment_trans cc17 where p.[salesid] = cc17.[salesid] and cc17.stockupId!=-2 and cc17.stockupId!=-1 ) Spengst, (SELECT  sum(cc18.penltycgst) from payment_trans cc18 where p.[salesid] = cc18.[salesid]  AND cc18.stockupId!=-2  and cc18.stockupId!=-1) Cpengst from payment_trans p    inner join salesrecord sales on p.salesid=sales.salesidgivenbyvloc and p.vlocid= sales.saleschannelvlocid  inner join ArchiveStockUpInward arc on arc.StockupID= p.stockupId and arc.ArchiveStockupID= sales.archiveid and sales.archiveid!=-1   left join Location l on l.LocationID= p.vlocid  inner join ItemStyle i on i.StyleID = arc.StyleID   inner join Size sz on sz.SizeID= arc.SizeID   inner join Lot lot on lot.BagId= arc.BagID   left join Column1 col1 on col1.Col1ID= i.Col1  inner join ItemCategory cat on cat.ItemCategoryID= i.ItemCatID inner join  hsnmaster h on h.hsnid = cat.hsnid  where p.stockupId!=-1 and p.stockupId!=-2 and p.vlocid=@vlocid2 " +
                                " union all " +
                                "select Distinct  p.[salesid], convert(varchar, arc.SystemDate, 104) as sys_date,CONVERT(CHAR(4), arc.SystemDate, 100) as sys_Month , CONVERT(CHAR(4), arc.SystemDate, 120)  as sys_year,  i.Title,lot.Percentage as cp, i.Control3 as Articel, sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,sales.status as System_status,sales.invoiceid,l.Location,p.stockupId,DATEDIFF(m, arc.SystemDate, sales.recordtimestamp) as stockAge, DATEDIFF(m, lot.invoiceDate, sales.recordtimestamp) as lotAge,(SELECT  sum(tt1.other_charges) from payment_trans tt1 where p.[salesid] = tt1.[salesid] and tt1.[stockupId]!=-1 AND tt1.stockupId!=-2 ) otherchrg,  arc.mrp ,sales.sellingprice as sys_sp ,STUFF((SELECT  ', ' + convert(varchar, n.order_date, 104)          from payment_trans n where p.[salesid] = n.[salesid] and  n.[stockupId]!=-1 and n.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_date ,STUFF((SELECT  ', ' + cast(sp.sp as NVARCHAR(MAX))          from payment_trans sp where p.[salesid] = sp.[salesid] and  sp.[stockupId]!=-1 and sp.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_sp,       STUFF((SELECT  ', ' + cast(pybamt.Payable_Amoun as NVARCHAR(MAX))          from payment_trans pybamt where p.[salesid] = pybamt.[salesid] and pybamt.[stockupId]!=-1  and pybamt.[stockupId]!=-2              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') Payableamt,  STUFF((SELECT  ', ' + t7.type from payment_trans t7          where p.[salesid] = t7.[salesid] and t7.[stockupId]!=-1 and t7.[stockupId]!=-2              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_effecting_status,   STUFF((SELECT  ', ' + t2.Merchant_SKU from payment_trans t2          where p.[salesid] = t2.[salesid] and t2.[stockupId]!=-1 and t2.[stockupId]!=-2              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') sku ,   STUFF((SELECT  ', ' + tp.Payment_Status from payment_trans tp          where p.[salesid] = tp.[salesid] and tp.[stockupId]!=-1 and tp.[stockupId]!=-2              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') payment_status,       (SELECT  sum(chn.channel_commsion)          from payment_trans chn where p.[salesid] = chn.[salesid] and  chn.[stockupId]!=-1 and chn.[stockupId]!=-2            ) chennelcommision,       (SELECT  sum(gate.Channel_Gateway)          from payment_trans gate where p.[salesid] = gate.[salesid] and gate.[stockupId]!=-1 and gate.[stockupId]!=-2             ) gatewaycommission,          (SELECT  sum(logic.VL_Logistics)          from payment_trans logic where p.[salesid] = logic.[salesid] and logic.[stockupId]!=-1 and logic.[stockupId]!=-2             ) logistic,          (SELECT  sum(pent.VLPenalty)          from payment_trans pent where p.[salesid] = pent.[salesid] and pent.[stockupId]!=-1 and pent.[stockupId]!=-2             ) Vlpenelty,             (SELECT  sum((k.pmt_cgate_inc_gst+k.pmt_scomm_inc_gst+k.pmt_vllogi_inc_gst+k.pmt_vlpenl_inc_gst+k.pmt_mic1_inc_gst+k.pmt_mic2_inc_gst)-(k.pmt_cgate_ex_gst+k.pmt_scomm_ex_gst+k.pmt_vllogi_ex_gst+k.pmt_mic1_ex_gst+k.pmt_vlpenl_ex_gst+k.pmt_mic2_ex_gst) )          from payment_trans k where p.[salesid] = k.[salesid] and  k.[stockupId]!=-1 and k.[stockupId]!=-2            ) IGST,             (SELECT  sum(k3.Payable_Amoun)          from payment_trans k3 where p.[salesid] = k3.[salesid] and  k3.[stockupId]!=-1 and k3.[stockupId]!=-2            ) totaldec,             (SELECT  sum((k4.pmt_scomm_inc_cgst+k4.pmt_cgate_inc_cgst+k4.pmt_vllogi_inc_cgst+k4.pmt_mic1_inc_cgst+k4.pmt_mic2_inc_cgst+k4.pmt_chnpent_inc_cgst)-(k4.pmt_scomm_ex_cgst+k4.pmt_cgate_ex_cgst+k4.pmt_vllogi_ex_cgst+k4.pmt_mic1_ex_cgst+k4.pmt_mic2_ex_cgst+k4.pmt_chnpent_exc_cgst) )          from payment_trans k4 where p.[salesid] = k4.[salesid] and  k4.[stockupId]!=-1 and k4.[stockupId]!=-2            ) CGST , (SELECT  sum(x.Misc_charge) from payment_trans x where p.[salesid] = x.[salesid] and x.[stockupId]!=-1 AND p.stockupId!=-2) Mis,      (SELECT  sum((k5.pmt_scomm_inc_sgst+k5.pmt_cgate_inc_sgst+k5.pmt_vllogi_inc_sgst+k5.pmt_mic1_inc_sgst+k5.pmt_mic2_inc_sgst+k5.pmt_chnpent_inc_sgst)-(k5.pmt_scomm_ex_sgst+k5.pmt_cgate_ex_sgst+k5.pmt_vllogi_ex_sgst+k5.pmt_mic1_ex_sgst+k5.pmt_mic2_ex_cgst+k5.pmt_chnpent_exc_sgst) )          from payment_trans k5 where p.[salesid] = k5.[salesid] and  k5.[stockupId]!=-1  and k5.[stockupId]!=-2  ) SGST ,h.ligst,h.higst,h.lowhighpt, (SELECT  sum(g1.TCS_IGST) from payment_trans g1 where p.[salesid] = g1.[salesid] and g1.[stockupId]!=-1 AND g1.stockupId!=-2 ) TCS_IGST, (SELECT  sum(g2.TCS_CGST) from payment_trans g2 where p.[salesid] = g2.[salesid] and g2.[stockupId]!=-1 AND g2.stockupId!=-2 ) TCS_CGST, (SELECT  sum(g3.TCS_GST) from payment_trans g3 where p.[salesid] = g3.[salesid] and g3.[stockupId]!=-1 AND g3.stockupId!=-2 ) TCS_SGST, (SELECT  count(cunt.Pt_id) from payment_trans cunt where p.[salesid] = cunt.[salesid] and cunt.[stockupId]!=-1 AND cunt.stockupId!=-2 ) cunt, (SELECT  sum(g3.tcsincgst) from payment_trans g3 where p.[salesid] = g3.[salesid] and g3.[stockupId]!=-1 AND g3.stockupId!=-2 ) tcsincgst, (SELECT  sum(g4.tcsexcgst) from payment_trans g4 where p.[salesid] = g4.[salesid] and g4.[stockupId]!=-1 AND g4.stockupId!=-2 ) tcsexcgst, (SELECT  sum(cc1.mp_commission_igst) from payment_trans cc1 where p.[salesid] = cc1.[salesid] and cc1.[stockupId]!=-1 AND cc1.stockupId!=-2 ) Ichgategst, (SELECT  sum(cc2.pg_commission_igst) from payment_trans cc2 where p.[salesid] = cc2.[salesid] and cc2.[stockupId]!=-1 AND cc2.stockupId!=-2 ) Ichncomm, (SELECT  sum(cc3.logistics_igst) from payment_trans cc3 where p.[salesid] = cc3.[salesid] and cc3.[stockupId]!=-1 AND cc3.stockupId!=-2 ) Ilogicgst, (SELECT  sum(cc4.Total_Igst) from payment_trans cc4 where p.[salesid] = cc4.[salesid] and cc4.[stockupId]!=-1 AND cc4.stockupId!=-2 ) Itotgst, (SELECT  sum(cc5.TCS_IGST) from payment_trans cc5 where p.[salesid] = cc5.[salesid] and cc5.[stockupId]!=-1 AND cc5.stockupId!=-2 ) Itcsgst, (SELECT  sum(cc6.mp_commission_cgst) from payment_trans cc6 where p.[salesid] = cc6.[salesid] and cc6.[stockupId]!=-1 AND cc6.stockupId!=-2 ) Cchgategst, (SELECT  sum(cc7.pg_commission_cgst) from payment_trans cc7 where p.[salesid] = cc7.[salesid] and cc7.[stockupId]!=-1 AND cc7.stockupId!=-2 ) Cchncomm, (SELECT  sum(cc8.logistics_cgst) from payment_trans cc8 where p.[salesid] = cc8.[salesid] and cc8.[stockupId]!=-1 AND cc8.stockupId!=-2 ) Clogicgst, (SELECT  sum(cc9.TCS_CGST) from payment_trans cc9 where p.[salesid] = cc9.[salesid] and cc9.[stockupId]!=-1 AND cc9.stockupId!=-2 ) Ctcsgst, (SELECT  sum(cc10.Total_Cgst) from payment_trans cc10 where p.[salesid] = cc10.[salesid] and cc10.[stockupId]!=-1 AND cc10.stockupId!=-2 ) Ctotgst, (SELECT  sum(cc11.mp_commission_sgst) from payment_trans cc11 where p.[salesid] = cc11.[salesid] and cc11.[stockupId]!=-1 AND cc11.stockupId!=-2 ) Schgategst, (SELECT  sum(cc12.pg_commission_sgst) from payment_trans cc12 where p.[salesid] = cc12.[salesid] and cc12.[stockupId]!=-1 AND cc12.stockupId!=-2 ) Schncomm, (SELECT  sum(cc13.logistics_sgst) from payment_trans cc13 where p.[salesid] = cc13.[salesid] and cc13.[stockupId]!=-1 AND cc13.stockupId!=-2 ) Slogicgst, (SELECT  sum(cc14.TCS_GST) from payment_trans cc14 where p.[salesid] = cc14.[salesid] and cc14.[stockupId]!=-1 AND cc14.stockupId!=-2 ) Stcsgst, (SELECT  sum(cc15.Total_Sgst) from payment_trans cc15 where p.[salesid] = cc15.[salesid] and cc15.[stockupId]!=-1 AND cc15.stockupId!=-2 ) Stotgst, (SELECT  sum(cc16.penltyigst) from payment_trans cc16 where p.[salesid] = cc16.[salesid] and cc16.stockupId!=-2 and cc16.stockupId!=-1  ) Ipengst, (SELECT  sum(cc17.penltysgst) from payment_trans cc17 where p.[salesid] = cc17.[salesid] and cc17.stockupId!=-2 and cc17.stockupId!=-1 ) Spengst, (SELECT  sum(cc18.penltycgst) from payment_trans cc18 where p.[salesid] = cc18.[salesid]  AND cc18.stockupId!=-2  and cc18.stockupId!=-1) Cpengst from payment_trans p    inner join salesrecord sales on p.salesid=sales.salesidgivenbyvloc and p.vlocid= sales.saleschannelvlocid  inner join ArchiveStockUpInward arc on arc.StockupID= p.stockupId and arc.StockupID= sales.itemid left join Location l on l.LocationID= p.vlocid  inner join ItemStyle i on i.StyleID = arc.StyleID   inner join Size sz on sz.SizeID= arc.SizeID   inner join Lot lot on lot.BagId= arc.BagID   left join Column1 col1 on col1.Col1ID= i.Col1  inner join ItemCategory cat on cat.ItemCategoryID= i.ItemCatID inner join  hsnmaster h on h.hsnid = cat.hsnid  where p.stockupId!=-1 and p.stockupId!=-2 and p.vlocid=@vlocid3  " +
                                " union all " +
                                "select Distinct p.[salesid], convert(varchar, arc.SystemDate, 104) as sys_date,CONVERT(CHAR(4), arc.SystemDate, 100) as sys_Month , CONVERT(CHAR(4), arc.SystemDate, 120)  as sys_year,  i.Title,lot.Percentage as cp,i.Control3 as Articel,  sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,sales.status as System_status,sales.invoiceid,l.Location,p.stockupId,DATEDIFF(m, arc.SystemDate, sales.recordtimestamp) as stockAge, DATEDIFF(m, lot.invoiceDate, sales.recordtimestamp) as lotAge ,(SELECT  sum(tt1.other_charges) from payment_trans tt1 where p.[salesid] = tt1.[salesid] and tt1.[stockupId]!=-1 AND tt1.stockupId!=-2 ) otherchrg,  arc.mrp ,sales.sellingprice as sys_sp ,STUFF((SELECT  ', ' + convert(varchar, n.order_date, 104)          from payment_trans n where p.[salesid] = n.[salesid] and  n.[stockupId]!=-1 and n.[stockupId]!=-2              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_date ,STUFF((SELECT  ', ' + cast(sp.sp as NVARCHAR(MAX))          from payment_trans sp where p.[salesid] = sp.[salesid] and  sp.[stockupId]!=-1 and sp.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_sp,     STUFF((SELECT  ', ' + cast(pybamt.Payable_Amoun as NVARCHAR(MAX))          from payment_trans pybamt where p.[salesid] = pybamt.[salesid] and pybamt.[stockupId]!=-1  and pybamt.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') Payableamt,  STUFF((SELECT  ', ' + t7.type from payment_trans t7          where p.[salesid] = t7.[salesid] and t7.[stockupId]!=-1 and t7.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_effecting_status,   STUFF((SELECT  ', ' + t2.Merchant_SKU from payment_trans t2          where p.[salesid] = t2.[salesid] and t2.[stockupId]!=-1 and t2.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') sku ,   STUFF((SELECT  ', ' + tp.Payment_Status from payment_trans tp          where p.[salesid] = tp.[salesid] and tp.[stockupId]!=-1 and tp.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') payment_status,       (SELECT  sum(chn.channel_commsion)          from payment_trans chn where p.[salesid] = chn.[salesid] and  chn.[stockupId]!=-1 and chn.[stockupId]!=-2            ) chennelcommision,       (SELECT  sum(gate.Channel_Gateway)          from payment_trans gate where p.[salesid] = gate.[salesid] and gate.[stockupId]!=-1 and gate.[stockupId]!=-2            ) gatewaycommission,          (SELECT  sum(logic.VL_Logistics)          from payment_trans logic where p.[salesid] = logic.[salesid] and logic.[stockupId]!=-1  and logic.[stockupId]!=-2             ) logistic,          (SELECT  sum(pent.VLPenalty)          from payment_trans pent where p.[salesid] = pent.[salesid] and pent.[stockupId]!=-1 and pent.[stockupId]!=-2             ) Vlpenelty,             (SELECT  sum((k.pmt_cgate_inc_gst+k.pmt_scomm_inc_gst+k.pmt_vllogi_inc_gst+k.pmt_vlpenl_inc_gst+k.pmt_mic1_inc_gst+k.pmt_mic2_inc_gst)-(k.pmt_cgate_ex_gst+k.pmt_scomm_ex_gst+k.pmt_vllogi_ex_gst+k.pmt_mic1_ex_gst+k.pmt_vlpenl_ex_gst+k.pmt_mic2_ex_gst) )          from payment_trans k where p.[salesid] = k.[salesid] and  k.[stockupId]!=-1 and k.[stockupId]!=-2            ) IGST,                 (SELECT  sum(k3.Payable_Amoun)          from payment_trans k3 where p.[salesid] = k3.[salesid] and  k3.[stockupId]!=-1 and k3.[stockupId]!=-2            ) totaldec,             (SELECT  sum((k4.pmt_scomm_inc_cgst+k4.pmt_cgate_inc_cgst+k4.pmt_vllogi_inc_cgst+k4.pmt_mic1_inc_cgst+k4.pmt_mic2_inc_cgst+k4.pmt_chnpent_inc_cgst)-(k4.pmt_scomm_ex_cgst+k4.pmt_cgate_ex_cgst+k4.pmt_vllogi_ex_cgst+k4.pmt_mic1_ex_cgst+k4.pmt_mic2_ex_cgst+k4.pmt_chnpent_exc_cgst) )          from payment_trans k4 where p.[salesid] = k4.[salesid] and  k4.[stockupId]!=-1 and k4.[stockupId]!=-2             ) CGST,  (SELECT  sum(x.Misc_charge) from payment_trans x where p.[salesid] = x.[salesid] and x.[stockupId]!=-1 AND p.stockupId!=-2) Mis,    (SELECT  sum((k5.pmt_scomm_inc_sgst+k5.pmt_cgate_inc_sgst+k5.pmt_vllogi_inc_sgst+k5.pmt_mic1_inc_sgst+k5.pmt_mic2_inc_sgst+k5.pmt_chnpent_inc_sgst)-(k5.pmt_scomm_ex_sgst+k5.pmt_cgate_ex_sgst+k5.pmt_vllogi_ex_sgst+k5.pmt_mic1_ex_sgst+k5.pmt_mic2_ex_cgst+k5.pmt_chnpent_exc_sgst) )          from payment_trans k5 where p.[salesid] = k5.[salesid] and  k5.[stockupId]!=-1 and k5.[stockupId]!=-2 )  SGST,h.ligst,h.higst,h.lowhighpt,  (SELECT  sum(g1.TCS_IGST) from payment_trans g1 where p.[salesid] = g1.[salesid] and g1.[stockupId]!=-1 AND g1.stockupId!=-2 ) TCS_IGST, (SELECT  sum(g2.TCS_CGST) from payment_trans g2 where p.[salesid] = g2.[salesid] and g2.[stockupId]!=-1 AND g2.stockupId!=-2 ) TCS_CGST, (SELECT  sum(g3.TCS_GST) from payment_trans g3 where p.[salesid] = g3.[salesid] and g3.[stockupId]!=-1 AND g3.stockupId!=-2 ) TCS_SGST, (SELECT  count(cunt.Pt_id) from payment_trans cunt where p.[salesid] = cunt.[salesid] and cunt.[stockupId]!=-1 AND cunt.stockupId!=-2 ) cunt, (SELECT  sum(g3.tcsincgst) from payment_trans g3 where p.[salesid] = g3.[salesid] and g3.[stockupId]!=-1 AND g3.stockupId!=-2 ) tcsincgst, (SELECT  sum(g4.tcsexcgst) from payment_trans g4 where p.[salesid] = g4.[salesid] and g4.[stockupId]!=-1 AND g4.stockupId!=-2 ) tcsexcgst, (SELECT  sum(cc1.mp_commission_igst) from payment_trans cc1 where p.[salesid] = cc1.[salesid] and cc1.[stockupId]!=-1 AND cc1.stockupId!=-2 ) Ichgategst, (SELECT  sum(cc2.pg_commission_igst) from payment_trans cc2 where p.[salesid] = cc2.[salesid] and cc2.[stockupId]!=-1 AND cc2.stockupId!=-2 ) Ichncomm, (SELECT  sum(cc3.logistics_igst) from payment_trans cc3 where p.[salesid] = cc3.[salesid] and cc3.[stockupId]!=-1 AND cc3.stockupId!=-2 ) Ilogicgst, (SELECT  sum(cc4.Total_Igst) from payment_trans cc4 where p.[salesid] = cc4.[salesid] and cc4.[stockupId]!=-1 AND cc4.stockupId!=-2 ) Itotgst, (SELECT  sum(cc5.TCS_IGST) from payment_trans cc5 where p.[salesid] = cc5.[salesid] and cc5.[stockupId]!=-1 AND cc5.stockupId!=-2 ) Itcsgst, (SELECT  sum(cc6.mp_commission_cgst) from payment_trans cc6 where p.[salesid] = cc6.[salesid] and cc6.[stockupId]!=-1 AND cc6.stockupId!=-2 ) Cchgategst, (SELECT  sum(cc7.pg_commission_cgst) from payment_trans cc7 where p.[salesid] = cc7.[salesid] and cc7.[stockupId]!=-1 AND cc7.stockupId!=-2 ) Cchncomm, (SELECT  sum(cc8.logistics_cgst) from payment_trans cc8 where p.[salesid] = cc8.[salesid] and cc8.[stockupId]!=-1 AND cc8.stockupId!=-2 ) Clogicgst, (SELECT  sum(cc9.TCS_CGST) from payment_trans cc9 where p.[salesid] = cc9.[salesid] and cc9.[stockupId]!=-1 AND cc9.stockupId!=-2 ) Ctcsgst, (SELECT  sum(cc10.Total_Cgst) from payment_trans cc10 where p.[salesid] = cc10.[salesid] and cc10.[stockupId]!=-1 AND cc10.stockupId!=-2 ) Ctotgst, (SELECT  sum(cc11.mp_commission_sgst) from payment_trans cc11 where p.[salesid] = cc11.[salesid] and cc11.[stockupId]!=-1 AND cc11.stockupId!=-2 ) Schgategst, (SELECT  sum(cc12.pg_commission_sgst) from payment_trans cc12 where p.[salesid] = cc12.[salesid] and cc12.[stockupId]!=-1 AND cc12.stockupId!=-2 ) Schncomm, (SELECT  sum(cc13.logistics_sgst) from payment_trans cc13 where p.[salesid] = cc13.[salesid] and cc13.[stockupId]!=-1 AND cc13.stockupId!=-2 ) Slogicgst, (SELECT  sum(cc14.TCS_GST) from payment_trans cc14 where p.[salesid] = cc14.[salesid] and cc14.[stockupId]!=-1 AND cc14.stockupId!=-2 ) Stcsgst, (SELECT  sum(cc15.Total_Sgst) from payment_trans cc15 where p.[salesid] = cc15.[salesid] and cc15.[stockupId]!=-1 AND cc15.stockupId!=-2 ) Stotgst, (SELECT  sum(cc16.penltyigst) from payment_trans cc16 where p.[salesid] = cc16.[salesid] and cc16.stockupId!=-2 and cc16.stockupId!=-1  ) Ipengst, (SELECT  sum(cc17.penltysgst) from payment_trans cc17 where p.[salesid] = cc17.[salesid] and cc17.stockupId!=-2 and cc17.stockupId!=-1 ) Spengst, (SELECT  sum(cc18.penltycgst) from payment_trans cc18 where p.[salesid] = cc18.[salesid]  AND cc18.stockupId!=-2  and cc18.stockupId!=-1) Cpengst from payment_trans p    inner join cancelTrans sales on p.salesid=sales.salesidgivenbyvloc and p.vlocid= sales.saleschannelvlocid   inner join StockUpInward arc on arc.StockupID= p.stockupId and arc.StockupID= sales.itemid  and sales.itemid!=-1    left join Location l on l.LocationID= p.vlocid    inner join ItemStyle i on i.StyleID = arc.StyleID  inner join Size sz on sz.SizeID= arc.SizeID  inner join Lot lot on lot.BagId= arc.BagID  left join Column1 col1 on col1.Col1ID= i.Col1 inner join ItemCategory cat on cat.ItemCategoryID= i.ItemCatID inner join  hsnmaster h on h.hsnid = cat.hsnid   where p.stockupId!=-1 and p.stockupId!=-2 and p.vlocid=@vlocid4 " +
                                " union all " +
                                "select Distinct p.[salesid], convert(varchar, arc.SystemDate, 104) as sys_date,CONVERT(CHAR(4), arc.SystemDate, 100) as sys_Month , CONVERT(CHAR(4), arc.SystemDate, 120)  as sys_year,  i.Title,lot.Percentage as cp, i.Control3 as Articel, sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,sales.status as System_status,sales.invoiceid,l.Location,p.stockupId,DATEDIFF(m, arc.SystemDate, sales.recordtimestamp) as stockAge, DATEDIFF(m, lot.invoiceDate, sales.recordtimestamp) as lotAge ,(SELECT  sum(tt1.other_charges) from payment_trans tt1 where p.[salesid] = tt1.[salesid] and tt1.[stockupId]!=-1 AND tt1.stockupId!=-2 ) otherchrg,  arc.mrp ,sales.sellingprice as sys_sp ,STUFF((SELECT  ', ' + convert(varchar, n.order_date, 104)          from payment_trans n where p.[salesid] = n.[salesid] and  n.[stockupId]!=-1  and n.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_date ,STUFF((SELECT  ', ' + cast(sp.sp as NVARCHAR(MAX))          from payment_trans sp where p.[salesid] = sp.[salesid] and  sp.[stockupId]!=-1 and sp.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_sp,    STUFF((SELECT  ', ' + cast(pybamt.Payable_Amoun as NVARCHAR(MAX))          from payment_trans pybamt where p.[salesid] = pybamt.[salesid] and pybamt.[stockupId]!=-1 and pybamt.[stockupId]!=-2              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') Payableamt,  STUFF((SELECT  ', ' + t7.type from payment_trans t7          where p.[salesid] = t7.[salesid] and t7.[stockupId]!=-1 and t7.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_effecting_status,   STUFF((SELECT  ', ' + t2.Merchant_SKU from payment_trans t2          where p.[salesid] = t2.[salesid] and t2.[stockupId]!=-1 and t2.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') sku ,   STUFF((SELECT  ', ' + tp.Payment_Status from payment_trans tp          where p.[salesid] = tp.[salesid] and tp.[stockupId]!=-1 and tp.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') payment_status,       (SELECT  sum(chn.channel_commsion)          from payment_trans chn where p.[salesid] = chn.[salesid] and  chn.[stockupId]!=-1 and chn.[stockupId]!=-2            ) chennelcommision,       (SELECT  sum(gate.Channel_Gateway)          from payment_trans gate where p.[salesid] = gate.[salesid] and gate.[stockupId]!=-1 and gate.[stockupId]!=-2            ) gatewaycommission,          (SELECT  sum(logic.VL_Logistics)          from payment_trans logic where p.[salesid] = logic.[salesid] and logic.[stockupId]!=-1 and logic.[stockupId]!=-2            ) logistic,          (SELECT  sum(pent.VLPenalty)          from payment_trans pent where p.[salesid] = pent.[salesid] and pent.[stockupId]!=-1 and pent.[stockupId]!=-2            ) Vlpenelty,             (SELECT  sum((k.pmt_cgate_inc_gst+k.pmt_scomm_inc_gst+k.pmt_vllogi_inc_gst+k.pmt_mic1_inc_gst+k.pmt_vlpenl_inc_gst+k.pmt_mic2_inc_gst)-(k.pmt_cgate_ex_gst+k.pmt_scomm_ex_gst+k.pmt_vllogi_ex_gst+k.pmt_vlpenl_ex_gst+k.pmt_mic1_ex_gst+k.pmt_mic2_ex_gst) )          from payment_trans k where p.[salesid] = k.[salesid] and  k.[stockupId]!=-1 and k.[stockupId]!=-2            ) IGST,                     (SELECT  sum(k3.Payable_Amoun)          from payment_trans k3 where p.[salesid] = k3.[salesid] and  k3.[stockupId]!=-1 and k3.[stockupId]!=-2            ) totaldec,             (SELECT  sum((k4.pmt_scomm_inc_cgst+k4.pmt_cgate_inc_cgst+k4.pmt_vllogi_inc_cgst+k4.pmt_mic1_inc_cgst+k4.pmt_mic2_inc_cgst+k4.pmt_chnpent_inc_cgst)-(k4.pmt_scomm_ex_cgst+k4.pmt_cgate_ex_cgst+k4.pmt_vllogi_ex_cgst+k4.pmt_mic1_ex_cgst+k4.pmt_mic2_ex_cgst+k4.pmt_chnpent_exc_cgst) )          from payment_trans k4 where p.[salesid] = k4.[salesid] and  k4.[stockupId]!=-1 and k4.[stockupId]!=-2            ) CGST,  (SELECT  sum(x.Misc_charge) from payment_trans x where p.[salesid] = x.[salesid] and x.[stockupId]!=-1 AND p.stockupId!=-2) Mis,   (SELECT  sum((k5.pmt_scomm_inc_sgst+k5.pmt_cgate_inc_sgst+k5.pmt_vllogi_inc_sgst+k5.pmt_mic1_inc_sgst+k5.pmt_mic2_inc_sgst+k5.pmt_chnpent_inc_sgst)-(k5.pmt_scomm_ex_sgst+k5.pmt_cgate_ex_sgst+k5.pmt_vllogi_ex_sgst+k5.pmt_mic1_ex_sgst+k5.pmt_mic2_ex_cgst+k5.pmt_chnpent_exc_sgst) )          from payment_trans k5 where p.[salesid] = k5.[salesid] and  k5.[stockupId]!=-1 and k5.[stockupId]!=-2 ) SGST,h.ligst,h.higst,h.lowhighpt, (SELECT  sum(g1.TCS_IGST) from payment_trans g1 where p.[salesid] = g1.[salesid] and g1.[stockupId]!=-1 AND g1.stockupId!=-2 ) TCS_IGST, (SELECT  sum(g2.TCS_CGST) from payment_trans g2 where p.[salesid] = g2.[salesid] and g2.[stockupId]!=-1 AND g2.stockupId!=-2 ) TCS_CGST, (SELECT  sum(g3.TCS_GST) from payment_trans g3 where p.[salesid] = g3.[salesid] and g3.[stockupId]!=-1 AND g3.stockupId!=-2 ) TCS_SGST, (SELECT  count(cunt.Pt_id) from payment_trans cunt where p.[salesid] = cunt.[salesid] and cunt.[stockupId]!=-1 AND cunt.stockupId!=-2 ) cunt, (SELECT  sum(g3.tcsincgst) from payment_trans g3 where p.[salesid] = g3.[salesid] and g3.[stockupId]!=-1 AND g3.stockupId!=-2 ) tcsincgst, (SELECT  sum(g4.tcsexcgst) from payment_trans g4 where p.[salesid] = g4.[salesid] and g4.[stockupId]!=-1 AND g4.stockupId!=-2 ) tcsexcgst, (SELECT  sum(cc1.mp_commission_igst) from payment_trans cc1 where p.[salesid] = cc1.[salesid] and cc1.[stockupId]!=-1 AND cc1.stockupId!=-2 ) Ichgategst, (SELECT  sum(cc2.pg_commission_igst) from payment_trans cc2 where p.[salesid] = cc2.[salesid] and cc2.[stockupId]!=-1 AND cc2.stockupId!=-2 ) Ichncomm, (SELECT  sum(cc3.logistics_igst) from payment_trans cc3 where p.[salesid] = cc3.[salesid] and cc3.[stockupId]!=-1 AND cc3.stockupId!=-2 ) Ilogicgst, (SELECT  sum(cc4.Total_Igst) from payment_trans cc4 where p.[salesid] = cc4.[salesid] and cc4.[stockupId]!=-1 AND cc4.stockupId!=-2 ) Itotgst, (SELECT  sum(cc5.TCS_IGST) from payment_trans cc5 where p.[salesid] = cc5.[salesid] and cc5.[stockupId]!=-1 AND cc5.stockupId!=-2 ) Itcsgst, (SELECT  sum(cc6.mp_commission_cgst) from payment_trans cc6 where p.[salesid] = cc6.[salesid] and cc6.[stockupId]!=-1 AND cc6.stockupId!=-2 ) Cchgategst, (SELECT  sum(cc7.pg_commission_cgst) from payment_trans cc7 where p.[salesid] = cc7.[salesid] and cc7.[stockupId]!=-1 AND cc7.stockupId!=-2 ) Cchncomm, (SELECT  sum(cc8.logistics_cgst) from payment_trans cc8 where p.[salesid] = cc8.[salesid] and cc8.[stockupId]!=-1 AND cc8.stockupId!=-2 ) Clogicgst, (SELECT  sum(cc9.TCS_CGST) from payment_trans cc9 where p.[salesid] = cc9.[salesid] and cc9.[stockupId]!=-1 AND cc9.stockupId!=-2 ) Ctcsgst, (SELECT  sum(cc10.Total_Cgst) from payment_trans cc10 where p.[salesid] = cc10.[salesid] and cc10.[stockupId]!=-1 AND cc10.stockupId!=-2 ) Ctotgst, (SELECT  sum(cc11.mp_commission_sgst) from payment_trans cc11 where p.[salesid] = cc11.[salesid] and cc11.[stockupId]!=-1 AND cc11.stockupId!=-2 ) Schgategst, (SELECT  sum(cc12.pg_commission_sgst) from payment_trans cc12 where p.[salesid] = cc12.[salesid] and cc12.[stockupId]!=-1 AND cc12.stockupId!=-2 ) Schncomm, (SELECT  sum(cc13.logistics_sgst) from payment_trans cc13 where p.[salesid] = cc13.[salesid] and cc13.[stockupId]!=-1 AND cc13.stockupId!=-2 ) Slogicgst, (SELECT  sum(cc14.TCS_GST) from payment_trans cc14 where p.[salesid] = cc14.[salesid] and cc14.[stockupId]!=-1 AND cc14.stockupId!=-2 ) Stcsgst, (SELECT  sum(cc15.Total_Sgst) from payment_trans cc15 where p.[salesid] = cc15.[salesid] and cc15.[stockupId]!=-1 AND cc15.stockupId!=-2 ) Stotgst, (SELECT  sum(cc16.penltyigst) from payment_trans cc16 where p.[salesid] = cc16.[salesid] and cc16.stockupId!=-2 and cc16.stockupId!=-1  ) Ipengst, (SELECT  sum(cc17.penltysgst) from payment_trans cc17 where p.[salesid] = cc17.[salesid] and cc17.stockupId!=-2 and cc17.stockupId!=-1 ) Spengst, (SELECT  sum(cc18.penltycgst) from payment_trans cc18 where p.[salesid] = cc18.[salesid]  AND cc18.stockupId!=-2  and cc18.stockupId!=-1) Cpengst from payment_trans p     inner join cancelTrans sales on p.salesid=sales.salesidgivenbyvloc and p.vlocid= sales.saleschannelvlocid   inner join ArchiveStockUpInward arc on arc.StockupID= p.stockupId and arc.ArchiveStockupID= sales.archiveid   and sales.archiveid!=-1  left join Location l on l.LocationID= p.vlocid    inner join ItemStyle i on i.StyleID = arc.StyleID  inner join Size sz on sz.SizeID= arc.SizeID  inner join Lot lot on lot.BagId= arc.BagID  left join Column1 col1 on col1.Col1ID= i.Col1 inner join ItemCategory cat on cat.ItemCategoryID= i.ItemCatID inner join  hsnmaster h on h.hsnid = cat.hsnid   where p.stockupId!=-1  and p.stockupId!=-2 and p.vlocid=@vlocid5" +
                                " union all " +
                                "select Distinct p.[salesid], convert(varchar, arc.SystemDate, 104) as sys_date,CONVERT(CHAR(4), arc.SystemDate, 100) as sys_Month , CONVERT(CHAR(4), arc.SystemDate, 120)  as sys_year,  i.Title,lot.Percentage as cp, i.Control3 as Articel, sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,sales.status as System_status,sales.invoiceid,l.Location,p.stockupId,DATEDIFF(m, arc.SystemDate, sales.recordtimestamp) as stockAge, DATEDIFF(m, lot.invoiceDate, sales.recordtimestamp) as lotAge ,(SELECT  sum(tt1.other_charges) from payment_trans tt1 where p.[salesid] = tt1.[salesid] and tt1.[stockupId]!=-1 AND tt1.stockupId!=-2 ) otherchrg,  arc.mrp ,sales.sellingprice as sys_sp ,STUFF((SELECT  ', ' + convert(varchar, n.order_date, 104)          from payment_trans n where p.[salesid] = n.[salesid] and  n.[stockupId]!=-1  and n.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_date ,STUFF((SELECT  ', ' + cast(sp.sp as NVARCHAR(MAX))          from payment_trans sp where p.[salesid] = sp.[salesid] and  sp.[stockupId]!=-1 and sp.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_sp,    STUFF((SELECT  ', ' + cast(pybamt.Payable_Amoun as NVARCHAR(MAX))          from payment_trans pybamt where p.[salesid] = pybamt.[salesid] and pybamt.[stockupId]!=-1 and pybamt.[stockupId]!=-2              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') Payableamt,  STUFF((SELECT  ', ' + t7.type from payment_trans t7          where p.[salesid] = t7.[salesid] and t7.[stockupId]!=-1 and t7.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_effecting_status,   STUFF((SELECT  ', ' + t2.Merchant_SKU from payment_trans t2          where p.[salesid] = t2.[salesid] and t2.[stockupId]!=-1 and t2.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') sku ,   STUFF((SELECT  ', ' + tp.Payment_Status from payment_trans tp          where p.[salesid] = tp.[salesid] and tp.[stockupId]!=-1 and tp.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') payment_status,       (SELECT  sum(chn.channel_commsion)          from payment_trans chn where p.[salesid] = chn.[salesid] and  chn.[stockupId]!=-1 and chn.[stockupId]!=-2            ) chennelcommision,       (SELECT  sum(gate.Channel_Gateway)          from payment_trans gate where p.[salesid] = gate.[salesid] and gate.[stockupId]!=-1 and gate.[stockupId]!=-2            ) gatewaycommission,          (SELECT  sum(logic.VL_Logistics)          from payment_trans logic where p.[salesid] = logic.[salesid] and logic.[stockupId]!=-1 and logic.[stockupId]!=-2            ) logistic,          (SELECT  sum(pent.VLPenalty)          from payment_trans pent where p.[salesid] = pent.[salesid] and pent.[stockupId]!=-1 and pent.[stockupId]!=-2            ) Vlpenelty,             (SELECT  sum((k.pmt_cgate_inc_gst+k.pmt_scomm_inc_gst+k.pmt_vllogi_inc_gst+k.pmt_mic1_inc_gst+k.pmt_vlpenl_inc_gst+k.pmt_mic2_inc_gst)-(k.pmt_cgate_ex_gst+k.pmt_scomm_ex_gst+k.pmt_vllogi_ex_gst+k.pmt_vlpenl_ex_gst+k.pmt_mic1_ex_gst+k.pmt_mic2_ex_gst) )          from payment_trans k where p.[salesid] = k.[salesid] and  k.[stockupId]!=-1 and k.[stockupId]!=-2            ) IGST,                     (SELECT  sum(k3.Payable_Amoun)          from payment_trans k3 where p.[salesid] = k3.[salesid] and  k3.[stockupId]!=-1 and k3.[stockupId]!=-2            ) totaldec,             (SELECT  sum((k4.pmt_scomm_inc_cgst+k4.pmt_cgate_inc_cgst+k4.pmt_vllogi_inc_cgst+k4.pmt_mic1_inc_cgst+k4.pmt_mic2_inc_cgst+k4.pmt_chnpent_inc_cgst)-(k4.pmt_scomm_ex_cgst+k4.pmt_cgate_ex_cgst+k4.pmt_vllogi_ex_cgst+k4.pmt_mic1_ex_cgst+k4.pmt_mic2_ex_cgst+k4.pmt_chnpent_exc_cgst) )          from payment_trans k4 where p.[salesid] = k4.[salesid] and  k4.[stockupId]!=-1 and k4.[stockupId]!=-2            ) CGST,  (SELECT  sum(x.Misc_charge) from payment_trans x where p.[salesid] = x.[salesid] and x.[stockupId]!=-1 AND p.stockupId!=-2) Mis,   (SELECT  sum((k5.pmt_scomm_inc_sgst+k5.pmt_cgate_inc_sgst+k5.pmt_vllogi_inc_sgst+k5.pmt_mic1_inc_sgst+k5.pmt_mic2_inc_sgst+k5.pmt_chnpent_inc_sgst)-(k5.pmt_scomm_ex_sgst+k5.pmt_cgate_ex_sgst+k5.pmt_vllogi_ex_sgst+k5.pmt_mic1_ex_sgst+k5.pmt_mic2_ex_cgst+k5.pmt_chnpent_exc_sgst) )          from payment_trans k5 where p.[salesid] = k5.[salesid] and  k5.[stockupId]!=-1 and k5.[stockupId]!=-2 ) SGST,h.ligst,h.higst,h.lowhighpt, (SELECT  sum(g1.TCS_IGST) from payment_trans g1 where p.[salesid] = g1.[salesid] and g1.[stockupId]!=-1 AND g1.stockupId!=-2 ) TCS_IGST, (SELECT  sum(g2.TCS_CGST) from payment_trans g2 where p.[salesid] = g2.[salesid] and g2.[stockupId]!=-1 AND g2.stockupId!=-2 ) TCS_CGST, (SELECT  sum(g3.TCS_GST) from payment_trans g3 where p.[salesid] = g3.[salesid] and g3.[stockupId]!=-1 AND g3.stockupId!=-2 ) TCS_SGST, (SELECT  count(cunt.Pt_id) from payment_trans cunt where p.[salesid] = cunt.[salesid] and cunt.[stockupId]!=-1 AND cunt.stockupId!=-2 ) cunt, (SELECT  sum(g3.tcsincgst) from payment_trans g3 where p.[salesid] = g3.[salesid] and g3.[stockupId]!=-1 AND g3.stockupId!=-2 ) tcsincgst, (SELECT  sum(g4.tcsexcgst) from payment_trans g4 where p.[salesid] = g4.[salesid] and g4.[stockupId]!=-1 AND g4.stockupId!=-2 ) tcsexcgst, (SELECT  sum(cc1.mp_commission_igst) from payment_trans cc1 where p.[salesid] = cc1.[salesid] and cc1.[stockupId]!=-1 AND cc1.stockupId!=-2 ) Ichgategst, (SELECT  sum(cc2.pg_commission_igst) from payment_trans cc2 where p.[salesid] = cc2.[salesid] and cc2.[stockupId]!=-1 AND cc2.stockupId!=-2 ) Ichncomm, (SELECT  sum(cc3.logistics_igst) from payment_trans cc3 where p.[salesid] = cc3.[salesid] and cc3.[stockupId]!=-1 AND cc3.stockupId!=-2 ) Ilogicgst, (SELECT  sum(cc4.Total_Igst) from payment_trans cc4 where p.[salesid] = cc4.[salesid] and cc4.[stockupId]!=-1 AND cc4.stockupId!=-2 ) Itotgst, (SELECT  sum(cc5.TCS_IGST) from payment_trans cc5 where p.[salesid] = cc5.[salesid] and cc5.[stockupId]!=-1 AND cc5.stockupId!=-2 ) Itcsgst, (SELECT  sum(cc6.mp_commission_cgst) from payment_trans cc6 where p.[salesid] = cc6.[salesid] and cc6.[stockupId]!=-1 AND cc6.stockupId!=-2 ) Cchgategst, (SELECT  sum(cc7.pg_commission_cgst) from payment_trans cc7 where p.[salesid] = cc7.[salesid] and cc7.[stockupId]!=-1 AND cc7.stockupId!=-2 ) Cchncomm, (SELECT  sum(cc8.logistics_cgst) from payment_trans cc8 where p.[salesid] = cc8.[salesid] and cc8.[stockupId]!=-1 AND cc8.stockupId!=-2 ) Clogicgst, (SELECT  sum(cc9.TCS_CGST) from payment_trans cc9 where p.[salesid] = cc9.[salesid] and cc9.[stockupId]!=-1 AND cc9.stockupId!=-2 ) Ctcsgst, (SELECT  sum(cc10.Total_Cgst) from payment_trans cc10 where p.[salesid] = cc10.[salesid] and cc10.[stockupId]!=-1 AND cc10.stockupId!=-2 ) Ctotgst, (SELECT  sum(cc11.mp_commission_sgst) from payment_trans cc11 where p.[salesid] = cc11.[salesid] and cc11.[stockupId]!=-1 AND cc11.stockupId!=-2 ) Schgategst, (SELECT  sum(cc12.pg_commission_sgst) from payment_trans cc12 where p.[salesid] = cc12.[salesid] and cc12.[stockupId]!=-1 AND cc12.stockupId!=-2 ) Schncomm, (SELECT  sum(cc13.logistics_sgst) from payment_trans cc13 where p.[salesid] = cc13.[salesid] and cc13.[stockupId]!=-1 AND cc13.stockupId!=-2 ) Slogicgst, (SELECT  sum(cc14.TCS_GST) from payment_trans cc14 where p.[salesid] = cc14.[salesid] and cc14.[stockupId]!=-1 AND cc14.stockupId!=-2 ) Stcsgst, (SELECT  sum(cc15.Total_Sgst) from payment_trans cc15 where p.[salesid] = cc15.[salesid] and cc15.[stockupId]!=-1 AND cc15.stockupId!=-2 ) Stotgst, (SELECT  sum(cc16.penltyigst) from payment_trans cc16 where p.[salesid] = cc16.[salesid] and cc16.stockupId!=-2 and cc16.stockupId!=-1  ) Ipengst, (SELECT  sum(cc17.penltysgst) from payment_trans cc17 where p.[salesid] = cc17.[salesid]  and cc17.stockupId!=-2 and cc17.stockupId!=-1 ) Spengst, (SELECT  sum(cc18.penltycgst) from payment_trans cc18 where p.[salesid] = cc18.[salesid]  AND cc18.stockupId!=-2  and cc18.stockupId!=-1) Cpengst from payment_trans p     inner join cancelTrans sales on p.salesid=sales.salesidgivenbyvloc and p.vlocid= sales.saleschannelvlocid   inner join ArchiveStockUpInward arc on arc.StockupID= p.stockupId and arc.StockupID= sales.itemid   left join Location l on l.LocationID= p.vlocid    inner join ItemStyle i on i.StyleID = arc.StyleID  inner join Size sz on sz.SizeID= arc.SizeID  inner join Lot lot on lot.BagId= arc.BagID  left join Column1 col1 on col1.Col1ID= i.Col1 inner join ItemCategory cat on cat.ItemCategoryID= i.ItemCatID inner join  hsnmaster h on h.hsnid = cat.hsnid   where p.stockupId!=-1  and p.stockupId!=-2 and p.vlocid=@vlocid6  ";

            command.Parameters.AddWithValue("@vlocid1", vlocid);
            command.Parameters.AddWithValue("@vlocid2", vlocid);
            command.Parameters.AddWithValue("@vlocid3", vlocid);
            command.Parameters.AddWithValue("@vlocid4", vlocid);
            command.Parameters.AddWithValue("@vlocid5", vlocid);
            command.Parameters.AddWithValue("@vlocid6", vlocid);
            /*command.CommandText ="select * from (select distinct p.[salesid],convert(varchar, arc.SystemDate, 104) as sys_date,CONVERT(CHAR(4), arc.SystemDate, 100) as sys_Month , CONVERT(CHAR(4), arc.SystemDate, 120)  as sys_year,  i.Title,cast(((arc.mrp* isnull(lot.Percentage,0))/100) as decimal (10,2)) as cp,i.Control3 as Articel,  sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,sales.status as System_status,sales.invoiceid,l.Location,p.stockupId,DATEDIFF(m, arc.SystemDate, sales.recordtimestamp) as stockAge, DATEDIFF(m, lot.invoiceDate, sales.recordtimestamp) as lotAge, arc.mrp ,sales.sellingprice as sys_sp ,STUFF((SELECT distinct ', ' + convert(varchar, n.order_date, 104)          from payment_trans n where p.[salesid] = n.[salesid] and  n.[stockupId]!=-1 and n.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_date ,STUFF((SELECT distinct ', ' + cast(sp.sp as NVARCHAR(MAX))          from payment_trans sp where p.[salesid] = sp.[salesid] and  sp.[stockupId]!=-1 and sp.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_sp,   STUFF((SELECT distinct ', ' +cast(s.sellingprice as NVARCHAR(MAX))          from payment_trans t1 inner join salesrecord s on s.salesidgivenbyvloc=t1.salesid and s.saleschannelvlocid=t1.vlocid and s.itemid!=-1          where p.[salesid] = t1.[salesid] and t1.[stockupId]!=-1 and t1.[stockupId]!=-2              FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,2,'') sm,        STUFF((SELECT distinct ', ' + cast(pybamt.Payable_Amoun as NVARCHAR(MAX))          from payment_trans pybamt where p.[salesid] = pybamt.[salesid] and pybamt.[stockupId]!=-1  and pybamt.[stockupId]!=-2              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') Payableamt,  STUFF((SELECT distinct ', ' + t7.type from payment_trans t7          where p.[salesid] = t7.[salesid] and t7.[stockupId]!=-1 and t7.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_effecting_status,   STUFF((SELECT distinct ', ' + t2.Merchant_SKU from payment_trans t2          where p.[salesid] = t2.[salesid] and t2.[stockupId]!=-1 and t2.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') sku ,   STUFF((SELECT distinct ', ' + tp.Payment_Status from payment_trans tp          where p.[salesid] = tp.[salesid] and tp.[stockupId]!=-1             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') payment_status,       (SELECT distinct sum(chn.channel_commsion)          from payment_trans chn where p.[salesid] = chn.[salesid] and  chn.[stockupId]!=-1 and chn.[stockupId]!=-2            ) chennelcommision,       (SELECT distinct sum(gate.Channel_Gateway)          from payment_trans gate where p.[salesid] = gate.[salesid] and gate.[stockupId]!=-1 and gate.[stockupId]!=-2            ) gatewaycommission,          (SELECT distinct sum(logic.VL_Logistics)          from payment_trans logic where p.[salesid] = logic.[salesid] and logic.[stockupId]!=-1 and logic.[stockupId]!=-2            ) logistic,          (SELECT distinct sum(pent.VLPenalty)          from payment_trans pent where p.[salesid] = pent.[salesid] and pent.[stockupId]!=-1 and pent.[stockupId]!=-2            ) Vlpenelty,             (SELECT distinct sum((k.pmt_cgate_inc_gst+k.pmt_scomm_inc_gst+k.pmt_vllogi_inc_gst+k.pmt_vlpenl_inc_gst+k.pmt_mic1_inc_gst+k.pmt_mic2_inc_gst)-(k.pmt_cgate_ex_gst+k.pmt_scomm_ex_gst+k.pmt_vllogi_ex_gst+k.pmt_vlpenl_ex_gst+k.pmt_mic1_ex_gst+k.pmt_mic2_ex_gst) )          from payment_trans k where p.[salesid] = k.[salesid] and  k.[stockupId]!=-1 and k.[stockupId]!=-2            ) IGST,             (SELECT distinct sum(k3.Payable_Amoun)          from payment_trans k3 where p.[salesid] = k3.[salesid] and  k3.[stockupId]!=-1 and k3.[stockupId]!=-2            ) totaldec,             (SELECT distinct sum((k4.pmt_scomm_inc_cgst+k4.pmt_cgate_inc_cgst+k4.pmt_vllogi_inc_cgst+k4.pmt_mic1_inc_cgst+k4.pmt_mic2_inc_cgst+k4.pmt_chnpent_inc_cgst+k4.pmt_chnpent_inc_cgst)-(k4.pmt_scomm_ex_cgst+k4.pmt_cgate_ex_cgst+k4.pmt_vllogi_ex_cgst+k4.pmt_mic1_ex_cgst+k4.pmt_mic2_ex_cgst+k4.pmt_chnpent_exc_cgst+k4.pmt_chnpent_exc_cgst) )          from payment_trans k4 where p.[salesid] = k4.[salesid] and  k4.[stockupId]!=-1 and k4.[stockupId]!=-2            ) CGST , (SELECT distinct sum(x.Misc_charge) from payment_trans x where p.[salesid] = x.[salesid] and x.[stockupId]!=-1 AND x.stockupId!=-2 ) Mis ,   (SELECT distinct sum((k5.pmt_scomm_inc_sgst+k5.pmt_cgate_inc_sgst+k5.pmt_vllogi_inc_sgst+k5.pmt_mic1_inc_sgst+k5.pmt_mic2_inc_sgst+k5.pmt_chnpent_inc_sgst+k5.pmt_chnpent_inc_sgst)-(k5.pmt_scomm_ex_sgst+k5.pmt_cgate_ex_sgst+k5.pmt_vllogi_ex_sgst+k5.pmt_mic1_ex_sgst+k5.pmt_mic2_ex_cgst+k5.pmt_chnpent_exc_sgst+k5.pmt_chnpent_exc_sgst) )          from payment_trans k5 where p.[salesid] = k5.[salesid] and  k5.[stockupId]!=-1  and k5.[stockupId]!=-2 ) SGST,h.ligst,h.higst,h.lowhighpt, (SELECT distinct sum(g1.TCS_IGST) from payment_trans g1 where p.[salesid] = g1.[salesid] and g1.[stockupId]!=-1 AND g1.stockupId!=-2 ) TCS_IGST, (SELECT distinct sum(g2.TCS_CGST) from payment_trans g2 where p.[salesid] = g2.[salesid] and g2.[stockupId]!=-1 AND g2.stockupId!=-2 ) TCS_CGST, (SELECT distinct sum(g3.TCS_GST) from payment_trans g3 where p.[salesid] = g3.[salesid] and g3.[stockupId]!=-1 AND g3.stockupId!=-2 ) TCS_SGST from payment_trans p " +
                                "inner join salesrecord sales on p.salesid= sales.salesidgivenbyvloc and p.vlocid= sales.saleschannelvlocid " +
                                "inner join StockUpInward arc on arc.StockupID= p.stockupId  and arc.StockupID= sales.itemid  and sales.itemid!=-1  " +
                                "left join Location l on l.LocationID= p.vlocid " +
                                "inner join ItemStyle i on i.StyleID = arc.StyleID " +
                                "inner join Size sz on sz.SizeID= arc.SizeID  " +
                                "inner join Lot lot on lot.BagId= arc.BagID  " +
                                "left join Column1 col1 on col1.Col1ID= i.Col1  " +
                                "inner join ItemCategory cat on cat.ItemCategoryID= i.ItemCatID " +
                                "inner join  hsnmaster h on h.hsnid = cat.hsnid " +
                                "where p.stockupId!=-1 and p.stockupId!=-2  " +
                                "union all " +
                                "select distinct p.[salesid], convert(varchar, arc.SystemDate, 104) as sys_date,CONVERT(CHAR(4), arc.SystemDate, 100) as sys_Month , CONVERT(CHAR(4), arc.SystemDate, 120)  as sys_year,  i.Title,cast(((arc.mrp* isnull(lot.Percentage,0))/100) as decimal (10,2)) as cp, i.Control3 as Articel, sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,sales.status as System_status,sales.invoiceid,l.Location,p.stockupId,DATEDIFF(m, arc.SystemDate, sales.recordtimestamp) as stockAge, DATEDIFF(m, lot.invoiceDate, sales.recordtimestamp) as lotAge, arc.mrp ,sales.sellingprice as sys_sp ,STUFF((SELECT distinct ', ' + convert(varchar, n.order_date, 104)          from payment_trans n where p.[salesid] = n.[salesid] and  n.[stockupId]!=-1 and n.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_date ,STUFF((SELECT distinct ', ' + cast(sp.sp as NVARCHAR(MAX))          from payment_trans sp where p.[salesid] = sp.[salesid] and  sp.[stockupId]!=-1 and sp.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_sp,   STUFF((SELECT distinct ', ' +cast(s.sellingprice as NVARCHAR(MAX))          from payment_trans t1 inner join salesrecord s on s.salesidgivenbyvloc=t1.salesid and s.saleschannelvlocid=t1.vlocid and s.itemid!=-1          where p.[salesid] = t1.[salesid] and t1.[stockupId]!=-1 and t1.[stockupId]!=-2               FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,2,'') sm,        STUFF((SELECT distinct ', ' + cast(pybamt.Payable_Amoun as NVARCHAR(MAX))          from payment_trans pybamt where p.[salesid] = pybamt.[salesid] and pybamt.[stockupId]!=-1  and pybamt.[stockupId]!=-2              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') Payableamt,  STUFF((SELECT distinct ', ' + t7.type from payment_trans t7          where p.[salesid] = t7.[salesid] and t7.[stockupId]!=-1 and t7.[stockupId]!=-2              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_effecting_status,   STUFF((SELECT distinct ', ' + t2.Merchant_SKU from payment_trans t2          where p.[salesid] = t2.[salesid] and t2.[stockupId]!=-1 and t2.[stockupId]!=-2              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') sku ,   STUFF((SELECT distinct ', ' + tp.Payment_Status from payment_trans tp          where p.[salesid] = tp.[salesid] and tp.[stockupId]!=-1 and tp.[stockupId]!=-2              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') payment_status,       (SELECT distinct sum(chn.channel_commsion)          from payment_trans chn where p.[salesid] = chn.[salesid] and  chn.[stockupId]!=-1 and chn.[stockupId]!=-2            ) chennelcommision,       (SELECT distinct sum(gate.Channel_Gateway)          from payment_trans gate where p.[salesid] = gate.[salesid] and gate.[stockupId]!=-1 and gate.[stockupId]!=-2             ) gatewaycommission,          (SELECT distinct sum(logic.VL_Logistics)          from payment_trans logic where p.[salesid] = logic.[salesid] and logic.[stockupId]!=-1 and logic.[stockupId]!=-2             ) logistic,          (SELECT distinct sum(pent.VLPenalty)          from payment_trans pent where p.[salesid] = pent.[salesid] and pent.[stockupId]!=-1 and pent.[stockupId]!=-2             ) Vlpenelty,             (SELECT distinct sum((k.pmt_cgate_inc_gst+k.pmt_scomm_inc_gst+k.pmt_vllogi_inc_gst+k.pmt_vlpenl_inc_gst+k.pmt_mic1_inc_gst+k.pmt_mic2_inc_gst)-(k.pmt_cgate_ex_gst+k.pmt_scomm_ex_gst+k.pmt_vllogi_ex_gst+k.pmt_mic1_ex_gst+k.pmt_vlpenl_ex_gst+k.pmt_mic2_ex_gst) )          from payment_trans k where p.[salesid] = k.[salesid] and  k.[stockupId]!=-1 and k.[stockupId]!=-2            ) IGST,             (SELECT distinct sum(k3.Payable_Amoun)          from payment_trans k3 where p.[salesid] = k3.[salesid] and  k3.[stockupId]!=-1 and k3.[stockupId]!=-2            ) totaldec,             (SELECT distinct sum((k4.pmt_scomm_inc_cgst+k4.pmt_cgate_inc_cgst+k4.pmt_vllogi_inc_cgst+k4.pmt_mic1_inc_cgst+k4.pmt_mic2_inc_cgst+k4.pmt_chnpent_inc_cgst)-(k4.pmt_scomm_ex_cgst+k4.pmt_cgate_ex_cgst+k4.pmt_vllogi_ex_cgst+k4.pmt_mic1_ex_cgst+k4.pmt_mic2_ex_cgst+k4.pmt_chnpent_exc_cgst) )          from payment_trans k4 where p.[salesid] = k4.[salesid] and  k4.[stockupId]!=-1 and k4.[stockupId]!=-2            ) CGST , (SELECT distinct sum(x.Misc_charge) from payment_trans x where p.[salesid] = x.[salesid] and x.[stockupId]!=-1 AND p.stockupId!=-2) Mis,      (SELECT distinct sum((k5.pmt_scomm_inc_sgst+k5.pmt_cgate_inc_sgst+k5.pmt_vllogi_inc_sgst+k5.pmt_mic1_inc_sgst+k5.pmt_mic2_inc_sgst+k5.pmt_chnpent_inc_sgst)-(k5.pmt_scomm_ex_sgst+k5.pmt_cgate_ex_sgst+k5.pmt_vllogi_ex_sgst+k5.pmt_mic1_ex_sgst+k5.pmt_mic2_ex_cgst+k5.pmt_chnpent_exc_sgst) )          from payment_trans k5 where p.[salesid] = k5.[salesid] and  k5.[stockupId]!=-1  and k5.[stockupId]!=-2  ) SGST ,h.ligst,h.higst,h.lowhighpt, (SELECT distinct sum(g1.TCS_IGST) from payment_trans g1 where p.[salesid] = g1.[salesid] and g1.[stockupId]!=-1 AND g1.stockupId!=-2 ) TCS_IGST, (SELECT distinct sum(g2.TCS_CGST) from payment_trans g2 where p.[salesid] = g2.[salesid] and g2.[stockupId]!=-1 AND g2.stockupId!=-2 ) TCS_CGST, (SELECT distinct sum(g3.TCS_GST) from payment_trans g3 where p.[salesid] = g3.[salesid] and g3.[stockupId]!=-1 AND g3.stockupId!=-2 ) TCS_SGST from payment_trans p " +
                                "inner join salesrecord sales on p.salesid=sales.salesidgivenbyvloc and p.vlocid= sales.saleschannelvlocid " +
                                "inner join ArchiveStockUpInward arc on arc.StockupID= p.stockupId and arc.StockupID= sales.itemid " +
                                "left join Location l on l.LocationID= p.vlocid " +
                                "inner join ItemStyle i on i.StyleID = arc.StyleID " +
                                "inner join Size sz on sz.SizeID= arc.SizeID " +
                                "inner join Lot lot on lot.BagId= arc.BagID " +
                                "left join Column1 col1 on col1.Col1ID= i.Col1 " +
                                "inner join ItemCategory cat on cat.ItemCategoryID= i.ItemCatID " +
                                "inner join  hsnmaster h on h.hsnid = cat.hsnid " +
                                "where p.stockupId!=-1 and p.stockupId!=-2  " +
                                "union all " +
                                "select distinct p.[salesid], convert(varchar, arc.SystemDate, 104) as sys_date,CONVERT(CHAR(4), arc.SystemDate, 100) as sys_Month , CONVERT(CHAR(4), arc.SystemDate, 120)  as sys_year,  i.Title,cast(((arc.mrp* isnull(lot.Percentage,0))/100) as decimal (10,2)) as cp, i.Control3 as Articel, sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,sales.status as System_status,sales.invoiceid,l.Location,p.stockupId,DATEDIFF(m, arc.SystemDate, sales.recordtimestamp) as stockAge, DATEDIFF(m, lot.invoiceDate, sales.recordtimestamp) as lotAge, arc.mrp ,sales.sellingprice as sys_sp ,STUFF((SELECT distinct ', ' + convert(varchar, n.order_date, 104)          from payment_trans n where p.[salesid] = n.[salesid] and  n.[stockupId]!=-1 and n.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_date ,STUFF((SELECT distinct ', ' + cast(sp.sp as NVARCHAR(MAX))          from payment_trans sp where p.[salesid] = sp.[salesid] and  sp.[stockupId]!=-1 and sp.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_sp,   STUFF((SELECT distinct ', ' +cast(s.sellingprice as NVARCHAR(MAX))          from payment_trans t1 inner join salesrecord s on s.salesidgivenbyvloc=t1.salesid and s.saleschannelvlocid=t1.vlocid and s.itemid!=-1          where p.[salesid] = t1.[salesid] and t1.[stockupId]!=-1 and t1.[stockupId]!=-2               FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,2,'') sm,        STUFF((SELECT distinct ', ' + cast(pybamt.Payable_Amoun as NVARCHAR(MAX))          from payment_trans pybamt where p.[salesid] = pybamt.[salesid] and pybamt.[stockupId]!=-1  and pybamt.[stockupId]!=-2              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') Payableamt,  STUFF((SELECT distinct ', ' + t7.type from payment_trans t7          where p.[salesid] = t7.[salesid] and t7.[stockupId]!=-1 and t7.[stockupId]!=-2              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_effecting_status,   STUFF((SELECT distinct ', ' + t2.Merchant_SKU from payment_trans t2          where p.[salesid] = t2.[salesid] and t2.[stockupId]!=-1 and t2.[stockupId]!=-2              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') sku ,   STUFF((SELECT distinct ', ' + tp.Payment_Status from payment_trans tp          where p.[salesid] = tp.[salesid] and tp.[stockupId]!=-1 and tp.[stockupId]!=-2              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') payment_status,       (SELECT distinct sum(chn.channel_commsion)          from payment_trans chn where p.[salesid] = chn.[salesid] and  chn.[stockupId]!=-1 and chn.[stockupId]!=-2            ) chennelcommision,       (SELECT distinct sum(gate.Channel_Gateway)          from payment_trans gate where p.[salesid] = gate.[salesid] and gate.[stockupId]!=-1 and gate.[stockupId]!=-2             ) gatewaycommission,          (SELECT distinct sum(logic.VL_Logistics)          from payment_trans logic where p.[salesid] = logic.[salesid] and logic.[stockupId]!=-1 and logic.[stockupId]!=-2             ) logistic,          (SELECT distinct sum(pent.VLPenalty)          from payment_trans pent where p.[salesid] = pent.[salesid] and pent.[stockupId]!=-1 and pent.[stockupId]!=-2             ) Vlpenelty,             (SELECT distinct sum((k.pmt_cgate_inc_gst+k.pmt_scomm_inc_gst+k.pmt_vllogi_inc_gst+k.pmt_vlpenl_inc_gst+k.pmt_mic1_inc_gst+k.pmt_mic2_inc_gst)-(k.pmt_cgate_ex_gst+k.pmt_scomm_ex_gst+k.pmt_vllogi_ex_gst+k.pmt_mic1_ex_gst+k.pmt_vlpenl_ex_gst+k.pmt_mic2_ex_gst) )          from payment_trans k where p.[salesid] = k.[salesid] and  k.[stockupId]!=-1 and k.[stockupId]!=-2            ) IGST,             (SELECT distinct sum(k3.Payable_Amoun)          from payment_trans k3 where p.[salesid] = k3.[salesid] and  k3.[stockupId]!=-1 and k3.[stockupId]!=-2            ) totaldec,             (SELECT distinct sum((k4.pmt_scomm_inc_cgst+k4.pmt_cgate_inc_cgst+k4.pmt_vllogi_inc_cgst+k4.pmt_mic1_inc_cgst+k4.pmt_mic2_inc_cgst+k4.pmt_chnpent_inc_cgst)-(k4.pmt_scomm_ex_cgst+k4.pmt_cgate_ex_cgst+k4.pmt_vllogi_ex_cgst+k4.pmt_mic1_ex_cgst+k4.pmt_mic2_ex_cgst+k4.pmt_chnpent_exc_cgst) )          from payment_trans k4 where p.[salesid] = k4.[salesid] and  k4.[stockupId]!=-1 and k4.[stockupId]!=-2            ) CGST , (SELECT distinct sum(x.Misc_charge) from payment_trans x where p.[salesid] = x.[salesid] and x.[stockupId]!=-1 AND p.stockupId!=-2) Mis,      (SELECT distinct sum((k5.pmt_scomm_inc_sgst+k5.pmt_cgate_inc_sgst+k5.pmt_vllogi_inc_sgst+k5.pmt_mic1_inc_sgst+k5.pmt_mic2_inc_sgst+k5.pmt_chnpent_inc_sgst)-(k5.pmt_scomm_ex_sgst+k5.pmt_cgate_ex_sgst+k5.pmt_vllogi_ex_sgst+k5.pmt_mic1_ex_sgst+k5.pmt_mic2_ex_cgst+k5.pmt_chnpent_exc_sgst) )          from payment_trans k5 where p.[salesid] = k5.[salesid] and  k5.[stockupId]!=-1  and k5.[stockupId]!=-2  ) SGST ,h.ligst,h.higst,h.lowhighpt, (SELECT distinct sum(g1.TCS_IGST) from payment_trans g1 where p.[salesid] = g1.[salesid] and g1.[stockupId]!=-1 AND g1.stockupId!=-2 ) TCS_IGST, (SELECT distinct sum(g2.TCS_CGST) from payment_trans g2 where p.[salesid] = g2.[salesid] and g2.[stockupId]!=-1 AND g2.stockupId!=-2 ) TCS_CGST, (SELECT distinct sum(g3.TCS_GST) from payment_trans g3 where p.[salesid] = g3.[salesid] and g3.[stockupId]!=-1 AND g3.stockupId!=-2 ) TCS_SGST from payment_trans p " +
                                "inner join salesrecord sales on p.salesid=sales.salesidgivenbyvloc and p.vlocid= sales.saleschannelvlocid " +
                                "inner join ArchiveStockUpInward arc on arc.StockupID= p.stockupId and arc.ArchiveStockupID= sales.archiveid and sales.archiveid!=-1 " +
                                "left join Location l on l.LocationID= p.vlocid " +
                                "inner join ItemStyle i on i.StyleID = arc.StyleID " +
                                "inner join Size sz on sz.SizeID= arc.SizeID " +
                                "inner join Lot lot on lot.BagId= arc.BagID " +
                                "left join Column1 col1 on col1.Col1ID= i.Col1 " +
                                "inner join ItemCategory cat on cat.ItemCategoryID= i.ItemCatID " +
                                "inner join  hsnmaster h on h.hsnid = cat.hsnid " +
                                "where p.stockupId!=-1 and p.stockupId!=-2  " +
                                "union all " +
                                "select distinct p.[salesid], convert(varchar, arc.SystemDate, 104) as sys_date,CONVERT(CHAR(4), arc.SystemDate, 100) as sys_Month , CONVERT(CHAR(4), arc.SystemDate, 120)  as sys_year,  i.Title,cast(((arc.mrp* isnull(lot.Percentage,0))/100) as decimal (10,2)) as cp,i.Control3 as Articel,  sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,sales.status as System_status,sales.invoiceid,l.Location,p.stockupId,DATEDIFF(m, arc.SystemDate, sales.recordtimestamp) as stockAge, DATEDIFF(m, lot.invoiceDate, sales.recordtimestamp) as lotAge, arc.mrp ,sales.sellingprice as sys_sp ,STUFF((SELECT distinct ', ' + convert(varchar, n.order_date, 104)          from payment_trans n where p.[salesid] = n.[salesid] and  n.[stockupId]!=-1 and n.[stockupId]!=-2              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_date ,STUFF((SELECT distinct ', ' + cast(sp.sp as NVARCHAR(MAX))          from payment_trans sp where p.[salesid] = sp.[salesid] and  sp.[stockupId]!=-1 and sp.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_sp,   STUFF((SELECT distinct ', ' +cast(s.sellingprice as NVARCHAR(MAX))          from payment_trans t1 inner join cancelTrans s on s.salesidgivenbyvloc=t1.salesid and s.saleschannelvlocid=t1.vlocid and s.itemid!=-1          where p.[salesid] = t1.[salesid] and t1.[stockupId]!=-1 and t1.[stockupId]!=-2               FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,2,'') sm,        STUFF((SELECT distinct ', ' + cast(pybamt.Payable_Amoun as NVARCHAR(MAX))          from payment_trans pybamt where p.[salesid] = pybamt.[salesid] and pybamt.[stockupId]!=-1  and pybamt.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') Payableamt,  STUFF((SELECT distinct ', ' + t7.type from payment_trans t7          where p.[salesid] = t7.[salesid] and t7.[stockupId]!=-1 and t7.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_effecting_status,   STUFF((SELECT distinct ', ' + t2.Merchant_SKU from payment_trans t2          where p.[salesid] = t2.[salesid] and t2.[stockupId]!=-1 and t2.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') sku ,   STUFF((SELECT distinct ', ' + tp.Payment_Status from payment_trans tp          where p.[salesid] = tp.[salesid] and tp.[stockupId]!=-1 and tp.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') payment_status,       (SELECT distinct sum(chn.channel_commsion)          from payment_trans chn where p.[salesid] = chn.[salesid] and  chn.[stockupId]!=-1 and chn.[stockupId]!=-2            ) chennelcommision,       (SELECT distinct sum(gate.Channel_Gateway)          from payment_trans gate where p.[salesid] = gate.[salesid] and gate.[stockupId]!=-1 and gate.[stockupId]!=-2            ) gatewaycommission,          (SELECT distinct sum(logic.VL_Logistics)          from payment_trans logic where p.[salesid] = logic.[salesid] and logic.[stockupId]!=-1  and logic.[stockupId]!=-2             ) logistic,          (SELECT distinct sum(pent.VLPenalty)          from payment_trans pent where p.[salesid] = pent.[salesid] and pent.[stockupId]!=-1 and pent.[stockupId]!=-2             ) Vlpenelty,             (SELECT distinct sum((k.pmt_cgate_inc_gst+k.pmt_scomm_inc_gst+k.pmt_vllogi_inc_gst+k.pmt_vlpenl_inc_gst+k.pmt_mic1_inc_gst+k.pmt_mic2_inc_gst)-(k.pmt_cgate_ex_gst+k.pmt_scomm_ex_gst+k.pmt_vllogi_ex_gst+k.pmt_mic1_ex_gst+k.pmt_vlpenl_ex_gst+k.pmt_mic2_ex_gst) )          from payment_trans k where p.[salesid] = k.[salesid] and  k.[stockupId]!=-1 and k.[stockupId]!=-2            ) IGST,             (SELECT distinct sum(k3.Payable_Amoun)          from payment_trans k3 where p.[salesid] = k3.[salesid] and  k3.[stockupId]!=-1 and k3.[stockupId]!=-2            ) totaldec,             (SELECT distinct sum((k4.pmt_scomm_inc_cgst+k4.pmt_cgate_inc_cgst+k4.pmt_vllogi_inc_cgst+k4.pmt_mic1_inc_cgst+k4.pmt_mic2_inc_cgst+k4.pmt_chnpent_inc_cgst)-(k4.pmt_scomm_ex_cgst+k4.pmt_cgate_ex_cgst+k4.pmt_vllogi_ex_cgst+k4.pmt_mic1_ex_cgst+k4.pmt_mic2_ex_cgst+k4.pmt_chnpent_exc_cgst) )          from payment_trans k4 where p.[salesid] = k4.[salesid] and  k4.[stockupId]!=-1 and k4.[stockupId]!=-2             ) CGST,  (SELECT distinct sum(x.Misc_charge) from payment_trans x where p.[salesid] = x.[salesid] and x.[stockupId]!=-1 AND p.stockupId!=-2) Mis,    (SELECT distinct sum((k5.pmt_scomm_inc_sgst+k5.pmt_cgate_inc_sgst+k5.pmt_vllogi_inc_sgst+k5.pmt_mic1_inc_sgst+k5.pmt_mic2_inc_sgst+k5.pmt_chnpent_inc_sgst)-(k5.pmt_scomm_ex_sgst+k5.pmt_cgate_ex_sgst+k5.pmt_vllogi_ex_sgst+k5.pmt_mic1_ex_sgst+k5.pmt_mic2_ex_cgst+k5.pmt_chnpent_exc_sgst) )          from payment_trans k5 where p.[salesid] = k5.[salesid] and  k5.[stockupId]!=-1 and k5.[stockupId]!=-2 )  SGST,h.ligst,h.higst,h.lowhighpt,  (SELECT distinct sum(g1.TCS_IGST) from payment_trans g1 where p.[salesid] = g1.[salesid] and g1.[stockupId]!=-1 AND g1.stockupId!=-2 ) TCS_IGST, (SELECT distinct sum(g2.TCS_CGST) from payment_trans g2 where p.[salesid] = g2.[salesid] and g2.[stockupId]!=-1 AND g2.stockupId!=-2 ) TCS_CGST, (SELECT distinct sum(g3.TCS_GST) from payment_trans g3 where p.[salesid] = g3.[salesid] and g3.[stockupId]!=-1 AND g3.stockupId!=-2 ) TCS_SGST from payment_trans p " +
                                " inner join cancelTrans sales on p.salesid=sales.salesidgivenbyvloc and p.vlocid= sales.saleschannelvlocid " +
                                "inner join StockUpInward arc on arc.StockupID= p.stockupId    and arc.StockupID= sales.itemid and sales.itemid!=-1 " +
                                "left join Location l on l.LocationID= p.vlocid " +
                                "inner join ItemStyle i on i.StyleID = arc.StyleID " +
                                "inner join Size sz on sz.SizeID= arc.SizeID " +
                                "inner join Lot lot on lot.BagId= arc.BagID  " +
                                "left join Column1 col1 on col1.Col1ID= i.Col1 " +
                                "inner join ItemCategory cat on cat.ItemCategoryID= i.ItemCatID " +
                                "inner join  hsnmaster h on h.hsnid = cat.hsnid " +
                                "where p.stockupId!=-1 and p.stockupId!=-2 " +
                                "union all " +
                                "select distinct p.[salesid], convert(varchar, arc.SystemDate, 104) as sys_date,CONVERT(CHAR(4), arc.SystemDate, 100) as sys_Month , CONVERT(CHAR(4), arc.SystemDate, 120)  as sys_year,  i.Title,cast(((arc.mrp* isnull(lot.Percentage,0))/100) as decimal (10,2)) as cp, i.Control3 as Articel, sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,sales.status as System_status,sales.invoiceid,l.Location,p.stockupId,DATEDIFF(m, arc.SystemDate, sales.recordtimestamp) as stockAge, DATEDIFF(m, lot.invoiceDate, sales.recordtimestamp) as lotAge, arc.mrp ,sales.sellingprice as sys_sp ,STUFF((SELECT distinct ', ' + convert(varchar, n.order_date, 104)          from payment_trans n where p.[salesid] = n.[salesid] and  n.[stockupId]!=-1  and n.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_date ,STUFF((SELECT distinct ', ' + cast(sp.sp as NVARCHAR(MAX))          from payment_trans sp where p.[salesid] = sp.[salesid] and  sp.[stockupId]!=-1 and sp.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_sp,   STUFF((SELECT distinct ', ' +cast(s.sellingprice as NVARCHAR(MAX))          from payment_trans t1 inner join cancelTrans s on s.salesidgivenbyvloc=t1.salesid and s.saleschannelvlocid=t1.vlocid and s.itemid!=-1          where p.[salesid] = t1.[salesid] and t1.[stockupId]!=-1 and t1.[stockupId]!=-2              FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,2,'') sm,        STUFF((SELECT distinct ', ' + cast(pybamt.Payable_Amoun as NVARCHAR(MAX))          from payment_trans pybamt where p.[salesid] = pybamt.[salesid] and pybamt.[stockupId]!=-1 and pybamt.[stockupId]!=-2              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') Payableamt,  STUFF((SELECT distinct ', ' + t7.type from payment_trans t7          where p.[salesid] = t7.[salesid] and t7.[stockupId]!=-1 and t7.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_effecting_status,   STUFF((SELECT distinct ', ' + t2.Merchant_SKU from payment_trans t2          where p.[salesid] = t2.[salesid] and t2.[stockupId]!=-1 and t2.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') sku ,   STUFF((SELECT distinct ', ' + tp.Payment_Status from payment_trans tp          where p.[salesid] = tp.[salesid] and tp.[stockupId]!=-1 and tp.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') payment_status,       (SELECT distinct sum(chn.channel_commsion)          from payment_trans chn where p.[salesid] = chn.[salesid] and  chn.[stockupId]!=-1 and chn.[stockupId]!=-2            ) chennelcommision,       (SELECT distinct sum(gate.Channel_Gateway)          from payment_trans gate where p.[salesid] = gate.[salesid] and gate.[stockupId]!=-1 and gate.[stockupId]!=-2            ) gatewaycommission,          (SELECT distinct sum(logic.VL_Logistics)          from payment_trans logic where p.[salesid] = logic.[salesid] and logic.[stockupId]!=-1 and logic.[stockupId]!=-2            ) logistic,          (SELECT distinct sum(pent.VLPenalty)          from payment_trans pent where p.[salesid] = pent.[salesid] and pent.[stockupId]!=-1 and pent.[stockupId]!=-2            ) Vlpenelty,             (SELECT distinct sum((k.pmt_cgate_inc_gst+k.pmt_scomm_inc_gst+k.pmt_vllogi_inc_gst+k.pmt_mic1_inc_gst+k.pmt_vlpenl_inc_gst+k.pmt_mic2_inc_gst)-(k.pmt_cgate_ex_gst+k.pmt_scomm_ex_gst+k.pmt_vllogi_ex_gst+k.pmt_vlpenl_ex_gst+k.pmt_mic1_ex_gst+k.pmt_mic2_ex_gst) )          from payment_trans k where p.[salesid] = k.[salesid] and  k.[stockupId]!=-1 and k.[stockupId]!=-2            ) IGST,             (SELECT distinct sum(k3.Payable_Amoun)          from payment_trans k3 where p.[salesid] = k3.[salesid] and  k3.[stockupId]!=-1 and k3.[stockupId]!=-2            ) totaldec,             (SELECT distinct sum((k4.pmt_scomm_inc_cgst+k4.pmt_cgate_inc_cgst+k4.pmt_vllogi_inc_cgst+k4.pmt_mic1_inc_cgst+k4.pmt_mic2_inc_cgst+k4.pmt_chnpent_inc_cgst)-(k4.pmt_scomm_ex_cgst+k4.pmt_cgate_ex_cgst+k4.pmt_vllogi_ex_cgst+k4.pmt_mic1_ex_cgst+k4.pmt_mic2_ex_cgst+k4.pmt_chnpent_exc_cgst) )          from payment_trans k4 where p.[salesid] = k4.[salesid] and  k4.[stockupId]!=-1 and k4.[stockupId]!=-2            ) CGST,  (SELECT distinct sum(x.Misc_charge) from payment_trans x where p.[salesid] = x.[salesid] and x.[stockupId]!=-1 AND p.stockupId!=-2) Mis,   (SELECT distinct sum((k5.pmt_scomm_inc_sgst+k5.pmt_cgate_inc_sgst+k5.pmt_vllogi_inc_sgst+k5.pmt_mic1_inc_sgst+k5.pmt_mic2_inc_sgst+k5.pmt_chnpent_inc_sgst)-(k5.pmt_scomm_ex_sgst+k5.pmt_cgate_ex_sgst+k5.pmt_vllogi_ex_sgst+k5.pmt_mic1_ex_sgst+k5.pmt_mic2_ex_cgst+k5.pmt_chnpent_exc_sgst) )          from payment_trans k5 where p.[salesid] = k5.[salesid] and  k5.[stockupId]!=-1 and k5.[stockupId]!=-2 ) SGST,h.ligst,h.higst,h.lowhighpt, (SELECT distinct sum(g1.TCS_IGST) from payment_trans g1 where p.[salesid] = g1.[salesid] and g1.[stockupId]!=-1 AND g1.stockupId!=-2 ) TCS_IGST, (SELECT distinct sum(g2.TCS_CGST) from payment_trans g2 where p.[salesid] = g2.[salesid] and g2.[stockupId]!=-1 AND g2.stockupId!=-2 ) TCS_CGST, (SELECT distinct sum(g3.TCS_GST) from payment_trans g3 where p.[salesid] = g3.[salesid] and g3.[stockupId]!=-1 AND g3.stockupId!=-2 ) TCS_SGST from payment_trans p " +
                                "inner join cancelTrans sales on p.salesid=sales.salesidgivenbyvloc and p.vlocid= sales.saleschannelvlocid " +
                                "inner join ArchiveStockUpInward arc on arc.StockupID= p.stockupId  and arc.StockupID= sales.itemid " +
                                "left join Location l on l.LocationID= p.vlocid " +
                                "inner join ItemStyle i on i.StyleID = arc.StyleID " +
                                "inner join Size sz on sz.SizeID= arc.SizeID " +
                                "inner join Lot lot on lot.BagId= arc.BagID " +
                                "left join Column1 col1 on col1.Col1ID= i.Col1 " +
                                "inner join ItemCategory cat on cat.ItemCategoryID= i.ItemCatID " +
                                "inner join  hsnmaster h on h.hsnid = cat.hsnid " +
                                "where p.stockupId!=-1   and p.stockupId!=-2  " +
                                "union all " +
                                "select distinct p.[salesid], convert(varchar, arc.SystemDate, 104) as sys_date,CONVERT(CHAR(4), arc.SystemDate, 100) as sys_Month , CONVERT(CHAR(4), arc.SystemDate, 120)  as sys_year,  i.Title,cast(((arc.mrp* isnull(lot.Percentage,0))/100) as decimal (10,2)) as cp, i.Control3 as Articel, sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,sales.status as System_status,sales.invoiceid,l.Location,p.stockupId,DATEDIFF(m, arc.SystemDate, sales.recordtimestamp) as stockAge, DATEDIFF(m, lot.invoiceDate, sales.recordtimestamp) as lotAge, arc.mrp ,sales.sellingprice as sys_sp ,STUFF((SELECT distinct ', ' + convert(varchar, n.order_date, 104)          from payment_trans n where p.[salesid] = n.[salesid] and  n.[stockupId]!=-1  and n.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_date ,STUFF((SELECT distinct ', ' + cast(sp.sp as NVARCHAR(MAX))          from payment_trans sp where p.[salesid] = sp.[salesid] and  sp.[stockupId]!=-1 and sp.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_sp,   STUFF((SELECT distinct ', ' +cast(s.sellingprice as NVARCHAR(MAX))          from payment_trans t1 inner join cancelTrans s on s.salesidgivenbyvloc=t1.salesid and s.saleschannelvlocid=t1.vlocid and s.itemid!=-1          where p.[salesid] = t1.[salesid] and t1.[stockupId]!=-1 and t1.[stockupId]!=-2              FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,2,'') sm,        STUFF((SELECT distinct ', ' + cast(pybamt.Payable_Amoun as NVARCHAR(MAX))          from payment_trans pybamt where p.[salesid] = pybamt.[salesid] and pybamt.[stockupId]!=-1 and pybamt.[stockupId]!=-2              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') Payableamt,  STUFF((SELECT distinct ', ' + t7.type from payment_trans t7          where p.[salesid] = t7.[salesid] and t7.[stockupId]!=-1 and t7.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') pmt_effecting_status,   STUFF((SELECT distinct ', ' + t2.Merchant_SKU from payment_trans t2          where p.[salesid] = t2.[salesid] and t2.[stockupId]!=-1 and t2.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') sku ,   STUFF((SELECT distinct ', ' + tp.Payment_Status from payment_trans tp          where p.[salesid] = tp.[salesid] and tp.[stockupId]!=-1 and tp.[stockupId]!=-2             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') payment_status,       (SELECT distinct sum(chn.channel_commsion)          from payment_trans chn where p.[salesid] = chn.[salesid] and  chn.[stockupId]!=-1 and chn.[stockupId]!=-2            ) chennelcommision,       (SELECT distinct sum(gate.Channel_Gateway)          from payment_trans gate where p.[salesid] = gate.[salesid] and gate.[stockupId]!=-1 and gate.[stockupId]!=-2            ) gatewaycommission,          (SELECT distinct sum(logic.VL_Logistics)          from payment_trans logic where p.[salesid] = logic.[salesid] and logic.[stockupId]!=-1 and logic.[stockupId]!=-2            ) logistic,          (SELECT distinct sum(pent.VLPenalty)          from payment_trans pent where p.[salesid] = pent.[salesid] and pent.[stockupId]!=-1 and pent.[stockupId]!=-2            ) Vlpenelty,             (SELECT distinct sum((k.pmt_cgate_inc_gst+k.pmt_scomm_inc_gst+k.pmt_vllogi_inc_gst+k.pmt_mic1_inc_gst+k.pmt_vlpenl_inc_gst+k.pmt_mic2_inc_gst)-(k.pmt_cgate_ex_gst+k.pmt_scomm_ex_gst+k.pmt_vllogi_ex_gst+k.pmt_vlpenl_ex_gst+k.pmt_mic1_ex_gst+k.pmt_mic2_ex_gst) )          from payment_trans k where p.[salesid] = k.[salesid] and  k.[stockupId]!=-1 and k.[stockupId]!=-2            ) IGST,             (SELECT distinct sum(k3.Payable_Amoun)          from payment_trans k3 where p.[salesid] = k3.[salesid] and  k3.[stockupId]!=-1 and k3.[stockupId]!=-2            ) totaldec,             (SELECT distinct sum((k4.pmt_scomm_inc_cgst+k4.pmt_cgate_inc_cgst+k4.pmt_vllogi_inc_cgst+k4.pmt_mic1_inc_cgst+k4.pmt_mic2_inc_cgst+k4.pmt_chnpent_inc_cgst)-(k4.pmt_scomm_ex_cgst+k4.pmt_cgate_ex_cgst+k4.pmt_vllogi_ex_cgst+k4.pmt_mic1_ex_cgst+k4.pmt_mic2_ex_cgst+k4.pmt_chnpent_exc_cgst) )          from payment_trans k4 where p.[salesid] = k4.[salesid] and  k4.[stockupId]!=-1 and k4.[stockupId]!=-2            ) CGST,  (SELECT distinct sum(x.Misc_charge) from payment_trans x where p.[salesid] = x.[salesid] and x.[stockupId]!=-1 AND p.stockupId!=-2) Mis,   (SELECT distinct sum((k5.pmt_scomm_inc_sgst+k5.pmt_cgate_inc_sgst+k5.pmt_vllogi_inc_sgst+k5.pmt_mic1_inc_sgst+k5.pmt_mic2_inc_sgst+k5.pmt_chnpent_inc_sgst)-(k5.pmt_scomm_ex_sgst+k5.pmt_cgate_ex_sgst+k5.pmt_vllogi_ex_sgst+k5.pmt_mic1_ex_sgst+k5.pmt_mic2_ex_cgst+k5.pmt_chnpent_exc_sgst) )          from payment_trans k5 where p.[salesid] = k5.[salesid] and  k5.[stockupId]!=-1 and k5.[stockupId]!=-2 ) SGST,h.ligst,h.higst,h.lowhighpt, (SELECT distinct sum(g1.TCS_IGST) from payment_trans g1 where p.[salesid] = g1.[salesid] and g1.[stockupId]!=-1 AND g1.stockupId!=-2 ) TCS_IGST, (SELECT distinct sum(g2.TCS_CGST) from payment_trans g2 where p.[salesid] = g2.[salesid] and g2.[stockupId]!=-1 AND g2.stockupId!=-2 ) TCS_CGST, (SELECT distinct sum(g3.TCS_GST) from payment_trans g3 where p.[salesid] = g3.[salesid] and g3.[stockupId]!=-1 AND g3.stockupId!=-2 ) TCS_SGST from payment_trans p " +
                                "inner join cancelTrans sales on p.salesid=sales.salesidgivenbyvloc and p.vlocid= sales.saleschannelvlocid " +
                                "inner join ArchiveStockUpInward arc on arc.StockupID= p.stockupId and arc.ArchiveStockupID= sales.archiveid    and sales.archiveid!=-1   " +
                                "left join Location l on l.LocationID= p.vlocid " +
                                "inner join ItemStyle i on i.StyleID = arc.StyleID " +
                                "inner join Size sz on sz.SizeID= arc.SizeID " +
                                "inner join Lot lot on lot.BagId= arc.BagID " +
                                "left join Column1 col1 on col1.Col1ID= i.Col1 " +
                                "inner join ItemCategory cat on cat.ItemCategoryID= i.ItemCatID " +
                                "inner join  hsnmaster h on h.hsnid = cat.hsnid " +
                                "where p.stockupId!=-1   and p.stockupId!=-2) newtab  ";*/


            Table.Load(command.ExecuteReader());
            command.Parameters.Clear();
            DataTable dtnew = Table.Clone();
            DataTable exc = new DataTable();
            command.CommandText = "select distinct p.salesid,STUFF((SELECT distinct ', ' + convert(varchar, n.order_date, 104)from payment_trans n     where p.[salesid] = n.[salesid] and  n.[stockupId] = -2 FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') ,1,2,'') pmt_date,STUFF((SELECT distinct ', ' + cast(sp.sp as NVARCHAR(MAX))       from payment_trans sp       where p.[salesid] = sp.[salesid] and  sp.[stockupId] = -2 FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') ,1,2,'') pmt_sp,    STUFF((SELECT  ', ' + cast(pybamt.Payable_Amoun as NVARCHAR(MAX))     from payment_trans pybamt     where p.[salesid] = pybamt.[salesid] and pybamt.[stockupId] = -2  FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') Payableamt,     STUFF((SELECT distinct ', ' + t7.type     from payment_trans t7     where p.[salesid] = t7.[salesid] and t7.[stockupId] = -2 FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') ,1,2,'') pmt_effecting_status,   STUFF((SELECT distinct ', ' + t2.Merchant_SKU     from payment_trans t2     where p.[salesid] = t2.[salesid] and t2.[stockupId] = -2 FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') ,1,2,'') sku ,   STUFF((SELECT distinct ', ' + tp.Payment_Status     from payment_trans tp     where p.[salesid] = tp.[salesid] and tp.[stockupId] = -2 FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') ,1,2,'') payment_status,  (SELECT distinct sum(chn.channel_commsion)     from payment_trans chn     where p.[salesid] = chn.[salesid] and chn.[stockupId]=-2  ) chennelcommision,  (SELECT distinct sum(gate.Channel_Gateway)     from payment_trans gate     where p.[salesid] = gate.[salesid] and gate.[stockupId]=-2   ) gatewaycommission,     (SELECT distinct sum(logic.VL_Logistics)     from payment_trans logic     where p.[salesid] = logic.[salesid] and logic.[stockupId]=-2  ) logistic,     (SELECT distinct sum(pent.VLPenalty)     from payment_trans pent     where p.[salesid] = pent.[salesid] and pent.[stockupId]=-2 ) Vlpenelty,        (SELECT distinct sum((ISNULL(g.mp_commission_cgst,0) + ISNULL(g.pg_commission_cgst,0) + ISNULL(g.logistics_cgst,0)+ ISNULL(g.TCS_CGST,0)+ ISNULL(g.penltycgst,0)))     from payment_trans g     where p.[salesid] = g.[salesid] and g.[stockupId]=-2 ) CGST ,       (SELECT distinct sum((ISNULL(h.mp_commission_sgst,0) + ISNULL(h.pg_commission_sgst,0) + ISNULL(h.logistics_sgst,0)+ ISNULL(h.TCS_GST,0)+ ISNULL(h.penltysgst,0)))     from payment_trans h      where p.[salesid] = h.[salesid] and h.[stockupId]=-2 ) SGST ,        (SELECT distinct sum((ISNULL(i.mp_commission_igst,0) + ISNULL(i.pg_commission_igst,0) + ISNULL(i.logistics_igst,0)+ ISNULL(i.TCS_IGST,0)+ ISNULL(i.penltyigst,0)))     from payment_trans i     where p.[salesid] = i.[salesid] and i.[stockupId]=-2  ) IGST,    (SELECT distinct sum(k.Misc_charge)             from payment_trans k             where p.[salesid] = k.[salesid] and k.[stockupId]=-2  ) Mis,l.Location,  (SELECT  sum(n.Payable_Amoun)     from payment_trans n     where p.[salesid] = n.[salesid] and n.[stockupId]=-2  ) totaldec,  (SELECT distinct count(y.Pt_id)     from payment_trans y     where p.[salesid] = y.[salesid] and y.[stockupId]=-2  ) cunt,(SELECT distinct sum(gh.tcsexcgst)             from payment_trans gh             where p.[salesid] = gh.[salesid] and gh.[stockupId]=-2  ) tcsexcgst,(SELECT distinct sum(gl.tcsincgst)             from payment_trans gl             where p.[salesid] = gl.[salesid] and gl.[stockupId]=-2  ) tcsincgst, (SELECT distinct sum(cc1.mp_commission_igst) from payment_trans cc1 where p.[salesid] = cc1.[salesid] and cc1.stockupId=-2 ) Ichgategst, (SELECT distinct sum(cc2.pg_commission_igst) from payment_trans cc2 where p.[salesid] = cc2.[salesid] and  cc2.stockupId=-2 ) Ichncomm, (SELECT distinct sum(cc3.logistics_igst) from payment_trans cc3 where p.[salesid] = cc3.[salesid]  AND cc3.stockupId=-2 ) Ilogicgst, (SELECT distinct sum(cc4.Total_Igst) from payment_trans cc4 where p.[salesid] = cc4.[salesid]  AND cc4.stockupId=-2 ) Itotgst, (SELECT distinct sum(cc5.TCS_IGST) from payment_trans cc5 where p.[salesid] = cc5.[salesid]  AND cc5.stockupId=-2 ) Itcsgst, (SELECT distinct sum(cc6.mp_commission_cgst) from payment_trans cc6 where p.[salesid] = cc6.[salesid]  AND cc6.stockupId=-2 ) Cchgategst, (SELECT distinct sum(cc7.pg_commission_cgst) from payment_trans cc7 where p.[salesid] = cc7.[salesid] and cc7.stockupId=-2 ) Cchncomm, (SELECT distinct sum(cc8.logistics_cgst) from payment_trans cc8 where p.[salesid] = cc8.[salesid]  AND cc8.stockupId=-2 ) Clogicgst, (SELECT distinct sum(cc9.TCS_CGST) from payment_trans cc9 where p.[salesid] = cc9.[salesid] and cc9.stockupId=-2 ) Ctcsgst, (SELECT distinct sum(cc10.Total_Cgst) from payment_trans cc10 where p.[salesid] = cc10.[salesid]  and cc10.stockupId=-2 ) Ctotgst, (SELECT distinct sum(cc11.mp_commission_sgst) from payment_trans cc11 where p.[salesid] = cc11.[salesid]  and cc11.stockupId=-2 ) Schgategst, (SELECT distinct sum(cc12.pg_commission_sgst) from payment_trans cc12 where p.[salesid] = cc12.[salesid] and  cc12.stockupId=-2 ) Schncomm, (SELECT distinct sum(cc13.logistics_sgst) from payment_trans cc13 where p.[salesid] = cc13.[salesid] and  cc13.stockupId=-2 ) Slogicgst, (SELECT distinct sum(cc14.TCS_GST) from payment_trans cc14 where p.[salesid] = cc14.[salesid]  AND cc14.stockupId=-2 ) Stcsgst, (SELECT distinct sum(cc15.Total_Sgst) from payment_trans cc15 where p.[salesid] = cc15.[salesid]  AND cc15.stockupId=-2 ) Stotgst, (SELECT distinct sum(cc16.penltyigst) from payment_trans cc16 where p.[salesid] = cc16.[salesid] and cc16.stockupId=-2 ) Ipengst, (SELECT distinct sum(cc17.penltysgst) from payment_trans cc17 where p.[salesid] = cc17.[salesid] and cc17.stockupId=-2 ) Spengst, (SELECT distinct sum(cc18.penltycgst) from payment_trans cc18 where p.[salesid] = cc18.[salesid]  AND cc18.stockupId=-2 ) Cpengst, (SELECT distinct sum(tty.other_charges) from payment_trans tty where p.[salesid] = tty.[salesid]  AND tty.stockupId=-2 ) otherchrg    from payment_trans p    inner join Location l on l.LocationID=p.vlocid     where p.stockupId= -2 and p.vlocid=@vlocid1 ";
            command.Parameters.AddWithValue("@vlocid1", vlocid);
            exc.Load(command.ExecuteReader());
            command.Parameters.Clear();
            int cunt = Table.Columns.Count;
            int ctn2 = dtnew.Columns.Count;
            foreach (DataRow dr in exc.Rows)
            {
                DataRow row = dtnew.NewRow();



                row["salesid"] = dr["salesid"];//
               
                row["sys_date"] = DBNull.Value;//
                row["sys_Month"] = DBNull.Value;//
                row["sys_year"] = DBNull.Value;//
                row["pmt_date"] =Convert.ToString(dr["pmt_date"]);//
                row["Location"] = Convert.ToString(dr["Location"]);//
                row["invoiceid"] = "0";//
               
                row["stockupId"] = DBNull.Value;//
                row["payment_status"] = Convert.ToString(dr["payment_status"]);//
                row["pmt_effecting_status"] = Convert.ToString(dr["pmt_effecting_status"]);//
                row["System_status"] = "NA";
                row["Lotno"] = DBNull.Value;//
                row["brand"] = DBNull.Value;//
                row["Title"] = DBNull.Value; //
                row["Size1"] = DBNull.Value;//
                row["Articel"] = DBNull.Value;//
                row["lotAge"] = DBNull.Value;//
                row["stockAge"] = DBNull.Value;//
                row["mrp"] = DBNull.Value;//
                row["cp"] = 0;//
                row["pmt_sp"] = Convert.ToString(dr["pmt_sp"]);//
                row["sys_sp"] = DBNull.Value;//
                row["chennelcommision"] = Convert.ToDecimal(dr["chennelcommision"]);//
                row["gatewaycommission"] = Convert.ToDecimal(dr["gatewaycommission"]);//
                row["logistic"] = Convert.ToDecimal(dr["logistic"]);//
                row["Vlpenelty"] = Convert.ToDecimal(dr["Vlpenelty"]);//
                row["IGST"] = Convert.ToDecimal(dr["IGST"]);//
                row["SGST"] = Convert.ToDecimal(dr["SGST"]);//
                row["CGST"] = Convert.ToDecimal(dr["CGST"]);//
                row["Payableamt"] = dr["Payableamt"];//
                row["totaldec"] = dr["totaldec"];
                row["otherchrg"] = dr["otherchrg"];



                //row["sm"] = DBNull.Value;//

                row["sku"] = DBNull.Value;//
                row["Mis"] = Convert.ToDecimal(dr["Mis"]);//

                row["ligst"] = DBNull.Value;//
                row["higst"] = DBNull.Value;//
                row["lowhighpt"] = -1;
                row["tcsincgst"] = dr["tcsincgst"];//
                row["tcsexcgst"] = dr["tcsexcgst"];
                row["cunt"] = dr["cunt"];

                row["ligst"] = DBNull.Value;//
                row["higst"] = DBNull.Value;//
                row["lowhighpt"] = -1;
                row["tcsincgst"] = dr["tcsincgst"];//
                row["tcsexcgst"] = dr["tcsexcgst"];

                row["Ichgategst"] = dr["Ichgategst"];//
                row["Ichncomm"] = dr["Ichncomm"];//
                row["Ilogicgst"] = dr["Ilogicgst"];
                row["Itcsgst"] = dr["Itcsgst"];//
                row["Itotgst"] = dr["Itotgst"];
                row["Cchgategst"] = dr["Cchgategst"];//
                row["Cchncomm"] = dr["Cchncomm"];//
                row["Clogicgst"] = dr["Clogicgst"];
                row["Ctcsgst"] = dr["Ctcsgst"];//
                row["Ctotgst"] = dr["Ctotgst"];
                row["Schgategst"] = dr["Schgategst"];//
                row["Schncomm"] = dr["Schncomm"];//
                row["Slogicgst"] = dr["Slogicgst"];
                row["Stcsgst"] = dr["Stcsgst"];//
                row["Stotgst"] = dr["Stotgst"];

                row["Ipengst"] = dr["Ipengst"];
                row["Spengst"] = dr["Spengst"];//
                row["Cpengst"] = dr["Cpengst"];


                dtnew.Rows.Add(row);
                int aftcunt = Table.Rows.Count;
            }
           
            finaldt = Table.Copy();
            finaldt.Merge(dtnew);


            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();
            return finaldt;
        }
        catch (Exception ex)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
            return null;
        }
       
    }
    public DataTable BindReturn(string type)
    {
        DataTable tab = new DataTable();
        DataTable Table = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("BindReturn");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {

            tab.Columns.Add("Date", typeof(string));
            tab.Columns.Add("Month", typeof(string));
            tab.Columns.Add("Year", typeof(string));
            tab.Columns.Add("Channel", typeof(string));
            tab.Columns.Add("TotalOrder", typeof(decimal));
            tab.Columns.Add("ReturnOrder", typeof(decimal));
            tab.Columns.Add("Amount Charged", typeof(decimal));

            if (type.Equals("Date"))
            {
                //command.CommandText = "select  convert(varchar, p.order_date, 104) as date,CONVERT(CHAR(4), p.order_date, 100) as Month , CONVERT(CHAR(4), p.order_date, 120)  as year,count(p.Pt_id) as totalreturn,l.Location,sum(p.Payable_Amoun) as amount from payment_trans p left join salesrecord s on s.salesidgivenbyvloc=p.salesid left join Location l on l.LocationID=p.vlocid where s.status='RETURN' and p.stockupId!=-1 group by p.order_date,l.Location ";
                /* command.CommandText = "select * from " +
                                       "(select p.order_date, " +
                                       "convert(varchar, p.order_date, 104) as date, CONVERT(CHAR(4), p.order_date, 100) as Month, CONVERT(CHAR(4), p.order_date, 120) as year, count(p.Pt_id) as totalreturn, l.Location, sum(p.Payable_Amoun) as amount " +
                                       "from payment_trans p inner join salesrecord s on s.salesidgivenbyvloc = p.salesid and s.saleschannelvlocid = p.vlocid " +
                                       "left join Location l on l.LocationID = p.vlocid " +
                                       "where s.status = 'RETURN' and p.stockupId != -1 group by p.order_date, l.Location " +
                                       "union all " +
                                       "select p.order_date, " +
                                       "convert(varchar, p.order_date, 104) as date, CONVERT(CHAR(4), p.order_date, 100) as Month, CONVERT(CHAR(4), p.order_date, 120) as year, count(p.Pt_id) as totalreturn, l.Location, sum(p.Payable_Amoun) as amount " +
                                       "from payment_trans p " +
                                       "inner join cancelTrans c on c.saleschannelvlocid = p.vlocid and c.salesidgivenbyvloc = p.salesid " +
                                       "left join Location l on l.LocationID = c.saleschannelvlocid and p.vlocid = l.LocationID " +
                                       "where  c.status = 'RETURN' and p.stockupId != -1 group by p.order_date, l.Location) y ";
 */
                command.CommandText = "select p.Payable_Amoun,p.salesid,l.Location,month(p.order_date) as month,year(p.order_date) as year,p.vlocid, CONVERT(CHAR(4), p.order_date, 100) as Monthname,convert(varchar, p.order_date, 104) as order_date from payment_trans p inner join salesrecord s on s.saleschannelvlocid = p.vlocid and s.salesidgivenbyvloc = p.salesid  inner join Location l on l.LocationID = p.vlocid where s.status = 'RETURN' and p.stockupId != -1 and p.stockupId != -2 group by l.Location,month(p.order_date),year(p.order_date),p.vlocid ,p.salesid,p.Payable_Amoun, CONVERT(CHAR(4), p.order_date, 100)  ,p.order_date " +
                                      "union all " +
                                      "select p.Payable_Amoun,p.salesid,l.Location,month(p.order_date) as month,year(p.order_date) as year,p.vlocid, CONVERT(CHAR(4), p.order_date, 100) as Monthname,convert(varchar, p.order_date, 104) as order_date from payment_trans p inner join cancelTrans s on s.saleschannelvlocid = p.vlocid and s.salesidgivenbyvloc = p.salesid  inner join Location l on l.LocationID = p.vlocid  where s.status = 'RETURN' and p.stockupId != -1 and p.stockupId != -2 group by l.Location,month(p.order_date),year(p.order_date),p.vlocid  ,p.salesid,p.Payable_Amoun, CONVERT(CHAR(4), p.order_date, 100),p.order_date ";


                Table.Load(command.ExecuteReader());
                var _result = from r1 in Table.AsEnumerable()
                              group r1 by new
                              {
                                  month = r1.Field<int>("month"),
                                  year = r1.Field<int>("year"),
                                  vlocid = r1.Field<int>("vlocid"),
                                  order_date = r1.Field<string>("order_date"),
                                  Location = r1.Field<string>("Location"),
                                  Monthname = r1.Field<string>("Monthname")

                              } into g
                              select new
                              {
                                  month = g.Key.month,
                                  year = g.Key.year,
                                  vlocid = g.Key.vlocid,
                                  order_date = g.Key.order_date,
                                  Location = g.Key.Location,
                                  Monthname = g.Key.Monthname,

                                  count = g.Count(),
                                  Payable_Amoun = g.Sum(x => x.Field<decimal>("Payable_Amoun"))
                              };

                DataTable df = new DataTable();
                command.CommandText = "select p.salesid,l.Location,month(p.order_date) as month,year(p.order_date) as year,p.vlocid,convert(varchar, p.order_date, 104) as order_date from payment_trans p inner join salesrecord s on s.saleschannelvlocid = p.vlocid and s.salesidgivenbyvloc = p.salesid  inner join Location l on l.LocationID = p.vlocid where p.stockupId != -1 and p.stockupId != -2   group by l.Location,month(p.order_date),year(p.order_date),p.vlocid ,p.salesid,p.Payable_Amoun,p.order_date " +
                                           "union all " +
                                           "select p.salesid,l.Location,month(p.order_date) as month,year(p.order_date) as year,p.vlocid,convert(varchar, p.order_date, 104) as order_date from payment_trans p inner join cancelTrans s on s.saleschannelvlocid = p.vlocid and s.salesidgivenbyvloc = p.salesid  inner join Location l on l.LocationID = p.vlocid  where p.stockupId != -1 and p.stockupId != -2   group by l.Location,month(p.order_date),year(p.order_date),p.vlocid  ,p.salesid,p.Payable_Amoun,p.order_date ";
                df.Load(command.ExecuteReader());

                var _result2 = from r1 in df.AsEnumerable()
                               group r1 by new
                               {
                                   month = r1.Field<int>("month"),
                                   year = r1.Field<int>("year"),
                                   vlocid = r1.Field<int>("vlocid"),
                                   order_date = r1.Field<string>("order_date"),
                                   Location = r1.Field<string>("Location")

                               } into g
                               select new
                               {
                                   month = g.Key.month,
                                   year = g.Key.year,
                                   vlocid = g.Key.vlocid,
                                   //salesid = g.Key.salesid,
                                   Location = g.Key.Location,
                                   order_date = g.Key.order_date,
                                   count = g.Count(),

                               };
                DataTable t = new DataTable();
                t.Columns.Add("Date", typeof(string));
                t.Columns.Add("Month", typeof(string));
                t.Columns.Add("Year", typeof(string));
                t.Columns.Add("Channel", typeof(string));
                t.Columns.Add("count", typeof(int));
                t.Columns.Add("vlocid", typeof(int));
                t.Columns.Add("Monthname", typeof(string));
                foreach (var r in _result2)
                {
                    DataRow ro = t.NewRow();

                    ro["Date"] = r.order_date.ToString();
                    ro["Month"] = r.month.ToString();
                    ro["Year"] = r.year.ToString();
                    ro["Channel"] = r.Location.ToString();
                    ro["count"] = r.count.ToString();
                    ro["vlocid"] = r.vlocid.ToString();
                    ro["Monthname"] = r.vlocid.ToString();
                    t.Rows.Add(ro);
                }
                string cunt = "";
                foreach (var v in _result)
                {

                    //DataRow [] dr = t.Select("Month ="+v.month.ToString()+ " and Year="+v.year.ToString()+ " and vlocid="+v.vlocid.ToString()+"");


                    var res = from c in t.AsEnumerable()
                              where c.Field<string>("Month") == v.month.ToString() && c.Field<string>("Year") == v.year.ToString() && c.Field<int>("vlocid") == v.vlocid && c.Field<string>("Date") == v.order_date.ToString()
                              select new
                              {
                                  h = c.Field<int>("count")
                              };
                    foreach (var y in res)
                    {
                        cunt = y.h.ToString();
                    }

                    DataRow row = tab.NewRow();


                    row["Date"] = v.order_date.ToString();
                    row["Month"] = v.Monthname.ToString();
                    row["Year"] = v.year.ToString();
                    row["Channel"] = v.Location.ToString();
                    row["TotalOrder"] = Convert.ToDecimal(cunt);
                    row["ReturnOrder"] = Convert.ToDecimal(v.count.ToString());
                    row["Amount Charged"] = Convert.ToDecimal(v.Payable_Amoun.ToString());
                    tab.Rows.Add(row);
                }
            }




        
            else if (type.Equals("Month"))
            {
                /* command.CommandText = "select sum(g.pay) as totalpay,g.Location,g.month,g.year,g.vlocid   from ( " +
                                       "select sum(p.Payable_Amoun) pay,l.Location,month(p.order_date) as month,year(p.order_date) as year,p.vlocid from payment_trans p inner join salesrecord s on s.saleschannelvlocid = p.vlocid and s.salesidgivenbyvloc = p.salesid  inner join Location l on l.LocationID = p.vlocid where s.status = 'RETURN' group by l.Location,month(p.order_date),year(p.order_date),p.vlocid "+
                                       " union all "+
                                       "select sum(p.Payable_Amoun) pay,l.Location,month(p.order_date) as month,year(p.order_date) as year,p.vlocid from payment_trans p inner join cancelTrans s on s.saleschannelvlocid = p.vlocid and s.salesidgivenbyvloc = p.salesid  inner join Location l on l.LocationID = p.vlocid  where s.status = 'RETURN' group by l.Location,month(p.order_date),year(p.order_date),p.vlocid " +
                                       ") g group by g.Location,g.month,g.year,g.vlocid ";*/
                command.CommandText = "select p.Payable_Amoun,p.salesid,l.Location,month(p.order_date) as month,year(p.order_date) as year,p.vlocid, CONVERT(CHAR(4), p.order_date, 100) as Monthname from payment_trans p inner join salesrecord s on s.saleschannelvlocid = p.vlocid and s.salesidgivenbyvloc = p.salesid  inner join Location l on l.LocationID = p.vlocid where s.status = 'RETURN' and p.stockupId!=-1 and p.stockupId!=-2 group by l.Location,month(p.order_date),year(p.order_date),p.vlocid ,p.salesid,p.Payable_Amoun, CONVERT(CHAR(4), p.order_date, 100) " +
                                      " union all" +
                                      " select p.Payable_Amoun,p.salesid,l.Location,month(p.order_date) as month,year(p.order_date) as year,p.vlocid, CONVERT(CHAR(4), p.order_date, 100) as Monthname from payment_trans p inner join cancelTrans s on s.saleschannelvlocid = p.vlocid and s.salesidgivenbyvloc = p.salesid  inner join Location l on l.LocationID = p.vlocid  where s.status = 'RETURN' and p.stockupId!=-1 and p.stockupId!=-2 group by l.Location,month(p.order_date),year(p.order_date),p.vlocid  ,p.salesid,p.Payable_Amoun, CONVERT(CHAR(4), p.order_date, 100) ";




                Table.Load(command.ExecuteReader());


                var _result = from r1 in Table.AsEnumerable()
                              group r1 by new
                              {
                                  month = r1.Field<int>("month"),
                                  year = r1.Field<int>("year"),
                                  vlocid = r1.Field<int>("vlocid"),
                                  //salesid = r1.Field<string>("salesid"),
                                  Location = r1.Field<string>("Location"),
                                  Monthname = r1.Field<string>("Monthname")

                              } into g
                              select new
                              {
                                  month = g.Key.month,
                                  year = g.Key.year,
                                  vlocid = g.Key.vlocid,
                                  //salesid = g.Key.salesid,
                                  Location = g.Key.Location,
                                  Monthname = g.Key.Monthname,

                                  count = g.Count(),
                                  Payable_Amoun = g.Sum(x => x.Field<decimal>("Payable_Amoun"))
                              };
                
                DataTable df = new DataTable();
                command.CommandText = "select p.salesid,l.Location,month(p.order_date) as month,year(p.order_date) as year,p.vlocid from payment_trans p inner join salesrecord s on s.saleschannelvlocid = p.vlocid and s.salesidgivenbyvloc = p.salesid  inner join Location l on l.LocationID = p.vlocid where p.stockupId != -1 and p.stockupId != -2   group by l.Location,month(p.order_date),year(p.order_date),p.vlocid ,p.salesid,p.Payable_Amoun " +
                                           "union all " +
                                           "select p.salesid,l.Location,month(p.order_date) as month,year(p.order_date) as year,p.vlocid from payment_trans p inner join cancelTrans s on s.saleschannelvlocid = p.vlocid and s.salesidgivenbyvloc = p.salesid  inner join Location l on l.LocationID = p.vlocid  where p.stockupId != -1 and p.stockupId != -2   group by l.Location,month(p.order_date),year(p.order_date),p.vlocid  ,p.salesid,p.Payable_Amoun ";
                df.Load(command.ExecuteReader());

                var _result2 = from r1 in df.AsEnumerable()
                              group r1 by new
                              {
                                  month = r1.Field<int>("month"),
                                  year = r1.Field<int>("year"),
                                  vlocid = r1.Field<int>("vlocid"),
                                  
                                  Location = r1.Field<string>("Location")

                              } into g
                              select new
                              {
                                  month = g.Key.month,
                                  year = g.Key.year,
                                  vlocid = g.Key.vlocid,
                                  //salesid = g.Key.salesid,
                                  Location = g.Key.Location,

                                  count = g.Count(),
                                  
                              };
                DataTable t = new DataTable();
               
                t.Columns.Add("Month", typeof(string));
                t.Columns.Add("Year", typeof(string));
                t.Columns.Add("Channel", typeof(string));
                t.Columns.Add("count", typeof(int));
                t.Columns.Add("vlocid", typeof(int));
                t.Columns.Add("Monthname", typeof(string));
                foreach (var r in _result2)
                {
                    DataRow ro = t.NewRow();
                    
                    ro["Month"] = r.month.ToString();
                    ro["Year"] = r.year.ToString();
                    ro["Channel"] = r.Location.ToString();
                    ro["count"] = r.count.ToString();
                    ro["vlocid"] = r.vlocid.ToString();
                    ro["Monthname"] = r.vlocid.ToString();
                    t.Rows.Add(ro);
                }
                string cunt="";
                foreach (var v in _result)
                {
                    
                    //DataRow [] dr = t.Select("Month ="+v.month.ToString()+ " and Year="+v.year.ToString()+ " and vlocid="+v.vlocid.ToString()+"");


                    var res = from c in t.AsEnumerable()
                                    where c.Field<string>("Month") ==v.month.ToString() && c.Field<string>("Year") == v.year.ToString() && c.Field<int>("vlocid") == v.vlocid
                                    select new
                                    {
                                     h = c.Field<int>("count")
                                    };
                   foreach(var y in res)
                    {
                        cunt = y.h.ToString();
                    }
                 
                    DataRow row = tab.NewRow();


                    row["Date"] = "";
                    row["Month"] = v.Monthname.ToString();
                    row["Year"] = v.year.ToString();
                    row["Channel"] = v.Location.ToString();
                    row["TotalOrder"] = Convert.ToDecimal(cunt);
                    row["ReturnOrder"] = Convert.ToDecimal(v.count.ToString());
                    row["Amount Charged"] = Convert.ToDecimal(v.Payable_Amoun.ToString());
                    tab.Rows.Add(row);
                }
            }





            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();
            return tab;
        }
        catch (Exception ex)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
            return null;
        }

    }
    public DataTable getwrongvloc(string vloc)
    {
        DataTable Table = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("getwrongvloc");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText= "select distinct p.salesid,s.saleschannelvlocid,s.sid as id,1 as frm,l.Location,s.salesUserId,u.username from payment_trans p inner join salesrecord s on s.salesidgivenbyvloc = p.salesid inner join Location l on l.LocationID = p.vlocid inner join login u on u.userid=s.salesUserId where p.stockupId != -1 and p.stockupId != -2 and p.vlocid =@vlocid1 and s.saleschannelvlocid !=@vlocid2 " +
                                 "union all " +
                                 "select distinct p.salesid,s.saleschannelvlocid,s.cid as id,2 as frm,l.Location,s.salesUserId,u.username from payment_trans p inner join cancelTrans s on s.salesidgivenbyvloc = p.salesid inner join Location l on l.LocationID = p.vlocid inner join login u on u.userid=s.salesUserId  where p.stockupId != -1 and p.stockupId != -2 and p.vlocid =@vlocid3 and s.saleschannelvlocid !=@vlocid4 ";
            command.Parameters.AddWithValue("@vlocid1", vloc);
            command.Parameters.AddWithValue("@vlocid2", vloc);
            command.Parameters.AddWithValue("@vlocid3", vloc);
            command.Parameters.AddWithValue("@vlocid4", vloc);

            Table.Load(command.ExecuteReader());

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
        return Table;
    }
    public int updatelocation(DataTable tb,string vloc)
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
        transaction = connection.BeginTransaction("updatelocation");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            foreach (DataRow dr in tb.Rows)
            {
                string comming = dr["frm"].ToString();
                if (comming.Equals("1"))
                {
                    command.CommandText = "Update  salesrecord set saleschannelvlocid=@saleschannelvlocid where sid=@sid ";
                    command.Parameters.AddWithValue("@saleschannelvlocid", vloc);
                    command.Parameters.AddWithValue("@sid", dr["id"].ToString());
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
                else
                {
                    command.CommandText = "Update  cancelTrans set saleschannelvlocid=@saleschannelvlocid where cid=@cid ";
                    command.Parameters.AddWithValue("@saleschannelvlocid", vloc);
                    command.Parameters.AddWithValue("@cid", dr["id"].ToString());
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
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