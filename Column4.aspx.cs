using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Column4 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindColumn4();
        }
    }

    public void BindColumn4()
    {
        DataBase.StyleColumnTable ObjBind = new DataBase.StyleColumnTable();

        DataSet ds = ObjBind.BindCol4();

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

            DataSet ds = objEdit.GetCol4ByID(ID);

            txtName.Text = ds.Tables[0].Rows[0]["C4Name"].ToString();

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

        int ID = objAdd.AddCol4(txtName.Text.Trim());
        if (ID > 0)
        {
            BindColumn4();
            Clear();
            ctfrmDet.Visible = false;
            objAdd = null;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        DataBase.StyleColumnTable objUpdate = new DataBase.StyleColumnTable();

        int Success = objUpdate.UpdateCol4(txtName.Text.Trim(), hdnID.Value);

        if (Success > 0)
        {
            btnUpdate.Visible = false;
            btnSave.Visible = true;

            Clear();

            BindColumn4();
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