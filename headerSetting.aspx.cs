using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class headerSetting : System.Web.UI.Page
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
            DataTable dt = uObj.getTableColwithID("headerSettings", "headerId", "1", "*");
            if(!dt.Rows.Count.Equals(0))
            {
                deals.Text = dt.Rows[0]["deals"].ToString();
                headerId.Text = dt.Rows[0]["headerId"].ToString();
                latest.Text = dt.Rows[0]["latest"].ToString();
            }
            else
            {
                headerId.Text = "0";
            }
            
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
            
            communicationCls obj = new communicationCls();
            int success = obj.editHeaderSettings(deals.Text,latest.Text, headerId.Text);
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