using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class Vendors : System.Web.UI.Page
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
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey1", "firedate();", true);
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
            styleCls obj = new styleCls();
            DataTable dt = obj.getTable("Vendor");
            GV.DataSource = dt;
            GV.DataBind();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void clearData()
    {
        try
        {
            VendorName.Text = string.Empty;
            Contact.Text = string.Empty;
            Email.Text = string.Empty;
            vAddress.Text = string.Empty;
            gstin.Text = string.Empty;
            City.Text = string.Empty;
            hdnID.Text = string.Empty;
            divError.Visible = false;
            
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
            clearData();
            btnSave.Text = "Save";
            devCapone.Visible = false;

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
            string err = string.Empty;
            if(VendorName.Text.Equals(""))
            {
                err += "Vendor Name ,";
            }
            if (Contact.Text.Equals(""))
            {
                err += "Contact No. ,";
            }
            if (Email.Text.Equals(""))
            {
                err += "Email Address ,";
            }
            if (City.Text.Equals(""))
            {
                err += "City ,";
            }
            if (vAddress.Text.Equals(""))
            {
                err += "Address ,";
            }
            if (gstin.Text.Equals(""))
            {
                err += "GST No. ,";
            }

            if(!err.Equals(""))
            {
                divError.InnerHtml = "Please Enter " + err.TrimEnd(',');
                divError.Visible = true;
            }
            else
            {
                divErrorAlert.Visible = false;
                divAddAlert.Visible = false;
                divUpdAlert.Visible = false;
                VendorsCls obj = new VendorsCls();
                int Success = obj.addUpdateVendor(Convert.ToInt32(hdnID.Text), VendorName.Text, Contact.Text, Email.Text, City.Text,
                    vAddress.Text, gstin.Text,drpsupervendor.SelectedValue);
                if (Success != -1)
                {
                    if (hdnID.Text.Equals(0))
                    {
                        divAddAlert.InnerHtml = "Added Successfully";
                        divAddAlert.Visible = true;
                    }
                    else
                    {
                        divUpdAlert.InnerHtml = "Updated Successfully";
                        divUpdAlert.Visible = true;
                    }
                    BindData();
                }
                else
                {
                    divErrorAlert.InnerHtml = "Transaction Rolled Back";
                    divErrorAlert.Visible = true;
                }
                clearData();
                devCapone.Visible = false;
            }
            
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
            clearData();
            hdnID.Text = "0";
            btnSave.Text = "Save";            
            devCapone.Visible = true;
            divErrorAlert.Visible = false;
            divAddAlert.Visible = false;
            divUpdAlert.Visible = false;
            styleCls obj = new styleCls();
            DataTable dt = obj.getTable("Column1");
            drpsupervendor.DataSource = dt;
            drpsupervendor.DataBind();
            drpsupervendor.Items.Insert(0, new ListItem("--- Select----", "0"));
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
            clearData();
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            Label lblVendorID = (Label)rp1.FindControl("lblVendorID");
            styleCls objL = new styleCls();
            DataTable lot = objL.getTablewithID("Vendor","VendorID",Convert.ToInt32(lblVendorID.Text));
            VendorName.Text = lot.Rows[0]["VendorName"].ToString();
            Contact.Text = lot.Rows[0]["Contact"].ToString();
            btnSave.Text = "Update";
            hdnID.Text = lot.Rows[0]["VendorID"].ToString();
            Email.Text = lot.Rows[0]["Email"].ToString();
            vAddress.Text = lot.Rows[0]["vAddress"].ToString();
            gstin.Text = lot.Rows[0]["gstin"].ToString();
            City.Text = lot.Rows[0]["City"].ToString();

            
            DataTable dt = objL.getTable("Column1");
            drpsupervendor.DataSource = dt;
            drpsupervendor.DataBind();
            if(lot.Rows[0]["svid"].ToString().Equals(""))
            {
                drpsupervendor.Items.Insert(0, new ListItem("--- Select----", "0"));
            }
            else
            {
                drpsupervendor.SelectedValue = lot.Rows[0]["svid"].ToString();
            }

            devCapone.Visible = true;
            divErrorAlert.Visible = false;
            divAddAlert.Visible = false;
            divUpdAlert.Visible = false;

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

}