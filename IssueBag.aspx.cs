using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class IssueBag : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ctfrmDet.Visible = true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtHSN.Text = String.Empty;

        btnSave.Text = "Save";
        ctfrmDet.Visible = false;
    }
}