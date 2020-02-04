using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class lotPayment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if(Session["makeLotPayment"] !=null)
                {
                    lotId.Text = Session["makeLotPayment"].ToString();
                    Session.Remove("makeLotPayment");
                    bindData();
                }
                else
                {
                    Response.Redirect("newLot.aspx", true);
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

    private void bindData()
    {
        try
        {
            // lot details
            newLotCls objL = new newLotCls();
            DataTable lot = objL.getLotById(lotId.Text);
            vendorId.Text = lot.Rows[0]["VendorID"].ToString();
            invoiceDate.Text = Convert.ToDateTime(lot.Rows[0]["invoiceDate"]).ToString("dd MMM yyyy");
            invoiceNo.Text = lot.Rows[0]["invoiceNo"].ToString();
            lotPiece.Text = lot.Rows[0]["totalPiece"].ToString();
            lotAmount.Text = lot.Rows[0]["totalAmount"].ToString();
            lotNo.Text = lot.Rows[0]["BagDescription"].ToString();
            styleCls obj = new styleCls();
            DataTable vendor = obj.getTableColwithID("Vendor", "VendorID", Convert.ToInt32(lot.Rows[0]["VendorID"]), "VendorName");
            vendorName.Text = vendor.Rows[0]["VendorName"].ToString();

            // payment details
            lotPaymentCls lObj = new lotPaymentCls();
            DataTable paymentDt = lObj.getPaymentByLot(lotId.Text);
            paymentRpt.DataSource = paymentDt;
            paymentRpt.DataBind();

            decimal paidAmount = Convert.ToDecimal(0);
            if(!paymentDt.Rows.Count.Equals(0))
            {
                object sum;
                sum = paymentDt.Compute("SUM(paymentAmount)", string.Empty);
                paidAmount = Convert.ToDecimal(sum);
            }
            paidAmountTD.Text = paidAmount.ToString();
            pendingAmount.Text = (Convert.ToDecimal(lot.Rows[0]["totalAmount"].ToString())- paidAmount).ToString();

            paymentDate.Text = DateTime.Now.ToString("MM/dd/yyyy");

            // bind Centre          
            DataTable centreDt = obj.getTable("accountingCentre");
            paymentCentre.DataSource = centreDt;
            paymentCentre.DataBind();

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
            Response.Redirect("newLot.aspx", true);
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
            int success = obj.saveLotPayment(lotId.Text,paymentCentre.SelectedValue,paymentDate.Text,
                paymentMode.SelectedValue,paymentAmount.Text,paymentRemarks.Text, paymentTransaction.Text,Session["login"].ToString(), vendorId.Text, lotAmount.Text);
            if(success.Equals(0))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),"alert","alert('Added Successfully');window.location ='newLot.aspx';",true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1","alert('Transaction Failed');window.location ='newLot.aspx';", true);
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
    
}