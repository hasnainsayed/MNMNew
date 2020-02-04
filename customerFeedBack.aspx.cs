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


public partial class customerFeedBack : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindData();
                if (Session["custFeedBack"] != null)
                {
                    if (Session["custFeedBack"].ToString().Equals("Status Updated Successfully"))
                    {
                        divSucc.InnerText = Session["custFeedBack"].ToString();
                        divSucc.Visible = true;
                        divError.Visible = false;
                        Session.Remove("custFeedBack");
                    }
                    else
                    {
                        divError.InnerText = Session["custFeedBack"].ToString();
                        divError.Visible = true;
                        divSucc.Visible = false;
                        Session.Remove("custFeedBack");
                    }

                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "firedtSearch();", true);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey132", "firedate();", true);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey135", "firedatetime();", true); 
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
            fromDateF.Text = startDate.ToString("MM/dd/yyyy");
            toDateF.Text = endDate.ToString("MM/dd/yyyy");

            getFeedbacks();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    public void getFeedbacks()
    {
        try
        {
            divSucc.Visible = false;
            divError.Visible = false;
            customerFeedbackCls obj = new customerFeedbackCls();
            DataTable dt = obj.getFeedbacks(fromDate.Text, toDate.Text, salesid.Text, barcode.Text,custname.Text,phoneNo.Text, dateRange.Checked, salesCheck.Checked, barcodeCheck.Checked,customerCheck.Checked, phoneNoCheck.Checked, dateFollowup.Checked,fromDateF.Text,toDateF.Text);

            DataTable Satisfied = new DataTable();
            DataRow[] satisfiedRes = dt.Select("customerStatus = 'Satisfied'");
            if (satisfiedRes.Any())
            {
                Satisfied = satisfiedRes.CopyToDataTable();
            }
            rpt_Satisfied.DataSource = Satisfied;
            rpt_Satisfied.DataBind();

            DataTable Dissatisfied = new DataTable();
            DataRow[] dissatisfiedRes = dt.Select("customerStatus = 'Dissatisfied'");
            if (dissatisfiedRes.Any())
            {
                Dissatisfied = dissatisfiedRes.CopyToDataTable();
            }
            rpt_Disatisfied.DataSource = Dissatisfied;
            rpt_Disatisfied.DataBind();

            DataTable Others = new DataTable();
            DataRow[] othersRes = dt.Select("customerStatus = 'Pending' OR customerStatus = 'Follow-up Required' OR customerStatus = 'Refund Applied'");
            if (othersRes.Any())
            {
                Others = othersRes.CopyToDataTable();
            }
            rpt_Others.DataSource = Others;
            rpt_Others.DataBind();

            if (Session["custFeedBack"] != null)
            {
                if (Session["custFeedBack"].ToString().Equals("Status Updated Successfully"))
                {
                    divSucc.InnerText = Session["custFeedBack"].ToString();
                    divSucc.Visible = true;
                    divError.Visible = false;
                    Session.Remove("custFeedBack");
                }
                else
                {
                    divError.InnerText = Session["custFeedBack"].ToString();
                    divError.Visible = true;
                    divSucc.Visible = false;
                    Session.Remove("custFeedBack");
                }

            }

            divSubmitError.Visible = false;          
            showOthers.Visible = false;           
            mainList.Visible = true;


        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void searchFeedback_Click(object sender, EventArgs e)
    {
        try
        {
            getFeedbacks();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void setCallForm(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));

            Label sid = (Label)rp1.FindControl("sid");
            sid1.Text = sid.Text;

            Label callingCount = (Label)rp1.FindControl("callingCount");
            callingCount1.Text = callingCount.Text;

            Label customerStatus = (Label)rp1.FindControl("customerStatus");
            customerStatus1.Text = customerStatus.Text;

            Label dispatchtimestamp = (Label)rp1.FindControl("dispatchtimestamp");
            dispatchtimestamp1.Text = dispatchtimestamp.Text;

            Label salesidgivenbyvloc = (Label)rp1.FindControl("salesidgivenbyvloc");
            salesidgivenbyvloc1.Text = salesidgivenbyvloc.Text;

            Label custname = (Label)rp1.FindControl("custname");
            custname1.Text = custname.Text;

            Label phoneNo = (Label)rp1.FindControl("phoneNo");
            phoneNo1.Text = phoneNo.Text;

            Label Title = (Label)rp1.FindControl("Title");
            Title1.Text = Title.Text;

            Label C1Name = (Label)rp1.FindControl("C1Name");
            C1Name1.Text = C1Name.Text;

            Label salesAbwno = (Label)rp1.FindControl("salesAbwno");
            salesAbwno1.Text = salesAbwno.Text;

            Label deliveryStatus = (Label)rp1.FindControl("deliveryStatus");
            deliveryStatus1.Text = deliveryStatus.Text;

            Label deliveryDate = (Label)rp1.FindControl("deliveryDate");
            deliveryDate1.Text = deliveryDate.Text;

            Label smsStatus = (Label)rp1.FindControl("smsStatus");
            smsStatus1.Text = smsStatus.Text;

            Label callingStatus = (Label)rp1.FindControl("callingStatus");
            callHistory.Text = callingStatus.Text;

            Label contactStatus = (Label)rp1.FindControl("contactStatus");
            conStatus1.Text = contactStatus.Text; 

            //Button saveBtn = (Button)FindControl("btnSaveSuccess");

            if (callingCount.Text.Equals("0"))
            {
                btnSaveSuccess.Text = "Save Call 1";
                delStatusHide.Visible = false;
                delDateHide.Visible = false;
                delStatus.SelectedValue = "Picked Up";
                custStatus.SelectedValue = "Follow-up Required";
                custStatus.Enabled = false;
                follow.Visible = true;
                followUp.Text = DateTime.Now.ToString("MM/dd/yyyy");
            }

            if (callingCount.Text.Equals("1"))
            {
                btnSaveSuccess.Text = "Save Call 2";
                delStatusHide.Visible = true;
                delDateHide.Visible = true;
                delStatus.SelectedValue = deliveryStatus.Text;
                custStatus.SelectedValue = customerStatus.Text;
                conStatus.SelectedValue = conStatus1.Text;
                delDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                custStatus.Enabled = true;
                follow.Visible = true;
                followUp.Text = DateTime.Now.ToString("MM/dd/yyyy");
            }

            if (callingCount.Text.Equals("2"))
            {
                btnSaveSuccess.Text = "Save Call 3";
                delStatusHide.Visible = false;
                delDateHide.Visible = false;
                delStatus.SelectedValue = deliveryStatus.Text;
                custStatus.SelectedValue = customerStatus.Text;
                conStatus.SelectedValue = conStatus1.Text;
                //delDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                custStatus.Enabled = true;
                follow.Visible = true;
                followUp.Text = DateTime.Now.ToString("MM/dd/yyyy");
            }

            if (callingCount.Text.Equals("3"))
            {
                btnSaveSuccess.Text = "Save Call 4";
                delStatusHide.Visible = false;
                delDateHide.Visible = false;
                delStatus.SelectedValue = deliveryStatus.Text;
                custStatus.SelectedValue = customerStatus.Text;
                conStatus.SelectedValue = conStatus1.Text;
                //delDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                custStatus.Enabled = true;
                follow.Visible = true;
                followUp.Text = DateTime.Now.ToString("MM/dd/yyyy");
            }

            if (callingCount.Text.Equals("4"))
            {
                btnSaveSuccess.Text = "Save Call 5";
                delStatusHide.Visible = false;
                delDateHide.Visible = false;
                delStatus.SelectedValue = deliveryStatus.Text;
                custStatus.SelectedValue = customerStatus.Text;
                conStatus.SelectedValue = conStatus1.Text;
                //delDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                custStatus.Enabled = true;
                
            }

            showOthers.Visible = true;
            mainList.Visible = false;
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void call1_Click(object sender, EventArgs e)
    {
        try
        {
            setCallForm(sender,e);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void call2_Click(object sender, EventArgs e)
    {
        try
        {
            setCallForm(sender, e);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void call3_Click(object sender, EventArgs e)
    {
        try
        {
            setCallForm(sender, e);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void rpt_Others_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            Label callingCount = (Label)e.Item.FindControl("callingCount");

            // for ascending order list according to followuptime
            Label follwUpTime = (Label)e.Item.FindControl("follwUpTime");
            Label dispatchtimestamp = (Label)e.Item.FindControl("dispatchtimestamp");
            if(follwUpTime.Text.Equals(""))
            {
                follwUpTime.Text = dispatchtimestamp.Text;
            }
            LinkButton call1 = (LinkButton)e.Item.FindControl("call1");
            LinkButton call2 = (LinkButton)e.Item.FindControl("call2");
            LinkButton call3 = (LinkButton)e.Item.FindControl("call3");
            LinkButton call4 = (LinkButton)e.Item.FindControl("call4");
            LinkButton call5 = (LinkButton)e.Item.FindControl("call5");

            // visible false to all
            call1.Visible = false;
            call2.Visible = false;
            call3.Visible = false;
            call4.Visible = false;
            call5.Visible = false;
            

            if (callingCount.Text.Equals("0"))
            {
                call1.Visible = true;
            }
            if (callingCount.Text.Equals("1"))
            {
                call2.Visible = true;
            }
            if (callingCount.Text.Equals("2"))
            {
                call3.Visible = true;
            }
            if (callingCount.Text.Equals("3"))
            {
                call4.Visible = true;
            }
            if (callingCount.Text.Equals("4"))
            {
                call5.Visible = true;
                follow.Visible = false;
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
            getFeedbacks();
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
            sid1.Text = string.Empty;
            callingCount1.Text = string.Empty;
            customerStatus1.Text = string.Empty;
            dispatchtimestamp1.Text = string.Empty;
            salesidgivenbyvloc1.Text = string.Empty;
            custname1.Text = string.Empty;
            phoneNo1.Text = string.Empty;
            Title1.Text = string.Empty;
            C1Name1.Text = string.Empty;
            salesAbwno1.Text = string.Empty;
            deliveryStatus1.Text = string.Empty;
            deliveryDate1.Text = string.Empty;
            smsStatus1.Text = string.Empty;

            divSucc.Visible = false;
            divError.Visible = false;
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
            customerFeedbackCls obj = new customerFeedbackCls();
            int success = obj.saveFeedback(callingCount1.Text, sid1.Text, custStatus.SelectedValue, conStatus.SelectedValue, delStatus.SelectedValue, delDate.Text, callingStatus1.Text, followUp.Text);
            if (success.Equals(1))
            {
                Session["custFeedBack"] = "FeedBack Status Changed Successfully";
            }
            else
            {
                Session["custFeedBack"] = "FeedBack Status Changing Failed";
            }
            getFeedbacks();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void exportFeedback_Click(object sender, EventArgs e)
    {
        try
        {
            customerFeedbackCls obj = new customerFeedbackCls();
            DataTable dt = obj.getFeedbacks(fromDate.Text, toDate.Text, salesid.Text, barcode.Text, custname.Text, phoneNo.Text, dateRange.Checked, salesCheck.Checked, barcodeCheck.Checked, customerCheck.Checked, phoneNoCheck.Checked, dateFollowup.Checked, fromDateF.Text, toDateF.Text);
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "CustomerFeedback");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                //Response.ContentType = "application / vnd.ms-excel";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fname = "CustomerFeedback_" + DateTime.Now.ToString("MM/dd/yyyy_HH:mm:ss");

                Response.AddHeader("content-disposition", "attachment;filename=" + fname + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void call4_Click(object sender, EventArgs e)
    {
        try
        {
            setCallForm(sender, e);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void call5_Click(object sender, EventArgs e)
    {
        try
        {
            setCallForm(sender, e);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}