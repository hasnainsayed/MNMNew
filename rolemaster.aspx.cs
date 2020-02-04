using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class rolemaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
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

    protected void BindData()
    {
        try
        {

            //get data
            styleCls obj = new styleCls();
            DataTable dt = obj.getTable("roles");
            //DataTable dt1 = obj.getTablewithID("roleLoginTrans","roles",);
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
            roleName.Text = string.Empty;
            hdnID.Text = "0";
            LocationType.Checked = false;
            PhysicalLocation.Checked = false;
            VirtualLocation.Checked = false;
            Vendors.Checked = false;
            newLot.Checked = false;
            hsnCode.Checked = false;
            courier.Checked = false;
            usermaster.Checked = false;
            rolemasters.Checked = false;
            ColumnTableSetting.Checked = false;
            StyleColumnSetting.Checked = false;
            ItemCategory.Checked = false;
            Size.Checked = false;
            Category2.Checked = false;
            Category3.Checked = false;
            Category4.Checked = false;
            Category5.Checked = false;
            Column1.Checked = false;
            Column2.Checked = false;
            Column3.Checked = false;
            Column4.Checked = false;
            Column5.Checked = false;
            Column6.Checked = false;
            Column7.Checked = false;
            Column8.Checked = false;
            Column9.Checked = false;
            Column10.Checked = false;
            Column11.Checked = false;
            Column12.Checked = false;
            Column13.Checked = false;
            Column14.Checked = false;
            Column15.Checked = false;
            Column16.Checked = false;
            Column17.Checked = false;
            Column18.Checked = false;
            Column19.Checked = false;
            Column20.Checked = false;
            ItemStyleSearchAdd.Checked = false;
            addStyle.Checked = false;
            addRFLNR.Checked = false;
            ViewInventory.Checked = false;
            listing.Checked = false;
            sellingInd.Checked = false;
            invoice.Checked = false;
            returnItem.Checked = false;
            statusControl.Checked = false;
            cancleBarcode.Checked = false;
            changeMRP.Checked = false;
            deleteWrongBarcode.Checked = false;
            pickDispatch.Checked = false;
            pickCancel.Checked = false;
            notListed.Checked = false;
            skuInventory.Checked = false;
            dumps.Checked = false;
            salesDump.Checked = false;
            dispatchedDump.Checked = false;
            warehouseMap.Checked = false;
            userWork.Checked = false;
            barcodeStatus.Checked = false;
            styleStatus.Checked = false;
            bulkListing.Checked = false;
            saleScan.Checked = false;
            saleExcel.Checked = false;
            dispatchScan.Checked = false;
            dispatchExcel.Checked = false;
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
            if(roleName.Text.Equals(""))
            {
                err += "Please Eter Role Name";
            }
            if(!err.Equals(""))
            {
                divErr.InnerText = err;
            }
            else
            {
                roleCls obj = new roleCls();
                int success = obj.addEditRole(roleName.Text, LocationType.Checked.ToString(), PhysicalLocation.Checked.ToString(), VirtualLocation.Checked.ToString(), Vendors.Checked.ToString(), newLot.Checked.ToString()
                    , hsnCode.Checked.ToString(), courier.Checked.ToString(), usermaster.Checked.ToString(), rolemasters.Checked.ToString(), ColumnTableSetting.Checked.ToString(), StyleColumnSetting.Checked.ToString(), ItemCategory.Checked.ToString(),
                    Size.Checked.ToString(), Category2.Checked.ToString(), Category3.Checked.ToString(), Category4.Checked.ToString(), Category5.Checked.ToString(),
                    Column1.Checked.ToString(), Column2.Checked.ToString(), Column3.Checked.ToString(), Column4.Checked.ToString(), Column5.Checked.ToString(), Column6.Checked.ToString(), Column7.Checked.ToString(), Column8.Checked.ToString(),
                    Column9.Checked.ToString(), Column10.Checked.ToString(), Column11.Checked.ToString(), Column12.Checked.ToString(), Column13.Checked.ToString(), Column14.Checked.ToString(), Column15.Checked.ToString(),
                    Column16.Checked.ToString(), Column17.Checked.ToString(), Column18.Checked.ToString(), Column19.Checked.ToString(), Column20.Checked.ToString(),
                    ItemStyleSearchAdd.Checked.ToString(), addStyle.Checked.ToString(), addRFLNR.Checked.ToString(), ViewInventory.Checked.ToString(), listing.Checked.ToString(), sellingInd.Checked.ToString(), invoice.Checked.ToString(),
                    returnItem.Checked.ToString(), statusControl.Checked.ToString(), cancleBarcode.Checked.ToString(), changeMRP.Checked.ToString(), deleteWrongBarcode.Checked.ToString(),
                    pickDispatch.Checked.ToString(), pickCancel.Checked.ToString(), notListed.Checked.ToString(), skuInventory.Checked.ToString(), dumps.Checked.ToString(),
                    salesDump.Checked.ToString(), dispatchedDump.Checked.ToString(), warehouseMap.Checked.ToString(), userWork.Checked.ToString(), barcodeStatus.Checked.ToString(),
                    styleStatus.Checked.ToString(), bulkListing.Checked.ToString(), saleScan.Checked.ToString(), saleExcel.Checked.ToString(), dispatchScan.Checked.ToString(), dispatchExcel.Checked.ToString(),hdnID.Text, pickList.Checked.ToString(), tickets.Checked.ToString(), addTicket.Checked.ToString());
                if (success.Equals(0))
                {
                    clearData();
                    devCapone.Visible = false;
                    divAddAlert.Visible = true;
                    divUpdAlert.Visible = false;
                }                
                else
                {
                    clearData();
                    devCapone.Visible = false;
                    divUpdAlert.Visible = true;
                    divAddAlert.Visible = false;
                }
                BindData();
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
            clearData();
            devCapone.Visible = false;
            divErr.Visible = false;
            divAddAlert.Visible = false;
            divUpdAlert.Visible = false;
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
            Label roleId = (Label)rp1.FindControl("roleId");
            styleCls obj = new styleCls();
            DataTable roles = obj.getTablewithID("roles", "roleId", Convert.ToInt32(roleId.Text));
            hdnID.Text = roles.Rows[0]["roleId"].ToString();
            roleName.Text = roles.Rows[0]["roleName"].ToString();
            LocationType.Checked = Convert.ToBoolean(roles.Rows[0]["LocationType"].ToString());
            PhysicalLocation.Checked = Convert.ToBoolean(roles.Rows[0]["PhysicalLocation"].ToString());
            VirtualLocation.Checked = Convert.ToBoolean(roles.Rows[0]["VirtualLocation"].ToString());
            Vendors.Checked = Convert.ToBoolean(roles.Rows[0]["Vendors"].ToString());
            newLot.Checked = Convert.ToBoolean(roles.Rows[0]["newLot"].ToString());
            hsnCode.Checked = Convert.ToBoolean(roles.Rows[0]["hsnCode"].ToString());
            courier.Checked = Convert.ToBoolean(roles.Rows[0]["courier"].ToString());
            usermaster.Checked = Convert.ToBoolean(roles.Rows[0]["usermaster"].ToString());
            rolemasters.Checked = Convert.ToBoolean(roles.Rows[0]["rolemasters"].ToString());
            ColumnTableSetting.Checked = Convert.ToBoolean(roles.Rows[0]["ColumnTableSetting"].ToString());
            StyleColumnSetting.Checked = Convert.ToBoolean(roles.Rows[0]["StyleColumnSetting"].ToString());
            ItemCategory.Checked = Convert.ToBoolean(roles.Rows[0]["ItemCategory"].ToString());
            Size.Checked = Convert.ToBoolean(roles.Rows[0]["Size"].ToString());
            Category2.Checked = Convert.ToBoolean(roles.Rows[0]["Category2"].ToString());
            Category3.Checked = Convert.ToBoolean(roles.Rows[0]["Category3"].ToString());
            Category4.Checked = Convert.ToBoolean(roles.Rows[0]["Category4"].ToString());
            Category5.Checked = Convert.ToBoolean(roles.Rows[0]["Category5"].ToString());
            Column1.Checked = Convert.ToBoolean(roles.Rows[0]["Column1"].ToString());
            Column2.Checked = Convert.ToBoolean(roles.Rows[0]["Column2"].ToString());
            Column3.Checked = Convert.ToBoolean(roles.Rows[0]["Column3"].ToString());
            Column4.Checked = Convert.ToBoolean(roles.Rows[0]["Column4"].ToString());
            Column5.Checked = Convert.ToBoolean(roles.Rows[0]["Column5"].ToString());
            Column6.Checked = Convert.ToBoolean(roles.Rows[0]["Column6"].ToString());
            Column7.Checked = Convert.ToBoolean(roles.Rows[0]["Column7"].ToString());
            Column8.Checked = Convert.ToBoolean(roles.Rows[0]["Column8"].ToString());
            Column9.Checked = Convert.ToBoolean(roles.Rows[0]["Column9"].ToString());
            Column10.Checked = Convert.ToBoolean(roles.Rows[0]["Column10"].ToString());
            Column11.Checked = Convert.ToBoolean(roles.Rows[0]["Column11"].ToString());
            Column12.Checked = Convert.ToBoolean(roles.Rows[0]["Column12"].ToString());
            Column13.Checked = Convert.ToBoolean(roles.Rows[0]["Column13"].ToString());
            Column14.Checked = Convert.ToBoolean(roles.Rows[0]["Column14"].ToString());
            Column15.Checked = Convert.ToBoolean(roles.Rows[0]["Column15"].ToString());
            Column16.Checked = Convert.ToBoolean(roles.Rows[0]["Column16"].ToString());
            Column17.Checked = Convert.ToBoolean(roles.Rows[0]["Column17"].ToString());
            Column18.Checked = Convert.ToBoolean(roles.Rows[0]["Column18"].ToString());
            Column19.Checked = Convert.ToBoolean(roles.Rows[0]["Column19"].ToString());
            Column20.Checked = Convert.ToBoolean(roles.Rows[0]["Column20"].ToString());
            ItemStyleSearchAdd.Checked = Convert.ToBoolean(roles.Rows[0]["ItemStyleSearchAdd"].ToString());
            addStyle.Checked = Convert.ToBoolean(roles.Rows[0]["addStyle"].ToString());
            addRFLNR.Checked = Convert.ToBoolean(roles.Rows[0]["addRFLNR"].ToString());
            ViewInventory.Checked = Convert.ToBoolean(roles.Rows[0]["ViewInventory"].ToString());
            listing.Checked = Convert.ToBoolean(roles.Rows[0]["listing"].ToString());
            sellingInd.Checked = Convert.ToBoolean(roles.Rows[0]["sellingInd"].ToString());
            invoice.Checked = Convert.ToBoolean(roles.Rows[0]["invoice"].ToString());
            returnItem.Checked = Convert.ToBoolean(roles.Rows[0]["returnItem"].ToString());
            statusControl.Checked = Convert.ToBoolean(roles.Rows[0]["statusControl"].ToString());
            cancleBarcode.Checked = Convert.ToBoolean(roles.Rows[0]["cancleBarcode"].ToString());
            changeMRP.Checked = Convert.ToBoolean(roles.Rows[0]["changeMRP"].ToString());
            deleteWrongBarcode.Checked = Convert.ToBoolean(roles.Rows[0]["deleteWrongBarcode"].ToString());
            pickDispatch.Checked = Convert.ToBoolean(roles.Rows[0]["pickDispatch"].ToString());
            pickCancel.Checked = Convert.ToBoolean(roles.Rows[0]["pickCancel"].ToString());
            notListed.Checked = Convert.ToBoolean(roles.Rows[0]["notListed"].ToString());
            skuInventory.Checked = Convert.ToBoolean(roles.Rows[0]["skuInventory"].ToString());
            dumps.Checked = Convert.ToBoolean(roles.Rows[0]["dumps"].ToString());
            salesDump.Checked = Convert.ToBoolean(roles.Rows[0]["salesDump"].ToString());            
            dispatchedDump.Checked = Convert.ToBoolean(roles.Rows[0]["dispatchedDump"].ToString());
            warehouseMap.Checked = Convert.ToBoolean(roles.Rows[0]["warehouseMap"].ToString());
            userWork.Checked = Convert.ToBoolean(roles.Rows[0]["userWork"].ToString());
            barcodeStatus.Checked = Convert.ToBoolean(roles.Rows[0]["barcodeStatus"].ToString());
            styleStatus.Checked = Convert.ToBoolean(roles.Rows[0]["styleStatus"].ToString());
            bulkListing.Checked = Convert.ToBoolean(roles.Rows[0]["bulkListing"].ToString());
            saleScan.Checked = Convert.ToBoolean(roles.Rows[0]["saleScan"].ToString());
            saleExcel.Checked = Convert.ToBoolean(roles.Rows[0]["saleExcel"].ToString());
            dispatchScan.Checked = Convert.ToBoolean(roles.Rows[0]["dispatchScan"].ToString());
            dispatchExcel.Checked = Convert.ToBoolean(roles.Rows[0]["dispatchExcel"].ToString());
            devCapone.Visible = true;
            divErr.Visible = false;
            divAddAlert.Visible = false;
            divUpdAlert.Visible = false;
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
            devCapone.Visible = true;
            divErr.Visible = false;
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