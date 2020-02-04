using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PostCalculation : System.Web.UI.Page
{
    //string fromdate=
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if(!IsPostBack)
            {

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey132", "firedate();", true);
            }
        }
        catch(Exception ex)
        {
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }
    }

    protected void btncal_Click(object sender, EventArgs e)
    {
        try
        {
            Payment_fileCls obj = new Payment_fileCls();
            int Success = obj.PostCalculation(fromDate.Text, toDate.Text);
            if(Success.Equals(1))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Calculation Successfull !');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Failed !');", true);
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }
    }
}