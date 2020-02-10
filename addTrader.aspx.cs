using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net;
using System.Text;

public partial class addTrader : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["addTrader"] != null)
                {
                    //bind lot
                    storedProcedureCls obj = new storedProcedureCls();
                    DataTable lotDt = obj.getTableWithCondition("Lot", "IsActive", "1", "BagDescription", "asc");
                    lotId.DataSource = lotDt;
                    lotId.DataBind();
                    lotId.Items.Insert(0, new ListItem("---- Select Bora ----", "-1"));

                    DataTable customerDt = obj.getTableWithCondition("websiteCustomer", "cusStatus", "1", "custFirstName", "asc");
                    customerId.DataSource = customerDt;
                    customerId.DataBind();
                    customerId.Items.Insert(0, new ListItem("---- Select Customer ----", "-1"));

                    // bind style repeater
                    bindStyle();
                }
                else
                {
                    Response.Redirect("invoice.aspx", true);
                }
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void styleAdd_Click(object sender, EventArgs e)
    {
        try
        {
            bindStyle();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    private void bindStyle()
    {
        try
        {
            DataTable dtProgLang = new DataTable();
            dtProgLang.Columns.Add("styleDrp");
            dtProgLang.Columns.Add("mrp");
            dtProgLang.Columns.Add("purchaseRate");
            dtProgLang.Columns.Add("sp");
            dtProgLang.Columns.Add("quantity");
            dtProgLang.Columns.Add("Size");
            foreach (RepeaterItem itemEquipment in rptStyle.Items)
            {

                DropDownList styleDrp = (DropDownList)itemEquipment.FindControl("styleDrp");
                DropDownList Size = (DropDownList)itemEquipment.FindControl("Size");
                TextBox mrp = (TextBox)itemEquipment.FindControl("mrp");
                TextBox purchaseRate = (TextBox)itemEquipment.FindControl("purchaseRate");
                TextBox quantity = (TextBox)itemEquipment.FindControl("quantity");
                TextBox sp = (TextBox)itemEquipment.FindControl("sp");
                dtProgLang.Rows.Add(styleDrp.SelectedValue, mrp.Text, purchaseRate.Text,sp.Text, quantity.Text, Size.SelectedValue); // Add Data 

            }

            dtProgLang.Rows.Add("-1", "", "", "", "", "-1");

            rptStyle.DataSource = dtProgLang;
            rptStyle.DataBind();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void rptStyle_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            string styleDrpT = ((DataRowView)e.Item.DataItem)["styleDrp"].ToString();
            string SizeT = ((DataRowView)e.Item.DataItem)["Size"].ToString();
            DropDownList styleDrp = e.Item.FindControl("styleDrp") as DropDownList;
            DropDownList Size = e.Item.FindControl("Size") as DropDownList;

            storedProcedureCls obj = new storedProcedureCls();
            DataTable styleDt = obj.getTable("ItemStyle", "Title", "asc");
            styleDrp.DataSource = styleDt;
            styleDrp.DataBind();
            styleDrp.Items.Insert(0, new ListItem("---- Select Style ----", "-1"));
            styleDrp.SelectedValue = styleDrpT;

            DataTable sizeDt = obj.getTable("Size", "Size1", "asc");
            Size.DataSource = sizeDt;
            Size.DataBind();
            Size.Items.Insert(0, new ListItem("---- Select Piece/Packet ----", "-1"));
            Size.SelectedValue = SizeT;
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void removeStyle_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            int index = rp1.ItemIndex;

            DataTable dtProgLang = new DataTable();
            dtProgLang.Columns.Add("styleDrp");
            dtProgLang.Columns.Add("mrp");
            dtProgLang.Columns.Add("purchaseRate");
            dtProgLang.Columns.Add("sp");
            dtProgLang.Columns.Add("quantity");
            dtProgLang.Columns.Add("Size");
            foreach (RepeaterItem itemEquipment in rptStyle.Items)
            {
                if (!itemEquipment.ItemIndex.Equals(index))
                {
                    DropDownList styleDrp = (DropDownList)itemEquipment.FindControl("styleDrp");
                    DropDownList Size = (DropDownList)itemEquipment.FindControl("Size");
                    TextBox mrp = (TextBox)itemEquipment.FindControl("mrp");
                    TextBox purchaseRate = (TextBox)itemEquipment.FindControl("purchaseRate");
                    TextBox quantity = (TextBox)itemEquipment.FindControl("quantity");
                    TextBox sp = (TextBox)itemEquipment.FindControl("sp");
                    dtProgLang.Rows.Add(styleDrp.SelectedValue, mrp.Text, purchaseRate.Text, sp.Text, quantity.Text, Size.SelectedValue); // Add Data 
                }
            }
            rptStyle.DataSource = dtProgLang;
            rptStyle.DataBind();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    private void DisableChilds(Control ctrl)
    {
        foreach (Control c in ctrl.Controls)
        {
            DisableChilds(c);
            if (c is TextBox)
            {
                ((TextBox)(c)).Enabled = false;
            }

            else if (c is DropDownList)
            {
                ((DropDownList)(c)).Enabled = false;
            }

            else if (c is RadioButtonList)
            {
                ((RadioButtonList)(c)).Enabled = false;
            }
        }

        foreach (RepeaterItem itemEquipment in rptStyle.Items)
        {
            LinkButton removeStyle = (LinkButton)itemEquipment.FindControl("removeStyle");
            removeStyle.Visible = false;
        }
    }

    private void EnableChilds(Control ctrl)
    {
        foreach (Control c in ctrl.Controls)
        {
            EnableChilds(c);
            if (c is TextBox)
            {
                ((TextBox)(c)).Enabled = true;
            }

            else if (c is DropDownList)
            {
                ((DropDownList)(c)).Enabled = true;
            }

            else if (c is RadioButtonList)
            {
                ((RadioButtonList)(c)).Enabled = true;
            }
        }
        foreach (RepeaterItem itemEquipment in rptStyle.Items)
        {
            LinkButton removeStyle = (LinkButton)itemEquipment.FindControl("removeStyle");
            removeStyle.Visible = true;
        }
    }

    protected void preview_Click(object sender, EventArgs e)
    {
        try
        {
            styleAdd.Visible = false;
            btnSave.Visible = true;
            editTrader.Visible = true;
            preview.Visible = false;
            DisableChilds(this.Page);

            //check the count of pieces available
            string res = pieceCountCheck();
            if(!res.Equals(""))
            {
               ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + res + "');", true);
               btnSave.Visible = false;
            }
            
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    private string pieceCountCheck()
    {
        string res = string.Empty;
        try
        {
            styleCls stObj = new styleCls();
            int countPieces = 0;
            foreach (RepeaterItem itemEquipment in rptStyle.Items)
            {
                DropDownList Size = (DropDownList)itemEquipment.FindControl("Size");
                TextBox quantity = (TextBox)itemEquipment.FindControl("quantity");

                DataTable lotDt = stObj.getTableColwithID("Size", "SizeID", Convert.ToInt32(Size.SelectedValue), "Size1");
                countPieces += Convert.ToInt32(quantity.Text) * Convert.ToInt32(lotDt.Rows[0]["Size1"]);
            }

            if(countPieces>Convert.ToInt32(avlPiece.Text))
            {
                res = "Available Pieces in BORA is less than the entered Pieces";
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
            res = ex.ToString();
        }
        return res;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string err = string.Empty;
            if(customerId.SelectedValue.Equals("-1"))
            {
                err += "Please Select Customer <br>";
            }
            if (lotId.SelectedValue.Equals("-1"))
            {
                err += "Please Select Bora <br>";
            }

            if(err.Equals(""))
            {
                string checks = pieceCountCheck();
                if (checks.Equals(""))
                {
                    DataTable dtProgLang = new DataTable();
                    dtProgLang.Columns.Add("styleDrp");
                    dtProgLang.Columns.Add("mrp");
                    dtProgLang.Columns.Add("purchaseRate");
                    dtProgLang.Columns.Add("sp");
                    dtProgLang.Columns.Add("quantity");
                    dtProgLang.Columns.Add("Size");
                    foreach (RepeaterItem itemEquipment in rptStyle.Items)
                    {

                        DropDownList styleDrp = (DropDownList)itemEquipment.FindControl("styleDrp");
                        DropDownList Size = (DropDownList)itemEquipment.FindControl("Size");
                        TextBox mrp = (TextBox)itemEquipment.FindControl("mrp");
                        TextBox purchaseRate = (TextBox)itemEquipment.FindControl("purchaseRate");
                        TextBox quantity = (TextBox)itemEquipment.FindControl("quantity");
                        TextBox sp = (TextBox)itemEquipment.FindControl("sp");
                        dtProgLang.Rows.Add(styleDrp.SelectedValue, mrp.Text, purchaseRate.Text, sp.Text, quantity.Text, Size.SelectedValue); // Add Data 

                    }

                    traderCls obj = new traderCls();
                    int res = obj.saveTraderNote(customerId.SelectedValue, lotId.SelectedValue, dtProgLang, Session["login"].ToString(), Session["userName"].ToString());
                    string res1 = string.Empty;
                    if (res.Equals(-1))
                    {
                        res1 = "Saving FALIED";
                    }
                    else
                    {
                        res1 = "Saved Successfully";
                    }
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", "alert('" + res1 + "');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(),"alert1", "alert('" + res1 + "');window.location ='Default.aspx';",true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + checks + "');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert333", "alert('" + err + "');", true);
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
            Response.Redirect("invoice.aspx", true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void editTrader_Click(object sender, EventArgs e)
    {
        try
        {
            styleAdd.Visible = true;
            btnSave.Visible = false;
            editTrader.Visible = false;
            preview.Visible = true;
            EnableChilds(this.Page);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void lotId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lotPieces.Text = string.Empty;
            barcodePiece.Text = string.Empty;
            avlPiece.Text = string.Empty;

            //get pieces count
            styleCls stObj = new styleCls();
            DataTable pieceCountDt = stObj.getPieceCountByLot(lotId.SelectedValue);
            int pieceCount = 0;
            if (!pieceCountDt.Rows.Count.Equals(0))
            {
                pieceCount = Convert.ToInt32(pieceCountDt.Rows[0]["pieces"]);
            }

            DataTable lotDt = stObj.getTableColwithID("Lot", "BagId", Convert.ToInt32(lotId.SelectedValue), "totalPiece");
            int pieceAvailable = (Int32)lotDt.Rows[0]["totalPiece"] - pieceCount;

            lotPieces.Text = lotDt.Rows[0]["totalPiece"].ToString();
            barcodePiece.Text = pieceCount.ToString();
            avlPiece.Text = pieceAvailable.ToString();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}