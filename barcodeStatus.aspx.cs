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

public partial class barcodeStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
               
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "firedtSearchDesc();", true);
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void barcodeHistory_Click(object sender, EventArgs e)
    {
        try
        {
            rtp_List.DataSource = new DataTable();
            rtp_List.DataBind();
            showData.Visible = false;

            reportCls r = new reportCls();
            string[] barcodesarr = barcodeNo.Text.Split('/');
            DataTable style = r.getStyleDets(barcodesarr[0]);
            /*ItemCategory.Text = style.Rows[0]["ItemCategory"].ToString();
            C2Name.Text = style.Rows[0]["C2Name"].ToString();
            C3Name.Text = style.Rows[0]["C3Name"].ToString();
            brand.Text = style.Rows[0]["brand"].ToString();
            Title.Text = style.Rows[0]["Title"].ToString();
            mrp.Text = style.Rows[0]["mrp"].ToString();
            image1Display.Visible = false;
            string imagelink = "http://finetouchimages.dzvdesk.com/Uploads/";
            if (!style.Rows[0]["image1"].ToString().Equals(""))
            {
                image1Display.Visible = true;
                image1Display.ImageUrl = imagelink + style.Rows[0]["image1"].ToString();
            }*/

            DataTable styledt = new DataTable();
            styledt.Columns.Add("Details");
            styledt.Columns.Add("User");
            styledt.Columns.Add("Dets");
            styledt.Rows.Add("Created", style.Rows[0]["username"].ToString() ,Convert.ToDateTime(style.Rows[0]["SystemDate"]).ToString("dd MMM yyyy HH:m:ss tt"));
            if (!style.Rows[0]["logs"].ToString().Equals(","))
            {
                string log = style.Rows[0]["logs"].ToString().Remove(style.Rows[0]["logs"].ToString().Length - 1, 1);
                string[] stylesArr = (log.Substring(1).Split(','));
                foreach (var line in stylesArr)
                {
                    string[] split = line.Split('#');
                    styledt.Rows.Add("Edited", split[0], Convert.ToDateTime(split[1]).ToString("dd MMM yyyy HH:m:ss tt"));
                }
            }
            
            rpt_Style.DataSource = styledt;
            rpt_Style.DataBind();

            DataSet ds = r.barcodeStatusHistory(barcodeNo.Text);
            DataTable dt = new DataTable();
            
            dt.Columns.Add("User");
            dt.Columns.Add("Status");
            dt.Columns.Add("DateTime",typeof(DateTime));

            DataTable createdby = ds.Tables["createdby"];
            dt.Rows.Add(createdby.Rows[0]["username"], createdby.Rows[0]["initialStatus"],createdby.Rows[0]["SystemDate"]);

            DataTable locTable = ds.Tables["locTable"];
            foreach(DataRow row in locTable.Rows)
            {
                if(!row["salesUser"].ToString().Equals("")) 
                {
                    dt.Rows.Add(row["salesUser"].ToString(), "SOLD", row["salesDateTime"]);
                }
                if (!row["dispatchUser"].ToString().Equals(""))
                {
                    dt.Rows.Add(row["dispatchUser"].ToString(), "DISPATCHED", row["dispatchtimestamp"]);
                }
                if (!row["retUser"].ToString().Equals(""))
                {
                    dt.Rows.Add(row["retUser"].ToString(), "RETURNED", row["returntimestamp"]);
                    dt.Rows.Add(row["retUser"].ToString(), row["changeStatus"].ToString(), row["returntimestamp"]);
                }
                

            }

            DataTable cancle = ds.Tables["cancle"];
            foreach (DataRow row in cancle.Rows)
            {
                if (!row["salesUser"].ToString().Equals(""))
                {
                    dt.Rows.Add(row["salesUser"].ToString(), "SOLD", row["salesDateTime"]);
                }
                if (!row["cancelUser"].ToString().Equals(""))
                {
                    dt.Rows.Add(row["cancelUser"].ToString(), "CANCELLED", row["cancelTimeStamp"]);
                    dt.Rows.Add(row["cancelUser"].ToString(), row["changeStatus"].ToString(), row["cancelTimeStamp"]);
                }
                
            }

            DataTable labels = ds.Tables["labels"];
            foreach (DataRow row in labels.Rows)
            {                
                if (!row["username"].ToString().Equals(""))
                {
                    dt.Rows.Add(row["username"].ToString(), "Labels : "+row["labelStatus"].ToString(), row["entryDate"]);
                }

            }

            //status change
            DataTable statusChange = ds.Tables["statusChange"];
            foreach (DataRow row in statusChange.Rows)
            {
                if (!row["username"].ToString().Equals(""))
                {
                    dt.Rows.Add(row["username"].ToString(), row["barcodenewstus"].ToString(), row["datetime"]);
                }

            }

            DataView dtview = new DataView(dt);
            string sortstring = "DateTime ASC"; // sorting in descending manner 
            dtview.Sort = sortstring;
            DataTable dtsort = dtview.ToTable();

            rtp_List.DataSource = dtsort;
            rtp_List.DataBind();
            showData.Visible = true;
            
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }
}