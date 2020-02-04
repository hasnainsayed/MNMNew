using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class statusControl : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //BindData();
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

    public void BindData()
    {
        try
        {
            statusControlCls obj = new statusControlCls();
            DataTable dt = obj.getStockupInv();
            rtp_List.DataSource = dt;
            rtp_List.DataBind();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void rtp_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            Label currentStatus = (Label)e.Item.FindControl("currentStatus");
            DropDownList Status = (DropDownList)e.Item.FindControl("Status");
            Status.SelectedValue = currentStatus.Text;
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void changeStatus_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            DropDownList Status = (DropDownList)rp1.FindControl("Status");
            Label StockupID = (Label)rp1.FindControl("StockupID");
            Label currentStatus = (Label)rp1.FindControl("currentStatus");
            Label barcode = (Label)rp1.FindControl("barcode"); 
            statusControlCls obj = new statusControlCls();
            int success = obj.changeBarcodeStatus(StockupID.Text, Status.SelectedValue, currentStatus.Text, barcode.Text);
            if(success.Equals(1))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Status Updated Successfully !');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertFail", "alert('Status Update Failed');", true);
            }
            DataTable newDt = new DataTable();
            rtp_List.DataSource = newDt;
            rtp_List.DataBind();
            searchField.Text = string.Empty;
            //BindData();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void searchBarcode_Click(object sender, EventArgs e)
    {
        try
        {
            statusControlCls obj = new statusControlCls();
            DataTable dt = obj.getStockupInvByBarcode(searchField.Text);
            rtp_List.DataSource = dt;
            rtp_List.DataBind();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}