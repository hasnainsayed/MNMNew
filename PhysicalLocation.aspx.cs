using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PhysicalLocation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindPhysicalLocation();
            //dvlocation.Visible = true;
        }
    }

    #region Location

    public void BindPhysicalLocation()
    {   
        DataBase.Masters ObjBind = new DataBase.Masters();

        string PhysicalLocation = "1";

        DataSet ds = ObjBind.BindLocationByLocation2typeID(PhysicalLocation);

        if (ds.Tables[0].Rows.Count > 0)
        {
            GV.DataSource = ds;
            GV.DataBind();
        }
        ds.Dispose();
        ObjBind = null;
    }

    public void BindLocationTypeCombo()
    {
        DataBase.Masters ObjBind = new DataBase.Masters();

        DataSet ds = ObjBind.BindLocationType();

        ddlLocationType.DataSource = ds;
        ddlLocationType.DataTextField = "Name";
        ddlLocationType.DataValueField = "LocationTypeID";
        ddlLocationType.DataBind();
        ddlLocationType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Location Type", "0"));
    }

    //public void BindLocationType2Combo()
    //{
    //    DataBase.Masters ObjBind = new DataBase.Masters();

    //    DataSet ds = ObjBind.BindLocationType2();

    //    ddlLocationType2.DataSource = ds;
    //    ddlLocationType2.DataTextField = "Name";
    //    ddlLocationType2.DataValueField = "LTypeID2";
    //    ddlLocationType2.DataBind();
    //    ddlLocationType2.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Location Type2", "0"));
    //}

    public void Clear()
    {
        txtName.Text = string.Empty;
        txtContact.Text = string.Empty;
        txtAddress.Text = string.Empty;
        ddlLocationType.SelectedValue = "0";
    }

    protected void rptr_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        DataBase.Masters objEdit = new DataBase.Masters();

        if (e.CommandName.ToLower().Equals("edit"))
        {
            BindLocationTypeCombo();
            //BindLocationType2Combo();

            int ID = int.Parse(e.CommandArgument.ToString());
            hdnID.Value = e.CommandArgument.ToString();

            ctfrmDet.Visible = true;

            DataSet ds = objEdit.GetLocationByID(ID);

            txtName.Text = ds.Tables[0].Rows[0]["Location"].ToString();
            ddlLocationType.SelectedValue = ds.Tables[0].Rows[0]["LocationTypeID"].ToString();
            //ddlLocationType2.SelectedValue = ds.Tables[0].Rows[0]["LTypeID2"].ToString();
            txtContact.Text = ds.Tables[0].Rows[0]["Contact"].ToString();
            txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();

            btnSave.Visible = false;
            btnUpdate.Visible = true;

        }

        if (e.CommandName.ToLower().Equals("addsl"))
        {
            int ID = int.Parse(e.CommandArgument.ToString());

            Session["LocationID"] = ID.ToString();
            Response.Redirect("SubLocation.aspx");
        }

        objEdit = null;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        BindLocationTypeCombo();
        //BindLocationType2Combo();

        ctfrmDet.Visible = true;
        btnSave.Visible = true;
        btnUpdate.Visible = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataBase.Masters objAdd = new DataBase.Masters();

        string PhysicalLocation = "1";

        int ID = objAdd.AddLocation(txtName.Text.Trim(), ddlLocationType.SelectedValue, PhysicalLocation, txtContact.Text.Trim(), txtAddress.Text.Trim());
        if (ID > 0)
        {
            Clear();
            ctfrmDet.Visible = false;
            BindPhysicalLocation();
            objAdd = null;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        DataBase.Masters objUpdate = new DataBase.Masters();

        string PhysicalLocation = "1";

        int Success = objUpdate.UpdateLocation(txtName.Text.Trim(), ddlLocationType.SelectedValue, PhysicalLocation, hdnID.Value, txtContact.Text.Trim(), txtAddress.Text.Trim());

        if (Success > 0)
        {
            btnUpdate.Visible = false;
            btnSave.Visible = true;

            Clear();

            BindPhysicalLocation();
            ctfrmDet.Visible = false;
            //Response.Redirect("AddBuyer.aspx");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtName.Text = String.Empty;

        Response.Redirect("Location.aspx");

        btnSave.Text = "Save";
        ctfrmDet.Visible = false;
    }

    #endregion
}