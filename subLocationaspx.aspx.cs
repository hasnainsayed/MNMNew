using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class subLocationaspx : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string physicalLocationID = Session["physicalLocationID"].ToString();
                Session.Remove("physicalLocationID");
                physicalID.Text = physicalLocationID;

                string physicalLocationName = Session["physicalLocationName"].ToString();
                Session.Remove("physicalLocationName");
                physicalLocation.Text = physicalLocationName;

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
            DataTable dt = obj.getSubLocation(physicalID.Text);
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
                divFormErr.InnerHtml = "Please Enter Sub Loaction";
                divFormErr.Visible = true;
            }
            else
            {
                locationCls obj = new locationCls();
                int Success = obj.addUpdateSubLocation(Convert.ToInt32(physicalID.Text), Convert.ToInt32(hdnID.Text), Location.Text);
                if (Success != -1)
                {
                    if(Success.Equals(2))
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

    protected void btnAddRack_Click(object sender, EventArgs e)
    {
        try
        {
            clearData();
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            Label SublocationID = (Label)rp1.FindControl("SublocationID");
            Label Sublocation = (Label)rp1.FindControl("Sublocation");
            //Label physicalNamePass = (Label)FindControl("physicalLocation");
            Session["SublocationIDPass"] = SublocationID.Text;
            Session["SubLocationNamePass"] = Sublocation.Text;
            Session["physicalNamePass"] = physicalLocation.Text;
            Session["physicalIDPass"] = physicalID.Text;
            Response.Redirect("rackLocation.aspx", true);
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
            Label SublocationID = (Label)rp1.FindControl("SublocationID");
            styleCls obj = new styleCls();
            DataTable lot = obj.getTablewithID("SubLocation", "SublocationID", Convert.ToInt32(SublocationID.Text));
            Location.Text = lot.Rows[0]["SubLocation"].ToString();            
            btnSave.Text = "Update";
            hdnID.Text = lot.Rows[0]["SublocationID"].ToString();
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

    protected void GV_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            
            string SublocationID = ((DataRowView)e.Item.DataItem)["SublocationID"].ToString();
            string stockCnt = ((DataRowView)e.Item.DataItem)["stockCnt"].ToString();

            locationCls obj = new locationCls();
            int occupancy = obj.getSubLocationOccupancy(SublocationID);

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