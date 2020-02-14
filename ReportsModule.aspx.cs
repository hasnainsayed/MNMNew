using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DataBase;
using System.Text;
using System.IO;
using ClosedXML.Excel;

public partial class ReportsModule : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey1", "firedate();", true);
    }

    protected void BindData()
    {

    }

    protected void report1_Click(object sender, EventArgs e)
    {
        try
        { 
            lblreport.Text = "VendorWise Stock";
            lblrpt.Text = lblreport.Text;
            buttons.Visible = true;
            btnexporttoexcel.Visible = true;
            lblrpt.Visible = true;
            getData();
            //getSummary();
        }
        catch (Exception ex)
        {

        }
    }

    protected void report2_Click(object sender, EventArgs e)
    {
        try
        { 
            lblreport.Text = "Stock";
            lblrpt.Text = lblreport.Text;
            buttons.Visible = true;
            btnexporttoexcel.Visible = true;
            lblrpt.Visible = true;
            getData();
           // getSummary();
        }
        catch (Exception ex)
        {

        }
    }

    protected void report3_Click(object sender, EventArgs e)
    {
        try
        {
            lblreport.Text = "VendorWise Stock Shop";
            lblrpt.Text = lblreport.Text;
            buttons.Visible = true;
            btnexporttoexcel.Visible = true;
            lblrpt.Visible = true;
            getData();
            //getSummary();
        }
        catch (Exception ex)
        {

        }
        
    }

    protected void report4_Click(object sender, EventArgs e)
    {
        try
        {
            lblreport.Text = "VendorWise Stock Warehouse";
            lblrpt.Text = lblreport.Text;
            buttons.Visible = true;
            btnexporttoexcel.Visible = true;
            lblrpt.Visible = true;
            getData();
            //getSummary();
        }
        catch(Exception ex)
        {

        }
        

    }

    protected void report5_Click(object sender, EventArgs e)
    {
        try
        {
            lblreport.Text = "Sales With Margin";
            ModalPopupExtender1.Show();
            buttons.Visible = true;
            btnexporttoexcel.Visible = true;
        }
        catch (Exception ex)
        {

        }
        

    }

    protected void report6_Click(object sender, EventArgs e)
    {
        try
        {
            lblreport.Text = "Sales For Warehouse";
            ModalPopupExtender1.Show();
            buttons.Visible = true;
            btnexporttoexcel.Visible = true;
        }
        catch (Exception ex)
        {

        }
        

    }

    protected void report7_Click(object sender, EventArgs e)
    {
        try
        {
            lblreport.Text = "Sales For Shop";
            ModalPopupExtender1.Show();
            buttons.Visible = true;
            btnexporttoexcel.Visible = true;
        }
        catch (Exception ex)
        {

        }
        

    }

    protected void report8_Click(object sender, EventArgs e)
    {
        try
        {
            lblreport.Text = "Purchase With Margin";
            lblrpt.Text = lblreport.Text;
            buttons.Visible = true;
            btnexporttoexcel.Visible = true;
            lblrpt.Visible = true;
            getData();
            //getSummary();  
        }
        catch (Exception ex)
        {

        }
        

    }

    protected void report9_Click(object sender, EventArgs e)
    {
        lblreport.Text = "Report 9";
        lblrpt.Visible = false;
        getData();
        //getSummary();
        
    }

    protected void report10_Click(object sender, EventArgs e)
    {
        lblreport.Text = "Report 10";
        lblrpt.Visible = false;
        getData();
        //getSummary();
        
    }

    protected void report11_Click(object sender, EventArgs e)
    {
        lblreport.Text = "Report 11";
        lblrpt.Visible = false;
        getData();
        //getSummary();
        
    }

    protected void report12_Click(object sender, EventArgs e)
    {
        lblreport.Text = "Report 12";
        lblrpt.Visible = false;
        getData();
        //getSummary();
       
    }

    protected void getData()
    {
        try
        {
            //StringBuilder html1 = getdataStream();
            DataTable getDt = new bulkReportCls().getRecord(lblreport.Text,frmDate.Text,toDate.Text);
            string html1 = ConvertDataTableToHTML(getDt);
            PlaceHolder2.Controls.Add(new Literal { Text = html1.ToString() });
            //txtarea.InnerHtml = html1.ToString();
            Session["htmlstring"] = html1.ToString();
        }
        catch (Exception ex)
        {
        }
    }

    public static string ConvertDataTableToHTML(DataTable dt)
    {
        string html = "<table  class='table table-hover' id='Header' width='100%' style='border-collapse:collapse' >";
        //add header row
        html += "<tr>";
        for (int i = 0; i < dt.Columns.Count; i++)
            html += "<th class='report-data' style='padding-left:17px;'>" + dt.Columns[i].ColumnName + "</th>";
        html += "</tr>";
        //add rows
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            html += "<tr>";
            for (int j = 0; j < dt.Columns.Count; j++)
                html += "<td style='padding-left:17px;'>" + dt.Rows[i][j].ToString() + "</td>";
            html += "</tr>";
        }
        html += "</table>";
        return html;
    }

    //private StringBuilder getdataStream()
    //{
    //    StringBuilder html1 = new StringBuilder();
    //    html1.Append("<table id='Header'width='100%' style='border-collapse:collapse'>");
    //    html1.Append("</table>");
    //    html1.Append("<table width='100%' style='border-collapse:collapse'>");
    //    html1.Append("<tr><td style='text-align: left;font-size:25px;padding-top:5px;padding-left:15px;'>Report 1</td></tr>");
    //    html1.Append("<tr><th style='padding-left:17px;'>Barcode No. </th> " +
    //                     "<th>Style Code </th> " +
    //                     "<th>Item Category </th> " +
    //                     "<th>SKU </th>" +
    //                     "<th>Lot No.</th>" +
    //                     "<th>Brand</th></tr>");
    //    html1.Append("<tr><td style='padding-left:17px;'>08AD/12/007</td><td>08AD</td><td>Tops</td><td>08AD-12</td><td>Old Bora</td><td>DZV:DreamzVision</td></tr> " +
    //                 "<tr><td style='padding-left:17px;'>08AD/11/002</td><td>08AD</td><td>Tops</td><td>08AD-11</td><td>Old Bora</td><td>DZV:DreamzVision</td></tr>" +
    //                 "<tr><td style='padding-left:17px;'>08AD/11/003</td><td>08AD</td><td>Tops</td><td>08AD-11</td><td>Old Bora</td><td>DZV:DreamzVision</td></tr>" +
    //                 "<tr><td style='padding-left:17px;'>08AD/12/000</td><td>08AD</td><td>Tops</td><td>08AD-12</td><td>Old Bora</td><td>DZV:DreamzVision</td></tr>" +
    //                 "<tr><td style='padding-left:17px;'>08AD/12/001</td><td>08AD</td><td>Tops</td><td>08AD-12</td><td>Old Bora</td><td>DZV:DreamzVision</td></tr>");
    //    html1.Append("</table>");
    //    return html1;
    //}
    //protected void getSummary()
    //{
    //    try
    //    {
    //        StringBuilder html2 = getSummaryStream();
    //        PlaceHolder1.Controls.Add(new Literal { Text = html2.ToString() });
    //        //txtarea.InnerHtml = html1.ToString();
    //        Session["htmlstring"] = html2.ToString();
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //}

    //private StringBuilder getSummaryStream()
    //{
    //    StringBuilder html2 = new StringBuilder();
    //    html2.Append("<table id='Header'width='100%' style='border-collapse:collapse'>");
    //    html2.Append("</table>");
    //    html2.Append("<table width='100%' style='border-collapse:collapse'>");
    //    html2.Append("<tr><td style='text-align: left;font-size:25px;padding-top:5px;padding-left:15px;'> Summary</td></tr>");
    //    //html2.Append("<tr><th style='padding-left:17px;'>Total Invoices </th> " +
    //    //                 "<th>Total Selling Price</th> " +
    //    //                 "<th>Item Category </th> " +
    //    //                 "<th>SKU </th>" +
    //    //                 "<th>Lot No.</th>" +
    //    //                 "<th>Brand</th></tr>");
    //    html2.Append("<tr><th style='padding-left:17px;'>Total Invoices :</th><td style='padding-right:650px;'>05</td></tr> " +
    //                 "<tr><th style='padding-left:17px;'>Total Selling Price :</th><td style='padding-right:650px;'>27500</td></tr>" +
    //                 "<tr><th style='padding-left:17px;'>XYZ :</th><td style='padding-right:650px;'>000</td></tr>" +
    //                 "<tr><th style='padding-left:17px;'>XYZ :</th><td style='padding-right:650px;'>000</td></tr>" +
    //                 "<tr><th style='padding-left:17px;'>XYZ :</th><td style='padding-right:650px;'>000</td></tr>");
    //    html2.Append("</table>");
    //    return html2;
    //}

    protected void btnGetRpt_Click(object sender, EventArgs e)
    {
        try
        {
            lblrpt.Text = lblreport.Text;
            lblrpt.Visible = true;
            getData();
            //getSummary();
        }
        catch (Exception ex)
        {

        }
        
    }

    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
        try
        {

            bulkReportCls obj = new bulkReportCls();
            DataTable getDt = new bulkReportCls().getRecord(lblreport.Text, frmDate.Text, toDate.Text);
            using (XLWorkbook wb = new XLWorkbook())
            {

                wb.Worksheets.Add(getDt, lblrpt.Text);
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + lblrpt.Text  + DateTime.Now + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }

        }
        catch (Exception ex)
        {

        }
    }
}