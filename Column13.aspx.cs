﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class Column13 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //get column name
                styleColumn sObj = new styleColumn();
                DataTable style = sObj.getTableColwithID("ColTableSetting", "TableName", "Column 13", "SettingName");
                headerName.Text = breadcrumName.Text = addName.Text = displayCol.Text = style.Rows[0]["SettingName"].ToString();
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

            //get data
            styleCls obj = new styleCls();
            DataTable dt = obj.getTable("Column13");
            GV.DataSource = dt;
            GV.DataBind();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void clearData()
    {
        try
        {
            C13Name.Text = string.Empty;
            hdnID.Text = "0";

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
            clearData();
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
            clearData();
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
            Label Col13ID = (Label)rp1.FindControl("Col13ID");
            styleCls obj = new styleCls();
            DataTable lot = obj.getTablewithID("Column13", "Col13ID", Convert.ToInt32(Col13ID.Text));
            C13Name.Text = lot.Rows[0]["C13Name"].ToString();
            hdnID.Text = lot.Rows[0]["Col13ID"].ToString();
            devCapone.Visible = true;

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
            styleColumn obj = new styleColumn();
            int success = obj.addEditCol("Column13", "Col13ID", "C13Name", C13Name.Text, hdnID.Text);
            clearData();
            devCapone.Visible = false;
            if (success.Equals(0))
            {
                divAddAlert.Visible = true;
                divUpdAlert.Visible = false;
            }
            else
            {
                divUpdAlert.Visible = true;
                divAddAlert.Visible = false;
            }
            BindData();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}