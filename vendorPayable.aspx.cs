using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class vendorPayable : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                styleCls obj = new styleCls();
                DataTable dt = obj.getTable("Vendor");
                vendorId.DataSource = dt;
                vendorId.DataBind();
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

    protected void getVendorPayable_Click(object sender, EventArgs e)
    {
        try
        {
            lotPaymentCls obj = new lotPaymentCls();
            DataTable dt = obj.getVendorPayable(vendorId.SelectedValue);
            rtp_List.DataSource = dt;
            rtp_List.DataBind();

            // bind headers
            styleCls obj1 = new styleCls();
            DataTable dt1 = obj1.getTable("paymentCheckPoint");
            checkColumn1Th.Text = "Under " + dt1.Rows[0]["checkOne"].ToString();
            checkColumn2Th.Text = (Convert.ToInt32(dt1.Rows[0]["checkOne"]) + 1).ToString() + "-" + dt1.Rows[0]["checkTwo"].ToString();
            checkColumn3Th.Text = (Convert.ToInt32(dt1.Rows[0]["checkTwo"]) + 1).ToString() + "-" + dt1.Rows[0]["checkThree"].ToString();
            checkColumn4Th.Text = dt1.Rows[0]["checkThree"].ToString() + "+";

            checkColumn1Sum.Text = string.Empty;
            checkColumn2Sum.Text = string.Empty;
            checkColumn3Sum.Text = string.Empty;
            checkColumn4Sum.Text = string.Empty;
            total.Text = string.Empty;

            if (!dt.Rows.Count.Equals(0) && !dt1.Rows.Count.Equals(0))
            {
                object sum1;
                sum1 = dt.Compute("SUM(pendingAmount)", "dayDifference <=" + dt1.Rows[0]["checkOne"].ToString() + " and dayDifference>0 and paymentStatus='Unpaid'");
                checkColumn1Sum.Text = sum1.ToString();

                object sum2;
                sum2 = dt.Compute("SUM(pendingAmount)", "dayDifference <=" + dt1.Rows[0]["checkTwo"].ToString() + " and dayDifference>" + dt1.Rows[0]["checkOne"].ToString()+ " and paymentStatus='Unpaid'");
                checkColumn2Sum.Text = sum2.ToString();

                object sum3;
                sum3 = dt.Compute("SUM(pendingAmount)", "dayDifference <=" + dt1.Rows[0]["checkThree"].ToString() + " and dayDifference>" + dt1.Rows[0]["checkTwo"].ToString() + " and paymentStatus='Unpaid'");
                checkColumn3Sum.Text = sum3.ToString();

                object sum4;
                sum4 = dt.Compute("SUM(pendingAmount)", "dayDifference >" + dt1.Rows[0]["checkThree"].ToString() + " and paymentStatus='Unpaid'");
                checkColumn4Sum.Text = sum4.ToString();

                object tot;
                tot = dt.Compute("SUM(pendingAmount)", "dayDifference >0 and paymentStatus='Unpaid'");
                total.Text = tot.ToString();
            }
            }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void rtp_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            string lotId = ((DataRowView)e.Item.DataItem)["BagID"].ToString();
            // payment details
            lotPaymentCls lObj = new lotPaymentCls();
            DataTable paymentDt = lObj.getPaymentByLot(lotId);
            Repeater paymentRpt = (Repeater)e.Item.FindControl("paymentRpt");
            paymentRpt.DataSource = paymentDt;
            paymentRpt.DataBind();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void getSuspenseReport_Click(object sender, EventArgs e)
    {
        try
        {
            Session["suspensePay"] = vendorId.SelectedValue;
            /*Page.ClientScript.RegisterStartupScript(
            this.GetType(), "OpenWindow", "window.open('suspenseReceivable.aspx','_newtab');", true);*/
            Response.Redirect("suspensePayable.aspx", true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}