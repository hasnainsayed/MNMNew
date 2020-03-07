using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using ClosedXML.Excel;
using System.IO;
using System.Globalization;

public partial class LRReport : System.Web.UI.Page
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
            styleCls obj = new styleCls();
            DataTable dt = obj.getTable("lrListing");
            drplr.DataSource = dt;
            drplr.DataBind();
            drplr.Items.Insert(0, new ListItem("--- Select----", "-2"));


        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
    protected void btnget_Click(object sender, EventArgs e)
    {
        try
        {
            LRReportsCls obj = new LRReportsCls();
            DataTable dt = obj.getLRReport(drplr.SelectedValue);
            GV.DataSource = dt;
            GV.DataBind();

            int barcode = dt.AsEnumerable().Where(x => x["invType"].ToString() == "Single").ToList().Count;
            int trader = dt.AsEnumerable().Where(x => x["invType"].ToString() == "Trader Note").ToList().Count;
            lbl1.Text = trader.ToString();
            lbl2.Text = barcode.ToString();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
    protected void refresh_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("LRReport.aspx", true);
        }
        catch (Exception ex)
        {

        }
    }
}