using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class salesCode : System.Web.UI.Page
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

    protected void BindData()
    {
        try
        {
            lblStyleID.Text = "26376";
            locationCls lobj = new locationCls();
            DataTable loc = lobj.getVirtualLocation("2");
            virtualLocation.DataSource = loc;
            virtualLocation.DataBind();

            styleCls sobj = new styleCls();
            DataTable state = sobj.getTable("stateCode");
            stateID.DataSource = state;
            stateID.DataBind();

            string gst = sobj.getGST(lblStyleID.Text);
            lblGSTPercent.Text = gst;

            DataTable dt = sobj.getStockUpInward(lblStyleID.Text, loc.Rows[0]["LocationID"].ToString());
            rptSales.DataSource = dt;
            rptSales.DataBind();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void virtualLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        try {

            styleCls sobj = new styleCls();
            DataTable dt = sobj.getStockUpInward(lblStyleID.Text, virtualLocation.SelectedValue);
            rptSales.DataSource = dt;
            rptSales.DataBind();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void saveSales_Click(object sender, EventArgs e)
    {
        try
        {
            styleCls obj = new styleCls();
            DataTable field = new DataTable();
            field.Columns.Add("StockUpId");
            field.Columns.Add("sp");
            decimal sum = Convert.ToDecimal(0.0);
            foreach (RepeaterItem itemEquipment in rptSales.Items)
            {
                CheckBox sales = (CheckBox)itemEquipment.FindControl("sales");
                
                if (sales.Checked)
                {
                    TextBox sp = (TextBox)itemEquipment.FindControl("sp");
                    Label stockUpId = (Label)itemEquipment.FindControl("stockUpId");
                    string selling = string.Empty;

                    if (sp.Text.Equals(""))
                    {
                        selling = "0";
                    }
                    else {
                        selling = sp.Text;
                    }
                    sum += Convert.ToDecimal(selling);
                    field.Rows.Add(stockUpId.Text, selling);

                }

            }

            //int success = obj.addSales(field, lblStyleID.Text, lblGSTPercent.Text,
                //salesId.Text,custname.Text,address1.Text,address2.Text,city.Text,stateID.SelectedValue,sum,virtualLocation.SelectedValue);
            Response.Redirect("invoice.aspx",true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}