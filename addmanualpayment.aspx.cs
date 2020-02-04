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
using System.Globalization;

public partial class addmanualpayment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            try
            {
                BindData();
            }
            catch (Exception ex)
            {
                RecordExceptionCls obj = new RecordExceptionCls();
                obj.recordException(ex);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "firedtSearch();", true);
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey1", "firedate();", true);
        }

    }

    protected void btngo_Click(object sender, EventArgs e)
    {
        try
        {
            showItem.Visible = true;
            action.Visible = true;
        }
        catch (Exception ex)
        {
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }
    }
    protected void BindData()
    {
        try
        {
            locationCls obj = new locationCls();
            DataTable dt = obj.getVirtualLocation("2");
            virtualLocation.DataSource = dt;
            virtualLocation.DataBind();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void btnaddrow_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtnewrow = new DataTable();
            dtnewrow.Columns.Add("salesid");
            dtnewrow.Columns.Add("barcode");
            dtnewrow.Columns.Add("Type");
            dtnewrow.Columns.Add("Sp");

            dtnewrow.Columns.Add("orderdat");
            dtnewrow.Columns.Add("channelcomm");
            dtnewrow.Columns.Add("totaligst");
            dtnewrow.Columns.Add("totalcgst");
            dtnewrow.Columns.Add("totalsgst");
            dtnewrow.Columns.Add("channelgate");
            dtnewrow.Columns.Add("logistic");
            dtnewrow.Columns.Add("penalty");
            dtnewrow.Columns.Add("totaldecincgst");
            dtnewrow.Columns.Add("totaldecexcgst");
            dtnewrow.Columns.Add("tcsincgst");
            dtnewrow.Columns.Add("tcsexcgst");
            dtnewrow.Columns.Add("payableamt");

            foreach (RepeaterItem itemEquipment in rptShowItem.Items)
            {


                TextBox txtsalesid = (TextBox)itemEquipment.FindControl("txtsalesid");
                TextBox txtbarcode = (TextBox)itemEquipment.FindControl("txtbarcode");
                TextBox txttype = (TextBox)itemEquipment.FindControl("txttype");
                TextBox txtsp = (TextBox)itemEquipment.FindControl("txtsp");


                TextBox txtdate = (TextBox)itemEquipment.FindControl("txtdate");
                TextBox txtchcomm = (TextBox)itemEquipment.FindControl("txtchcomm");
                TextBox txttotigst = (TextBox)itemEquipment.FindControl("txttotigst");
                TextBox txttotcgst = (TextBox)itemEquipment.FindControl("txttotcgst");
                TextBox txttotsgst = (TextBox)itemEquipment.FindControl("txttotsgst");
                TextBox txtchgate = (TextBox)itemEquipment.FindControl("txtchgate");
                TextBox txtlogis = (TextBox)itemEquipment.FindControl("txtlogis");
                TextBox txtpenalty = (TextBox)itemEquipment.FindControl("txtpenalty");

                TextBox txttotaldecincgst = (TextBox)itemEquipment.FindControl("txttotaldecincgst");
                TextBox txttotaldecexcgst = (TextBox)itemEquipment.FindControl("txttotaldecexcgst");
                TextBox txcincgst = (TextBox)itemEquipment.FindControl("txcincgst");
                TextBox txcexcgst = (TextBox)itemEquipment.FindControl("txcexcgst");
                TextBox txtpayamt = (TextBox)itemEquipment.FindControl("txtpayamt");

                dtnewrow.Rows.Add(txtsalesid.Text, txtbarcode.Text, txttype.Text, txtsp.Text, txtdate.Text, txtchcomm.Text, txttotigst.Text, txttotcgst.Text, txttotsgst.Text, txtchgate.Text,
                   txtlogis.Text, txtpenalty.Text, txttotaldecincgst.Text, txttotaldecexcgst.Text, txcincgst.Text, txcexcgst.Text, txtpayamt.Text);

            }
            dtnewrow.Rows.Add("", "", "");
            rptShowItem.DataSource = dtnewrow;
            rptShowItem.DataBind();
        }
        catch (Exception ex)
        {
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtnewrow = new DataTable();
            dtnewrow.Columns.Add("salesid");
            dtnewrow.Columns.Add("barcode");
            dtnewrow.Columns.Add("Type");
            dtnewrow.Columns.Add("Sp");
            
            dtnewrow.Columns.Add("orderdat");
            dtnewrow.Columns.Add("channelcomm");
            dtnewrow.Columns.Add("totaligst");
            dtnewrow.Columns.Add("totalcgst");
            dtnewrow.Columns.Add("totalsgst");
            dtnewrow.Columns.Add("channelgate");
            dtnewrow.Columns.Add("logistic");
            dtnewrow.Columns.Add("penalty");
            dtnewrow.Columns.Add("totaldecincgst");
            dtnewrow.Columns.Add("totaldecexcgst");
            dtnewrow.Columns.Add("tcsincgst");
            dtnewrow.Columns.Add("tcsexcgst");
            dtnewrow.Columns.Add("payableamt");

            foreach (RepeaterItem itemEquipment in rptShowItem.Items)
            {


                TextBox txtsalesid = (TextBox)itemEquipment.FindControl("txtsalesid");
                TextBox txtbarcode = (TextBox)itemEquipment.FindControl("txtbarcode");
                TextBox txttype = (TextBox)itemEquipment.FindControl("txttype");
                TextBox txtsp = (TextBox)itemEquipment.FindControl("txtsp");
                
                
                TextBox txtdate = (TextBox)itemEquipment.FindControl("txtdate");
                TextBox txtchcomm = (TextBox)itemEquipment.FindControl("txtchcomm");
                TextBox txttotigst = (TextBox)itemEquipment.FindControl("txttotigst");
                TextBox txttotcgst = (TextBox)itemEquipment.FindControl("txttotcgst");
                TextBox txttotsgst = (TextBox)itemEquipment.FindControl("txttotsgst");
                TextBox txtchgate = (TextBox)itemEquipment.FindControl("txtchgate");
                TextBox txtlogis = (TextBox)itemEquipment.FindControl("txtlogis");
                TextBox txtpenalty = (TextBox)itemEquipment.FindControl("txtpenalty");
                
                TextBox txttotaldecincgst = (TextBox)itemEquipment.FindControl("txttotaldecincgst");
                TextBox txttotaldecexcgst = (TextBox)itemEquipment.FindControl("txttotaldecexcgst");
                TextBox txcincgst = (TextBox)itemEquipment.FindControl("txcincgst");
                TextBox txcexcgst = (TextBox)itemEquipment.FindControl("txcexcgst");
                TextBox txtpayamt = (TextBox)itemEquipment.FindControl("txtpayamt");

                dtnewrow.Rows.Add(txtsalesid.Text, txtbarcode.Text, txttype.Text, txtsp.Text, txtdate.Text, txtchcomm.Text, txttotigst.Text, txttotcgst.Text, txttotsgst.Text, txtchgate.Text,
                    txtlogis.Text, txtpenalty.Text, txttotaldecincgst.Text, txttotaldecexcgst.Text, txcincgst.Text, txcexcgst.Text, txtpayamt.Text);

            }

            Payment_fileCls obj = new Payment_fileCls();
            DataTable dt = obj.Addmanualenteris(dtnewrow, virtualLocation.SelectedValue);
            if(dt.Rows.Count.Equals(0))
            {
                showItem.Visible = false;
                DataTable dtnull = null;
                rptShowItem.DataSource = dtnull;
                rptShowItem.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfully Added!');", true);
            }
            else if(dt.Rows.Count.Equals(null))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Failed');", true);
            }
            else
            {
                rptShowItem.DataSource = dt;
                rptShowItem.DataBind();
                showItem.Visible = true;
                action.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Wrong Barcodes');", true);
            }


        }
        catch (Exception ex)
        {
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        try
        {
            showItem.Visible = false;
            DataTable dtnull = null;
            rptShowItem.DataSource = dtnull;
            rptShowItem.DataBind();
        }
        catch (Exception ex)
        {
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }
    }

    protected void btndelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = ((LinkButton)(sender));

            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            int index = rp1.ItemIndex;

            DataTable dtnewrow = new DataTable();
            dtnewrow.Columns.Add("salesid");
            dtnewrow.Columns.Add("barcode");
            dtnewrow.Columns.Add("Type");
            dtnewrow.Columns.Add("Sp");

            dtnewrow.Columns.Add("orderdat");
            dtnewrow.Columns.Add("channelcomm");
            dtnewrow.Columns.Add("totaligst");
            dtnewrow.Columns.Add("totalcgst");
            dtnewrow.Columns.Add("totalsgst");
            dtnewrow.Columns.Add("channelgate");
            dtnewrow.Columns.Add("logistic");
            dtnewrow.Columns.Add("penalty");
            dtnewrow.Columns.Add("totaldecincgst");
            dtnewrow.Columns.Add("totaldecexcgst");
            dtnewrow.Columns.Add("tcsincgst");
            dtnewrow.Columns.Add("tcsexcgst");
            dtnewrow.Columns.Add("payableamt");

            foreach (RepeaterItem itemEquipment in rptShowItem.Items)
            {

                TextBox txtsalesid = (TextBox)itemEquipment.FindControl("txtsalesid");
                TextBox txtbarcode = (TextBox)itemEquipment.FindControl("txtbarcode");
                TextBox txttype = (TextBox)itemEquipment.FindControl("txttype");
                TextBox txtsp = (TextBox)itemEquipment.FindControl("txtsp");


                TextBox txtdate = (TextBox)itemEquipment.FindControl("txtdate");
                TextBox txtchcomm = (TextBox)itemEquipment.FindControl("txtchcomm");
                TextBox txttotigst = (TextBox)itemEquipment.FindControl("txttotigst");
                TextBox txttotcgst = (TextBox)itemEquipment.FindControl("txttotcgst");
                TextBox txttotsgst = (TextBox)itemEquipment.FindControl("txttotsgst");
                TextBox txtchgate = (TextBox)itemEquipment.FindControl("txtchgate");
                TextBox txtlogis = (TextBox)itemEquipment.FindControl("txtlogis");
                TextBox txtpenalty = (TextBox)itemEquipment.FindControl("txtpenalty");

                TextBox txttotaldecincgst = (TextBox)itemEquipment.FindControl("txttotaldecincgst");
                TextBox txttotaldecexcgst = (TextBox)itemEquipment.FindControl("txttotaldecexcgst");
                TextBox txcincgst = (TextBox)itemEquipment.FindControl("txcincgst");
                TextBox txcexcgst = (TextBox)itemEquipment.FindControl("txcexcgst");
                TextBox txtpayamt = (TextBox)itemEquipment.FindControl("txtpayamt");
                if (!itemEquipment.ItemIndex.Equals(index))
                {
                    dtnewrow.Rows.Add(txtsalesid.Text, txtbarcode.Text, txttype.Text, txtsp.Text, txtdate.Text, txtchcomm.Text, txttotigst.Text, txttotcgst.Text, txttotsgst.Text, txtchgate.Text,
                  txtlogis.Text, txtpenalty.Text, txttotaldecincgst.Text, txttotaldecexcgst.Text, txcincgst.Text, txcexcgst.Text, txtpayamt.Text);

                }

            }
            //dtProgLang.Rows.Add("-1");

            rptShowItem.DataSource = dtnewrow;
            rptShowItem.DataBind();
        }
        catch (Exception ex)
        {
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }
    }
}