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
//using System.Web.UI.HtmlControls;

public partial class styleStatus : System.Web.UI.Page
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

    protected void styleHistory_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtSortempty = new DataTable();
            rpt_barcode.DataSource = dtSortempty;
            rpt_barcode.DataBind();

            DataTable styledt = new DataTable();
            rpt_Style.DataSource = styledt;
            rpt_Style.DataBind();

            reportCls r = new reportCls();
            DataTable style = r.getStyleDets(styleCode.Text);
                        
            styledt.Columns.Add("Details");
            styledt.Columns.Add("User");
            styledt.Columns.Add("Dets");
            styledt.Rows.Add("Created", style.Rows[0]["username"].ToString(), Convert.ToDateTime(style.Rows[0]["SystemDate"]).ToString("dd MMM yyyy HH:m:ss tt"));
            if(!style.Rows[0]["logs"].ToString().Equals(","))
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

            DataTable barcodes = r.getBarcodes(styleCode.Text);

            DataView dtview = new DataView(barcodes);
            string sortstring = "SystemDate ASC"; // sorting in descending manner 
            dtview.Sort = sortstring;
            DataTable dtsort = dtview.ToTable();

            rpt_barcode.DataSource = dtsort;
            rpt_barcode.DataBind();

            DataTable listed = r.getListedStyles(styleCode.Text);
            Listedrpt.DataSource = listed;
            Listedrpt.DataBind();
            showData.Visible = true;

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void shoeBarcodeStatus_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            Label lblBarcodeNo = (Label)rp1.FindControl("lblBarcodeNo");
            string[] barcodesarr = lblBarcodeNo.Text.Split('/');
            utilityCls Uobj = new utilityCls();
            Uobj.showCapARAPopUpSingle_Click(sender, e, PanelSinglePopUp, barcodesarr[0], lblBarcodeNo.Text);
            ModalPopupExtender299.Show();
            /*Panel PanelSinglePopUp = (Panel)Master.FindControl("PanelSinglePopUp");
            Uobj.showCapARAPopUpSingle_Click(sender, e, PanelSinglePopUp, barcodesarr[0], lblBarcodeNo.Text);
            MasterPage ms = (MasterPage)(this.Master);
            ms.ShowModalSignlePop();*/
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }
}