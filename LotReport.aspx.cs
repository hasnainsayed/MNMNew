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

public partial class LotReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if(!IsPostBack)
            {
                BindData();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "firedtSearch();", true);
            }
        }
        catch(Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }
    public void BindData()
    {
        try
        {
            
            LotRepoCls obj = new LotRepoCls();
            DataTable dt = obj.BindLotRepo();
            rpt_Lot.DataSource = dt;
            rpt_Lot.DataBind();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
       
            try
            {
            LotRepoCls obj = new LotRepoCls();
            DataTable dtExcel = obj.BindLotRepo();
            


                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dtExcel,"Lot Report");
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    string fname = "LotReport_" + DateTime.Now;

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