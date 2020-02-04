using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class suspenseReceivable : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if(Session["suspenseRec"] !=null)
                {
                    custId.Text = Session["suspenseRec"].ToString();
                    Session.Remove("suspenseRec");
                    styleCls obj = new styleCls();
                    DataTable dt = obj.getTableColwithID("websiteCustomer", "webCustId",Convert.ToInt32(custId.Text), "custFirstName");
                    custName.Text = dt.Rows[0]["custFirstName"].ToString();
                    bindData();
                }
                else
                {
                    Response.Redirect("Default.aspx",true);
                }
                
                
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

    private void bindData()
    {
        try
        {
            lotPaymentCls obj = new lotPaymentCls();
            DataTable dt = obj.getSuspenseReceivable(custId.Text);
            rtp_List.DataSource = dt;
            rtp_List.DataBind();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void backToReceivable_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("getAllReceivable.aspx", true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}