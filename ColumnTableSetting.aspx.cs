using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ColumnTableSetting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindColumnSetting();
        }
    }

    public void BindColumnSetting()
    {
        DataBase.StyleColumnTable ObjBind = new DataBase.StyleColumnTable();

        DataSet ds = ObjBind.BindColSetting();

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
        DataBase.StyleCategory objEdit = new DataBase.StyleCategory();

        if (e.CommandName.ToLower().Equals("edit"))
        {
            int ID = int.Parse(e.CommandArgument.ToString());
            ID = +1;

            DataSet ds = objEdit.BindCatSettingByID(ID);
        }
        objEdit = null;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataBase.StyleColumnTable objUpdate = new DataBase.StyleColumnTable();

        for (int i = 0; i < GV.Items.Count; i++)
        {
            TextBox txtName = (TextBox)GV.Items[i].FindControl("txtName");
            CheckBox ChkQty = (CheckBox)GV.Items[i].FindControl("ChkQty");
            HiddenField hdnID = (HiddenField)GV.Items[i].FindControl("hdnID");

            bool IsAssigned = false;

            //if (ChkQty.Checked)
            {
                if (txtName.Text != "" && ChkQty.Checked)
                {
                    IsAssigned = true;
                }
                else
                {
                    IsAssigned = false;
                }

                int Success = objUpdate.UpdateColSetting(txtName.Text.Trim(), IsAssigned, hdnID.Value);
            }
        }

        Response.Redirect("ColumnTableSetting.aspx");
        //BindCategorySetting();  
    }
}