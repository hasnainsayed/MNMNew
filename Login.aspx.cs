using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["loginid"] = null;
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string Admin = string.Empty;
        string AvtaarBussy = string.Empty;
        string Avtaar = string.Empty;

        DataBase.login objAdminlogin = new DataBase.login();

        DataSet ds = objAdminlogin.AdminLogin(txtUserName.Text.Trim(), txtPassword.Text.Trim());
        
        if (ds.Tables[0].Rows.Count > 0)
        {
            Session["login"] = ds.Tables[0].Rows[0]["userid"].ToString();
            Session["userName"] = txtUserName.Text.Trim();
            Session["uImage"] = ds.Tables[0].Rows[0]["uImage"].ToString();
            Session["userrole"] = ds.Tables[0].Rows[0]["userrole"].ToString();
            Session["uType"] = ds.Tables[0].Rows[0]["uType"].ToString();
            Session["physicalLocation"] = ds.Tables[0].Rows[0]["physicalLocation"].ToString();
            Response.Redirect("Default.aspx");
        }
        else
        {
            lblErrorMsg.Visible = true;
            lblErrorMsg.Text = "Incorrect UserName Or Password";
        }
    }
}