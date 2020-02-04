using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Column3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindColumn3();
        }
    }

    public void BindColumn3()
    {
        DataBase.StyleColumnTable ObjBind = new DataBase.StyleColumnTable();

        DataSet ds = ObjBind.BindCol3();

        if (ds.Tables[0].Rows.Count > 0)
        {
            GV.DataSource = ds.Tables[0];
            GV.DataBind();
        }

        if (ds.Tables[1].Rows.Count > 0)
        {
            lblCol.Text = ds.Tables[1].Rows[0]["SettingName"].ToString();
            lblCol1.Text = ds.Tables[1].Rows[0]["SettingName"].ToString();
            lblColName.Text = ds.Tables[1].Rows[0]["SettingName"].ToString();
            lblColName1.Text = ds.Tables[1].Rows[0]["SettingName"].ToString();
        }

        ds.Dispose();
        ObjBind = null;
    }

    public void Clear()
    {
        txtName.Text = string.Empty;
    }

    protected void rptr_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        DataBase.StyleColumnTable objEdit = new DataBase.StyleColumnTable();

        if (e.CommandName.ToLower().Equals("edit"))
        {
            int ID = int.Parse(e.CommandArgument.ToString());
            hdnID.Value = e.CommandArgument.ToString();

            ctfrmDet.Visible = true;

            DataSet ds = objEdit.GetCol3ByID(ID);

            txtName.Text = ds.Tables[0].Rows[0]["C3Name"].ToString();

            btnSave.Visible = false;
            btnUpdate.Visible = true;

        }
        objEdit = null;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //if (hdnCount.Value == "1")
        {
            ctfrmDet.Visible = true;
            btnSave.Visible = true;
            btnUpdate.Visible = false;
        }
        /* else
         {
             lblErrorMsg.Visible = true;
             lblErrorMsg.Text = "Please Add Item Category";
         }
         */
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataBase.StyleColumnTable objAdd = new DataBase.StyleColumnTable();

        int ID = objAdd.AddCol3(txtName.Text.Trim());
        if (ID > 0)
        {
            BindColumn3();
            Clear();
            ctfrmDet.Visible = false;
            objAdd = null;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        DataBase.StyleColumnTable objUpdate = new DataBase.StyleColumnTable();

        int Success = objUpdate.UpdateCol3(txtName.Text.Trim(), hdnID.Value);

        if (Success > 0)
        {
            btnUpdate.Visible = false;
            btnSave.Visible = true;

            Clear();

            BindColumn3();
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