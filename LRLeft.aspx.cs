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
public partial class LRLeft : System.Web.UI.Page
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
            drplr.Items.Insert(0, new ListItem("--- Select All----", "-2"));


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
            DataTable dt = obj.getLRLeft(drplr.SelectedValue);
            GV.DataSource = dt;
            GV.DataBind();

            object qunty = dt.Compute("Sum(quantity)", string.Empty);
            object rflqty = dt.Compute("Sum(rflQuantity)", string.Empty);
            object diff = dt.Compute("Sum(diff)", string.Empty);
            lbl1.Text = qunty.ToString();
            lbl2.Text = rflqty.ToString();
            lbl3.Text = diff.ToString();

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