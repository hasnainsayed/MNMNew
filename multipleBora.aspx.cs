using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;

public partial class multipleBora : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindData();
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

    private void BindData()
    {
        try
        {
            invoiceDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            btnSave.Text = "Save";
            styleCls obj = new styleCls();
            DataTable dt = obj.getTable("Vendor");
            vendorID.DataSource = dt;
            vendorID.DataBind();

            DataTable dt1 = obj.getTable("lrListing");
            lrno.DataSource = dt1;
            lrno.DataBind();
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
            utilityCls Uobj = new utilityCls();
            string years = Uobj.getYearCode(DateTime.Now.ToString("yyyy"));
            string month = Uobj.getCurrentMonth(DateTime.Now.ToString("MMMM"));
            string msg = string.Empty;
            if (month == "ERROR" || years == "ERROR")
            {
                msg = "Month/Year Conversion Error";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + msg + "');", true);
            }
            else
            {
                newLotCls obj = new newLotCls();
                string res = obj.saveMulBora(years, month, vendorID.SelectedValue, noOfBora.Text, Session["login"].ToString(), totalAmount.Text, invoiceNo.Text, invoiceDate.Text, totalPiece.Text, lrno.SelectedValue, travelCost.Text);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", "alert('" + res + "');window.location ='newLot.aspx';", true);
                Response.Redirect("newLot.aspx", true);
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
            Response.Redirect("newLot.aspx",true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}