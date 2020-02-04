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
public partial class addWebsiteBanner : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                if (Session["addWebsiteBanner"] != null)
                {
                    bannerId.Text = Session["addWebsiteBanner"].ToString();
                    bindGenderDisplay(bannerId.Text);
                    bindVerticalDisplay(bannerId.Text);
                    bindData(bannerId.Text);
                    if(Session["addWebsiteBanner"].ToString().Equals("0"))
                    {
                        bindDropDown();
                    }
                    else
                    {
                        storedProcedureCls obj = new storedProcedureCls();
                        DataTable dt = obj.getDropdowns(bannerId.Text, "fetchDropdowns");
                        rpt_dropdown.DataSource = dt;
                        rpt_dropdown.DataBind();
                        bannerType.Enabled = false;
                    }
                    
                }
                else
                {
                    Session["bannerSuccFail"] = "Session Error";
                    Response.Redirect("websiteBanners.aspx", true);
                }

            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    private void bindDropDown()
    {
        try
        {
            DataTable dtProgLang = new DataTable();
            dtProgLang.Columns.Add("CTSettingID");
            dtProgLang.Columns.Add("valueId");
            dtProgLang.Columns.Add("TableName");
            foreach (RepeaterItem itemEquipment in rpt_dropdown.Items)
            {

                DropDownList drp_dropdown = (DropDownList)itemEquipment.FindControl("drp_dropdown");
                DropDownList drp_dropValues = (DropDownList)itemEquipment.FindControl("drp_dropValues");
                Label TableName = (Label)itemEquipment.FindControl("TableName");
                dtProgLang.Rows.Add(drp_dropdown.SelectedValue, drp_dropValues.SelectedValue, TableName.Text); // Add Data 

            }

            dtProgLang.Rows.Add("-1", "-1","");

            rpt_dropdown.DataSource = dtProgLang;
            rpt_dropdown.DataBind();



        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    private void bindData(string bannerId)
    {
        try
        {
            storedProcedureCls obj = new storedProcedureCls();
            DataTable dt = obj.getTableWithCondition("banners", "bannerId", bannerId, "bannerId", "desc");
            if (!dt.Rows.Count.Equals(0))
            {
                bannerType.SelectedValue = dt.Rows[0]["bannerType"].ToString();
                bannerName.Text = dt.Rows[0]["bannerName"].ToString();
                if (!dt.Rows[0]["bannerImageName"].ToString().Equals(""))
                {
                    DataTable imagePath = obj.getTableWithCondition("imagePaths", "pathId", "1", "pathId", "desc");
                    if(!imagePath.Rows.Count.Equals(0))
                    {
                        if(!imagePath.Rows[0]["paths"].ToString().Equals("") && !dt.Rows[0]["bannerImageName"].ToString().Equals(""))
                        {
                            image1Display.Visible = true;
                            image1Display.ImageUrl = imagePath.Rows[0]["paths"].ToString() + dt.Rows[0]["bannerImageName"].ToString();
                            
                        }
                    }
                }
                
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    private void bindGenderDisplay(string bannerId)
    {
        try
        {
            storedProcedureCls obj = new storedProcedureCls();
            DataTable dt = obj.getGenderforBanner(bannerId);
            rpt_gender.DataSource = dt;
            rpt_gender.DataBind();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    private void bindVerticalDisplay(string bannerId)
    {
        try
        {
            storedProcedureCls obj = new storedProcedureCls();
            DataTable dt = obj.getVerticalforBanner(bannerId);
            rpt_vertical.DataSource = dt;
            rpt_vertical.DataBind();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void cancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("websiteBanners.aspx", true);
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
            request.Credentials = new NetworkCredential("images@finetouch.dzvdesk.com", "0v!$$S%m*Fuj");
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

    protected void save_Click(object sender, EventArgs e)
    {      
            try
            {
                if (Page.IsValid)
                {
                    string error = string.Empty;
                    DataTable gender = new DataTable();
                    gender.Columns.Add("ID"); gender.Columns.Add("genderId"); gender.Columns.Add("displayId");
                    foreach (RepeaterItem itemEquipment in rpt_gender.Items)
                    {
                        CheckBox checkValue = (CheckBox)itemEquipment.FindControl("genCheck");
                        if (checkValue.Checked.Equals(true))
                        {
                            Label bgId = (Label)itemEquipment.FindControl("bgId");
                            gender.Rows.Add(bgId.Text, checkValue.Attributes["Value"], 1);
                        }
                    }

                    DataTable vertical = new DataTable();
                    vertical.Columns.Add("ID"); vertical.Columns.Add("verticalId");

                    DataTable category = new DataTable();
                    category.Columns.Add("ID"); category.Columns.Add("verticalId"); category.Columns.Add("categoryId");

                    foreach (RepeaterItem itemEquipment in rpt_vertical.Items)
                    {
                        RadioButton checkValue = (RadioButton)itemEquipment.FindControl("verCheck");
                        if (checkValue.Checked.Equals(true))
                        {
                            Label bvId = (Label)itemEquipment.FindControl("bvId");
                            Repeater rpt_category = (Repeater)itemEquipment.FindControl("rpt_category");
                            vertical.Rows.Add(bvId.Text, checkValue.Attributes["Value"]);
                            foreach (RepeaterItem itemEquipment1 in rpt_category.Items)
                            {
                                CheckBox checkValue1 = (CheckBox)itemEquipment1.FindControl("catCheck");
                                if (checkValue1.Checked.Equals(true))
                                {
                                    Label bcId = (Label)itemEquipment1.FindControl("bcId");
                                    category.Rows.Add(bcId.Text, checkValue.Attributes["Value"], checkValue1.Attributes["Value"]);
                                }
                            }

                        }
                    }

                    DataTable drops = new DataTable();
                    drops.Columns.Add("dropId"); drops.Columns.Add("subDropId");
                    foreach (RepeaterItem itemEquipment in rpt_dropdown.Items)
                    {
                        DropDownList drp_dropdown = (DropDownList)itemEquipment.FindControl("drp_dropdown");
                        DropDownList drp_dropValues = (DropDownList)itemEquipment.FindControl("drp_dropValues");
                        if(!drp_dropdown.SelectedValue.Equals("-1") && !drp_dropValues.Items.Count.Equals(0))
                        {
                            drops.Rows.Add(drp_dropdown.SelectedValue, drp_dropValues.SelectedValue);
                        }                                          
                    }

                    if (gender.Rows.Count.Equals(0))
                    {
                        error += "Please Select atleast one gender <br/>";
                    }

                    if (vertical.Rows.Count.Equals(0))
                    {
                        error += "Please Select atleast one Vertical <br/>";
                    }

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


                    
                    int mheight = 0; int mwidth = 0; int msize = 0;
                    if (bannerType.SelectedValue.Equals("1"))
                    {
                        msize = 300;
                        mheight = 500;
                        mwidth = 1920;
                        /*msize = 181;
                        mheight = 400;
                        mwidth = 1600;*/
                    }
                    if (bannerType.SelectedValue.Equals("2") || bannerType.SelectedValue.Equals("7"))
                    {
                        msize = 25;
                        mheight = 200;
                        mwidth = 326;
                    }
                    if (bannerType.SelectedValue.Equals("3"))
                    {
                        msize = 25;
                        mheight = 360;
                        mwidth = 360;
                    }
                    if (bannerType.SelectedValue.Equals("4"))
                    {
                        msize = 100;
                        mheight = 756;
                        mwidth = 920;
                    }
                    if (bannerType.SelectedValue.Equals("5") || bannerType.SelectedValue.Equals("6"))
                    {
                        msize = 25;
                        mheight = 258;
                        mwidth = 316;
                    }
                    int imageError = 0;
                    
                    //if (size != msize)
                    //{
                    //    imageError = 1;
                    //}
                    if (height != mheight)
                    {
                        imageError = 1;
                    }
                    if (width != mwidth)
                    {
                        imageError = 1;
                    }
                    if (imageError.Equals(0))
                    {                    
                        int succ1 = uploadFTP(newFileName1, fileBytes);

                        if (succ1 == 1)
                        {
                            image1 = newFileName1;
                        }
                    }
                    else
                    {
                        error += "Image Size and KB not Matched <br/>";
                    }
                    
                    }
                else
                {
                    if (bannerId.Text.Equals("0"))
                    {
                        error += "Please Select banner Image <br/>";
                    }
                }
                    if (error.Equals(""))
                    {
                        string logs = "," + Session["userName"] + ":" + DateTime.Now;
                        storedProcedureCls obj = new storedProcedureCls();
                        string result = obj.saveBannerDetaiils(bannerName.Text, image1, gender, vertical, category,drops, bannerId.Text, logs,bannerType.SelectedValue);
                        Session["bannerSuccFail"] = result;
                        Response.Redirect("websiteBanners.aspx", true);
                    }
                    else
                    {
                        Label1.Text = error;
                        Label1.Visible = true;
                    }
                    //Response.Redirect("sellOnWebsite.aspx", true);
                }
                else
                {
                    Label1.Text = "";
                }
            }
        catch(Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void rpt_vertical_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            RadioButton verCheck = (RadioButton)e.Item.FindControl("verCheck");
            if (verCheck.Checked.Equals(true))
            {
                Repeater rpt_category = (Repeater)e.Item.FindControl("rpt_category");
                string bvId = ((DataRowView)e.Item.DataItem)["bvId"].ToString();
                HtmlTableCell row1 = (HtmlTableCell)e.Item.FindControl("showCategory");
                bindCategory(rpt_category, bvId, row1, verCheck.Attributes["Value"]);
                /*CheckBox btn = (CheckBox)(sender);
                RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
                Repeater pRepeater = ((Repeater)(rp1.NamingContainer));
                RepeaterItem pRepeater1 = ((RepeaterItem)(pRepeater.NamingContainer));
                Label wvId = (Label)pRepeater1.FindControl("wvId"); */


            }

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    private void bindCategory(Repeater rpt_category, string bvId, HtmlTableCell row1, string cat1ID)
    {
        try
        {
            storedProcedureCls obj = new storedProcedureCls();
            DataTable dt = obj.getCategoryforBanner(bvId, cat1ID);
            rpt_category.DataSource = dt;
            rpt_category.DataBind();
            //row1.Visible = true;
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void verCheck_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            //Uncheck all RadioButtons of the rptCustomer
            foreach (RepeaterItem item in rpt_vertical.Items)
            {
                ((RadioButton)item.FindControl("verCheck")).Checked = false;
                Repeater rpt_category1 = (Repeater)item.FindControl("rpt_category");
                HtmlTableCell row2 = (HtmlTableCell)item.FindControl("showCategory");
                rpt_category1.DataSource = new DataTable();
                rpt_category1.DataBind();
                //row2.Visible = false;
            }

            RadioButton RepeaterItem = (RadioButton)(sender);
            RepeaterItem rp1 = ((RepeaterItem)(RepeaterItem.NamingContainer));
            ((RadioButton)rp1.FindControl("verCheck")).Checked = true;
            RadioButton verCheck = (RadioButton)rp1.FindControl("verCheck");            
            Repeater rpt_category = (Repeater)rp1.FindControl("rpt_category");
            HtmlTableCell row1 = (HtmlTableCell)rp1.FindControl("showCategory");
            if (verCheck.Checked.Equals(true))
            {
                Label bvId = (Label)rp1.FindControl("bvId");
                bindCategory(rpt_category, bvId.Text, row1, verCheck.Attributes["Value"]);
            }
            else
            {
                rpt_category.DataSource = new DataTable();
                rpt_category.DataBind();
                //row1.Visible = false;
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void rpt_dropdown_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            string CTSettingID = ((DataRowView)e.Item.DataItem)["CTSettingID"].ToString();
            string valueId = ((DataRowView)e.Item.DataItem)["valueId"].ToString();
            string TableName = ((DataRowView)e.Item.DataItem)["TableName"].ToString();
            DropDownList drp_dropdown = e.Item.FindControl("drp_dropdown") as DropDownList;
            DropDownList drp_dropValues = e.Item.FindControl("drp_dropValues") as DropDownList;
            Label LblTableName = e.Item.FindControl("TableName") as Label;

            LblTableName.Text = TableName;
            storedProcedureCls obj = new storedProcedureCls();            
            DataTable dt = obj.getTableWithCondition("ColTableSetting","IsAssigned","True", "SettingName", "asc");         
            drp_dropdown.DataSource = dt;
            drp_dropdown.DataBind();
            drp_dropdown.Items.Insert(0, new ListItem("---- Select ----", "-1"));
            drp_dropdown.SelectedValue = CTSettingID;

            if(!TableName.Equals(""))
            {
                DataTable dt1 = obj.getSubDropdown(TableName, TableName.Substring(6));
                string colId = "Col"+ TableName.Substring(6)+"ID";
                string colName = "C" + TableName.Substring(6) + "Name";
                dt1.Columns[colId].ColumnName = "valueId";
                dt1.Columns[colName].ColumnName = "valueName";
                drp_dropValues.DataSource = dt1;
                drp_dropValues.DataBind();
                drp_dropValues.Items.Insert(0, new ListItem("---- Select "+ drp_dropdown.SelectedItem.Text + " ----", "-1"));
                drp_dropValues.SelectedValue = valueId;
            }
            

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void dropdownAdd_Click(object sender, EventArgs e)
    {
        try
        {
            bindDropDown();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void drp_dropdown_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList RepeaterItem = (DropDownList)(sender);
            RepeaterItem rp1 = ((RepeaterItem)(RepeaterItem.NamingContainer));
            DropDownList drp_dropdown = (DropDownList)rp1.FindControl("drp_dropdown");
            DropDownList drp_dropValues = (DropDownList)rp1.FindControl("drp_dropValues");
            Label TableName = (Label)rp1.FindControl("TableName");
            if(!drp_dropdown.SelectedValue.Equals("-1"))
            {
                storedProcedureCls obj = new storedProcedureCls();
                DataTable setting = obj.getTableWithCondition("ColTableSetting", "CTSettingID", drp_dropdown.SelectedValue, "SettingName", "asc");
                TableName.Text = (setting.Rows[0]["TableName"].ToString()).Replace(" ","");

                DataTable dt1 = obj.getSubDropdown(TableName.Text, TableName.Text.Substring(6));
                string colId = "Col" + TableName.Text.Substring(6) + "ID";
                string colName = "C" + TableName.Text.Substring(6) + "Name";
                dt1.Columns[colId].ColumnName = "valueId";
                dt1.Columns[colName].ColumnName = "valueName";
                drp_dropValues.DataSource = dt1;
                drp_dropValues.DataBind();
                drp_dropValues.Items.Insert(0, new ListItem("---- Select "+ drp_dropdown.SelectedItem.Text+" ----", "-1"));
                
            }
            else
            {
                drp_dropValues.DataSource = new DataTable();
                drp_dropValues.DataBind();
            }
           
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
        //set table name
        //get sub drown values

    }
}