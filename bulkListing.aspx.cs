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

public partial class bulkListing : System.Web.UI.Page
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
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void BindData()
    {
        try
        {
            locationCls obj = new locationCls();
            DataTable dt = obj.getVirtualLocation("2");
            virtualLocation.DataSource = dt;
            virtualLocation.DataBind();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void uploadCSV_Click(object sender, EventArgs e)
    {
        try
        {
            /*string extension = System.IO.Path.GetExtension(FileUpload1.FileName);
            if (!extension.Equals("csv"))
            {
                divError.InnerText = "Only CSV File Allowed";
                divError.Visible = true;
            }*/
            //else { 
            //Read the contents of CSV file.
            System.IO.StreamReader myReader = new System.IO.StreamReader(FileUpload1.PostedFile.InputStream);
            string csvData = myReader.ReadToEnd();

            //Create a DataTable.
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3] { new DataColumn("SKU", typeof(string)),
            new DataColumn("ListID", typeof(string)),
            new DataColumn("Price",typeof(string)) });

            //Execute a loop over the rows.
            foreach (string row in csvData.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    dt.Rows.Add();
                    int i = 0;

                    //Execute a loop over the columns.
                    foreach (string cell in row.Split(','))
                    {
                        dt.Rows[dt.Rows.Count - 1][i] = cell;
                        i++;
                    }
                }
            }
            bulkUploads obj = new bulkUploads();
            DataTable succDT = obj.doBulkListing(dt, virtualLocation.SelectedValue);
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(succDT, "BulkListStats");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                //Response.ContentType = "application / vnd.ms-excel";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fname = virtualLocation.SelectedItem+"_List_"  + DateTime.Now.ToString("MM/dd/yyyy_HH:mm:ss");

                Response.AddHeader("content-disposition", "attachment;filename=" + fname + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            divInsert.InnerText = "File Downloaded with Status of Listing";
            divInsert.Visible = true;
            /*if (!succDT.Rows[0]["Error"].Equals(""))
            {
                divError.InnerText = "Errors : " + succDT.Rows[0]["Error"].ToString();
                divError.Visible = true;
            }
            if (!succDT.Rows[0]["Insert"].Equals(""))
            {
                divInsert.InnerText = "Inserted SKU : " + succDT.Rows[0]["Insert"].ToString();
                divInsert.Visible = true;
            }
            if (!succDT.Rows[0]["Update"].Equals(""))
            {
                divUpdate.InnerText = "Updated : " + succDT.Rows[0]["Update"].ToString();
                divUpdate.Visible = true;
            }*/

        }
        
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }
}