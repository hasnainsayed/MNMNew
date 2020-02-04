using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class couponListing : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindData();
                if (Session["CouponSuccFail"] != null)
                {
                    divError.InnerText = Session["CouponSuccFail"].ToString();
                    divError.Visible = true;
                    Session.Remove("CouponSuccFail");
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "firedtSearch();", true);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey1", "firedate();", true);
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void BindData()
    {
        try
        {
            couponCls obj = new couponCls();
            DataTable dt = obj.getCouponList();
            rpt_Coupons.DataSource = dt;
            rpt_Coupons.DataBind();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            divSuccess.Visible = false;
            divError.Visible = false;
            couponCls obj = new couponCls();
            //int success = obj.addEditCoupon(hdnID.Text, couponName.Text, couponCategory.SelectedValue,validFrom.Text,validTo.Text,couponType.SelectedValue,couponDiscount.Text,discountOn.SelectedValue);
            int success = 1;
            // -1 rollback
            if (success.Equals(-1))
            {
                divError.InnerText = "Transaction RolledBack";
                divError.Visible = true;
            }
            // 0 success
            if (success.Equals(0))
            {
                if(hdnID.Text.Equals(0))
                { divSuccess.InnerText = "Successfully Added"; }
                else { divSuccess.InnerText = "Successfully Updated"; }                
                divSuccess.Visible = true;
                clearData();
                devCapone.Visible = false;
                BindData();
            }            
            // 1 duplicate record
            if (success.Equals(1))
            {
                divError.InnerText = "Duplicate Entry.Plaese Change Coupon Name";
                divError.Visible = true;
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            clearData();
            devCapone.Visible = false;
            btnSave.Text = "Save";
            divSuccess.Visible = false;
            divError.Visible = false;
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            /*clearData();
            validFrom.Text = DateTime.Now.ToString("MM/dd/yyyy");
            validTo.Text = DateTime.Now.ToString("MM/dd/yyyy");
            hdnID.Text = "0";
            btnSave.Text = "Save";
            styleCls obj = new styleCls();
            DataTable dt = obj.getTable("ItemCategory");
            couponCategory.DataSource = dt;
            couponCategory.DataBind();
            devCapone.Visible = true;*/

            Session["addCoupon"] = "0";
            Response.Redirect("addCoupon.aspx", true);

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void clearData()
    {
        try
        {
            couponName.Text = string.Empty;
            couponDiscount.Text = string.Empty;
            validFrom.Text = string.Empty;
            validTo.Text = string.Empty;           
            hdnID.Text = string.Empty;
            
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void editCoupon_Click(object sender, EventArgs e)
    {
        try
        {
            clearData();
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = (RepeaterItem)(btn.NamingContainer);
            Label couponId = (Label)rp1.FindControl("couponId");
            Session["addCoupon"] = couponId.Text;
            Response.Redirect("addCoupon.aspx", true);
            /*storedProcedureCls obj = new storedProcedureCls();
            DataTable dt = obj.getTableWithCondition("couponMaster", "couponId", couponId.Text, "couponId", "desc");

            styleCls sObj = new styleCls();
            DataTable dt1 = sObj.getTable("ItemCategory");
            couponCategory.DataSource = dt1;
            couponCategory.DataBind();

            hdnID.Text = dt.Rows[0]["couponId"].ToString();
            couponName.Text = dt.Rows[0]["couponName"].ToString();
            couponCategory.SelectedValue = dt.Rows[0]["couponCategory"].ToString();
            validFrom.Text = Convert.ToDateTime(dt.Rows[0]["validFrom"]).ToString("MM/dd/yyyy");
            validTo.Text = Convert.ToDateTime(dt.Rows[0]["validTo"]).ToString("MM/dd/yyyy");
            discountOn.SelectedValue = dt.Rows[0]["discountOn"].ToString();
            couponType.SelectedValue = dt.Rows[0]["couponType"].ToString();
            couponDiscount.Text = dt.Rows[0]["couponDiscount"].ToString();
            devCapone.Visible = true;*/
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}