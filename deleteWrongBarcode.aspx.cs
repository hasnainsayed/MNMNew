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

public partial class deleteWrongBarcode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnDeleteBarcode_Click(object sender, EventArgs e)
    {
        try
        {
            if (barcode.Text.Equals(""))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", "alert('Please Enter Barcode');", true);
            }
            else if (reasons.Text.Equals(""))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Reason');", true);
            }
            else
            {
                cancleCls obj = new cancleCls();
                string success = obj.deleteWrongBarcode(barcode.Text, reasons.Text);
                barcode.Text = string.Empty;
                reasons.Text = string.Empty;
                divAddAlert.InnerText = success;
                divAddAlert.Visible = true;
            }
            
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }
}