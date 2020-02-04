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


public partial class bulkSales : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                locationCls lobj = new locationCls();
                DataTable loc = lobj.getVirtualLocation("2");
                virtualLocation.DataSource = loc;
                virtualLocation.DataBind();

                styleCls sobj = new styleCls();
                DataTable state = sobj.getTable("stateCode");
                stateID.DataSource = state;
                stateID.DataBind();

                paymentMode.SelectedValue = "cod";
                salesDate.Text = DateTime.Now.ToString("MM/dd/yyyy");

                totalBarcodes.Text = "0";
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "firedate();", true);
            }
            
        }
        catch(Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }
        
    protected void removeBarcode_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            int index = rp1.ItemIndex;
            DataTable dtBarcode = new DataTable();
            dtBarcode.Columns.Add("barcode");
            foreach (RepeaterItem itemEquipment in rpt_Barcode.Items)
            {

                Label barcode = (Label)itemEquipment.FindControl("barcode");
                if(!itemEquipment.ItemIndex.Equals(index))
                {
                    dtBarcode.Rows.Add(barcode.Text); // Add Data 
                }
            }
            rpt_Barcode.DataSource = dtBarcode;
            rpt_Barcode.DataBind();

            totalBarcodes.Text = dtBarcode.Rows.Count.ToString();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void btnAddToRepeater_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtBarcode = new DataTable();
            dtBarcode.Columns.Add("barcode");

            foreach (RepeaterItem itemEquipment in rpt_Barcode.Items)
            {

                Label barcode = (Label)itemEquipment.FindControl("barcode");
                dtBarcode.Rows.Add(barcode.Text); // Add Data 
                
                

            }
            String searchBarcode = barcodeTxt.Text;
            if(!searchBarcode.Equals(""))
            {
                bool contains = dtBarcode.AsEnumerable().Any(row => searchBarcode == row.Field<String>("barcode"));

                if (contains.Equals(false))
                {
                    if (Session["uType"].ToString().Equals("1")) // warehouse
                    {
                        dtBarcode.Rows.Add(barcodeTxt.Text);
                    }
                    else
                    {
                        // function to check physicalLocation
                        Boolean? check = checkBarcodeLocation(searchBarcode, Session["physicalLocation"].ToString());
                        if (check.Equals(true))
                        {
                            dtBarcode.Rows.Add(barcodeTxt.Text);
                        }
                        else if (check.Equals(null))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showqerr", "alert('Exception Occured');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showlocdiff", "alert('Barcode at different Loaction or marked Sold');", true);
                        }
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showBarcodealert", "alert('Barcode already Scanned');", true);
                }

                rpt_Barcode.DataSource = dtBarcode;
                rpt_Barcode.DataBind();

                totalBarcodes.Text = dtBarcode.Rows.Count.ToString();
            }
            
            barcodeTxt.Text = string.Empty;                   
            barcodeTxt.Focus();
           
            //string b = barcodeTxt.Text;
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    public Boolean? checkBarcodeLocation(string barcode, string physicalLocation)
    {
        try
        {
            
            bulkUploads obj = new bulkUploads();
            Boolean? check = obj.checkBarcodeLocation(barcode, physicalLocation);
            return check;
        }
         catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
            return Convert.ToBoolean(null);
        }
    }

    protected void saveBulkSales_Click(object sender, EventArgs e)
    {
        try
        {
            string err = string.Empty;
            DataTable dtBarcode = new DataTable();
            dtBarcode.Columns.Add("barcode");

            foreach (RepeaterItem itemEquipment in rpt_Barcode.Items)
            {

                Label barcode = (Label)itemEquipment.FindControl("barcode");
                dtBarcode.Rows.Add(barcode.Text); // Add Data 

            }

            // check for all sales details added
            if (salesId.Text.Equals(""))
            {
                err += "Please Enter Sales ID" + Environment.NewLine;
            }

            // check for scanned barcodes
            if (dtBarcode.Rows.Count.Equals(0))
            {
                err += "Please Scan Atleast ONE Barcode";
            }
            
            if(!err.Equals(""))
            {
                divError.InnerHtml = err;
                divError.Visible = true;
            }
            else
            {
                bulkUploads obj = new bulkUploads();

                // check if any one barcode marked as sold
                DataTable soldBarcode = obj.getSoldBarcode(dtBarcode);
                if(soldBarcode.Equals(null))
                {
                    divError.InnerText = "Exception Occured. Not saved";
                    divError.Visible = true;
                }
                else if(!soldBarcode.Rows.Count.Equals(0))
                {
                    divError.InnerText = "Not saved . Please refer Excel for Issues";
                    divError.Visible = true;
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(soldBarcode, "BulkScanSales");
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        //Response.ContentType = "application / vnd.ms-excel";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        string fname = "BulkSalesSave_" + DateTime.Now.ToString("MM/dd/yyyy_HH:mm:ss");

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
                //if(!soldBarcode.Equals(""))
                //{
                //    if(soldBarcode.Equals("-1"))
                //    {
                //        divError.InnerText = "Some Error Occured";
                //    }
                //    else
                //    {
                //        divError.InnerHtml = soldBarcode;
                //    }                    
                    
                //}
                else
                {
                    // else do bulk save - add bulk invoice type in invoice table
                    int success = obj.doBulkSales(dtBarcode,salesId.Text, 
                        custname.Text, address1.Text, address2.Text, 
                        city.Text, stateID.SelectedValue, virtualLocation.SelectedValue, paymentMode.SelectedValue, salesDate.Text,phoneNo.Text);
                    if (success.Equals(-1))
                    {
                        Session["salesSuccfail"] = "Invoice Generation Failed";

                    }
                    else if (success.Equals(-2))
                    {
                        Session["salesSuccfail"] = "Duplicate Invoice Generation Prevented";

                    }
                    else if (success.Equals(1))
                    {
                        Session["salesSuccfail"] = "Invoice Generated Successfully";
                    }
                    else
                    {
                        Session["salesSuccfail"] = "Some Error";
                    }
                    Response.Redirect("invoice.aspx", true);
                }



            }

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void uploadExcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUpload1.HasFile)
            {
                //Create a new DataTable.
                DataTable dt = new DataTable();
                dt.Columns.Add("barcode");
                dt.Columns.Add("Status");

                foreach (RepeaterItem itemEquipment in rpt_Barcode.Items)
                {

                    Label barcode = (Label)itemEquipment.FindControl("barcode");
                    dt.Rows.Add(barcode.Text); // Add Data 

                }

                bool checkDuplicate = false;
                string dupBarcodes = string.Empty;
                using (XLWorkbook workBook = new XLWorkbook(FileUpload1.PostedFile.InputStream))
                {
                    //Read the first Sheet from Excel file.
                    IXLWorksheet workSheet = workBook.Worksheet(1);

                    

                    //Loop through the Worksheet rows.
                    bool firstRow = true;
                   
                    foreach (IXLRow row in workSheet.Rows())
                    {
                        //Use the first row to add columns to DataTable.
                        if (firstRow)
                        {
                            // dont add frst row ie header
                            /*foreach (IXLCell cell in row.Cells())
                            {
                                dt.Columns.Add(cell.Value.ToString());
                            }*/
                            firstRow = false;
                        }
                        else
                        {
                            //Add rows to DataTable.
                            dt.Rows.Add();
                            int i = 0;
                            foreach (IXLCell cell in row.Cells())
                            {

                                String searchBarcode = cell.Value.ToString();
                                bool contains = dt.AsEnumerable().Any(rows => cell.Value.ToString() == rows.Field<String>("barcode"));

                                Boolean? check = true;
                                if (!Session["uType"].ToString().Equals("1")) // warehouse
                                {
                                    // function to check physicalLocation
                                    check = checkBarcodeLocation(searchBarcode, Session["physicalLocation"].ToString());
                                }
                                
                                if (contains.Equals(false) && check.Equals(true))
                                {
                                    dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                                    dt.Rows[dt.Rows.Count - 1][1] = "";
                                }
                                else
                                {                                   
                                    if (contains.Equals(true))
                                    {
                                        dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                                        dt.Rows[dt.Rows.Count - 1][1] = "Duplicate";
                                    }
                                    else if (check.Equals(null))
                                    {
                                        dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                                        dt.Rows[dt.Rows.Count - 1][1] = "Exception Occured";
                                    }
                                    else if (check.Equals(false))
                                    {
                                        dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                                        dt.Rows[dt.Rows.Count - 1][1] = "Not in Physical Loaction or marked SOLD";
                                    }
                                    checkDuplicate = true;
                                    //dupBarcodes += ','+ cell.Value.ToString();
                                    //break;
                                }
                                //dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();

                                //cell.Value.ToString();
                                i++;
                            }
                        }


                    }
                }
               
                if(checkDuplicate.Equals(false))
                {
                    rpt_Barcode.DataSource = dt;
                    rpt_Barcode.DataBind();

                    totalBarcodes.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, "BulkScanSales");
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        //Response.ContentType = "application / vnd.ms-excel";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        string fname = "BulkSalesScan_" + DateTime.Now.ToString("MM/dd/yyyy_HH:mm:ss");

                        Response.AddHeader("content-disposition", "attachment;filename=" + fname + ".xlsx");
                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "duplicateValues", "alert('Issue in Uploading Excel.Please Check Downloaded Excel');", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "selectFile", "alert('Please Select Excel File');", true);
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }


}