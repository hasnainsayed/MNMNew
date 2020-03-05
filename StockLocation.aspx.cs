using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using ClosedXML.Excel;
using System.IO;
using System.Globalization;

public partial class StockLocation : System.Web.UI.Page
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
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "firedtSearch();", true);
            }
            
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
    public void BindData()
    {
        try
        {
            StockLocationCls obj = new StockLocationCls();
            DataTable dt = obj.getStockLocation();
            GV.DataSource = dt;
            GV.DataBind();

            object qunty = dt.Compute("Sum(mrp)", string.Empty);
            object rflqty = dt.Compute("Sum(cnt)", string.Empty);
          
            lbl1.Text = qunty.ToString();
            lbl2.Text = rflqty.ToString();


        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}