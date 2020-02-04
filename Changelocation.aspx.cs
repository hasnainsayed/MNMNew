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
using ClosedXML;
using System.Data.SqlClient;
using ClosedXML.Excel;
using System.Globalization;

public partial class Changelocation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if(!IsPostBack)
            {
                BindData();
                locdiv.Visible = true;
            }
        }
        catch(Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }
    protected void BindData()
    {
        try
        {
            locationCls obj = new locationCls();
            DataTable dt = obj.getVirtualLocation("2");
            virtualLocation.DataSource = dt;
            virtualLocation.DataBind();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }
    protected void btngo_Click(object sender, EventArgs e)
    {
        try
        {
            payment_reportCls obj = new payment_reportCls();
            DataTable dt = obj.getwrongvloc(virtualLocation.SelectedValue);
            if (!dt.Rows.Count.Equals(0))
            {
                rptchloc.DataSource = dt;
                rptchloc.DataBind();
                chngstatus.Visible = true;
                locdiv.Visible = false;
                nodata.Visible = false;
                lblcurrntloc.Text = "Current Location Is " + virtualLocation.SelectedItem.Text;
                locationCls obj2 = new locationCls();
                DataTable dt1 = obj2.getVirtualLocation("2");
                ddllocation.DataSource = dt1;
                ddllocation.DataBind();
            }
            else
            {
                nodata.Visible = true;
                chngstatus.Visible = false;
                locdiv.Visible = true;
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    


    protected void changelocation_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("salesid");
            dt.Columns.Add("frm");
            foreach (RepeaterItem itemEquipment in rptchloc.Items)
            {


                Label id = (Label)itemEquipment.FindControl("id");
                Label salesid = (Label)itemEquipment.FindControl("salesid");
                Label frm = (Label)itemEquipment.FindControl("frm");
                dt.Rows.Add(id.Text, salesid.Text, frm.Text);
            }
            payment_reportCls obj = new payment_reportCls();
            int Success = obj.updatelocation(dt, virtualLocation.SelectedValue);
            if(Success.Equals(1))
            {
                chngstatus.Visible = false;
                locdiv.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfully Updated!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Failed!');", true);
            }
            }
        catch (Exception ex)
        {
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        try
        {
            locdiv.Visible = true;
            chngstatus.Visible = false;
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }
}