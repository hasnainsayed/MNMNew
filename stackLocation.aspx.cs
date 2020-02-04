using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class stackLocation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string RackIDPass = Session["RackIDPass"].ToString();
                Session.Remove("RackIDPass");
                rackID.Text = RackIDPass;

                string RackPass = Session["RackPass"].ToString();
                Session.Remove("RackPass");
                string SubLocationNamePass = Session["subLocNameR"].ToString();
                Session.Remove("subLocNameR");
                string physicalNamePass = Session["phyLocationNameR"].ToString();
                Session.Remove("phyLocationNameR");
                subLocation.Text = physicalNamePass + " => //" + SubLocationNamePass + "-" + RackPass;
                phyLocationName.Text = physicalNamePass;
                subLocName.Text = SubLocationNamePass;
                rackName.Text = RackPass;

                string physicalIDPass1 = Session["physicalIDPassR"].ToString();
                Session.Remove("physicalIDPassR");
                physicalIDPass.Text = physicalIDPass1;

                string subLocationIDR = Session["subLocationIDR"].ToString();
                Session.Remove("subLocationIDR");
                subLocIDPass.Text = subLocationIDR;

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
            locationCls obj = new locationCls();
            DataTable dt = obj.getStack(rackID.Text);
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
            Location.Text = string.Empty;
            stackQuantity.Text = string.Empty;
            hdnID.Text = string.Empty;
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
            divAddAlert.Visible = false;
            divUpdAlert.Visible = false;
            divErrorAlert.Visible = false;
            divFormErr.Visible = false;
            if (Location.Text.Equals(""))
            {
                divFormErr.InnerHtml = "Please Enter Stack";
                divFormErr.Visible = true;
            }
            else if (stackQuantity.Text.Equals(""))
            {
                divFormErr.InnerHtml = "Please Enter Occupancy";
                divFormErr.Visible = true;
            }
            else
            {
                locationCls obj = new locationCls();
                int Success = obj.addUpdateStack(Convert.ToInt32(rackID.Text), Convert.ToInt32(hdnID.Text), Location.Text, stackQuantity.Text);
                if (Success != -1)
                {
                    if (Success.Equals(2))
                    {
                        divErrorAlert.InnerHtml = "Duplicate Entry.Action Failed";
                        divErrorAlert.Visible = true;
                    }
                    else
                    {
                        if (hdnID.Text.Equals("0"))
                        {
                            divAddAlert.InnerHtml = "Added Successfully";
                            divAddAlert.Visible = true;
                        }
                        else
                        {
                            divUpdAlert.InnerHtml = "Updated Successfully";
                            divUpdAlert.Visible = true;

                        }
                    }


                }
                else
                {
                    divErrorAlert.InnerHtml = "Transaction Rolled Back";
                    divErrorAlert.Visible = true;
                }

                BindData();
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            divAddAlert.Visible = false;
            divUpdAlert.Visible = false;
            divErrorAlert.Visible = false;
            divFormErr.Visible = false;
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

    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            divAddAlert.Visible = false;
            divUpdAlert.Visible = false;
            divErrorAlert.Visible = false;
            divFormErr.Visible = false;
            clearData();
            hdnID.Text = "0";
            btnSave.Text = "Save";
            devCapone.Visible = true;
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void btnEditStack_Click(object sender, EventArgs e)
    {
        try
        {
            clearData();
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            Label StackID = (Label)rp1.FindControl("StackID");
            styleCls obj = new styleCls();
            DataTable lot = obj.getTablewithID("Stack", "StackID", Convert.ToInt32(StackID.Text));
            Location.Text = lot.Rows[0]["Stack"].ToString();
            stackQuantity.Text = lot.Rows[0]["stackQnty"].ToString();
            btnSave.Text = "Update";
            hdnID.Text = lot.Rows[0]["StackID"].ToString();
            devCapone.Visible = true;

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void backToRack_Click(object sender, EventArgs e)
    {
        try
        {
            Session["SublocationIDPass"] = subLocIDPass.Text;
            Session["SubLocationNamePass"] = subLocName.Text;
            Session["physicalNamePass"] = phyLocationName.Text;
            Session["physicalIDPass"] = physicalIDPass.Text;
            Response.Redirect("rackLocation.aspx", true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}