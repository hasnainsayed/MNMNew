using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class LrListing : System.Web.UI.Page
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
            LrListingCls obj = new LrListingCls();
            DataTable dt = obj.getLr();
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
                LrListingCls obj1 = new LrListingCls();
                DataTable dt = obj1.getDuplicate(txtlrno.Text);
                if (dt.Rows.Count.Equals(0))
                {
                    LrListingCls obj = new LrListingCls();
                    int Success = obj.addLR(txtlrno.Text, txtdesc.Text);
                    if (Success != -1)
                    {
                        txtlrno.Text = string.Empty;
                        txtdesc.Text = string.Empty;
                        hdnID.Text = string.Empty;
                        devCapone.Visible = false;
                        BindData();
                    }

                    else
                    {

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Duplicate LR No. !');", true);
                }


            }



            else if (btnSave.Text == "Update")
            {
                LrListingCls obj1 = new LrListingCls();
                DataTable dt = obj1.getDuplicateById(txtlrno.Text, hdnID.Text);
                if (dt.Rows.Count.Equals(0))
                {
                    LrListingCls obj = new LrListingCls();
                    int Success = obj.updateLR(Convert.ToInt32(hdnID.Text), txtlrno.Text, txtdesc.Text);
                    if (Success != -1)
                    {
                        txtlrno.Text = string.Empty;
                        txtdesc.Text = string.Empty;
                        hdnID.Text = string.Empty;

                        devCapone.Visible = false;
                        BindData();
                    }
                    else
                    {

                    }
                }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Duplicate LR No. !');", true);
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
            txtlrno.Text = string.Empty;
            txtdesc.Text = string.Empty;
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
            txtlrno.Text = string.Empty;
            txtdesc.Text = string.Empty;
            btnSave.Text = "Save";
            devCapone.Visible = true;

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void statusActive_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            Label id = (Label)rp1.FindControl("lbId");
            LrListingCls objL = new LrListingCls();
            int success = objL.changeLRStatus(Convert.ToInt32(id.Text), "0");
            BindData();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void statusInActive_Click(object sender, EventArgs e)
    {

        try
        {
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            Label id = (Label)rp1.FindControl("lbId");
            LrListingCls objL = new LrListingCls();
            int success = objL.changeLRStatus(Convert.ToInt32(id.Text), "1");
            //Response.Redirect("newLot.aspx",true);
            BindData();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void GV_ItemDataBound(object sender, RepeaterItemEventArgs e)

    {
        try
        {
            string status = ((DataRowView)e.Item.DataItem)["status"].ToString();
            LinkButton statusActive = e.Item.FindControl("statusActive") as LinkButton;
            LinkButton statusInActive = e.Item.FindControl("statusInActive") as LinkButton;
            LinkButton edit_Click = e.Item.FindControl("edit") as LinkButton;
            if (status.Equals("False"))
            {
                statusActive.CssClass = statusActive.CssClass.Replace("btn-default", "btn-success");
                statusInActive.CssClass = statusInActive.CssClass.Replace("btn-danger", "btn-default");
                edit_Click.Visible = true;
            }
            else
            {
                statusActive.CssClass = statusActive.CssClass.Replace("btn-success", "btn-default");
                statusInActive.CssClass = statusInActive.CssClass.Replace("btn-default", "btn-danger");
            }

            Label activeid = e.Item.FindControl("activeid") as Label;
            Label inactiveid = e.Item.FindControl("inactiveid") as Label;
            Label loadedid = e.Item.FindControl("loadedid") as Label;
            Label generatedid = e.Item.FindControl("generatedid") as Label;
            Label totalid = e.Item.FindControl("totalid") as Label;
            Label lbId = e.Item.FindControl("lbId") as Label;
            LrListingCls objRecord = new LrListingCls();
            DataTable dt = objRecord.getRecords(lbId.Text);
            int numberOfInActive = dt.AsEnumerable().Where(x => x["IsActive"].ToString() == "0").ToList().Count;
            int numberOfActive = dt.AsEnumerable().Where(x => x["IsActive"].ToString() == "1").ToList().Count;
            int numberOfLoaded = dt.AsEnumerable().Where(x => x["IsActive"].ToString() == "3").ToList().Count;
            int numberOfGenerated = dt.AsEnumerable().Where(x => x["IsActive"].ToString() == "2").ToList().Count;
            int numberOfTotal = dt.Rows.Count;
            activeid.Text = numberOfActive.ToString();
            inactiveid.Text = numberOfInActive.ToString();
            loadedid.Text = numberOfLoaded.ToString();
            generatedid.Text = numberOfGenerated.ToString();
            totalid.Text = numberOfTotal.ToString();
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
            Label id = (Label)rp1.FindControl("lbId");
            LrListingCls objL = new LrListingCls();
            DataTable hsn = objL.getLRById(id.Text);
            txtlrno.Text = hsn.Rows[0]["lrno"].ToString();
            txtdesc.Text = hsn.Rows[0]["description"].ToString();
            hdnID.Text = id.Text;
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