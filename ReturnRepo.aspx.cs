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
public partial class ReturnRepo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if(!IsPostBack)
            {
               
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey1", "firedtSearch();", true);
            }
        }
        catch(Exception Ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(Ex);
        }
    }
    public void BindData()
    {
        try
        {
            
            DataTable dt = new DataTable();
            if (rbttype.SelectedValue.Equals("Month"))
            {
                payment_reportCls Obj = new payment_reportCls();
                 dt = Obj.BindReturn("Month");
                thdate.Visible = false;
                DataView dv = dt.DefaultView;
                dv.Sort = "Year desc";
                 dt = dv.ToTable();

            }
            else if (rbttype.SelectedValue.Equals("Date"))
            {
                payment_reportCls Obj = new payment_reportCls();
                 dt = Obj.BindReturn("Date");
                thdate.Visible = true;
               
            }
            

            rpt_Return.DataSource = dt;
            rpt_Return.DataBind();
        }
        catch(Exception Ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(Ex);
        }
    }

    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtExcel = new DataTable();
            if (rbttype.SelectedValue.Equals("Month"))
            {
                payment_reportCls Obj = new payment_reportCls();
                dtExcel = Obj.BindReturn("Month");
                dtExcel.Columns.Remove("Date");
                dtExcel.AcceptChanges();
                DataView dv = dtExcel.DefaultView;
                dv.Sort = "Year desc";
                dtExcel = dv.ToTable();
            }
            else if (rbttype.SelectedValue.Equals("Date"))
            {
                payment_reportCls Obj = new payment_reportCls();
                dtExcel = Obj.BindReturn("Date");
            }
            

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dtExcel, "Return Report");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fname = "ReturnRepo_" + DateTime.Now.ToString("dd-MM-yyyy-HH:mm:ss");

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

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindData();
            sortdiv.Visible = true;
        }
        catch(Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void rpt_Return_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            HtmlTableCell tddate = (HtmlTableCell)e.Item.FindControl("tddate");

            if (rbttype.SelectedValue.Equals("Month"))
            {
                tddate.Visible = false;
            }
            else if (rbttype.SelectedValue.Equals("Date"))
            {
                tddate.Visible = true;
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }
}