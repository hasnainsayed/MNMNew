using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class hsnCode : System.Web.UI.Page
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

    protected void BindData()
    {
        try
        {
            hsnCls obj = new hsnCls();
            DataTable dt = obj.getHsn();
            GV.DataSource = dt;
            GV.DataBind();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                hsnCls obj = new hsnCls();
                int Success = obj.addHSN(hsncode.Text, lowhighpt.Text, higst.Text,
                    hcgst.Text, hsgst.Text, ligst.Text, lcgst.Text, lsgst.Text);
                if (Success != -1)
                {
                    hsncode.Text = string.Empty;
                    lowhighpt.Text = string.Empty;
                    higst.Text = string.Empty;
                    hcgst.Text = string.Empty;
                    hsgst.Text = string.Empty;
                    ligst.Text = string.Empty;
                    lcgst.Text = string.Empty;
                    lsgst.Text = string.Empty;
                    hdnID.Text = string.Empty;
                    devCapone.Visible = false;
                    BindData();
                }

                else
                {

                }

            }
            else if (btnSave.Text == "Update")
            {
                hsnCls obj = new hsnCls();
                int Success = obj.updateHSN(Convert.ToInt32(hdnID.Text), hsncode.Text, lowhighpt.Text, higst.Text,
                    hcgst.Text, hsgst.Text, ligst.Text, lcgst.Text, lsgst.Text);
                if (Success != -1)
                {
                    hsncode.Text = string.Empty;
                    lowhighpt.Text = string.Empty;
                    higst.Text = string.Empty;
                    hcgst.Text = string.Empty;
                    hsgst.Text = string.Empty;
                    ligst.Text = string.Empty;
                    lcgst.Text = string.Empty;
                    lsgst.Text = string.Empty;
                    hdnID.Text = string.Empty;

                    devCapone.Visible = false;
                    BindData();
                }
                else
                {

                }
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            hsncode.Text = string.Empty;
            lowhighpt.Text = string.Empty;
            higst.Text = string.Empty;
            hcgst.Text = string.Empty;
            hsgst.Text = string.Empty;
            ligst.Text = string.Empty;
            lcgst.Text = string.Empty;
            lsgst.Text = string.Empty;
            hdnID.Text = string.Empty;
            btnSave.Text = "Save";
            devCapone.Visible = false;

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void edit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            Label hsnid = (Label)rp1.FindControl("lblhsnId");
            hsnCls objL = new hsnCls();
            DataTable hsn = objL.getHSNById(hsnid.Text);
            hsncode.Text = hsn.Rows[0]["hsncode"].ToString();
            lowhighpt.Text = hsn.Rows[0]["lowhighpt"].ToString();
            higst.Text = hsn.Rows[0]["higst"].ToString();
            hcgst.Text = hsn.Rows[0]["hcgst"].ToString();
            hsgst.Text = hsn.Rows[0]["hsgst"].ToString();
            ligst.Text = hsn.Rows[0]["ligst"].ToString();
            lcgst.Text = hsn.Rows[0]["lcgst"].ToString();
            lsgst.Text = hsn.Rows[0]["lsgst"].ToString();
            hdnID.Text = hsnid.Text;
            btnSave.Text = "Update";

            devCapone.Visible = true;

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            hsncode.Text = string.Empty;
            lowhighpt.Text = string.Empty;
            higst.Text = string.Empty;
            hcgst.Text = string.Empty;
            hsgst.Text = string.Empty;
            ligst.Text = string.Empty;
            lcgst.Text = string.Empty;
            lsgst.Text = string.Empty;
            hdnID.Text = string.Empty;
            btnSave.Text = "Save";
            devCapone.Visible = true;

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}