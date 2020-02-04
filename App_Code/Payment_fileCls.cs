using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

/// <summary>
/// Summary description for Payment_fileCls
/// </summary>
public class Payment_fileCls
{
    string userId = HttpContext.Current.Session["login"].ToString();//id of logged in admin
    string userName = HttpContext.Current.Session["userName"].ToString();//id of logged in admin

    public Payment_fileCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable insertpaymentfile(DataTable dt, string vlocid,string commingfrom)
    {
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }
        int line = 0;
        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("insertpaymentfile");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            //DataTable table = new DataTable();
            //table.Columns.Add("saleschannelvlocid");
            //table.Columns.Add("salesid");
            //table.Columns.Add("sid");
            //table.Columns.Add("frm");
            //table.Columns.Add("Location");
            string checksalesprice;
            DataTable post = new DataTable();
            command.CommandText = "Select checkgst,chksalsprice,chckstatus,chckchncomgst,chckchngategst," +
                "chckchnloggst,chckchnpenlgst,chckchnmic1gst,chckchnmic2gst,shipping_service,transfer,orders,refund from Pymt_trans_setting where vloc_id=@vloc_id ";
            command.Parameters.AddWithValue("@vloc_id", vlocid);
            post.Load(command.ExecuteReader());
            command.Parameters.Clear();
            //int result = 0;
            DataTable SNTalreayindb = dt.Clone();
            DataTable did = new DataTable();
            did.Columns.Add("id");
            foreach (DataRow dr in dt.Rows)
            {
                string stockupid;
                string sys_status;

                string dr1 = dr["type"].ToString();

                DataTable dtcheck = new DataTable();
                //command.CommandText = "select * from ( select a.StockupID as id from  ArchiveStockUpInward a  inner  join salesrecord s  on a.ArchiveStockupID=s.archiveid where s.salesidgivenbyvloc=@salesidgivenbyvloc1  union all  select i.StockupID from StockUpInward i  inner join salesrecord s1 on s1.itemid=i.StockupID where  s1.salesidgivenbyvloc=@salesidgivenbyvloc2  ) y";
                //command.CommandText = "select s.salesidgivenbyvloc,a.StockupID,stk.StockupID as id from salesrecord s   left join ArchiveStockUpInward a on a.ArchiveStockupID=s.archiveid left join StockUpInward stk on stk.StockupID=s.itemid where s.salesidgivenbyvloc=@salesidgivenbyvloc";
                command.CommandText = "select * from " +
                                     "(select Distinct a.StockupID as id,s.status from ArchiveStockUpInward a  inner join salesrecord s  on a.ArchiveStockupID = s.archiveid where s.salesidgivenbyvloc =@salesidgivenbyvloc1 and s.archiveid != -1 " +
                                     "union all " +
                                     "select Distinct a.StockupID as id,s.status from ArchiveStockUpInward a  inner join salesrecord s  on a.StockupID = s.itemid where s.salesidgivenbyvloc =@salesidgivenbyvloc5 " +
                                     "union all " +
                                     "select  Distinct i.StockupID as id,s1.status from StockUpInward i  inner join salesrecord s1 on s1.itemid = i.StockupID  where s1.salesidgivenbyvloc =@salesidgivenbyvloc2 and s1.itemid != -1 " +
                                     "union all " +
                                     "select Distinct a.StockupID as id,s.status from ArchiveStockUpInward a  inner join cancelTrans s  on a.ArchiveStockupID = s.archiveid where s.salesidgivenbyvloc = @salesidgivenbyvloc3 and s.archiveid != -1 " +
                                     "union all " +
                                     "select Distinct i.StockupID as id,s1.status from StockUpInward i  inner join cancelTrans s1 on s1.itemid = i.StockupID  where s1.salesidgivenbyvloc =@salesidgivenbyvloc4 and s1.itemid != -1" +
                                     "union all " +
                                     "select Distinct a.StockupID as id,s.status from ArchiveStockUpInward a  inner join cancelTrans s  on a.StockupID = s.itemid where s.salesidgivenbyvloc =@salesidgivenbyvloc6 ) y" +

                command.Parameters.AddWithValue("@salesidgivenbyvloc1", dr["salesid"]);
                command.Parameters.AddWithValue("@salesidgivenbyvloc2", dr["salesid"]);
                command.Parameters.AddWithValue("@salesidgivenbyvloc3", dr["salesid"]);
                command.Parameters.AddWithValue("@salesidgivenbyvloc4", dr["salesid"]);
                command.Parameters.AddWithValue("@salesidgivenbyvloc5", dr["salesid"]);
                command.Parameters.AddWithValue("@salesidgivenbyvloc6", dr["salesid"]);
                dtcheck.Load(command.ExecuteReader());
                command.Parameters.Clear();
                if (!dtcheck.Rows.Count.Equals(0))
                {
                    if (dtcheck.Rows.Count.Equals(1))
                    {
                        stockupid = dtcheck.Rows[0]["id"].ToString();
                        sys_status = dtcheck.Rows[0]["status"].ToString();
                    }
                    else
                    {
                        stockupid = "-1";
                        sys_status = "NA";
                    }
                }
                else
                {
                    stockupid = "-2";
                    sys_status = "NA";
                }
                string salesid = dr["salesid"].ToString();
                int p_id;
                DataTable checksalesntype = new DataTable();
                
                if (!commingfrom.Equals("comingfromduplicate"))
                {
                   
                    command.CommandText = "select p.salesid,p.type  from payment_trans p where p.vlocid=@vlocid and p.salesid=@salesid and p.type=@type";
                    command.Parameters.AddWithValue("@vlocid", vlocid);
                    command.Parameters.AddWithValue("@salesid", dr["salesid"]);
                    command.Parameters.AddWithValue("@type", dr["type"]);
                    checksalesntype.Load(command.ExecuteReader());
                    command.Parameters.Clear();
                }
                else
                {
                    checksalesntype.Columns.Add("salesid");
                    checksalesntype.Columns.Add("type");
                }
               
                if (checksalesntype.Rows.Count.Equals(0))
                {
                    if (dr["salesid"].ToString().Equals(""))
                    {
                        SNTalreayindb.Rows.Add(dr.ItemArray);
                    }
                    else
                    {
                        command.CommandText = "Insert Into payment_trans (stockupId,salesid,type,vlocid,sp,channel_commsion,Channel_Gateway," +
                                                                   "VL_Logistics,VLPenalty,Pack_Charges_VL_Misc,Spcl_pack_chrgs_vlMics,mp_commission_cgst,pg_commission_cgst,logistics_cgst," +
                                                                   "TCS_CGST,mp_commission_igst,pg_commission_igst,logistics_igst,TCS_IGST,mp_commission_sgst,pg_commission_sgst,logistics_sgst,TCS_GST," +
                                                                   "Total_Cgst,Total_Igst,Total_Sgst,totalsales_taxliable_gstbeforeadustingTCS,Payment_Status,Merchant_SKU,Settlement_Date,Payment_Type,Payable_Amoun," +
                                                                   "PG_UTR,packfee_for_return,pymtfee_for_return,logistfee_for_return,reverse_logistfee_for_return,order_date,spcl_pack_fee_for_return,Misc_charge,other_charges,closing_fees,penltycgst,penltysgst,penltyigst,TCS,sys_status,productid,productname)" +
                                                                   "values(@stockupId,@salesid,@type,@vlocid,@sp,@channel_commsion,@Channel_Gateway," +
                                                                   "@VL_Logistics,@VLPenalty,@Pack_Charges_VL_Misc,@Spcl_pack_chrgs_vlMics,@mp_commission_cgst,@pg_commission_cgst," +
                                                                   "@logistics_cgst,@TCS_CGST,@mp_commission_igst,@pg_commission_igst,@logistics_igst,@TCS_IGST,@mp_commission_sgst," +
                                                                   "@pg_commission_sgst,@logistics_sgst,@TCS_GST,@Total_Cgst,@Total_Igst,@Total_Sgst,@totalsales_taxliable_gstbeforeadustingTCS," +
                                                                   "@Payment_Status,@Merchant_SKU,@Settlement_Date,@Payment_Type,@Payable_Amoun,@PG_UTR,@packfee_for_return,@pymtfee_for_return," +
                                                                   "@logistfee_for_return,@reverse_logistfee_for_return,@order_date,@spcl_pack_fee_for_return,@Misc_charge,@other_charges,@closing_fees,@penltycgst,@penltysgst,@penltyigst,@TCS,@sys_status,@productid,@productname)" +
                                                                   "SELECT CAST(scope_identity() AS int)";


                        int chn = 0;
                        int gate = 0;
                        int logic = 0;
                        int penl = 0;
                        int mis1 = 0;
                        int payable = 0;

                        DataColumnCollection columns = dt.Columns;
                        if (post.Rows[0]["chckstatus"].ToString().Equals("True"))
                        {
                            //shipping service
                            if (post.Rows[0]["shipping_service"].ToString().Equals("1"))
                            {
                                if (columns.Contains("Payable_Amoun"))
                                {
                                    command.Parameters.AddWithValue("@channel_commsion", dr["Payable_Amoun"].Equals("") ? 0 : Convert.ToDecimal(dr["Payable_Amoun"]));

                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@channel_commsion", 0.00);
                                }
                                chn = 1;
                            }
                            else if (post.Rows[0]["shipping_service"].ToString().Equals("2"))
                            {
                                if (columns.Contains("Payable_Amoun"))
                                {
                                    command.Parameters.AddWithValue("@Channel_Gateway", dr["Payable_Amoun"].Equals("") ? 0 : Convert.ToDecimal(dr["Payable_Amoun"]));
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@Channel_Gateway", 0.00);
                                }
                                gate = 1;
                            }
                            else if (post.Rows[0]["shipping_service"].ToString().Equals("3"))
                            {
                                if (columns.Contains("Payable_Amoun"))
                                {
                                    command.Parameters.AddWithValue("@VL_Logistics", dr["Payable_Amoun"].Equals("") ? 0 : Convert.ToDecimal(dr["Payable_Amoun"]));
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@VL_Logistics", 0.00);
                                }
                                logic = 1;
                            }
                            else if (post.Rows[0]["shipping_service"].ToString().Equals("4"))
                            {
                                if (columns.Contains("Payable_Amoun"))
                                {
                                    string p = dr["Payable_Amoun"].ToString();
                                    command.Parameters.AddWithValue("@VLPenalty", dr["Payable_Amoun"].Equals("") ? 0 : Convert.ToDouble(dr["Payable_Amoun"]));
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@VLPenalty", 0.00);
                                }
                                penl = 1;
                            }
                            else if (post.Rows[0]["shipping_service"].ToString().Equals("5"))
                            {
                                if (columns.Contains("Payable_Amoun"))
                                {
                                    string p = dr["Payable_Amoun"].ToString();
                                    command.Parameters.AddWithValue("@Misc_charge", dr["Payable_Amoun"].Equals("") ? 0 : Convert.ToDouble(dr["Payable_Amoun"]));
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@Misc_charge", 0.00);
                                }
                                mis1 = 1;
                            }
                            else if (post.Rows[0]["shipping_service"].ToString().Equals("6"))
                            {
                                if (columns.Contains("Payable_Amoun"))
                                {
                                    string p = dr["Payable_Amoun"].ToString();
                                    command.Parameters.AddWithValue("@Payable_Amoun", dr["Payable_Amoun"].Equals("") ? 0 : Convert.ToDouble(dr["Payable_Amoun"]));
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@Payable_Amoun", 0.00);
                                }
                                payable = 1;
                            }
                            else
                            {

                            }
                            //transfer
                            if (post.Rows[0]["transfer"].ToString().Equals("1"))
                            {
                                if (columns.Contains("Payable_Amoun"))
                                {
                                    command.Parameters.AddWithValue("@channel_commsion", dr["Payable_Amoun"].Equals("") ? 0 : Convert.ToDecimal(dr["Payable_Amoun"]));

                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@channel_commsion", 0.00);
                                }
                                chn = 1;
                            }
                            else if (post.Rows[0]["transfer"].ToString().Equals("2"))
                            {
                                if (columns.Contains("Payable_Amoun"))
                                {
                                    command.Parameters.AddWithValue("@Channel_Gateway", dr["Payable_Amoun"].Equals("") ? 0 : Convert.ToDecimal(dr["Payable_Amoun"]));
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@Channel_Gateway", 0.00);
                                }
                                gate = 1;
                            }
                            else if (post.Rows[0]["transfer"].ToString().Equals("3"))
                            {
                                if (columns.Contains("Payable_Amoun"))
                                {
                                    command.Parameters.AddWithValue("@VL_Logistics", dr["Payable_Amoun"].Equals("") ? 0 : Convert.ToDecimal(dr["Payable_Amoun"]));
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@VL_Logistics", 0.00);
                                }
                                logic = 1;
                            }
                            else if (post.Rows[0]["transfer"].ToString().Equals("4"))
                            {
                                if (columns.Contains("Payable_Amoun"))
                                {
                                    string p = dr["Payable_Amoun"].ToString();
                                    command.Parameters.AddWithValue("@VLPenalty", dr["Payable_Amoun"].Equals("") ? 0 : Convert.ToDouble(dr["Payable_Amoun"]));
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@VLPenalty", 0.00);
                                }
                                penl = 1;
                            }
                            else if (post.Rows[0]["transfer"].ToString().Equals("5"))
                            {
                                if (columns.Contains("Payable_Amoun"))
                                {
                                    string p = dr["Payable_Amoun"].ToString();
                                    command.Parameters.AddWithValue("@Misc_charge", dr["Payable_Amoun"].Equals("") ? 0 : Convert.ToDouble(dr["Payable_Amoun"]));
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@Misc_charge", 0.00);
                                }
                                mis1 = 1;
                            }
                            else if (post.Rows[0]["transfer"].ToString().Equals("6"))
                            {
                                if (columns.Contains("Payable_Amoun"))
                                {
                                    string p = dr["Payable_Amoun"].ToString();
                                    command.Parameters.AddWithValue("@Payable_Amoun", dr["Payable_Amoun"].Equals("") ? 0 : Convert.ToDouble(dr["Payable_Amoun"]));
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@Payable_Amoun", 0.00);
                                }
                                payable = 1;
                            }
                            else
                            {

                            }
                            //refund
                            if (post.Rows[0]["refund"].ToString().Equals("1"))
                            {
                                if (columns.Contains("Payable_Amoun"))
                                {
                                    command.Parameters.AddWithValue("@channel_commsion", dr["Payable_Amoun"].Equals("") ? 0 : Convert.ToDecimal(dr["Payable_Amoun"]));

                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@channel_commsion", 0.00);
                                }
                                chn = 1;
                            }
                            else if (post.Rows[0]["refund"].ToString().Equals("2"))
                            {
                                if (columns.Contains("Payable_Amoun"))
                                {
                                    command.Parameters.AddWithValue("@Channel_Gateway", dr["Payable_Amoun"].Equals("") ? 0 : Convert.ToDecimal(dr["Payable_Amoun"]));
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@Channel_Gateway", 0.00);
                                }
                                gate = 1;
                            }
                            else if (post.Rows[0]["refund"].ToString().Equals("3"))
                            {
                                if (columns.Contains("Payable_Amoun"))
                                {
                                    command.Parameters.AddWithValue("@VL_Logistics", dr["Payable_Amoun"].Equals("") ? 0 : Convert.ToDecimal(dr["Payable_Amoun"]));
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@VL_Logistics", 0.00);
                                }
                                logic = 1;
                            }
                            else if (post.Rows[0]["refund"].ToString().Equals("4"))
                            {
                                if (columns.Contains("Payable_Amoun"))
                                {
                                    string p = dr["Payable_Amoun"].ToString();
                                    command.Parameters.AddWithValue("@VLPenalty", dr["Payable_Amoun"].Equals("") ? 0 : Convert.ToDouble(dr["Payable_Amoun"]));
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@VLPenalty", 0.00);
                                }
                                penl = 1;
                            }
                            else if (post.Rows[0]["refund"].ToString().Equals("5"))
                            {
                                if (columns.Contains("Payable_Amoun"))
                                {
                                    string p = dr["Payable_Amoun"].ToString();
                                    command.Parameters.AddWithValue("@Misc_charge", dr["Payable_Amoun"].Equals("") ? 0 : Convert.ToDouble(dr["Payable_Amoun"]));
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@Misc_charge", 0.00);
                                }
                                mis1 = 1;
                            }
                            else if (post.Rows[0]["refund"].ToString().Equals("6"))
                            {
                                if (columns.Contains("Payable_Amoun"))
                                {
                                    string p = dr["Payable_Amoun"].ToString();
                                    command.Parameters.AddWithValue("@Payable_Amoun", dr["Payable_Amoun"].Equals("") ? 0 : Convert.ToDouble(dr["Payable_Amoun"]));
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@Payable_Amoun", 0.00);
                                }
                                payable = 1;
                            }

                            else
                            {

                            }
                            //order
                            if (post.Rows[0]["orders"].ToString().Equals("1"))
                            {
                                if (columns.Contains("Payable_Amoun"))
                                {
                                    command.Parameters.AddWithValue("@channel_commsion", dr["Payable_Amoun"].Equals("") ? 0 : Convert.ToDecimal(dr["Payable_Amoun"]));

                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@channel_commsion", 0.00);
                                }
                                chn = 1;
                            }
                            else if (post.Rows[0]["orders"].ToString().Equals("2"))
                            {
                                if (columns.Contains("Payable_Amoun"))
                                {
                                    command.Parameters.AddWithValue("@Channel_Gateway", dr["Payable_Amoun"].Equals("") ? 0 : Convert.ToDecimal(dr["Payable_Amoun"]));
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@Channel_Gateway", 0.00);
                                }
                                gate = 1;
                            }
                            else if (post.Rows[0]["orders"].ToString().Equals("3"))
                            {
                                if (columns.Contains("Payable_Amoun"))
                                {
                                    command.Parameters.AddWithValue("@VL_Logistics", dr["Payable_Amoun"].Equals("") ? 0 : Convert.ToDecimal(dr["Payable_Amoun"]));
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@VL_Logistics", 0.00);
                                }
                                logic = 1;
                            }
                            else if (post.Rows[0]["orders"].ToString().Equals("4"))
                            {
                                if (columns.Contains("Payable_Amoun"))
                                {
                                    string p = dr["Payable_Amoun"].ToString();
                                    command.Parameters.AddWithValue("@VLPenalty", dr["Payable_Amoun"].Equals("") ? 0 : Convert.ToDouble(dr["Payable_Amoun"]));
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@VLPenalty", 0.00);
                                }
                                penl = 1;

                            }
                            else if (post.Rows[0]["orders"].ToString().Equals("5"))
                            {
                                if (columns.Contains("Payable_Amoun"))
                                {
                                    string p = dr["Payable_Amoun"].ToString();
                                    command.Parameters.AddWithValue("@Misc_charge", dr["Payable_Amoun"].Equals("") ? 0 : Convert.ToDouble(dr["Payable_Amoun"]));
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@Misc_charge", 0.00);
                                }
                                mis1 = 1;
                            }
                            else if (post.Rows[0]["orders"].ToString().Equals("6"))
                            {
                                if (columns.Contains("Payable_Amoun"))
                                {
                                    string p = dr["Payable_Amoun"].ToString();
                                    command.Parameters.AddWithValue("@Payable_Amoun", dr["Payable_Amoun"].Equals("") ? 0 : Convert.ToDouble(dr["Payable_Amoun"]));
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@Payable_Amoun", 0.00);
                                }
                                payable = 1;
                            }
                            else
                            {

                            }
                            if (chn != 1)
                            {
                                command.Parameters.AddWithValue("@channel_commsion", 0.00);
                            }
                            if (gate != 1)
                            {
                                command.Parameters.AddWithValue("@Channel_Gateway", 0.00);
                            }
                            if (logic != 1)
                            {
                                command.Parameters.AddWithValue("@VL_Logistics", 0.00);
                            }
                            if (penl != 1)
                            {
                                command.Parameters.AddWithValue("@VLPenalty", 0.00);
                            }
                            if (mis1 != 1)
                            {
                                command.Parameters.AddWithValue("@Misc_charge", 0.00);
                            }
                            if (payable != 1)
                            {
                                command.Parameters.AddWithValue("@Payable_Amoun", 0.00);
                            }
                        }
                        else
                        {
                            if (columns.Contains("channel_commsion"))
                            {
                                command.Parameters.AddWithValue("@channel_commsion", dr["channel_commsion"].Equals("") ? 0 : Convert.ToDecimal(dr["channel_commsion"]));

                            }
                            else
                            {
                                command.Parameters.AddWithValue("@channel_commsion", 0.00);
                            }
                            if (columns.Contains("Channel_Gateway"))
                            {
                                command.Parameters.AddWithValue("@Channel_Gateway", dr["Channel_Gateway"].Equals("") ? 0 : Convert.ToDecimal(dr["Channel_Gateway"]));
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Channel_Gateway", 0.00);
                            }
                            if (columns.Contains("VL_Logistics"))
                            {
                                command.Parameters.AddWithValue("@VL_Logistics", dr["VL_Logistics"].Equals("") ? 0 : Convert.ToDecimal(dr["VL_Logistics"]));
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@VL_Logistics", 0.00);
                            }
                            if (columns.Contains("VLPenalty"))
                            {
                                string p = dr["VLPenalty"].ToString();
                                command.Parameters.AddWithValue("@VLPenalty", dr["VLPenalty"].Equals("") ? 0 : Convert.ToDouble(dr["VLPenalty"]));
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@VLPenalty", 0.00);
                            }
                            if (columns.Contains("Payable_Amoun"))
                            {
                                command.Parameters.AddWithValue("@Payable_Amoun", dr["Payable_Amoun"].Equals("") ? 0 : Convert.ToDecimal(dr["Payable_Amoun"]));
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Payable_Amoun", 0.00);
                            }
                            if (columns.Contains("Misc_charge"))
                            {
                                command.Parameters.AddWithValue("@Misc_charge", dr["Misc_charge"].Equals("") ? 0 : Convert.ToDecimal(dr["Misc_charge"]));
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Misc_charge", 0.00);
                            }
                        }


                        command.Parameters.AddWithValue("@stockupId", stockupid);
                        command.Parameters.AddWithValue("@salesid", dr["salesid"]);
                        command.Parameters.AddWithValue("@type", dr["type"]);
                        command.Parameters.AddWithValue("@vlocid", vlocid);
                        command.Parameters.AddWithValue("@sp", dr["sp"]);

                        if (columns.Contains("Pack_Charges_VL_Misc"))
                        {
                            command.Parameters.AddWithValue("@Pack_Charges_VL_Misc", dr["Pack_Charges_VL_Misc"].Equals("") ? 0 : Convert.ToDecimal(dr["Pack_Charges_VL_Misc"]));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Pack_Charges_VL_Misc", 0.00);
                        }
                        if (columns.Contains("Spcl_pack_chrgs_vlMics"))
                        {
                            command.Parameters.AddWithValue("@Spcl_pack_chrgs_vlMics", dr["Spcl_pack_chrgs_vlMics"].Equals("") ? 0 : Convert.ToDecimal(dr["Spcl_pack_chrgs_vlMics"]));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Spcl_pack_chrgs_vlMics", 0.00);
                        }
                        if (columns.Contains("mp_commission_cgst"))
                        {
                            command.Parameters.AddWithValue("@mp_commission_cgst", dr["mp_commission_cgst"].Equals("") ? 0 : Convert.ToDecimal(dr["mp_commission_cgst"]));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@mp_commission_cgst", 0.00);
                        }
                        if (columns.Contains("pg_commission_cgst"))
                        {
                            command.Parameters.AddWithValue("@pg_commission_cgst", dr["pg_commission_cgst"].Equals("") ? 0 : Convert.ToDecimal(dr["pg_commission_cgst"]));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@pg_commission_cgst", 0.00);
                        }
                        if (columns.Contains("logistics_cgst"))
                        {
                            command.Parameters.AddWithValue("@logistics_cgst", dr["logistics_cgst"].Equals("") ? 0 : Convert.ToDecimal(dr["logistics_cgst"]));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@logistics_cgst", 0.00);
                        }
                        if (columns.Contains("TCS_CGST"))
                        {
                            command.Parameters.AddWithValue("@TCS_CGST", dr["TCS_CGST"].Equals("") ? 0 : Convert.ToDecimal(dr["TCS_CGST"]));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@TCS_CGST", 0.00);
                        }
                        if (columns.Contains("mp_commission_igst"))
                        {
                            command.Parameters.AddWithValue("@mp_commission_igst", dr["mp_commission_igst"].Equals("") ? 0 : Convert.ToDecimal(dr["mp_commission_igst"]));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@mp_commission_igst", 0.00);
                        }
                        if (columns.Contains("pg_commission_igst"))
                        {
                            command.Parameters.AddWithValue("@pg_commission_igst", dr["pg_commission_igst"].Equals("") ? 0 : Convert.ToDecimal(dr["pg_commission_igst"]));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@pg_commission_igst", 0.00);
                        }

                        if (columns.Contains("logistics_igst"))
                        {
                            command.Parameters.AddWithValue("@logistics_igst", dr["logistics_igst"].Equals("") ? 0 : Convert.ToDecimal(dr["logistics_igst"]));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@logistics_igst", 0.00);
                        }
                        if (columns.Contains("TCS_IGST"))
                        {
                            command.Parameters.AddWithValue("@TCS_IGST", dr["TCS_IGST"].Equals("") ? 0 : Convert.ToDecimal(dr["TCS_IGST"]));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@TCS_IGST", 0.00);
                        }
                        if (columns.Contains("mp_commission_sgst"))
                        {
                            command.Parameters.AddWithValue("@mp_commission_sgst", dr["mp_commission_sgst"].Equals("") ? 0 : Convert.ToDecimal(dr["mp_commission_sgst"]));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@mp_commission_sgst", 0.00);
                        }
                        if (columns.Contains("pg_commission_sgst"))
                        {
                            command.Parameters.AddWithValue("@pg_commission_sgst", dr["pg_commission_sgst"].Equals("") ? 0 : Convert.ToDecimal(dr["pg_commission_sgst"]));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@pg_commission_sgst", 0.00);
                        }
                        if (columns.Contains("logistics_sgst"))
                        {
                            command.Parameters.AddWithValue("@logistics_sgst", dr["logistics_sgst"].Equals("") ? 0 : Convert.ToDecimal(dr["logistics_sgst"]));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@logistics_sgst", 0.00);
                        }
                        if (columns.Contains("TCS_GST"))
                        {
                            command.Parameters.AddWithValue("@TCS_GST", dr["TCS_CGST"].Equals("") ? 0 : Convert.ToDecimal(dr["TCS_CGST"]));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@TCS_GST", 0.00);
                        }
                        if (columns.Contains("Total_Cgst"))
                        {
                            command.Parameters.AddWithValue("@Total_Cgst", dr["Total_Cgst"].Equals("") ? 0 : Convert.ToDecimal(dr["Total_Cgst"]));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Total_Cgst", 0.00);
                        }
                        if (columns.Contains("Total_Igst"))
                        {
                            command.Parameters.AddWithValue("@Total_Igst", dr["Total_Igst"].Equals("") ? 0 : Convert.ToDecimal(dr["Total_Igst"]));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Total_Igst", 0.00);
                        }
                        if (columns.Contains("Total_Sgst"))
                        {
                            command.Parameters.AddWithValue("@Total_Sgst", dr["Total_Sgst"].Equals("") ? 0 : Convert.ToDecimal(dr["Total_Sgst"]));

                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Total_Sgst", 0.00);
                        }
                        //
                        if (columns.Contains("totalsales_taxliable_gstbeforeadustingTCS"))
                        {

                            command.Parameters.AddWithValue("@totalsales_taxliable_gstbeforeadustingTCS", dr["totalsales_taxliable_gstbeforeadustingTCS"].Equals("") ? 0 : Convert.ToDecimal(dr["totalsales_taxliable_gstbeforeadustingTCS"]));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@totalsales_taxliable_gstbeforeadustingTCS", 0.00);
                        }

                        if (columns.Contains("Payment_Status"))
                        {
                            command.Parameters.AddWithValue("@Payment_Status", dr["Payment_Status"]);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Payment_Status", "NA");
                        }
                        if (columns.Contains("Merchant_SKU"))
                        {
                            command.Parameters.AddWithValue("@Merchant_SKU", dr["Merchant_SKU"]);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Merchant_SKU", "NA");
                        }
                        if (columns.Contains("Settlement_Date"))
                        {
                            if (dr["Settlement_Date"].Equals(""))
                            {
                                command.Parameters.AddWithValue("@Settlement_Date", DBNull.Value);
                            }
                            else
                            {
                                try
                                {
                                    string datet = dr["Settlement_Date"].ToString();
                                    CultureInfo MyCultureInfo = new CultureInfo("de-DE");
                                    DateTime n1 = DateTime.Parse(datet, MyCultureInfo);
                                    string m = n1.ToString("yyyy-MM-dd");
                                    //tring DateString = dr["Settlement_Date"].ToString();
                                    //IFormatProvider culture = new CultureInfo("en-US", true);
                                    //DateTime dateVal = DateTime.ParseExact(DateString, "dd/MM/yyyy", culture);


                                    //CultureInfo MyCultureInfo = new CultureInfo("en-US");

                                    //DateTime date = DateTime.ParseExact(datet, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                                    //DateTime n1 = DateTime.Parse(datet, MyCultureInfo);
                                    //string m = n1.ToString("dd-MM-yyyy");
                                    //string check = dr["Settlement_Date"].ToString();
                                    //DateTime d = Convert.ToDateTime(check);
                                    //string f = d.ToString("yyyy-MM-dd");

                                    command.Parameters.AddWithValue("@Settlement_Date",m);
                                }
                                catch (Exception re)
                                {
                                    string check = dr["Settlement_Date"].ToString();
                                    DateTime d = Convert.ToDateTime(check);
                                    string f = d.ToString("yyyy-MM-dd");

                                    
                                    command.Parameters.AddWithValue("@Settlement_Date", f);
                                }
                            }
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Settlement_Date", DBNull.Value);
                        }
                        if (columns.Contains("Payment_Type"))
                        {
                            command.Parameters.AddWithValue("@Payment_Type", dr["Payment_Type"]);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Payment_Type", "NA");

                        }

                        if (columns.Contains("PG_UTR"))
                        {
                            command.Parameters.AddWithValue("@PG_UTR", dr["PG_UTR"]);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@PG_UTR", DBNull.Value);
                        }
                        if (columns.Contains("packfee_for_return"))
                        {
                            command.Parameters.AddWithValue("@packfee_for_return", dr["packfee_for_return"].Equals("") ? 0 : Convert.ToDecimal(dr["packfee_for_return"]));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@packfee_for_return", 0.00);
                        }
                        if (columns.Contains("pymtfee_for_return"))
                        {
                            command.Parameters.AddWithValue("@pymtfee_for_return", dr["pymtfee_for_return"].Equals("") ? 0 : Convert.ToDecimal(dr["pymtfee_for_return"]));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@pymtfee_for_return", 0.00);

                        }
                        if (columns.Contains("logistfee_for_return"))
                        {
                            command.Parameters.AddWithValue("@logistfee_for_return", dr["logistfee_for_return"].Equals("") ? 0 : Convert.ToDecimal(dr["logistfee_for_return"]));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@logistfee_for_return", 0.00);
                        }
                        if (columns.Contains("reverse_logistfee_for_return"))
                        {
                            command.Parameters.AddWithValue("@reverse_logistfee_for_return", dr["reverse_logistfee_for_return"].Equals("") ? 0 : Convert.ToDecimal(dr["reverse_logistfee_for_return"]));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@reverse_logistfee_for_return", 0.00);
                        }
                        if (columns.Contains("order_date"))
                        {
                            if (dr["order_date"].Equals(""))
                            {
                                command.Parameters.AddWithValue("@order_date", DBNull.Value);
                            }
                            else
                            {
                                try
                                {
                                    //CultureInfo MyCultureInfo = new CultureInfo("en-US");
                                    string datet = dr["order_date"].ToString();

                                    CultureInfo MyCultureInfo = new CultureInfo("de-DE");
                                    DateTime n1 = DateTime.Parse(datet, MyCultureInfo);
                                    string m = n1.ToString("yyyy-MM-dd");

                                    //datetime datestring = convert.todatetime(dr["order_date"].tostring());
                                    //string con = datestring.tostring();
                                    //iformatprovider culture = new cultureinfo("de-de", true);
                                    //datetime dateval = datetime.parseexact(con, "dd/mm/yyyy", culture);
                                    //string check = dr["order_date"].ToString();
                                    //DateTime d = Convert.ToDateTime(check);
                                    //string f = d.ToString("MM/dd/yyyy");


                                    command.Parameters.AddWithValue("@order_date", m);
                                }
                                catch (Exception ex)
                                {
                                    string check = dr["order_date"].ToString();
                                    DateTime d = Convert.ToDateTime(check);
                                    string f = d.ToString("MM/dd/yyyy");

                                    command.Parameters.AddWithValue("@order_date", f);
                                }

                            }
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@order_date", DBNull.Value);
                        }
                        if (columns.Contains("spcl_pack_fee_for_return"))
                        {
                            command.Parameters.AddWithValue("@spcl_pack_fee_for_return", dr["spcl_pack_fee_for_return"].Equals("") ? 0 : Convert.ToDecimal(dr["spcl_pack_fee_for_return"]));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@spcl_pack_fee_for_return", 0.00);
                        }

                        if (columns.Contains("other_charges"))
                        {
                            command.Parameters.AddWithValue("@other_charges", dr["other_charges"].Equals("") ? 0 : Convert.ToDecimal(dr["other_charges"]));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@other_charges", 0.00);
                        }
                        if (columns.Contains("closing_fees"))
                        {
                            command.Parameters.AddWithValue("@closing_fees", dr["closing_fees"].Equals("") ? 0 : Convert.ToDecimal(dr["closing_fees"]));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@closing_fees", 0.00);
                        }
                        if (columns.Contains("penltycgst"))
                        {
                            command.Parameters.AddWithValue("@penltycgst", dr["penltycgst"].Equals("") ? 0 : Convert.ToDecimal(dr["penltycgst"]));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@penltycgst", 0.00);
                        }



                        if (columns.Contains("penltysgst"))
                        {
                            command.Parameters.AddWithValue("@penltysgst", dr["penltysgst"].Equals("") ? 0 : Convert.ToDecimal(dr["penltysgst"]));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@penltysgst", 0.00);
                        }


                        if (columns.Contains("penltyigst"))
                        {
                            command.Parameters.AddWithValue("@penltyigst", dr["penltyigst"].Equals("") ? 0 : Convert.ToDecimal(dr["penltyigst"]));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@penltyigst", 0.00);
                        }
                        if (columns.Contains("TCS"))
                        {
                            command.Parameters.AddWithValue("@TCS", dr["TCS"].Equals("") ? "NA" : Convert.ToString(dr["TCS"]));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@TCS", "NA");
                        }
                        if (columns.Contains("productid"))
                        {
                            command.Parameters.AddWithValue("@productid", dr["productid"].Equals("") ? "NA" : Convert.ToString(dr["productid"]));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@productid", "NA");
                        }
                        if (columns.Contains("productname"))
                        {
                            command.Parameters.AddWithValue("@productname", dr["productname"].Equals("") ? "NA" : Convert.ToString(dr["productname"]));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@productname", "NA");
                        }

                        command.Parameters.AddWithValue("@sys_status",sys_status);

                        //p_id = (Int32)command.ExecuteScalar();

                        command.ExecuteNonQuery();
                        command.Parameters.Clear();

                        DataRow drid = did.NewRow();
                        drid["id"] = string.Empty;
                        did.Rows.Add(drid);






                    }
                }
                else
                {
                    SNTalreayindb.Rows.Add(dr.ItemArray);
                }

                line = line + 1;

                //DataTable dexc = new DataTable();
                //command.CommandText = "select s.sid,s.saleschannelvlocid,1 as frm,l.Location from salesrecord s inner join Location l on l.LocationID=s.saleschannelvlocid where s.salesidgivenbyvloc =@salesidgivenbyvloc1 " +
                //                     "union all " +
                //                     "select s.sid,s.saleschannelvlocid,2 as frm,l.Location from cancelTrans s inner join Location l on l.LocationID=s.saleschannelvlocid where s.salesidgivenbyvloc =@salesidgivenbyvloc2 ";
                //command.Parameters.AddWithValue("@salesidgivenbyvloc1", dr["salesid"]);
                //command.Parameters.AddWithValue("@salesidgivenbyvloc2", dr["salesid"]);
                //dexc.Load(command.ExecuteReader());
                //if(dexc.Rows.Count.Equals(1))
                //{
                //    DataRow row = table.NewRow();
                //    row["saleschannelvlocid"] = dexc.Rows[0]["saleschannelvlocid"].ToString();
                //    row["sid"] = dexc.Rows[0]["sid"].ToString();
                //    row["frm"] = dexc.Rows[0]["frm"].ToString();
                //    row["salesid"] = dr["salesid"];
                //    row["Location"] = dr["Location"]; 
                //    table.Rows.Add(row);
                //}
            }

            transaction.Commit();

            return SNTalreayindb;

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
                return null;
            }
            catch (Exception ex2)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex2);
                return null;

            }

        }
    }
    public int updatestockupid(string barcode, string Pt_id, string type, string salesidgivenbyvloc)
    {
        //,DataTable dt, string comingfrom
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        DataTable tableeerror = new DataTable();
        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;
        DataTable Table = new DataTable();
        // Start a local transaction.
        transaction = connection.BeginTransaction("updatestockupid");
        command.Connection = connection;
        command.Transaction = transaction;
        int result = 1;

        try
        {
            string Stockid = "-0";
            if (type.Equals("stock"))
            {
                command.CommandText = "select s.StockupID from StockUpInward s where s.BarcodeNo =@BarcodeNo1 " +
                                          "union all " +
                                          "select s.StockupID from ArchiveStockUpInward s where s.BarcodeNo =@BarcodeNo2 ";
                command.Parameters.AddWithValue("@BarcodeNo1", barcode);
                command.Parameters.AddWithValue("@BarcodeNo2", barcode);
                tableeerror.Load(command.ExecuteReader());
                command.Parameters.Clear();
                if (!tableeerror.Rows.Count.Equals(0))
                {
                    Stockid = tableeerror.Rows[0]["StockupID"].ToString();
                }
            }
            else
            {
                DataTable table = new DataTable();
                command.CommandText = "select c.itemid from cancelTrans c where c.salesidgivenbyvloc=@salesidgivenbyvloc";
                command.Parameters.AddWithValue("@salesidgivenbyvloc", salesidgivenbyvloc);
                table.Load(command.ExecuteReader());
                command.Parameters.Clear();
                if (!table.Rows.Count.Equals(0))
                {
                    DataTable tab = new DataTable();
                    string stockupid = table.Rows[0]["itemid"].ToString();
                    command.CommandText = "select d.BarcodeNo from  deleteStockUpInward d where d.StockupID=@StockupID";
                    command.Parameters.AddWithValue("@StockupID", stockupid);
                    tab.Load(command.ExecuteReader());
                    command.Parameters.Clear();
                    if(!tab.Rows.Count.Equals(0))
                    {
                        if (tab.Rows[0]["BarcodeNo"].ToString().Equals(barcode))
                        {
                            Stockid = stockupid;

                        }
                    }
                    

                }

            }
            if (Stockid.ToString().Equals("-0"))
            {
                return -1;
            }
            else
            {
                command.CommandText = "update payment_trans set stockupId=@stockupId,logdet=@logdet,comingfrom=@comingfrom where Pt_id=@Pt_id";
                command.Parameters.AddWithValue("@stockupId", Stockid);
                command.Parameters.AddWithValue("@Pt_id", Pt_id);
                command.Parameters.AddWithValue("@comingfrom", type);//wheather it is in deletestockup or ArchiveStockUpInward and StockUpInward
                command.Parameters.AddWithValue("@logdet", userName + "-" + userId + "-" + DateTime.Now.ToString("dd-MM-yy HH:mm"));
                command.ExecuteNonQuery();

                command.Parameters.Clear();
            }


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
    public DataTable dobulkupload(DataTable dt, string vlocid)
    {
        string error = string.Empty;
        string insert = string.Empty;
        string update = string.Empty;
        DataTable succDt = new DataTable();
        DataTable newDT = dt;
        newDT.Columns.Add("Status");
        newDT.Columns.Add("Reason");
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;
        transaction = connection.BeginTransaction("bulkupload");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            
            int k = 0;
                foreach (DataRow dRow in dt.Rows)
                {
                    // Start a local transaction.

                    DataTable tableeerror = new DataTable();


                    string Barcode = dRow["Barcode"].ToString();
                    string Pt_id = dRow["Pt_id"].ToString();
                    string Stock = dRow["typeofbarcode"].ToString();
                    string Action = dRow["Action"].ToString();
                    string stockupid1 = "0";

                    if (!Stock.Equals(""))
                    {
                        if (Action.Equals("U"))
                        {


                            if (!Barcode.Equals(""))
                            {
                                if (Stock.Contains("S"))
                                {

                                    command.CommandText = "select s.StockupID from StockUpInward s where s.BarcodeNo =@BarcodeNo1 " +
                                                  "union all " +
                                                  "select s.StockupID from ArchiveStockUpInward s where s.BarcodeNo =@BarcodeNo2 ";
                                    command.Parameters.AddWithValue("@BarcodeNo1", Barcode);
                                    command.Parameters.AddWithValue("@BarcodeNo2", Barcode);
                                    tableeerror.Load(command.ExecuteReader());
                                    if (!tableeerror.Rows.Count.Equals(0))
                                    {
                                        stockupid1 = tableeerror.Rows[0]["StockupID"].ToString();
                                    }
                                    command.Parameters.Clear();
                                }
                                else if (Stock.Contains("D"))
                                {
                                    DataTable table = new DataTable();
                                    command.CommandText = "select c.itemid from cancelTrans c where c.salesidgivenbyvloc=@salesidgivenbyvloc";
                                    command.Parameters.AddWithValue("@salesidgivenbyvloc", dRow["salesid"]);
                                    table.Load(command.ExecuteReader());
                                    command.Parameters.Clear();
                                    if (!table.Rows.Count.Equals(0))
                                    {
                                        DataTable tab = new DataTable();
                                        string stockupid = table.Rows[0]["itemid"].ToString();
                                        command.CommandText = "select d.BarcodeNo from  deleteStockUpInward d where d.StockupID=@StockupID";
                                        command.Parameters.AddWithValue("@StockupID", stockupid);
                                        tab.Load(command.ExecuteReader());
                                        command.Parameters.Clear();
                                        if (!tab.Rows.Count.Equals(0))
                                        {
                                            if (tab.Rows[0]["BarcodeNo"].ToString().Equals(Barcode))
                                            {
                                                stockupid1 = stockupid;

                                            }
                                        }
                                        else
                                        {
                                            //transaction.Rollback();
                                            error += "Barcode not Found-" + Barcode + Environment.NewLine;
                                            newDT.Rows[k]["Status"] = "Failed";
                                            newDT.Rows[k]["Reason"] = "Barcode not Found";
                                        }
                                    }
                                    else
                                    {
                                        //transaction.Rollback();
                                        error += "Barcode not Found-" + Barcode + Environment.NewLine;
                                        newDT.Rows[k]["Status"] = "Failed";
                                        newDT.Rows[k]["Reason"] = "Barcode not Found";
                                    }
                                }
                                if (!stockupid1.Equals("0"))
                                {
                                    command.CommandText = "update payment_trans set stockupId=@stockupId,logdet=@logdet,comingfrom=@comingfrom where Pt_id=@Pt_id and vlocid=@vlocid";
                                    command.Parameters.AddWithValue("@stockupId", stockupid1);
                                    command.Parameters.AddWithValue("@Pt_id", Pt_id);
                                    command.Parameters.AddWithValue("@vlocid", vlocid);
                                    command.Parameters.AddWithValue("@comingfrom", Stock);//wheather it is in deletestockup or ArchiveStockUpInward and StockUpInward
                                    command.Parameters.AddWithValue("@logdet", userName + "-" + userId + "-" + DateTime.Now.ToString("dd-MM-yy HH:mm"));
                                    command.ExecuteNonQuery();

                                    command.Parameters.Clear();

                                    newDT.Rows[k]["Status"] = "Success";
                                    newDT.Rows[k]["Reason"] = "Updated ";
                                }
                                else
                                {
                                    //transaction.Rollback();
                                    error += "Barcode not Found-" + Barcode + Environment.NewLine;
                                    newDT.Rows[k]["Status"] = "Failed";
                                    newDT.Rows[k]["Reason"] = "Error Occured";
                                }

                                command.Parameters.Clear();
                            }
                            else
                            {
                                //transaction.Rollback();
                                newDT.Rows[k]["Status"] = "Failed";
                                newDT.Rows[k]["Reason"] = "Empty Barcode";
                            }
                        }

                        else if (Action.Equals("D"))
                        {
                            command.CommandText = "Delete From payment_trans where Pt_id=@Pt_id";
                            command.Parameters.AddWithValue("@Pt_id", dRow["Pt_id"]);
                            command.Parameters.Clear();
                            newDT.Rows[k]["Status"] = "Success";
                            newDT.Rows[k]["Reason"] = "Deleted ";
                        }
                    }
                    else
                    {
                        //transaction.Rollback();
                        error += "Barcode not Found-" + Barcode + Environment.NewLine;
                        newDT.Rows[k]["Status"] = "Failed";
                        newDT.Rows[k]["Reason"] = "Empty Charecter";
                    }
                k++;
            }
            transaction.Commit();

            if (connection.State == ConnectionState.Open)
                connection.Close();
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
                error += "Transaction Rolled Back";

            }
            catch (Exception ex2)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex2);
                error += "Some error Occured";

            }
        }

        succDt.Columns.Add("Error");
        succDt.Columns.Add("Insert");
        succDt.Columns.Add("Update");
        succDt.Rows.Add(error, insert, update);
        return newDT;
    }
    public DataTable Detele(string Pt_id,DataTable dt,string comingfrom)
    {
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;
        DataTable table = new DataTable();
        // Start a local transaction.
        transaction = connection.BeginTransaction("updatestockupid");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            if (comingfrom.Equals("multiple"))
            {
                foreach (DataRow dr in dt.Rows)
                {
                    command.CommandText = "delete from payment_trans where Pt_id=@Pt_id";
                    command.Parameters.AddWithValue("@Pt_id", dr["ID"]);
                    command.ExecuteNonQuery();
                   
                    command.Parameters.Clear();
                }
                
            }
            else
            {
                command.CommandText = "delete from payment_trans where Pt_id=@Pt_id";
                command.Parameters.AddWithValue("@Pt_id", Pt_id);
                command.ExecuteNonQuery();
               
                command.Parameters.Clear();
            }
            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();
            return table;
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
                return null;
            }
            catch (Exception ex2)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex2);
                return null;

            }
        }
    }
    public DataTable getpaymentbyserch(DataTable search)
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
        transaction = connection.BeginTransaction("getpaymentbyserch");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            if (search.Rows[0]["SearchBy"].ToString().Equals("All"))//by all
            {
                command.CommandText = "select  * from ( " +
                                    "select p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,convert(varchar, p.order_date, 104) as order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,i.Control13 as Articel,r.sellingprice,r.invoiceid  from payment_trans p  inner join StockUpInward s on s.StockupID=p.stockupId inner join Location l on l.LocationID=p.vlocid inner join ItemStyle i on i.StyleID = s.StyleID  inner join Size sz on sz.SizeID=s.SizeID inner join Lot lot on lot.BagId=s.BagID left join Column1 col1 on col1.Col1ID=i.Col1 inner join salesrecord r on r.saleschannelvlocid=p.vlocid and p.salesid=r.salesidgivenbyvloc  where  p.stockupId!=-1 group by  p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,p.order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1, lot.BagDescription,i.Control13,col1.C1Name, r.sellingprice,r.invoiceid  " +
                                    "union all   " +
                                    "select p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,convert(varchar, p.order_date, 104) as order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,i.Control13 as Articel,r.sellingprice,r.invoiceid  from payment_trans p  inner join ArchiveStockUpInward s on s.StockupID=p.stockupId inner join Location l on l.LocationID=p.vlocid inner join ItemStyle i on i.StyleID = s.StyleID  inner join Size sz on sz.SizeID=s.SizeID inner join Lot lot on lot.BagId=s.BagID left join Column1 col1 on col1.Col1ID=i.Col1  inner join salesrecord r on r.saleschannelvlocid=p.vlocid and p.salesid=r.salesidgivenbyvloc where  p.stockupId!=-1 group by p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,p.order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1, lot.BagDescription,i.Control13,col1.C1Name,r.sellingprice,r.invoiceid " +

                                    "union all  " +
                                    "select p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,convert(varchar, p.order_date, 104) as order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,i.Control13 as Articel,r.sellingprice,r.invoiceid  from payment_trans p  inner join StockUpInward s on s.StockupID=p.stockupId inner join Location l on l.LocationID=p.vlocid inner join ItemStyle i on i.StyleID = s.StyleID  inner join Size sz on sz.SizeID=s.SizeID inner join Lot lot on lot.BagId=s.BagID left join Column1 col1 on col1.Col1ID=i.Col1 inner join cancelTrans r on r.saleschannelvlocid=p.vlocid and p.salesid=r.salesidgivenbyvloc  where  p.stockupId!=-1 group by  p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,p.order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1, lot.BagDescription,i.Control13,col1.C1Name, r.sellingprice,r.invoiceid  " +
                                    "union all   " +
                                    "select p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,convert(varchar, p.order_date, 104) as order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,i.Control13 as Articel,r.sellingprice,r.invoiceid  from payment_trans p  inner join ArchiveStockUpInward s on s.StockupID=p.stockupId inner join Location l on l.LocationID=p.vlocid inner join ItemStyle i on i.StyleID = s.StyleID  inner join Size sz on sz.SizeID=s.SizeID inner join Lot lot on lot.BagId=s.BagID left join Column1 col1 on col1.Col1ID=i.Col1  inner join cancelTrans r on r.saleschannelvlocid=p.vlocid and p.salesid=r.salesidgivenbyvloc where  p.stockupId!=-1 group by p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,p.order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1, lot.BagDescription,i.Control13,col1.C1Name,r.sellingprice,r.invoiceid)b group by b.Pt_id, b.stockupId, b.salesid, b.type, b.vlocid, b.Payment_Status, b.Payment_Type, b.Payable_Amoun, b.order_date, b.BarcodeNo,b.Location, b.Title, b.Size1,b.brand, b.Lotno,b.Articel,b.sp ,b.sellingprice,b.invoiceid ";


                invTable.Load(command.ExecuteReader());
            }
            else if (search.Rows[0]["SearchBy"].ToString().Equals("1")) // by barcode
            {
                invTable = SearchByBarcode(search.Rows[0]["searchField"].ToString());

            }
            else if (search.Rows[0]["SearchBy"].ToString().Equals("2")) // by salesid
            {
                invTable = SearchBySalesID(search.Rows[0]["searchField"].ToString());
            }
            else if (search.Rows[0]["SearchBy"].ToString().Equals("3")) // by virtual location
            {
                invTable = SearchByVLoc(search.Rows[0]["Vloc"].ToString());
            }
            else if (search.Rows[0]["SearchBy"].ToString().Equals("4")) // by Date
            {
                invTable = SearchByDate(search.Rows[0]["fromDate"].ToString(), search.Rows[0]["toDate"].ToString());
            }
            else if (search.Rows[0]["SearchBy"].ToString().Equals("5")) // by -1
            {
                //invTable = SearchByOne();
            }




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
    //ok
    public DataTable SearchByBarcode(string Barcode1)
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
        transaction = connection.BeginTransaction("SearchByBarcode");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            /*command.CommandText = "select * from  " +
                                  "(select p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.MRP, p.Payment_Status, p.Payment_Type, p.Payable_Amoun, p.order_date, s.BarcodeNo  from payment_trans p  inner join StockUpInward s on s.StockupID=p.stockupId  where s.BarcodeNo=@BarcodeNo1  " +
                                  "union all  " +
                                  "select p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.MRP, p.Payment_Status, p.Payment_Type, p.Payable_Amoun, p.order_date, a.BarcodeNo  from payment_trans p  inner join ArchiveStockUpInward a on a.StockupID=p.stockupId  where a.BarcodeNo=@BarcodeNo2 )a  ";*/


            command.CommandText = "select  * from ( " +
                                    "select p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,convert(varchar, p.order_date, 104) as order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,i.Control13 as Articel,r.sellingprice,r.invoiceid  from payment_trans p  inner join StockUpInward s on s.StockupID=p.stockupId inner join Location l on l.LocationID=p.vlocid inner join ItemStyle i on i.StyleID = s.StyleID  inner join Size sz on sz.SizeID=s.SizeID inner join Lot lot on lot.BagId=s.BagID left join Column1 col1 on col1.Col1ID=i.Col1 inner join salesrecord r on r.saleschannelvlocid=p.vlocid and p.salesid=r.salesidgivenbyvloc  where s.BarcodeNo=@BarcodeNo1 and p.stockupId!=-1 group by  p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,p.order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1, lot.BagDescription,i.Control13,col1.C1Name, r.sellingprice,r.invoiceid  " +
                                    "union all   " +
                                    "select p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,convert(varchar, p.order_date, 104) as order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,i.Control13 as Articel,r.sellingprice,r.invoiceid  from payment_trans p  inner join ArchiveStockUpInward s on s.StockupID=p.stockupId inner join Location l on l.LocationID=p.vlocid inner join ItemStyle i on i.StyleID = s.StyleID  inner join Size sz on sz.SizeID=s.SizeID inner join Lot lot on lot.BagId=s.BagID left join Column1 col1 on col1.Col1ID=i.Col1  inner join salesrecord r on r.saleschannelvlocid=p.vlocid and p.salesid=r.salesidgivenbyvloc where s.BarcodeNo=@BarcodeNo2 and p.stockupId!=-1 group by p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,p.order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1, lot.BagDescription,i.Control13,col1.C1Name,r.sellingprice,r.invoiceid " +

                                    "union all  " +
                                    "select p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,convert(varchar, p.order_date, 104) as order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,i.Control13 as Articel,r.sellingprice,r.invoiceid  from payment_trans p  inner join StockUpInward s on s.StockupID=p.stockupId inner join Location l on l.LocationID=p.vlocid inner join ItemStyle i on i.StyleID = s.StyleID  inner join Size sz on sz.SizeID=s.SizeID inner join Lot lot on lot.BagId=s.BagID left join Column1 col1 on col1.Col1ID=i.Col1 inner join cancelTrans r on r.saleschannelvlocid=p.vlocid and p.salesid=r.salesidgivenbyvloc  where s.BarcodeNo=@BarcodeNo3 and p.stockupId!=-1 group by  p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,p.order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1, lot.BagDescription,i.Control13,col1.C1Name, r.sellingprice,r.invoiceid  " +
                                    "union all   " +
                                    "select p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,convert(varchar, p.order_date, 104) as order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,i.Control13 as Articel,r.sellingprice,r.invoiceid  from payment_trans p  inner join ArchiveStockUpInward s on s.StockupID=p.stockupId inner join Location l on l.LocationID=p.vlocid inner join ItemStyle i on i.StyleID = s.StyleID  inner join Size sz on sz.SizeID=s.SizeID inner join Lot lot on lot.BagId=s.BagID left join Column1 col1 on col1.Col1ID=i.Col1  inner join cancelTrans r on r.saleschannelvlocid=p.vlocid and p.salesid=r.salesidgivenbyvloc where s.BarcodeNo=@BarcodeNo4 and p.stockupId!=-1 group by p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,p.order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1, lot.BagDescription,i.Control13,col1.C1Name,r.sellingprice,r.invoiceid)b group by b.Pt_id, b.stockupId, b.salesid, b.type, b.vlocid, b.Payment_Status, b.Payment_Type, b.Payable_Amoun, b.order_date, b.BarcodeNo,b.Location, b.Title, b.Size1,b.brand, b.Lotno,b.Articel,b.sp ,b.sellingprice,b.invoiceid ";

            command.Parameters.AddWithValue("@BarcodeNo1", Barcode1);
            command.Parameters.AddWithValue("@BarcodeNo2", Barcode1);
            command.Parameters.AddWithValue("@BarcodeNo3", Barcode1);
            command.Parameters.AddWithValue("@BarcodeNo4", Barcode1);
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

    //ok
    public DataTable SearchByVLoc(string vloc)
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
        transaction = connection.BeginTransaction("SearchByVLoc");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            /*command.CommandText = "select * from " +
                                  "(select p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.MRP, p.Payment_Status, p.Payment_Type, p.Payable_Amoun, p.order_date, s.BarcodeNo  from payment_trans p  inner join StockUpInward s on s.StockupID = p.stockupId  where p.vlocid@vlocid " +
                                  "union all " +
                                  "select p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.MRP, p.Payment_Status, p.Payment_Type, p.Payable_Amoun, p.order_date, a.BarcodeNo  from payment_trans p  inner join ArchiveStockUpInward a on a.StockupID = p.stockupId  where p.vlocid=@vlocid )a ";*/
            command.CommandText = "select  * from ( " +
                                    "select p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,convert(varchar, p.order_date, 104) as order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,i.Control13 as Articel,r.sellingprice,r.invoiceid  from payment_trans p  inner join StockUpInward s on s.StockupID=p.stockupId inner join Location l on l.LocationID=p.vlocid inner join ItemStyle i on i.StyleID = s.StyleID  inner join Size sz on sz.SizeID=s.SizeID inner join Lot lot on lot.BagId=s.BagID left join Column1 col1 on col1.Col1ID=i.Col1 inner join salesrecord r on r.saleschannelvlocid=p.vlocid and p.salesid=r.salesidgivenbyvloc  where p.vlocid=@vlocid1 and p.stockupId!=-1 group by  p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,p.order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1, lot.BagDescription,i.Control13,col1.C1Name, r.sellingprice ,r.invoiceid " +
                                    "union all   " +
                                    "select p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,convert(varchar, p.order_date, 104) as order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,i.Control13 as Articel,r.sellingprice,r.invoiceid  from payment_trans p  inner join ArchiveStockUpInward s on s.StockupID=p.stockupId inner join Location l on l.LocationID=p.vlocid inner join ItemStyle i on i.StyleID = s.StyleID  inner join Size sz on sz.SizeID=s.SizeID inner join Lot lot on lot.BagId=s.BagID left join Column1 col1 on col1.Col1ID=i.Col1  inner join salesrecord r on r.saleschannelvlocid=p.vlocid and p.salesid=r.salesidgivenbyvloc where p.vlocid=@vlocid2  and p.stockupId!=-1 group by p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,p.order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1, lot.BagDescription,i.Control13,col1.C1Name,r.sellingprice,r.invoiceid " +
                                    "union all  " +
                                    "select p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,convert(varchar, p.order_date, 104) as order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,i.Control13 as Articel,r.sellingprice,r.invoiceid  from payment_trans p  inner join StockUpInward s on s.StockupID=p.stockupId inner join Location l on l.LocationID=p.vlocid inner join ItemStyle i on i.StyleID = s.StyleID  inner join Size sz on sz.SizeID=s.SizeID inner join Lot lot on lot.BagId=s.BagID left join Column1 col1 on col1.Col1ID=i.Col1 inner join cancelTrans r on r.saleschannelvlocid=p.vlocid and p.salesid=r.salesidgivenbyvloc  where p.vlocid=@vlocid3  and p.stockupId!=-1 group by  p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,p.order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1, lot.BagDescription,i.Control13,col1.C1Name, r.sellingprice,r.invoiceid  " +
                                    "union all  " +
                                    "select p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,convert(varchar, p.order_date, 104) as order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,i.Control13 as Articel,r.sellingprice ,r.invoiceid from payment_trans p  inner join ArchiveStockUpInward s on s.StockupID=p.stockupId inner join Location l on l.LocationID=p.vlocid inner join ItemStyle i on i.StyleID = s.StyleID  inner join Size sz on sz.SizeID=s.SizeID inner join Lot lot on lot.BagId=s.BagID left join Column1 col1 on col1.Col1ID=i.Col1  inner join cancelTrans r on r.saleschannelvlocid=p.vlocid and p.salesid=r.salesidgivenbyvloc where p.vlocid=@vlocid4  and p.stockupId!=-1 group by p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,p.order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1, lot.BagDescription,i.Control13,col1.C1Name,r.sellingprice,r.invoiceid )b group by b.Pt_id, b.stockupId, b.salesid, b.type, b.vlocid, b.Payment_Status, b.Payment_Type, b.Payable_Amoun, b.order_date, b.BarcodeNo,b.Location, b.Title, b.Size1,b.brand, b.Lotno,b.Articel,b.sp ,b.sellingprice,b.invoiceid ";




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

    //ok
    public DataTable SearchByDate(string fromdate, string todate)
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
        transaction = connection.BeginTransaction("SearchByDate");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = " select  * from ( " +
                                    " select p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,convert(varchar, p.order_date, 104) as order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,i.Control13 as Articel,r.sellingprice,r.invoiceid  from payment_trans p  inner join StockUpInward s on s.StockupID=p.stockupId inner join Location l on l.LocationID=p.vlocid inner join ItemStyle i on i.StyleID = s.StyleID  inner join Size sz on sz.SizeID=s.SizeID inner join Lot lot on lot.BagId=s.BagID left join Column1 col1 on col1.Col1ID=i.Col1 inner join salesrecord r on r.saleschannelvlocid=p.vlocid and p.salesid=r.salesidgivenbyvloc    where (p.order_date between @fromdate1 and @todate1) and p.stockupId!=-1 group by  p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,p.order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1, lot.BagDescription,i.Control13,col1.C1Name, r.sellingprice,r.invoiceid " +
                                    "union all   " +
                                    " select p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,convert(varchar, p.order_date, 104) as order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,i.Control13 as Articel,r.sellingprice,r.invoiceid  from payment_trans p  inner join ArchiveStockUpInward s on s.StockupID=p.stockupId inner join Location l on l.LocationID=p.vlocid inner join ItemStyle i on i.StyleID = s.StyleID  inner join Size sz on sz.SizeID=s.SizeID inner join Lot lot on lot.BagId=s.BagID left join Column1 col1 on col1.Col1ID=i.Col1  inner join salesrecord r on r.saleschannelvlocid=p.vlocid and p.salesid=r.salesidgivenbyvloc where (p.order_date between @fromdate2 and @todate2)  and p.stockupId!=-1 group by p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,p.order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1, lot.BagDescription,i.Control13,col1.C1Name,r.sellingprice,r.invoiceid " +
                                    "union all  " +
                                    " select p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,convert(varchar, p.order_date, 104) as order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,i.Control13 as Articel,r.sellingprice,r.invoiceid  from payment_trans p  inner join StockUpInward s on s.StockupID=p.stockupId inner join Location l on l.LocationID=p.vlocid inner join ItemStyle i on i.StyleID = s.StyleID  inner join Size sz on sz.SizeID=s.SizeID inner join Lot lot on lot.BagId=s.BagID left join Column1 col1 on col1.Col1ID=i.Col1 inner join cancelTrans r on r.saleschannelvlocid=p.vlocid and p.salesid=r.salesidgivenbyvloc    where (p.order_date between @fromdate3 and @todate3)  and p.stockupId!=-1 group by  p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,p.order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1, lot.BagDescription,i.Control13,col1.C1Name, r.sellingprice ,r.invoiceid " +
                                    "union all  " +
                                    " select p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,convert(varchar, p.order_date, 104) as order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,i.Control13 as Articel,r.sellingprice,r.invoiceid  from payment_trans p  inner join ArchiveStockUpInward s on s.StockupID=p.stockupId inner join Location l on l.LocationID=p.vlocid inner join ItemStyle i on i.StyleID = s.StyleID  inner join Size sz on sz.SizeID=s.SizeID inner join Lot lot on lot.BagId=s.BagID left join Column1 col1 on col1.Col1ID=i.Col1  inner join cancelTrans r on r.saleschannelvlocid=p.vlocid and p.salesid=r.salesidgivenbyvloc   where (p.order_date between @fromdate4 and @todate4)  and p.stockupId!=-1 group by p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,p.order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1, lot.BagDescription,i.Control13,col1.C1Name,r.sellingprice ,r.invoiceid ) b group by b.Pt_id, b.stockupId, b.salesid, b.type, b.vlocid, b.Payment_Status, b.Payment_Type, b.Payable_Amoun, b.order_date, b.BarcodeNo,b.Location, b.Title, b.Size1,b.brand, b.Lotno,b.Articel,b.sp ,b.sellingprice,b.invoiceid ";
            command.Parameters.AddWithValue("@fromdate1", fromdate);
            command.Parameters.AddWithValue("@todate1", todate);
            command.Parameters.AddWithValue("@fromdate2", fromdate);
            command.Parameters.AddWithValue("@todate2", todate);
            command.Parameters.AddWithValue("@fromdate3", fromdate);
            command.Parameters.AddWithValue("@todate3", todate);
            command.Parameters.AddWithValue("@fromdate4", fromdate);
            command.Parameters.AddWithValue("@todate4", todate);











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


    //ok
    public DataTable SearchByOne(string vlocid)
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
        transaction = connection.BeginTransaction("SearchByOne");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select p.Pt_id,p.stockupId,p.salesid,p.type,p.vlocid,p.sp,p.Payment_Status,p.Payment_Type,p.Payable_Amoun,convert(varchar, p.order_date, 104) as order_date,l.Location,Merchant_SKU,productid,productname from payment_trans p inner join Location l on l.LocationID=p.vlocid where p.stockupId=-1  and p.vlocid=@vlocid";
            command.Parameters.AddWithValue("@vlocid", vlocid);
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

    //ok
    public DataTable SearchBySalesID(string salesid)
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
        transaction = connection.BeginTransaction("SearchBySalesID");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            //command.CommandText = "select * from " +
            //                      "(select p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.MRP, p.Payment_Status, p.Payment_Type, p.Payable_Amoun, p.order_date, s.BarcodeNo  from payment_trans p  inner join StockUpInward s on s.StockupID = p.stockupId  where p.vlocid=30= @vlocid " +
            //                      "union all " +
            //                      "select p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.MRP, p.Payment_Status, p.Payment_Type, p.Payable_Amoun, p.order_date, a.BarcodeNo  from payment_trans p  inner join ArchiveStockUpInward a on a.StockupID = p.stockupId  where p.vlocid=30=@vlocid )a " +

            command.CommandText = "select  * from ( " +
                                    "select p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,convert(varchar, p.order_date, 104) as order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,i.Control13 as Articel,r.sellingprice,r.invoiceid  from payment_trans p  inner join StockUpInward s on s.StockupID=p.stockupId inner join Location l on l.LocationID=p.vlocid inner join ItemStyle i on i.StyleID = s.StyleID  inner join Size sz on sz.SizeID=s.SizeID inner join Lot lot on lot.BagId=s.BagID left join Column1 col1 on col1.Col1ID=i.Col1 inner join salesrecord r on r.saleschannelvlocid=p.vlocid and p.salesid=r.salesidgivenbyvloc  where p.salesid=@salesid1 and p.stockupId!=-1 group by  p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,p.order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1, lot.BagDescription,i.Control13,col1.C1Name, r.sellingprice ,r.invoiceid " +
                                    "union all   " +
                                    "select p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,convert(varchar, p.order_date, 104) as order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,i.Control13 as Articel,r.sellingprice,r.invoiceid  from payment_trans p  inner join ArchiveStockUpInward s on s.StockupID=p.stockupId inner join Location l on l.LocationID=p.vlocid inner join ItemStyle i on i.StyleID = s.StyleID  inner join Size sz on sz.SizeID=s.SizeID inner join Lot lot on lot.BagId=s.BagID left join Column1 col1 on col1.Col1ID=i.Col1  inner join salesrecord r on r.saleschannelvlocid=p.vlocid and p.salesid=r.salesidgivenbyvloc where p.salesid=@salesid2  and p.stockupId!=-1 group by p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,p.order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1, lot.BagDescription,i.Control13,col1.C1Name,r.sellingprice,r.invoiceid " +
                                    "union all  " +
                                    "select p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,convert(varchar, p.order_date, 104) as order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,i.Control13 as Articel,r.sellingprice,r.invoiceid  from payment_trans p  inner join StockUpInward s on s.StockupID=p.stockupId inner join Location l on l.LocationID=p.vlocid inner join ItemStyle i on i.StyleID = s.StyleID  inner join Size sz on sz.SizeID=s.SizeID inner join Lot lot on lot.BagId=s.BagID left join Column1 col1 on col1.Col1ID=i.Col1 inner join cancelTrans r on r.saleschannelvlocid=p.vlocid and p.salesid=r.salesidgivenbyvloc  where p.salesid=@salesid3  and p.stockupId!=-1 group by  p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,p.order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1, lot.BagDescription,i.Control13,col1.C1Name, r.sellingprice,r.invoiceid  " +
                                    "union all  " +
                                    "select p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,convert(varchar, p.order_date, 104) as order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1.C1Name as brand, lot.BagDescription as Lotno,i.Control13 as Articel,r.sellingprice ,r.invoiceid from payment_trans p  inner join ArchiveStockUpInward s on s.StockupID=p.stockupId inner join Location l on l.LocationID=p.vlocid inner join ItemStyle i on i.StyleID = s.StyleID  inner join Size sz on sz.SizeID=s.SizeID inner join Lot lot on lot.BagId=s.BagID left join Column1 col1 on col1.Col1ID=i.Col1  inner join cancelTrans r on r.saleschannelvlocid=p.vlocid and p.salesid=r.salesidgivenbyvloc where p.salesid=@salesid4  and p.stockupId!=-1 group by p.Pt_id, p.stockupId, p.salesid, p.type, p.vlocid, p.sp, p.Payment_Status, p.Payment_Type, p.Payable_Amoun,p.order_date, s.BarcodeNo,l.Location, i.Title, sz.Size1,col1, lot.BagDescription,i.Control13,col1.C1Name,r.sellingprice,r.invoiceid )b group by b.Pt_id, b.stockupId, b.salesid, b.type, b.vlocid, b.Payment_Status, b.Payment_Type, b.Payable_Amoun, b.order_date, b.BarcodeNo,b.Location, b.Title, b.Size1,b.brand, b.Lotno,b.Articel,b.sp ,b.sellingprice,b.invoiceid ";


            command.Parameters.AddWithValue("@salesid1", salesid);
            command.Parameters.AddWithValue("@salesid2", salesid);
            command.Parameters.AddWithValue("@salesid3", salesid);
            command.Parameters.AddWithValue("@salesid4", salesid);

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

    //PostCalculation
    public int PostCalculation(string fromdate,string todate)
    {
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }
        DataTable table = new DataTable();
        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("PostCalculation");
        command.Connection = connection;
        command.Transaction = transaction;
        int line = 0;

        int result = 1;
       
        try
        {


            
                DataTable dtfetch = new DataTable();
                command.CommandText = "select * from payment_trans where (order_date between @fromdate and @todate) and stockupId!=-1 and stockupId!=-2";
                command.Parameters.AddWithValue("@fromdate",fromdate);
                command.Parameters.AddWithValue("@todate",todate);
                dtfetch.Load(command.ExecuteReader());
                command.Parameters.Clear();
            DataTable dc = new DataTable();
            foreach (DataRow row in dtfetch.Rows)
            {
                
                DataTable post = new DataTable();
                command.CommandText = "Select checkgst,chksalsprice,chckstatus,chckchncomgst,chckchngategst," +
                    "chckchnloggst,chckchnpenlgst,chckchnmic1gst,chckchnmic2gst,shipping_service,transfer,orders,refund,TCSchck from Pymt_trans_setting where vloc_id=@vloc_id ";
                command.Parameters.AddWithValue("@vloc_id", row["vlocid"].ToString());
                post.Load(command.ExecuteReader());
                command.Parameters.Clear();

                DataTable gstdt = new DataTable();
                if (dtfetch.Rows.Count.Equals(0))
                {

                }
                else
                {
                    command.CommandText = "select h.*,si.StockupID,i.state from hsnmaster h " +
                                          "inner join ItemCategory c on h.hsnid = c.hsnid " +
                                          "inner join ItemStyle s on s.ItemCatID = c.ItemCategoryID " +
                                          "inner join StockUpInward si on si.StyleID = s.StyleID " +
                                          "inner join salesrecord r on r.itemid = si.StockupID and r.itemid != -1 " +
                                          "inner join invoice i on i.invid = r.invoiceid " +
                                          "where si.StockupID=@StockupID1 " +
                                          "union all " +
                                          "select h.*,si.StockupID,i.state from hsnmaster h " +
                                          "inner join ItemCategory c on h.hsnid = c.hsnid " +
                                          "inner join ItemStyle s on s.ItemCatID = c.ItemCategoryID " +
                                          "inner join ArchiveStockUpInward si on si.StyleID = s.StyleID " +
                                          "inner join salesrecord r on r.archiveid=si.ArchiveStockupID and r.archiveid != -1 " +
                                          "inner join invoice i on i.invid = r.invoiceid " +
                                          "where si.StockupID=@StockupID2 " +
                                          "union all " +
                                          "select h.*,si.StockupID,i.state from hsnmaster h " +
                                          "inner join ItemCategory c on h.hsnid = c.hsnid " +
                                          "inner join ItemStyle s on s.ItemCatID = c.ItemCategoryID " +
                                          "inner join ArchiveStockUpInward si on si.StyleID = s.StyleID " +
                                          "inner join cancelTrans r on r.archiveid=si.ArchiveStockupID and r.archiveid != -1 " +
                                          "inner join invoice i on i.invid = r.invoiceid " +
                                          "where si.StockupID=@StockupID3 " +
                                          "union all " +
                                          "select h.*,si.StockupID,i.state from hsnmaster h " +
                                          "inner join ItemCategory c on h.hsnid = c.hsnid " +
                                          "inner join ItemStyle s on s.ItemCatID = c.ItemCategoryID " +
                                          "inner join StockUpInward si on si.StyleID = s.StyleID " +
                                          "inner join cancelTrans r on r.itemid = si.StockupID and r.itemid != -1 " +
                                          "inner join invoice i on i.invid = r.invoiceid " +
                                          "where si.StockupID=@StockupID4 ";
                    command.Parameters.AddWithValue("@StockupID1", row["stockupId"].ToString());
                    command.Parameters.AddWithValue("@StockupID2", row["stockupId"].ToString());
                    command.Parameters.AddWithValue("@StockupID3", row["stockupId"].ToString());
                    command.Parameters.AddWithValue("@StockupID4", row["stockupId"].ToString());
                    dc.Load(command.ExecuteReader());
                    
                    command.Parameters.Clear();
                    if (!gstdt.Rows.Count.Equals(0))
                    {
                        gstdt = dc.Copy();
                    }
                    else
                    {
                        DataTable dtg = new DataTable();
                        command.CommandText = "select h.*,27 AS state from hsnmaster h where h.hsnid=1";
                        dtg.Load(command.ExecuteReader());
                        gstdt = dtg.Copy();
                    }

                    #region  post Calculation
                    decimal sp = Convert.ToDecimal(row["sp"].ToString());
                    decimal pmtspinclugst = 0;
                    decimal pmtspexclgst = 0;
                    decimal pmtspinclucgst = 0;
                    decimal pmtspexclcgst = 0;
                    decimal pmtspinclusgst = 0;
                    decimal pmtspexclsgst = 0;
                    int satecode = Convert.ToInt32(gstdt.Rows[0]["state"]);

                    decimal lowhighpt = Convert.ToDecimal(gstdt.Rows[0]["lowhighpt"]);
                    //IGST
                    decimal ligst = Convert.ToDecimal(gstdt.Rows[0]["ligst"]);
                    decimal higst = Convert.ToDecimal(gstdt.Rows[0]["higst"]);
                    //SGST
                    decimal hisgst = Convert.ToDecimal(gstdt.Rows[0]["higst"]);
                    decimal hicgst = Convert.ToDecimal(gstdt.Rows[0]["higst"]);
                    //CGST
                    decimal lisgst = Convert.ToDecimal(gstdt.Rows[0]["ligst"]);
                    decimal licgst = Convert.ToDecimal(gstdt.Rows[0]["ligst"]);

                    decimal taxableamnt = 0;
                    decimal igst = 0;
                    decimal cgst = 0;
                    decimal sgst = 0;
                    decimal gst = 0;
                    //chennelcommisiondr["penltycgst"].Equals("") ? 0 : Convert.ToDecimal(dr["penltycgst"])
                    decimal chnncomm = row["channel_commsion"].ToString().Equals("") ? 0 : Convert.ToDecimal(row["channel_commsion"]);
                   
                    decimal chnncommigst = 0;
                    decimal chnncommpmtinclugst = 0;
                    decimal chnncommpmtexclgst = 0;
                    decimal chnncomminccgst = 0;
                    decimal chnncommexccgst = 0;
                    decimal chnncommincsgst = 0;
                    decimal chnncommexcsgst = 0;
                    //chennel gateway
                    decimal chnngate = row["Channel_Gateway"].ToString().Equals("") ? 0 : Convert.ToDecimal(row["Channel_Gateway"]);
                    decimal chnngateigst = 0;
                    decimal chnngatepmtinclugst = 0;
                    decimal chnngatepmtexclgst = 0;
                    decimal chnngateinccgst = 0;
                    decimal chnngateincsgst = 0;
                    decimal chnngateexccgst = 0;
                    decimal chnngateexcsgst = 0;

                    //chennel logis
                    decimal chnnlogic = row["VL_Logistics"].ToString().Equals("") ? 0 : Convert.ToDecimal(row["VL_Logistics"]); 
                    decimal chnnchnnlogicigst = 0;
                    decimal chnnchnnlogicpmtinclugst = 0;
                    decimal chnnchnnlogicpmtexclgst = 0;
                    decimal chnnlogicinccgst = 0;
                    decimal chnnlogicincsgst = 0;
                    decimal chnnlogicexccgst = 0;
                    decimal chnnlogicexcsgst = 0;

                    //chennel Pnlty
                    decimal chnnpnlty = row["VLPenalty"].ToString().Equals("") ? 0 : Convert.ToDecimal(row["VLPenalty"]); 
                    decimal chnnchnnpnltyigst = 0;
                    decimal chnnchnnpnltypmtinclugst = 0;
                    decimal chnnchnnpnltypmtexclgst = 0;
                    decimal chnnpnltyinccgst = 0;
                    decimal chnnpnltyincsgst = 0;
                    decimal chnnpnltyexccgst = 0;
                    decimal chnnpnltyexcsgst = 0;

                    //chennel Mis1
                    string check = row["Pack_Charges_VL_Misc"].ToString();
                    decimal chnnmis1 = row["Pack_Charges_VL_Misc"].ToString().Equals("") ? 0 : Convert.ToDecimal(row["Pack_Charges_VL_Misc"]); 
                    decimal chnnchnnmis1igst = 0;
                    decimal chnnchnnmis1pmtinclugst = 0;
                    decimal chnnchnnmis1pmtexclgst = 0;
                    decimal chnnmis1inccgst = 0;
                    decimal chnnmis1incsgst = 0;
                    decimal chnnmis1exccgst = 0;
                    decimal chnnmis1sexcgst = 0;

                    //chennel Mis2
                    decimal chnnmis2 = row["Spcl_pack_chrgs_vlMics"].ToString().Equals("") ? 0 : Convert.ToDecimal(row["Spcl_pack_chrgs_vlMics"]); 
                    decimal chnnchnnmis2igst = 0;
                    decimal chnnchnnmis2pmtinclugst = 0;
                    decimal chnnchnnmis2pmtexclgst = 0;
                    decimal chnnmis2inccgst = 0;
                    decimal chnnmis2incsgst = 0;
                    decimal chnnmis2exccgst = 0;
                    decimal chnnmis2sexcgst = 0;


                    //TCS
                    decimal TCS = Convert.ToDecimal(row["sp"].ToString());
                    string TCS2 = Convert.ToString(row["TCS"].ToString());
                    decimal tcsinccgst = 0;
                    decimal tcsexcgst = 0;
                    decimal tcsigst = 0;


                    if (sp <= 0)
                    {

                    }
                    else
                    {
                        if (post.Rows[0]["chksalsprice"].ToString().Equals("True"))
                        {

                            decimal amnt = ((Convert.ToDecimal(sp)) - ((Convert.ToDecimal(sp) * ligst) / 100));
                            if (amnt <= lowhighpt)
                            {
                                taxableamnt = amnt;
                                igst = ((Convert.ToDecimal(sp) * ligst) / 100);
                                cgst = ((Convert.ToDecimal(sp) * licgst) / 100);
                                sgst = ((Convert.ToDecimal(sp) * lisgst) / 100);
                                gst = ligst;
                                //chennelcommisiion
                                if (post.Rows[0]["chckchncomgst"].ToString().Equals("True"))
                                {
                                    chnncommigst = ((chnncomm * ligst) / 100);
                                    chnncommpmtinclugst = chnncomm;
                                    chnncommpmtexclgst = chnncomm - chnncommigst;

                                    chnncomminccgst = chnncomm;
                                    chnncommexccgst = ((chnncomm - chnncomm * licgst) / 100);
                                    chnncommincsgst = chnncomm;
                                    chnncommexcsgst = ((chnncomm - chnncomm * lisgst) / 100);
                                }
                                else
                                {
                                    chnncommigst = ((chnncomm * ligst) / 100);
                                    chnncommpmtinclugst = chnncomm + chnncommigst;
                                    chnncommpmtexclgst = chnncomm;

                                    chnncommexccgst = chnncomm;
                                    chnncomminccgst = ((chnncomm + chnncomm * licgst) / 100);
                                    chnncommexcsgst = chnncomm;
                                    chnncommincsgst = ((chnncomm + chnncomm * lisgst) / 100);
                                }


                                //Channel_gateway
                                if (post.Rows[0]["chckchngategst"].ToString().Equals("True"))
                                {
                                    chnngateigst = ((chnngate * ligst) / 100);
                                    chnngatepmtinclugst = chnngate;
                                    chnngatepmtexclgst = chnngate - chnncommigst;

                                    chnngateinccgst = chnngate;
                                    chnngateexccgst = ((chnngate - chnngate * licgst) / 100);
                                    chnngateincsgst = chnngate;
                                    chnngateexcsgst = ((chnngate - chnngate * lisgst) / 100);
                                }
                                else
                                {
                                    chnngateigst = ((chnngate * ligst) / 100);
                                    chnngatepmtinclugst = chnngate + chnncommigst;
                                    chnngatepmtexclgst = chnngate;


                                    chnngateinccgst = chnngate;
                                    chnngateexccgst = ((chnngate + chnngate * licgst) / 100);
                                    chnngateincsgst = chnngate;
                                    chnngateexcsgst = ((chnngate + chnngate * lisgst) / 100);
                                }


                                //Channel_Logistic
                                if (post.Rows[0]["chckchnloggst"].ToString().Equals("True"))
                                {
                                    chnnchnnlogicigst = ((chnnlogic * ligst) / 100);
                                    chnnchnnlogicpmtinclugst = chnnlogic;
                                    chnnchnnlogicpmtexclgst = chnnlogic - chnnchnnlogicigst;

                                    chnnlogicinccgst = chnnlogic;
                                    chnnlogicexccgst = ((chnnlogic - chnnlogic * licgst) / 100);
                                    chnnlogicincsgst = chnnlogic;
                                    chnnlogicexcsgst = ((chnnlogic - chnnlogic * lisgst) / 100);
                                }
                                else
                                {
                                    chnnchnnlogicigst = ((chnnlogic * ligst) / 100);
                                    chnnchnnlogicpmtinclugst = chnnlogic + chnnchnnlogicigst;
                                    chnnchnnlogicpmtexclgst = chnnlogic;

                                    chnnlogicinccgst = chnnlogic;
                                    chnnlogicexccgst = ((chnnlogic + chnnlogic * licgst) / 100);
                                    chnnlogicincsgst = chnnlogic;
                                    chnnlogicexcsgst = ((chnnlogic + chnnlogic * lisgst) / 100);

                                }

                                //Channel Pnlty
                                if (post.Rows[0]["chckchnpenlgst"].ToString().Equals("True"))
                                {
                                    chnnchnnpnltyigst = ((chnnpnlty * ligst) / 100);
                                    chnnchnnpnltypmtinclugst = chnnpnlty;
                                    chnnchnnpnltypmtexclgst = chnnpnlty - chnnchnnpnltyigst;

                                    chnnpnltyinccgst = chnnpnlty;
                                    chnnpnltyexccgst = ((chnnpnlty - chnnpnlty * licgst) / 100);
                                    chnnpnltyincsgst = chnnpnlty;
                                    chnnpnltyexcsgst = ((chnnpnlty - chnnpnlty * lisgst) / 100);
                                }
                                else
                                {
                                    chnnchnnpnltyigst = ((chnnpnlty * ligst) / 100);
                                    chnnchnnpnltypmtinclugst = chnnpnlty + chnnchnnpnltyigst;
                                    chnnchnnpnltypmtexclgst = chnnpnlty;


                                    chnnpnltyinccgst = chnnpnlty;
                                    chnnpnltyexccgst = ((chnnpnlty + chnnpnlty * licgst) / 100);
                                    chnnpnltyincsgst = chnnpnlty;
                                    chnnpnltyexcsgst = ((chnnpnlty + chnnpnlty * lisgst) / 100);

                                }


                                //Channel Mis1
                                if (post.Rows[0]["chckchnmic1gst"].ToString().Equals("True"))
                                {
                                    chnnchnnmis1igst = ((chnnmis1 * ligst) / 100);
                                    chnnchnnmis1pmtinclugst = chnnmis1;
                                    chnnchnnmis1pmtexclgst = chnnmis1 - chnnchnnmis1igst;

                                    chnnmis1inccgst = chnnmis1;
                                    chnnmis1sexcgst = ((chnnmis1 - chnnmis1 * licgst) / 100);
                                    chnnmis1incsgst = chnnmis1;
                                    chnnmis1exccgst = ((chnnmis1 - chnnmis1 * lisgst) / 100);
                                }
                                else
                                {
                                    chnnchnnmis1igst = ((chnnmis1 * ligst) / 100);
                                    chnnchnnmis1pmtinclugst = chnnmis1 + chnnchnnmis1igst;
                                    chnnchnnmis1pmtexclgst = chnnmis1;

                                    chnnmis1inccgst = chnnmis1;
                                    chnnmis1sexcgst = ((chnnmis1 + chnnmis1 * licgst) / 100);
                                    chnnmis1incsgst = chnnmis1;
                                    chnnmis1exccgst = ((chnnmis1 + chnnmis1 * lisgst) / 100);


                                }



                                //Channel Mis2
                                if (post.Rows[0]["chckchnmic2gst"].ToString().Equals("True"))
                                {
                                    chnnchnnmis2igst = ((chnnmis2 * ligst) / 100);
                                    chnnchnnmis2pmtinclugst = chnnmis2;
                                    chnnchnnmis2pmtexclgst = chnnmis2 - chnnchnnmis2igst;


                                    chnnmis2inccgst = chnnmis2;
                                    chnnmis2sexcgst = ((chnnmis2 - chnnmis2 * licgst) / 100);
                                    chnnmis2incsgst = chnnmis2;
                                    chnnmis2exccgst = ((chnnmis2 - chnnmis2 * lisgst) / 100);
                                }
                                else
                                {
                                    chnnchnnmis2igst = ((chnnmis2 * ligst) / 100);
                                    chnnchnnmis2pmtinclugst = chnnmis2 + chnnchnnmis2igst;
                                    chnnchnnmis2pmtexclgst = chnnmis2;


                                    chnnmis2inccgst = chnnmis2;
                                    chnnmis2sexcgst = ((chnnmis2 + chnnmis2 * licgst) / 100);
                                    chnnmis2incsgst = chnnmis2;
                                    chnnmis2exccgst = ((chnnmis2 + chnnmis2 * lisgst) / 100);

                                }
                                
                                
                            }
                            else
                            {
                                taxableamnt = ((Convert.ToDecimal(sp)) - ((Convert.ToDecimal(sp) * higst) / 100));
                                igst = ((Convert.ToDecimal(sp) * higst) / 100);
                                cgst = ((Convert.ToDecimal(sp) * hicgst) / 100);
                                sgst = ((Convert.ToDecimal(sp) * hisgst) / 100);
                                gst = higst;
                                //chennelcommisiion
                                if (post.Rows[0]["chckchncomgst"].ToString().Equals("True"))
                                {
                                    chnncommigst = ((chnncomm * higst) / 100);
                                    chnncommpmtinclugst = chnncomm;
                                    chnncommpmtexclgst = chnncomm - chnncommigst;

                                    chnncomminccgst = chnncomm;
                                    chnncommexccgst = ((chnncomm - chnncomm * hicgst) / 100);
                                    chnncommincsgst = chnncomm;
                                    chnncommexcsgst = ((chnncomm - chnncomm * hisgst) / 100);
                                }
                                else
                                {
                                    chnncommigst = ((chnncomm * higst) / 100);
                                    chnncommpmtinclugst = chnncomm + chnncommigst;
                                    chnncommpmtexclgst = chnncomm;

                                    chnncommexccgst = chnncomm;
                                    chnncomminccgst = ((chnncomm + chnncomm * hicgst) / 100);
                                    chnncommexcsgst = chnncomm;
                                    chnncommincsgst = ((chnncomm + chnncomm * hisgst) / 100);
                                }


                                //Channel_gateway
                                if (post.Rows[0]["chckchngategst"].ToString().Equals("True"))
                                {
                                    chnngateigst = ((chnngate * higst) / 100);
                                    chnngatepmtinclugst = chnngate;
                                    chnngatepmtexclgst = chnngate - chnncommigst;

                                    chnngateinccgst = chnngate;
                                    chnngateexccgst = ((chnngate - chnngate * hicgst) / 100);
                                    chnngateincsgst = chnngate;
                                    chnngateexcsgst = ((chnngate - chnngate * hisgst) / 100);
                                }
                                else
                                {
                                    chnngateigst = ((chnngate * higst) / 100);
                                    chnngatepmtinclugst = chnngate + chnncommigst;
                                    chnngatepmtexclgst = chnngate;


                                    chnngateinccgst = chnngate;
                                    chnngateexccgst = ((chnngate + chnngate * hicgst) / 100);
                                    chnngateincsgst = chnngate;
                                    chnngateexcsgst = ((chnngate + chnngate * hisgst) / 100);
                                }


                                //Channel_Logistic
                                if (post.Rows[0]["chckchnloggst"].ToString().Equals("True"))
                                {
                                    chnnchnnlogicigst = ((chnnlogic * higst) / 100);
                                    chnnchnnlogicpmtinclugst = chnnlogic;
                                    chnnchnnlogicpmtexclgst = chnnlogic - chnnchnnlogicigst;

                                    chnnlogicinccgst = chnnlogic;
                                    chnnlogicexccgst = ((chnnlogic - chnnlogic * hicgst) / 100);
                                    chnnlogicincsgst = chnnlogic;
                                    chnnlogicexcsgst = ((chnnlogic - chnnlogic * hisgst) / 100);
                                }
                                else
                                {
                                    chnnchnnlogicigst = ((chnnlogic * higst) / 100);
                                    chnnchnnlogicpmtinclugst = chnnlogic + chnnchnnlogicigst;
                                    chnnchnnlogicpmtexclgst = chnnlogic;

                                    chnnlogicinccgst = chnnlogic;
                                    chnnlogicexccgst = ((chnnlogic + chnnlogic * hicgst) / 100);
                                    chnnlogicincsgst = chnnlogic;
                                    chnnlogicexcsgst = ((chnnlogic + chnnlogic * hisgst) / 100);

                                }

                                //Channel Pnlty
                                if (post.Rows[0]["chckchnpenlgst"].ToString().Equals("True"))
                                {
                                    chnnchnnpnltyigst = ((chnnpnlty * higst) / 100);
                                    chnnchnnpnltypmtinclugst = chnnpnlty;
                                    chnnchnnpnltypmtexclgst = chnnpnlty - chnnchnnpnltyigst;

                                    chnnpnltyinccgst = chnnpnlty;
                                    chnnpnltyexccgst = ((chnnpnlty - chnnpnlty * hicgst) / 100);
                                    chnnpnltyincsgst = chnnpnlty;
                                    chnnpnltyexcsgst = ((chnnpnlty - chnnpnlty * hisgst) / 100);
                                }
                                else
                                {
                                    chnnchnnpnltyigst = ((chnnpnlty * higst) / 100);
                                    chnnchnnpnltypmtinclugst = chnnpnlty + chnnchnnpnltyigst;
                                    chnnchnnpnltypmtexclgst = chnnpnlty;


                                    chnnpnltyinccgst = chnnpnlty;
                                    chnnpnltyexccgst = ((chnnpnlty + chnnpnlty * hicgst) / 100);
                                    chnnpnltyincsgst = chnnpnlty;
                                    chnnpnltyexcsgst = ((chnnpnlty + chnnpnlty * hisgst) / 100);

                                }


                                //Channel Mis1
                                if (post.Rows[0]["chckchnmic1gst"].ToString().Equals("True"))
                                {
                                    chnnchnnmis1igst = ((chnnmis1 * higst) / 100);
                                    chnnchnnmis1pmtinclugst = chnnmis1;
                                    chnnchnnmis1pmtexclgst = chnnmis1 - chnnchnnmis1igst;

                                    chnnmis1inccgst = chnnmis1;
                                    chnnmis1sexcgst = ((chnnmis1 - chnnmis1 * hicgst) / 100);
                                    chnnmis1incsgst = chnnmis1;
                                    chnnmis1exccgst = ((chnnmis1 - chnnmis1 * hisgst) / 100);
                                }
                                else
                                {
                                    chnnchnnmis1igst = ((chnnmis1 * higst) / 100);
                                    chnnchnnmis1pmtinclugst = chnnmis1 + chnnchnnmis1igst;
                                    chnnchnnmis1pmtexclgst = chnnmis1;

                                    chnnmis1inccgst = chnnmis1;
                                    chnnmis1sexcgst = ((chnnmis1 + chnnmis1 * hicgst) / 100);
                                    chnnmis1incsgst = chnnmis1;
                                    chnnmis1exccgst = ((chnnmis1 + chnnmis1 * hisgst) / 100);


                                }



                                //Channel Mis2
                                if (post.Rows[0]["chckchnmic2gst"].ToString().Equals("True"))
                                {
                                    chnnchnnmis2igst = ((chnnmis2 * higst) / 100);
                                    chnnchnnmis2pmtinclugst = chnnmis2;
                                    chnnchnnmis2pmtexclgst = chnnmis2 - chnnchnnmis2igst;


                                    chnnmis2inccgst = chnnmis2;
                                    chnnmis2sexcgst = ((chnnmis2 - chnnmis2 * hicgst) / 100);
                                    chnnmis2incsgst = chnnmis2;
                                    chnnmis2exccgst = ((chnnmis2 - chnnmis2 * hisgst) / 100);
                                }
                                else
                                {
                                    chnnchnnmis2igst = ((chnnmis2 * higst) / 100);
                                    chnnchnnmis2pmtinclugst = chnnmis2 + chnnchnnmis2igst;
                                    chnnchnnmis2pmtexclgst = chnnmis2;


                                    chnnmis2inccgst = chnnmis2;
                                    chnnmis2sexcgst = ((chnnmis2 + chnnmis2 * hicgst) / 100);
                                    chnnmis2incsgst = chnnmis2;
                                    chnnmis2exccgst = ((chnnmis2 + chnnmis2 * hisgst) / 100);

                                }

                            }

                            pmtspinclugst = sp;
                            pmtspexclgst = sp - igst;

                            pmtspinclucgst = sp;
                            pmtspexclcgst = sp - licgst;
                            pmtspinclusgst = sp;
                            pmtspexclsgst = sp - lisgst;
                        }
                        else
                        {


                            if (sp <= lowhighpt)
                            {

                                igst = ((Convert.ToDecimal(sp) * ligst) / 100);
                                cgst = ((Convert.ToDecimal(sp) * licgst) / 100);
                                sgst = ((Convert.ToDecimal(sp) * lisgst) / 100);
                                gst = ligst;
                                //chennelcommisiion
                                if (post.Rows[0]["chckchncomgst"].ToString().Equals("True"))
                                {
                                    chnncommigst = ((chnncomm * ligst) / 100);
                                    chnncommpmtinclugst = chnncomm;
                                    chnncommpmtexclgst = chnncomm - chnncommigst;

                                    chnncomminccgst = chnncomm;
                                    chnncommexccgst = ((chnncomm - chnncomm * licgst) / 100);
                                    chnncommincsgst = chnncomm;
                                    chnncommexcsgst = ((chnncomm - chnncomm * lisgst) / 100);
                                }
                                else
                                {
                                    chnncommigst = ((chnncomm * ligst) / 100);
                                    chnncommpmtinclugst = chnncomm + chnncommigst;
                                    chnncommpmtexclgst = chnncomm;

                                    chnncommexccgst = chnncomm;
                                    chnncomminccgst = ((chnncomm + chnncomm * licgst) / 100);
                                    chnncommexcsgst = chnncomm;
                                    chnncommincsgst = ((chnncomm + chnncomm * lisgst) / 100);
                                }


                                //Channel_gateway
                                if (post.Rows[0]["chckchngategst"].ToString().Equals("True"))
                                {
                                    chnngateigst = ((chnngate * ligst) / 100);
                                    chnngatepmtinclugst = chnngate;
                                    chnngatepmtexclgst = chnngate - chnncommigst;

                                    chnngateinccgst = chnngate;
                                    chnngateexccgst = ((chnngate - chnngate * licgst) / 100);
                                    chnngateincsgst = chnngate;
                                    chnngateexcsgst = ((chnngate - chnngate * lisgst) / 100);
                                }
                                else
                                {
                                    chnngateigst = ((chnngate * ligst) / 100);
                                    chnngatepmtinclugst = chnngate + chnncommigst;
                                    chnngatepmtexclgst = chnngate;


                                    chnngateinccgst = chnngate;
                                    chnngateexccgst = ((chnngate + chnngate * licgst) / 100);
                                    chnngateincsgst = chnngate;
                                    chnngateexcsgst = ((chnngate + chnngate * lisgst) / 100);
                                }


                                //Channel_Logistic
                                if (post.Rows[0]["chckchnloggst"].ToString().Equals("True"))
                                {
                                    chnnchnnlogicigst = ((chnnlogic * ligst) / 100);
                                    chnnchnnlogicpmtinclugst = chnnlogic;
                                    chnnchnnlogicpmtexclgst = chnnlogic - chnnchnnlogicigst;

                                    chnnlogicinccgst = chnnlogic;
                                    chnnlogicexccgst = ((chnnlogic - chnnlogic * licgst) / 100);
                                    chnnlogicincsgst = chnnlogic;
                                    chnnlogicexcsgst = ((chnnlogic - chnnlogic * lisgst) / 100);
                                }
                                else
                                {
                                    chnnchnnlogicigst = ((chnnlogic * ligst) / 100);
                                    chnnchnnlogicpmtinclugst = chnnlogic + chnnchnnlogicigst;
                                    chnnchnnlogicpmtexclgst = chnnlogic;

                                    chnnlogicinccgst = chnnlogic;
                                    chnnlogicexccgst = ((chnnlogic + chnnlogic * licgst) / 100);
                                    chnnlogicincsgst = chnnlogic;
                                    chnnlogicexcsgst = ((chnnlogic + chnnlogic * lisgst) / 100);

                                }

                                //Channel Pnlty
                                if (post.Rows[0]["chckchnpenlgst"].ToString().Equals("True"))
                                {
                                    chnnchnnpnltyigst = ((chnnpnlty * ligst) / 100);
                                    chnnchnnpnltypmtinclugst = chnnpnlty;
                                    chnnchnnpnltypmtexclgst = chnnpnlty - chnnchnnpnltyigst;

                                    chnnpnltyinccgst = chnnpnlty;
                                    chnnpnltyexccgst = ((chnnpnlty - chnnpnlty * licgst) / 100);
                                    chnnpnltyincsgst = chnnpnlty;
                                    chnnpnltyexcsgst = ((chnnpnlty - chnnpnlty * lisgst) / 100);
                                }
                                else
                                {
                                    chnnchnnpnltyigst = ((chnnpnlty * ligst) / 100);
                                    chnnchnnpnltypmtinclugst = chnnpnlty + chnnchnnpnltyigst;
                                    chnnchnnpnltypmtexclgst = chnnpnlty;


                                    chnnpnltyinccgst = chnnpnlty;
                                    chnnpnltyexccgst = ((chnnpnlty + chnnpnlty * licgst) / 100);
                                    chnnpnltyincsgst = chnnpnlty;
                                    chnnpnltyexcsgst = ((chnnpnlty + chnnpnlty * lisgst) / 100);

                                }


                                //Channel Mis1
                                if (post.Rows[0]["chckchnmic1gst"].ToString().Equals("True"))
                                {
                                    chnnchnnmis1igst = ((chnnmis1 * ligst) / 100);
                                    chnnchnnmis1pmtinclugst = chnnmis1;
                                    chnnchnnmis1pmtexclgst = chnnmis1 - chnnchnnmis1igst;

                                    chnnmis1inccgst = chnnmis1;
                                    chnnmis1sexcgst = ((chnnmis1 - chnnmis1 * licgst) / 100);
                                    chnnmis1incsgst = chnnmis1;
                                    chnnmis1exccgst = ((chnnmis1 - chnnmis1 * lisgst) / 100);
                                }
                                else
                                {
                                    chnnchnnmis1igst = ((chnnmis1 * ligst) / 100);
                                    chnnchnnmis1pmtinclugst = chnnmis1 + chnnchnnmis1igst;
                                    chnnchnnmis1pmtexclgst = chnnmis1;

                                    chnnmis1inccgst = chnnmis1;
                                    chnnmis1sexcgst = ((chnnmis1 + chnnmis1 * licgst) / 100);
                                    chnnmis1incsgst = chnnmis1;
                                    chnnmis1exccgst = ((chnnmis1 + chnnmis1 * lisgst) / 100);


                                }



                                //Channel Mis2
                                if (post.Rows[0]["chckchnmic2gst"].ToString().Equals("True"))
                                {
                                    chnnchnnmis2igst = ((chnnmis2 * ligst) / 100);
                                    chnnchnnmis2pmtinclugst = chnnmis2;
                                    chnnchnnmis2pmtexclgst = chnnmis2 - chnnchnnmis2igst;


                                    chnnmis2inccgst = chnnmis2;
                                    chnnmis2sexcgst = ((chnnmis2 - chnnmis2 * licgst) / 100);
                                    chnnmis2incsgst = chnnmis2;
                                    chnnmis2exccgst = ((chnnmis2 - chnnmis2 * lisgst) / 100);
                                }
                                else
                                {
                                    chnnchnnmis2igst = ((chnnmis2 * ligst) / 100);
                                    chnnchnnmis2pmtinclugst = chnnmis2 + chnnchnnmis2igst;
                                    chnnchnnmis2pmtexclgst = chnnmis2;


                                    chnnmis2inccgst = chnnmis2;
                                    chnnmis2sexcgst = ((chnnmis2 + chnnmis2 * licgst) / 100);
                                    chnnmis2incsgst = chnnmis2;
                                    chnnmis2exccgst = ((chnnmis2 + chnnmis2 * lisgst) / 100);

                                }

                            }
                            else
                            {

                                igst = ((Convert.ToDecimal(sp) * higst) / 100);
                                cgst = ((Convert.ToDecimal(sp) * hicgst) / 100);
                                sgst = ((Convert.ToDecimal(sp) * hisgst) / 100);
                                gst = higst;
                                //chennelcommisiion
                                if (post.Rows[0]["chckchncomgst"].ToString().Equals("True"))
                                {
                                    chnncommigst = ((chnncomm * higst) / 100);
                                    chnncommpmtinclugst = chnncomm;
                                    chnncommpmtexclgst = chnncomm - chnncommigst;

                                    chnncomminccgst = chnncomm;
                                    chnncommexccgst = ((chnncomm - chnncomm * hicgst) / 100);
                                    chnncommincsgst = chnncomm;
                                    chnncommexcsgst = ((chnncomm - chnncomm * hisgst) / 100);
                                }
                                else
                                {
                                    chnncommigst = ((chnncomm * higst) / 100);
                                    chnncommpmtinclugst = chnncomm + chnncommigst;
                                    chnncommpmtexclgst = chnncomm;

                                    chnncommexccgst = chnncomm;
                                    chnncomminccgst = ((chnncomm + chnncomm * hicgst) / 100);
                                    chnncommexcsgst = chnncomm;
                                    chnncommincsgst = ((chnncomm + chnncomm * hisgst) / 100);
                                }


                                //Channel_gateway
                                if (post.Rows[0]["chckchngategst"].ToString().Equals("True"))
                                {
                                    chnngateigst = ((chnngate * higst) / 100);
                                    chnngatepmtinclugst = chnngate;
                                    chnngatepmtexclgst = chnngate - chnncommigst;

                                    chnngateinccgst = chnngate;
                                    chnngateexccgst = ((chnngate - chnngate * hicgst) / 100);
                                    chnngateincsgst = chnngate;
                                    chnngateexcsgst = ((chnngate - chnngate * hisgst) / 100);
                                }
                                else
                                {
                                    chnngateigst = ((chnngate * higst) / 100);
                                    chnngatepmtinclugst = chnngate + chnncommigst;
                                    chnngatepmtexclgst = chnngate;


                                    chnngateinccgst = chnngate;
                                    chnngateexccgst = ((chnngate + chnngate * hicgst) / 100);
                                    chnngateincsgst = chnngate;
                                    chnngateexcsgst = ((chnngate + chnngate * hisgst) / 100);
                                }


                                //Channel_Logistic
                                if (post.Rows[0]["chckchnloggst"].ToString().Equals("True"))
                                {
                                    chnnchnnlogicigst = ((chnnlogic * higst) / 100);
                                    chnnchnnlogicpmtinclugst = chnnlogic;
                                    chnnchnnlogicpmtexclgst = chnnlogic - chnnchnnlogicigst;

                                    chnnlogicinccgst = chnnlogic;
                                    chnnlogicexccgst = ((chnnlogic - chnnlogic * hicgst) / 100);
                                    chnnlogicincsgst = chnnlogic;
                                    chnnlogicexcsgst = ((chnnlogic - chnnlogic * hisgst) / 100);
                                }
                                else
                                {
                                    chnnchnnlogicigst = ((chnnlogic * higst) / 100);
                                    chnnchnnlogicpmtinclugst = chnnlogic + chnnchnnlogicigst;
                                    chnnchnnlogicpmtexclgst = chnnlogic;

                                    chnnlogicinccgst = chnnlogic;
                                    chnnlogicexccgst = ((chnnlogic + chnnlogic * hicgst) / 100);
                                    chnnlogicincsgst = chnnlogic;
                                    chnnlogicexcsgst = ((chnnlogic + chnnlogic * hisgst) / 100);

                                }

                                //Channel Pnlty
                                if (post.Rows[0]["chckchnpenlgst"].ToString().Equals("True"))
                                {
                                    chnnchnnpnltyigst = ((chnnpnlty * higst) / 100);
                                    chnnchnnpnltypmtinclugst = chnnpnlty;
                                    chnnchnnpnltypmtexclgst = chnnpnlty - chnnchnnpnltyigst;

                                    chnnpnltyinccgst = chnnpnlty;
                                    chnnpnltyexccgst = ((chnnpnlty - chnnpnlty * hicgst) / 100);
                                    chnnpnltyincsgst = chnnpnlty;
                                    chnnpnltyexcsgst = ((chnnpnlty - chnnpnlty * hisgst) / 100);
                                }
                                else
                                {
                                    chnnchnnpnltyigst = ((chnnpnlty * higst) / 100);
                                    chnnchnnpnltypmtinclugst = chnnpnlty + chnnchnnpnltyigst;
                                    chnnchnnpnltypmtexclgst = chnnpnlty;


                                    chnnpnltyinccgst = chnnpnlty;
                                    chnnpnltyexccgst = ((chnnpnlty + chnnpnlty * hicgst) / 100);
                                    chnnpnltyincsgst = chnnpnlty;
                                    chnnpnltyexcsgst = ((chnnpnlty + chnnpnlty * hisgst) / 100);

                                }


                                //Channel Mis1
                                if (post.Rows[0]["chckchnmic1gst"].ToString().Equals("True"))
                                {
                                    chnnchnnmis1igst = ((chnnmis1 * higst) / 100);
                                    chnnchnnmis1pmtinclugst = chnnmis1;
                                    chnnchnnmis1pmtexclgst = chnnmis1 - chnnchnnmis1igst;

                                    chnnmis1inccgst = chnnmis1;
                                    chnnmis1sexcgst = ((chnnmis1 - chnnmis1 * hicgst) / 100);
                                    chnnmis1incsgst = chnnmis1;
                                    chnnmis1exccgst = ((chnnmis1 - chnnmis1 * hisgst) / 100);
                                }
                                else
                                {
                                    chnnchnnmis1igst = ((chnnmis1 * higst) / 100);
                                    chnnchnnmis1pmtinclugst = chnnmis1 + chnnchnnmis1igst;
                                    chnnchnnmis1pmtexclgst = chnnmis1;

                                    chnnmis1inccgst = chnnmis1;
                                    chnnmis1sexcgst = ((chnnmis1 + chnnmis1 * hicgst) / 100);
                                    chnnmis1incsgst = chnnmis1;
                                    chnnmis1exccgst = ((chnnmis1 + chnnmis1 * hisgst) / 100);


                                }



                                //Channel Mis2
                                if (post.Rows[0]["chckchnmic2gst"].ToString().Equals("True"))
                                {
                                    chnnchnnmis2igst = ((chnnmis2 * higst) / 100);
                                    chnnchnnmis2pmtinclugst = chnnmis2;
                                    chnnchnnmis2pmtexclgst = chnnmis2 - chnnchnnmis2igst;


                                    chnnmis2inccgst = chnnmis2;
                                    chnnmis2sexcgst = ((chnnmis2 - chnnmis2 * hicgst) / 100);
                                    chnnmis2incsgst = chnnmis2;
                                    chnnmis2exccgst = ((chnnmis2 - chnnmis2 * hisgst) / 100);
                                }
                                else
                                {
                                    chnnchnnmis2igst = ((chnnmis2 * higst) / 100);
                                    chnnchnnmis2pmtinclugst = chnnmis2 + chnnchnnmis2igst;
                                    chnnchnnmis2pmtexclgst = chnnmis2;


                                    chnnmis2inccgst = chnnmis2;
                                    chnnmis2sexcgst = ((chnnmis2 + chnnmis2 * hicgst) / 100);
                                    chnnmis2incsgst = chnnmis2;
                                    chnnmis2exccgst = ((chnnmis2 + chnnmis2 * hisgst) / 100);

                                }
                            }

                            pmtspexclgst = sp;
                            pmtspinclugst = sp + igst;


                            pmtspinclucgst = sp + licgst;
                            pmtspexclcgst = sp;
                            pmtspinclusgst = sp + lisgst;
                            pmtspexclsgst = sp;
                        }
                    }
                    if (satecode.Equals("27"))
                    {
                        pmtspexclgst = 0;
                        pmtspinclugst = 0;
                        chnncommpmtexclgst = 0;
                        chnncommpmtinclugst = 0;
                        chnngatepmtexclgst = 0;
                        chnngatepmtinclugst = 0;
                        chnnchnnpnltypmtexclgst = 0;
                        chnnchnnpnltypmtinclugst = 0;
                        chnnchnnlogicpmtexclgst = 0;
                        chnnchnnlogicpmtinclugst = 0;
                        chnnchnnmis1pmtexclgst = 0;
                        chnnchnnmis1pmtinclugst = 0;
                        chnnchnnmis2pmtexclgst = 0;
                        chnnchnnmis2pmtinclugst = 0;


                        pmtspinclucgst = pmtspinclucgst;
                        pmtspexclcgst = pmtspexclcgst;
                        pmtspinclusgst = pmtspinclusgst;
                        pmtspexclsgst = pmtspexclsgst;

                        chnncomminccgst = chnncomminccgst;
                        chnncommexccgst = chnncommexccgst;
                        chnncommincsgst = chnncommincsgst;
                        chnncommexcsgst = chnncommexcsgst;


                        chnngateinccgst = chnngateinccgst;
                        chnngateexccgst = chnngateexccgst;
                        chnngateincsgst = chnngateincsgst;
                        chnngateexcsgst = chnngateexcsgst;


                        chnngateinccgst = chnngateinccgst;
                        chnngateexccgst = chnngateexccgst;
                        chnngateincsgst = chnngateincsgst;
                        chnngateexcsgst = chnngateexcsgst;


                        chnnpnltyinccgst = chnnpnltyinccgst;
                        chnnpnltyincsgst = chnnpnltyincsgst;
                        chnnpnltyexccgst = chnnpnltyexccgst;
                        chnnpnltyexcsgst = chnnpnltyexcsgst;

                        chnnmis2inccgst = chnnmis2inccgst;
                        chnnmis2sexcgst = chnnmis2sexcgst;
                        chnnmis2incsgst = chnnmis2incsgst;
                        chnnmis2exccgst = chnnmis2exccgst;

                        chnnmis1inccgst = chnnmis1inccgst;
                        chnnmis1sexcgst = chnnmis1sexcgst;
                        chnnmis1incsgst = chnnmis1incsgst;
                        chnnmis1exccgst = chnnmis1exccgst;
                    }
                    else
                    {
                        pmtspexclgst = pmtspexclgst;
                        pmtspinclugst = pmtspinclugst;
                        chnncommpmtexclgst = chnncommpmtexclgst;
                        chnncommpmtinclugst = chnncommpmtinclugst;
                        chnngatepmtexclgst = chnngatepmtexclgst;
                        chnngatepmtinclugst = chnngatepmtinclugst;
                        chnnchnnpnltypmtexclgst = chnnchnnpnltypmtexclgst;
                        chnnchnnpnltypmtinclugst = chnnchnnpnltypmtinclugst;
                        chnnchnnlogicpmtexclgst = chnnchnnlogicpmtexclgst;
                        chnnchnnlogicpmtinclugst = chnnchnnlogicpmtinclugst;
                        chnnchnnmis1pmtexclgst = chnnchnnmis1pmtexclgst;
                        chnnchnnmis1pmtinclugst = chnnchnnmis1pmtinclugst;
                        chnnchnnmis2pmtexclgst = chnnchnnmis2pmtexclgst;
                        chnnchnnmis2pmtinclugst = chnnchnnmis2pmtinclugst;

                        pmtspinclucgst = 0;
                        pmtspexclcgst = 0;
                        pmtspinclusgst = 0;
                        pmtspexclsgst = 0;

                        chnncomminccgst = 0;
                        chnncommexccgst = 0;
                        chnncommincsgst = 0;
                        chnncommexcsgst = 0;


                        chnngateinccgst = 0;
                        chnngateexccgst = 0;
                        chnngateincsgst = 0;
                        chnngateexcsgst = 0;


                        chnngateinccgst = 0;
                        chnngateexccgst = 0;
                        chnngateincsgst = 0;
                        chnngateexcsgst = 0;


                        chnnpnltyinccgst = 0;
                        chnnpnltyincsgst = 0;
                        chnnpnltyexccgst = 0;
                        chnnpnltyexcsgst = 0;

                        chnnmis2inccgst = 0;
                        chnnmis2sexcgst = 0;
                        chnnmis2incsgst = 0;
                        chnnmis2exccgst = 0;

                        chnnmis1inccgst = 0;
                        chnnmis1sexcgst = 0;
                        chnnmis1incsgst = 0;
                        chnnmis1exccgst = 0;
                    }

                    //TCS
                    if (post.Rows[0]["TCSchck"].ToString().Equals("True"))
                    {
                        if(TCS2.ToString().Equals("TCS"))
                        {
                            tcsigst = ((TCS * 1) / 100);
                            tcsinccgst = TCS;
                            tcsexcgst = TCS - tcsigst;
                        }
                        
                    }
                    else
                    {
                        if (TCS2.ToString().Equals("TCS"))
                        {
                            tcsigst = ((TCS * 1) / 100);
                            tcsinccgst = TCS + tcsigst;
                            tcsexcgst = TCS;
                        }
                    }
                    #endregion
                    command.CommandText = "Update payment_trans set  pmt_sp_ex_gst=@pmt_sp_ex_gst,pmt_sp_inc_gst=@pmt_sp_inc_gst,pmt_scomm_ex_gst=@pmt_scomm_ex_gst," +
                        "pmt_scomm_inc_gst=@pmt_scomm_inc_gst,pmt_cgate_ex_gst=@pmt_cgate_ex_gst,pmt_cgate_inc_gst=@pmt_cgate_inc_gst,pmt_vlpenl_ex_gst=@pmt_vlpenl_ex_gst," +
                        "pmt_vlpenl_inc_gst=@pmt_vlpenl_inc_gst,pmt_vllogi_ex_gst=@pmt_vllogi_ex_gst,pmt_vllogi_inc_gst=@pmt_vllogi_inc_gst,pmt_mic1_ex_gst=@pmt_mic1_ex_gst," +
                        "pmt_mic1_inc_gst=@pmt_mic1_inc_gst,pmt_mic2_ex_gst=@pmt_mic2_ex_gst,pmt_mic2_inc_gst=@pmt_mic2_inc_gst,percentage=@percentage," +
                        "pmt_scomm_inc_cgst=@pmt_scomm_inc_cgst,pmt_scomm_ex_cgst=@pmt_scomm_ex_cgst,pmt_scomm_inc_sgst=@pmt_scomm_inc_sgst,pmt_scomm_ex_sgst=@pmt_scomm_ex_sgst," +
                        "pmt_cgate_inc_cgst=@pmt_cgate_inc_cgst,pmt_cgate_ex_cgst=@pmt_cgate_ex_cgst,pmt_cgate_inc_sgst=@pmt_cgate_inc_sgst,pmt_cgate_ex_sgst=@pmt_cgate_ex_sgst," +
                        "pmt_vllogi_inc_cgst=@pmt_vllogi_inc_cgst,pmt_vllogi_ex_cgst=@pmt_vllogi_ex_cgst,pmt_vllogi_inc_sgst=@pmt_vllogi_inc_sgst,pmt_vllogi_ex_sgst=@pmt_vllogi_ex_sgst," +
                        "pmt_mic1_inc_cgst=@pmt_mic1_inc_cgst,pmt_mic1_ex_cgst=@pmt_mic1_ex_cgst,pmt_mic1_inc_sgst=@pmt_mic1_inc_sgst,pmt_mic1_ex_sgst=@pmt_mic1_ex_sgst," +
                        "pmt_mic2_inc_cgst=@pmt_mic2_inc_cgst,pmt_mic2_ex_cgst=@pmt_mic2_ex_cgst,pmt_mic2_inc_sgst=@pmt_mic2_inc_sgst,pmt_mic2_ex_sgst=@pmt_mic2_ex_sgst,tcsincgst=@tcsincgst,tcsexcgst=@tcsexcgst where Pt_id=@Pt_id";


                    command.Parameters.AddWithValue("@pmt_sp_ex_gst", pmtspexclgst);
                    command.Parameters.AddWithValue("@pmt_sp_inc_gst", pmtspinclugst);
                    command.Parameters.AddWithValue("@pmt_scomm_ex_gst", chnncommpmtexclgst);
                    command.Parameters.AddWithValue("@pmt_scomm_inc_gst", chnncommpmtinclugst);
                    command.Parameters.AddWithValue("@pmt_cgate_ex_gst", chnngatepmtexclgst);
                    command.Parameters.AddWithValue("@pmt_cgate_inc_gst", chnngatepmtinclugst);
                    command.Parameters.AddWithValue("@pmt_vlpenl_ex_gst", chnnchnnpnltypmtexclgst);
                    command.Parameters.AddWithValue("@pmt_vlpenl_inc_gst", chnnchnnpnltypmtinclugst);
                    command.Parameters.AddWithValue("@pmt_vllogi_ex_gst", chnnchnnlogicpmtexclgst);
                    command.Parameters.AddWithValue("@pmt_vllogi_inc_gst", chnnchnnlogicpmtinclugst);
                    command.Parameters.AddWithValue("@pmt_mic1_ex_gst", chnnchnnmis1pmtexclgst);
                    command.Parameters.AddWithValue("@pmt_mic1_inc_gst", chnnchnnmis1pmtinclugst);

                    command.Parameters.AddWithValue("@pmt_mic2_ex_gst", chnnchnnmis2pmtexclgst);
                    command.Parameters.AddWithValue("@pmt_mic2_inc_gst", chnnchnnmis2pmtinclugst);
                    command.Parameters.AddWithValue("@percentage", gst);

                    command.Parameters.AddWithValue("@pmt_scomm_inc_cgst", chnncomminccgst);
                    command.Parameters.AddWithValue("@pmt_scomm_ex_cgst", chnncommexccgst);
                    command.Parameters.AddWithValue("@pmt_scomm_inc_sgst", chnncommincsgst);
                    command.Parameters.AddWithValue("@pmt_scomm_ex_sgst", chnncommexcsgst);

                    command.Parameters.AddWithValue("@pmt_cgate_inc_cgst", chnngateinccgst);
                    command.Parameters.AddWithValue("@pmt_cgate_ex_cgst", chnngateexccgst);
                    command.Parameters.AddWithValue("@pmt_cgate_inc_sgst", chnngateincsgst);
                    command.Parameters.AddWithValue("@pmt_cgate_ex_sgst", chnngateexcsgst);



                    command.Parameters.AddWithValue("@pmt_vllogi_inc_cgst", chnnlogicinccgst);
                    command.Parameters.AddWithValue("@pmt_vllogi_ex_cgst", chnnlogicexccgst);
                    command.Parameters.AddWithValue("@pmt_vllogi_inc_sgst", chnnlogicincsgst);
                    command.Parameters.AddWithValue("@pmt_vllogi_ex_sgst", chnnlogicexcsgst);


                    command.Parameters.AddWithValue("@pmt_chnpent_inc_cgst", chnnpnltyinccgst);
                    command.Parameters.AddWithValue("@pmt_chnpent_exc_cgst", chnnpnltyincsgst);
                    command.Parameters.AddWithValue("@pmt_chnpent_inc_sgst", chnnpnltyexccgst);
                    command.Parameters.AddWithValue("@pmt_chnpent_exc_sgst", chnnpnltyexcsgst);

                    command.Parameters.AddWithValue("@pmt_mic1_inc_cgst", chnnmis1inccgst);
                    command.Parameters.AddWithValue("@pmt_mic1_ex_cgst", chnnmis1sexcgst);
                    command.Parameters.AddWithValue("@pmt_mic1_inc_sgst", chnnmis1incsgst);
                    command.Parameters.AddWithValue("@pmt_mic1_ex_sgst", chnnmis1exccgst);

                    command.Parameters.AddWithValue("@pmt_mic2_inc_cgst", chnnmis2inccgst);
                    command.Parameters.AddWithValue("@pmt_mic2_ex_cgst", chnnmis2sexcgst);
                    command.Parameters.AddWithValue("@pmt_mic2_inc_sgst", chnnmis2incsgst);
                    command.Parameters.AddWithValue("@pmt_mic2_ex_sgst", chnnmis2exccgst);


                    command.Parameters.AddWithValue("@pmt_sp_inc_cgst", pmtspinclucgst);
                    command.Parameters.AddWithValue("@pmt_sp_ex_cgst", pmtspexclcgst);
                    command.Parameters.AddWithValue("@pmt_sp_inc_sgst", pmtspinclusgst);
                    command.Parameters.AddWithValue("@pmt_sp_ex_sgst", pmtspexclsgst);

                    command.Parameters.AddWithValue("@tcsincgst", tcsinccgst);
                    command.Parameters.AddWithValue("@tcsexcgst", tcsexcgst);
                   

                    command.Parameters.AddWithValue("@Pt_id", row["Pt_id"].ToString());
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    line++;
                }

            }
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
                //return -1;
                return line;
            }
            catch (Exception ext)
            {
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ext);
            }
        }

        return result;
    }


    public DataTable Addmanualenteris(DataTable dt, string virtuallocation)
    {
        DataTable dtwrong = new DataTable();
        dtwrong=dt.Clone();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;
        DataTable table = new DataTable();
        // Start a local transaction.
        transaction = connection.BeginTransaction("Addmanualenteris");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {

            foreach (DataRow dr in dt.Rows)
            {
                DataTable dtbar = new DataTable();
                command.CommandText = "select * from " +
                                    "(select Distinct a.StockupID as id from ArchiveStockUpInward a  inner join salesrecord s  on a.ArchiveStockupID = s.archiveid where s.salesidgivenbyvloc =@salesidgivenbyvloc1 and a.BarcodeNo=@BarcodeNo1 and s.archiveid != -1 " +
                                    "union all " +
                                    "select Distinct a.StockupID as id from ArchiveStockUpInward a  inner join salesrecord s  on a.StockupID = s.itemid where s.salesidgivenbyvloc =@salesidgivenbyvloc5 and a.BarcodeNo=@BarcodeNo2 " +
                                    "union all " +
                                    "select  Distinct i.StockupID as id from StockUpInward i  inner join salesrecord s1 on s1.itemid = i.StockupID  where s1.salesidgivenbyvloc =@salesidgivenbyvloc2 and i.BarcodeNo=@BarcodeNo3 and s1.itemid != -1  " +
                                    "union all " +
                                    "select Distinct a.StockupID as id from ArchiveStockUpInward a  inner join cancelTrans s  on a.ArchiveStockupID = s.archiveid where s.salesidgivenbyvloc = @salesidgivenbyvloc3 and a.BarcodeNo=@BarcodeNo4 and s.archiveid != -1 " +
                                    "union all " +
                                    "select Distinct i.StockupID as id from StockUpInward i  inner join cancelTrans s1 on s1.itemid = i.StockupID  where s1.salesidgivenbyvloc =@salesidgivenbyvloc4 and i.BarcodeNo=@BarcodeNo5 and s1.itemid != -1" +
                                    "union all " +
                                    "select Distinct a.StockupID as id from ArchiveStockUpInward a  inner join cancelTrans s  on a.StockupID = s.itemid where s.salesidgivenbyvloc =@salesidgivenbyvloc6 and a.BarcodeNo=@BarcodeNo6 ) y" +

               command.Parameters.AddWithValue("@salesidgivenbyvloc1", dr["salesid"]);
                command.Parameters.AddWithValue("@salesidgivenbyvloc2", dr["salesid"]);
                command.Parameters.AddWithValue("@salesidgivenbyvloc3", dr["salesid"]);
                command.Parameters.AddWithValue("@salesidgivenbyvloc4", dr["salesid"]);
                command.Parameters.AddWithValue("@salesidgivenbyvloc5", dr["salesid"]);
                command.Parameters.AddWithValue("@salesidgivenbyvloc6", dr["salesid"]);
                command.Parameters.AddWithValue("@BarcodeNo1", dr["barcode"]);
                command.Parameters.AddWithValue("@BarcodeNo2", dr["barcode"]);
                command.Parameters.AddWithValue("@BarcodeNo3", dr["barcode"]);
                command.Parameters.AddWithValue("@BarcodeNo4", dr["barcode"]);
                command.Parameters.AddWithValue("@BarcodeNo5", dr["barcode"]);
                command.Parameters.AddWithValue("@BarcodeNo6", dr["barcode"]);
                dtbar.Load(command.ExecuteReader());
                command.Parameters.Clear();
                if (!dtbar.Rows.Count.Equals(0))
                {

                    command.CommandText = "Insert Into payment_trans (stockupId,type,sp,salesid,vlocid,channel_commsion,Channel_Gateway,Total_Igst,Total_Cgst,Total_Sgst," +
                                                               "VL_Logistics,VLPenalty,totaldecincgst,totaldecexcgst,tcsincgst,tcsexcgst,Payable_Amoun,order_date,manualornot)" +
                                                               "values(@stockupId,@type,@sp,@salesid,@vlocid,@channel_commsion,@Channel_Gateway,@Total_Igst,@Total_Cgst,@Total_Sgst," +
                                                               "@VL_Logistics,@VLPenalty,@totaldecincgst,@totaldecexcgst,@tcsincgst,@tcsexcgst,@Payable_Amoun,@order_date,@manualornot)";


                    command.Parameters.AddWithValue("@stockupId", dtbar.Rows[0]["id"].ToString());
                    command.Parameters.AddWithValue("@salesid", dr["salesid"]);
                    command.Parameters.AddWithValue("@type", dr["Type"]);
                    command.Parameters.AddWithValue("@sp", dr["Sp"]);
                    command.Parameters.AddWithValue("@vlocid", virtuallocation);
                    command.Parameters.AddWithValue("@channel_commsion", dr["channelcomm"]);
                    command.Parameters.AddWithValue("@Channel_Gateway", dr["channelgate"]);
                    command.Parameters.AddWithValue("@VL_Logistics", dr["logistic"]);
                    command.Parameters.AddWithValue("@VLPenalty", dr["penalty"]);
                    command.Parameters.AddWithValue("@totaldecincgst", dr["totaldecincgst"]);
                    command.Parameters.AddWithValue("@totaldecexcgst", dr["totaldecexcgst"]);
                    command.Parameters.AddWithValue("@tcsincgst", dr["tcsincgst"]);
                    command.Parameters.AddWithValue("@tcsexcgst", dr["tcsexcgst"]);
                    command.Parameters.AddWithValue("@Payable_Amoun", dr["payableamt"]);
                    command.Parameters.AddWithValue("@order_date", dr["orderdat"]);
                    command.Parameters.AddWithValue("@manualornot", "Yes");
                    command.Parameters.AddWithValue("@Total_Igst", dr["totaligst"]); 
                    command.Parameters.AddWithValue("@Total_Cgst", dr["totalcgst"]);
                    command.Parameters.AddWithValue("@Total_Sgst", dr["totalsgst"]);
                    //command.Parameters.AddWithValue("@pmt_sp_inc_gst", dr["Spincgst"]);,pmt_sp_inc_gst,pmt_sp_ex_gst
                    //command.Parameters.AddWithValue("@pmt_sp_ex_gst", dr["Spexcgst"]);  ,@pmt_sp_inc_gst,pmt_sp_ex_gst
                    command.ExecuteNonQuery();

                    command.Parameters.Clear();
                }
                else
                {
                    dtwrong.Rows.Add(dr.ItemArray);
                }
            }

           
            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();
            return dtwrong;
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
                return null;
            }
            catch (Exception ex2)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex2);
                return null;

            }
        }
    }
}


