using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;


public partial class ViewInventory : System.Web.UI.Page
{
	#region   // pagination /////////////////////////
	readonly PagedDataSource _pgsource = new PagedDataSource();
	int _firstIndex, _lastIndex;
	private int _pageSize = 5;
	private int CurrentPage
	{
		get
		{
			if (ViewState["CurrentPage"] == null)
			{
				return 0;
			}
			return ((int)ViewState["CurrentPage"]);
		}
		set
		{
			ViewState["CurrentPage"] = value;
		}
	}
	#endregion// pagination /////////////////////////

	#region   // pagination /////////////////////////
	private void HandlePaging()
	{
		var dt = new DataTable();
		dt.Columns.Add("PageIndex"); //Start from 0
		dt.Columns.Add("PageText"); //Start from 1

		_firstIndex = CurrentPage - 5;
		if (CurrentPage > 5)
			_lastIndex = CurrentPage + 5;
		else
			_lastIndex = 10;

		// Check last page is greater than total page then reduced it 
		// to total no. of page is last index
		if (_lastIndex > Convert.ToInt32(ViewState["TotalPages"]))
		{
			_lastIndex = Convert.ToInt32(ViewState["TotalPages"]);
			_firstIndex = _lastIndex - 10;
		}

		if (_firstIndex < 0)
			_firstIndex = 0;

		// Now creating page number based on above first and last page index
		for (var i = _firstIndex; i < _lastIndex; i++)
		// for (var i = _firstIndex; i < 5; i++)
		{
			var dr = dt.NewRow();
			dr[0] = i;
			dr[1] = i + 1;
			dt.Rows.Add(dr);
		}

		rptPaging.DataSource = dt;
		rptPaging.DataBind();
	}

	protected void lbFirst_Click(object sender, EventArgs e)
	{
		//if (lblcomingfrom.Text == "Search")
		//{
		//	CurrentPage = 0;
		//	search();
		//}
		//else
		//{
		CurrentPage = 0;
		BindData();
		//}

	}

	protected void lbLast_Click(object sender, EventArgs e)
	{
		//if (lblcomingfrom.Text == "Search")
		//{
		//	CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
		//	search();
		//}
		//else
		//{
		CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
		BindData();
		//}

	}
	protected void lbPrevious_Click(object sender, EventArgs e)
	{
		//if (lblcomingfrom.Text == "Search")
		//{
		//	CurrentPage -= 1;
		//	search();
		//}
		//else
		//{
		CurrentPage -= 1;
		BindData();
		//}

	}
	protected void lbNext_Click(object sender, EventArgs e)
	{
		//if (lblcomingfrom.Text == "Search")
		//{
		//	CurrentPage += 1;
		//	search();
		//}
		//else
		//{
		CurrentPage += 1;
		BindData();
		//}

	}

	protected void rptPaging_ItemCommand(object source, DataListCommandEventArgs e)
	{
		//if (lblcomingfrom.Text == "Search")
		//{
		//	if (!e.CommandName.Equals("newPage")) return;
		//	CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
		//	search();
		//}
		//else
		//{
		if (!e.CommandName.Equals("newPage")) return;
		CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
		BindData();
		//}

	}

	protected void rptPaging_ItemDataBound(object sender, DataListItemEventArgs e)
	{
		Button lnkPage = (Button)e.Item.FindControl("lbPaging");
		if (lnkPage.CommandArgument != CurrentPage.ToString()) return;
		lnkPage.Enabled = false;
		lnkPage.BackColor = System.Drawing.Color.FromName("#014d79");
		lnkPage.BorderColor = System.Drawing.Color.FromName("#014d79");
		lnkPage.CssClass = "btn btn-primary btn-flat";


		//lnkPage.Attributes.Add("class", lnkPage.Attributes["class"].ToString().Replace(" ", "btn btn-success btn-flat"));
		// lnkPage.Attributes.Add("class","btn btn-success btn-flat");

		//  lnkPage.BackColor = "#00FF00";
	}

	#endregion // pagination /////////////////////////

	protected void Page_Load(object sender, EventArgs e)
	{
		try
		{
			if (!IsPostBack)
			{
                //BindData();
                bindCatName();
                bindItemCategory();
                drp_itemCategory.Items.Insert(0, new ListItem("---- Select ----", "-1"));
                drp_catTwo.Items.Insert(0, new ListItem("---- Select ----", "-1"));
                drp_catThree.Items.Insert(0, new ListItem("---- Select ----", "-1"));
                drp_catFour.Items.Insert(0, new ListItem("---- Select ----", "-1"));
                drp_catFive.Items.Insert(0, new ListItem("---- Select ----", "-1"));
                bindFieldValue();
                bindDropDown();
                paymentMode.SelectedValue = "cod";
                salesDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                //salesDate.ReadOnly = true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "firedate();", true);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey1", "firedtSearch();", true);
            }
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
                field.Rows.Add("s.StyleCode", drpStyles.Text);
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
            ViewState["cat"] = cat;
            ViewState["field"] = field;
            ViewState["drp"] = drp;
            btnShowAll.Visible = false;
            BindData();
           
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
            else
            {
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
                field.Rows.Add("s.StyleCode", drpStyles.Text);
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

            ViewState["cat"] = cat;
            ViewState["field"] = field;
            ViewState["drp"] = drp;
            BindData();
            /*DataTable dt = obj.SearchStyle(cat, field, drp);
            if (!dt.Rows.Count.Equals(0))
            {
                divMainStockList.Visible = true;
                //Error.Visible = false;


            }
            else
            {
                //Error.Visible = true;
                divMainStockList.Visible = false;

            }*/
            /*rpt_Style.DataSource = dt;
            rpt_Style.DataBind();*/
        }
        catch (Exception ex)
        {

            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    private void BindData()
	{
		try
		{
			divDetails.Visible = false;
			divItemList.Visible = false;
			divSales.Visible = false;
			divBack.Visible = false;
			divMainStockList.Visible = true;

            DataTable cat = (DataTable)ViewState["cat"];
            DataTable field = (DataTable)ViewState["field"];
            DataTable drp = (DataTable)ViewState["drp"];

            viewInventoryCls obj = new viewInventoryCls();
			//DataTable dt = obj.bindInventory();
            DataTable dt = obj.bindInventorySearch(cat, field, drp);

            // pagination /////////////////////////
            _pgsource.DataSource = dt.DefaultView;
			_pgsource.AllowPaging = true;
			// Number of items to be displayed in the Repeater
			_pgsource.PageSize = _pageSize;
			_pgsource.CurrentPageIndex = CurrentPage;
			// Keep the Total pages in View State
			ViewState["TotalPages"] = _pgsource.PageCount;
			// Example: "Page 1 of 10"
			lblpage.Text = "Page " + (CurrentPage + 1) + " of " + _pgsource.PageCount;
			// Enable First, Last, Previous, Next buttons
			lbPrevious.Enabled = !_pgsource.IsFirstPage;
			lbNext.Enabled = !_pgsource.IsLastPage;
			lbFirst.Enabled = !_pgsource.IsFirstPage;
			lbLast.Enabled = !_pgsource.IsLastPage;

			// Bind data into repeater
			rptViewInventory.DataSource = _pgsource;
			rptViewInventory.DataBind();

			// Call the function to do paging
			HandlePaging();
			// pagination /////////////////////////
		}
		catch (Exception ex)
		{
			RecordExceptionCls rec = new RecordExceptionCls();
			rec.recordException(ex);
		}
	}

	protected void rptViewInventory_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		try
		{
			string styleid = ((DataRowView)e.Item.DataItem)["StyleID"].ToString();
			viewInventoryCls obj = new viewInventoryCls();
			DataTable dt = obj.bindInventorySizeDet(styleid);

			Label lblSizeDet = (Label)e.Item.FindControl("rptlblSizeDet");
			lblSizeDet.Text = dt.Rows[0]["sizedet"].ToString();
		}
		catch (Exception ex)
		{
			RecordExceptionCls rec = new RecordExceptionCls();
			rec.recordException(ex);
		}
	}

	protected void btnSell_Click(object sender, EventArgs e)
	{
        try
        {
            utilityCls obj = new utilityCls();
            DataTable dt = obj.getTableColwithID("roles", "roleId", Session["userrole"].ToString(), "sellingInd");
            if (dt.Rows[0]["sellingInd"].Equals("False"))
            {
                Response.Redirect("accessDenied.aspx", true);

            }
            else {
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));

            Label rptlblStyleId = (Label)rp1.FindControl("rptlblStyleId");
            lblStyleId.Text = rptlblStyleId.Text;
            Label rptlblStyleCode = (Label)rp1.FindControl("rptlblStyleCode");
            lblStyleCode.Text = rptlblStyleCode.Text;
            Label rptlblTitle = (Label)rp1.FindControl("rptlblTitle");
            lblTitle.Text = rptlblTitle.Text;
            Label rptlblMRP = (Label)rp1.FindControl("rptlblMRP");
            lblMRP.Text = rptlblMRP.Text;
            Label rptlblItemCat = (Label)rp1.FindControl("rptlblItemCat");
            lblItemCat.Text = rptlblItemCat.Text;
            Label rptlblSizeDet = (Label)rp1.FindControl("rptlblSizeDet");
            lblSizeDet.Text = rptlblSizeDet.Text;

            BindSalesData();

            divDetails.Visible = true;
            divSales.Visible = true;
            divBack.Visible = true;
            divMainStockList.Visible = false;
         }

        }
		catch (Exception ex)
		{
			RecordExceptionCls rec = new RecordExceptionCls();
			rec.recordException(ex);
		}
	}

	protected void btnList_Click(object sender, EventArgs e)
	{
        try
        {
            utilityCls obj = new utilityCls();
            DataTable dt = obj.getTableColwithID("roles", "roleId", Session["userrole"].ToString(), "listing");
            if (dt.Rows[0]["listing"].Equals("False"))
            {
                Response.Redirect("accessDenied.aspx", true);

            }
            else { 
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));

            Label rptlblStyleId = (Label)rp1.FindControl("rptlblStyleId");
            lblStyleId.Text = rptlblStyleId.Text;
            Label rptlblStyleCode = (Label)rp1.FindControl("rptlblStyleCode");
            lblStyleCode.Text = rptlblStyleCode.Text;
            Label rptlblTitle = (Label)rp1.FindControl("rptlblTitle");
            lblTitle.Text = rptlblTitle.Text;
            Label rptlblMRP = (Label)rp1.FindControl("rptlblMRP");
            lblMRP.Text = rptlblMRP.Text;
            Label rptlblItemCat = (Label)rp1.FindControl("rptlblItemCat");
            lblItemCat.Text = rptlblItemCat.Text;
            Label rptlblSizeDet = (Label)rp1.FindControl("rptlblSizeDet");
            lblSizeDet.Text = rptlblSizeDet.Text;

            locationCls lobj = new locationCls();
            DataTable loc = lobj.getVirtualLocation("2");
            cmbListLoc.DataSource = loc;
            cmbListLoc.DataBind();

            BindListItems();

            divDetails.Visible = true;
            divItemList.Visible = true;
            divBack.Visible = true;
            divMainStockList.Visible = false;
            }
		}
		catch (Exception ex)
		{
			RecordExceptionCls rec = new RecordExceptionCls();
			rec.recordException(ex);
		}
	}

	protected void cmbListLoc_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			BindListItems();
		}
		catch (Exception ex)
		{
			RecordExceptionCls rec = new RecordExceptionCls();
			rec.recordException(ex);
		}
	}

	private void BindListItems()
	{
		try
		{
			viewInventoryCls obj = new viewInventoryCls();
			DataTable dt = obj.bindItemsToList(lblStyleId.Text, cmbListLoc.SelectedValue);

			rptListItems.DataSource = dt;
			rptListItems.DataBind();

			DataView dv = new DataView(dt);
			dv.RowFilter = "Isnull(listidgivenbyvloc,'') <> ''";
			DataTable dtlistidgivenbyvloc = dv.ToTable(true, "listidgivenbyvloc");
			string listidgivenbyvloc = "";
			foreach (DataRow drow in dtlistidgivenbyvloc.Rows)
			{
				listidgivenbyvloc += drow["listidgivenbyvloc"] + ",";
			}
			if (!listidgivenbyvloc.Length.Equals(0))
				listidgivenbyvloc = listidgivenbyvloc.Remove(listidgivenbyvloc.Length - 1);
			txtlistidgivenbyvloc.Text = listidgivenbyvloc;
		}
		catch (Exception ex)
		{
			RecordExceptionCls rec = new RecordExceptionCls();
			rec.recordException(ex);
		}
	}

	protected void btnSaveListDet_Click(object sender, EventArgs e)
	{
		try
		{
			viewInventoryCls obj = new viewInventoryCls();
            DataTable dt = GetItemListDt();
            DataTable newDt = dt;
            string err = string.Empty;
            int dtFalse = 0;
            int dtTrue = 0;
            foreach (DataRow rows in newDt.Rows)
            {
                if(rows["chkItem"].Equals("True"))
                {
                    if(rows["listidgivenbyvloc"].Equals("") || rows["listprice"].Equals(""))
                    {
                        err += "Please Add List ID and List Price for all Selected SKU";
                        break;
                    }
                    dtTrue = 1;
                }
                else
                {
                    dtFalse = 1;
                }
            }
            //int success = obj.ListItems(GetItemListDt());
            if(!err.Equals(""))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert123", "alert('Please Add List ID and List Price for all Selected SKU');", true);
            }
            else if (dtFalse.Equals(1) && dtTrue.Equals(0))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1235454", "alert('Please Select Atleast One SKU');", true);
            }
            else
            {
                int success = obj.ListItems(dt);
                if (success.Equals(0))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Action Completed Successfully !');", true);
                    BindListItems();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Action Failed !');", true);
                    BindListItems();
                }
            }
		}
		catch (Exception ex)
		{
			RecordExceptionCls rec = new RecordExceptionCls();
			rec.recordException(ex);
		}
	}

	private DataTable GetItemListDt()
	{
		DataTable dt = new DataTable();
		try
		{

			dt.Columns.Add("StockUpID");
			dt.Columns.Add("listid");
			dt.Columns.Add("chkItem");
			dt.Columns.Add("listidgivenbyvloc");
			dt.Columns.Add("saleschannelvlocid");
			dt.Columns.Add("listprice");
            dt.Columns.Add("styleId");
            dt.Columns.Add("sizeId");
            dt.Columns.Add("sku");
            foreach (RepeaterItem itemEquipment in rptListItems.Items)
			{
				Label rptlblstockUpId = (Label)itemEquipment.FindControl("rptlblstockUpId");
				Label rptlblListId = (Label)itemEquipment.FindControl("rptlblListId");
				CheckBox chkListed = (CheckBox)itemEquipment.FindControl("chkListed");
				TextBox rpttxtListPrice = (TextBox)itemEquipment.FindControl("rpttxtListPrice");
                TextBox listidgivenbyvloc = (TextBox)itemEquipment.FindControl("listidgivenbyvloc");
                Label rptStyleId = (Label)itemEquipment.FindControl("rptStyleId");
                Label rptSizeId = (Label)itemEquipment.FindControl("rptSizeId");
                Label fullBarcode = (Label)itemEquipment.FindControl("fullBarcode"); 
                dt.Rows.Add(0, rptlblListId.Text, chkListed.Checked, listidgivenbyvloc.Text, cmbListLoc.SelectedValue, rpttxtListPrice.Text, rptStyleId.Text, rptSizeId.Text, fullBarcode.Text);

			}

		}
		catch (Exception ex)
		{
			RecordExceptionCls rec = new RecordExceptionCls();
			rec.recordException(ex);
		}
		return dt;
	}
    
	protected void BindSalesData()
	{
		try
		{
			//lblStyleID.Text = "26376";
			locationCls lobj = new locationCls();
			DataTable loc = lobj.getVirtualLocation("2");
			virtualLocation.DataSource = loc;
			virtualLocation.DataBind();

			styleCls sobj = new styleCls();
			DataTable state = sobj.getTable("stateCode");
			stateID.DataSource = state;
			stateID.DataBind();

			string gst = sobj.getGST(lblStyleId.Text);
			lblGSTPercent.Text = gst;

			DataTable dt = sobj.getStockUpInward(lblStyleId.Text, loc.Rows[0]["LocationID"].ToString());
			rptSales.DataSource = dt;
			rptSales.DataBind();
            
            //foreach (RepeaterItem item in rptSales.Items)
            //{
            //    HtmlTableCell saleStatusTD = (HtmlTableCell)item.FindControl("saleStatusTD");
            //    if (!saleStatusTD.InnerText.Equals("RFL"))
            //    {
            //        HtmlTableRow salesTR = (HtmlTableRow)item.FindControl("salesTR");
            //        salesTR.Visible = false;
            //    }
            //}

            salesId.Text = string.Empty;
            paymentMode.SelectedValue = "cod";
            salesDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            address1.Text = string.Empty;
            address2.Text = string.Empty;
            city.Text = string.Empty;
            custname.Text = string.Empty;

            divError.InnerHtml = string.Empty;
            divError.Visible = false;
        }
		catch (Exception ex)
		{
			RecordExceptionCls rex = new RecordExceptionCls();
			rex.recordException(ex);
		}
	}

	protected void virtualLocation_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{

			styleCls sobj = new styleCls();
			DataTable dt = sobj.getStockUpInward(lblStyleId.Text, virtualLocation.SelectedValue);
			rptSales.DataSource = dt;
			rptSales.DataBind();
		}
		catch (Exception ex)
		{
			RecordExceptionCls rex = new RecordExceptionCls();
			rex.recordException(ex);
		}
	}

	protected void saveSales_Click(object sender, EventArgs e)
	{
		try
		{
            styleCls obj = new styleCls();
			DataTable field = new DataTable();
			field.Columns.Add("StockUpId");
			field.Columns.Add("sp");
			decimal sum = Convert.ToDecimal(0.0);
            int countSales = 0;
            string err = string.Empty;
			foreach (RepeaterItem itemEquipment in rptSales.Items)
			{
				CheckBox sales = (CheckBox)itemEquipment.FindControl("sales");

				if (sales.Checked)
				{
                    countSales = 1;
                    TextBox sp = (TextBox)itemEquipment.FindControl("sp");
					Label stockUpId = (Label)itemEquipment.FindControl("stockUpId");
					string selling = string.Empty;

					if (sp.Text.Equals(""))
					{
						selling = "0";
                        err += "SP Cannot be NULL for Selected Barcode <br>";
                        break;

                    }
					else
					{
						selling = sp.Text;
					}
					sum += Convert.ToDecimal(selling);
					field.Rows.Add(stockUpId.Text, selling);

				}

			}
            if(countSales.Equals(0))
            {
                err += "Please Select atleast ONE Barcode<br>";
               // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1234", "alert('Please Select atleast ONE Barcode');", true);
            }
            
                if(salesId.Text.Equals(""))
                {
                    err += "Please Enter Sales ID";
                }
                if(!err.Equals(""))
                {
                    divError.InnerHtml = err;
                    divError.Visible = true;
                }
                else
                {
                    int success = obj.addSales(field, lblStyleId.Text, lblGSTPercent.Text,
                salesId.Text, custname.Text, address1.Text, address2.Text, city.Text, stateID.SelectedValue, sum, virtualLocation.SelectedValue, paymentMode.SelectedValue, salesDate.Text,phoneNo.Text);
                    if (success.Equals(-1))
                    {
                        //Session["salesSuccfail"] = "Invoice Generation Failed";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert4", "alert('Invoice Generation Failed');", true);
                        BindListItems();
                    }
                    else if (success.Equals(-2))
                    {
                        //Session["salesSuccfail"] = "Duplicate Invoice Generation Prevented";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert5", "alert('Duplicate Invoice Generation Prevented');", true);
                        BindListItems();
                    }
                else if (success.Equals(-3))
                {
                    //Session["salesSuccfail"] = "Duplicate Invoice Generation Prevented";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertSOLD", "alert('Invoice Generation Failed due to some SOLD Barcode Choosen');", true);
                    BindListItems();
                }
                else if (success.Equals(1))
                    {
                        //Session["salesSuccfail"] = "Invoice Generated Successfully";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert6", "alert('Invoice Generated Successfully');", true);
                        BindListItems();
                    }
                    else
                    {
                        //Session["salesSuccfail"] = "Some Error";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert7", "alert('Some Error');", true);
                        BindListItems();
                    }
                    divSales.Visible = false;
                    //Response.Redirect("invoice.aspx", true);
                }

            
			
		}
		catch (Exception ex)
		{
			RecordExceptionCls rex = new RecordExceptionCls();
			rex.recordException(ex);
		}
	}
    
	protected void btnBackToIventory_Click(object sender, EventArgs e)
	{
		try
		{
			
				BindData();

		}
		catch (Exception ex)
		{
			RecordExceptionCls rec = new RecordExceptionCls();
			rec.recordException(ex);
		}
	}

    protected void rptListItems_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            CheckBox chkListed = (CheckBox)e.Item.FindControl("chkListed");
            if (chkListed.Checked)
            {
                chkListed.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("ViewInventory.aspx", true);
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
            string imagelink = "http://finetouchimages.dzvdesk.com/Uploads/";
            LinkButton btn = ((LinkButton)(sender));
            //RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            //Label title = (Label)FindControl("lblTitle");
            lblItemNamedets.Text = lblTitle.Text;
            //Label StyleID = (Label)FindControl("lblStyleId");
            lblDetsStyleID.Text = lblStyleId.Text;

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

            DataTable item = obj.getItemStyleByID(lblStyleId.Text);
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

            DataTable dt = obj.getDataFieldNameView(lblStyleId.Text);
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
                        dt = obj.getTablewithID("Column1", "Col1ID", Convert.ToInt32(item.Rows[0]["Col1"].ToString()));
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

    //protected void showSalesByStatus_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DataTable showSalesStatusWise = (DataTable)ViewState["showSalesStatusWise"];

    //    }
    //    catch (Exception ex)
    //    {
    //        RecordExceptionCls rex = new RecordExceptionCls();
    //        rex.recordException(ex);
    //    }
    //}

    protected void showBarcodeStatus_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            Label lblBarcodeNo = (Label)rp1.FindControl("lblBarcodeNoPop");
            string[] barcodesarr = lblBarcodeNo.Text.Split('/');
            utilityCls Uobj = new utilityCls();
            //Panel PanelSinglePopUp = (Panel)FindControl("PanelSinglePopUp");
            Uobj.showCapARAPopUpSingle_Click(sender, e, PanelSinglePopUp, barcodesarr[0], lblBarcodeNo.Text);
            ModalPopupExtender299.Show();
            //ModalPopupExtender299.Show();
            /*MasterPage ms = (MasterPage)(this.Master);
            ms.ShowModalSignlePop();*/
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }
}