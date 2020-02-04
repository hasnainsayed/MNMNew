using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class CStylecolSetting : System.Web.UI.Page
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
                
            }
        }
        catch(Exception ex)
        {
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }
    }
    public void BindData()
    {
        try
        {

            ColNStySettingCls obj = new ColNStySettingCls();
            DataTable dt = obj.BindCategoryforStylecol();
            rptbind.DataSource = dt;
            rptbind.DataBind();
            divbind.Visible = true;
            divsave.Visible = false;
            divupdate.Visible = false;
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
            DataTable dtstycolset = new DataTable();
            dtstycolset.Columns.Add("itemcatid");
            dtstycolset.Columns.Add("stycolid");
           
            dtstycolset.Columns.Add("colname");
            dtstycolset.Columns.Add("mandatory");
            dtstycolset.Columns.Add("optinal");
            dtstycolset.Columns.Add("Na");
            dtstycolset.Columns.Add("cmgfrom");
            foreach (RepeaterItem rpt in rptupdate.Items)
            {
                Label ColumnNo = rpt.FindControl("colname") as Label;
               
                Label StyleCSID = rpt.FindControl("colid") as Label;
                RadioButtonList rblstytype = rpt.FindControl("rblstytype") as RadioButtonList;
                string mandatory = "False";
                string optinal = "False";
                string Na = "False";
                if (rblstytype.SelectedValue.Equals("mandatory"))
                {
                    mandatory = "True";
                }
                else if (rblstytype.SelectedValue.Equals("optinal"))
                {
                    optinal = "True";
                }
                else if (rblstytype.SelectedValue.Equals("Na"))
                {
                    Na = "True";
                }
                dtstycolset.Rows.Add(drp_itemCategory.SelectedValue, StyleCSID.Text, ColumnNo.Text, mandatory, optinal, Na, StyleCSID.Text);

            }

            ColNStySettingCls obj = new ColNStySettingCls();
            int table = obj.addEditstycolumnsetting(dtstycolset);
            if (table.Equals(0))
            {
                BindData();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfull !');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Failed !');", true);
            }

        }
        catch (Exception ex)
        {
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            divbind.Visible = true;
            divsave.Visible = false ;
            divupdate.Visible = false;
        }
        catch (Exception ex)
        {
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            styleCls obj2 = new styleCls();
            DataTable dt = obj2.bindItemCategory();
            drp_itemCategory.DataSource = dt;
            drp_itemCategory.DataBind();

            ColNStySettingCls obj = new ColNStySettingCls();
            DataTable dt3 = obj.gettable("StyleColumnSettings");
            rptsave.DataSource = dt3;
            rptsave.DataBind();
            divbind.Visible = false;
            divsave.Visible = true;
            divupdate.Visible = false;
        }
        catch (Exception ex)
        {
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            categoryid.Text = string.Empty;
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            Label catid = (Label)rp1.FindControl("catid");
            Label lblcatname = (Label)rp1.FindControl("lblcatname");
            ColNStySettingCls obj = new ColNStySettingCls();
            DataTable dt = obj.GetStyleColumnbycatid(catid.Text);
            rptupdate.DataSource = dt;
            rptupdate.DataBind();
            divbind.Visible = false;
            divsave.Visible = false;
            divupdate.Visible = true;
            catname.Text = lblcatname.Text;
        }
        catch (Exception ex)
        {
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }
    }

    protected void rptbind_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            
            string mandatory = ((DataRowView)e.Item.DataItem)["mandatory"].ToString();
            string optinal = ((DataRowView)e.Item.DataItem)["optinal"].ToString();
            string Na = ((DataRowView)e.Item.DataItem)["Na"].ToString();
            RadioButtonList rblstytype = (RadioButtonList)e.Item.FindControl("rblstytype");
            if(mandatory.Equals("True"))
            {
                rblstytype.SelectedIndex = 0;
            }
            if (optinal.Equals("True"))
            {
                rblstytype.SelectedIndex = 1;
            }
            if (Na.Equals("True"))
            {
                rblstytype.SelectedIndex = 2;
            }

        }
        catch (Exception ex)
        {
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }
    }

    protected void btncelset_Click(object sender, EventArgs e)
    {
        divbind.Visible = true;
        divsave.Visible = false;
        divupdate.Visible = false;
    }

    protected void btnsaveset_Click(object sender, EventArgs e)
    {
        try
        {
            ColNStySettingCls obj = new ColNStySettingCls();
            DataTable ut = obj.Check(drp_itemCategory.SelectedValue, "checkStycolSetting");
            if (ut.Rows.Count == 0)
            {
                DataTable dtstycolset = new DataTable();
                dtstycolset.Columns.Add("itemcatid");
                dtstycolset.Columns.Add("stycolid");
              
                dtstycolset.Columns.Add("colname");
                dtstycolset.Columns.Add("mandatory");
                dtstycolset.Columns.Add("optinal");
                dtstycolset.Columns.Add("Na");
                dtstycolset.Columns.Add("cmgfrom");
                foreach (RepeaterItem rpt in rptsave.Items)
                {
                    Label ColumnNo = rpt.FindControl("ColumnNo") as Label;
                   
                    Label StyleCSID = rpt.FindControl("StyleCSID") as Label;
                    RadioButtonList rblstytype = rpt.FindControl("rblstytype") as RadioButtonList;
                    string mandatory = "False";
                    string optinal = "False";
                    string Na = "False";
                    if (rblstytype.SelectedValue.Equals("mandatory"))
                    {
                        mandatory = "True";
                    }
                    else if (rblstytype.SelectedValue.Equals("optinal"))
                    {
                        optinal = "True";
                    }
                    else if (rblstytype.SelectedValue.Equals("Na"))
                    {
                        Na = "True";
                    }
                    dtstycolset.Rows.Add(drp_itemCategory.SelectedValue, StyleCSID.Text, ColumnNo.Text, mandatory, optinal, Na, -1);

                }

               
                int table = obj.addEditstycolumnsetting(dtstycolset);
                if (table.Equals(0))
                {
                    BindData();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfull !');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Failed !');", true);
                }
            }
            else
            {
                BindData();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Already Exsist !');", true);
                
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }
    }
}