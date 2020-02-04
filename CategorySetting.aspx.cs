using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ColunTableSetting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if(hdnCount.Value== "0")
            {
                BindCategorySetting();
            }
        }
    }

    public void BindCategorySetting()
    {
        DataBase.StyleCategory ObjBind = new DataBase.StyleCategory();

        DataSet ds = ObjBind.BindCatSettingByID(1);

        if (ds.Tables[0].Rows.Count > 0)
        {
            hdnCount.Value = "1";

            GV.DataSource = ds;
            GV.DataBind();

            Session["Dataset"] = ds;
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
            ID = ID + 1;

            DataSet ds = objEdit.BindCatSettingByID(ID);

            GV.DataSource = ds;
            GV.DataBind();

            //DataTable dt = (DataTable)Session["Dataset"];
            //DataRow dr = dt.NewRow();

            //dr = dt.NewRow();
            //dr["ICSettingID"] = ds.Tables[0].Rows[0]["SettingName"].ToString();
            //dr["TableName"] = ds.Tables[0].Rows[0]["SettingName"].ToString();
            //dr["SettingName"] = ds.Tables[0].Rows[0]["SettingName"].ToString();

            ////dr["ID"] = Counter;
            ////Session["RowCounter"] = Counter;
            //dt.Rows.Add(dr);

            //Session["Table"] = null;
            //Session["Table"] = dt;

            //GV.DataSource = dt;
            //GV.DataBind();
        }
        objEdit = null;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataBase.StyleCategory objUpdate = new DataBase.StyleCategory();

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

                int Success = objUpdate.UpdateCatSetting(txtName.Text.Trim(), IsAssigned, hdnID.Value);
            }
        }

        Response.Redirect("CategorySetting.aspx");
        //BindCategorySetting();  
    }
}