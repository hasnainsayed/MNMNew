using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class StyleColumnSetting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindColControlSetting();
        }
    }

    public void BindColControlSetting()
    {
        DataBase.StyleColumnTable ObjBind = new DataBase.StyleColumnTable();

        DataSet ds = ObjBind.BindColControlSetting();

        if (ds.Tables[0].Rows.Count > 0)
        {
            GV.DataSource = ds;
            GV.DataBind();
        }
        ds.Dispose();
        ObjBind = null;
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

                int Success = objUpdate.UpdateColControlSetting(txtName.Text.Trim(), IsAssigned, hdnID.Value);
            }
        }

        Response.Redirect("StyleColumnSetting.aspx");
        //BindCategorySetting();  
    }
}