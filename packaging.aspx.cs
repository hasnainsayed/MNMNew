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


public partial class packaging : System.Web.UI.Page
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
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey132", "firedate();", true);
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
            DateTime now = DateTime.Now;
            //var startDate = new DateTime(now.Year, now.Month, 1);

            var startDate = now.AddDays(-7);
            var endDate = now;
            fromDate.Text = startDate.ToString("MM/dd/yyyy");
            toDate.Text = endDate.ToString("MM/dd/yyyy");

            locationCls lobj = new locationCls();
            DataTable loc = lobj.getpackageLocation();
            vLoc.DataSource = loc;
            vLoc.DataBind();
            vLoc.Items.Insert(0, new ListItem("All", "-1"));

            getPackaging();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    private void getPackaging()
    {
        try
        {
            locationCls obj = new locationCls();
            DataTable dt = obj.getPackaging(fromDate.Text, toDate.Text, vLoc.SelectedValue,  barcode.Text,  vLocCheck.Checked,  barcodeCheck.Checked, dateRange.Checked);
            rpt_package.DataSource = dt;
            rpt_package.DataBind();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void searchPackaging_Click(object sender, EventArgs e)
    {
        try
        {
            getPackaging();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}