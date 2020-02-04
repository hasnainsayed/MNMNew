using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class changeMRP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["changeMRPSuccfail"] != null)
                {
                    if (Session["changeMRPSuccfail"].ToString().Equals("MRP Updated Successfully"))
                    {
                        divSucc.InnerText = Session["changeMRPSuccfail"].ToString();
                        divSucc.Visible = true;
                        divError.Visible = false;
                        Session.Remove("changeMRPSuccfail");
                    }
                    else
                    {
                        divError.InnerText = Session["changeMRPSuccfail"].ToString();
                        divError.Visible = true;
                        divSucc.Visible = false;
                        Session.Remove("changeMRPSuccfail");
                    }

                }
            }
            
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void searchBarcode_Click(object sender, EventArgs e)
    {
        try
        {
            changeMRPCls obj = new changeMRPCls();
            DataTable dt = obj.getBarcodeDetails(searchField.Text);
            if(dt.Rows.Count.Equals(0))
            {
                showItem.Visible = false;
                noData.Visible =  true;
            }
            else
            {
                StockupID.Text = dt.Rows[0]["StockupID"].ToString();
                Barcode.Text = dt.Rows[0]["BarcodeNo"].ToString();
                ItemCategory.Text = dt.Rows[0]["ItemCategory"].ToString();
                C1Name.Text = dt.Rows[0]["C1Name"].ToString();
                mrp.Text = dt.Rows[0]["mrp"].ToString();
                showItem.Visible = true;
                noData.Visible = false;
            }
            divSucc.Visible = false;
            divError.Visible = false;

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void saveMRP_Click(object sender, EventArgs e)
    {
        try
        {
            changeMRPCls obj = new changeMRPCls();
            int success = obj.updateMRP(StockupID.Text,mrp.Text);
            if (success.Equals(-1))
            {
                Session["changeMRPSuccfail"] = "MRP Update Failed";

            }
            else if (success>=0)
            {
                Session["changeMRPSuccfail"] = "MRP Updated Successfully";
            }
            else
            {
                Session["changeMRPSuccfail"] = "Some Error";
            }
            //divError.Visible = true;
            Response.Redirect("changeMRP.aspx", true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}