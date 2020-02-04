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
using System.Data.SqlClient;


public partial class sellOnWebsite : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindData();
                if (Session["websiteSuccFail"] != null)
                {
                    if (Session["websiteSuccFail"].ToString().Trim().Equals("Add/Update Successfully Done"))
                    {
                        divSucc.InnerText = Session["websiteSuccFail"].ToString();
                        divSucc.Visible = true;
                        divError.Visible = false;
                        Session.Remove("websiteSuccFail");
                    }
                    else
                    {
                        divError.InnerText = Session["websiteSuccFail"].ToString();
                        divError.Visible = true;
                        divSucc.Visible = false;
                        Session.Remove("websiteSuccFail");
                    }

                }
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

    private void BindData()
    {
        try
        {
            storedProcedureCls obj = new storedProcedureCls();
            DataTable dt = obj.getTable("webDisplay", "sellId","desc");
            GV.DataSource = dt;
            GV.DataBind();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            Session["addWebsite"] = "0";
            Response.Redirect("addWebsite.aspx",true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void editWebsite_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)(sender);
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            Label sellId = (Label)rp1.FindControl("sellId");
            Session["addWebsite"] = sellId.Text;
            Response.Redirect("addWebsite.aspx", true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}