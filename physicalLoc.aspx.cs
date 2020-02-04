using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class physicalLoc : System.Web.UI.Page
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
            locationCls obj = new locationCls();
            DataTable dt = obj.getPhysicalLocation();
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
            Contact.Text = string.Empty;
            Address.Text = string.Empty;
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
            
            locationCls obj = new locationCls();
            int Success = obj.addUpdatePhysical(Convert.ToInt32(hdnID.Text), LocationTypeID.SelectedValue,Location.Text, Contact.Text, Address.Text);
            if (Success != -1)
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

    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            clearData();
            styleCls obj = new styleCls();
            DataTable dt = obj.getTable("LocationType");
            LocationTypeID.DataSource = dt;
            LocationTypeID.DataBind();
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

    protected void btnAddSubLoc_Click(object sender, EventArgs e)
    {
        try
        {
            clearData();
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            Label LocationID = (Label)rp1.FindControl("LocationID");
            Label LocationName = (Label)rp1.FindControl("LocationName");
            Session["physicalLocationID"] = LocationID.Text;
            Session["physicalLocationName"] = LocationName.Text;
            Response.Redirect("subLocationaspx.aspx",true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void btnEditLoc_Click(object sender, EventArgs e)
    {
        try
        {
            clearData();
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            Label LocationID = (Label)rp1.FindControl("LocationID");

            styleCls obj = new styleCls();

            DataTable dt = obj.getTable("LocationType");
            LocationTypeID.DataSource = dt;
            LocationTypeID.DataBind();

            DataTable lot = obj.getTablewithID("Location", "LocationID", Convert.ToInt32(LocationID.Text));
            Location.Text = lot.Rows[0]["Location"].ToString();
            Contact.Text = lot.Rows[0]["Contact"].ToString();
            btnSave.Text = "Update";
            hdnID.Text = lot.Rows[0]["LocationID"].ToString();
            Address.Text = lot.Rows[0]["Address"].ToString();
            LocationTypeID.SelectedValue = lot.Rows[0]["LocationTypeID"].ToString();         
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
            string LocationTypeID = ((DataRowView)e.Item.DataItem)["LocationTypeID"].ToString();
            string LocationID = ((DataRowView)e.Item.DataItem)["LocationID"].ToString();
            string stockCnt = ((DataRowView)e.Item.DataItem)["stockCnt"].ToString();
            
            locationCls obj = new locationCls();
            int occupancy = obj.getPhysicalOccupancy(LocationID);

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