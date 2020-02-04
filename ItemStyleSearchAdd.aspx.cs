using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class ItemStyleSearchAdd : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
            if (!IsPostBack)
            {
                bindCatName();
                bindItemCategory();
                drp_itemCategory.Items.Insert(0, new ListItem("---- Select ----", "-1"));
                drp_catTwo.Items.Insert(0, new ListItem("---- Select ----", "-1"));
                drp_catThree.Items.Insert(0, new ListItem("---- Select ----", "-1"));
                drp_catFour.Items.Insert(0, new ListItem("---- Select ----", "-1"));
                drp_catFive.Items.Insert(0, new ListItem("---- Select ----", "-1"));
                bindFieldValue();

                bindDropDown();
                bindLatestStyles();
            }
            else {

                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "firedtSearch();", true);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "datekey", "firedate();", true);
            }

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void bindLatestStyles()
    {
        try
        {
            styleCls obj = new styleCls();
            DataTable dt = obj.getLatestStyle();
            rpt_Style.DataSource = dt;
            rpt_Style.DataBind();
            if(Session["addStyleSuccFail"] != null)
            {
                successfailure.Text = Session["addStyleSuccFail"].ToString();
                successfailure.Visible = true;
                Session.Remove("addStyleSuccFail");
            }
            searchData.Visible = true;

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void bindCatName()
    {
        try
        {
            styleCls obj = new styleCls();
            DataTable dt = obj.getCatName();
            if (!dt.Rows.Count.Equals(0))
            {
                lblCat2.Text = dt.Rows[0]["SettingName"].ToString();
                lblCat3.Text = dt.Rows[1]["SettingName"].ToString();
                lblCat4.Text = dt.Rows[2]["SettingName"].ToString();
                lblCat5.Text = dt.Rows[3]["SettingName"].ToString();
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void bindItemCategory()
    {
        try
        {
            styleCls obj = new styleCls();
            DataTable dt = obj.bindItemCategory();
            drp_itemCategory.DataSource = dt;
            drp_itemCategory.DataBind();
            
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void drp_itemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            styleCls obj = new styleCls();
            DataTable dt = obj.bindCategoryTwo(Convert.ToInt32(drp_itemCategory.SelectedValue));
            drp_catTwo.DataSource = dt;
            drp_catTwo.DataBind();
            drp_catTwo.Items.Insert(0, new ListItem("---- Select ----", "-1"));
            drp_catThree.Items.Clear();
            drp_catThree.Items.Insert(0, new ListItem("---- Select ----", "-1"));
            drp_catFour.Items.Clear();
            drp_catFour.Items.Insert(0, new ListItem("---- Select ----", "-1"));
            drp_catFive.Items.Clear();
            drp_catFive.Items.Insert(0, new ListItem("---- Select ----", "-1"));
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void drp_catTwo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            styleCls obj = new styleCls();
            DataTable dt = obj.bindCategoryThree(Convert.ToInt32(drp_catTwo.SelectedValue));
            drp_catThree.DataSource = dt;
            drp_catThree.DataBind();
            drp_catThree.Items.Insert(0, new ListItem("---- Select ----", "-1"));
            drp_catFour.Items.Clear();
            drp_catFour.Items.Insert(0, new ListItem("---- Select ----", "-1"));
            drp_catFive.Items.Clear();
            drp_catFive.Items.Insert(0, new ListItem("---- Select ----", "-1"));
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void drp_catThree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            styleCls obj = new styleCls();
            DataTable dt = obj.bindCategoryFour(Convert.ToInt32(drp_catThree.SelectedValue));
            drp_catFour.DataSource = dt;
            drp_catFour.DataBind();
            drp_catFour.Items.Insert(0, new ListItem("---- Select ----", "-1"));
            drp_catFive.Items.Clear();
            drp_catFive.Items.Insert(0, new ListItem("---- Select ----", "-1"));
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void drp_catFour_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            styleCls obj = new styleCls();
            DataTable dt = obj.bindCategoryFive(Convert.ToInt32(drp_catFour.SelectedValue));
            drp_catFive.DataSource = dt;
            drp_catFive.DataBind();
            drp_catFive.Items.Insert(0, new ListItem("---- Select ----", "-1"));
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void bindFieldValue()
    {
        try
        {
            styleCls obj = new styleCls();
            DataTable dt = obj.getDataFieldName();
            rpt_DataField.DataSource = dt;
            rpt_DataField.DataBind();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void btnShowAll_Click(object sender, EventArgs e)
    {
        try
        {
            styleCls obj = new styleCls();
            DataTable cat = new DataTable();
            cat.Columns.Add("Column");
            cat.Columns.Add("Search");
            if (!drp_itemCategory.SelectedValue.Equals("-1"))
            {
                cat.Rows.Add("ItemCatID", drp_itemCategory.SelectedValue);
            }
            if (!drp_catTwo.SelectedValue.Equals("-1"))
            {
                cat.Rows.Add("Cat2ID", drp_catTwo.SelectedValue);
            }
            if (!drp_catThree.SelectedValue.Equals("-1"))
            {
                cat.Rows.Add("Cat3ID", drp_catThree.SelectedValue);
            }
            if (!drp_catFour.SelectedValue.Equals("-1"))
            {
                cat.Rows.Add("Cat4ID", drp_catFour.SelectedValue);
            }
            if (!drp_catFive.SelectedValue.Equals("-1"))
            {
                cat.Rows.Add("Cat5ID", drp_catFive.SelectedValue);
            }

            DataTable field = new DataTable();
            field.Columns.Add("Column");
            field.Columns.Add("Search");
            if (!drpStyles.Equals(""))
            {
                field.Rows.Add("StyleCode", drpStyles.Text);
            }
            DataTable drp = new DataTable();
            drp.Columns.Add("Column");
            drp.Columns.Add("Search");

            searchLabel.Visible = true;
            showCat.Visible = false;
            drp_itemCategorylbl.InnerText = drp_itemCategory.SelectedItem.Text;
            drp_catTwolbl.InnerText = drp_catTwo.SelectedItem.Text;
            drp_catThreelbl.InnerText = drp_catThree.SelectedItem.Text;
            drp_catFourlbl.InnerText = drp_catFour.SelectedItem.Text;
            drp_catFivelbl.InnerText = drp_catFive.SelectedItem.Text;
            drpStyleDisplay.InnerText = drpStyles.Text;

            DataTable dt = obj.SearchStyle(cat, field, drp);
            if (!dt.Rows.Count.Equals(0))
            {
                searchData.Visible = true;
                Error.Visible = false;

            }
            else
            {
                Error.Visible = true;
                searchData.Visible = false;

            }
            rpt_Style.DataSource = dt;
            rpt_Style.DataBind();
            btnShowAll.Visible = false;
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            filterHideShow.Visible = true;
            searchLabel.Visible = true;
            showCat.Visible = false;
            drp_itemCategorylbl.InnerText = drp_itemCategory.SelectedItem.Text;
            drp_catTwolbl.InnerText = drp_catTwo.SelectedItem.Text;
            drp_catThreelbl.InnerText = drp_catThree.SelectedItem.Text;
            drp_catFourlbl.InnerText = drp_catFour.SelectedItem.Text;
            drp_catFivelbl.InnerText = drp_catFive.SelectedItem.Text;
            drpStyleDisplay.InnerText = drpStyles.Text;
            btnShowAll.Visible = false;
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void rpt_DataField_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            string IsAssignedDataField = ((DataRowView)e.Item.DataItem)["IsAssigned"].ToString();
            HtmlTableRow tb1 = (HtmlTableRow)e.Item.FindControl("datafieldHideShow");
            TextBox controlSearch = (TextBox)e.Item.FindControl("controlSearch");
            if (IsAssignedDataField.Equals("False"))
            {
                tb1.Visible = false;
            }
           
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void bindDropDown()
    {
        try
        {
            styleCls obj = new styleCls();
            DataTable dt = obj.getDropDown();
            rpt_dropdown.DataSource = dt;
            rpt_dropdown.DataBind();
        }
        catch (Exception ex)
        {

            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void rpt_dropdown_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            string IsAssignedDataField = ((DataRowView)e.Item.DataItem)["IsAssigned"].ToString();
            DropDownList drpCols = (DropDownList)e.Item.FindControl("drpCols");
            HtmlTableRow tb1 = (HtmlTableRow)e.Item.FindControl("drpHideShow");
            DataTable dt = new DataTable();
            styleCls obj = new styleCls();
            if (IsAssignedDataField.Equals("False"))
            {
                tb1.Visible = false;
            }
            else {
                string drpName = ((DataRowView)e.Item.DataItem)["checkValue"].ToString();
                if (drpName.Equals("Col1"))
                {
                    dt = obj.getTable("Column1");
                    drpCols.DataTextField = "C1Name";
                    drpCols.DataValueField = "Col1ID";
                }
                else if (drpName.Equals("Col2"))
                {
                    dt = obj.getTable("Column2");
                    drpCols.DataTextField = "C2Name";
                    drpCols.DataValueField = "Col2ID";
                }
                else if (drpName.Equals("Col3"))
                {
                    dt = obj.getTable("Column3");
                    drpCols.DataTextField = "C3Name";
                    drpCols.DataValueField = "Col3ID";
                }
                else if (drpName.Equals("Col4"))
                {
                    dt = obj.getTable("Column4");
                    drpCols.DataTextField = "C4Name";
                    drpCols.DataValueField = "Col4ID";
                }
                else if (drpName.Equals("Col5"))
                {
                    dt = obj.getTable("Column5");
                    drpCols.DataTextField = "C5Name";
                    drpCols.DataValueField = "Col5ID";
                }
                else if (drpName.Equals("Col6"))
                {
                    dt = obj.getTable("Column6");
                    drpCols.DataTextField = "C6Name";
                    drpCols.DataValueField = "Col6ID";
                }
                else if (drpName.Equals("Col7"))
                {
                    dt = obj.getTable("Column7");
                    drpCols.DataTextField = "C7Name";
                    drpCols.DataValueField = "Col7ID";
                }
                else if (drpName.Equals("Col8"))
                {
                    dt = obj.getTable("Column8");
                    drpCols.DataTextField = "C8Name";
                    drpCols.DataValueField = "Col8ID";
                }
                else if (drpName.Equals("Col9"))
                {
                    dt = obj.getTable("Column9");
                    drpCols.DataTextField = "C9Name";
                    drpCols.DataValueField = "Col9ID";
                }
                else if (drpName.Equals("Col10"))
                {
                    dt = obj.getTable("Column10");
                    drpCols.DataTextField = "C10Name";
                    drpCols.DataValueField = "Col10ID";
                }
                else if (drpName.Equals("Col11"))
                {
                    dt = obj.getTable("Column11");
                    drpCols.DataTextField = "C11Name";
                    drpCols.DataValueField = "Col11ID";
                }
                else if (drpName.Equals("Col12"))
                {
                    dt = obj.getTable("Column12");
                    drpCols.DataTextField = "C12Name";
                    drpCols.DataValueField = "Col12ID";
                }
                else if (drpName.Equals("Col13"))
                {
                    dt = obj.getTable("Column13");
                    drpCols.DataTextField = "C13Name";
                    drpCols.DataValueField = "Col13ID";
                }
                else if (drpName.Equals("Col14"))
                {
                    dt = obj.getTable("Column14");
                    drpCols.DataTextField = "C14Name";
                    drpCols.DataValueField = "Col14ID";
                }
                else if (drpName.Equals("Col15"))
                {
                    dt = obj.getTable("Column15");
                    drpCols.DataTextField = "C15Name";
                    drpCols.DataValueField = "Col15ID";
                }
                else if (drpName.Equals("Col16"))
                {
                    dt = obj.getTable("Column16");
                    drpCols.DataTextField = "C16Name";
                    drpCols.DataValueField = "Col16ID";
                }
                else if (drpName.Equals("Col17"))
                {
                    dt = obj.getTable("Column17");
                    drpCols.DataTextField = "C17Name";
                    drpCols.DataValueField = "Col17ID";
                }
                else if (drpName.Equals("Col18"))
                {
                    dt = obj.getTable("Column18");
                    drpCols.DataTextField = "C18Name";
                    drpCols.DataValueField = "Col18ID";
                }
                else if (drpName.Equals("Col19"))
                {
                    dt = obj.getTable("Column19");
                    drpCols.DataTextField = "C19Name";
                    drpCols.DataValueField = "Col19ID";
                }
                else if (drpName.Equals("Col20"))
                {
                    dt = obj.getTable("Column20");
                    drpCols.DataTextField = "C20Name";
                    drpCols.DataValueField = "Col20ID";
                }
                drpCols.DataSource = dt;
                drpCols.DataBind();
                drpCols.Items.Insert(0, new ListItem("---- Show All ----", "-1"));
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void searchbyFilter_Click(object sender, EventArgs e)
    {
        try
        {
            styleCls obj = new styleCls();
            DataTable cat = new DataTable();
            cat.Columns.Add("Column");
            cat.Columns.Add("Search");
            if (!drp_itemCategory.SelectedValue.Equals("-1"))
            {
                cat.Rows.Add("ItemCatID", drp_itemCategory.SelectedValue);
            }
            if (!drp_catTwo.SelectedValue.Equals("-1"))
            {
                cat.Rows.Add("Cat2ID", drp_catTwo.SelectedValue);
            }
            if (!drp_catThree.SelectedValue.Equals("-1"))
            {
                cat.Rows.Add("Cat3ID", drp_catThree.SelectedValue);
            }
            if (!drp_catFour.SelectedValue.Equals("-1"))
            {
                cat.Rows.Add("Cat4ID", drp_catFour.SelectedValue);
            }
            if (!drp_catFive.SelectedValue.Equals("-1"))
            {
                cat.Rows.Add("Cat5ID", drp_catFive.SelectedValue);
            }

            DataTable field = new DataTable();
            field.Columns.Add("Column");
            field.Columns.Add("Search");
            if (controlNameTitle.Checked)
            {
                field.Rows.Add(controlNameTitle.Attributes["Value"], titleSearch.Text);
            }
            if (!drpStyles.Equals(""))
            {
                field.Rows.Add("StyleCode", drpStyles.Text);
            }
            foreach (RepeaterItem itemEquipment in rpt_DataField.Items)
            {
                Label IsAssignedDataField = (Label)itemEquipment.FindControl("IsAssignedDataField");
                
                if (IsAssignedDataField.Text.Equals("True"))
                {
                    TextBox controlSearch = (TextBox)itemEquipment.FindControl("controlSearch");
                    CheckBox controlName = (CheckBox)itemEquipment.FindControl("controlName");
                    if (controlName.Checked && !controlSearch.Text.Equals(""))
                    {
                        field.Rows.Add(controlName.Attributes["Value"], controlSearch.Text);
                        
                    }
                }
            
            }

            DataTable drp = new DataTable();
            drp.Columns.Add("Column");
            drp.Columns.Add("Search");
            foreach (RepeaterItem itemEquipment in rpt_dropdown.Items)
            {
                Label IsAssigneddrp = (Label)itemEquipment.FindControl("IsAssigneddrp");

                if (IsAssigneddrp.Text.Equals("True"))
                {
                    DropDownList drpCols = (DropDownList)itemEquipment.FindControl("drpCols");
                    CheckBox drpName = (CheckBox)itemEquipment.FindControl("drpName");
                    if (drpName.Checked && !drpCols.SelectedValue.Equals("-1"))
                    {
                        drp.Rows.Add(drpName.Attributes["Value"], drpCols.SelectedValue);

                    }
                }

            }
            DataTable dt = obj.SearchStyle(cat, field, drp);
            if (!dt.Rows.Count.Equals(0))
            {
                searchData.Visible = true;
                Error.Visible = false;
                

            }
            else {
                Error.Visible = true;
                searchData.Visible = false;
                
            }
            rpt_Style.DataSource = dt;
            rpt_Style.DataBind();
        }
        catch (Exception ex)
        {

            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void btnRFL_Click(object sender, EventArgs e)
    {
        try
        {
            utilityCls obj = new utilityCls();
            DataTable dt = obj.getTableColwithID("roles", "roleId", Session["userrole"].ToString(), "addRFLNR");
            if (dt.Rows[0]["addRFLNR"].Equals("False"))
            {
                Response.Redirect("accessDenied.aspx", true);

            }
            else
            {
                //piecePerPacketRFL.Text = string.Empty;
                totalBarcodeRFL.Text = string.Empty;
                isSampleRFL.SelectedValue = "0";
                lblstyleIDHidden.Text = string.Empty;
                purchaseRfl.Text = string.Empty;
                LinkButton btn = ((LinkButton)(sender));
                RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
                Label title = (Label)rp1.FindControl("title");
                lblTitle.Text = title.Text;
                Label StyleID = (Label)rp1.FindControl("StyleID");
                lblstyleIDHidden.Text = StyleID.Text;
                Label mrp = (Label)rp1.FindControl("mrp");
                mrpRfl.Text = mrp.Text;
                rflMfgDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                 // bind Lot
                 lotCls lObj = new lotCls();
                DataTable lot = lObj.getActiveLot();
                lot_RPT.DataSource = lot;
                lot_RPT.DataBind();

                string lotId = string.Empty;
                int lotPiece = 0;
                if (!lot.Rows.Count.Equals(0))
                {
                    foreach (RepeaterItem itemEquipment in lot_RPT.Items)
                    {
                        RadioButton lotRadio = (RadioButton)itemEquipment.FindControl("lotRadio");
                        Label lotLblHdn = (Label)itemEquipment.FindControl("lotLblHdn");
                        Label lotPieceHdn = (Label)itemEquipment.FindControl("lotPiece");
                        lotId = lotLblHdn.Text;
                        lotPiece = Convert.ToInt32(lotPieceHdn.Text);
                        lotRadio.Checked = true;
                        break;
                    }

                    // get pieces count
                    setRFLLotPieces(lotId, lotPiece);                    
                }

                // bind Sizes
                sizeCls sObj = new sizeCls();
                styleCls uObj = new styleCls();
                DataTable size = uObj.getTable("Size");
                //DataTable size = sObj.getSizeByStyle(StyleID.Text);
                size_Rpt.DataSource = size;
                size_Rpt.DataBind();

                if (lot.Rows.Count.Equals(0))
                {
                    lotTitle.Text = title.Text;
                    ModalPopupExtender2.Show();
                }
                else if (size.Rows.Count.Equals(0))
                {
                    lblSizeTitle.Text = title.Text;
                    ModalPopupExtender3.Show();
                }
                else
                {
                    rflError.Text = string.Empty;
                    rflError.Visible = false;
                    ModalPopupExtender1.Show();
                }

            }


        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void btnNR_Click(object sender, EventArgs e)
    {
        try
        {
            utilityCls obj = new utilityCls();
            DataTable dt = obj.getTableColwithID("roles", "roleId", Session["userrole"].ToString(), "addRFLNR");
            if (dt.Rows[0]["addRFLNR"].Equals("False"))
            {
                Response.Redirect("accessDenied.aspx", true);

            }
            else
            {
                //piecePerPacketNRR.Text = string.Empty;
                totalBarcodeNRR.Text = string.Empty;
                isSampleNRR.SelectedValue = "0";
                lblSTtyleNRId.Text = string.Empty;
                purchaseNR.Text = string.Empty;
                LinkButton btn = ((LinkButton)(sender));
                RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
                Label title = (Label)rp1.FindControl("title");
                lblNRTitle.Text = title.Text;
                Label StyleID = (Label)rp1.FindControl("StyleID");
                lblSTtyleNRId.Text = StyleID.Text;
                Label mrp = (Label)rp1.FindControl("mrp");
                mrpNR.Text = mrp.Text;
                nrMfgDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                // bind Lot
                lotCls lObj = new lotCls();
                DataTable lot = lObj.getActiveLot();
                lot_RPTNR.DataSource = lot;
                lot_RPTNR.DataBind();
                string lotId = string.Empty;
                int lotPiece = 0;
                if (!lot.Rows.Count.Equals(0))
                {
                    foreach (RepeaterItem itemEquipment in lot_RPTNR.Items)
                    {
                        RadioButton lotRadio = (RadioButton)itemEquipment.FindControl("lotRadio");
                        Label lotLblHdn = (Label)itemEquipment.FindControl("lotLblHdn");
                        Label lotPieceHdn = (Label)itemEquipment.FindControl("lotPiece");
                        lotId = lotLblHdn.Text;
                        lotPiece = Convert.ToInt32(lotPieceHdn.Text);
                        lotRadio.Checked = true;
                        break;
                    }

                    setNRLotPieces(lotId, lotPiece);
                }

                // bind Sizes
                sizeCls sObj = new sizeCls();
                //DataTable size = sObj.getSizeByStyle(StyleID.Text);
                styleCls uObj = new styleCls();
                DataTable size = uObj.getTable("Size");
                size_RptNR.DataSource = size;
                size_RptNR.DataBind();

                if (lot.Rows.Count.Equals(0))
                {
                    lotTitle.Text = title.Text;
                    ModalPopupExtender2.Show();
                }
                else if (size.Rows.Count.Equals(0))
                {
                    lblSizeTitle.Text = title.Text;
                    ModalPopupExtender3.Show();
                }
                else
                {
                    ModalPopupExtender4.Show();
                }
            }



        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void SaveRFL_Click(object sender, EventArgs e)
    {
        try
        {            
            string size = string.Empty;string sizeText = string.Empty;
            string rflErrorText = string.Empty;
            foreach (RepeaterItem itemEquipment in size_Rpt.Items)
            {
                RadioButton sizeRadioBtn = (RadioButton)itemEquipment.FindControl("sizeRadio");
                Label SizeID = (Label)itemEquipment.FindControl("sizeLblHdn");
                if (sizeRadioBtn.Checked)
                {
                    size = SizeID.Text;
                    //sizeText = sizeRadioBtn.SelectedItem.Value.ToString();
                    break;
                }
            }

            if (size.Equals(""))
            {
                rflErrorText += "Please Select Piece";
            }

            string lot = string.Empty;
            foreach (RepeaterItem itemEquipment in lot_RPT.Items)
            {
                RadioButton lotRadioBtn = (RadioButton)itemEquipment.FindControl("lotRadio");
                Label lotLblHdn = (Label)itemEquipment.FindControl("lotLblHdn");
                if (lotRadioBtn.Checked)
                {
                    lot = lotLblHdn.Text;
                    break;
                }
            }

            if (lot.Equals(""))
            {
                rflErrorText += "</br>Please Select Lot";
            }

            if (rflMfgDate.Text.Equals(""))
            {
                rflErrorText += "</br>Please Select MFG Date";
            }

            if (purchaseRfl.Text.Equals(""))
            {
                rflErrorText += "</br>Please Enter Purchase Rate";
            }

            /*if (piecePerPacketRFL.Text.Equals(""))
            {
                rflErrorText += "</br>Please Enter Piece Per Packet";
            }*/

            if (totalBarcodeRFL.Text.Equals(""))
            {
                rflErrorText += "</br>Please Enter Total No. of Barcodes";
            }

            utilityCls uObj = new utilityCls();
            DataTable pieceDt = uObj.getTableColwithID("Size","SizeId",size,"Size1");

            if(pieceDt.Rows.Count.Equals(0))
            {
                rflErrorText += "</br>Please Select Piece/Packet";
            }
            else
            {

                // check for pieces entering in lot 
                if (!pieceDt.Rows[0]["Size1"].ToString().Equals("") && !totalBarcodeRFL.Text.Equals(""))
                {
                    if ((Convert.ToInt32(pieceDt.Rows[0]["Size1"].ToString()) * Convert.ToInt32(totalBarcodeRFL.Text)) > Convert.ToInt32(avlPiece.Text))
                    {
                        rflErrorText += "</br>Total No. of pieces are more than available";
                    }
                }
            }

            
           
            if (!rflErrorText.Equals(""))
            {
                rflError.Text = rflErrorText;
                rflError.Visible = true;
                ModalPopupExtender1.Show();
            }
            else {
                styleCls sObj = new styleCls();
                int success = sObj.addRflNR(size, lot, lblstyleIDHidden.Text, "RFL", mrpRfl.Text,oldBarcode.Text,rflMfgDate.Text, pieceDt.Rows[0]["Size1"].ToString(), totalBarcodeRFL.Text,isSampleRFL.SelectedValue, lotPieces.Text, purchaseRfl.Text);
                if (!success.Equals(-1))
                {
                    successfailure.Text = "Succesfully added to Inventory";
                }
                else
                { successfailure.Text = " Adding to Inventory FAILED"; }
                successfailure.Visible = true;
                lblstyleIDHidden.Text = string.Empty;
                lblTitle.Text = string.Empty;
            }
            

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void lotRadio_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            //Uncheck all RadioButtons of the rptCustomer
            foreach (RepeaterItem item in lot_RPT.Items)
            {
                ((RadioButton)item.FindControl("lotRadio")).Checked = false;
            }

            //Set the new selected row
            RadioButton rb = (RadioButton)sender;
            RepeaterItem row = (RepeaterItem)rb.NamingContainer;
            ((RadioButton)row.FindControl("lotRadio")).Checked = true;

            // set the available pieces
            int lotPiece = 0;
            string lotId = string.Empty;
            Label lotLblHdn = (Label)row.FindControl("lotLblHdn");
            Label lotPieceHdn = (Label)row.FindControl("lotPiece");
            lotId = lotLblHdn.Text;
            lotPiece = Convert.ToInt32(lotPieceHdn.Text);
            setRFLLotPieces(lotId,lotPiece);
            ModalPopupExtender1.Show();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    private void setRFLLotPieces(string lotId, int lotPiece)
    {
        try
        {
            lotPieces.Text = string.Empty;
            barcodePiece.Text = string.Empty;
            avlPiece.Text = string.Empty;
            isSampleRFL.SelectedValue = "0";
            // get pieces count
            styleCls stObj = new styleCls();
            DataTable pieceCountDt = stObj.getPieceCountByLot(lotId);
            int pieceCount = 0;
            if (!pieceCountDt.Rows.Count.Equals(0))
            {
                pieceCount = Convert.ToInt32(pieceCountDt.Rows[0]["pieces"]);
            }

            int pieceAvailable = lotPiece - pieceCount;

            lotPieces.Text = lotPiece.ToString();
            barcodePiece.Text = pieceCount.ToString();
            avlPiece.Text = pieceAvailable.ToString();

            if(pieceCount.Equals(0))
            {
                isSampleRFL.SelectedValue = "1";
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    private void setNRLotPieces(string lotId, int lotPiece)
    {
        try
        {
            lotPiecesN.Text = string.Empty;
            barcodePieceN.Text = string.Empty;
            avlPieceN.Text = string.Empty;

            // get pieces count
            styleCls stObj = new styleCls();
            DataTable pieceCountDt = stObj.getPieceCountByLot(lotId);
            int pieceCount = 0;
            if (!pieceCountDt.Rows.Count.Equals(0))
            {
                pieceCount = Convert.ToInt32(pieceCountDt.Rows[0]["pieces"]);
            }

            int pieceAvailable = lotPiece - pieceCount;

            lotPiecesN.Text = lotPiece.ToString();
            barcodePieceN.Text = pieceCount.ToString();
            avlPieceN.Text = pieceAvailable.ToString();

            if (pieceCount.Equals(0))
            {
                isSampleNRR.SelectedValue = "1";
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void lotRadio_CheckedChangedNR(object sender, EventArgs e)
    {
        try
        {
            //Uncheck all RadioButtons of the rptCustomer
            foreach (RepeaterItem item in lot_RPTNR.Items)
            {
                ((RadioButton)item.FindControl("lotRadio")).Checked = false;
            }

            //Set the new selected row
            RadioButton rb = (RadioButton)sender;
            RepeaterItem row = (RepeaterItem)rb.NamingContainer;
            ((RadioButton)row.FindControl("lotRadio")).Checked = true;

            // set the available pieces
            int lotPiece = 0;
            string lotId = string.Empty;
            Label lotLblHdn = (Label)row.FindControl("lotLblHdn");
            Label lotPieceHdn = (Label)row.FindControl("lotPiece");
            lotId = lotLblHdn.Text;
            lotPiece = Convert.ToInt32(lotPieceHdn.Text);
            setNRLotPieces(lotId, lotPiece);

            ModalPopupExtender4.Show();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void saveNR_Click(object sender, EventArgs e)
    {
        try
        {
            string size = string.Empty;
            string rflErrorText = string.Empty;
            foreach (RepeaterItem itemEquipment in size_RptNR.Items)
            {
                RadioButton sizeRadioBtn = (RadioButton)itemEquipment.FindControl("sizeRadio");
                Label SizeID = (Label)itemEquipment.FindControl("sizeLblHdn");
                if (sizeRadioBtn.Checked)
                {
                    size = SizeID.Text;
                    break;
                }
            }

            if (size.Equals(""))
            {
                rflErrorText += "Please Select Size";
            }
            string lot = string.Empty;
            foreach (RepeaterItem itemEquipment in lot_RPTNR.Items)
            {
                RadioButton lotRadioBtn = (RadioButton)itemEquipment.FindControl("lotRadio");
                Label lotLblHdn = (Label)itemEquipment.FindControl("lotLblHdn");
                if (lotRadioBtn.Checked)
                {
                    lot = lotLblHdn.Text;
                    break;
                }
            }
            if (lot.Equals(""))
            {
                rflErrorText += "</br>Please Select Lot";
            }
            if (nrMfgDate.Text.Equals(""))
            {
                rflErrorText += "</br>Please Select MFG Date";
            }
            if (purchaseNR.Text.Equals(""))
            {
                rflErrorText += "</br>Please Enter Purchase Rate";
            }
            utilityCls uObj = new utilityCls();
            DataTable pieceDt = uObj.getTableColwithID("Size", "SizeId", size, "Size1");

            if (pieceDt.Rows.Count.Equals(0))
            {
                rflErrorText += "</br>Please Select Piece/Packet";
            }
            else
            {

                // check for pieces entering in lot 
                if (!pieceDt.Rows[0]["Size1"].ToString().Equals("") && !totalBarcodeRFL.Text.Equals(""))
                {
                    if ((Convert.ToInt32(pieceDt.Rows[0]["Size1"].ToString()) * Convert.ToInt32(totalBarcodeRFL.Text)) > Convert.ToInt32(avlPiece.Text))
                    {
                        rflErrorText += "</br>Total No. of pieces are more than available";
                    }
                }
            }
            if (!rflErrorText.Equals(""))
            {
                nrError.Text = rflErrorText;
                nrError.Visible = true;
                ModalPopupExtender4.Show();
            }
            else
            {
                styleCls sObj = new styleCls();
                int success = sObj.addRflNR(size, lot, lblSTtyleNRId.Text, ddlReason.SelectedValue, mrpNR.Text, oldBarcodeNR.Text,nrMfgDate.Text, pieceDt.Rows[0]["Size1"].ToString(), totalBarcodeNRR.Text, isSampleNRR.SelectedValue, lotPiecesN.Text, purchaseNR.Text);
                // int success = sObj.addRflNR(size, lot, lblstyleIDHidden.Text, ddlReason.SelectedValue, mrpNR.Text);
                if (!success.Equals(-1))
                {
                    successfailure.Text = "Succesfully added to Inventory";
                }
                else
                { successfailure.Text = " Adding to Inventory FAILED"; }
                successfailure.Visible = true;
                lblSTtyleNRId.Text = string.Empty;
                lblNRTitle.Text = string.Empty;
            }



        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void sizeRadio_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            //Uncheck all RadioButtons of the rptCustomer
            foreach (RepeaterItem item in size_Rpt.Items)
            {
                ((RadioButton)item.FindControl("sizeRadio")).Checked = false;
            }

            //Set the new selected row
            RadioButton rb = (RadioButton)sender;
            RepeaterItem row = (RepeaterItem)rb.NamingContainer;
            ((RadioButton)row.FindControl("sizeRadio")).Checked = true;
            
            ModalPopupExtender1.Show();
            //Label scrollDownLotRpt = (Label)FindControl("scrollDownLotRpt");
            //scrollDownLotRpt.Focus();
            /*ScriptManager.GetCurrent(this).SetFocus(scrollDownLotRpt);
            ScriptManager.RegisterStartupScript(this,
                                                this.GetType(),
                                                "FocusScript",
                                                "setTimeout(function(){$get('" + scrollDownLotRpt.ClientID + "').focus();}, 100);",
                                                true);*/
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void sizeRadio_CheckedChangedNR(object sender, EventArgs e)
    {
        try
        {
            //Uncheck all RadioButtons of the rptCustomer
            foreach (RepeaterItem item in size_Rpt.Items)
            {
                ((RadioButton)item.FindControl("sizeRadio")).Checked = false;
            }

            //Set the new selected row
            RadioButton rb = (RadioButton)sender;
            RepeaterItem row = (RepeaterItem)rb.NamingContainer;
            ((RadioButton)row.FindControl("sizeRadio")).Checked = true;
            ModalPopupExtender4.Show();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        try {
            Response.Redirect("ItemStyleSearchAdd.aspx",true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void addNewStyle_Click(object sender, EventArgs e)
    {
        try
        {
            Session["EditStyleID"] = "0";
            Response.Redirect("addStyle.aspx", true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void btnItemStyleDetails_Click(object sender, EventArgs e)
    {
        try
        {
            string imagelink = "http://mnmimages.dzvdesk.com/Uploads/";
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            Label title = (Label)rp1.FindControl("title");
            Label stylecode = (Label)rp1.FindControl("stylecode");
            lblItemNamedets.Text = title.Text;
            Label StyleID = (Label)rp1.FindControl("StyleID");
            lblDetsStyleID.Text = StyleID.Text;
            lblViewEANStyleid.Text = StyleID.Text;
            lbltitileviewEAN.Text = title.Text;
            lblstylecodeviewEAN.Text = stylecode.Text;
            styleCls obj = new styleCls();
            DataTable catName = obj.getCatName();
            if (!catName.Rows.Count.Equals(0))
            {
                DetItemCatname2.Text = catName.Rows[0]["SettingName"].ToString();
                DetItemCatname3.Text = catName.Rows[1]["SettingName"].ToString();
                DetItemCatname4.Text = catName.Rows[2]["SettingName"].ToString();
                DetItemCatname5.Text = catName.Rows[3]["SettingName"].ToString();
            }
            else
            {
                DetItemCatname2.Text = string.Empty;
                DetItemCatname3.Text = string.Empty;
                DetItemCatname4.Text = string.Empty;
                DetItemCatname5.Text = string.Empty;
            }

            DataTable item = obj.getItemStyleByID(StyleID.Text);
            if (!item.Rows.Count.Equals(0))
            {
                DetItemCatVal1.Text = item.Rows[0]["ItemCategory"].ToString();
                DetItemCatVal2.Text = item.Rows[0]["C2Name"].ToString();
                DetItemCatVal3.Text = item.Rows[0]["C3Name"].ToString();
                DetItemCatVal4.Text = item.Rows[0]["C4Name"].ToString();
                DetItemCatVal5.Text = item.Rows[0]["C5Name"].ToString();
                detMrp.Text = item.Rows[0]["mrp"].ToString();
                if (!item.Rows[0]["image1"].ToString().Equals(""))
                {
                    image1Display.Visible = true;
                    image1Display.ImageUrl = imagelink + item.Rows[0]["image1"].ToString();
                }
                else
                {
                    image1Display.Visible = false;
                    image1Display.ImageUrl = string.Empty;
                }
                if (!item.Rows[0]["image2"].ToString().Equals(""))
                {
                    image2Display.Visible = true;
                    image2Display.ImageUrl = imagelink + item.Rows[0]["image2"].ToString();
                }
                else
                {
                    image2Display.Visible = false;
                    image2Display.ImageUrl = string.Empty;
                }
                if (!item.Rows[0]["image3"].ToString().Equals(""))
                {
                    image3Display.Visible = true;
                    image3Display.ImageUrl = imagelink + item.Rows[0]["image3"].ToString();
                }
                else
                {
                    image3Display.Visible = false;
                    image3Display.ImageUrl = string.Empty;
                }
                if (!item.Rows[0]["image4"].ToString().Equals(""))
                {
                    image4Display.Visible = true;
                    image4Display.ImageUrl = imagelink + item.Rows[0]["image4"].ToString();
                }
                else
                {
                    image4Display.Visible = false;
                    image4Display.ImageUrl = string.Empty;
                }
                if (!item.Rows[0]["image5"].ToString().Equals(""))
                {
                    image5Display.Visible = true;
                    image5Display.ImageUrl = imagelink + item.Rows[0]["image5"].ToString();
                }
                else
                {
                    image5Display.Visible = false;
                    image5Display.ImageUrl = string.Empty;
                }
                if (!item.Rows[0]["image6"].ToString().Equals(""))
                {
                    image6Display.Visible = true;
                    image6Display.ImageUrl = imagelink + item.Rows[0]["image6"].ToString();
                }
                else
                {
                    image6Display.Visible = false;
                    image6Display.ImageUrl = string.Empty;
                }
            }
            else
            {
                DetItemCatVal1.Text = string.Empty;
                DetItemCatVal2.Text = string.Empty;
                DetItemCatVal3.Text = string.Empty;
                DetItemCatVal4.Text = string.Empty;
                DetItemCatVal5.Text = string.Empty;
                detMrp.Text = string.Empty;
                image1Display.Visible = false;
                image1Display.ImageUrl = string.Empty;
                image2Display.Visible = false;
                image2Display.ImageUrl = string.Empty;
                image3Display.Visible = false;
                image3Display.ImageUrl = string.Empty;
                image4Display.Visible = false;
                image4Display.ImageUrl = string.Empty;
                image5Display.Visible = false;
                image5Display.ImageUrl = string.Empty;
                image6Display.Visible = false;
                image6Display.ImageUrl = string.Empty;
            }

            DataTable dt = obj.getDataFieldNameView(StyleID.Text);
            rptDataFieldDets.DataSource = dt;
            rptDataFieldDets.DataBind();

            DataTable drop = obj.getDropDown();
            rptDrop.DataSource = drop;
            rptDrop.DataBind();

            
            // image slider
            ModalPopupExtender7.Show();
            //DataTable dt = 
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void rptDrop_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            styleCls obj = new styleCls();
            Label lblDetsStyleID = (Label)PanelDets.FindControl("lblDetsStyleID");
            DataTable item = obj.getItemStyleByID(lblDetsStyleID.Text);
            string IsAssignedDataField = ((DataRowView)e.Item.DataItem)["IsAssigned"].ToString();
            Label colName = (Label)e.Item.FindControl("colName");
            HtmlTableRow tb1 = (HtmlTableRow)e.Item.FindControl("drpHideShow");
            DataTable dt = new DataTable();
            
            if (IsAssignedDataField.Equals("False"))
            {
                tb1.Visible = false;
            }
            else
            {
                string drpName = ((DataRowView)e.Item.DataItem)["checkValue"].ToString();
                if (drpName.Equals("Col1"))
                {
                    if (item.Rows[0]["Col1"].ToString().Equals("") || item.Rows[0]["Col1"].ToString().Equals("-1"))
                    {
                        tb1.Visible = false;
                    }
                    else
                    {
                        dt = obj.getTablewithID("Column1", "Col1ID",Convert.ToInt32(item.Rows[0]["Col1"].ToString()));
                        colName.Text = dt.Rows[0]["C1Name"].ToString();
                    }
                    
                    
                    
                }
                else if (drpName.Equals("Col2"))
                {
                    if (item.Rows[0]["Col2"].ToString().Equals("") || item.Rows[0]["Col2"].ToString().Equals("-1"))
                    {
                        tb1.Visible = false;
                    }
                    else
                    {
                        dt = obj.getTablewithID("Column2", "Col2ID", Convert.ToInt32(item.Rows[0]["Col2"].ToString()));
                        colName.Text = dt.Rows[0]["C2Name"].ToString();
                    }
                }
                else if (drpName.Equals("Col3"))
                {
                    if (item.Rows[0]["Col3"].ToString().Equals("") || item.Rows[0]["Col3"].ToString().Equals("-1"))
                    {
                        tb1.Visible = false;
                    }
                    else
                    {
                        dt = obj.getTablewithID("Column3", "Col3ID", Convert.ToInt32(item.Rows[0]["Col3"].ToString()));
                        colName.Text = dt.Rows[0]["C3Name"].ToString();
                      
                    }
                }
                else if (drpName.Equals("Col4"))
                {
                    if (item.Rows[0]["Col4"].ToString().Equals("") || item.Rows[0]["Col4"].ToString().Equals("-1"))
                    {
                        tb1.Visible = false;
                    }
                    else
                    {
                        dt = obj.getTablewithID("Column4", "Col4ID", Convert.ToInt32(item.Rows[0]["Col4"].ToString()));
                        colName.Text = dt.Rows[0]["C4Name"].ToString();
                    }
                }
                else if (drpName.Equals("Col5"))
                {
                    if (item.Rows[0]["Col5"].ToString().Equals("") || item.Rows[0]["Col5"].ToString().Equals("-1"))
                    {
                        tb1.Visible = false;
                    }
                    else
                    {
                        dt = obj.getTablewithID("Column5", "Col5ID", Convert.ToInt32(item.Rows[0]["Col5"].ToString()));
                        colName.Text = dt.Rows[0]["C5Name"].ToString();
                    }
                }
                else if (drpName.Equals("Col6"))
                {
                    if (item.Rows[0]["Col6"].ToString().Equals("") || item.Rows[0]["Col6"].ToString().Equals("-1"))
                    {
                        tb1.Visible = false;
                    }
                    else
                    {
                        dt = obj.getTablewithID("Column6", "Col6ID", Convert.ToInt32(item.Rows[0]["Col6"].ToString()));
                        colName.Text = dt.Rows[0]["C6Name"].ToString();
                    }
                }
                else if (drpName.Equals("Col7"))
                {
                    if (item.Rows[0]["Col7"].ToString().Equals("") || item.Rows[0]["Col7"].ToString().Equals("-1"))
                    {
                        tb1.Visible = false;
                    }
                    else
                    {
                        dt = obj.getTablewithID("Column7", "Col7ID", Convert.ToInt32(item.Rows[0]["Col7"].ToString()));
                        colName.Text = dt.Rows[0]["C7Name"].ToString();
                    }
                }
                else if (drpName.Equals("Col8"))
                {
                    if (item.Rows[0]["Col8"].ToString().Equals("") || item.Rows[0]["Col8"].ToString().Equals("-1"))
                    {
                        tb1.Visible = false;
                    }
                    else
                    {
                        dt = obj.getTablewithID("Column8", "Col8ID", Convert.ToInt32(item.Rows[0]["Col8"].ToString()));
                        colName.Text = dt.Rows[0]["C8Name"].ToString();
                    }
                }
                else if (drpName.Equals("Col9"))
                {
                    if (item.Rows[0]["Col9"].ToString().Equals("") || item.Rows[0]["Col9"].ToString().Equals("-1"))
                    {
                        tb1.Visible = false;
                    }
                    else
                    {
                        dt = obj.getTablewithID("Column9", "Col9ID", Convert.ToInt32(item.Rows[0]["Col9"].ToString()));
                        colName.Text = dt.Rows[0]["C9Name"].ToString();
                    }
                }
                else if (drpName.Equals("Col10"))
                {
                    if (item.Rows[0]["Col10"].ToString().Equals("") || item.Rows[0]["Col10"].ToString().Equals("-1"))
                    {
                        tb1.Visible = false;
                    }
                    else
                    {
                        dt = obj.getTablewithID("Column10", "Col10ID", Convert.ToInt32(item.Rows[0]["Col10"].ToString()));
                        colName.Text = dt.Rows[0]["C10Name"].ToString();
                    }
                }
                else if (drpName.Equals("Col11"))
                {
                    if (item.Rows[0]["Col11"].ToString().Equals("") || item.Rows[0]["Col11"].ToString().Equals("-1"))
                    {
                        tb1.Visible = false;
                    }
                    else
                    {
                        dt = obj.getTablewithID("Column11", "Col11ID", Convert.ToInt32(item.Rows[0]["Col11"].ToString()));
                        colName.Text = dt.Rows[0]["C11Name"].ToString();
                    }
                }
                else if (drpName.Equals("Col12"))
                {
                    if (item.Rows[0]["Col12"].ToString().Equals("") || item.Rows[0]["Col12"].ToString().Equals("-1"))
                    {
                        tb1.Visible = false;
                    }
                    else
                    {
                        dt = obj.getTablewithID("Column12", "Col12ID", Convert.ToInt32(item.Rows[0]["Col12"].ToString()));
                        colName.Text = dt.Rows[0]["C12Name"].ToString();
                    }
                }
                else if (drpName.Equals("Col13"))
                {
                    if (item.Rows[0]["Col13"].ToString().Equals("") || item.Rows[0]["Col13"].ToString().Equals("-1"))
                    {
                        tb1.Visible = false;
                    }
                    else
                    {
                        dt = obj.getTablewithID("Column13", "Col13ID", Convert.ToInt32(item.Rows[0]["Col13"].ToString()));
                        colName.Text = dt.Rows[0]["C13Name"].ToString();
                    }
                }
                else if (drpName.Equals("Col14"))
                {
                    if (item.Rows[0]["Col14"].ToString().Equals("") || item.Rows[0]["Col14"].ToString().Equals("-1"))
                    {
                        tb1.Visible = false;
                    }
                    else
                    {
                        dt = obj.getTablewithID("Column14", "Col14ID", Convert.ToInt32(item.Rows[0]["Col14"].ToString()));
                        colName.Text = dt.Rows[0]["C14Name"].ToString();
                    }
                }
                else if (drpName.Equals("Col15"))
                {
                    if (item.Rows[0]["Col15"].ToString().Equals("") || item.Rows[0]["Col15"].ToString().Equals("-1"))
                    {
                        tb1.Visible = false;
                    }
                    else
                    {
                        dt = obj.getTablewithID("Column15", "Col15ID", Convert.ToInt32(item.Rows[0]["Col15"].ToString()));
                        colName.Text = dt.Rows[0]["C15Name"].ToString();
                    }
                }
                else if (drpName.Equals("Col16"))
                {
                    if (item.Rows[0]["Col16"].ToString().Equals("") || item.Rows[0]["Col16"].ToString().Equals("-1"))
                    {
                        tb1.Visible = false;
                    }
                    else
                    {
                        dt = obj.getTablewithID("Column16", "Col16ID", Convert.ToInt32(item.Rows[0]["Col16"].ToString()));
                        colName.Text = dt.Rows[0]["C16Name"].ToString();
                    }
                }
                else if (drpName.Equals("Col17"))
                {
                    if (item.Rows[0]["Col17"].ToString().Equals("") || item.Rows[0]["Col17"].ToString().Equals("-1"))
                    {
                        tb1.Visible = false;
                    }
                    else
                    {
                        dt = obj.getTablewithID("Column17", "Col17ID", Convert.ToInt32(item.Rows[0]["Col17"].ToString()));
                        colName.Text = dt.Rows[0]["C17Name"].ToString();
                    }
                }
                else if (drpName.Equals("Col18"))
                {
                    if (item.Rows[0]["Col18"].ToString().Equals("") || item.Rows[0]["Col18"].ToString().Equals("-1"))
                    {
                        tb1.Visible = false;
                    }
                    else
                    {
                        dt = obj.getTablewithID("Column18", "Col18ID", Convert.ToInt32(item.Rows[0]["Col18"].ToString()));
                        colName.Text = dt.Rows[0]["C18Name"].ToString();
                    }
                }
                else if (drpName.Equals("Col19"))
                {
                    if (item.Rows[0]["Col19"].ToString().Equals("") || item.Rows[0]["Col19"].ToString().Equals("-1"))
                    {
                        tb1.Visible = false;
                    }
                    else
                    {
                        dt = obj.getTablewithID("Column19", "Col19ID", Convert.ToInt32(item.Rows[0]["Col19"].ToString()));
                        colName.Text = dt.Rows[0]["C19Name"].ToString();
                    }
                }
                else if (drpName.Equals("Col20"))
                {
                    if (item.Rows[0]["Col20"].ToString().Equals("") || item.Rows[0]["Col20"].ToString().Equals("-1"))
                    {
                        tb1.Visible = false;
                    }
                    else
                    {
                        dt = obj.getTablewithID("Column20", "Col20ID", Convert.ToInt32(item.Rows[0]["Col20"].ToString()));
                        colName.Text = dt.Rows[0]["C20Name"].ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void btnEditItemStyle_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            Label StyleID = (Label)rp1.FindControl("StyleID");
            Session["EditStyleID"] = StyleID.Text;
            Response.Redirect("addStyle.aspx",true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void rptDataFieldDets_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            string IsAssignedDataField = ((DataRowView)e.Item.DataItem)["IsAssigned"].ToString();
            HtmlTableRow tb1 = (HtmlTableRow)e.Item.FindControl("datafieldHideShow");
            Label controlSearch = (Label)e.Item.FindControl("controlSearch");
            if (IsAssignedDataField.Equals("False"))
            {
                tb1.Visible = false;
            }
            else
            {
                if (controlSearch.Text.Equals(""))
                {
                    tb1.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void btnsaveEAN_Click(object sender, EventArgs e)
    {
        try
        {
             
            styleCls obj = new styleCls();

            DataTable dt = new DataTable();
            dt.Columns.Add("styleid");
            dt.Columns.Add("sizeid");
            dt.Columns.Add("ean");
            dt.Columns.Add("cmgfrom");
            dt.Columns.Add("itemcatid"); 
            string cmgfrom;
            foreach (RepeaterItem itemEquipment in rpt_EAN.Items)
            {
                Label sizeLblHdn = (Label)itemEquipment.FindControl("sizeLblHdn");
                TextBox txtEAN = (TextBox)itemEquipment.FindControl("txtEAN");
                Label lblhdnid = (Label)itemEquipment.FindControl("lblhdnid");
                //Label lblEANitemcatid = (Label)itemEquipment.FindControl("lblEANitemcatid");
                //DataTable table = obj.getstyleNsize(lblstyleEANid.Text, sizeLblHdn.Text);
                if (lblhdnid.Text.Equals(""))
                {
                    cmgfrom = "-1";
                }
                else
                {
                    cmgfrom = lblhdnid.Text;
                }

                dt.Rows.Add(lblstyleEANid.Text, sizeLblHdn.Text, txtEAN.Text, cmgfrom, lblEANitemcatid.Text);

            }

            styleCls ob = new styleCls();
            int result = ob.addEditEAN(dt);
            if (result==0)
            {
                dt.Clear();
                ModalPopupExtender5.Hide();
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('EAN Added Succesfully  !');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('EAN Added Failed !');", true);
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }
    }

    protected void btnEAN_Click(object sender, EventArgs e)
    {
        try
        {

            
            lblstyleEANid.Text = string.Empty;
            lblEANitemcatid.Text = string.Empty;
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            Label title = (Label)rp1.FindControl("title");
            Label lbltemcatid = (Label)rp1.FindControl("lbltemcatid");
            Label stylecode = (Label)rp1.FindControl("stylecode");
            Label StyleID = (Label)rp1.FindControl("StyleID");
            Label sizeLblHdn = (Label)rp1.FindControl("sizeLblHdn");
            //TextBox txtEAN = (TextBox)rp1.FindControl("txtEAN");
            lbleantitle.Text = title.Text;
            lbleanstylecode.Text = stylecode.Text;
            lblstyleEANid.Text = StyleID.Text;
            lblEANitemcatid.Text = lbltemcatid.Text;
            utilityCls ob = new utilityCls();
            DataTable dt = ob.getTableColwithID("ItemCategory", "ItemCategoryID", lblEANitemcatid.Text, "ItemCategory");
            lblcat.Text = dt.Rows[0]["ItemCategory"].ToString();

            styleCls sObj = new styleCls();
            DataTable size = sObj.getEAN(StyleID.Text, lblEANitemcatid.Text);
            rpt_EAN.DataSource = size;
            rpt_EAN.DataBind();
            ModalPopupExtender5.Show();


            //utilityCls obj = new utilityCls();
            //DataTable dt = obj.getTableColwithID("EAN", "styleid", lblstyleEANid.Text, "*");

            // bind Sizes



        }
        catch (Exception ex)
        {
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }
    }
       
    protected void btnviewEAN_Click(object sender, EventArgs e)
    {
        try
        {
            
            styleCls sObj = new styleCls();
            DataTable size = sObj.getEAN(lblViewEANStyleid.Text, lblEANitemcatid.Text);
            rptViewEAN.DataSource = size;
            rptViewEAN.DataBind();
            ModalPopupExtender7.Hide();
            ModalPopupExtender6.Show();
        }
        catch(Exception ex)
        {
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }
    }
}