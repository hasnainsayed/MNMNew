using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class deletedBarcodes : System.Web.UI.Page
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
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey132", "firedate();", true);
            }
           

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    public void BindData()
    {
        try
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            fromDate.Text = startDate.ToString("MM/dd/yyyy");
            toDate.Text = endDate.ToString("MM/dd/yyyy");
            getDeletedBarcodes();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    public void getDeletedBarcodes()
    {
        try
        {
            divSucc.Visible = false;
            divError.Visible = false;
            cancleCls obj = new cancleCls();
            DataTable dt = obj.getDeletedBarcodes(fromDate.Text, toDate.Text, barcode.Text, salesid.Text, salesCheck.Checked, barcodeCheck.Checked, dateRange.Checked);
            DataTable dt1 = obj.getDeletedBarcodesCan(fromDate.Text, toDate.Text, barcode.Text, salesid.Text, salesCheck.Checked, barcodeCheck.Checked, dateRange.Checked);
            DataTable dt2 = obj.getDeletedpick(fromDate.Text, toDate.Text, barcode.Text, salesid.Text, salesCheck.Checked, barcodeCheck.Checked, dateRange.Checked);
            string err = string.Empty;
            
            if (err.Equals("error"))
            {
                divError.Visible = true;
                divError.InnerText = "Database Error";
            }
            else {
                del_rpt.DataSource = dt;
                del_rpt.DataBind();
                delcan_rpt.DataSource = dt1;
                delcan_rpt.DataBind();
                pick_rpt.DataSource = dt2;
                pick_rpt.DataBind();
            }
            
            /*DataTable Pending = new DataTable();
            DataRow[] pendingRes = dt.Select("ticketStatus = 'Pending'");
            if (pendingRes.Any())
            {
                Pending = pendingRes.CopyToDataTable();
            }
            rpt_Pending.DataSource = Pending;
            rpt_Pending.DataBind();

            DataTable Success = new DataTable();
            DataRow[] successRes = dt.Select("ticketStatus = 'Success'");
            if (successRes.Any())
            {
                Success = successRes.CopyToDataTable();
            }
            rpt_Success.DataSource = Success;
            rpt_Success.DataBind();

            DataTable Rejected = new DataTable();
            DataRow[] rejectedRes = dt.Select("ticketStatus = 'Rejected'");
            if (rejectedRes.Any())
            {
                Rejected = rejectedRes.CopyToDataTable();
            }
            rpt_Rejected.DataSource = Rejected;
            rpt_Rejected.DataBind();

            if (Session["saveTicketSuccess"] != null)
            {
                if (Session["saveTicketSuccess"].ToString().Equals("Ticket Status Changed Successfully"))
                {
                    divSucc.InnerText = Session["saveTicketSuccess"].ToString();
                    divSucc.Visible = true;
                    divError.Visible = false;
                    Session.Remove("saveTicketSuccess");
                }
                else
                {
                    divError.InnerText = Session["saveTicketSuccess"].ToString();
                    divError.Visible = true;
                    divSucc.Visible = false;
                    Session.Remove("saveTicketSuccess");
                }

            }*/




        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void searchBarcodesDel_Click(object sender, EventArgs e)
    {
        try
        {
            getDeletedBarcodes();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}