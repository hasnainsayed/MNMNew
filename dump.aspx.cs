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

public partial class dump : System.Web.UI.Page
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

    protected void downloadDump_Click(object sender, EventArgs e)
    {
        try
        {
            reportCls obj = new reportCls();
            DataTable dt = obj.getDump(string.Empty);
            DataTable dt1 = obj.getSoldDump(string.Empty);
            dt.Merge(dt1);
            // csv
            /*string fname = "Dump_" + DateTime.Now.ToString("MM/dd/yyyy_HH:mm:ss");
            Response.Clear();

            Response.Buffer = true;

            Response.AddHeader("content-disposition","attachment;filename=" + fname + ".csv");

            Response.Charset = "";

            Response.ContentType = "application/text";

            StringBuilder sb = new StringBuilder();

            for (int k = 0; k < dt.Columns.Count; k++)
            {

                //add separator

                sb.Append(dt.Columns[k].ColumnName + ',');

            }

            //append new line

            sb.Append("\r\n");

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                for (int k = 0; k < dt.Columns.Count; k++)
                {

                    //add separator

                    sb.Append(dt.Rows[i][k].ToString().Replace(",", ";") + ',');

                }

                //append new line

                sb.Append("\r\n");

            }

            Response.Output.Write(sb.ToString());

            Response.Flush();

            Response.End();*/

            /*utilityCls uObj = new utilityCls();
            uObj.generateExcel(dt);*/
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Dump");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                //Response.ContentType = "application / vnd.ms-excel";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fname = "Dump_" + DateTime.Now.ToString("MM/dd/yyyy_HH:mm:ss");

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
}