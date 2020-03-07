using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class TaxMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                styleCls obj = new styleCls();
                DataTable dt = obj.getTable("tax");
                if (dt.Rows.Count.Equals(0))
                {
                    txttax.Text = string.Empty;
                    lblid.Text = "0";
                    btnSave.Text = "Save";
                }
                else
                {
                    txttax.Text = dt.Rows[0]["tax"].ToString();
                    lblid.Text = dt.Rows[0]["taxid"].ToString();
                    btnSave.Text = "Update";
                }
            }
        }
        catch (Exception ex)
        { }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            taxCls obj = new taxCls();
            obj.addedittax(lblid.Text, txttax.Text);

        }
        catch (Exception ex)
        { }
    }
}