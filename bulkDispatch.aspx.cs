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


public partial class bulkDispatch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindData();
                if (Session["bulkDispatchScanSess"] != null)
                {
                    if (Session["bulkDispatchScanSess"].ToString().Equals("Dispatched Successfully"))
                    {
                        divInsert.InnerText = Session["bulkDispatchScanSess"].ToString();
                        divInsert.Visible = true;
                        divError.Visible = false;
                        Session.Remove("bulkDispatchScanSess");
                    }
                    else
                    {
                        divError.InnerText = Session["bulkDispatchScanSess"].ToString();
                        divError.Visible = true;
                        divInsert.Visible = false;
                        Session.Remove("bulkDispatchScanSess");
                    }

                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey11", "firedtSearch();", true);
            }
           
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void BindData()
    {
        try
        {
            bulkUploads obj = new bulkUploads();
            DataTable dt = obj.getBulkInvoice();
            invid.DataSource = dt;
            invid.DataBind();

            courierCls cObj = new courierCls();
            DataTable courierDt = cObj.getCourier();
            courier.DataSource = courierDt;
            courier.DataBind();

            styleCls sobj = new styleCls();
            DataTable state = sobj.getTable("stateCode");
            stateID.DataSource = state;
            stateID.DataBind();
                       
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void searchBarcode_Click(object sender, EventArgs e)
    {
        try
        {
            bulkUploads obj = new bulkUploads();
            DataTable dt = obj.getSoldBarcodeByInvoice(invid.SelectedValue);
            rpt_Barcode.DataSource = dt;
            rpt_Barcode.DataBind();
            invid.Enabled = false;

            invoiceCls invObj = new invoiceCls();
            DataTable invNo = invObj.getInvoicebyID(invid.SelectedValue);
            custname.Text = invNo.Rows[0]["custname"].ToString();
            address1.Text = invNo.Rows[0]["address1"].ToString();
            address2.Text = invNo.Rows[0]["address2"].ToString();
            phoneNo.Text = invNo.Rows[0]["phoneNo"].ToString();
            city.Text = invNo.Rows[0]["city"].ToString();
            stateID.SelectedValue = invNo.Rows[0]["state"].ToString();

            tableHide.Visible = true;
           
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void checkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            bool setValue = false;
            if(checkSelectAll.Checked)
            {
                setValue = true;
            }
            foreach (RepeaterItem item in rpt_Barcode.Items)
            {
                CheckBox chkdel = (CheckBox)item.FindControl("checkBoxBarcode");//assume the ID of checkbox is 'chkdel'
                chkdel.Checked = setValue;
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void saveBulkDispatch_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("StockupID");
            dt.Columns.Add("sid");
            dt.Columns.Add("sellingprice");
            dt.Columns.Add("barcode");
            foreach (RepeaterItem item in rpt_Barcode.Items)
            {
                CheckBox chkdel = (CheckBox)item.FindControl("checkBoxBarcode");//assume the ID of checkbox is 'chkdel'
                if(chkdel.Checked)
                {
                    Label StockupID = (Label)item.FindControl("StockupID");
                    Label sid = (Label)item.FindControl("sid");
                    TextBox sellingprice = (TextBox)item.FindControl("sellingprice");
                    Label barcode = (Label)item.FindControl("barcodeNo");
                    dt.Rows.Add(StockupID.Text,sid.Text, sellingprice.Text, barcode.Text);
                }
            }
            string err = string.Empty;
            if(custname.Text.Equals(""))
            {
                err += "Enter Customer Name<br>";
            }
            if (address1.Text.Equals(""))
            {
                err += "Enter Address 1<br>";
            }
            if (address2.Text.Equals(""))
            {
                err += "Enter Address 2<br>";
            }
            if (city.Text.Equals(""))
            {
                err += "Enter City<br>";
            }
            if (phoneNo.Text.Equals(""))
            {
                err += "Enter Phone No.<br>";
            }
            if (salesabwno.Text.Equals(""))
            {
                err += "Enter ABW No.<br>";
            }
            if (dt.Rows.Count.Equals(0))
            {
                err += "Select atleast One barcode";
            }
            if (!err.Equals(""))
            {
                divError.InnerHtml = err;
                divError.Visible = true;
            }
            else {
                bulkUploads obj = new bulkUploads();
                int success = obj.doBulkDispatch(invid.SelectedValue,salesabwno.Text,courier.SelectedValue,dt,custname.Text,address1.Text,address2.Text,city.Text,stateID.SelectedValue,phoneNo.Text);
                
                if (success>=0)
                {
                    //obj.setDispatchStatus();
                    Session["bulkDispatchScanSess"] = "Dispatched Successfully";
                    /*divInsert.InnerHtml = "Dispatched Successfully";
                    divInsert.Visible = true;
                    divError.Visible = false;*/
                }
                else
                {
                    
                    Session["bulkDispatchScanSess"] = "Dispatch Failed";
                    /*divError.InnerHtml = "Dispatch Failed";
                    divError.Visible = true;
                    divInsert.Visible = false;*/
                }
                Response.Redirect("bulkDispatch.aspx", true);
                //tableHide.Visible = false;
            }

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void changeSPAmount_Click(object sender, EventArgs e)
    {
        try
        {
            string search = changeSP.Text;
            string searchFields = searchField.Text;
            if (searchFields.Equals(""))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Percentage/Amount');", true);
            }
            else
            {
                foreach (RepeaterItem item in rpt_Barcode.Items)
                {
                    TextBox sellingprice = (TextBox)item.FindControl("sellingprice");
                    Label actualSP = (Label)item.FindControl("actualSP");
                    CheckBox chkdel = (CheckBox)item.FindControl("checkBoxBarcode");//assume the ID of checkbox is 'chkdel'
                    if (chkdel.Checked)
                    {
                        if (search.Equals("1")) // by percent
                        {
                            decimal percent = Convert.ToDecimal(actualSP.Text) - Convert.ToDecimal((Convert.ToDecimal(actualSP.Text) * (Convert.ToDecimal(searchFields)) / 100));
                            sellingprice.Text = percent.ToString();
                        }
                        else // direct change amount
                        {
                            sellingprice.Text = searchFields;
                        }
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

    protected void changeSpbyExcel_Click(object sender, EventArgs e)
    {
        //Open the Excel file using ClosedXML.
        using (XLWorkbook workBook = new XLWorkbook(FileUpload1.PostedFile.InputStream))
        {
            // loop threough repeater to make it datatable
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("StockupID");
            dt1.Columns.Add("sid");
            dt1.Columns.Add("sellingprice");
            dt1.Columns.Add("barcodeNo");
            dt1.Columns.Add("actualSP");
            dt1.Columns.Add("Title");
            dt1.Columns.Add("ItemCategory");
            dt1.Columns.Add("salesidgivenbyvloc");
            
            
            foreach (RepeaterItem item in rpt_Barcode.Items)
            {
                CheckBox chkdel = (CheckBox)item.FindControl("checkBoxBarcode");//assume the ID of checkbox is 'chkdel'
                if (chkdel.Checked)
                {
                    Label StockupID = (Label)item.FindControl("StockupID");
                    Label sid = (Label)item.FindControl("sid");
                    TextBox sellingprice = (TextBox)item.FindControl("sellingprice");
                    Label actualSP = (Label)item.FindControl("actualSP");
                    Label barcode = (Label)item.FindControl("barcodeNo");
                    Label Title = (Label)item.FindControl("Title");
                    Label ItemCategory = (Label)item.FindControl("ItemCategory");
                    Label salesidgivenbyvloc = (Label)item.FindControl("salesidgivenbyvloc");
                    dt1.Rows.Add(StockupID.Text, sid.Text, sellingprice.Text, barcode.Text, actualSP.Text, Title.Text, ItemCategory.Text, salesidgivenbyvloc.Text);
                }
            }

           


            //Read the first Sheet from Excel file.
            IXLWorksheet workSheet = workBook.Worksheet(1);

            //Create a new DataTable.
            DataTable dt = new DataTable();

            //Loop through the Worksheet rows.
            bool firstRow = true;
            int j = 0;
            string error = string.Empty ;
            foreach (IXLRow row in workSheet.Rows())
            {
                //Use the first row to add columns to DataTable.
                if (firstRow)
                {
                    foreach (IXLCell cell in row.Cells())
                    {
                        dt.Columns.Add(cell.Value.ToString());
                    }
                    dt.Columns.Add("Status");
                    firstRow = false;
                }
                else
                {
                    
                    IXLCell cell = row.Cell(1);
                    string barcode = cell.Value.ToString();

                    IXLCell cell1 = row.Cell(2);
                    object mrp = cell1.Value.ToString();

                    DataRow[] rows = dt1.Select("barcodeNo='" + barcode+"'");

                    dt.Rows.Add();
                    int i = 0;
                    foreach (IXLCell cells in row.Cells())
                    {
                        dt.Rows[dt.Rows.Count - 1][i] = cells.Value.ToString();                       
                        i++;
                    }
                    

                    if (!rows.Length.Equals(0))
                    {
                        rows[0]["sellingprice"] = mrp;
                        
                        dt.Rows[dt.Rows.Count - 1][i] = "Found";
                    }
                    else {
                        dt.Rows[dt.Rows.Count - 1][i] = "Not Found";
                        error += barcode + ",";
                    }
                   
                }
                j++;

            }



            // re-assign repeater
            rpt_Barcode.DataSource = new DataTable();
            rpt_Barcode.DataBind();

            rpt_Barcode.DataSource = dt1;
            rpt_Barcode.DataBind();

            /*using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "DispatchChangeSP");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                //Response.ContentType = "application / vnd.ms-excel";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fname = "DispatchChangeSP" + DateTime.Now.ToString("MM/dd/yyyy_HH:mm:ss");

                Response.AddHeader("content-disposition", "attachment;filename=" + fname + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }*/


            if (!error.Equals(""))
            {
                divError.InnerText = "Barcodes not found from excel - "+ error.Remove(error.Length - 1); ;
                divError.Visible = true;
            }

            
        }
    }
}