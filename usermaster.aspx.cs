using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net;
using System.Text;

public partial class usermaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (!IsPostBack)
            {
                BindData();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "firedtSearch();", true);
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void BindData()
    {
        try
        {
            styleCls obj1 = new styleCls();
            DataTable dt1 = obj1.getTable("roles");
            userrole.DataSource = dt1;
            userrole.DataBind();
            //get data
            styleCls obj = new styleCls();
            DataTable dt = obj.getTable("login");
            GV.DataSource = dt;
            GV.DataBind();

            DataTable loc = obj.getTablewithID("Location","LTypeID2",1);
            physicalLocation.DataSource = loc;
            physicalLocation.DataBind();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void clearData()
    {
        try
        {
            username.Text = string.Empty;
            password.Text = string.Empty;
            hdnID.Text = "0";
            image1Display.Visible = false;
            image1Display.ImageUrl = "";

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected int uploadFTP(string fileName, byte[] fileBytes)
    {
        try
        {
            //FTP Server URL.
            // string ftp = "ftp://backup.dzvdesk.com/";
            string ftp = "ftp://finetouchimages.dzvdesk.com/";
            //FTP Folder name. Leave blank if you want to upload to root folder.
            string ftpFolder = "Uploads/";
            //Create FTP Request.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp + ftpFolder + fileName);
            request.Method = WebRequestMethods.Ftp.UploadFile;

            //Enter FTP Server credentials.
            // request.Credentials = new NetworkCredential("images@finetouchimages.dzvdesk.com", "Yt5fMY5Oy6~N");
            request.Credentials = new NetworkCredential("images@finetouchimages.dzvdesk.com", "0v!$$S%m*Fuj");
            request.ContentLength = fileBytes.Length;
            request.UsePassive = true;
            request.UseBinary = true;
            request.ServicePoint.ConnectionLimit = fileBytes.Length;
            request.EnableSsl = false;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(fileBytes, 0, fileBytes.Length);
                requestStream.Close();
            }

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            //lblMessage.Text += fileName + " uploaded.<br />";
            response.Close();
            return 1;
        }
        catch (WebException ex)
        {
            return 2;
            //throw new Exception((ex.Response as FtpWebResponse).StatusDescription);

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string image1 = string.Empty;
            string hasImage = "0";
            if (FileUpload1.HasFile)
            {
                byte[] fileBytes = null;

                //Read the FileName and convert it to Byte array.
                string fileName1 = Path.GetFileName(FileUpload1.FileName);
                string newFileName1 = Path.Combine(Path.GetDirectoryName(fileName1)
                    , string.Concat(Path.GetFileNameWithoutExtension(fileName1)
                                   , DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")
                                   , Path.GetExtension(fileName1)
                                   )
                    );
                //using (StreamReader fileStream = new StreamReader(FileUpload1.PostedFile.InputStream))
                //{
                //    fileBytes = Encoding.UTF8.GetBytes(fileStream.ReadToEnd());
                //    fileStream.Close();
                //}
                using (BinaryReader fileStream = new BinaryReader(FileUpload1.PostedFile.InputStream))
                {
                    fileBytes = fileStream.ReadBytes(FileUpload1.PostedFile.ContentLength);
                    fileStream.Close();
                }
                int succ1 = uploadFTP(newFileName1, fileBytes);

                if (succ1 == 1)
                {
                    image1 = newFileName1;
                    hasImage = "1";
                }
            }

            userCls obj = new userCls();
            int success = obj.addEditUsers(username.Text,password.Text, hdnID.Text,userrole.SelectedValue, image1, hasImage,uType.SelectedValue,physicalLocation.SelectedValue);
            
            if (success.Equals(0))
            {
                clearData();
                devCapone.Visible = false;
                divAddAlert.Visible = true;
                divUpdAlert.Visible = false;
            }else if(success.Equals(1))
            {
                divErr.InnerHtml = "Duplicate Password";
                divErr.Visible = true;
                divUpdAlert.Visible = false;
                divAddAlert.Visible = false;
            }
            else
            {
                clearData();
                devCapone.Visible = false;
                divUpdAlert.Visible = true;
                divAddAlert.Visible = false;
            }
            BindData();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            clearData();
            devCapone.Visible = false;
            divErr.Visible = false;
            divAddAlert.Visible = false;
            divUpdAlert.Visible = false;
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {            
            clearData();
            Button btn = ((Button)(sender));
            ScriptManager.GetCurrent(this).RegisterPostBackControl(btn);
            devCapone.Visible = true;
            divErr.Visible = false;
            divAddAlert.Visible = false;
            divUpdAlert.Visible = false;
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void edit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = ((LinkButton)(sender));
            ScriptManager.GetCurrent(this).RegisterPostBackControl(btn);
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            Label userid = (Label)rp1.FindControl("userid");
            styleCls obj = new styleCls();
            DataTable lot = obj.getTablewithID("login", "userid", Convert.ToInt32(userid.Text));
            username.Text = lot.Rows[0]["username"].ToString();
            password.Text = lot.Rows[0]["password"].ToString();
            userrole.SelectedValue = lot.Rows[0]["userrole"].ToString();
            uType.SelectedValue = lot.Rows[0]["uType"].ToString();
            physicalLocation.SelectedValue = lot.Rows[0]["physicalLocation"].ToString();
            hdnID.Text = lot.Rows[0]["userid"].ToString();
            image1Display.Visible = false;
            image1Display.ImageUrl = "";
            if (!lot.Rows[0]["uImage"].ToString().Equals(""))
            {
                image1Display.Visible = true;
                image1Display.ImageUrl = "http://finetouchimages.dzvdesk.com/Uploads/" + lot.Rows[0]["uImage"].ToString();
            }
            
            devCapone.Visible = true;
            divErr.Visible = false;
            divAddAlert.Visible = false;
            divUpdAlert.Visible = false;
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}