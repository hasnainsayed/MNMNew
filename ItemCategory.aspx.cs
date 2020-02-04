using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ItemCategory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindItemCategory();
        }
    }

    public void BindItemCategory()
    {
        DataBase.StyleCategory ObjBind = new DataBase.StyleCategory();

        DataTable ds = ObjBind.BindItemCat();

        if (ds.Rows.Count > 0)
        {
            GV.DataSource = ds;
            GV.DataBind();
        }
        ds.Dispose();
        ObjBind = null;
    }

    public void BindHsn()
    {
        DataBase.StyleCategory ObjBind = new DataBase.StyleCategory();

        DataTable ds = ObjBind.BindHsnCodes();

        cmbHsn.DataSource = ds;
        cmbHsn.DataBind();
        cmbHsn.Items.Insert(0, new ListItem("--Select--","-1"));
    }

    public void Clear()
    {
        txtName.Text = string.Empty;
        txtHSN.Text = string.Empty;
        txtTax.Text = string.Empty;
        BindHsn();
    }

    protected void rptr_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        DataBase.StyleCategory objEdit = new DataBase.StyleCategory();

        if (e.CommandName.ToLower().Equals("edit"))
        {
            Clear();

            int ID = int.Parse(e.CommandArgument.ToString());
            hdnID.Value = e.CommandArgument.ToString();

            ctfrmDet.Visible = true;

            DataTable ds = objEdit.GetItemCatByID(ID);

            txtName.Text = ds.Rows[0]["ItemCategory"].ToString();
            txtHSN.Text = ds.Rows[0]["HSNCode"].ToString();
            txtTax.Text = ds.Rows[0]["Tax"].ToString();
            cmbHsn.SelectedValue = ds.Rows[0]["hsnid"].ToString();

            
            btnSave.Visible = false;
            btnUpdate.Visible = true;

        }
        objEdit = null;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Clear();
        ctfrmDet.Visible = true;
        btnSave.Visible = true;
        btnUpdate.Visible = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataBase.StyleCategory objAdd = new DataBase.StyleCategory();

        int ID = objAdd.AddItemCat(txtName.Text.Trim(), txtHSN.Text.Trim(), txtTax.Text.Trim(),cmbHsn.SelectedValue);
        if (ID.Equals(0))
        {
            BindItemCategory();
            Clear();
            ctfrmDet.Visible = false;
            objAdd = null;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        DataBase.StyleCategory objUpdate = new DataBase.StyleCategory();

        int Success = objUpdate.UpdateItemCat(txtName.Text.Trim(), txtHSN.Text.Trim(), txtTax.Text.Trim(), hdnID.Value, cmbHsn.SelectedValue);

        if (Success.Equals(0))
        {
            btnUpdate.Visible = false;
            btnSave.Visible = true;

            Clear();

            BindItemCategory();
            ctfrmDet.Visible = false;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();

        btnSave.Text = "Save";
        ctfrmDet.Visible = false;
    }
}