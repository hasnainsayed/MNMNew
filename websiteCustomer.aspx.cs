using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class websiteCustomer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                bindData();
                if (Session["BStatChng"] != null)
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
            styleCls obj = new styleCls();
            DataTable dt = obj.getTable("websiteCustomer");
            rptCustomer.DataSource = dt;
            rptCustomer.DataBind();
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
            Label webCustId = (Label)rp1.FindControl("webCustId");
            string logs = "," + Session["username"].ToString() + ":" + DateTime.Now;
            storedProcedureCls obj = new storedProcedureCls();
            string result = obj.modifySingleCol("websiteCustomer", "cusStatus", "1", "webCustId", webCustId.Text, logs);
            Session["BStatChng"] = result;
            Response.Redirect("websiteCustomer.aspx");
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
            Label webCustId = (Label)rp1.FindControl("webCustId");
            string logs = "," + Session["username"].ToString() + ":" + DateTime.Now;
            storedProcedureCls obj = new storedProcedureCls();
            string result = obj.modifySingleCol("websiteCustomer", "cusStatus", "0", "webCustId", webCustId.Text, logs);
            Session["BStatChng"] = result;
            Response.Redirect("websiteCustomer.aspx");
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void rptCustomer_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            string cusStatus = ((DataRowView)e.Item.DataItem)["cusStatus"].ToString();
            LinkButton statusOn = (LinkButton)e.Item.FindControl("statusOn");
            LinkButton statusOff = (LinkButton)e.Item.FindControl("statusOff");

            if (cusStatus.Equals("True"))
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
}