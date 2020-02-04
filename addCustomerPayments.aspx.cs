using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class addCustomerPayments : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                bindData();
            }
            else
            {
                
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey1", "firedate();", true);
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    private void bindData()
    {
        try
        {
            // bind Centre  
            styleCls obj = new styleCls();
            /*DataTable centreDt = obj.getTable("accountingCentre");
            paymentCentre.DataSource = centreDt;
            paymentCentre.DataBind();*/

            DataTable vendorDt = obj.getTable("websiteCustomer");
            customerId.DataSource = vendorDt;
            customerId.DataBind();

            paymentDate.Text = DateTime.Now.ToString("MM/dd/yyyy");

            //bindMoreMoney();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtProgLang = new DataTable();
            dtProgLang.Columns.Add("paymentCentre");
            dtProgLang.Columns.Add("paymentMode");
            dtProgLang.Columns.Add("paymentAmount");
            dtProgLang.Columns.Add("paymentTransaction");
            foreach (RepeaterItem itemEquipment in rptPayments.Items)
            {
                DropDownList paymentCentre = itemEquipment.FindControl("paymentCentre") as DropDownList;
                DropDownList paymentMode = itemEquipment.FindControl("paymentMode") as DropDownList;
                TextBox paymentAmount = itemEquipment.FindControl("paymentAmount") as TextBox;
                TextBox paymentTransaction = itemEquipment.FindControl("paymentTransaction") as TextBox;

                dtProgLang.Rows.Add(paymentCentre.SelectedValue, paymentMode.SelectedValue, paymentAmount.Text, paymentTransaction.Text);

            }
            lotPaymentCls obj = new lotPaymentCls();
            int success = obj.saveAddCustPayment(customerId.SelectedValue, paymentDate.Text, paymentRemarks.Text,
                dtProgLang,Session["login"].ToString());
            if (success.Equals(0))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Added Successfully');window.location ='addCustomerPayments.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", "alert('Transaction Failed');window.location ='addCustomerPayments.aspx';", true);
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void rptPayments_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            string centreId = ((DataRowView)e.Item.DataItem)["paymentCentre"].ToString();
            DropDownList paymentCentre = e.Item.FindControl("paymentCentre") as DropDownList;


            styleCls obj = new styleCls();
            DataTable centreDt = obj.getTable("accountingCentre");
            paymentCentre.DataSource = centreDt;
            paymentCentre.DataBind();
            paymentCentre.Items.Insert(0, new ListItem("---- Select Centre ----", "-1"));
            paymentCentre.SelectedValue = centreId;
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void addMoreMoney_Click(object sender, EventArgs e)
    {
        try
        {
            bindMoreMoney();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    private void bindMoreMoney()
    {
        try
        {
            DataTable dtProgLang = new DataTable();
            dtProgLang.Columns.Add("paymentCentre");
            dtProgLang.Columns.Add("paymentMode");
            dtProgLang.Columns.Add("paymentAmount");
            dtProgLang.Columns.Add("paymentTransaction");
            dtProgLang.Columns.Add("moneyNo");
            int moneyNo = 0;
            decimal totAmount = Convert.ToDecimal(0);
            foreach (RepeaterItem itemEquipment in rptPayments.Items)
            {
                DropDownList paymentCentre = itemEquipment.FindControl("paymentCentre") as DropDownList;
                DropDownList paymentMode = itemEquipment.FindControl("paymentMode") as DropDownList;
                TextBox paymentAmount = itemEquipment.FindControl("paymentAmount") as TextBox;
                TextBox paymentTransaction = itemEquipment.FindControl("paymentTransaction") as TextBox;

                dtProgLang.Rows.Add(paymentCentre.SelectedValue, paymentMode.SelectedValue, paymentAmount.Text, paymentTransaction.Text,++moneyNo);
                totAmount += Convert.ToDecimal(paymentAmount.Text);
            }
            dtProgLang.Rows.Add("-1", "1", string.Empty, string.Empty, ++moneyNo);
            rptPayments.DataSource = dtProgLang;
            rptPayments.DataBind();
            calculatedAmount.Text = totAmount.ToString();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void addMore_Click(object sender, EventArgs e)
    {
        try
        {
            string abc = string.Empty;
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}