using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class addPayment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                bindData();
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

    private void bindData()
    {
        try
        {
            // bind Centre  
            styleCls obj = new styleCls();
            DataTable centreDt = obj.getTable("accountingCentre");
            paymentCentre.DataSource = centreDt;
            paymentCentre.DataBind();

            DataTable vendorDt = obj.getTable("Vendor");
            vendorId.DataSource = vendorDt;
            vendorId.DataBind();

            paymentDate.Text = DateTime.Now.ToString("MM/dd/yyyy");

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
            Response.Redirect("newLot.aspx",true);
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
            lotPaymentCls obj = new lotPaymentCls();
            int success = obj.saveAddPayment(paymentCentre.SelectedValue, paymentDate.Text,
                paymentMode.SelectedValue, paymentAmount.Text, paymentRemarks.Text, paymentTransaction.Text, Session["login"].ToString(), vendorId.Text);
            if (success.Equals(0))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Added Successfully');window.location ='newLot.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", "alert('Transaction Failed');window.location ='newLot.aspx';", true);
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}