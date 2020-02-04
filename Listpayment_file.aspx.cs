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
using ClosedXML;
using System.Data.SqlClient;
using ClosedXML.Excel;
using System.Globalization;

public partial class Listpayment_file : System.Web.UI.Page
{
    
    //#region   // pagination /////////////////////////
    //readonly PagedDataSource _pgsource = new PagedDataSource();
    //int _firstIndex, _lastIndex;
    //private int _pageSize = 10;
    //private int CurrentPage
    //{
    //    get
    //    {
    //        if (ViewState["CurrentPage"] == null)
    //        {
    //            return 0;
    //        }
    //        return ((int)ViewState["CurrentPage"]);
    //    }
    //    set
    //    {
    //        ViewState["CurrentPage"] = value;
    //    }
    //}
    //#endregion// pagination /////////////////////////

    //#region   // pagination /////////////////////////
    //private void HandlePaging()
    //{
    //    var dt = new DataTable();
    //    dt.Columns.Add("PageIndex"); //Start from 0
    //    dt.Columns.Add("PageText"); //Start from 1

    //    _firstIndex = CurrentPage - 5;
    //    if (CurrentPage > 5)
    //        _lastIndex = CurrentPage + 5;
    //    else
    //        _lastIndex = 10;

    //    // Check last page is greater than total page then reduced it 
    //    // to total no. of page is last index
    //    if (_lastIndex > Convert.ToInt32(ViewState["TotalPages"]))
    //    {
    //        _lastIndex = Convert.ToInt32(ViewState["TotalPages"]);
    //        _firstIndex = _lastIndex - 10;
    //    }

    //    if (_firstIndex < 0)
    //        _firstIndex = 0;

    //    // Now creating page number based on above first and last page index
    //    for (var i = _firstIndex; i < _lastIndex; i++)
    //    // for (var i = _firstIndex; i < 5; i++)
    //    {
    //        var dr = dt.NewRow();
    //        dr[0] = i;
    //        dr[1] = i + 1;
    //        dt.Rows.Add(dr);
    //    }

    //    rptPaging.DataSource = dt;
    //    rptPaging.DataBind();
    //}

    //protected void lbFirst_Click(object sender, EventArgs e)
    //{
    //    //if (lblcomingfrom.Text == "Search")
    //    //{
    //    //	CurrentPage = 0;
    //    //	search();
    //    //}
    //    //else
    //    //{
    //    CurrentPage = 0;
    //    BindData();
    //    //}

    //}

    //protected void lbLast_Click(object sender, EventArgs e)
    //{
    //    //if (lblcomingfrom.Text == "Search")
    //    //{
    //    //	CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
    //    //	search();
    //    //}
    //    //else
    //    //{
    //    CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
    //    BindData();
    //    //}

    //}
    //protected void lbPrevious_Click(object sender, EventArgs e)
    //{
    //    //if (lblcomingfrom.Text == "Search")
    //    //{
    //    //	CurrentPage -= 1;
    //    //	search();
    //    //}
    //    //else
    //    //{
    //    CurrentPage -= 1;
    //    BindData();
    //    //}

    //}
    //protected void lbNext_Click(object sender, EventArgs e)
    //{
    //    //if (lblcomingfrom.Text == "Search")
    //    //{
    //    //	CurrentPage += 1;
    //    //	search();
    //    //}
    //    //else
    //    //{
    //    CurrentPage += 1;
    //    BindData();
    //    //}

    //}

    //protected void rptPaging_ItemCommand(object source, DataListCommandEventArgs e)
    //{
    //    //if (lblcomingfrom.Text == "Search")
    //    //{
    //    //	if (!e.CommandName.Equals("newPage")) return;
    //    //	CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
    //    //	search();
    //    //}
    //    //else
    //    //{
    //    if (!e.CommandName.Equals("newPage")) return;
    //    CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
    //    BindData();
    //    //}

    //}

    //protected void rptPaging_ItemDataBound(object sender, DataListItemEventArgs e)
    //{
    //    Button lnkPage = (Button)e.Item.FindControl("lbPaging");
    //    if (lnkPage.CommandArgument != CurrentPage.ToString()) return;
    //    lnkPage.Enabled = false;
    //    lnkPage.BackColor = System.Drawing.Color.FromName("#014d79");
    //    lnkPage.BorderColor = System.Drawing.Color.FromName("#014d79");
    //    lnkPage.CssClass = "btn btn-primary btn-flat";


    //    //lnkPage.Attributes.Add("class", lnkPage.Attributes["class"].ToString().Replace(" ", "btn btn-success btn-flat"));
    //    // lnkPage.Attributes.Add("class","btn btn-success btn-flat");

    //    //  lnkPage.BackColor = "#00FF00";
    //}

    //#endregion // pagination /////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("SearchBy"); dt.Columns.Add("searchField"); dt.Columns.Add("Vloc"); dt.Columns.Add("fromDate"); dt.Columns.Add("toDate");
                dt.Rows.Add("All", "", "", "", "");
                ViewState["searchFields"] = dt;
               // BindData();
                fromDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                toDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                lblerrorbarcode.Text = "";
               
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey132", "firedate();", true);
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
            //locationCls lobj = new locationCls();
            //DataTable loc = lobj.getVirtualLocation("2");
            //vLoc.DataSource = loc;
            //vLoc.DataBind();

            Payment_fileCls obj = new Payment_fileCls();
            DataTable searchFields = (DataTable)ViewState["searchFields"];
            DataTable dt = obj.getpaymentbyserch(searchFields);
            int count = dt.Rows.Count;
           
            if (searchBy.SelectedValue.Equals("5"))
            {
                
                Payment_fileCls ob = new Payment_fileCls();
                DataTable data = ob.SearchByOne(vLoc.SelectedValue);
                rptsales.DataSource = data;
                rptsales.DataBind();
                sales.Visible = true;
                divmain.Visible = false;
                btnexpoettoexcel.Visible = true;
                btnaddmanual.Visible = true;
                
                FileUpload.Visible = true;
                btnupload.Visible = true;
                //vLoc.DataSource = loc;
                //vLoc.DataBind();


            }
            else
            {
                // Bind data into repeater
                rpt_pymt.DataSource = dt;
                rpt_pymt.DataBind();
                divmain.Visible = true;
                sales.Visible = false;
                btnaddmanual.Visible = false;
                btnexpoettoexcel.Visible = false;
            }
            // Call the function to do paging
            //HandlePaging();
            // pagination /////////////////////////

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
            //reclocation = "";
            hSearch.Visible = false;
            hVloc.Visible = false;
            hFDate.Visible = false;
            hTdate.Visible = false;
            tSearch.Visible = false;
            tVloc.Visible = false;
            tFDate.Visible = false;
            tTdate.Visible = false;
            btnexpoettoexcel.Visible = false;
            btnaddmanual.Visible = false;
            if (searchBy.SelectedValue.Equals("1") || searchBy.SelectedValue.Equals("2"))
            {
                hSearch.Visible = true;
                tSearch.Visible = true;
            }
            else if (searchBy.SelectedValue.Equals("3"))
            {
                hVloc.Visible = true;
                tVloc.Visible = true;
                locationCls lobj = new locationCls();
                DataTable loc = lobj.getVirtualLocation("2");
                vLoc.DataSource = loc;
                vLoc.DataBind();
            }
            else if (searchBy.SelectedValue.Equals("4"))
            {
                hFDate.Visible = true;
                hTdate.Visible = true;
                tFDate.Visible = true;
                tTdate.Visible = true;

            }
            else if (searchBy.SelectedValue.Equals("5"))
            {
                locationCls lobj = new locationCls();
                DataTable loc = lobj.getVirtualLocation("2");
                vLoc.DataSource = loc;
                vLoc.DataBind();
                hVloc.Visible = true;
                tVloc.Visible = true;
                
            }

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void search_Click(object sender, EventArgs e)
    {
        try
        {
            divmain.Visible = false;
            sales.Visible = true;
            invoiceCls obj = new invoiceCls();
            string search = searchBy.SelectedValue;
            string searchFields = searchField.Text;

            DataTable dt = new DataTable();
            dt.Columns.Add("SearchBy"); dt.Columns.Add("searchField"); dt.Columns.Add("Vloc"); dt.Columns.Add("fromDate"); dt.Columns.Add("toDate");
            dt.Rows.Add(search, searchFields, vLoc.SelectedValue, fromDate.Text, toDate.Text);
            ViewState["searchFields"] = dt;
            BindData();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }

    }
    protected void btnaction_Click(object sender, EventArgs e)
    {
        try
        {
           
            LinkButton det = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(det.NamingContainer));
            Label id = (Label)rp1.FindControl("id");
            Label salesid = (Label)rp1.FindControl("salesid");
            TextBox txtstockupid = (TextBox)rp1.FindControl("txtstockupid");
            RadioButtonList rbttype = (RadioButtonList)rp1.FindControl("rbttype");
            Label lbltype = (Label)rp1.FindControl("lbltype");
            if (rbttype.SelectedValue.Equals(""))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Type!');", true);
            }
            else
            {
                Payment_fileCls obj = new Payment_fileCls();
                int result = obj.updatestockupid(txtstockupid.Text, id.Text, rbttype.SelectedValue, salesid.Text);
                if (result.Equals(1))
                {
                    BindData();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfully Updated!');", true);
                }
                else if (result.Equals(-1))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Barcode Not Found!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Failed !');", true);
                }

            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void btndelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton det = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(det.NamingContainer));
            Label id = (Label)rp1.FindControl("id");


            DataTable nul = null;
            Payment_fileCls obj = new Payment_fileCls();
            DataTable result = obj.Detele(id.Text, nul,"");
            if (!result.Equals(null))
            {
                BindData();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfully !');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Failed !');", true);
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }
    public DataTable Readexcel()
    {
        DataTable dt = new DataTable();
        try
        {
            if (FileUpload.HasFile)
            {
                using (XLWorkbook workBook = new XLWorkbook(FileUpload.PostedFile.InputStream))
                {
                    //Read the first Sheet from Excel file.
                    IXLWorksheet workSheet = workBook.Worksheet(1);

                    //Create a new DataTable.
                    //DataTable dt = new DataTable();

                    //Loop through the Worksheet rows.
                    bool firstRow = true;
                    foreach (IXLRow row in workSheet.Rows())
                    {

                        //Use the first row to add columns to DataTable.
                        if (firstRow)
                        {
                            foreach (IXLCell cell in row.Cells())
                            {
                                dt.Columns.Add(cell.Value.ToString().Trim());
                            }

                            firstRow = false;
                        }
                        else
                        {

                            //Add rows to DataTable.
                            dt.Rows.Add();
                            int count = dt.Rows.Count;
                            int i = 0;
                            for (int j = 1; j <= dt.Columns.Count; j++)
                            {
                                if (string.IsNullOrEmpty(row.Cell(j).Value.ToString()))
                                    dt.Rows[dt.Rows.Count - 1][i] = "";
                                else
                                    dt.Rows[dt.Rows.Count - 1][i] = row.Cell(j).Value.ToString().Trim();
                                i++;
                            }

                        }
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('File Not Selected!');", true);
            }

        }
        catch (Exception ex)
        {

            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
            return null;
        }
        return dt;
    }





    //protected void ChkSelectAllupdate_CheckedChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        bool selectallupdate = false;
    //        if(ChkSelectAllupdate.Checked.Equals(true))
    //        {
    //            selectallupdate = true;

    //        }
    //        foreach(RepeaterItem rpt in rptsales.Items)
    //        {
    //            CheckBox chckforupdate = (CheckBox)rpt.FindControl("chckforupdate");

    //            chckforupdate.Checked = selectallupdate;
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        RecordExceptionCls rec = new RecordExceptionCls();
    //        rec.recordException(ex);
    //    }
    //}

    //protected void ChkSelectAlldelete_CheckedChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        bool selectalldelete = false;
    //        if (ChkSelectAlldelete.Checked.Equals(true))
    //        {
    //            selectalldelete = true;
    //        }
    //        foreach (RepeaterItem rpt in rptsales.Items)
    //        {
    //            CheckBox chckfordelete = (CheckBox)rpt.FindControl("chckfordelete");

    //            chckfordelete.Checked = selectalldelete;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        RecordExceptionCls rec = new RecordExceptionCls();
    //        rec.recordException(ex);
    //    }
    //}

    //protected void btnupdatese_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        dt.Columns.Add("ID");
    //        dt.Columns.Add("barcode");

    //        foreach(RepeaterItem rpt in rptsales.Items)
    //        {
    //            CheckBox chckforupdate = (CheckBox)rpt.FindControl("chckforupdate");
    //            TextBox txtstockupid = (TextBox)rpt.FindControl("txtstockupid");
    //            Label id = (Label)rpt.FindControl("id");
    //            if (chckforupdate.Checked.Equals(true))
    //            {
    //                dt.Rows.Add(id.Text,txtstockupid.Text);
    //            }
    //        }
    //        Payment_fileCls obj = new Payment_fileCls();
    //        DataTable result = obj.updatestockupid("", "", "", dt, "multiple");
    //        if (!result.Equals(null))
    //        {
    //            if(result.Rows.Count==0)
    //            {
    //                BindData();
    //                lblerrorbarcode.Visible = false;
    //                divid.Visible = false;
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Successfull !');", true);
    //            }
    //            else
    //            {
    //                string Store = result.AsEnumerable().Select(row => row["barcode"].ToString()).Aggregate((s1, s2) => String.Concat(s1, "," +s2));
    //                lblerrorbarcode.Text = Store;
    //                lblerrorbarcode.Visible = true;
    //                divid.Visible = true;
    //                BindData();
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Following Barcode Not Found !');", true);
    //            }

    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Failed !');", true);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        RecordExceptionCls rec = new RecordExceptionCls();
    //        rec.recordException(ex);
    //    }
    //}

    //protected void btndeletese_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        dt.Columns.Add("ID");


    //        foreach (RepeaterItem rpt in rptsales.Items)
    //        {
    //            CheckBox chckfordelete = (CheckBox)rpt.FindControl("chckfordelete");
    //            Label id = (Label)rpt.FindControl("id");
    //            if (chckfordelete.Checked.Equals(true))
    //            {
    //                dt.Rows.Add(id.Text);
    //            }
    //        }
    //        Payment_fileCls obj = new Payment_fileCls();
    //        DataTable result = obj.Detele("",dt, "multiple");
    //        if(!result.Equals(null))
    //        {
    //            BindData();
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Successfull !');", true);
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Failed !');", true);
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        RecordExceptionCls rec = new RecordExceptionCls();
    //        rec.recordException(ex);
    //    }
    //}

    protected void rptsales_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            RequiredFieldValidator val = (RequiredFieldValidator)e.Item.FindControl("myVal");
            LinkButton btn = (LinkButton)e.Item.FindControl("btnaction");
            RadioButtonList rbttype = (RadioButtonList)e.Item.FindControl("rbttype");

            val.ControlToValidate = rbttype.ID;
            val.ValidationGroup = btn.ToString();
            val.ErrorMessage = "Required";
        }
        catch(Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void btnexpoettoexcel_Click(object sender, EventArgs e)
    {
        try
        {
            Payment_fileCls obj = new Payment_fileCls();
            DataTable searchFields = (DataTable)ViewState["searchFields"];
            DataTable dt = obj.SearchByOne(vLoc.SelectedValue);
            dt.Columns.Add("Barcode");
            dt.Columns.Add("typeofbarcode");
            dt.Columns.Add("Action");

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "MapRecords");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                //Response.ContentType = "application / vnd.ms-excel";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fname = vLoc.SelectedItem.Text +" Map Records" + DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss");

                Response.AddHeader("content-disposition", "attachment;filename=" + fname + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        catch(Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void btnaddmanual_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("addmanualpayment.aspx", true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }


    protected void btngo_Click(object sender, EventArgs e)
    {
        try
        {
            
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void vLoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //reclocation = vLoc.SelectedValue;
        }
        catch(Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void btnupload_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = Readexcel();
            if (!dt.Rows.Count.Equals(0))
            {
                Payment_fileCls obj = new Payment_fileCls();
                DataTable scdt = obj.dobulkupload(dt,vLoc.SelectedValue);
                BindData();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(scdt, "BulkUploadMapRecord");
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    //Response.ContentType = "application / vnd.ms-excel";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    string fname = vLoc.SelectedItem + "_Map Record_" + DateTime.Now.ToString("MM/dd/yyyy_HH:mm:ss");

                    Response.AddHeader("content-disposition", "attachment;filename=" + fname + ".xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            
        }
        catch(Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }
}