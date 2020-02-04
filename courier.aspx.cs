using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class courier : System.Web.UI.Page
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

    protected void BindData()
    {
        try
        {
            courierCls obj = new courierCls();
            DataTable dt = obj.getCourier();
            GV.DataSource = dt;
            GV.DataBind();
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
            if (btnSave.Text == "Save")
            {
                courierCls obj = new courierCls();
                int Success = obj.addCourier(courierName.Text);
                if (Success != -1)
                {
                    courierName.Text = string.Empty;
                    hdnID.Text = string.Empty;
                    btnSave.Text = "Save";
                    devCapone.Visible = false;
                    BindData();
                }

                else
                {

                }

            }
            else if (btnSave.Text == "Update")
            {
                courierCls obj = new courierCls();
                int Success = obj.updateCourier(Convert.ToInt32(hdnID.Text), courierName.Text);
                if (Success != -1)
                {
                    courierName.Text = string.Empty;
                    hdnID.Text = string.Empty;
                    btnSave.Text = "Save";
                    devCapone.Visible = false;

                    devCapone.Visible = false;
                    BindData();
                }
                else
                {

                }
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
            courierName.Text = string.Empty;
            hdnID.Text = string.Empty;
            btnSave.Text = "Save";
            devCapone.Visible = false;

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            courierName.Text = string.Empty;
            hdnID.Text = string.Empty;
            btnSave.Text = "Save";
            devCapone.Visible = true;

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void edit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            Label courierId = (Label)rp1.FindControl("courierId");
            courierCls objL = new courierCls();
            DataTable courier = objL.getCourierById(courierId.Text);
            courierName.Text = courier.Rows[0]["courierName"].ToString();
            hdnID.Text = courierId.Text;
            btnSave.Text = "Update";
            devCapone.Visible = true;
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}