using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


/// <summary>
/// Summary description for VirtualLoactionCls
/// </summary>
public class VirtualLoactionCls
{
    public VirtualLoactionCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int addVirtualLocation(string LocationTypeID, string LTypeID2, string Location, string Contact, string Address,DataTable dt,string cmgfrmup, string locid)
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
        transaction = connection.BeginTransaction("addVirtualLocation");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            int result = 0;
            int vlocid = 0;
            if (!cmgfrmup.Equals("cmgfrmup"))
            {


                

                command.CommandText = "INSERT INTO Location(LocationTypeID, LTypeID2, Location, Contact, Address) " +
                                                            "VALUES(@LocationTypeID, @LTypeID2, @Location, @Contact, @Address)"
                                                            + "SELECT CAST(scope_identity() AS int)"; ;
                command.Parameters.AddWithValue("@LocationTypeID", LocationTypeID);
                command.Parameters.AddWithValue("@LTypeID2", LTypeID2);
                command.Parameters.AddWithValue("@Location", Location);
                command.Parameters.AddWithValue("@Contact", Contact);
                command.Parameters.AddWithValue("@Address", Address);

                //command.ExecuteNonQuery();
                vlocid = (Int32)command.ExecuteScalar();
                command.Parameters.Clear();
            }
            command.CommandText = "Insert Into Pymt_trans_setting (salesid,type,sp,channel_commsion,Channel_Gateway,VL_Logistics,VLPenalty,Pack_Charges_VL_Misc,Spcl_pack_chrgs_vlMics," +
                "mp_commission_cgst,pg_commission_cgst,logistics_cgst,TCS_CGST,mp_commission_igst,pg_commission_igst,logistics_igst,TCS_IGST," +
                "mp_commission_sgst,pg_commission_sgst,logistics_sgst,TCS_GST,Total_Cgst,Total_Igst,Total_Sgst,totalsales_taxliable_gstbeforeadustingTCS," +
                "Payment_Status,Merchant_SKU,Settlement_Date,Payment_Type,Payable_Amoun,PG_UTR,packfee_for_return,spcl_pack_fee_for_return,pymtfee_for_return,logistfee_for_return,reverse_logistfee_for_return," +
                "order_date,closing_fees,other_charges,Misc_charge,chksalsprice,chckstatus,shipping_service,transfer,orders,refund,vloc_id," +
                "chckchncomgst,chckchngategst,chckchnloggst,chckchnpenlgst,chckchnmic1gst,chckchnmic2gst,productid,productname)" +
                                   "Values(@salesid,@type,@sp,@channel_commsion,@Channel_Gateway,@VL_Logistics,@VLPenalty,@Pack_Charges_VL_Misc,@Spcl_pack_chrgs_vlMics," +
                                   "@mp_commission_cgst,@pg_commission_cgst,@logistics_cgst,@TCS_CGST,@mp_commission_igst,@pg_commission_igst,@logistics_igst,@TCS_IGST," +
                                   "@mp_commission_sgst,@pg_commission_sgst,@logistics_sgst,@TCS_GST,@Total_Cgst,@Total_Igst,@Total_Sgst,@totalsales_taxliable_gstbeforeadustingTCS," +
                                   "@Payment_Status,@Merchant_SKU,@Settlement_Date,@Payment_Type,@Payable_Amoun,@PG_UTR,@packfee_for_return,@spcl_pack_fee_for_return,@pymtfee_for_return,@logistfee_for_return,@reverse_logistfee_for_return," +
                                   "@order_date,@closing_fees,@other_charges,@Misc_charge,@chksalsprice,@chckstatus,@shipping_service,@transfer,@orders,@refund," +
                                   "@vloc_id,@chckchncomgst,@chckchngategst,@chckchnloggst,@chckchnpenlgst,@chckchnmic1gst,@chckchnmic2gst,@productid,@productname)";

            command.Parameters.AddWithValue("@salesid", dt.Rows[0]["txtstockupId"].ToString());
            command.Parameters.AddWithValue("@type", dt.Rows[0]["txttype"].ToString());
            command.Parameters.AddWithValue("@sp", dt.Rows[0]["txtMRP"].ToString());
            command.Parameters.AddWithValue("@channel_commsion", dt.Rows[0]["txtchannel_commsion"].ToString());
            command.Parameters.AddWithValue("@Channel_Gateway", dt.Rows[0]["txtChannel_Gateway"].ToString());
            command.Parameters.AddWithValue("@VL_Logistics", dt.Rows[0]["txtVL_Logistics"].ToString());
            command.Parameters.AddWithValue("@VLPenalty", dt.Rows[0]["txtVLPenalty"].ToString());
            command.Parameters.AddWithValue("@Pack_Charges_VL_Misc", dt.Rows[0]["txtPack_Charges_VL_Misc"].ToString());
            command.Parameters.AddWithValue("@Spcl_pack_chrgs_vlMics", dt.Rows[0]["txtSpcl_pack_chrgs_vlMics"].ToString());

            command.Parameters.AddWithValue("@mp_commission_cgst", dt.Rows[0]["txtmp_commission_cgst"].ToString());
            command.Parameters.AddWithValue("@pg_commission_cgst", dt.Rows[0]["txtpg_commission_cgst"].ToString());
            command.Parameters.AddWithValue("@logistics_cgst", dt.Rows[0]["txtlogistics_cgst"].ToString());
            command.Parameters.AddWithValue("@TCS_CGST", dt.Rows[0]["txtTCS_CGST"].ToString());

            command.Parameters.AddWithValue("@mp_commission_igst", dt.Rows[0]["txtmp_commission_igst"].ToString());
            command.Parameters.AddWithValue("@pg_commission_igst", dt.Rows[0]["txtpg_commission_igst"].ToString());
            command.Parameters.AddWithValue("@logistics_igst", dt.Rows[0]["txtlogistics_igst"].ToString());
            command.Parameters.AddWithValue("@TCS_IGST", dt.Rows[0]["txtTCS_IGST"].ToString());

            command.Parameters.AddWithValue("@mp_commission_sgst", dt.Rows[0]["txtmp_commission_sgst"].ToString());
            command.Parameters.AddWithValue("@pg_commission_sgst", dt.Rows[0]["txtpg_commission_sgst"].ToString());
            command.Parameters.AddWithValue("@logistics_sgst", dt.Rows[0]["txtlogistics_sgst"].ToString());
            command.Parameters.AddWithValue("@TCS_GST", dt.Rows[0]["txtTCS_GST"].ToString());

            command.Parameters.AddWithValue("@Total_Cgst", dt.Rows[0]["txtTotal_Cgst"].ToString());
            command.Parameters.AddWithValue("@Total_Igst", dt.Rows[0]["txtTotal_Igst"].ToString());
            command.Parameters.AddWithValue("@Total_Sgst", dt.Rows[0]["txtTotal_Sgst"].ToString());

            command.Parameters.AddWithValue("@totalsales_taxliable_gstbeforeadustingTCS", dt.Rows[0]["txttotalsales_taxliable_gstbeforeadustingTCS"].ToString());

            command.Parameters.AddWithValue("@Payment_Status", dt.Rows[0]["txtPayment_Status"].ToString());
            command.Parameters.AddWithValue("@Merchant_SKU", dt.Rows[0]["txtMerchant_SKU"].ToString());
            command.Parameters.AddWithValue("@Settlement_Date", dt.Rows[0]["txtSettlement_Date"].ToString());
            command.Parameters.AddWithValue("@Payment_Type", dt.Rows[0]["txtPayment_Type"].ToString());
            command.Parameters.AddWithValue("@Payable_Amoun", dt.Rows[0]["txtPayable_Amoun"].ToString());
            command.Parameters.AddWithValue("@PG_UTR", dt.Rows[0]["txtPG_UTR"].ToString());
            command.Parameters.AddWithValue("@packfee_for_return", dt.Rows[0]["txtpackfee_for_return"].ToString());
            command.Parameters.AddWithValue("@spcl_pack_fee_for_return", dt.Rows[0]["txtspcl_pack_fee_for_return"].ToString());

            command.Parameters.AddWithValue("@pymtfee_for_return", dt.Rows[0]["txtpymtfee_for_return"].ToString());
            command.Parameters.AddWithValue("@logistfee_for_return", dt.Rows[0]["txtlogistfee_for_return"].ToString());
            command.Parameters.AddWithValue("@reverse_logistfee_for_return", dt.Rows[0]["txtlogistfee_for_return"].ToString());
            command.Parameters.AddWithValue("@order_date", dt.Rows[0]["txtorder_date"].ToString());

            command.Parameters.AddWithValue("@closing_fees", dt.Rows[0]["txtclosing_fees"].ToString());
            command.Parameters.AddWithValue("@other_charges", dt.Rows[0]["txtother_charges"].ToString());
            command.Parameters.AddWithValue("@Misc_charge", dt.Rows[0]["txtMisc_charge"].ToString());
            command.Parameters.AddWithValue("@chksalsprice", dt.Rows[0]["chksalsprice"].ToString());


            command.Parameters.AddWithValue("@chckstatus", dt.Rows[0]["chckstatus"].ToString());
            command.Parameters.AddWithValue("@shipping_service", dt.Rows[0]["shipping_service"].ToString());
            command.Parameters.AddWithValue("@transfer", dt.Rows[0]["transfer"].ToString());
            command.Parameters.AddWithValue("@orders", dt.Rows[0]["orders"].ToString());
            command.Parameters.AddWithValue("@refund", dt.Rows[0]["refund"].ToString());


            command.Parameters.AddWithValue("@chckchncomgst", dt.Rows[0]["chckchncomgst"].ToString());
            command.Parameters.AddWithValue("@chckchngategst", dt.Rows[0]["chckchngategst"].ToString());
            command.Parameters.AddWithValue("@chckchnloggst", dt.Rows[0]["chckchnloggst"].ToString());
            command.Parameters.AddWithValue("@chckchnpenlgst", dt.Rows[0]["chckchnpenlgst"].ToString());
            command.Parameters.AddWithValue("@chckchnmic1gst", dt.Rows[0]["chckchnmic1gst"].ToString());
            command.Parameters.AddWithValue("@chckchnmic2gst", dt.Rows[0]["chckchnmic2gst"].ToString());
            command.Parameters.AddWithValue("@productid", dt.Rows[0]["productid"].ToString());
            command.Parameters.AddWithValue("@productname", dt.Rows[0]["productname"].ToString());
            if (!cmgfrmup.Equals("cmgfrmup"))
            {
                command.Parameters.AddWithValue("@vloc_id", vlocid);
            }
            else
            {
                command.Parameters.AddWithValue("@vloc_id", locid);
            }
                command.ExecuteNonQuery();
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
    public DataTable BindSettingbyvlocid(string vlocid)
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
        transaction = connection.BeginTransaction("BindSettingbyvlocid");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select * from Pymt_trans_setting p where p.vloc_id=@v_locid";
            command.Parameters.AddWithValue("@v_locid", vlocid);
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
    public int UpdateSettingbyVlocid(DataTable dt, string vlocid)
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
        transaction = connection.BeginTransaction("UpdateSettingbyVlocid");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            DataTable check = new DataTable();
            int result = 0;
            command.CommandText = "select l.pyt_st_id from Pymt_trans_setting l where l.vloc_id=@vloc_id ";
            command.Parameters.AddWithValue("@vloc_id", vlocid);
            check.Load(command.ExecuteReader());
            command.Parameters.Clear();
            if (check.Rows.Count.Equals(0))
            {
                string cmgfrmup= "cmgfrmup";
                int go = addVirtualLocation("","","","","", dt,cmgfrmup, vlocid);
            }
            else
            {


                command.CommandText = "Update Pymt_trans_setting set  salesid=@salesid,type=@type,sp=@sp,channel_commsion=@channel_commsion,Channel_Gateway=@Channel_Gateway" +
                    ",VL_Logistics=@VL_Logistics,VLPenalty=@VLPenalty,Pack_Charges_VL_Misc=@Pack_Charges_VL_Misc,Spcl_pack_chrgs_vlMics=@Spcl_pack_chrgs_vlMics," +
                    "mp_commission_cgst=@mp_commission_cgst,pg_commission_cgst=@pg_commission_cgst,logistics_cgst=@logistics_cgst,TCS_CGST=@TCS_CGST,mp_commission_igst=@mp_commission_igst," +
                    "pg_commission_igst=@pg_commission_igst,logistics_igst=@logistics_igst,TCS_IGST=@TCS_IGST," +
                    "mp_commission_sgst=@mp_commission_sgst,pg_commission_sgst=@pg_commission_sgst,logistics_sgst=@logistics_sgst,TCS_GST=@TCS_GST," +
                    "Total_Cgst=@Total_Cgst,Total_Igst=@Total_Igst,Total_Sgst=@Total_Sgst,totalsales_taxliable_gstbeforeadustingTCS=@totalsales_taxliable_gstbeforeadustingTCS," +
                    "Payment_Status=@Payment_Status,Merchant_SKU=@Merchant_SKU,Settlement_Date=@Settlement_Date,Payment_Type=@Payment_Type,Payable_Amoun=@Payable_Amoun,PG_UTR=@PG_UTR," +
                    "packfee_for_return=@packfee_for_return,pymtfee_for_return=@pymtfee_for_return,logistfee_for_return=@logistfee_for_return,reverse_logistfee_for_return=@reverse_logistfee_for_return," +
                    "order_date=@order_date,closing_fees=@closing_fees,other_charges=@other_charges,Misc_charge=@Misc_charge,chksalsprice=@chksalsprice," +
                    "chckstatus=@chckstatus,shipping_service=@shipping_service,transfer=@transfer,orders=@orders,refund=@refund," +
                    "chckchncomgst=@chckchncomgst,chckchngategst=@chckchngategst,chckchnloggst=@chckchnloggst,chckchnpenlgst=@chckchnpenlgst,chckchnmic1gst=@chckchnmic1gst,chckchnmic2gst=@chckchnmic2gst,productid=@productid,productname=@productname where vloc_id=@vloc_id";
                //,checkgst=@checkgst
                command.Parameters.AddWithValue("@salesid", dt.Rows[0]["txtstockupId"].ToString());
                command.Parameters.AddWithValue("@type", dt.Rows[0]["txttype"].ToString());
                command.Parameters.AddWithValue("@sp", dt.Rows[0]["txtMRP"].ToString());
                command.Parameters.AddWithValue("@channel_commsion", dt.Rows[0]["txtchannel_commsion"].ToString());
                command.Parameters.AddWithValue("@Channel_Gateway", dt.Rows[0]["txtChannel_Gateway"].ToString());
                command.Parameters.AddWithValue("@VL_Logistics", dt.Rows[0]["txtVL_Logistics"].ToString());
                command.Parameters.AddWithValue("@VLPenalty", dt.Rows[0]["txtVLPenalty"].ToString());
                command.Parameters.AddWithValue("@Pack_Charges_VL_Misc", dt.Rows[0]["txtPack_Charges_VL_Misc"].ToString());
                command.Parameters.AddWithValue("@Spcl_pack_chrgs_vlMics", dt.Rows[0]["txtSpcl_pack_chrgs_vlMics"].ToString());

                command.Parameters.AddWithValue("@mp_commission_cgst", dt.Rows[0]["txtmp_commission_cgst"].ToString());
                command.Parameters.AddWithValue("@pg_commission_cgst", dt.Rows[0]["txtpg_commission_cgst"].ToString());
                command.Parameters.AddWithValue("@logistics_cgst", dt.Rows[0]["txtlogistics_cgst"].ToString());
                command.Parameters.AddWithValue("@TCS_CGST", dt.Rows[0]["txtTCS_CGST"].ToString());

                command.Parameters.AddWithValue("@mp_commission_igst", dt.Rows[0]["txtmp_commission_igst"].ToString());
                command.Parameters.AddWithValue("@pg_commission_igst", dt.Rows[0]["txtpg_commission_igst"].ToString());
                command.Parameters.AddWithValue("@logistics_igst", dt.Rows[0]["txtlogistics_igst"].ToString());
                command.Parameters.AddWithValue("@TCS_IGST", dt.Rows[0]["txtTCS_IGST"].ToString());

                command.Parameters.AddWithValue("@mp_commission_sgst", dt.Rows[0]["txtmp_commission_sgst"].ToString());
                command.Parameters.AddWithValue("@pg_commission_sgst", dt.Rows[0]["txtpg_commission_sgst"].ToString());
                command.Parameters.AddWithValue("@logistics_sgst", dt.Rows[0]["txtlogistics_sgst"].ToString());
                command.Parameters.AddWithValue("@TCS_GST", dt.Rows[0]["txtTCS_GST"].ToString());

                command.Parameters.AddWithValue("@Total_Cgst", dt.Rows[0]["txtTotal_Cgst"].ToString());
                command.Parameters.AddWithValue("@Total_Igst", dt.Rows[0]["txtTotal_Igst"].ToString());
                command.Parameters.AddWithValue("@Total_Sgst", dt.Rows[0]["txtTotal_Sgst"].ToString());

                command.Parameters.AddWithValue("@totalsales_taxliable_gstbeforeadustingTCS", dt.Rows[0]["txttotalsales_taxliable_gstbeforeadustingTCS"].ToString());

                command.Parameters.AddWithValue("@Payment_Status", dt.Rows[0]["txtPayment_Status"].ToString());
                command.Parameters.AddWithValue("@Merchant_SKU", dt.Rows[0]["txtMerchant_SKU"].ToString());
                command.Parameters.AddWithValue("@Settlement_Date", dt.Rows[0]["txtSettlement_Date"].ToString());
                command.Parameters.AddWithValue("@Payment_Type", dt.Rows[0]["txtPayment_Type"].ToString());
                command.Parameters.AddWithValue("@Payable_Amoun", dt.Rows[0]["txtPayable_Amoun"].ToString());
                command.Parameters.AddWithValue("@PG_UTR", dt.Rows[0]["txtPG_UTR"].ToString());
                command.Parameters.AddWithValue("@packfee_for_return", dt.Rows[0]["txtpackfee_for_return"].ToString());
                command.Parameters.AddWithValue("@spcl_pack_fee_for_return", dt.Rows[0]["txtspcl_pack_fee_for_return"].ToString());

                command.Parameters.AddWithValue("@pymtfee_for_return", dt.Rows[0]["txtpymtfee_for_return"].ToString());
                command.Parameters.AddWithValue("@logistfee_for_return", dt.Rows[0]["txtlogistfee_for_return"].ToString());
                command.Parameters.AddWithValue("@reverse_logistfee_for_return", dt.Rows[0]["txtlogistfee_for_return"].ToString());
                command.Parameters.AddWithValue("@order_date", dt.Rows[0]["txtorder_date"].ToString());

                command.Parameters.AddWithValue("@closing_fees", dt.Rows[0]["txtclosing_fees"].ToString());
                command.Parameters.AddWithValue("@other_charges", dt.Rows[0]["txtother_charges"].ToString());
                command.Parameters.AddWithValue("@Misc_charge", dt.Rows[0]["txtMisc_charge"].ToString());
                //command.Parameters.AddWithValue("@checkgst", dt.Rows[0]["checkgst"].ToString());
                command.Parameters.AddWithValue("@chksalsprice", dt.Rows[0]["chksalsprice"].ToString());


                command.Parameters.AddWithValue("@chckstatus", dt.Rows[0]["chckstatus"].ToString());
                command.Parameters.AddWithValue("@shipping_service", dt.Rows[0]["shipping_service"].ToString());
                command.Parameters.AddWithValue("@transfer", dt.Rows[0]["transfer"].ToString());
                command.Parameters.AddWithValue("@orders", dt.Rows[0]["orders"].ToString());
                command.Parameters.AddWithValue("@refund", dt.Rows[0]["refund"].ToString());

                command.Parameters.AddWithValue("@chckchncomgst", dt.Rows[0]["chckchncomgst"].ToString());
                command.Parameters.AddWithValue("@chckchngategst", dt.Rows[0]["chckchngategst"].ToString());
                command.Parameters.AddWithValue("@chckchnloggst", dt.Rows[0]["chckchnloggst"].ToString());
                command.Parameters.AddWithValue("@chckchnpenlgst", dt.Rows[0]["chckchnpenlgst"].ToString());
                command.Parameters.AddWithValue("@chckchnmic1gst", dt.Rows[0]["chckchnmic1gst"].ToString());
                command.Parameters.AddWithValue("@chckchnmic2gst", dt.Rows[0]["chckchnmic2gst"].ToString());
                command.Parameters.AddWithValue("@productid", dt.Rows[0]["productid"].ToString());
                command.Parameters.AddWithValue("@productname", dt.Rows[0]["productname"].ToString());

                command.Parameters.AddWithValue("@vloc_id", vlocid);
                



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
    public DataTable selectColums()
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
        transaction = connection.BeginTransaction("selectColums");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select * from payment_trans";
           
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
    public DataTable getcolumsett(string vlocid,string column)
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
        transaction = connection.BeginTransaction("BindSettingbyvlocid");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select "+column+" from Pymt_trans_setting   where vloc_id=@vloc_id";
            command.Parameters.AddWithValue("@vloc_id", vlocid);
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