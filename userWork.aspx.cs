using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class userWork : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                styleCls obj = new styleCls();
                DataTable dt = obj.getTable("login");
                users.DataSource = dt;
                users.DataBind();

                dates.Text = DateTime.Now.ToString("MM/dd/yyyy");
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "firedate();", true);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykeySearch", "firedtSearchDesc();", true);
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void getUserReport_Click(object sender, EventArgs e)
    {
        try
        {
            reportCls obj = new reportCls();
            DataTable stock = obj.getStocked(users.SelectedValue,dates.Text);
            DataView dtstockview = new DataView(stock);
            string sortstring = "SystemDate ASC"; // sorting in descending manner 
            dtstockview.Sort = sortstring;
            DataTable dtstocksort = dtstockview.ToTable();
            rptStocked.DataSource = dtstocksort;
            rptStocked.DataBind();
            stockCount.Text = stock.Rows.Count.ToString();

            //DataTable list = obj.getList(users.SelectedValue, dates.Text);
            //DataView dtlistview = new DataView(list);
            //string liststring = "recordtimestamp ASC"; // sorting in descending manner 
            //dtlistview.Sort = liststring;
            //DataTable dtlistsort = dtlistview.ToTable();
            //rptListed.DataSource = dtlistsort;
            //rptListed.DataBind();
            //listedCnt.Text = list.Rows.Count.ToString();

            DataTable sold = obj.getSoldByUser(users.SelectedValue, dates.Text);
            DataView dtsoldview = new DataView(sold);
            string soldstring = "salesDateTime ASC"; // sorting in descending manner 
            dtsoldview.Sort = soldstring;
            DataTable dtsoldsort = dtsoldview.ToTable();
            rptSold.DataSource = dtsoldsort;
            rptSold.DataBind();
            soldCnt.Text = sold.Rows.Count.ToString();

            DataTable dispatch = obj.getDispatched(users.SelectedValue, dates.Text);
            DataView dtdispatchview = new DataView(dispatch);
            string dispatchstring = "dispatchtimestamp ASC"; // sorting in descending manner 
            dtdispatchview.Sort = dispatchstring;
            DataTable dtdispatchsort = dtdispatchview.ToTable();
            rptDispatch.DataSource = dtdispatchsort;
            rptDispatch.DataBind();
            dispatchedCnt.Text = dispatch.Rows.Count.ToString();

            DataTable returned = obj.getReturned(users.SelectedValue, dates.Text);
            DataView dtreturnedview = new DataView(returned);
            string returnedstring = "returntimestamp ASC"; // sorting in descending manner 
            dtreturnedview.Sort = returnedstring;
            DataTable dtreturnedsort = dtreturnedview.ToTable();
            rptReturn.DataSource = dtreturnedsort;
            rptReturn.DataBind();
            returnCnt.Text = returned.Rows.Count.ToString();

            DataTable canceled = obj.getCancelled(users.SelectedValue, dates.Text);
            DataView dtcanceledview = new DataView(canceled);
            string canceledstring = "cancelTimeStamp ASC"; // sorting in descending manner 
            dtcanceledview.Sort = canceledstring;
            DataTable dtcanceledsort = dtcanceledview.ToTable();
            rptCancel.DataSource = dtcanceledsort;
            rptCancel.DataBind();
            cancelledCnt.Text = canceled.Rows.Count.ToString();

            DataTable styleCode = obj.getStyle(users.SelectedValue, dates.Text);
            DataView dtstyleCodeview = new DataView(styleCode);
            string styleCodestring = "SystemDate ASC"; // sorting in descending manner 
            dtstyleCodeview.Sort = styleCodestring;
            DataTable dtstyleCodesort = dtstyleCodeview.ToTable();
            rptStyle.DataSource = dtstyleCodesort;
            rptStyle.DataBind();
            styleCnt.Text = styleCode.Rows.Count.ToString();


            DataTable statuscon = obj.getstatuscontrol(users.SelectedValue, dates.Text);
            DataView dtstatusconview = new DataView(statuscon);
            string statusstring = "datetime ASC"; // sorting in descending manner 
            dtstatusconview.Sort = statusstring;
            DataTable dtstatusconsort = dtstatusconview.ToTable();
            rptstatuscontrol.DataSource = dtstatusconsort;
            rptstatuscontrol.DataBind();
            lblstatuscunt.Text = statuscon.Rows.Count.ToString();

            DataTable listed = obj.getListed(users.SelectedValue, dates.Text);
            DataView dtlistedview = new DataView(listed);
            string listedstring = "datetime ASC"; // sorting in descending manner 
            dtlistedview.Sort = listedstring;
            DataTable dtlistedsort = dtlistedview.ToTable();
            Listedrpt.DataSource = dtlistedsort;
            Listedrpt.DataBind();
            lbllisted.Text = listed.Rows.Count.ToString();

            DataTable labels = obj.getLabels(users.SelectedValue, dates.Text);
            rpt_Labels.DataSource = labels;
            rpt_Labels.DataBind();
            lblCount.Text = labels.Rows.Count.ToString();


            accordionExample.Visible = true;

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}