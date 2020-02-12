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

public partial class multipleInvoice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                bindInvoice();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey1", "firedatetime();", true);
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    private void bindInvoice()
    {
        try
        {
            invoiceCls obj = new invoiceCls();
            DataTable invoicelist = obj.getAllInvoice();            
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void printInvoices_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable drops = new DataTable();
            drops.Columns.Add("invid");
            int i = 0;
            foreach (RepeaterItem itemEquipment in rpt_dropdown.Items)
            {
                DropDownList drp_dropdown = (DropDownList)itemEquipment.FindControl("drp_dropdown");              
                /*if (!drp_dropdown.SelectedValue.Equals("-1"))
                {
                    drops.Rows.Add(drp_dropdown.SelectedValue);
                }*/
               
                Session["invoiceId"] = drp_dropdown.SelectedValue;
                ScriptManager.RegisterStartupScript(this, GetType(), "openWindow"+i, "window.open('viewInvoice.aspx','_blank');", true);
                //Response.Redirect("viewInvoice.aspx", true);
                i++;
            }

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void rpt_dropdown_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            string invid = ((DataRowView)e.Item.DataItem)["invid"].ToString();            
            DropDownList drp_dropdown = e.Item.FindControl("drp_dropdown") as DropDownList;

            invoiceCls obj = new invoiceCls();
            DataTable invoicelist = obj.getAllInvoice();
            drp_dropdown.DataSource = invoicelist;
            drp_dropdown.DataBind();
            drp_dropdown.Items.Insert(0, new ListItem("---- Select ----", "-1"));
            drp_dropdown.SelectedValue = invid;

            
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
            bindDropDown();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    private void bindDropDown()
    {
        try
        {
            DataTable dtProgLang = new DataTable();
            dtProgLang.Columns.Add("invid");
            foreach (RepeaterItem itemEquipment in rpt_dropdown.Items)
            {
                DropDownList drp_dropdown = (DropDownList)itemEquipment.FindControl("drp_dropdown");                
                dtProgLang.Rows.Add(drp_dropdown.SelectedValue); // Add Data 
            }

            dtProgLang.Rows.Add("-1");

            rpt_dropdown.DataSource = dtProgLang;
            rpt_dropdown.DataBind();



        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    
}