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

public partial class ViewInventorySearch : System.Web.UI.Page
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
				BindData();
				
			}
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
			viewInventoryCls obj = new viewInventoryCls();
			DataTable dt = obj.bindInventory();

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


	public void BindItemStyleShowAll()
    {
        DataBase.StockUp ObjBind = new DataBase.StockUp();

        string Status = Session["Status"].ToString();

        DataSet ds = ObjBind.BindStockUpItem(Status);

        Session["ds"] = ds;

        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[1].Rows.Count > 0)
            {
                int Counter = ds.Tables[1].Rows.Count;
                int lblCount = 0;

                if (Counter > lblCount)
                {
                    lblcol1.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col1.Visible = true;

                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol2.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col2.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol3.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col3.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol4.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col4.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol5.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col5.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol6.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col6.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol7.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col7.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol8.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col8.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol9.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col9.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol10.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col10.Visible = true;
                    lblCount++;
                }

            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                GV.DataSource = ds.Tables[0];
                GV.DataBind();
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                int Counter = ds.Tables[2].Rows.Count;
                int lblCount = 0;

                if (Counter > lblCount)
                {
                    lblcontrol1.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control1.Visible = true;

                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol2.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control2.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol3.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control3.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol4.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control4.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol5.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control5.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol6.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control6.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol7.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control7.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol8.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control8.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol9.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control9.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol10.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control10.Visible = true;
                    lblCount++;
                }

                if (Counter > lblCount)
                {
                    lblcontrol11.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control11.Visible = true;

                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol12.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control12.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol13.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control13.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol14.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control14.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol15.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control15.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol16.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control16.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol17.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control17.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol18.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control18.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol19.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control19.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol20.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control20.Visible = true;
                    lblCount++;
                }
            }
        }

        ds.Dispose();
        ObjBind = null;

        //loader.Visible = false;
    }

    protected void rptr_ItemBound(object sender, RepeaterItemEventArgs e)
    {
        //ItemBoundCounter++;

        //if (ItemBoundCounter == 1)
        {
            HtmlTableCell icol1 = (HtmlTableCell)e.Item.FindControl("col1");
            HtmlTableCell icol2 = (HtmlTableCell)e.Item.FindControl("col2");
            HtmlTableCell icol3 = (HtmlTableCell)e.Item.FindControl("col3");
            HtmlTableCell icol4 = (HtmlTableCell)e.Item.FindControl("col4");
            HtmlTableCell icol5 = (HtmlTableCell)e.Item.FindControl("col5");
            HtmlTableCell icol6 = (HtmlTableCell)e.Item.FindControl("col6");
            HtmlTableCell icol7 = (HtmlTableCell)e.Item.FindControl("col7");
            HtmlTableCell icol8 = (HtmlTableCell)e.Item.FindControl("col8");
            HtmlTableCell icol9 = (HtmlTableCell)e.Item.FindControl("col9");
            HtmlTableCell icol10 = (HtmlTableCell)e.Item.FindControl("col10");

            HtmlTableCell icontrol1 = (HtmlTableCell)e.Item.FindControl("control1");
            HtmlTableCell icontrol2 = (HtmlTableCell)e.Item.FindControl("control2");
            HtmlTableCell icontrol3 = (HtmlTableCell)e.Item.FindControl("control3");
            HtmlTableCell icontrol4 = (HtmlTableCell)e.Item.FindControl("control4");
            HtmlTableCell icontrol5 = (HtmlTableCell)e.Item.FindControl("control5");
            HtmlTableCell icontrol6 = (HtmlTableCell)e.Item.FindControl("control6");
            HtmlTableCell icontrol7 = (HtmlTableCell)e.Item.FindControl("control7");
            HtmlTableCell icontrol8 = (HtmlTableCell)e.Item.FindControl("control8");
            HtmlTableCell icontrol9 = (HtmlTableCell)e.Item.FindControl("control9");
            HtmlTableCell icontrol10 = (HtmlTableCell)e.Item.FindControl("control10");

            HtmlTableCell icontrol11 = (HtmlTableCell)e.Item.FindControl("control11");
            HtmlTableCell icontrol12 = (HtmlTableCell)e.Item.FindControl("control12");
            HtmlTableCell icontrol13 = (HtmlTableCell)e.Item.FindControl("control13");
            HtmlTableCell icontrol14 = (HtmlTableCell)e.Item.FindControl("control14");
            HtmlTableCell icontrol15 = (HtmlTableCell)e.Item.FindControl("control15");
            HtmlTableCell icontrol16 = (HtmlTableCell)e.Item.FindControl("control16");
            HtmlTableCell icontrol17 = (HtmlTableCell)e.Item.FindControl("control17");
            HtmlTableCell icontrol18 = (HtmlTableCell)e.Item.FindControl("control18");
            HtmlTableCell icontrol19 = (HtmlTableCell)e.Item.FindControl("control19");
            HtmlTableCell icontrol20 = (HtmlTableCell)e.Item.FindControl("control20");

            DataSet ds = (DataSet)Session["ds"];

            if (ds.Tables[1].Rows.Count > 0)
            {
                int Counter = ds.Tables[1].Rows.Count;
                int lblCount = 0;

                if (Counter > lblCount)
                {
                    icol1.Visible = true;

                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icol2.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icol3.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icol4.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icol5.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icol6.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icol7.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icol8.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icol9.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icol10.Visible = true;
                    lblCount++;
                }

            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                int Counter = ds.Tables[2].Rows.Count;
                int lblCount = 0;

                if (Counter > lblCount)
                {
                    icontrol1.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol2.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol3.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol4.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol5.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol6.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol7.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol8.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol9.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol10.Visible = true;
                    lblCount++;
                }

                if (Counter > lblCount)
                {
                    icontrol11.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol12.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol13.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol14.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol15.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol16.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol17.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol18.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol19.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol20.Visible = true;
                    lblCount++;
                }
            }
        }

    }

	protected void rptViewInventory_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		try
		{
			string styleid = ((DataRowView)e.Item.DataItem)["StyleID"].ToString();
			viewInventoryCls obj = new viewInventoryCls();
			DataTable dt = obj.bindInventorySizeDet(styleid);

			Label lblSizeDet = (Label)e.Item.FindControl("lblSizeDet");
			lblSizeDet.Text = dt.Rows[0]["sizedet"].ToString();
		}
		catch (Exception ex)
		{
			RecordExceptionCls rec = new RecordExceptionCls();
			rec.recordException(ex);
		}
	}
}