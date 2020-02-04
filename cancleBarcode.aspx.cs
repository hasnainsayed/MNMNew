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

public partial class cancleBarcode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void btnCancelBarcode_Click(object sender, EventArgs e)
    {
        try
        {
            cancleCls obj = new cancleCls();
            string success = obj.deleteBarcode(barcode.Text,reasons.Text);
            barcode.Text = string.Empty;
            divAddAlert.InnerText = success;
            divAddAlert.Visible = true;
        }
        catch(Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void reprintBarcode_Click(object sender, EventArgs e)
    {
        try
        {
            cancleCls obj = new cancleCls();
            string success = obj.changePrintStatus(barcode.Text);
            barcode.Text = string.Empty;
            divAddAlert.InnerText = success;
            divAddAlert.Visible = true;
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }
}