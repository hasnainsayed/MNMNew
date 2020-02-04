using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ColumnTable2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCategory3();
        }
    }

    public void BindCategory3()
    {
        DataBase.StyleCategory ObjBind = new DataBase.StyleCategory();

        DataSet ds = ObjBind.BindCat3();

        if (ds.Tables[0].Rows.Count > 0)
        {
            GV.DataSource = ds;
            GV.DataBind();
        }

        if (ds.Tables[1].Rows.Count > 0)
        {
            lblCat.Text = ds.Tables[1].Rows[0]["SettingName"].ToString();
            lblCat1.Text = ds.Tables[1].Rows[0]["SettingName"].ToString();

            lblCatName.Text = ds.Tables[1].Rows[0]["SettingName"].ToString();
            lblCatName1.Text = ds.Tables[1].Rows[0]["SettingName"].ToString();
        }

        if (ds.Tables[2].Rows.Count > 0)
        {
            lblCate2Name.Text = ds.Tables[2].Rows[0]["SettingName"].ToString();
            lblCate2Name1.Text = ds.Tables[2].Rows[0]["SettingName"].ToString();
        }

        ds.Dispose();
        ObjBind = null;
    }

    public void BindCategoryCombo()
    {
        DataBase.StyleCategory ObjBind = new DataBase.StyleCategory();

        DataSet ds = ObjBind.BindCat2();

        if (ds.Tables[0].Rows.Count > 0)
        {
            hdnCount.Value = "1";
        }

        ddlCategory.DataSource = ds;
        ddlCategory.DataTextField = "C2Name";
        ddlCategory.DataValueField = "Cat2ID";
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Category", "0"));
    }

    public void Clear()
    {
        txtName.Text = string.Empty;
        txtAbbriviation.Text = string.Empty;
    }

    protected void rptr_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        DataBase.StyleCategory objEdit = new DataBase.StyleCategory();

        if (e.CommandName.ToLower().Equals("edit"))
        {
            BindCategoryCombo();

            int ID = int.Parse(e.CommandArgument.ToString());
            hdnID.Value = e.CommandArgument.ToString();

            ctfrmDet.Visible = true;

            DataSet ds = objEdit.GetCat3ByID(ID);

            txtName.Text = ds.Tables[0].Rows[0]["C3Name"].ToString();
            txtAbbriviation.Text = ds.Tables[0].Rows[0]["C3Abbriviation"].ToString();
            ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["Cat2ID"].ToString();

            btnSave.Visible = false;
            btnUpdate.Visible = true;

        }
        objEdit = null;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        BindCategoryCombo();

        if (hdnCount.Value == "1")
        {
            ctfrmDet.Visible = true;
            btnSave.Visible = true;
            btnUpdate.Visible = false;
        }
        else
        {
            lblErrorMsg.Visible = true;
            lblErrorMsg.Text = "Please Add Category";
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataBase.StyleCategory objAdd = new DataBase.StyleCategory();

        int ID = objAdd.AddCat3(txtName.Text.Trim(), txtAbbriviation.Text.Trim(),ddlCategory.SelectedValue);
        if (ID > 0)
        {
            BindCategory3();
            Clear();
            ctfrmDet.Visible = false;
            objAdd = null;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        DataBase.StyleCategory objUpdate = new DataBase.StyleCategory();

        int Success = objUpdate.UpdateCat3(txtName.Text.Trim(), txtAbbriviation.Text.Trim(), hdnID.Value, ddlCategory.SelectedValue);

        if (Success > 0)
        {
            btnUpdate.Visible = false;
            btnSave.Visible = true;

            Clear();

            BindCategory3();
            ctfrmDet.Visible = false;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtName.Text = String.Empty;

        btnSave.Text = "Save";
        ctfrmDet.Visible = false;
    }
}