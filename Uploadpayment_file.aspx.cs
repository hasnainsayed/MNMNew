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

public partial class Uploadpayment_file : System.Web.UI.Page
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
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "firedtSearch();", true);
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
    protected void btnuploadcsv_Click(object sender, EventArgs e)
    {
        try
        {

            storelocation.Text = virtualLocation.SelectedItem.Text;
            DataTable dtupldt = new DataTable();
            if (rbttype.SelectedValue.Equals("csv"))
            {
                dtupldt = ReadCsvFile();
            }
            else if(rbttype.SelectedValue.Equals("excel"))
            {
                dtupldt = Readexcel();
            }
           
            VirtualLoactionCls obj = new VirtualLoactionCls();
            DataTable dtset = obj.BindSettingbyvlocid(virtualLocation.SelectedValue);

            DataTable dtc = obj.selectColums();

            DataTable dtcre = new DataTable();
            dtcre = dtupldt.Copy();
            DataTable dtclone = dtc.Clone();
            foreach (DataColumn dc in dtupldt.Columns)
            {
                string sucess = "false";
                if (sucess.Equals("false"))
                {
                    string d = Convert.ToString(dc);
                    string p = Convert.ToString(dtupldt.Rows[0][dc]);
                    for (int i = 0; i < dtset.Columns.Count; i++)
                    {

                        string column;
                        column = dtset.Columns[i].Caption;

                        
                        if (dc.ToString().Equals(dtset.Rows[0][column].ToString()))
                        {

                            dtcre.Columns[dc.ToString()].ColumnName = column;
                            dtcre.AcceptChanges();
                            sucess = "true";
                            break;
                        }

                        else
                        {
                            sucess = "false";
                        }

                    }
                }
            }
            int cunt = dtcre.Rows.Count;
            Payment_fileCls obj1 = new Payment_fileCls();
            DataTable dt = obj1.insertpaymentfile(dtcre, virtualLocation.SelectedValue,"");
            
            if(!dt.Equals(null))
            {
                if(!dt.Rows.Count.Equals(0))
                {
                    rptsales.DataSource = dt;
                    
                    rptsales.DataBind();
                    ViewState["duplicatetransforinsrt"] = dt;
                    
                    sales.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Following Transaction Are Duplicate  !');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('File Uploded !');", true);
                    
                }
                
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
    public DataTable ReadCsvFile()
    {
        DataTable dtCsv = new DataTable();
        try
        {
           
            string Fulltext;
            if (FileUpload1.HasFile)
            {

                
                System.IO.StreamReader myReader = new System.IO.StreamReader(FileUpload1.PostedFile.InputStream);

                    Fulltext = myReader.ReadToEnd().ToString(); //read full file text  
                    string[] rows = Fulltext.Split('\n'); //split full file text into rows  
                    for (int i = 0; i < rows.Count() - 1; i++)
                    {
                        string[] rowValues = rows[i].Split(','); //split each row with comma to get individual values  
                        {
                            if (i == 0)
                            {
                                for (int j = 0; j < rowValues.Count(); j++)
                                {
                                    dtCsv.Columns.Add(rowValues[j]); //add headers  
                                }
                            }
                            else
                            {
                                DataRow dr = dtCsv.NewRow();
                                for (int k = 0; k < rowValues.Count(); k++)
                                {
                                    dr[k] = rowValues[k].ToString();
                                }
                                dtCsv.Rows.Add(dr); //add other rows  
                            }
                        }
                    }
            }
        }
        


        
        catch (Exception ex)
        {

            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
            return null;
        }
        return dtCsv;
    }
    public DataTable Readexcel()
    {
        DataTable dt = new DataTable();
        try
        {
            using (XLWorkbook workBook = new XLWorkbook(FileUpload1.PostedFile.InputStream))
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
        catch (Exception ex)
        {

            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
            return null;
        }
        return dt;
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = (DataTable)ViewState["duplicatetransforinsrt"];
            Payment_fileCls obj1 = new Payment_fileCls();
            DataTable table = obj1.insertpaymentfile(dt, virtualLocation.SelectedValue,"comingfromduplicate");
            if(!table.Equals(null))
            {
                sales.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('File Uploded !');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Failed !');", true);
            }
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
            DataTable dt= (DataTable)ViewState["duplicatetransforinsrt"];
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "DuplicateTrans");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                //Response.ContentType = "application / vnd.ms-excel";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fname = "DuplicateTrans_Payment_file_" + DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss");

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

   

    protected void rptsales_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            Label lblloaction = e.Item.FindControl("lblloaction") as Label;
            lblloaction.Text = virtualLocation.SelectedItem.Text;
        }
        catch (Exception ex)
        {
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }
    }

    

    
}