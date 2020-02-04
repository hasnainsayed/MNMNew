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

public partial class addStyle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                bindCatName();
                bindItemCategory();
                DataTable dt = new DataTable();
                if (!Session["EditStyleID"].ToString().Equals("0"))
                {

                    string styleId = Session["EditStyleID"].ToString();
                    
                    StyleID.Text = styleId;
                    styleCls obj = new styleCls();
                    dt = obj.getItemStyleByID(styleId);

                    //category binding
                    drp_itemCategory.SelectedValue = dt.Rows[0]["ItemCatID"].ToString();
                    if (dt.Rows[0]["ItemCatID"].ToString().Equals(""))
                    {
                        drp_catTwo.DataSource = new DataTable();
                        drp_catTwo.DataBind();
                    }
                    else
                    {
                        DataTable dtCatTwo = obj.bindCategoryTwo(Convert.ToInt32(dt.Rows[0]["ItemCatID"].ToString()));
                        drp_catTwo.DataSource = dtCatTwo;
                        drp_catTwo.DataBind();
                        drp_catTwo.SelectedValue = dt.Rows[0]["Cat2ID"].ToString();
                    }
                    if (dt.Rows[0]["Cat2ID"].ToString().Equals(""))
                    {
                        drp_catThree.DataSource = new DataTable();
                        drp_catThree.DataBind();
                    }
                    else
                    {
                        DataTable dtCatthree = obj.bindCategoryThree(Convert.ToInt32(dt.Rows[0]["Cat2ID"].ToString()));
                        drp_catThree.DataSource = dtCatthree;
                        drp_catThree.DataBind();
                        drp_catThree.SelectedValue = dt.Rows[0]["Cat3ID"].ToString();
                    }
                    if (dt.Rows[0]["Cat3ID"].ToString().Equals(""))
                    {
                        drp_catFour.DataSource = new DataTable();
                        drp_catFour.DataBind();
                    }
                    else
                    {
                        DataTable dtCatFour = obj.bindCategoryFour(Convert.ToInt32(dt.Rows[0]["Cat3ID"].ToString()));
                        drp_catFour.DataSource = dtCatFour;
                        drp_catFour.DataBind();
                        drp_catFour.SelectedValue = dt.Rows[0]["Cat4ID"].ToString();
                    }

                    if (dt.Rows[0]["Cat4ID"].ToString().Equals(""))
                    {
                        drp_catFive.DataSource = new DataTable();
                        drp_catFive.DataBind();
                    }
                    else
                    {
                        DataTable dtCatFive = obj.bindCategoryFive(Convert.ToInt32(dt.Rows[0]["Cat4ID"].ToString()));
                        drp_catFive.DataSource = dtCatFive;
                        drp_catFive.DataBind();
                        drp_catFive.SelectedValue = dt.Rows[0]["Cat5ID"].ToString();
                    }

                    // default values 
                    txtTitle.Text = dt.Rows[0]["title"].ToString();
                    mrp.Text = dt.Rows[0]["mrp"].ToString();


					string imagelink = "http://mnmimages.dzvdesk.com/Uploads/";
					if (!dt.Rows[0]["image1"].ToString().Equals(""))
					{
						image1Display.Visible = true;
						image1Display.ImageUrl = imagelink + dt.Rows[0]["image1"].ToString();
                        image1DisplayDel.Visible = true;
                    }
					if (!dt.Rows[0]["image2"].ToString().Equals(""))
					{
						image2Display.Visible = true;
						image2Display.ImageUrl = imagelink + dt.Rows[0]["image2"].ToString();
                        image2DisplayDel.Visible = true;
                    }
					if (!dt.Rows[0]["image3"].ToString().Equals(""))
					{
						image3Display.Visible = true;
						image3Display.ImageUrl = imagelink + dt.Rows[0]["image3"].ToString();
                        image3DisplayDel.Visible = true;
                    }
					if (!dt.Rows[0]["image4"].ToString().Equals(""))
					{
						image4Display.Visible = true;
						image4Display.ImageUrl = imagelink + dt.Rows[0]["image4"].ToString();
                        image4DisplayDel.Visible = true;
                    }
					if (!dt.Rows[0]["image5"].ToString().Equals(""))
					{
						image5Display.Visible = true;
						image5Display.ImageUrl = imagelink + dt.Rows[0]["image5"].ToString();
                        image5DisplayDel.Visible = true;
                    }
					if (!dt.Rows[0]["image6"].ToString().Equals(""))
					{
						image6Display.Visible = true;
						image6Display.ImageUrl = imagelink + dt.Rows[0]["image6"].ToString();
                        image6DisplayDel.Visible = true;
                    }
                    

                    saveStyle.Text = "Update Style";

                }

                Session["styleDt"] = dt;
                bindFieldValue();
                bindDropDown();
                drp_itemCategory.Items.Insert(0, new ListItem("---- Select ----", "-1"));
                drp_catTwo.Items.Insert(0, new ListItem("---- Select ----", "-1"));
                drp_catThree.Items.Insert(0, new ListItem("---- Select ----", "-1"));
                drp_catFour.Items.Insert(0, new ListItem("---- Select ----", "-1"));
                drp_catFive.Items.Insert(0, new ListItem("---- Select ----", "-1"));
                Session.Remove("styleDt");
                if (!Session["EditStyleID"].ToString().Equals("0"))
                {
                    Check();
                    Session.Remove("EditStyleID");
                }
            }
            else { ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "firedtSearch();", true);
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void bindCatName()
    {
        try
        {
            styleCls obj = new styleCls();
            DataTable dt = obj.getCatName();
            if (!dt.Rows.Count.Equals(0))
            {
                lblCat2.Text = dt.Rows[0]["SettingName"].ToString();
                lblCat3.Text = dt.Rows[1]["SettingName"].ToString();
                lblCat4.Text = dt.Rows[2]["SettingName"].ToString();
                lblCat5.Text = dt.Rows[3]["SettingName"].ToString();
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }
    
    protected void bindItemCategory()
    {
        try
        {
            styleCls obj = new styleCls();
            DataTable dt = obj.bindItemCategory();
            drp_itemCategory.DataSource = dt;
            drp_itemCategory.DataBind();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void bindFieldValue()
    {
        try
        {
            styleCls obj = new styleCls();
            DataTable dt = obj.getDataFieldName();
            rpt_DataField.DataSource = dt;
            rpt_DataField.DataBind();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void bindDropDown()
    {
        try
        {
            styleCls obj = new styleCls();
            DataTable dt = obj.getDropDown();
            rpt_dropdown.DataSource = dt;
            rpt_dropdown.DataBind();
        }
        catch (Exception ex)
        {

            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void drp_itemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ColNStySettingCls obj2 = new ColNStySettingCls();
            DataTable ut = obj2.Checkcategory(drp_itemCategory.SelectedValue);
            if (ut.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please First Add Setting For Category !');window.location ='ItemStyleSearchAdd.aspx';", true);
            }
            else
            {
                styleCls obj = new styleCls();
                DataTable dt = obj.bindCategoryTwo(Convert.ToInt32(drp_itemCategory.SelectedValue));
                drp_catTwo.DataSource = dt;
                drp_catTwo.DataBind();
                drp_catTwo.Items.Insert(0, new ListItem("---- Select ----", "-1"));
                drp_catThree.Items.Clear();
                drp_catThree.Items.Insert(0, new ListItem("---- Select ----", "-1"));
                drp_catFour.Items.Clear();
                drp_catFour.Items.Insert(0, new ListItem("---- Select ----", "-1"));
                drp_catFive.Items.Clear();
                drp_catFive.Items.Insert(0, new ListItem("---- Select ----", "-1"));

                Check();

            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }
    public void Check()
    {
        try
        {
            ColNStySettingCls ob = new ColNStySettingCls();
            foreach (RepeaterItem rpt in rpt_DataField.Items)
            {

                CheckBox controlName = rpt.FindControl("controlName") as CheckBox;
                Label lblcolno = rpt.FindControl("lblcolno") as Label;
                DataTable dt2 = ob.GetStyleSetting(drp_itemCategory.SelectedValue, lblcolno.Text);
                controlName.Enabled = true;
                controlName.Checked = false;
                string mandatory = dt2.Rows[0]["mandatory"].ToString();
                string optinal = dt2.Rows[0]["optinal"].ToString();
                string Na = dt2.Rows[0]["Na"].ToString();
                if (mandatory.Equals("True"))
                {
                    controlName.Enabled = false;
                    controlName.Checked = true;
                }
                else if (optinal.Equals("True"))
                {
                    controlName.Checked = true;
                }
                else if (Na.Equals("True"))
                {
                    controlName.Checked = false;
                }


            }


            foreach (RepeaterItem rpt in rpt_dropdown.Items)
            {

                CheckBox drpName = rpt.FindControl("drpName") as CheckBox;
                drpName.Enabled = true;
                drpName.Checked = false;
                Label lblcolno = rpt.FindControl("lblcolno") as Label;
                DataTable dt2 = ob.GetColumnSetting(drp_itemCategory.SelectedValue, lblcolno.Text);
                string mandatory = dt2.Rows[0]["mandatory"].ToString();
                string optinal = dt2.Rows[0]["optinal"].ToString();
                string Na = dt2.Rows[0]["Na"].ToString();
                if (mandatory.Equals("True"))
                {
                    drpName.Checked = true;
                    drpName.Enabled = false;
                }
                else if (optinal.Equals("True"))
                {
                    drpName.Checked = true;
                }
                else if (Na.Equals("True"))
                {
                    drpName.Checked = false;
                }


            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }
    protected void drp_catTwo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            styleCls obj = new styleCls();
            DataTable dt = obj.bindCategoryThree(Convert.ToInt32(drp_catTwo.SelectedValue));
            drp_catThree.DataSource = dt;
            drp_catThree.DataBind();
            drp_catThree.Items.Insert(0, new ListItem("---- Select ----", "-1"));
            drp_catFour.Items.Clear();
            drp_catFour.Items.Insert(0, new ListItem("---- Select ----", "-1"));
            drp_catFive.Items.Clear();
            drp_catFive.Items.Insert(0, new ListItem("---- Select ----", "-1"));
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void drp_catThree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            styleCls obj = new styleCls();
            DataTable dt = obj.bindCategoryFour(Convert.ToInt32(drp_catThree.SelectedValue));
            drp_catFour.DataSource = dt;
            drp_catFour.DataBind();
            drp_catFour.Items.Insert(0, new ListItem("---- Select ----", "-1"));
            drp_catFive.Items.Clear();
            drp_catFive.Items.Insert(0, new ListItem("---- Select ----", "-1"));
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void drp_catFour_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            styleCls obj = new styleCls();
            DataTable dt = obj.bindCategoryFive(Convert.ToInt32(drp_catFour.SelectedValue));
            drp_catFive.DataSource = dt;
            drp_catFive.DataBind();
            drp_catFive.Items.Insert(0, new ListItem("---- Select ----", "-1"));
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void rpt_DataField_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            styleCls obj = new styleCls();

            string IsAssignedDataField = ((DataRowView)e.Item.DataItem)["IsAssigned"].ToString();
            
            HtmlTableRow tb1 = (HtmlTableRow)e.Item.FindControl("datafieldHideShow");
            CheckBox controlName = (CheckBox)e.Item.FindControl("controlName");
            if (IsAssignedDataField.Equals("False"))
            {
                tb1.Visible = false;
            }
            else
            {                
                
                DataTable dt = (DataTable)Session["styleDt"];
                
                if (!dt.Rows.Count.Equals(0))
                {
                    
                    string controlNameDt = controlName.Attributes["Value"];
                    TextBox controlSearch = (TextBox)e.Item.FindControl("controlSearch");
                    controlSearch.Text = dt.Rows[0][controlNameDt].ToString();
                    if (!dt.Rows[0][controlNameDt].ToString().Equals(""))
                    {
                        controlName.Checked = true;
                    }
                }
                else if (saveStyle.Text.Equals("Save Style"))
                {
                    controlName.Checked = true;
                }

            }
            //if(mandatory.Equals("True"))
            //{
            //    controlName.Enabled= true;
            //}
            //else if (optinal.Equals("True"))
            //{
            //    controlName.Checked = true;
            //}
            //else if (Na.Equals("True"))
            //{
            //    controlName.Checked = false;
            //}

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void rpt_dropdown_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            string IsAssignedDataField = ((DataRowView)e.Item.DataItem)["IsAssigned"].ToString();
            DropDownList drpCols = (DropDownList)e.Item.FindControl("drpCols");
            HtmlTableRow tb1 = (HtmlTableRow)e.Item.FindControl("drpHideShow");
            
            
            DataTable dt = new DataTable();
            styleCls obj = new styleCls();
            if (IsAssignedDataField.Equals("False"))
            {
                tb1.Visible = false;
            }
            else
            {
                                
                DataTable styleDt = (DataTable)Session["styleDt"];
                string drpName = ((DataRowView)e.Item.DataItem)["checkValue"].ToString();
                if (drpName.Equals("Col1"))
                {
                    dt = obj.getTable("Column1");
                    drpCols.DataTextField = "C1Name";
                    drpCols.DataValueField = "Col1ID";
                  
                }
                else if (drpName.Equals("Col2"))
                {
                    dt = obj.getTable("Column2");
                    drpCols.DataTextField = "C2Name";
                    drpCols.DataValueField = "Col2ID";
                }
                else if (drpName.Equals("Col3"))
                {
                    dt = obj.getTable("Column3");
                    drpCols.DataTextField = "C3Name";
                    drpCols.DataValueField = "Col3ID";
                }
                else if (drpName.Equals("Col4"))
                {
                    dt = obj.getTable("Column4");
                    drpCols.DataTextField = "C4Name";
                    drpCols.DataValueField = "Col4ID";
                }
                else if (drpName.Equals("Col5"))
                {
                    dt = obj.getTable("Column5");
                    drpCols.DataTextField = "C5Name";
                    drpCols.DataValueField = "Col5ID";
                }
                else if (drpName.Equals("Col6"))
                {
                    dt = obj.getTable("Column6");
                    drpCols.DataTextField = "C6Name";
                    drpCols.DataValueField = "Col6ID";
                }
                else if (drpName.Equals("Col7"))
                {
                    dt = obj.getTable("Column7");
                    drpCols.DataTextField = "C7Name";
                    drpCols.DataValueField = "Col7ID";
                }
                else if (drpName.Equals("Col8"))
                {
                    dt = obj.getTable("Column8");
                    drpCols.DataTextField = "C8Name";
                    drpCols.DataValueField = "Col8ID";
                }
                else if (drpName.Equals("Col9"))
                {
                    dt = obj.getTable("Column9");
                    drpCols.DataTextField = "C9Name";
                    drpCols.DataValueField = "Col9ID";
                }
                else if (drpName.Equals("Col10"))
                {
                    dt = obj.getTable("Column10");
                    drpCols.DataTextField = "C10Name";
                    drpCols.DataValueField = "Col10ID";
                }
                else if (drpName.Equals("Col11"))
                {
                    dt = obj.getTable("Column11");
                    drpCols.DataTextField = "C11Name";
                    drpCols.DataValueField = "Col11ID";
                }
                else if (drpName.Equals("Col12"))
                {
                    dt = obj.getTable("Column12");
                    drpCols.DataTextField = "C12Name";
                    drpCols.DataValueField = "Col12ID";
                }
                else if (drpName.Equals("Col13"))
                {
                    dt = obj.getTable("Column13");
                    drpCols.DataTextField = "C13Name";
                    drpCols.DataValueField = "Col13ID";
                }
                else if (drpName.Equals("Col14"))
                {
                    dt = obj.getTable("Column14");
                    drpCols.DataTextField = "C14Name";
                    drpCols.DataValueField = "Col14ID";
                }
                else if (drpName.Equals("Col15"))
                {
                    dt = obj.getTable("Column15");
                    drpCols.DataTextField = "C15Name";
                    drpCols.DataValueField = "Col15ID";
                }
                else if (drpName.Equals("Col16"))
                {
                    dt = obj.getTable("Column16");
                    drpCols.DataTextField = "C16Name";
                    drpCols.DataValueField = "Col16ID";
                }
                else if (drpName.Equals("Col17"))
                {
                    dt = obj.getTable("Column17");
                    drpCols.DataTextField = "C17Name";
                    drpCols.DataValueField = "Col17ID";
                }
                else if (drpName.Equals("Col18"))
                {
                    dt = obj.getTable("Column18");
                    drpCols.DataTextField = "C18Name";
                    drpCols.DataValueField = "Col18ID";
                }
                else if (drpName.Equals("Col19"))
                {
                    dt = obj.getTable("Column19");
                    drpCols.DataTextField = "C19Name";
                    drpCols.DataValueField = "Col19ID";
                }
                else if (drpName.Equals("Col20"))
                {
                    dt = obj.getTable("Column20");
                    drpCols.DataTextField = "C20Name";
                    drpCols.DataValueField = "Col20ID";
                }
                drpCols.DataSource = dt;
                drpCols.DataBind();
                drpCols.Items.Insert(0, new ListItem("---- Select ----", "-1"));
                CheckBox drpName1 = (CheckBox)e.Item.FindControl("drpName");
                string defaultSelection = ((DataRowView)e.Item.DataItem)["defaultSelection"].ToString();
                if (defaultSelection.Equals("1"))
                {
                    drpName1.Enabled = false;
                }
                if (!styleDt.Rows.Count.Equals(0))
                { 
                    drpCols.SelectedValue = styleDt.Rows[0][drpName].ToString();
                    if (!styleDt.Rows[0][drpName].ToString().Equals("") && !styleDt.Rows[0][drpName].ToString().Equals("-1"))
                    {
                        drpName1.Checked = true;
                        
                    }
                }
                else if (saveStyle.Text.Equals("Save Style"))
                {
                    drpName1.Checked = true;
                    
                }
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected int uploadFTP(string fileName, byte[] fileBytes)
    {
        try
            {
			//FTP Server URL.
			// string ftp = "ftp://backup.dzvdesk.com/";
			string ftp = "ftp://mnmimages.dzvdesk.com/";
			//FTP Folder name. Leave blank if you want to upload to root folder.
			string ftpFolder = "Uploads/";
                //Create FTP Request.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp + ftpFolder + fileName);
                request.Method = WebRequestMethods.Ftp.UploadFile;

			//Enter FTP Server credentials.
			// request.Credentials = new NetworkCredential("images@finetouchimages.dzvdesk.com", "Yt5fMY5Oy6~N");
			request.Credentials = new NetworkCredential("images@mnmimages.dzvdesk.com", "8mdYBox[e1zS");
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

    protected void saveStyle_Click(object sender, EventArgs e)
    {
        try
        {
            ColNStySettingCls obj2 = new ColNStySettingCls();
            DataTable ut = obj2.Checkcategory(drp_itemCategory.SelectedValue);
            if (ut.Rows.Count.Equals(0))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please First Add Setting For Category !');window.location ='ItemStyleSearchAdd.aspx';", true);
            }
            else
            {
                string errors = string.Empty;

                /*WebClient client = new WebClient();
                string FileUpload2N = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string tempPath = Path.GetTempPath();
                byte[] responseBinary = client.UploadFile("http://backup.dzvdesk.com/test.php", FileUpload2N);     */
                string image1 = string.Empty;
                string image2 = string.Empty;
                string image3 = string.Empty;
                string image4 = string.Empty;
                string image5 = string.Empty;
                string image6 = string.Empty;
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
                    }
                }

                if (FileUpload2.HasFile)
                {
                    byte[] fileBytes2 = null;

                    //Read the FileName and convert it to Byte array.
                    string fileName2 = Path.GetFileName(FileUpload2.FileName);
                    string newFileName2 = Path.Combine(Path.GetDirectoryName(fileName2)
                        , string.Concat(Path.GetFileNameWithoutExtension(fileName2)
                                       , DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")
                                       , Path.GetExtension(fileName2)
                                       )
                        );
                    using (BinaryReader fileStream = new BinaryReader(FileUpload2.PostedFile.InputStream))
                    {
                        fileBytes2 = fileStream.ReadBytes(FileUpload2.PostedFile.ContentLength);
                        fileStream.Close();
                    }
                    int succ2 = uploadFTP(newFileName2, fileBytes2);

                    if (succ2 == 1)
                    {
                        image2 = newFileName2;
                    }
                }

                if (FileUpload3.HasFile)
                {
                    byte[] fileBytes3 = null;

                    //Read the FileName and convert it to Byte array.
                    string fileName3 = Path.GetFileName(FileUpload3.FileName);
                    string newfileName3 = Path.Combine(Path.GetDirectoryName(fileName3)
                        , string.Concat(Path.GetFileNameWithoutExtension(fileName3)
                                       , DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")
                                       , Path.GetExtension(fileName3)
                                       )
                        );
                    using (BinaryReader fileStream = new BinaryReader(FileUpload3.PostedFile.InputStream))
                    {
                        fileBytes3 = fileStream.ReadBytes(FileUpload3.PostedFile.ContentLength);
                        fileStream.Close();
                    }
                    int succ3 = uploadFTP(newfileName3, fileBytes3);

                    if (succ3 == 1)
                    {
                        image3 = newfileName3;
                    }
                }

                if (FileUpload4.HasFile)
                {
                    byte[] fileBytes4 = null;

                    //Read the FileName and convert it to Byte array.
                    string fileName4 = Path.GetFileName(FileUpload4.FileName);
                    string newfileName4 = Path.Combine(Path.GetDirectoryName(fileName4)
                        , string.Concat(Path.GetFileNameWithoutExtension(fileName4)
                                       , DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")
                                       , Path.GetExtension(fileName4)
                                       )
                        );
                    using (BinaryReader fileStream = new BinaryReader(FileUpload4.PostedFile.InputStream))
                    {
                        fileBytes4 = fileStream.ReadBytes(FileUpload4.PostedFile.ContentLength);
                        fileStream.Close();
                    }
                    int succ4 = uploadFTP(newfileName4, fileBytes4);

                    if (succ4 == 1)
                    {
                        image4 = newfileName4;
                    }
                }

                if (FileUpload5.HasFile)
                {
                    byte[] fileBytes5 = null;

                    //Read the FileName and convert it to Byte array.
                    string fileName5 = Path.GetFileName(FileUpload5.FileName);
                    string newfileName5 = Path.Combine(Path.GetDirectoryName(fileName5)
                        , string.Concat(Path.GetFileNameWithoutExtension(fileName5)
                                       , DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")
                                       , Path.GetExtension(fileName5)
                                       )
                        );
                    using (BinaryReader fileStream = new BinaryReader(FileUpload5.PostedFile.InputStream))
                    {
                        fileBytes5 = fileStream.ReadBytes(FileUpload5.PostedFile.ContentLength);
                        fileStream.Close();
                    }
                    int succ5 = uploadFTP(newfileName5, fileBytes5);

                    if (succ5 == 1)
                    {
                        image5 = newfileName5;
                    }
                }

                if (FileUpload6.HasFile)
                {
                    byte[] fileBytes6 = null;

                    //Read the FileName and convert it to Byte array.
                    string fileName6 = Path.GetFileName(FileUpload6.FileName);
                    string newfileName6 = Path.Combine(Path.GetDirectoryName(fileName6)
                        , string.Concat(Path.GetFileNameWithoutExtension(fileName6)
                                       , DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")
                                       , Path.GetExtension(fileName6)
                                       )
                        );
                    using (BinaryReader fileStream = new BinaryReader(FileUpload6.PostedFile.InputStream))
                    {
                        fileBytes6 = fileStream.ReadBytes(FileUpload6.PostedFile.ContentLength);
                        fileStream.Close();
                    }
                    int succ6 = uploadFTP(newfileName6, fileBytes6);

                    if (succ6 == 1)
                    {
                        image6 = newfileName6;
                    }
                }

                styleCls obj = new styleCls();
                DataTable cat = new DataTable();
                cat.Columns.Add("Column");
                cat.Columns.Add("Search");
                if (!drp_itemCategory.SelectedValue.Equals("-1"))
                {
                    cat.Rows.Add("ItemCatID", drp_itemCategory.SelectedValue);
                }
                if (!drp_catTwo.SelectedValue.Equals("-1"))
                {
                    cat.Rows.Add("Cat2ID", drp_catTwo.SelectedValue);
                }
                if (!drp_catThree.SelectedValue.Equals("-1"))
                {
                    cat.Rows.Add("Cat3ID", drp_catThree.SelectedValue);
                }
                if (!drp_catFour.SelectedValue.Equals("-1"))
                {
                    cat.Rows.Add("Cat4ID", drp_catFour.SelectedValue);
                }
                if (!drp_catFive.SelectedValue.Equals("-1"))
                {
                    cat.Rows.Add("Cat5ID", drp_catFive.SelectedValue);
                }

                string err = string.Empty;
                string err1 = string.Empty;
                if (drp_itemCategory.SelectedValue.Equals("-1"))
                {
                    err += "Vertical <br>";
                    err1 += "Vertical <br>";
                }
                if (drp_catTwo.SelectedValue.Equals("-1"))
                {
                    err += "Sub Category <br>";
                    err1 += "Sub Category <br>";
                }
                //if (drp_catThree.SelectedValue.Equals("-1"))
                //{
                //    err += "Type <br>";
                //    err1 += "Type <br>";
                //}
                if (txtTitle.Text.Equals(""))
                {
                    err += "Title <br>";
                    err1 += "Title <br>";
                }
                if (mrp.Text.Equals("") || mrp.Text.Equals("0") || mrp.Text.Equals(0))
                {
                    err += "MRP <br>";
                    err1 += "MRP <br>";
                }
                if (saveStyle.Text.Equals("Save Style"))
                {
                    DataTable field = new DataTable();
                    field.Columns.Add("Column");
                    field.Columns.Add("Search");

                    foreach (RepeaterItem itemEquipment in rpt_DataField.Items)
                    {
                        Label IsAssignedDataField = (Label)itemEquipment.FindControl("IsAssignedDataField");

                        if (IsAssignedDataField.Text.Equals("True"))
                        {
                            TextBox controlSearch = (TextBox)itemEquipment.FindControl("controlSearch");
                            CheckBox controlName = (CheckBox)itemEquipment.FindControl("controlName");
                            if (controlName.Checked && !controlSearch.Text.Equals(""))
                            {
                                field.Rows.Add(controlName.Attributes["Value"], controlSearch.Text.Replace("'", "''").Trim());

                            }
                            else if (controlName.Checked && controlSearch.Text.Equals(""))
                            {
                                HtmlTableCell settingFieldName = (HtmlTableCell)itemEquipment.FindControl("settingFieldName");
                                err += settingFieldName.InnerText.ToString() + " ,";
                            }
                        }

                    }

                    DataTable drp = new DataTable();
                    drp.Columns.Add("Column");
                    drp.Columns.Add("Search");
                    foreach (RepeaterItem itemEquipment in rpt_dropdown.Items)
                    {
                        Label IsAssigneddrp = (Label)itemEquipment.FindControl("IsAssigneddrp");

                        if (IsAssigneddrp.Text.Equals("True"))
                        {
                            DropDownList drpCols = (DropDownList)itemEquipment.FindControl("drpCols");
                            CheckBox drpName = (CheckBox)itemEquipment.FindControl("drpName");
                            if (drpName.Checked && !drpCols.SelectedValue.Equals("-1"))
                            {
                                drp.Rows.Add(drpName.Attributes["Value"], drpCols.SelectedValue);

                            }
                            else if (drpName.Checked && drpCols.SelectedValue.Equals("-1"))
                            {
                                HtmlTableCell settingDrpName = (HtmlTableCell)itemEquipment.FindControl("settingDrpName");
                                err += settingDrpName.InnerText.ToString() + " ,";
                            }
                        }

                    }

                    if (err.Equals(""))
                    {
                        int success = obj.addStyle(cat, field, drp, txtTitle.Text.Replace("'", "''").Trim(), mrp.Text.Trim(), image1, image2, image3, image4, image5, image6);
                        if (success.Equals(0))
                        {
                            Session["addStyleSuccFail"] = "Style Added Successfully";
                        }
                        else
                        {
                            Session["addStyleSuccFail"] = "Style Add Failed";
                        }
                        Response.Redirect("ItemStyleSearchAdd.aspx", true);
                    }
                    else
                    {
                        divError.InnerHtml = "Please Add/Select " + err.TrimEnd(',');
                        divError.Visible = true;
                    }
                }
                else
                {


                    DataTable field = new DataTable();
                    field.Columns.Add("Column");
                    field.Columns.Add("Search");

                    foreach (RepeaterItem itemEquipment in rpt_DataField.Items)
                    {
                        Label IsAssignedDataField = (Label)itemEquipment.FindControl("IsAssignedDataField");

                        if (IsAssignedDataField.Text.Equals("True"))
                        {
                            TextBox controlSearch = (TextBox)itemEquipment.FindControl("controlSearch");
                            CheckBox controlName = (CheckBox)itemEquipment.FindControl("controlName");
                            field.Rows.Add(controlName.Attributes["Value"], controlSearch.Text.Replace("'", "''").Trim());
                            if (controlName.Checked && controlSearch.Text.Equals(""))
                            {
                                HtmlTableCell settingFieldName = (HtmlTableCell)itemEquipment.FindControl("settingFieldName");
                                err1 += settingFieldName.InnerText.ToString() + " ,";
                            }
                        }

                    }



                    DataTable drp = new DataTable();
                    drp.Columns.Add("Column");
                    drp.Columns.Add("Search");
                    foreach (RepeaterItem itemEquipment in rpt_dropdown.Items)
                    {
                        Label IsAssigneddrp = (Label)itemEquipment.FindControl("IsAssigneddrp");

                        if (IsAssigneddrp.Text.Equals("True"))
                        {
                            DropDownList drpCols = (DropDownList)itemEquipment.FindControl("drpCols");
                            CheckBox drpName = (CheckBox)itemEquipment.FindControl("drpName");
                            drp.Rows.Add(drpName.Attributes["Value"], drpCols.SelectedValue);
                            if (drpName.Checked && drpCols.SelectedValue.Equals("-1"))
                            {
                                HtmlTableCell settingDrpName = (HtmlTableCell)itemEquipment.FindControl("settingDrpName");
                                err1 += settingDrpName.InnerText.ToString() + " ,";
                            }
                        }

                    }
                    if (err1.Equals(""))
                    {
                        DataTable imageDel = new DataTable();
                        imageDel.Columns.Add("dbImageCol");
                        if (image1DisplayDel.Checked)
                        {
                            imageDel.Rows.Add("image1");
                        }
                        if (image2DisplayDel.Checked)
                        {
                            imageDel.Rows.Add("image2");
                        }
                        if (image3DisplayDel.Checked)
                        {
                            imageDel.Rows.Add("image3");
                        }
                        if (image4DisplayDel.Checked)
                        {
                            imageDel.Rows.Add("image4");
                        }
                        if (image5DisplayDel.Checked)
                        {
                            imageDel.Rows.Add("image5");
                        }
                        if (image6DisplayDel.Checked)
                        {
                            imageDel.Rows.Add("image6");
                        }
                        int success = obj.editStyle(cat, field, drp, txtTitle.Text.Replace("'", "''").Trim(), mrp.Text.Trim(), StyleID.Text, image1, image2, image3, image4, image5, image6, imageDel);
                        if (success.Equals(0))
                        {
                            Session["addStyleSuccFail"] = "Style Updated Successfully";
                        }
                        else
                        {
                            Session["addStyleSuccFail"] = "Style Update Failed";
                        }
                        Response.Redirect("ItemStyleSearchAdd.aspx", true);
                    }
                    else
                    {
                        divError.InnerHtml = "Please Add/Select " + err1.TrimEnd(',');
                        divError.Visible = true;
                    }

                }
            }
     
        }
        catch (Exception ex)
        {

            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void cancelStyle_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("ItemStyleSearchAdd.aspx", true);
        }
        catch (Exception ex)
        {

            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void selectDeselect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            bool checks = selectDeselect.Checked;
            foreach (RepeaterItem itemEquipment in rpt_DataField.Items)
            {
                CheckBox checkId = (CheckBox)itemEquipment.FindControl("controlName");
                checkId.Checked = checks;
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void drpSelect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            bool checks = drpSelect.Checked;
            foreach (RepeaterItem itemEquipment in rpt_dropdown.Items)
            {
                CheckBox checkId = (CheckBox)itemEquipment.FindControl("drpName");
                checkId.Checked = checks;
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}