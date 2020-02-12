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

public partial class shopTransferReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnGetRpt_Click(object sender, EventArgs e)
    {
        try
        { 
           getData();
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
        try
        {

            bulkTransferReportCls obj = new bulkTransferReportCls();
            DataTable getDt = new bulkTransferReportCls().getRecords(frmDate.Text, toDate.Text);
            using (XLWorkbook wb = new XLWorkbook())
            {

                wb.Worksheets.Add(getDt, "Shop Transfer Report");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=Shop Transfer Report"  + DateTime.Now + ".xlsx");
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

    protected void getData()
    {
        try
        {
            //StringBuilder html1 = getdataStream();
            DataTable getDt = new bulkTransferReportCls().getRecords(frmDate.Text, toDate.Text);
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
}