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

public partial class salesNdispatchdump : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
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

    protected void salesndispatchdump_Click(object sender, EventArgs e)
    {
        try
        {
            reportCls obj = new reportCls();
            DataTable dt = obj.getDump(string.Empty);
            DataTable dt1 = obj.getSoldDump(string.Empty);
            DataTable dt2 = obj.getSalesDumpArchive(string.Empty);
            dt.Merge(dt1);
            dt.Merge(dt2);
           
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Stock&Dispatch");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                //Response.ContentType = "application / vnd.ms-excel";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fname = "Stock&DistpatchDump" + DateTime.Now.ToString("MM/dd/yyyy_HH:mm:ss");

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
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }
}