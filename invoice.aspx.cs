using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class invoice : System.Web.UI.Page
{
    #region   // pagination /////////////////////////
    readonly PagedDataSource _pgsource = new PagedDataSource();
    int _firstIndex, _lastIndex;
    private int _pageSize = 10;
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
                DataTable dt = new DataTable();
                dt.Columns.Add("SearchBy"); dt.Columns.Add("searchField"); dt.Columns.Add("Vloc"); dt.Columns.Add("fromDate"); dt.Columns.Add("toDate");
                dt.Rows.Add("All","","","","");
                ViewState["searchFields"] = dt;
                BindData();
                fromDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                toDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                if (Session["salesSuccfail"].ToString().Equals("Invoice Generated Successfully"))
                {
                    divSucc.InnerText = Session["salesSuccfail"].ToString();
                    divSucc.Visible = true;
                    divError.Visible = false;
                    Session.Remove("salesSuccfail");
                }
                else
                {
                    divError.InnerText = Session["salesSuccfail"].ToString();
                    divError.Visible = true;
                    divSucc.Visible = false;
                    Session.Remove("salesSuccfail");
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "firedtSearch();", true);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey132", "firedate();", true);
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
            locationCls lobj = new locationCls();
            DataTable loc = lobj.getVirtualLocation("2");
            vLoc.DataSource = loc;
            vLoc.DataBind();

            invoiceCls obj = new invoiceCls();
            DataTable searchFields = (DataTable)ViewState["searchFields"];
            DataTable dt = obj.getInvoiceListSearch(searchFields);            
            
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
            rpt_Invoice.DataSource = _pgsource;
            rpt_Invoice.DataBind();

            // Call the function to do paging
            HandlePaging();
            // pagination /////////////////////////

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void printInv_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            Label invoiceid = (Label)rp1.FindControl("invoiceid");
            invoiceCls obj = new invoiceCls();
            DataTable invoice = obj.getInvoicebyID(invoiceid.Text);
            DataTable sales = obj.getSalesbyInvID(invoiceid.Text);
            Response.Redirect("print.aspx",true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void viewInv_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            Label invoiceid = (Label)rp1.FindControl("invoiceid");
            
            Session["invoiceId"] = invoiceid.Text;
            Response.Redirect("viewInvoice.aspx", true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void searchSoldItem_Click(object sender, EventArgs e)
    {
        try
        {
            invoiceCls obj = new invoiceCls();
            string search = searchBy.SelectedValue;
            string searchFields = searchField.Text;

            DataTable dt = new DataTable();
            dt.Columns.Add("SearchBy"); dt.Columns.Add("searchField"); dt.Columns.Add("Vloc"); dt.Columns.Add("fromDate"); dt.Columns.Add("toDate");
            dt.Rows.Add(search, searchFields, vLoc.SelectedValue, fromDate.Text, toDate.Text);
            ViewState["searchFields"] = dt;
            BindData();

            /*DataTable searchDt = new DataTable();
            if (search.Equals("1")) // by barcode
            {
                searchDt = obj.invoiceSearchByBarcode(searchFields);
            }
            else if(search.Equals("2")) // by salesid
            {
                searchDt = obj.invoiceSearchBySalesID(searchFields);
            }
            else if (search.Equals("3")) // by virtual location
            {
                searchDt = obj.invoiceSearchByVirtualLoc(vLoc.SelectedValue);
            }
            else if (search.Equals("4")) // by Date
            {
                searchDt = obj.invoiceSearchByDate(fromDate.Text,toDate.Text);
            }
            
            rpt_Invoice.DataSource = searchDt;
            rpt_Invoice.DataBind();*/
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void searchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            hSearch.Visible = false;
            hVloc.Visible = false;
            hFDate.Visible = false;
            hTdate.Visible = false;
            tSearch.Visible = false;
            tVloc.Visible = false;
            tFDate.Visible = false;
            tTdate.Visible = false;
            if (searchBy.SelectedValue.Equals("1") || searchBy.SelectedValue.Equals("2") || searchBy.SelectedValue.Equals("5") || searchBy.SelectedValue.Equals("6"))
            {
                hSearch.Visible = true;
                tSearch.Visible = true;
            }else if(searchBy.SelectedValue.Equals("3"))
            {
                hVloc.Visible = true;
                tVloc.Visible = true;
            }
            else if (searchBy.SelectedValue.Equals("4"))
            {
                hFDate.Visible = true;
                hTdate.Visible = true;
                tFDate.Visible = true;
                tTdate.Visible = true;
                
            }
           
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void cancelInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)(sender);
            RepeaterItem rp1 = (RepeaterItem)(btn.NamingContainer);
            Label invoiceid = (Label)rp1.FindControl("invoiceid");
            invoiceCls obj = new invoiceCls();
            int result = obj.cancelInvoice(invoiceid.Text);
            if (result.Equals(1))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert22", "alert('Invoice Canceled Successfully !');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert122", "alert('Invoice Cancellation Failed !');", true);
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void rpt_Invoice_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            string paymentStatus = ((DataRowView)e.Item.DataItem)["paymentStatus"].ToString();
            string invoiceStatus = ((DataRowView)e.Item.DataItem)["invoiceStatus"].ToString();
            

            LinkButton cancelInvoice = (LinkButton)e.Item.FindControl("cancelInvoice");
            LinkButton markPaid = (LinkButton)e.Item.FindControl("markPaid");
            LinkButton makePayment = (LinkButton)e.Item.FindControl("makePayment");
            LinkButton viewInv = (LinkButton)e.Item.FindControl("viewInv");
            LinkButton viewBarcodes = (LinkButton)e.Item.FindControl("viewBarcodes");
            if (paymentStatus.Equals("unpaid") && invoiceStatus.Equals("Invoiced"))
            {
                cancelInvoice.Visible = true;
                markPaid.Visible = true;
                makePayment.Visible = true;
                viewInv.Visible = true;
                viewBarcodes.Visible = true;
            }
            

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void markPaid_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)(sender);
            RepeaterItem rp1 = (RepeaterItem)(btn.NamingContainer);
            Label invoiceid = (Label)rp1.FindControl("invoiceid");
            invoiceCls obj = new invoiceCls();
            int result = obj.markInvoicePaid(invoiceid.Text);
            if (result.Equals(1))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert22", "alert('Invoice Marked as Paid Successfully !');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert122", "alert('Invoice Mark as Paid Failed !');", true);
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void makePayment_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            Label invoiceid1 = (Label)rp1.FindControl("invoiceid");
            Session["makeInvoicePayment"] = invoiceid1.Text;
            Response.Redirect("invoicePayment.aspx", true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void addPayments_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("addCustomerPayments.aspx", true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void addTrader_Click(object sender, EventArgs e)
    {
        try
        {
            Session["addTrader"] = "0";
            Response.Redirect("addTrader.aspx",true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void viewBarcodes_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            Label invoiceid = (Label)rp1.FindControl("invoiceid");

            Session["invoiceId"] = invoiceid.Text;
            Response.Redirect("viewBarcodes.aspx", true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void multipleInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("multipleInvoice.aspx",true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}