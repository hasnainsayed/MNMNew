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
        try
        {
            if (!IsPostBack)
            {
                BindData();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey1", "firedate();", true);
            }
        }
        catch (Exception ex)
        {
            
        }
    }

    protected void BindData()
    {
        styleCls objS = new styleCls();
        DataTable dt = objS.getTable("Vendor");
        vendorID1.DataSource = dt;
        vendorID1.DataBind();
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
        try
        {
            lblreport.Text = "Stock LR";
            lblrpt.Text = lblreport.Text;
            buttons.Visible = true;
            btnexporttoexcel.Visible = true;
            lblrpt.Visible = true;
            getData();
        }
        catch(Exception ex)
        {

        }
        //getSummary();

    }

    protected void report10_Click(object sender, EventArgs e)
    {
        try
        {
            lblreport.Text = "Sales With Trader Note";
            ModalPopupExtender1.Show();
            buttons.Visible = true;
            btnexporttoexcel.Visible = true;
        }
        catch (Exception ex)
        {

        }

    }

    protected void report11_Click(object sender, EventArgs e)
    {
        try
        {
            lblreport.Text = "Stock Location";
            ModalPopupExtender2.Show();
            buttons.Visible = true;
            btnexporttoexcel.Visible = true;
        }
        catch (Exception ex)
        {

        }

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
            DataTable getDt = new bulkReportCls().getRecord(lblreport.Text,frmDate.Text,toDate.Text,lblmin.Text,lblmax.Text,lblcmngfrm.Text,vendorID1.Text);
            int count = getDt.Rows.Count;
            int count1 = count - 1;
            if (lblreport.Text.Equals("VendorWise Stock"))
            {
                StringBuilder html2 = new StringBuilder();
                html2.Append("<table id='Header'width='100%' style='border-collapse:collapse'>");
                html2.Append("</table>");
                html2.Append("<table width='100%' style='border-collapse:collapse'>");
                html2.Append("<tr><td style='text-align: left;font-size:25px;padding-top:5px;padding-bottom: 9px;padding-left:15px;'>VendorWise Stock Summary</td></tr>");
                html2.Append("<tr><th class='report-data' style='padding-left:17px;'>Shop Total :</th><td style='padding-right:650px;'>" + getDt.Rows[count1]["Shop Total"].ToString()+"</td></tr> " +
                             "<tr><th class='report-data' style='padding-left:17px;'>WareHouse Total :</th><td style='padding-right:650px;'>" + getDt.Rows[count1]["Warehouse Total"].ToString() + "</td></tr>" +
                             "<tr><th class='report-data' style='padding-left:17px;'>LR Total :</th><td style='padding-right:650px;'>" + getDt.Rows[count1]["LR Total"].ToString() + "</td></tr>" +
                             "<tr><th class='report-data' style='padding-left:17px;'>Grand Total :</th><th style='padding-right:650px;'>" + getDt.Rows[count1]["Grand Total"].ToString() + "</th></tr>");
                html2.Append("</table>");
                PlaceHolder1.Controls.Add(new Literal { Text = html2.ToString() });
            }

            else if(lblreport.Text.Equals("VendorWise Stock Shop"))
            {
                StringBuilder html2 = new StringBuilder();
                html2.Append("<table id='Header'width='100%' style='border-collapse:collapse'>");
                html2.Append("</table>");
                html2.Append("<table width='100%' style='border-collapse:collapse'>");
                html2.Append("<tr><td style='text-align: left;font-size:25px;padding-top:5px;padding-bottom: 9px;padding-left:15px;'>VendorWise Stock Shop Summary</td></tr>");
                html2.Append("<tr><th class='report-data' style='padding-left:17px;'>Grand Total :</th><th style='padding-right:650px;'>" + getDt.Rows[count1]["Grand Total"].ToString() + "</th></tr> ");
                html2.Append("</table>");
                PlaceHolder1.Controls.Add(new Literal { Text = html2.ToString() });
            }

            else if (lblreport.Text.Equals("VendorWise Stock Warehouse"))
            {
                StringBuilder html2 = new StringBuilder();
                html2.Append("<table id='Header'width='100%' style='border-collapse:collapse'>");
                html2.Append("</table>");
                html2.Append("<table width='100%' style='border-collapse:collapse'>");
                html2.Append("<tr><td style='text-align: left;font-size:25px;padding-top:5px;padding-bottom: 9px;padding-left:15px;'>VendorWise Stock Warehouse Summary</td></tr>");
                html2.Append("<tr><th class='report-data' style='padding-left:17px;'>Grand Total :</th><th style='padding-right:650px;'>" + getDt.Rows[count1]["Grand Total"].ToString() + "</th></tr> ");
                html2.Append("</table>");
                PlaceHolder1.Controls.Add(new Literal { Text = html2.ToString() });
            }

            else if (lblreport.Text.Equals("Purchase With Margin"))
            {
                StringBuilder html2 = new StringBuilder();
                html2.Append("<table id='Header'width='100%' style='border-collapse:collapse'>");
                html2.Append("</table>");
                html2.Append("<table width='100%' style='border-collapse:collapse'>");
                html2.Append("<tr><td style='text-align: left;font-size:25px;padding-top:5px;padding-bottom: 9px;padding-left:15px;'>Purchase With Margin Summary</td></tr>");
                html2.Append("<tr><th class='report-data' style='padding-left:17px;'>Purchase Total :</th><td style='padding-right:650px;'>" + getDt.Rows[count1]["Purchase Total"].ToString() + "</td></tr>" +
                             "<tr><th class='report-data' style='padding-left:17px;'>MRP Total :</th><td style='padding-right:650px;'>" + getDt.Rows[count1]["MRP Total"].ToString() + "</td></tr>" +
                             "<tr><th class='report-data' style='padding-left:17px;'>Total Margin(In %) :</th><td style='padding-right:650px;'>" + getDt.Rows[count1]["Total Margin"].ToString() + "</td></tr>");
                html2.Append("</table>");
                PlaceHolder1.Controls.Add(new Literal { Text = html2.ToString() });
            }

            else if (lblreport.Text.Equals("Stock LR"))
            {
                StringBuilder html2 = new StringBuilder();
                html2.Append("<table id='Header'width='100%' style='border-collapse:collapse'>");
                html2.Append("</table>");
                html2.Append("<table width='100%' style='border-collapse:collapse'>");
                html2.Append("<tr><td style='text-align: left;font-size:25px;padding-top:5px;padding-bottom: 9px;padding-left:15px;'>Stock LR Summary</td></tr>");
                html2.Append("<tr><th class='report-data' style='padding-left:17px;'>Total Amount :</th><td style='padding-right:650px;'>" + getDt.Rows[count1]["Total Amount"].ToString() + "</td></tr>" +
                             "<tr><th class='report-data' style='padding-left:17px;'>Total Pieces :</th><td style='padding-right:650px;'>" + getDt.Rows[count1]["Total Pieces"].ToString() + "</td></tr>");
                html2.Append("</table>");
                PlaceHolder1.Controls.Add(new Literal { Text = html2.ToString() });
            }

            else if (lblreport.Text.Equals("Sales With Margin"))
            {
                StringBuilder html2 = new StringBuilder();
                html2.Append("<table id='Header'width='100%' style='border-collapse:collapse'>");
                html2.Append("</table>");
                html2.Append("<table width='100%' style='border-collapse:collapse'>");
                html2.Append("<tr><td style='text-align: left;font-size:25px;padding-top:5px;padding-bottom: 9px;padding-left:15px;'>Sales With Margin Summary</td></tr>");
                html2.Append("<tr><th class='report-data' style='padding-left:17px;'>Total Purchase :</th><td style='padding-right:650px;'>" + getDt.Rows[count1]["Total Purchase"].ToString() + "</td></tr>" +
                             "<tr><th class='report-data' style='padding-left:17px;'>Total Sales :</th><td style='padding-right:650px;'>" + getDt.Rows[count1]["Total Sales"].ToString() + "</td></tr>" +
                             "<tr><th class='report-data' style='padding-left:17px;'>Total Margin(In %) :</th><td style='padding-right:650px;'>" + getDt.Rows[count1]["Total Margin"].ToString() + "</td></tr>");
                html2.Append("</table>");
                PlaceHolder1.Controls.Add(new Literal { Text = html2.ToString() });
            }

            else if (lblreport.Text.Equals("Sales For Warehouse"))
            {
                StringBuilder html2 = new StringBuilder();
                html2.Append("<table id='Header'width='100%' style='border-collapse:collapse'>");
                html2.Append("</table>");
                html2.Append("<table width='100%' style='border-collapse:collapse'>");
                html2.Append("<tr><td style='text-align: left;font-size:25px;padding-top:5px;padding-bottom: 9px;padding-left:15px;'>Sales For Warehouse Summary</td></tr>");
                html2.Append("<tr><th class='report-data' style='padding-left:17px;'>Total Purchase :</th><td style='padding-right:650px;'>" + getDt.Rows[count1]["Total Purchase"].ToString() + "</td></tr>" +
                             "<tr><th class='report-data' style='padding-left:17px;'>Total Sales :</th><td style='padding-right:650px;'>" + getDt.Rows[count1]["Total Sales"].ToString() + "</td></tr>" +
                             "<tr><th class='report-data' style='padding-left:17px;'>Total Margin(In %) :</th><td style='padding-right:650px;'>" + getDt.Rows[count1]["Total Margin"].ToString() + "</td></tr>");
                html2.Append("</table>");
                PlaceHolder1.Controls.Add(new Literal { Text = html2.ToString() });
            }

            else if (lblreport.Text.Equals("Sales For Shop"))
            {
                StringBuilder html2 = new StringBuilder();
                html2.Append("<table id='Header'width='100%' style='border-collapse:collapse'>");
                html2.Append("</table>");
                html2.Append("<table width='100%' style='border-collapse:collapse'>");
                html2.Append("<tr><td style='text-align: left;font-size:25px;padding-top:5px;padding-bottom: 9px;padding-left:15px;'>Sales For Shop Summary</td></tr>");
                html2.Append("<tr><th class='report-data' style='padding-left:17px;'>Total Purchase :</th><td style='padding-right:650px;'>" + getDt.Rows[count1]["Total Purchase"].ToString() + "</td></tr>" +
                             "<tr><th class='report-data' style='padding-left:17px;'>Total Sales :</th><td style='padding-right:650px;'>" + getDt.Rows[count1]["Total Sales"].ToString() + "</td></tr>" +
                             "<tr><th class='report-data' style='padding-left:17px;'>Total Margin(In %) :</th><td style='padding-right:650px;'>" + getDt.Rows[count1]["Total Margin"].ToString() + "</td></tr>");
                html2.Append("</table>");
                PlaceHolder1.Controls.Add(new Literal { Text = html2.ToString() });
            }

            else if (lblreport.Text.Equals("Sales With Trader Note"))
            {
                StringBuilder html2 = new StringBuilder();
                html2.Append("<table id='Header'width='100%' style='border-collapse:collapse'>");
                html2.Append("</table>");
                html2.Append("<table width='100%' style='border-collapse:collapse'>");
                html2.Append("<tr><td style='text-align: left;font-size:25px;padding-top:5px;padding-bottom: 9px;padding-left:15px;'>Sales With Trader Note Summary</td></tr>");
                html2.Append("<tr><th class='report-data' style='padding-left:17px;'>Total Purchase :</th><td style='padding-right:650px;'>" + getDt.Rows[count1]["Total Purchase"].ToString() + "</td></tr>" +
                             "<tr><th class='report-data' style='padding-left:17px;'>Total Sales :</th><td style='padding-right:650px;'>" + getDt.Rows[count1]["Total Sales"].ToString() + "</td></tr>" +
                             "<tr><th class='report-data' style='padding-left:17px;'>Total Margin(In %) :</th><td style='padding-right:650px;'>" + getDt.Rows[count1]["Total Margin"].ToString() + "</td></tr>");
                html2.Append("</table>");
                PlaceHolder1.Controls.Add(new Literal { Text = html2.ToString() });
            }

            else if (lblreport.Text.Equals("Stock Location"))
            {
                StringBuilder html2 = new StringBuilder();
                html2.Append("<table id='Header'width='100%' style='border-collapse:collapse'>");
                html2.Append("</table>");
                html2.Append("<table width='100%' style='border-collapse:collapse'>");
                html2.Append("<tr><td style='text-align: left;font-size:25px;padding-top:5px;padding-bottom: 9px;padding-left:15px;'>Stock Location Summary</td></tr>");
                html2.Append("<tr><th class='report-data' style='padding-left:17px;'>Total Amount :</th><td style='padding-right:650px;'>" + getDt.Rows[count1]["Amount"].ToString() + "</td></tr>" +
                             "<tr><th class='report-data' style='padding-left:17px;'>Total Quantity :</th><td style='padding-right:650px;'>" + getDt.Rows[count1]["Quantity"].ToString() + "</td></tr>");
                html2.Append("</table>");
                PlaceHolder1.Controls.Add(new Literal { Text = html2.ToString() });
            }
            string html1 = ConvertDataTableToHTML(getDt);
            PlaceHolder2.Controls.Add(new Literal { Text = html1.ToString() });
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
            DataTable getDt = new bulkReportCls().getRecord(lblreport.Text, frmDate.Text, toDate.Text,lblmin.Text, lblmax.Text, lblcmngfrm.Text, vendorID1.Text);
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

    protected void btnGetRpt2_Click(object sender, EventArgs e)
    {
        int min=0;
        int max=0;
        string commingfrom = "";
        if(drpage.SelectedValue.Equals("0-30"))
        {
            min = 0;
            max = 30;
            commingfrom = "Less30";

        }
        else if (drpage.SelectedValue.Equals("31-60"))
        {
            min = 31;
            max = 60;
            commingfrom = "Normal";

        }
        else if (drpage.SelectedValue.Equals("61-90"))
        {
            min = 61;
            max = 90;
            commingfrom = "Normal";

        }
        else if (drpage.SelectedValue.Equals("91-120"))
        {
            min = 91;
            max = 120;
            commingfrom = "Normal";

        }
        else if (drpage.SelectedValue.Equals("121-150"))
        {
            min = 121;
            max = 150;
            commingfrom = "Normal";

        }
        else if (drpage.SelectedValue.Equals("151-180"))
        {
            min = 151;
            max = 180;
            commingfrom = "Normal";

        }
        else if (drpage.SelectedValue.Equals("181-240"))
        {
            min = 181;
            max = 240;
            commingfrom = "Normal";

        }
        else if (drpage.SelectedValue.Equals("241-300"))
        {
            min = 241;
            max = 300;
            commingfrom = "Normal";

        }
        else if (drpage.SelectedValue.Equals("301-360"))
        {
            min = 301;
            max = 360;
            commingfrom = "Normal";

        }
        else if (drpage.SelectedValue.Equals("360+"))
        {
            min = 0;
            max = 360;
            commingfrom = "More306";

        }
        lblmin.Text = min.ToString();
        lblmax.Text = max.ToString();
        lblcmngfrm.Text = commingfrom;
        lblrpt.Text = lblreport.Text;
        lblrpt.Visible = true;
        getData();
    }
}