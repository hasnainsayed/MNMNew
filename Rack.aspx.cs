using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Rack :  System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindRack();
        }
    }

    public void BindRack()
    {
        DataBase.Masters ObjBind = new DataBase.Masters();

        if (Session["SublocationID"] != null)
        {
            hdnSubloactionID.Value = Session["SublocationID"].ToString();
        }

        Session["SublocationID"] = null;

        DataSet ds = ObjBind.BindRackBySublocationID(hdnSubloactionID.Value);

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

            DataSet ds = objEdit.GetRackByID(ID);

            txtName.Text = ds.Tables[0].Rows[0]["Rack"].ToString();

            btnSave.Visible = false;
            btnUpdate.Visible = true;

        }

        if (e.CommandName.ToLower().Equals("addstack"))
        {
            int ID = int.Parse(e.CommandArgument.ToString());

            Session["RackID"] = ID.ToString();
            Session["SublocationID"] = hdnSubloactionID.Value;

            Response.Redirect("Stack.aspx");
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

        int ID = objAdd.AddRack(txtName.Text.Trim(), hdnSubloactionID.Value);
        if (ID > 0)
        {
            txtName.Text = "";
            BindRack();
            ctfrmDet.Visible = false;
            objAdd = null;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        DataBase.Masters objUpdate = new DataBase.Masters();

        int Success = objUpdate.UpdateRack(txtName.Text.Trim(), hdnID.Value, hdnSubloactionID.Value);

        if (Success > 0)
        {
            btnUpdate.Visible = false;
            btnSave.Visible = true;

            txtName.Text = "";
            BindRack();
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

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("SubLocation.aspx");
    }
}