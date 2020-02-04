using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class pickList : System.Web.UI.Page
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
            stockUpCls obj = new stockUpCls();
            DataTable dt = obj.getSoldList();
            rpt_PickList.DataSource = dt;
            rpt_PickList.DataBind();

            courierCls cObj = new courierCls();
            DataTable courierDt = cObj.getCourier();
            courier.DataSource = courierDt;
            courier.DataBind();

            locationCls lobj = new locationCls();
            DataTable dt1 = lobj.getVirtualLocation("2");
            virtualLocation.DataSource = dt1;
            virtualLocation.DataBind();

            pickListTable.Visible = true;
            dispatchtable.Visible = false;
            cancleTable.Visible = false;



        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void dispatchItem_Click(object sender, EventArgs e)
    {

		try
		{
            utilityCls obj = new utilityCls();
            DataTable dt = obj.getTableColwithID("roles", "roleId", Session["userrole"].ToString(), "pickDispatch");
            if (dt.Rows[0]["pickDispatch"].Equals("False"))
            {
                Response.Redirect("accessDenied.aspx", true);

            }
            else
            {
            
            divError.Visible = false;
            divError1.Visible = false;
            displayBarcode.Text = string.Empty;
            displayStockupID.Text = string.Empty;
            displayRackCode.Text = string.Empty;
            displayItemCategory.Text = string.Empty;
            displayC1Name.Text = string.Empty;
            displaysalesidgivenbyvloc.Text = string.Empty;
            displaysid.Text = string.Empty;
            displaySubCategory.Text = string.Empty;
            awbNo.Text = string.Empty;
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));

            Label StockupID = (Label)rp1.FindControl("StockupID");
            Label fullBarcode = (Label)rp1.FindControl("fullBarcode");
            Label RackBarcode = (Label)rp1.FindControl("RackBarcode");
            Label ItemCategory = (Label)rp1.FindControl("ItemCategory");
            Label C1Name = (Label)rp1.FindControl("C1Name");
            Label salesidgivenbyvloc = (Label)rp1.FindControl("salesidgivenbyvloc");
            Label sid = (Label)rp1.FindControl("sid");
            Label subCategory = (Label)rp1.FindControl("subCategory");
            Label vLoc = (Label)rp1.FindControl("vLoc");
            Label image1 = (Label)rp1.FindControl("image1");

            displayBarcode.Text = fullBarcode.Text;
            displayStockupID.Text = StockupID.Text;
            displayRackCode.Text = RackBarcode.Text;
            displayItemCategory.Text = ItemCategory.Text;
            displayC1Name.Text = C1Name.Text;
            displaysalesidgivenbyvloc.Text = salesidgivenbyvloc.Text;
            displaysid.Text = sid.Text;
            displaySubCategory.Text = subCategory.Text;
            displayimage1Display.Visible = false;
            string imagelink = "http://finetouchimages.dzvdesk.com/Uploads/";
            if (!image1.Text.Equals(""))
            {
                displayimage1Display.Visible = true;
                displayimage1Display.ImageUrl = imagelink + image1.Text;
            }

            styleCls sobj = new styleCls();
            DataTable state = sobj.getTable("stateCode");
            stateID.DataSource = state;
            stateID.DataBind();

            invoiceCls invObj = new invoiceCls();
            DataTable invNo = invObj.getInvoiceDetsByStockupID(StockupID.Text);
            custname.Text = invNo.Rows[0]["custname"].ToString();
            address1.Text = invNo.Rows[0]["address1"].ToString();
            address2.Text = invNo.Rows[0]["address2"].ToString();
            phoneNo.Text = invNo.Rows[0]["phoneNo"].ToString();
            city.Text = invNo.Rows[0]["city"].ToString();
            stateID.SelectedValue = invNo.Rows[0]["state"].ToString();
            invoiceId.Text = invNo.Rows[0]["invid"].ToString();
            displayVirtual.Text = invNo.Rows[0]["Location"].ToString();
            
            pickListTable.Visible = false;
            dispatchtable.Visible = true;
                /*dispatchCls obj = new dispatchCls();
                int success = obj.dispatchItem(StockupID.Text);
                if (success.Equals(0))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Item Dispatched Successfully !');", true);
                    BindData();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Item Dispatch Failed !');", true);

                }*/
            }
        }
		catch (Exception ex)
		{
			RecordExceptionCls rex = new RecordExceptionCls();
			rex.recordException(ex);
		}
	}

    protected void btnPickList_Click(object sender, EventArgs e)
    {
        try {
            displayBarcode.Text = string.Empty;
            displayStockupID.Text = string.Empty;
            awbNo.Text = string.Empty;
            dispatchtable.Visible = false;
            cancleTable.Visible = false;
            pickListTable.Visible = true;
            BindData();
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
            if(awbNo.Text.Equals(""))
            {
                err += "Please Enter Return ABW No.<br>";
            }
            if(custname.Text.Equals(""))
            {
                err += "Please Enter Customer Name<br>";
            }
            if (address1.Text.Equals(""))
            {
                err += "Please Enter Address 1<br>";
            }
            if (address2.Text.Equals(""))
            {
                err += "Please Enter Address 2<br>";
            }
            if (phoneNo.Text.Equals(""))
            {
                err += "Please Enter Contact Number<br>";
            }
            if (city.Text.Equals(""))
            {
                err += "Please Enter City<br>";
            }
            
            if (err.Equals(""))
            {
                dispatchCls obj = new dispatchCls();
                int success = obj.dispatchItem(displayStockupID.Text, courier.SelectedValue, awbNo.Text, invoiceId.Text, custname.Text, address1.Text, address2.Text, city.Text, stateID.Text, phoneNo.Text);
                if (success.Equals(0))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Item Dispatched Successfully !');", true);
                    BindData();
                    //Response.Redirect("pickList.aspx", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Item Dispatch Failed !');", true);

                }
            }
            else
            {
                divError.InnerHtml = err;
                divError.Visible = true;
              
            }
            
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void rpt_PickList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            Label image1 = e.Item.FindControl("image1") as Label;
            string imagelink = "http://finetouchimages.dzvdesk.com/Uploads/";
            if(!image1.Text.Equals(""))
            {
                Image image1Display = e.Item.FindControl("image1Display") as Image;
                image1Display.Visible = true;
                image1Display.ImageUrl = imagelink + image1.Text;
            }
            
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void printPickList_Click(object sender, EventArgs e)
    {
        try
        {
            //Populating a DataTable from repeater.
            DataTable dt = new DataTable();
            dt.Columns.Add("Barcode");
            dt.Columns.Add("Vertical");
            dt.Columns.Add("Sub Category");
            dt.Columns.Add("Virtual Location");
            dt.Columns.Add("Brand");
            dt.Columns.Add("Sales Id");
            dt.Columns.Add("RackCode");
            

            foreach (RepeaterItem item in rpt_PickList.Items)
            {
                Label vLoc = ((Label)item.FindControl("vLoc"));
                if(vLoc.Text.Equals(virtualLocation.SelectedItem.Text))
                {
                    Label fullBarcode = ((Label)item.FindControl("fullBarcode"));
                    Label ItemCategory = ((Label)item.FindControl("ItemCategory"));
                    Label subCategory = ((Label)item.FindControl("subCategory"));
                    Label C1Name = ((Label)item.FindControl("C1Name"));
                    Label salesidgivenbyvloc = ((Label)item.FindControl("salesidgivenbyvloc"));
                    Label RackBarcode = ((Label)item.FindControl("RackBarcode"));
                    dt.Rows.Add(fullBarcode.Text, ItemCategory.Text, subCategory.Text, vLoc.Text, C1Name.Text, salesidgivenbyvloc.Text, RackBarcode.Text);
                }
                
                
            }

            utilityCls obj = new utilityCls();
            string html = obj.datatableToHtml(dt);
            Session["htmlstring"] = html;
            ScriptManager.RegisterStartupScript(UpdatePanel3, UpdatePanel3.GetType(), "OpenWindow", "window.open('printPage.aspx','_newtab');", true);

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void cancleItem_Click(object sender, EventArgs e)
    {
        try
        {
            utilityCls obj = new utilityCls();
            DataTable dt = obj.getTableColwithID("roles", "roleId", Session["userrole"].ToString(), "pickCancel");
            if (dt.Rows[0]["pickCancel"].Equals("False"))
            {
                Response.Redirect("accessDenied.aspx", true);

            }
            else
            {

            displayBarcode.Text = string.Empty;
            displayStockupID.Text = string.Empty;
            displayRackCode.Text = string.Empty;
            displayItemCategory.Text = string.Empty;
            displayC1Name.Text = string.Empty;
            displaysalesidgivenbyvloc.Text = string.Empty;
            displaysid.Text = string.Empty;
            displaySubCategory.Text = string.Empty;

            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));

            Label StockupID = (Label)rp1.FindControl("StockupID");
            Label fullBarcode = (Label)rp1.FindControl("fullBarcode");
            Label RackBarcode = (Label)rp1.FindControl("RackBarcode");
            Label ItemCategory = (Label)rp1.FindControl("ItemCategory");
            Label C1Name = (Label)rp1.FindControl("C1Name");
            Label salesidgivenbyvloc = (Label)rp1.FindControl("salesidgivenbyvloc");
            Label sid = (Label)rp1.FindControl("sid");
            Label subCategory = (Label)rp1.FindControl("subCategory");
            Label vLoc = (Label)rp1.FindControl("vLoc");
            Label image1 = (Label)rp1.FindControl("image1");

            displayBarcode1.Text = fullBarcode.Text;
            displayStockupID1.Text = StockupID.Text;
            displayRackCode1.Text = RackBarcode.Text;
            displayItemCategory1.Text = ItemCategory.Text;
            displayC1Name1.Text = C1Name.Text;
            displaysalesidgivenbyvloc1.Text = salesidgivenbyvloc.Text;
            displaysid1.Text = sid.Text;
            displaySubCategory1.Text = subCategory.Text;
            displayimage1Display1.Visible = false;
            string imagelink = "http://finetouchimages.dzvdesk.com/Uploads/";
            if (!image1.Text.Equals(""))
            {
                displayimage1Display1.Visible = true;
                displayimage1Display1.ImageUrl = imagelink + image1.Text;
            }
                        
            invoiceCls invObj = new invoiceCls();
            DataTable invNo = invObj.getInvoiceDetsByStockupID(StockupID.Text);
            invoiceId1.Text = invNo.Rows[0]["invid"].ToString();
            displayVirtual1.Text = invNo.Rows[0]["Location"].ToString();

            pickListTable.Visible = false;
            dispatchtable.Visible = false;
            cancleTable.Visible = true;

            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void btnCanclePickList_Click(object sender, EventArgs e)
    {
        try
        {
            if(!cancleReason.SelectedItem.Text.Equals("Customer Cancellation") && reasons.Text.Equals(""))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Cancellation Reason');", true);
            }
            else
            {
                dispatchCls obj = new dispatchCls();
                int success = obj.cancleItem(displayStockupID1.Text, invoiceId1.Text, displaysid1.Text, cancleStatus.SelectedValue, displayBarcode1.Text, reasons.Text, cancleReason.SelectedValue);
                if (success.Equals(0))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Item Canceled Successfully !');", true);
                    BindData();
                    Response.Redirect("pickList.aspx", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Item Cancellation Failed !');", true);
                    pickListTable.Visible = true;
                    dispatchtable.Visible = false;
                    cancleTable.Visible = false;
                }
            }
            
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}