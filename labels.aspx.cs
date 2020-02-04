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
using System.Net;

public partial class labels : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindData();
                if (Session["labelSuccfail"] != null)
                {
                    if (Session["labelSuccfail"].ToString().Equals("Labels"))
                    {
                        divSucc.InnerText = Session["labelSuccfail"].ToString();
                        divSucc.Visible = true;
                        divError.Visible = false;
                        Session.Remove("labelSuccfail");
                    }
                    else
                    {
                        divError.InnerText = Session["labelSuccfail"].ToString();
                        divError.Visible = true;
                        divSucc.Visible = false;
                        Session.Remove("labelSuccfail");
                    }

                }
            }
            else
            {
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
            styleCls obj = new styleCls();
            brand.DataSource = obj.getTable("Column1");
            brand.DataBind();
            brand.Items.Insert(0, new ListItem("--- Select Brand ---", "-1"));

            login.DataSource = obj.getTable("login");
            login.DataBind();
            login.Items.Insert(0, new ListItem("--- Select User ---", "-1"));

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void searchLabels_Click(object sender, EventArgs e)
    {
        try
        {
            rpt_Barcode.DataSource = new DataTable();
            rpt_Barcode.DataBind();
            dispalyBarcodes.Visible = false;
            if (brand.SelectedValue.Equals("-1"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Brand !');", true);
            }
            else
            {
                labelCls obj = new labelCls();
                DataTable dt = obj.getLabels(brand.SelectedValue,login.SelectedValue);
                rpt_Barcode.DataSource = dt;
                rpt_Barcode.DataBind();
                dispalyBarcodes.Visible = true;
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void selectDeselect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            bool checks = selectDeselect.Checked;            
            foreach (RepeaterItem itemEquipment in rpt_Barcode.Items)
            {
                CheckBox checkId = (CheckBox)itemEquipment.FindControl("checkBarcode");
                checkId.Checked = checks;
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void btnSaveSuccess_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("StockupID");
            dt.Columns.Add("ArchiveStockupID");
            dt.Columns.Add("BarcodeNo");
            foreach (RepeaterItem itemEquipment in rpt_Barcode.Items)
            {
                CheckBox checkId = (CheckBox)itemEquipment.FindControl("checkBarcode");
                if(checkId.Checked)
                {
                    Label StockupID = (Label)itemEquipment.FindControl("StockupID");
                    Label ArchiveStockupID = (Label)itemEquipment.FindControl("ArchiveStockupID");
                    Label BarcodeNo = (Label)itemEquipment.FindControl("BarcodeNo");
                    dt.Rows.Add(StockupID.Text, ArchiveStockupID.Text, BarcodeNo.Text);
                }
            }
            if(dt.Rows.Count.Equals(0))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Atleast one Barcode !');", true);
            }
            else
            {
                labelCls obj = new labelCls();
                DataTable excelDt = obj.getLabelDetails(dt, brand.SelectedValue);
                if (!excelDt.Rows.Count.Equals(0))
                {
                    int success = obj.updateStockLabel(dt);
                    
                    if (success.Equals(-1))
                    {
                        rpt_Barcode.DataSource = new DataTable();
                        dispalyBarcodes.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertb", "alert('Error Occured in Execution !');", true);
                    }
                    else
                    {
                        rpt_Barcode.DataSource = new DataTable();
                        dispalyBarcodes.Visible = false;

                        excelDt.Columns.Remove("StyleID");
                        excelDt.Columns.Remove("SizeID");
                        excelDt.Columns.Remove("BarcodeNo");
                        if(brand.SelectedValue.Equals("2"))
                        {
                            excelDt.Columns.Remove("EAN");
                            excelDt.Columns.Remove("Made");
                            excelDt.Columns.Remove("Gender");
                            excelDt.Columns.Remove("MFg_Name");
                        }
                        
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(excelDt, "BrandLabels");
                            Response.Clear();
                            Response.Buffer = true;
                            Response.Charset = "";
                            //Response.ContentType = "application / vnd.ms-excel";
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            string fname = brand.SelectedItem + "_Labels_" + DateTime.Now.ToString("MM/dd/yyyy_HH:mm:ss");

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
                else
                {
                    rpt_Barcode.DataSource = new DataTable();
                    dispalyBarcodes.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", "alert('Error Occured in Execution !');", true);
                }
                
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
            rpt_Barcode.DataSource = new DataTable();
            dispalyBarcodes.Visible = false;
        }
        Response.Redirect("labels.aspx", true);
    }
}