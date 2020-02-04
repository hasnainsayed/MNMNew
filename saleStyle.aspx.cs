using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net;
using System.Text;

public partial class saleStyle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                styleCls obj = new styleCls();
                DataTable dt = obj.getTable("styleSale");
                rpt_Style.DataSource = dt;
                rpt_Style.DataBind();
                bindStyles();
                if (Session["styleSaleResult"] != null)
                {
                    divUpdAlert.InnerText = Session["styleSaleResult"].ToString();
                    divUpdAlert.Visible = true;
                    Session.Remove("styleSaleResult");
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

    private void bindStyles()
    {
        try
        {
            DataTable dtProgLang = new DataTable();
            dtProgLang.Columns.Add("styleId");
            foreach (RepeaterItem itemEquipment in rpt_Style.Items)
            {

                DropDownList style_drop = (DropDownList)itemEquipment.FindControl("style_drop");
                dtProgLang.Rows.Add(style_drop.SelectedValue); // Add Data 

            }

            dtProgLang.Rows.Add("-1");

            rpt_Style.DataSource = dtProgLang;
            rpt_Style.DataBind();



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
            DataTable dtProgLang = new DataTable();
            dtProgLang.Columns.Add("dropId");
            dtProgLang.Columns.Add("subDropId");
            foreach (RepeaterItem itemEquipment in rpt_Style.Items)
            {
                DropDownList style_drop = (DropDownList)itemEquipment.FindControl("style_drop");
                dtProgLang.Rows.Add(style_drop.SelectedValue, "-1"); // Add Data 
            }
            storedProcedureCls obj = new storedProcedureCls();
            string result = obj.addLatestProducts(dtProgLang, "styleSale");
            Session["styleSaleResult"] = result;
            Response.Redirect("saleStyle.aspx", true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void rpt_Style_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            string styleId = ((DataRowView)e.Item.DataItem)["styleId"].ToString();

            DropDownList style_drop = e.Item.FindControl("style_drop") as DropDownList;
            styleCls obj = new styleCls();
            DataTable dt = obj.getTable("ItemStyle");
            style_drop.DataSource = dt;
            style_drop.DataBind();
            style_drop.Items.Insert(0, new ListItem("---- Select ----", "-1"));
            style_drop.SelectedValue = styleId;

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void dropdownAdd_Click(object sender, EventArgs e)
    {
        try
        {
            bindStyles();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}