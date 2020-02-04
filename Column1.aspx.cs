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

public partial class Column1aspx : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            BindColumn1();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "firedtSearch();", true);
        }
    }

    public void BindColumn1()
    {
        DataBase.StyleColumnTable ObjBind = new DataBase.StyleColumnTable();

        DataSet ds = ObjBind.BindCol1();

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

        image1Display.Visible = false;
        image1Display.ImageUrl = string.Empty;
        if (e.CommandName.ToLower().Equals("edit"))
        {
            int ID = int.Parse(e.CommandArgument.ToString());
            hdnID.Text = ID.ToString(); ;

            ctfrmDet.Visible = true;

            DataSet ds = objEdit.GetCol1ByID(ID);

            txtName.Text = ds.Tables[0].Rows[0]["C1Name"].ToString();
            if (ds.Tables[0].Rows[0]["brandStatus"].ToString().Equals("True"))
            {
                brandStatus.SelectedValue = "1";
            }
            else
            {
                brandStatus.SelectedValue = "0";
            }
            

            if (!ds.Tables[0].Rows[0]["brandImage"].ToString().Equals(""))
            {
                storedProcedureCls obj = new storedProcedureCls();
                DataTable imagePath = obj.getTableWithCondition("imagePaths", "pathId", "1", "pathId", "desc");
                if (!imagePath.Rows.Count.Equals(0))
                {
                    if (!imagePath.Rows[0]["paths"].ToString().Equals("") && !ds.Tables[0].Rows[0]["brandImage"].ToString().Equals(""))
                    {
                        image1Display.Visible = true;
                        image1Display.ImageUrl = imagePath.Rows[0]["paths"].ToString() + ds.Tables[0].Rows[0]["brandImage"].ToString();

                    }
                }
              
            }


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
        string imageError = "0";
        string image1 = string.Empty;
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
            int height = 0;
            int width = 0; decimal size = 0;
            using (BinaryReader fileStream = new BinaryReader(FileUpload1.PostedFile.InputStream))
            {
                fileBytes = fileStream.ReadBytes(FileUpload1.PostedFile.ContentLength);
                System.Drawing.Image img = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);
                height = img.Height;
                width = img.Width;
                size = Math.Round(((decimal)FileUpload1.PostedFile.ContentLength / (decimal)1024), 2);
                fileStream.Close();
            }

            int msize = 25;
            int mheight = 300;
            int mwidth = 300;
            
            

            /*if (size != msize)
            {
                imageError = "1";
            }*/
            if (height != mheight)
            {
                imageError = "1";
            }
            if (width != mwidth)
            {
                imageError = "1";
            }
            if (imageError.Equals("0"))
            {
                int succ1 = uploadFTP(newFileName1, fileBytes);

                if (succ1 == 1)
                {
                    image1 = newFileName1;
                }
            }
            else
            {
                imageError += "Image Size and KB not Matched <br/>";
            }

        }

        if (imageError.Equals("0"))
        {
            DataBase.StyleColumnTable objAdd = new DataBase.StyleColumnTable();

            //int ID = objAdd.AddCol1(txtName.Text.Trim());
            int ID = objAdd.AddCol1New(txtName.Text.Trim(),image1,brandStatus.SelectedValue,"0");
            if (ID.Equals(0))
            {
                BindColumn1();
                Clear();
                ctfrmDet.Visible = false;
                objAdd = null;
            }
        }
        else
        {

        }
        
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string imageError = "0";
        string image1 = string.Empty;
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
            int height = 0;
            int width = 0; decimal size = 0;
            using (BinaryReader fileStream = new BinaryReader(FileUpload1.PostedFile.InputStream))
            {
                fileBytes = fileStream.ReadBytes(FileUpload1.PostedFile.ContentLength);
                System.Drawing.Image img = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);
                height = img.Height;
                width = img.Width;
                size = Math.Round(((decimal)FileUpload1.PostedFile.ContentLength / (decimal)1024), 2);
                fileStream.Close();
            }

            int msize = 25;
            int mheight = 300;
            int mwidth = 300;



            /*if (size != msize)
            {
                imageError = "1";
            }*/
            if (height != mheight)
            {
                imageError = "1";
            }
            if (width != mwidth)
            {
                imageError = "1";
            }
            if (imageError.Equals("0"))
            {
                int succ1 = uploadFTP(newFileName1, fileBytes);

                if (succ1 == 1)
                {
                    image1 = newFileName1;
                }
            }
            else
            {
                imageError += "Image Size and KB not Matched <br/>";
            }

        }

        DataBase.StyleColumnTable objUpdate = new DataBase.StyleColumnTable();

        //int Success = objUpdate.UpdateCol1(txtName.Text.Trim(),image1,brandStatus.SelectedValue, hdnID.Value);
        int Success = objUpdate.AddCol1New(txtName.Text.Trim(), image1, brandStatus.SelectedValue, hdnID.Text);
        if (Success.Equals(0))
        {
            btnUpdate.Visible = false;
            btnSave.Visible = true;

            Clear();

            BindColumn1();
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