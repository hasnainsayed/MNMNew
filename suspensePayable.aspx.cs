using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class suspensePayable : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["suspensePay"] != null)
                {
                    vendorId.Text = Session["suspensePay"].ToString();
                    Session.Remove("suspensePay");
                    styleCls obj = new styleCls();
                    DataTable dt = obj.getTableColwithID("Vendor", "VendorID", Convert.ToInt32(vendorId.Text), "VendorName");
                    VendorName.Text = dt.Rows[0]["VendorName"].ToString();
                    bindData();
                }
                else
                {
                    Response.Redirect("Default.aspx", true);
                }


            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "firedate();", true);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykeySearch", "firedtSearch();", true);
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    private void bindData()
    {
        try
        {
            lotPaymentCls obj = new lotPaymentCls();
            DataTable dt = obj.getSuspensePayable(vendorId.Text);
            rtp_List.DataSource = dt;
            rtp_List.DataBind();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void backToPayable_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("vendorPayable.aspx", true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}