using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class smsService : System.Web.UI.Page
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
            utilityCls uObj = new utilityCls();
            DataTable dt = uObj.getTableColwithID("smsService","smsid","1","*");
            smsMessage.Value = dt.Rows[0]["smsMessage"].ToString();
            logs.Value = dt.Rows[0]["logs"].ToString();
            smsSender.Text = dt.Rows[0]["smsSender"].ToString();
            apikey.Text = dt.Rows[0]["apikey"].ToString();
            smsid.Text = dt.Rows[0]["smsid"].ToString();
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
            string error = string.Empty;
            if(Convert.ToInt32(smsMessage.Value.Length)> Convert.ToInt32(10))
            {
                error += "Message length cannot be greater than 10 Letters";
            }
            communicationCls obj = new communicationCls();
            int success = obj.editSmsService(smsMessage.Value,apikey.Text,smsSender.Text,logs.Value,smsid.Text);            
            if (success.Equals(0))
            {
                divAddAlert.Visible = true;
                divUpdAlert.Visible = false;
            }
            else
            {
                divUpdAlert.Visible = true;
                divAddAlert.Visible = false;
            }
            BindData();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}