using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net;
using System.Text;

public partial class addCoupon : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                if (Session["addCoupon"] != null)
                {
                    couponId.Text = Session["addCoupon"].ToString();
                    styleCls sObj = new styleCls();
                    DataTable dt1 = sObj.getTable("ItemCategory");
                    couponCategory.DataSource = dt1;
                    couponCategory.DataBind();
                    couponCategory.Items.Insert(0, new ListItem("---- Select ----", "0"));
                    bindData(couponId.Text);
                    if (Session["addCoupon"].ToString().Equals("0"))
                    {
                        bindDropDown();
                    }
                    else
                    {
                        storedProcedureCls obj = new storedProcedureCls();
                        DataTable dt = obj.getDropdowns(couponId.Text, "fetchCouponDrop");
                        rpt_dropdown.DataSource = dt;
                        rpt_dropdown.DataBind();                        
                    }

                    
                }
                else
                {
                    Session["CouponSuccFail"] = "Session Error";
                    Response.Redirect("addCoupon.aspx", true);
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey1", "firedatetime();", true);
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    private void bindData(string couponId)
    {
        try
        {
            storedProcedureCls obj = new storedProcedureCls();
            DataTable dt = obj.getTableWithCondition("couponMaster", "couponId", couponId, "couponId", "desc");
            if (!dt.Rows.Count.Equals(0))
            {                                
                couponName.Text = dt.Rows[0]["couponName"].ToString();
                couponCategory.SelectedValue = dt.Rows[0]["couponCategory"].ToString();
                validFrom.Text = Convert.ToDateTime(dt.Rows[0]["validFrom"]).ToString("MM/dd/yyyy H:mm:ss");
                validTo.Text = Convert.ToDateTime(dt.Rows[0]["validTo"]).ToString("MM/dd/yyyy H:mm:ss");
                discountOn.SelectedValue = dt.Rows[0]["discountOn"].ToString();
                couponType.SelectedValue = dt.Rows[0]["couponType"].ToString();
                couponDiscount.Text = dt.Rows[0]["couponDiscount"].ToString();
                applicableOnAmount.SelectedValue = dt.Rows[0]["applicableOnAmount"].ToString();
                applicableAmount.Text = dt.Rows[0]["applicableAmount"].ToString();
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    private void bindDropDown()
    {
        try
        {
            DataTable dtProgLang = new DataTable();
            dtProgLang.Columns.Add("CTSettingID");
            dtProgLang.Columns.Add("valueId");
            dtProgLang.Columns.Add("TableName");
            foreach (RepeaterItem itemEquipment in rpt_dropdown.Items)
            {

                DropDownList drp_dropdown = (DropDownList)itemEquipment.FindControl("drp_dropdown");
                DropDownList drp_dropValues = (DropDownList)itemEquipment.FindControl("drp_dropValues");
                Label TableName = (Label)itemEquipment.FindControl("TableName");
                dtProgLang.Rows.Add(drp_dropdown.SelectedValue, drp_dropValues.SelectedValue, TableName.Text); // Add Data 

            }

            dtProgLang.Rows.Add("-1", "-1", "");

            rpt_dropdown.DataSource = dtProgLang;
            rpt_dropdown.DataBind();



        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void rpt_dropdown_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            string CTSettingID = ((DataRowView)e.Item.DataItem)["CTSettingID"].ToString();
            string valueId = ((DataRowView)e.Item.DataItem)["valueId"].ToString();
            string TableName = ((DataRowView)e.Item.DataItem)["TableName"].ToString();
            DropDownList drp_dropdown = e.Item.FindControl("drp_dropdown") as DropDownList;
            DropDownList drp_dropValues = e.Item.FindControl("drp_dropValues") as DropDownList;
            Label LblTableName = e.Item.FindControl("TableName") as Label;

            LblTableName.Text = TableName;
            storedProcedureCls obj = new storedProcedureCls();
            DataTable dt = obj.getTableWithCondition("ColTableSetting", "IsAssigned", "True", "SettingName", "asc");
            drp_dropdown.DataSource = dt;
            drp_dropdown.DataBind();
            drp_dropdown.Items.Insert(0, new ListItem("---- Select ----", "-1"));
            drp_dropdown.SelectedValue = CTSettingID;

            if (!TableName.Equals(""))
            {
                DataTable dt1 = obj.getSubDropdown(TableName, TableName.Substring(6));
                string colId = "Col" + TableName.Substring(6) + "ID";
                string colName = "C" + TableName.Substring(6) + "Name";
                dt1.Columns[colId].ColumnName = "valueId";
                dt1.Columns[colName].ColumnName = "valueName";
                drp_dropValues.DataSource = dt1;
                drp_dropValues.DataBind();
                drp_dropValues.Items.Insert(0, new ListItem("---- Select " + drp_dropdown.SelectedItem.Text + " ----", "-1"));
                drp_dropValues.SelectedValue = valueId;
            }


        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void dropdownAdd_Click(object sender, EventArgs e)
    {
        try
        {
            bindDropDown();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void drp_dropdown_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList RepeaterItem = (DropDownList)(sender);
            RepeaterItem rp1 = ((RepeaterItem)(RepeaterItem.NamingContainer));
            DropDownList drp_dropdown = (DropDownList)rp1.FindControl("drp_dropdown");
            DropDownList drp_dropValues = (DropDownList)rp1.FindControl("drp_dropValues");
            Label TableName = (Label)rp1.FindControl("TableName");
            if (!drp_dropdown.SelectedValue.Equals("-1"))
            {
                storedProcedureCls obj = new storedProcedureCls();
                DataTable setting = obj.getTableWithCondition("ColTableSetting", "CTSettingID", drp_dropdown.SelectedValue, "SettingName", "asc");
                TableName.Text = (setting.Rows[0]["TableName"].ToString()).Replace(" ", "");

                DataTable dt1 = obj.getSubDropdown(TableName.Text, TableName.Text.Substring(6));
                string colId = "Col" + TableName.Text.Substring(6) + "ID";
                string colName = "C" + TableName.Text.Substring(6) + "Name";
                dt1.Columns[colId].ColumnName = "valueId";
                dt1.Columns[colName].ColumnName = "valueName";
                drp_dropValues.DataSource = dt1;
                drp_dropValues.DataBind();
                drp_dropValues.Items.Insert(0, new ListItem("---- Select " + drp_dropdown.SelectedItem.Text + " ----", "-1"));

            }
            else
            {
                drp_dropValues.DataSource = new DataTable();
                drp_dropValues.DataBind();
            }

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
        //set table name
        //get sub drown values

    }

    protected void cancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("couponListing.aspx", true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void save_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable drops = new DataTable();
            drops.Columns.Add("dropId"); drops.Columns.Add("subDropId");
            foreach (RepeaterItem itemEquipment in rpt_dropdown.Items)
            {
                DropDownList drp_dropdown = (DropDownList)itemEquipment.FindControl("drp_dropdown");
                DropDownList drp_dropValues = (DropDownList)itemEquipment.FindControl("drp_dropValues");
                if (!drp_dropdown.SelectedValue.Equals("-1") && !drp_dropValues.Items.Count.Equals(0))
                {
                    drops.Rows.Add(drp_dropdown.SelectedValue, drp_dropValues.SelectedValue);
                }
            }

            couponCls obj = new couponCls();
            int success = obj.addEditCoupon(couponId.Text, couponName.Text, couponCategory.SelectedValue, validFrom.Text, validTo.Text, couponType.SelectedValue, couponDiscount.Text, discountOn.SelectedValue, drops,applicableOnAmount.SelectedValue,applicableAmount.Text);
            if(success==0)
            {
                Session["CouponSuccFail"] = "Add/Update Done Successfully";
            }
            else if(success == 0)
            {
                Session["CouponSuccFail"] = "Duplicate Coupon Name";
            }
            else
            {
                Session["CouponSuccFail"] = "Add/Update Failed";
            }
            Response.Redirect("couponListing.aspx",true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}