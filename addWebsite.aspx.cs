using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
public partial class addWebsite : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                if (Session["addWebsite"] != null)
                {
                    sellId.Text = Session["addWebsite"].ToString();
                    bindGenderDisplay(sellId.Text);
                    bindVerticalDisplay(sellId.Text);
                    bindData(sellId.Text);
                    Session.Remove("addWebsite");
                }
                else
                {
                    Session["websiteSuccFail"] = "Session Error";
                    Response.Redirect("sellOnWebsite.aspx", true);
                }
                
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    private void bindData(string sellId)
    {
        try
        {
            storedProcedureCls obj = new storedProcedureCls();
            DataTable dt = obj.getTableWithCondition("webDisplay", "sellId", sellId, "sellId","desc");
            DataTable dt1 = obj.getMenuBanners("banners", "bannerType", "7", "bannerStatus", "1");
            menuBannerId.DataSource = dt1;
            menuBannerId.DataBind();
            menuBannerId.Items.Insert(0, new ListItem("---- Select Banner ----", "-1"));
            if (!dt.Rows.Count.Equals(0))
            {
                menuBannerId.SelectedValue = dt.Rows[0]["menuBannerId"].ToString();
                displayTitle.Text = dt.Rows[0]["displayTitle"].ToString();
                priorities.Text = dt.Rows[0]["priorities"].ToString();
                if (dt.Rows[0]["displayStatus"].ToString().Equals(true))
                {
                    displayStatus.SelectedValue = "1";
                }
                else
                {
                    displayStatus.SelectedValue = "0";
                }
                
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    private void bindGenderDisplay(string sellID)
    {
        try
        {
            storedProcedureCls obj = new storedProcedureCls();
            DataTable dt = obj.getGenderforWeb(sellID);
            rpt_gender.DataSource = dt;
            rpt_gender.DataBind();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    private void bindVerticalDisplay(string sellID)
    {
        try
        {
            storedProcedureCls obj = new storedProcedureCls();
            DataTable dt = obj.getVerticalforWeb(sellID);
            rpt_vertical.DataSource = dt;
            rpt_vertical.DataBind();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void rpt_vertical_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            CheckBox verCheck = (CheckBox)e.Item.FindControl("verCheck");            
            if(verCheck.Checked.Equals(true))
            {
                Repeater rpt_category = (Repeater)e.Item.FindControl("rpt_category");
                string wvId = ((DataRowView)e.Item.DataItem)["wvId"].ToString();
                HtmlTableCell row1 = (HtmlTableCell)e.Item.FindControl("showCategory");
                bindCategory(rpt_category, wvId, row1, verCheck.Attributes["Value"]);
                /*CheckBox btn = (CheckBox)(sender);
                RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
                Repeater pRepeater = ((Repeater)(rp1.NamingContainer));
                RepeaterItem pRepeater1 = ((RepeaterItem)(pRepeater.NamingContainer));
                Label wvId = (Label)pRepeater1.FindControl("wvId"); */

                
            }
           
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    private void bindCategory(Repeater rpt_category, string wvId, HtmlTableCell row1,string cat1ID)
    {
        try
        {
            storedProcedureCls obj = new storedProcedureCls();
            DataTable dt = obj.getCategoryforWeb(wvId, cat1ID);
            rpt_category.DataSource = dt;
            rpt_category.DataBind();
            //row1.Visible = true;
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void verCheck_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox RepeaterItem = (CheckBox)(sender);
            RepeaterItem rp1 = ((RepeaterItem)(RepeaterItem.NamingContainer));
            CheckBox verCheck = (CheckBox)rp1.FindControl("verCheck");
            Repeater rpt_category = (Repeater)rp1.FindControl("rpt_category");
            HtmlTableCell row1 = (HtmlTableCell)rp1.FindControl("showCategory");
            if (verCheck.Checked.Equals(true))
            {
                Label wvId = (Label)rp1.FindControl("wvId");                
                bindCategory(rpt_category, wvId.Text, row1, verCheck.Attributes["Value"]);
            }
            else
            {
                rpt_category.DataSource = new DataTable();
                rpt_category.DataBind();
                //row1.Visible = false;
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void cancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("sellOnWebsite.aspx",true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void save_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                string error = string.Empty;
                DataTable gender = new DataTable();
                gender.Columns.Add("ID"); gender.Columns.Add("genderId"); gender.Columns.Add("displayId");
                foreach (RepeaterItem itemEquipment in rpt_gender.Items)
                {
                    CheckBox checkValue = (CheckBox)itemEquipment.FindControl("genCheck");
                    if(checkValue.Checked.Equals(true))
                    {
                        Label wgId = (Label)itemEquipment.FindControl("wgId");
                        gender.Rows.Add(wgId.Text, checkValue.Attributes["Value"],1);
                    }                    
                }

                DataTable vertical = new DataTable();
                vertical.Columns.Add("ID"); vertical.Columns.Add("verticalId");

                DataTable category = new DataTable();
                category.Columns.Add("ID"); category.Columns.Add("verticalId"); category.Columns.Add("categoryId");

                foreach (RepeaterItem itemEquipment in rpt_vertical.Items)
                {
                    CheckBox checkValue = (CheckBox)itemEquipment.FindControl("verCheck");
                    if (checkValue.Checked.Equals(true))
                    {
                        Label wvId = (Label)itemEquipment.FindControl("wvId");
                        Repeater rpt_category = (Repeater)itemEquipment.FindControl("rpt_category");
                        vertical.Rows.Add(wvId.Text, checkValue.Attributes["Value"]);
                        foreach (RepeaterItem itemEquipment1 in rpt_category.Items)
                        {
                            CheckBox checkValue1 = (CheckBox)itemEquipment1.FindControl("catCheck");
                            if (checkValue1.Checked.Equals(true))
                            {
                                Label wcId = (Label)itemEquipment1.FindControl("wcId");
                                category.Rows.Add(wcId.Text, checkValue.Attributes["Value"], checkValue1.Attributes["Value"]);
                            }
                        }
                            
                    }
                }

                if (gender.Rows.Count.Equals(0))
                {
                    error += "Please Select atleast one gender";
                }

                if (vertical.Rows.Count.Equals(0))
                {
                    error += "Please Select atleast one Vertical";
                }
                                
                if (error.Equals(""))
                {
                    string logs = "," + Session["userName"] + ":" + DateTime.Now;
                    storedProcedureCls obj = new storedProcedureCls();                    
                    string result = obj.saveWebDetaiils(displayTitle.Text, displayStatus.SelectedValue, gender, vertical,category,sellId.Text,logs,menuBannerId.SelectedValue, priorities.Text);
                    Session["websiteSuccFail"] = result;
                    Response.Redirect("sellOnWebsite.aspx", true);
                }
                else
                {
                    //write alert sting
                }
                //Response.Redirect("sellOnWebsite.aspx", true);
            }
            else
            {
                Label1.Text = "";
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}