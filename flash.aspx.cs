using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class flash : System.Web.UI.Page
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
            DataTable dt = uObj.getTableColwithID("flashTable", "flashId", "1", "*");
            flashText.Value = dt.Rows[0]["flashText"].ToString();
            flashId.Text = dt.Rows[0]["flashId"].ToString();
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
            if (Convert.ToInt32(flashText.Value.Length) > Convert.ToInt32(100))
            {
                error += "Message length cannot be greater than 100 Characters";
            }
            communicationCls obj = new communicationCls();
            int success = obj.editFlashData(flashText.Value,flashId.Text);
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