using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using ClosedXML;
using System.Data.SqlClient;
using ClosedXML.Excel;
using System.Net;

public partial class websiteRefund : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindData();
                if (Session["refundSuccfail"] != null)
                {
                    if (Session["refundSuccfail"].ToString().Equals("Refund Done Successfully"))
                    {
                        divSucc.InnerText = Session["refundSuccfail"].ToString();
                        divSucc.Visible = true;
                        divError.Visible = false;
                        Session.Remove("refundSuccfail");
                    }
                    else
                    {
                        divError.InnerText = Session["refundSuccfail"].ToString();
                        divError.Visible = true;
                        divSucc.Visible = false;
                        Session.Remove("refundSuccfail");
                    }

                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "firedtSearch();", true);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey132", "firedate();", true);
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
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            fromDate.Text = startDate.ToString("MM/dd/yyyy");
            toDate.Text = endDate.ToString("MM/dd/yyyy");
            dateRange.Checked = true;
            getWebRefund();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    private void getWebRefund()
    {

        try
        {
            divSucc.Visible = false;
            divError.Visible = false;
            storedProcedureCls obj = new storedProcedureCls();
            DataTable Pending = obj.getWebRefund(fromDate.Text, toDate.Text, dateRange.Checked, "Pending");

            //DataTable PendingCancel = obj.getWebRefund(fromDate.Text, toDate.Text, dateRange.Checked, "PendingCancel");
            //Pending.Merge(PendingCancel);

            rpt_Pending.DataSource = Pending;
            rpt_Pending.DataBind();

            DataTable Approved = obj.getWebApproved(fromDate.Text, toDate.Text, dateRange.Checked, "Approved");
            rpt_Success.DataSource = Approved;
            rpt_Success.DataBind();

            DataTable Rejected = obj.getWebRejected(fromDate.Text, toDate.Text, dateRange.Checked, "Approved");
            rpt_Rejected.DataSource = Rejected;
            rpt_Rejected.DataBind();

            if (Session["saveRefundSuccess"] != null)
            {
                if (Session["saveRefundSuccess"].ToString().Equals("Refund Marked Successfully"))
                {
                    divSucc.InnerText = Session["saveRefundSuccess"].ToString();
                    divSucc.Visible = true;
                    divError.Visible = false;
                    Session.Remove("saveRefundSuccess");
                }
                else
                {
                    divError.InnerText = Session["saveRefundSuccess"].ToString();
                    divError.Visible = true;
                    divSucc.Visible = false;
                    Session.Remove("saveRefundSuccess");
                }

            }

            divSubmitError.Visible = false;
            showReject.Visible = false;
            showSuccess.Visible = false;
            //showVideo.Visible = false;
            mainList.Visible = true;
            //showImages.Visible = false;

        }


        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    
    protected void searchWebRefund_Click(object sender, EventArgs e)
    {
        try
        {
            getWebRefund();
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
            displaySalesId.Text = string.Empty;            
            refundDets.Text = string.Empty;
            transferDets.Text = string.Empty;
            displayReturnFrom.Text = string.Empty;
            displaySalesIdR.Text = string.Empty;
            refundDetsR.Text = string.Empty;
            displayReturnFromR.Text = string.Empty;
            divSucc.Visible = false;
            divError.Visible = false;
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void approveRefund_Click(object sender, EventArgs e)
    {
        try
        {
            clearData();
            LinkButton btn = ((LinkButton)(sender));

            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));

            Label sid = (Label)rp1.FindControl("sid");
            displaySalesId.Text = sid.Text;

            Label custRetTime = (Label)rp1.FindControl("custRetTime");
            displayReturnReq.Text = custRetTime.Text;

            Label BarcodeNo = (Label)rp1.FindControl("BarcodeNo");
            displayBarcode.Text = BarcodeNo.Text;

            Label returnFrom = (Label)rp1.FindControl("returnFrom");
            displayReturnFrom.Text = returnFrom.Text;

            refundDets.Text = string.Empty;

            transferDets.Text = string.Empty;

            showSuccess.Visible = true;
            mainList.Visible = false;
            showReject.Visible = false;
            
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void rejectRefund_Click(object sender, EventArgs e)
    {
        try
        {
            clearData();
            LinkButton btn = ((LinkButton)(sender));

            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));

            Label sid = (Label)rp1.FindControl("sid");
            displaySalesIdR.Text = sid.Text;

            Label custRetTime = (Label)rp1.FindControl("custRetTime");
            displayReturnReqR.Text = custRetTime.Text;

            Label BarcodeNo = (Label)rp1.FindControl("BarcodeNo");
            displayBarcodeR.Text = BarcodeNo.Text;

            Label returnFrom = (Label)rp1.FindControl("returnFrom");
            displayReturnFromR.Text = returnFrom.Text; 

            refundDets.Text = string.Empty;

            showSuccess.Visible = false;
            mainList.Visible = false;
            showReject.Visible = true;

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void btnRejectCancle_Click(object sender, EventArgs e)
    {
        try
        {
            clearData();
            getWebRefund();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void btnSaveReject_Click(object sender, EventArgs e)
    {
        try
        {
            string err = string.Empty;
            
            if (refundDetsR.Text.Equals(""))
            {
                err += "Please Enter Reason <br>";
            }
            if (err.Equals(""))
            {
                storedProcedureCls obj = new storedProcedureCls();
                string success = obj.saveRefundSuccess(displaySalesIdR.Text, "-1", string.Empty, refundDetsR.Text, displayReturnFromR.Text, "3");
                if (success.Equals(1))
                {
                    Session["saveTicketSuccess"] = "Refund Marked Successfully";
                }
                else
                {
                    Session["saveTicketSuccess"] = "Refund Marking Failed";
                }
                getWebRefund();
            }
            else
            {
                divSubmitError.InnerHtml = err;
                divSubmitError.Visible = true;
            }

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void btnSaveSuccess_Click(object sender, EventArgs e)
    {
        try
        {
            string err = string.Empty;
            if (transferDets.Text.Equals(""))
            {
                err += "Please Enter Transfer Deatils <br>";
            }
            if (refundDets.Text.Equals(""))
            {
                err += "Please Enter Refund Details <br>";
            }
            if (err.Equals(""))
            {
                storedProcedureCls obj = new storedProcedureCls();
                string success = obj.saveRefundSuccess(displaySalesId.Text, refundType.SelectedValue, transferDets.Text, refundDets.Text, displayReturnFrom.Text,"2");
                if (success.Equals(1))
                {
                    Session["saveTicketSuccess"] = "Refund Marked Successfully";
                }
                else
                {
                    Session["saveTicketSuccess"] = "Refund Marking Failed";
                }
                getWebRefund();
            }
            else
            {
                divSubmitError.InnerHtml = err;
                divSubmitError.Visible = true;
            }

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void btnSuccessCancle_Click(object sender, EventArgs e)
    {
        try
        {
            clearData();
            getWebRefund();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}