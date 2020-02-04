using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.UI.HtmlControls;
using ClosedXML;
using System.Data.SqlClient;


public partial class confirmEmail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                emailId.Text = "1";
                BindData();
                if (Session["confirmEmailtSuccfail"] != null)
                {
                    if (Session["confirmEmailtSuccfail"].ToString().Trim().Equals("Email Settings Updated"))
                    {
                        divAddAlert.InnerText = Session["confirmEmailtSuccfail"].ToString();
                        divAddAlert.Visible = true;
                        divError.Visible = false;
                        Session.Remove("confirmEmailtSuccfail");
                    }
                    else
                    {
                        divError.InnerText = Session["confirmEmailtSuccfail"].ToString();
                        divError.Visible = true;
                        divAddAlert.Visible = false;
                        Session.Remove("confirmEmailtSuccfail");
                    }

                }
            }
            else
            {
                
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
            storedProcedureCls obj = new storedProcedureCls();
            DataTable dt = obj.getTableWithCondition("confirmEmail","emailId",emailId.Text,"emailId","desc");
            senders.Text = dt.Rows[0]["sender"].ToString();
            subject.Text = dt.Rows[0]["subject"].ToString();
            body.Text = dt.Rows[0]["body"].ToString();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }

    }

    protected void cancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Default.aspx", true);
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
            string logs = "," + Session["userName"] + ":" + DateTime.Now;
            storedProcedureCls obj = new storedProcedureCls();
            string result = obj.saveEmailSettings("confirmEmail", senders.Text, subject.Text, body.Text, "1", logs);
            Session["confirmEmailtSuccfail"] = result;
            Response.Redirect("confirmEmail.aspx", true);
            /*if (Page.IsValid)
            {
                
            }
            else
            {
                Label1.Text = "";
            }*/
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}