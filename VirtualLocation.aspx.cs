using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VirtualLocation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindVirtualLocation();
            //dvlocation.Visible = true;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "firedtSearch();", true);
        }
    }

    #region Virtual Location

    public void BindVirtualLocation()
    {
        DataBase.Masters ObjBind = new DataBase.Masters();

        string VirtualLocation = "2";

        DataSet ds = ObjBind.BindLocationByLocation2typeID(VirtualLocation);

        if (ds.Tables[0].Rows.Count > 0)
        {
            GV.DataSource = ds;
            GV.DataBind();
        }
        ds.Dispose();
        ObjBind = null;
    }

    public void BindLocationTypeCombo()
    {
        DataBase.Masters ObjBind = new DataBase.Masters();

        DataSet ds = ObjBind.BindLocationType();

        ddlLocationType.DataSource = ds;
        ddlLocationType.DataTextField = "Name";
        ddlLocationType.DataValueField = "LocationTypeID";
        ddlLocationType.DataBind();
        ddlLocationType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Location Type", "0"));
    }

    //public void BindLocationType2Combo()
    //{
    //    DataBase.Masters ObjBind = new DataBase.Masters();

    //    DataSet ds = ObjBind.BindLocationType2();

    //    ddlLocationType2.DataSource = ds;
    //    ddlLocationType2.DataTextField = "Name";
    //    ddlLocationType2.DataValueField = "LTypeID2";
    //    ddlLocationType2.DataBind();
    //    ddlLocationType2.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Location Type2", "0"));
    //}

    public void Clear()
    {
        txtName.Text = string.Empty;
        txtContact.Text = string.Empty;
        txtAddress.Text = string.Empty;
        ddlLocationType.SelectedValue = "0";
        h3vloc.Visible = false;


        btnupdatepymtset.Visible = false;
        
        lblstockupId.Text = String.Empty;
        txtstockupId.Text = String.Empty;
        lbltype.Text = String.Empty;
        txttype.Text = String.Empty;
        lblMRP.Text = String.Empty;
        txtMRP.Text = String.Empty;
        lblchannel_commsion.Text = String.Empty;
        txtchannel_commsion.Text = String.Empty;
        lblChannel_Gateway.Text = String.Empty;
        txtChannel_Gateway.Text = String.Empty;
        lblVL_Logistics.Text = String.Empty; 
        txtVL_Logistics.Text = String.Empty;
        lblVLPenalty.Text = String.Empty;
        txtVLPenalty.Text = String.Empty;
        lblPack_Charges_VL_Misc.Text = String.Empty;
        txtPack_Charges_VL_Misc.Text = String.Empty;
        lblSpcl_pack_chrgs_vlMics.Text= String.Empty;
        txtSpcl_pack_chrgs_vlMics.Text= String.Empty;
        lblmp_commission_cgst.Text = String.Empty;
        txtmp_commission_cgst.Text = String.Empty;
        lblpg_commission_cgst.Text = String.Empty;
        txtpg_commission_cgst.Text = String.Empty;
        lbllogistics_cgst.Text = String.Empty;
        txtlogistics_cgst.Text = String.Empty;
        lblTCS_CGST.Text = String.Empty;
        txtTCS_CGST.Text = String.Empty;
        lblmp_commission_igst.Text = String.Empty;
        txtmp_commission_igst.Text = String.Empty;
        lblpg_commission_igst.Text = String.Empty;
        txtpg_commission_igst.Text = String.Empty;
        lbllogistics_igst.Text = String.Empty;
        txtlogistics_igst.Text = String.Empty;
        lblTCS_IGST.Text = String.Empty;
        txtTCS_IGST.Text = String.Empty;
        lblmp_commission_sgst.Text = String.Empty;
        txtmp_commission_sgst.Text = String.Empty;
        lblpg_commission_sgst.Text = String.Empty;
        txtpg_commission_sgst.Text = String.Empty;
        lbllogistics_sgst.Text = String.Empty;
        txtlogistics_sgst.Text = String.Empty;
        lblTCS_GST.Text = String.Empty;
        txtTCS_GST.Text = String.Empty;
        lblTotal_Cgst.Text = String.Empty;
        txtTotal_Cgst.Text = String.Empty;
        lblTotal_Igst.Text = String.Empty;
        txtTotal_Igst.Text = String.Empty;
        lblTotal_Sgst.Text = String.Empty;
        txtTotal_Sgst.Text = String.Empty;
        lbltotalsales_taxliable_gstbeforeadustingTCS.Text = String.Empty;
        txttotalsales_taxliable_gstbeforeadustingTCS.Text = String.Empty;
        lblPayment_Status.Text = String.Empty;
        txtPayment_Status.Text = String.Empty;
        lblMerchant_SKU.Text = String.Empty;
        txtMerchant_SKU.Text = String.Empty;
        lblSettlement_Date.Text = String.Empty;
        txtSettlement_Date.Text = String.Empty;
        lblPayment_Type.Text = String.Empty;
        txtPayment_Type.Text = String.Empty;
        lblPayable_Amoun.Text = String.Empty;
        txtPayable_Amoun.Text = String.Empty;
        lblPG_UTR.Text = String.Empty;
        txtPG_UTR.Text = String.Empty;
        lblpackfee_for_return.Text = String.Empty;
        txtpackfee_for_return.Text = String.Empty;
        lblpymtfee_for_return.Text = String.Empty;
        txtpymtfee_for_return.Text = String.Empty;
        lbllogistfee_for_return.Text =String.Empty;
        txtlogistfee_for_return.Text= String.Empty;
        lblreverse_logistfee_for_return.Text = String.Empty;
        txtreverse_logistfee_for_return.Text = String.Empty;
        lblorder_date.Text = String.Empty;
        txtorder_date.Text = String.Empty;
        txtspcl_packfee_for_return.Text = String.Empty;
        txtproductname.Text = String.Empty;
        txtproductid.Text = String.Empty;
    }
    public  DataTable BindDt()
    {
        DataTable dt = new DataTable();
        try
        {
           
           
            dt.Columns.Add("txtstockupId", typeof(string));
            dt.Columns.Add("txttype", typeof(string));
            dt.Columns.Add("txtMRP", typeof(string));
            dt.Columns.Add("txtchannel_commsion", typeof(string));
            dt.Columns.Add("txtChannel_Gateway", typeof(string));
            dt.Columns.Add("txtVL_Logistics", typeof(string));
            dt.Columns.Add("txtVLPenalty", typeof(string));
            dt.Columns.Add("txtPack_Charges_VL_Misc", typeof(string));
           
            dt.Columns.Add("txtSpcl_pack_chrgs_vlMics", typeof(string));
            dt.Columns.Add("txtmp_commission_cgst", typeof(string));
            dt.Columns.Add("txtpg_commission_cgst", typeof(string));
            dt.Columns.Add("txtlogistics_cgst", typeof(string));
            dt.Columns.Add("txtTCS_CGST", typeof(string));
            dt.Columns.Add("txtmp_commission_igst", typeof(string));
            dt.Columns.Add("txtpg_commission_igst", typeof(string));
            dt.Columns.Add("txtlogistics_igst", typeof(string));
            dt.Columns.Add("txtTCS_IGST", typeof(string));
            dt.Columns.Add("txtmp_commission_sgst", typeof(string));
            dt.Columns.Add("txtpg_commission_sgst", typeof(string));
            dt.Columns.Add("txtlogistics_sgst", typeof(string));
            dt.Columns.Add("txtTCS_GST", typeof(string));
            dt.Columns.Add("txtTotal_Cgst", typeof(string));
            dt.Columns.Add("txtTotal_Igst", typeof(string));
            dt.Columns.Add("txtTotal_Sgst", typeof(string));
            dt.Columns.Add("txttotalsales_taxliable_gstbeforeadustingTCS", typeof(string));
            dt.Columns.Add("txtPayment_Status", typeof(string));
            dt.Columns.Add("txtMerchant_SKU", typeof(string));
            dt.Columns.Add("txtSettlement_Date", typeof(string));
            dt.Columns.Add("txtPayment_Type", typeof(string));
            dt.Columns.Add("txtPayable_Amoun", typeof(string));
            dt.Columns.Add("txtPG_UTR", typeof(string));
            dt.Columns.Add("txtpackfee_for_return", typeof(string));
            dt.Columns.Add("txtspcl_pack_fee_for_return", typeof(string));
            dt.Columns.Add("txtpymtfee_for_return", typeof(string));
            dt.Columns.Add("txtlogistfee_for_return", typeof(string));
            dt.Columns.Add("txtreverse_logistfee_for_return", typeof(string));
            dt.Columns.Add("txtorder_date", typeof(string));

            dt.Columns.Add("txtclosing_fees", typeof(string));
            dt.Columns.Add("txtother_charges", typeof(string));
            dt.Columns.Add("txtMisc_charge", typeof(string));
            //dt.Columns.Add("checkgst", typeof(string));
            dt.Columns.Add("chksalsprice", typeof(string));
            dt.Columns.Add("chckstatus", typeof(string));
            dt.Columns.Add("shipping_service", typeof(string));
            dt.Columns.Add("transfer", typeof(string));
            dt.Columns.Add("orders", typeof(string));
            dt.Columns.Add("refund", typeof(string));

            dt.Columns.Add("chckchncomgst", typeof(string));
            dt.Columns.Add("chckchngategst", typeof(string));
            dt.Columns.Add("chckchnloggst", typeof(string));
            dt.Columns.Add("chckchnpenlgst", typeof(string));
            dt.Columns.Add("chckchnmic1gst", typeof(string));
            dt.Columns.Add("chckchnmic2gst", typeof(string));
            dt.Columns.Add("productid", typeof(string));
            dt.Columns.Add("productname", typeof(string));



            dt.Rows.Add( txtstockupId.Text, txttype.Text, txtMRP.Text, txtchannel_commsion.Text
                 , txtChannel_Gateway.Text, txtVL_Logistics.Text,  txtVLPenalty.Text,
                 txtPack_Charges_VL_Misc.Text, txtSpcl_pack_chrgs_vlMics.Text,
                 txtmp_commission_cgst.Text, txtpg_commission_cgst.Text, txtlogistics_cgst.Text,
                 txtTCS_CGST.Text, txtmp_commission_igst.Text, txtpg_commission_igst.Text, 
                 txtlogistics_igst.Text, txtTCS_IGST.Text, txtmp_commission_sgst.Text,
                 txtpg_commission_sgst.Text, txtlogistics_sgst.Text, txtTCS_GST.Text, txtTotal_Cgst.Text,
                 txtTotal_Igst.Text, txtTotal_Sgst.Text, txttotalsales_taxliable_gstbeforeadustingTCS.Text,
                 txtPayment_Status.Text, txtMerchant_SKU.Text, txtSettlement_Date.Text,
                 txtPayment_Type.Text, txtPayable_Amoun.Text, txtPG_UTR.Text,
                 txtpackfee_for_return.Text, txtspcl_packfee_for_return.Text, txtpymtfee_for_return.Text, txtlogistfee_for_return.Text,
                 txtreverse_logistfee_for_return.Text, txtorder_date.Text, txtclosing_fees.Text, txtother_charges.Text, txtMisc_charge.Text,
                 chksalsprice.Checked, chckstatus.Checked,drpshipservi.SelectedValue,drptransfer.SelectedValue, drporder.SelectedValue, drprefund.SelectedValue,
                 chkchnelcomm.Checked, chchngate.Checked, chklogis.Checked,chkpenlty.Checked,chkmic1.Checked,chkmic2.Checked,txtproductid.Text,txtproductname.Text);
            //,checkgst.Checked
        }

        catch (Exception ex)
        {
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
            return null;
        }
        return dt;
    }

    protected void rptr_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        DataBase.Masters objEdit = new DataBase.Masters();

        if (e.CommandName.ToLower().Equals("edit"))
        {
            BindLocationTypeCombo();
            //BindLocationType2Combo();

            int ID = int.Parse(e.CommandArgument.ToString());
            hdnID.Value = e.CommandArgument.ToString();

            ctfrmDet.Visible = true;

            DataSet ds = objEdit.GetLocationByID(ID);

            txtName.Text = ds.Tables[0].Rows[0]["Location"].ToString();
            ddlLocationType.SelectedValue = ds.Tables[0].Rows[0]["LocationTypeID"].ToString();
            //ddlLocationType2.SelectedValue = ds.Tables[0].Rows[0]["LTypeID2"].ToString();
            txtContact.Text = ds.Tables[0].Rows[0]["Contact"].ToString();
            txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
            devsavehideshow.Visible = true;
            btnSave.Visible = false;
            btnUpdate.Visible = true;

        }

        if (e.CommandName.ToLower().Equals("addsl"))
        {
            int ID = int.Parse(e.CommandArgument.ToString());

            Session["LocationID"] = ID.ToString();
            Response.Redirect("VirtualLocationSetting.aspx");
        }

        objEdit = null;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        BindLocationTypeCombo();
        //BindLocationType2Combo();
        divpymtsetting.Visible = true;
        devsavehideshow.Visible = true;
        Clear();
        ctfrmDet.Visible = true;
        btnSave.Visible = true;
        btnUpdate.Visible = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //DataBase.Masters objAdd = new DataBase.Masters();

        string VirtualLocation = "2";

        //int ID = objAdd.AddLocation(txtName.Text.Trim(), ddlLocationType.SelectedValue, VirtualLocation, txtContact.Text.Trim(), txtAddress.Text.Trim());
        //if (ID > 0)
        //{
        //    Clear();
        //    ctfrmDet.Visible = false;
        //    BindVirtualLocation();
        //    objAdd = null;
        //}
        //DataTable dt = new DataTable();
        //dt.Columns.Add("dbname", typeof(string));
        //dt.Columns.Add("pymt_cname", typeof(string));

        //dt.Rows.Add(lblstockupId.Text, txtstockupId.Text);
        //dt.Rows.Add(lbltype.Text, txttype.Text);
        //dt.Rows.Add(lblMRP.Text, txtMRP.Text);
        //dt.Rows.Add(lblchannel_commsion.Text, txtchannel_commsion.Text);
        //dt.Rows.Add(lblChannel_Gateway.Text, txtChannel_Gateway.Text);
        //dt.Rows.Add(lblVL_Logistics.Text, txtVL_Logistics.Text);
        //dt.Rows.Add(lblVLPenalty.Text, txtVLPenalty.Text);
        //dt.Rows.Add(lblPack_Charges_VL_Misc.Text, txtPack_Charges_VL_Misc.Text);
        //dt.Rows.Add(lblSpcl_pack_chrgs_vlMics.Text, txtSpcl_pack_chrgs_vlMics.Text);
        //dt.Rows.Add(lblmp_commission_cgst.Text, txtmp_commission_cgst.Text);
        //dt.Rows.Add(lblpg_commission_cgst.Text, txtpg_commission_cgst.Text);
        //dt.Rows.Add(lbllogistics_cgst.Text, txtlogistics_cgst.Text);
        //dt.Rows.Add(lblTCS_CGST.Text, txtTCS_CGST.Text);
        //dt.Rows.Add(lblmp_commission_igst.Text, txtmp_commission_igst.Text);
        //dt.Rows.Add(lblpg_commission_igst.Text, txtpg_commission_igst.Text);
        //dt.Rows.Add(lbllogistics_igst.Text, txtlogistics_igst.Text);
        //dt.Rows.Add(lblTCS_IGST.Text, txtTCS_IGST.Text);
        //dt.Rows.Add(lblmp_commission_sgst.Text, txtmp_commission_sgst.Text);
        //dt.Rows.Add(lblpg_commission_sgst.Text, txtpg_commission_sgst.Text);
        //dt.Rows.Add(lbllogistics_sgst.Text, txtlogistics_sgst.Text);
        //dt.Rows.Add(lblTCS_GST.Text, txtTCS_GST.Text);
        //dt.Rows.Add(lblTotal_Cgst.Text, txtTotal_Cgst.Text);
        //dt.Rows.Add(lblTotal_Igst.Text, txtTotal_Igst.Text);
        //dt.Rows.Add(lblTotal_Sgst.Text, txtTotal_Sgst.Text);
        //dt.Rows.Add(lbltotalsales_taxliable_gstbeforeadustingTCS.Text, txttotalsales_taxliable_gstbeforeadustingTCS.Text);
        //dt.Rows.Add(lblPayment_Status.Text, txtPayment_Status.Text);
        //dt.Rows.Add(lblMerchant_SKU.Text, txtMerchant_SKU.Text);
        //dt.Rows.Add(lblSettlement_Date.Text, txtSettlement_Date.Text);
        //dt.Rows.Add(lblPayment_Type.Text, txtPayment_Type.Text);
        //dt.Rows.Add(lblPayable_Amoun.Text, txtPayable_Amoun.Text);
        //dt.Rows.Add(lblPG_UTR.Text, txtPG_UTR.Text);
        //dt.Rows.Add(lblpackfee_for_return.Text, txtpackfee_for_return.Text);
        //dt.Rows.Add(lblpymtfee_for_return.Text, txtpymtfee_for_return.Text);
        //dt.Rows.Add(lbllogistfee_for_return.Text, txtlogistfee_for_return.Text);
        //dt.Rows.Add(lblreverse_logistfee_for_return.Text, txtreverse_logistfee_for_return.Text);
        //dt.Rows.Add(lblorder_date.Text, txtorder_date.Text);
        DataTable dt = new DataTable();
        dt = BindDt();
        VirtualLoactionCls obj = new VirtualLoactionCls();
        int Result = obj.addVirtualLocation( ddlLocationType.SelectedValue, VirtualLocation, txtName.Text.Trim(), txtContact.Text.Trim(), txtAddress.Text.Trim(),dt,"","");
        if(Result==0)
        {
            Clear();
            ctfrmDet.Visible = false;
            divpymtsetting.Visible = false;
            devsavehideshow.Visible = false;
            BindVirtualLocation();
            
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Failed!');", true);
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        DataBase.Masters objUpdate = new DataBase.Masters();

        string VirtualLocation = "2";

        int Success = objUpdate.UpdateLocation(txtName.Text.Trim(), ddlLocationType.SelectedValue, VirtualLocation, hdnID.Value, txtContact.Text.Trim(), txtAddress.Text.Trim());

        if (Success > 0)
        {
            btnUpdate.Visible = false;
            btnSave.Visible = true;

            Clear();

            BindVirtualLocation();
            ctfrmDet.Visible = false;
            //Response.Redirect("AddBuyer.aspx");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtName.Text = String.Empty;

        Response.Redirect("Location.aspx");

        btnSave.Text = "Save";
        ctfrmDet.Visible = false;
    }

    #endregion

    protected void btnpaytmsetting_Click(object sender, EventArgs e)
    {
        try
        {
            h3vloc.Visible = true;
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            Label lbllocationid = (Label)rp1.FindControl("lbllocationid");
            lblhdID.Text = lbllocationid.Text;
            Label lblnlocname = (Label)rp1.FindControl("lblnlocname");
            lblvlocname.Text = lblnlocname.Text;
            VirtualLoactionCls obj = new VirtualLoactionCls();
            DataTable dt = obj.BindSettingbyvlocid(lbllocationid.Text);
            if (!dt.Rows.Count.Equals(0))
            {
                txtstockupId.Text = dt.Rows[0]["salesid"].ToString();

                txttype.Text = dt.Rows[0]["type"].ToString();

                txtMRP.Text = dt.Rows[0]["sp"].ToString();

                txtchannel_commsion.Text = dt.Rows[0]["channel_commsion"].ToString();

                txtChannel_Gateway.Text = dt.Rows[0]["Channel_Gateway"].ToString();

                txtVL_Logistics.Text = dt.Rows[0]["VL_Logistics"].ToString();

                txtVLPenalty.Text = dt.Rows[0]["VLPenalty"].ToString();

                txtPack_Charges_VL_Misc.Text = dt.Rows[0]["Pack_Charges_VL_Misc"].ToString();

                txtSpcl_pack_chrgs_vlMics.Text = dt.Rows[0]["Spcl_pack_chrgs_vlMics"].ToString();

                txtmp_commission_cgst.Text = dt.Rows[0]["mp_commission_cgst"].ToString();

                txtpg_commission_cgst.Text = dt.Rows[0]["pg_commission_cgst"].ToString();

                txtlogistics_cgst.Text = dt.Rows[0]["logistics_cgst"].ToString();

                txtTCS_CGST.Text = dt.Rows[0]["TCS_CGST"].ToString();

                txtmp_commission_igst.Text = dt.Rows[0]["mp_commission_igst"].ToString();

                txtpg_commission_igst.Text = dt.Rows[0]["pg_commission_igst"].ToString();

                txtlogistics_igst.Text = dt.Rows[0]["logistics_igst"].ToString();

                txtTCS_IGST.Text = dt.Rows[0]["TCS_IGST"].ToString();

                txtmp_commission_sgst.Text = dt.Rows[0]["mp_commission_sgst"].ToString();

                txtpg_commission_sgst.Text = dt.Rows[0]["pg_commission_sgst"].ToString();

                txtlogistics_sgst.Text = dt.Rows[0]["logistics_sgst"].ToString();

                txtTCS_GST.Text = dt.Rows[0]["TCS_GST"].ToString();

                txtTotal_Cgst.Text = dt.Rows[0]["Total_Cgst"].ToString();

                txtTotal_Igst.Text = dt.Rows[0]["Total_Igst"].ToString();

                txtTotal_Sgst.Text = dt.Rows[0]["Total_Sgst"].ToString();

                txttotalsales_taxliable_gstbeforeadustingTCS.Text = dt.Rows[0]["totalsales_taxliable_gstbeforeadustingTCS"].ToString();

                txtPayment_Status.Text = dt.Rows[0]["Payment_Status"].ToString();

                txtMerchant_SKU.Text = dt.Rows[0]["Merchant_SKU"].ToString();

                txtSettlement_Date.Text = dt.Rows[0]["Settlement_Date"].ToString();

                txtPayment_Type.Text = dt.Rows[0]["Payment_Type"].ToString();

                txtPayable_Amoun.Text = dt.Rows[0]["Payable_Amoun"].ToString();

                txtPG_UTR.Text = dt.Rows[0]["PG_UTR"].ToString();

                txtpackfee_for_return.Text = dt.Rows[0]["packfee_for_return"].ToString();

                txtpymtfee_for_return.Text = dt.Rows[0]["pymtfee_for_return"].ToString();

                txtlogistfee_for_return.Text = dt.Rows[0]["logistfee_for_return"].ToString();

                txtreverse_logistfee_for_return.Text = dt.Rows[0]["reverse_logistfee_for_return"].ToString();

                txtorder_date.Text = dt.Rows[0]["order_date"].ToString();
                txtclosing_fees.Text = dt.Rows[0]["closing_fees"].ToString();
                txtother_charges.Text = dt.Rows[0]["other_charges"].ToString();
                txtMisc_charge.Text = dt.Rows[0]["Misc_charge"].ToString();
                txtproductid.Text = dt.Rows[0]["productid"].ToString();
                txtproductname.Text = dt.Rows[0]["productname"].ToString();
                //lblvlocname.Text= dt.Rows[0]["Location"].ToString();
                
                //string check= dt.Rows[0]["checkgst"].ToString(); 
                //if (check.ToString().Equals("True"))
                //{
                //    checkgst.Checked = true;
                //}
                //else
                //{
                //    checkgst.Checked = false;
                //}
                string checkprice = dt.Rows[0]["chksalsprice"].ToString();
                if (checkprice.ToString().Equals("True"))
                {
                    chksalsprice.Checked = true;
                }
                else
                {
                    chksalsprice.Checked = false;
                }
                string checkstatus = dt.Rows[0]["chckstatus"].ToString();
                if (checkstatus.ToString().Equals("True"))
                {
                    chckstatus.Checked = true;
                }
                else
                {
                    chckstatus.Checked = false;
                }
                string chnncomm = dt.Rows[0]["chckchncomgst"].ToString();
                if (chnncomm.ToString().Equals("True"))
                {
                    chkchnelcomm.Checked = true;
                }
                else
                {
                    chkchnelcomm.Checked = false;
                }
                string chnngate = dt.Rows[0]["chckchngategst"].ToString();
                if (chnngate.ToString().Equals("True"))
                {
                    chchngate.Checked = true;
                }
                else
                {
                    chchngate.Checked = false;
                }
                string chnnlog = dt.Rows[0]["chckchnloggst"].ToString();
                if (chnnlog.ToString().Equals("True"))
                {
                    chklogis.Checked = true;
                }
                else
                {
                    chklogis.Checked = false;
                }
                string chnnpen = dt.Rows[0]["chckchnpenlgst"].ToString();
                if (chnnpen.ToString().Equals("True"))
                {
                    chkpenlty.Checked = true;
                }
                else
                {
                    chkpenlty.Checked = false;
                }
                string chnnmis1 = dt.Rows[0]["chckchnmic1gst"].ToString();
                if (chnnmis1.ToString().Equals("True"))
                {
                    chkmic1.Checked = true;
                }
                else
                {
                    chkmic1.Checked = false;
                }
                string chnnmis2 = dt.Rows[0]["chckchnmic2gst"].ToString();
                if (chnnmis2.ToString().Equals("True"))
                {
                    chkmic2.Checked = true;
                }
                else
                {
                    chkmic2.Checked = false;
                }
               
                
                if (chckstatus.Checked == true)
                {
                    status.Visible = true;
                    status2.Visible = true;

                    drpshipservi.SelectedValue = dt.Rows[0]["shipping_service"].ToString();
                    drptransfer.SelectedValue = dt.Rows[0]["transfer"].ToString();
                    drporder.SelectedValue = dt.Rows[0]["orders"].ToString();
                    drprefund.SelectedValue = dt.Rows[0]["refund"].ToString();
                }
                else
                {
                    status.Visible = false;
                    status2.Visible = false;
                }
            }
            else
            {

            }
            divpymtsetting.Visible = true;
            divupdatepytmsett.Visible = true;
            btnupdatepymtset.Visible = true;


        }
        catch(Exception ex)
        {
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }
    }

  
    protected void btnback_Click(object sender, EventArgs e)
    {
        try
        { 
            divpymtsetting.Visible = false;
            divupdatepytmsett.Visible = false;
            h3vloc.Visible = false;

        }
        catch(Exception ex)
        {
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }
    }

    protected void btnupdatepymtset_Click(object sender, EventArgs e)
    {
        try
        {

            DataTable dt = new DataTable();
            dt = BindDt();

            VirtualLoactionCls obj = new VirtualLoactionCls();
            int result = obj.UpdateSettingbyVlocid(dt, lblhdID.Text);
            if(result==0)
            {
                Clear();
                ctfrmDet.Visible = false;
                divpymtsetting.Visible = false;
                devsavehideshow.Visible = false;
                BindVirtualLocation();
                lblhdID.Text = String.Empty;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Failed!');", true);
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }
    }

    protected void chckstatus_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if(chckstatus.Checked==true)
            {
                status.Visible = true;
                status2.Visible = true;
            }
            else 
            {
                status.Visible = false;
                status2.Visible = false;
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }

    }
}