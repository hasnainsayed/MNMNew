using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LocationType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindLocationType();
        }
    }

    public void BindLocationType()
    {
        DataBase.Masters ObjBind = new DataBase.Masters();

        DataSet ds = ObjBind.BindLocationType();

        if (ds.Tables[0].Rows.Count > 0)
        {
            GV.DataSource = ds;
            GV.DataBind();
        }
        ds.Dispose();
        ObjBind = null;
    }

    protected void rptr_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        DataBase.Masters objEdit = new DataBase.Masters();

        if (e.CommandName.ToLower().Equals("edit"))
        {
            int ID = int.Parse(e.CommandArgument.ToString());
            hdnID.Value = e.CommandArgument.ToString();

            ctfrmDet.Visible = true;

            DataSet ds = objEdit.GetLocationTypeByID(ID);

            txtName.Text = ds.Tables[0].Rows[0]["Name"].ToString();

            btnSave.Visible = false;
            btnUpdate.Visible = true;

        }
        objEdit = null;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ctfrmDet.Visible = true;
        btnSave.Visible = true;
        btnUpdate.Visible = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataBase.Masters objAdd = new DataBase.Masters();

        int ID = objAdd.AddLoacationType(txtName.Text.Trim());
        if (ID > 0)
        {
            txtName.Text = "";
            ctfrmDet.Visible = false;
            BindLocationType();
            objAdd = null;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        DataBase.Masters objUpdate = new DataBase.Masters();

        int Success = objUpdate.UpdateLocationType(txtName.Text.Trim(), hdnID.Value);

        if (Success > 0)
        {
            btnUpdate.Visible = false;
            btnSave.Visible = true;

            txtName.Text = "";
            BindLocationType();
            ctfrmDet.Visible = false;
            //Response.Redirect("AddBuyer.aspx");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtName.Text = String.Empty;

        btnSave.Text = "Save";
        ctfrmDet.Visible = false;
    }
}