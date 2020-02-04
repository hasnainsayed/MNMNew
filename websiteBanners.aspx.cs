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
public partial class websiteBanners : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindData();
                if (Session["bannerSuccFail"] != null)
                {
                    if (Session["bannerSuccFail"].ToString().Trim().Equals("Add/Update Successfully Done"))
                    {
                        divSucc.InnerText = Session["bannerSuccFail"].ToString();
                        divSucc.Visible = true;
                        divError.Visible = false;
                        Session.Remove("bannerSuccFail");
                    }
                    else
                    {
                        divError.InnerText = Session["bannerSuccFail"].ToString();
                        divError.Visible = true;
                        divSucc.Visible = false;
                        Session.Remove("bannerSuccFail");
                    }

                }
            }
            else if (Session["BStatChng"] != null)
            {
                if (Session["BStatChng"].ToString().Trim().Equals("Status Updated"))
                {
                    divSucc.InnerText = Session["BStatChng"].ToString();
                    divSucc.Visible = true;
                    divError.Visible = false;
                    Session.Remove("BStatChng");
                }
                else
                {
                    divError.InnerText = Session["BStatChng"].ToString();
                    divError.Visible = true;
                    divSucc.Visible = false;
                    Session.Remove("BStatChng");
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
            DataTable dt = obj.getTable("banners", "bannerId", "desc");
            GV.DataSource = dt;
            GV.DataBind();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void editBanner_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)(sender);
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            Label bannerId = (Label)rp1.FindControl("bannerId");
            Session["addWebsiteBanner"] = bannerId.Text;
            Response.Redirect("addWebsiteBanner.aspx", true);
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
            Session["addWebsiteBanner"] = "0";
            Response.Redirect("addWebsiteBanner.aspx", true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void GV_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            string bannerType = ((DataRowView)e.Item.DataItem)["bannerType"].ToString();
            string bannerStatus = ((DataRowView)e.Item.DataItem)["bannerStatus"].ToString();
            LinkButton statusOn = (LinkButton)e.Item.FindControl("statusOn");
            LinkButton statusOff = (LinkButton)e.Item.FindControl("statusOff");
            
            if (bannerStatus.Equals("True"))
            {
                statusOn.CssClass = statusOn.CssClass.Replace("btn-default", "btn-success");                
            }
            else
            {
                statusOff.CssClass = statusOn.CssClass.Replace("btn-default", "btn-danger");
            }
            
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void statusOn_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton statusOn = ((LinkButton)(sender));
            RepeaterItem rp1 = (RepeaterItem)(statusOn.NamingContainer);
            Label bannerId = (Label)rp1.FindControl("bannerId");
            string logs = "," + Session["username"].ToString() + ":" + DateTime.Now;
            storedProcedureCls obj = new storedProcedureCls();
            string result = obj.modifySingleCol("banners", "bannerStatus", "1", "bannerId", bannerId.Text, logs);
            Session["BStatChng"] = result;
            Response.Redirect("websiteBanners.aspx");
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void statusOff_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton statusOn = ((LinkButton)(sender));
            RepeaterItem rp1 = (RepeaterItem)(statusOn.NamingContainer);
            Label bannerId = (Label)rp1.FindControl("bannerId");
            string logs = ","+Session["username"].ToString()+":"+DateTime.Now;
            storedProcedureCls obj = new storedProcedureCls();
            string result = obj.modifySingleCol("banners","bannerStatus", "0", "bannerId", bannerId.Text,logs);
            Session["BStatChng"] = result;
            Response.Redirect("websiteBanners.aspx");
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}