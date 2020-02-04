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

public partial class dispatchedDump : System.Web.UI.Page
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

    protected void dispatchdownloadDump_Click(object sender, EventArgs e)
    {
        try
        {
            reportCls obj = new reportCls();
            //DataTable dt = obj.getDispatched();
          
            DataTable dt = obj.getSalesDumpArchive(string.Empty);
            
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "DispatchDump");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                //Response.ContentType = "application / vnd.ms-excel";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fname = "DispatchDump_" + DateTime.Now.ToString("MM/dd/yyyy_HH:mm:ss");

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