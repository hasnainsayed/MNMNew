using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class rackLocation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string SublocationIDPass = Session["SublocationIDPass"].ToString();
                Session.Remove("SublocationIDPass");
                subloactionID.Text = SublocationIDPass;

                string SubLocationNamePass = Session["SubLocationNamePass"].ToString();
                Session.Remove("SubLocationNamePass");
                string physicalNamePass = Session["physicalNamePass"].ToString();
                Session.Remove("physicalNamePass");
                subLocation.Text = physicalNamePass+" => "+SubLocationNamePass;
                phyLocationName.Text = physicalNamePass;
                subLocName.Text = SubLocationNamePass;

                string physicalIDPass1 = Session["physicalIDPass"].ToString();
                Session.Remove("physicalIDPass");
                physicalIDPass.Text = physicalIDPass1;
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
            DataTable dt = obj.getRack(subloactionID.Text);
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
                divFormErr.InnerHtml = "Please Enter Rack";
                divFormErr.Visible = true;
            }
            else
            {
                locationCls obj = new locationCls();
                int Success = obj.addUpdateRack(Convert.ToInt32(subloactionID.Text), Convert.ToInt32(hdnID.Text), Location.Text);
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

    protected void backToSubLoc_Click(object sender, EventArgs e)
    {
        try
        {
            Session["physicalLocationID"] = physicalIDPass.Text;
            Session["physicalLocationName"] = phyLocationName.Text;
            Response.Redirect("subLocationaspx.aspx",true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void btnAddStack_Click(object sender, EventArgs e)
    {
        try
        {
            clearData();
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            Label RackID = (Label)rp1.FindControl("RackID");
            Label Rack = (Label)rp1.FindControl("Rack");
            Session["RackIDPass"] = RackID.Text;
            Session["RackPass"] = Rack.Text;
            Session["phyLocationNameR"] = phyLocationName.Text;            
            Session["subLocNameR"] = subLocName.Text;
            Session["physicalIDPassR"] = physicalIDPass.Text;
            Session["subLocationIDR"] = subloactionID.Text;
            Response.Redirect("stackLocation.aspx", true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void btnEditSubLoc_Click(object sender, EventArgs e)
    {
        try
        {
            clearData();
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            Label RackID = (Label)rp1.FindControl("RackID");
            styleCls obj = new styleCls();
            DataTable lot = obj.getTablewithID("Rack", "RackID", Convert.ToInt32(RackID.Text));
            Location.Text = lot.Rows[0]["Rack"].ToString();
            btnSave.Text = "Update";
            hdnID.Text = lot.Rows[0]["RackID"].ToString();
            devCapone.Visible = true;

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void GV_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {

            string RackID = ((DataRowView)e.Item.DataItem)["RackID"].ToString();
            string stockCnt = ((DataRowView)e.Item.DataItem)["stockCnt"].ToString();

            locationCls obj = new locationCls();
            int occupancy = obj.getRackOccupancy(RackID);

            Label totalSpace = (Label)e.Item.FindControl("totalSpace");
            Label spaceAvailable = (Label)e.Item.FindControl("spaceAvailable");
            totalSpace.Text = occupancy.ToString();
            spaceAvailable.Text = (occupancy - Convert.ToInt32(stockCnt)).ToString();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}