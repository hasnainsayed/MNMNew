using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VirtualLocationSetting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // BindColControlSetting();
        }
    }

    public void BindColControlSetting(string LocationID)
    {
        DataBase.StyleColumnTable ObjBind = new DataBase.StyleColumnTable();

        hdnLocationID.Value = LocationID;
        
        DataSet ds = ObjBind.BindVirtualLocationSetting(LocationID);

        if (ds.Tables[0].Rows.Count > 0)
        {
            GV.DataSource = ds;
            GV.DataBind();
        }
        ds.Dispose();
        ObjBind = null;
    }

    protected void rptr_ItemBound(object sender, RepeaterItemEventArgs e)
    {
        //ItemBoundCounter++;
        //if (ItemBoundCounter == 1)

        {
            //HtmlTableCell icol1 = (HtmlTableCell)e.Item.FindControl("col1");
            CheckBox ChkQty = (CheckBox)e.Item.FindControl("ChkQty");
            HiddenField hdnIsAssigned = (HiddenField)e.Item.FindControl("hdnIsAssigned");
            if (hdnIsAssigned.Value == "True")
            {
                ChkQty.Checked = true;
            }
            else
            {
                ChkQty.Checked = false;
            }
        }

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //ctfrmDet.Visible = true;
        //btnSave.Visible = true;
        //btnUpdate.Visible = false;
        btnSave.Visible = true;
        btnAdd.Visible = false;
        string LocationID = Session["LocationID"].ToString();
        BindColControlSetting(LocationID);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataBase.StyleColumnTable objAdd = new DataBase.StyleColumnTable();

        for (int i = 0; i < GV.Items.Count; i++)
        {
            TextBox txtName = (TextBox)GV.Items[i].FindControl("txtName");
            CheckBox ChkQty = (CheckBox)GV.Items[i].FindControl("ChkQty");
            HiddenField hdnID = (HiddenField)GV.Items[i].FindControl("hdnID");

            hdnLocationID.Value = Session["LocationID"].ToString();

            bool IsAssigned = false;

            if (ChkQty.Checked && hdnLocationID.Value !="")
            {
                if (txtName.Text != "" && ChkQty.Checked)
                {
                    IsAssigned = true;
                }
                else
                {
                    IsAssigned = false;
                }

                int Success = objAdd.AddVirtualLocationSetting(txtName.Text.Trim(), IsAssigned, hdnID.Value, hdnLocationID.Value);
            }
        }

        Response.Redirect("VirtualLocation.aspx");
        //BindCategorySetting();  
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("VirtualLocation.aspx");
    }
}