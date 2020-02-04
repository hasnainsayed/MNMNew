using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class ColNStySetting : System.Web.UI.Page
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
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }
    }
    public void BindData()
    {
        try
        {
            ColNStySettingCls obj = new ColNStySettingCls();
            DataTable dt = obj.Bind();
            GV.DataSource = dt;
            GV.DataBind();
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
    protected void cancel_Click(object sender, EventArgs e)
    {
        try
        {
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

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            ColNStySettingCls obj = new ColNStySettingCls();
            DataTable ut = obj.Check(drp_itemCategory.SelectedValue, "checkColSetting");
           
            if (ut.Rows.Count == 0)
            {
                DataTable dtcolset = new DataTable();
                dtcolset.Columns.Add("itemcatid");
                dtcolset.Columns.Add("colid");
               
                dtcolset.Columns.Add("TableName");
                dtcolset.Columns.Add("mandatory");
                dtcolset.Columns.Add("optinal");
                dtcolset.Columns.Add("Na");
                dtcolset.Columns.Add("cmgfrom");



                foreach (RepeaterItem rpt in rptcolsetting.Items)
                {
                    Label lbltabname = rpt.FindControl("lbltabname") as Label;
                    
                    Label lblcolidd = rpt.FindControl("lblcolidd") as Label;
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
                    dtcolset.Rows.Add(drp_itemCategory.SelectedValue, lblcolidd.Text, lbltabname.Text, mandatory, optinal, Na, -1);

                }

                
                int table = obj.addEditcolnstysetting(dtcolset);
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {


            styleCls obj = new styleCls();
            DataTable dt = obj.bindItemCategory();
            drp_itemCategory.DataSource = dt;
            drp_itemCategory.DataBind();

            ColNStySettingCls obj2 = new ColNStySettingCls();
            DataTable dt3 = obj2.gettable("ColTableSetting");
            
            rptcolsetting.DataSource = dt3;
            rptcolsetting.DataBind();
            
            
            divbind.Visible = false;
            divsave.Visible = true;
            divupdate.Visible = false;
        }
        catch(Exception ex)
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
            categoryid.Text = catid.Text;
            ColNStySettingCls obj = new ColNStySettingCls();
            DataTable dt = obj.Getcolcumnsettingbycatid(categoryid.Text);
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
    protected void rptupdate_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            string mandatory = ((DataRowView)e.Item.DataItem)["mandatory"].ToString();
            string optinal = ((DataRowView)e.Item.DataItem)["optinal"].ToString();
            string Na = ((DataRowView)e.Item.DataItem)["Na"].ToString();
            RadioButtonList rblstytype = (RadioButtonList)e.Item.FindControl("rblstytype");
            if (mandatory.Equals("True"))
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

    protected void btnupdarteset_Click(object sender, EventArgs e)
    {
        try
        {
            ColNStySettingCls obj = new ColNStySettingCls();

            DataTable dtcolset = new DataTable();
            dtcolset.Columns.Add("itemcatid");
            dtcolset.Columns.Add("colid");
            
            dtcolset.Columns.Add("TableName");
            dtcolset.Columns.Add("mandatory");
            dtcolset.Columns.Add("optinal");
            dtcolset.Columns.Add("Na");
            dtcolset.Columns.Add("cmgfrom");



            foreach (RepeaterItem rpt in rptupdate.Items)
            {
                Label lbltabname = rpt.FindControl("lbltabname") as Label;
                
                Label lblcolidd = rpt.FindControl("lblcolidd") as Label;
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
                dtcolset.Rows.Add(drp_itemCategory.SelectedValue, lblcolidd.Text, lbltabname.Text, mandatory, optinal, Na, lblcolidd.Text);

            }

            
            int table = obj.addEditcolnstysetting(dtcolset);
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

    protected void btncalset_Click(object sender, EventArgs e)
    {
        try
        {
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
}