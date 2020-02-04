using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;

public partial class ItemStyle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindItemStyle();
            BindItemCategoryCombo();
            if (Session["ItemCategoryID"] != null)
            {
                ddlItemcategory.Enabled = false;
                ddlItemcategory.SelectedValue = Session["ItemCategoryID"].ToString();
                hdnItemCatID.Value = Session["ItemCategoryID"].ToString();

                /* if (Session["ItemCategoryID"] != null)
                 {
                 }*/
                Session.Remove("ItemCategoryID");
            }
            else if(hdnItemCatID.Value !="")
            {
                ddlItemcategory.Enabled = false;
                ddlItemcategory.SelectedValue = hdnItemCatID.Value;  
            }
            else
            {
                Response.Redirect("StockUp.aspx");
            }

            BindVendorCombo();

            BindSetColumnTable();
            BindSetControl();

        }
    }

    public void BindItemStyle()
    {
        DataBase.Masters ObjBind = new DataBase.Masters();

        DataSet ds = ObjBind.BindVendor();

        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    GV.DataSource = ds;
        //    GV.DataBind();
        //}
        ds.Dispose();
        ObjBind = null;
    }

    public void BindItemCategoryCombo()
    {
        DataBase.StyleCategory ObjBind = new DataBase.StyleCategory();

        DataTable ds = ObjBind.BindItemCat();

        ddlItemcategory.DataSource = ds;
        ddlItemcategory.DataTextField = "ItemCategory";
        ddlItemcategory.DataValueField = "ItemCategoryID";
        ddlItemcategory.DataBind();
        ddlItemcategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Item Category", "0"));
    }

    public void BindVendorCombo()
    {
        DataBase.Masters ObjBind = new DataBase.Masters();

        DataSet ds = ObjBind.BindVendor();

        /* ddlVendor.DataSource = ds;
         ddlVendor.DataTextField = "VendorName";
         ddlVendor.DataValueField = "VendorID";
         ddlVendor.DataBind();
         ddlVendor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Vendor", "0"));*/
    }

    #region Bind Column Table Combo

    //public void BinCol1Combo()
    //{
    //    DataBase.Masters ObjBind = new DataBase.Masters();

    //    DataSet ds = ObjBind.BindVendor();

    //    ddlVendor.DataSource = ds;
    //    ddlVendor.DataTextField = "VendorName";
    //    ddlVendor.DataValueField = "Vendor"+"ID";
    //    ddlVendor.DataBind();
    //    ddlVendor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Vendor", "0"));
    //}

    public void BindSetColumnTable()
    {
        DataBase.StyleColumnTable ObjBind = new DataBase.StyleColumnTable();

        DataSet ds = ObjBind.BindAssignedColSetting();

        DataSet ds1 = new DataSet();

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            string ID = ds.Tables[0].Rows[i]["CTSettingID"].ToString();
            string SettingName = ds.Tables[0].Rows[i]["SettingName"].ToString();
            bool IsAssigned = bool.Parse(ds.Tables[0].Rows[i]["IsAssigned"].ToString());

            if (ID == "1" && IsAssigned == true)
            {
                lblcol1.Text = SettingName;
                col1.Visible = true;

                ds1 = ObjBind.BindCol1();

                ddlcol1.DataSource = ds1;
                ddlcol1.DataTextField = "C1Name";
                ddlcol1.DataValueField = "Col1ID";
                ddlcol1.DataBind();
                ddlcol1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select " + SettingName + "", "0"));
            }

            if (ID == "2" && IsAssigned == true)
            {
                lblcol2.Text = SettingName;
                col2.Visible = true;

                ds1 = ObjBind.BindCol2();

                ddlcol2.DataSource = ds1;
                ddlcol2.DataTextField = "C2Name";
                ddlcol2.DataValueField = "Col2ID";
                ddlcol2.DataBind();
                ddlcol2.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select " + SettingName + "", "0"));
            }

            if (ID == "3" && IsAssigned == true)
            {
                lblcol3.Text = SettingName;
                col3.Visible = true;

                ds1 = ObjBind.BindCol3();

                ddlcol3.DataSource = ds1;
                ddlcol3.DataTextField = "C3Name";
                ddlcol3.DataValueField = "Col3ID";
                ddlcol3.DataBind();
                ddlcol3.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select " + SettingName + "", "0"));
            }

            if (ID == "4" && IsAssigned == true)
            {
                lblcol4.Text = SettingName;
                col4.Visible = true;

                ds1 = ObjBind.BindCol4();

                ddlcol4.DataSource = ds1;
                ddlcol4.DataTextField = "C4Name";
                ddlcol4.DataValueField = "Col4ID";
                ddlcol4.DataBind();
                ddlcol4.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select " + SettingName + "", "0"));
            }

            if (ID == "5" && IsAssigned == true)
            {
                lblcol5.Text = SettingName;
                col5.Visible = true;

                ds1 = ObjBind.BindCol5();

                ddlcol5.DataSource = ds1;
                ddlcol5.DataTextField = "C5Name";
                ddlcol5.DataValueField = "Col5ID";
                ddlcol5.DataBind();
                ddlcol5.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select " + SettingName + "", "0"));
            }

            if (ID == "6" && IsAssigned == true)
            {
                lblcol6.Text = SettingName;
                col6.Visible = true;

                ds1 = ObjBind.BindCol6();

                ddlcol6.DataSource = ds1;
                ddlcol6.DataTextField = "C6Name";
                ddlcol6.DataValueField = "Col6ID";
                ddlcol6.DataBind();
                ddlcol6.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select " + SettingName + "", "0"));
            }

            if (ID == "7" && IsAssigned == true)
            {
                lblcol7.Text = SettingName;
                col7.Visible = true;

                ds1 = ObjBind.BindCol7();

                ddlcol7.DataSource = ds1;
                ddlcol7.DataTextField = "C7Name";
                ddlcol7.DataValueField = "Col7ID";
                ddlcol7.DataBind();
                ddlcol7.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select " + SettingName + "", "0"));
            }

            if (ID == "8" && IsAssigned == true)
            {
                lblcol8.Text = SettingName;
                col8.Visible = true;

                ds1 = ObjBind.BindCol8();

                ddlcol8.DataSource = ds1;
                ddlcol8.DataTextField = "C8Name";
                ddlcol8.DataValueField = "Col8ID";
                ddlcol8.DataBind();
                ddlcol8.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select " + SettingName + "", "0"));
            }

            if (ID == "9" && IsAssigned == true)
            {
                lblcol9.Text = SettingName;
                col9.Visible = true;

                ds1 = ObjBind.BindCol9();

                ddlcol9.DataSource = ds1;
                ddlcol9.DataTextField = "C9Name";
                ddlcol9.DataValueField = "Col9ID";
                ddlcol9.DataBind();
                ddlcol9.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select " + SettingName + "", "0"));
            }

            if (ID == "10" && IsAssigned == true)
            {
                lblcol10.Text = SettingName;
                col10.Visible = true;

                ds1 = ObjBind.BindCol10();

                ddlcol10.DataSource = ds1;
                ddlcol10.DataTextField = "C10Name";
                ddlcol10.DataValueField = "Col10ID";
                ddlcol10.DataBind();
                ddlcol10.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select " + SettingName + "", "0"));
            }
        }
    }

    #endregion

    public void BindSetControl()
    {
        DataBase.StyleColumnTable ObjBind = new DataBase.StyleColumnTable();

        DataSet ds = ObjBind.BindAssignedColControlSetting();

        DataSet ds1 = new DataSet();

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            string ID = ds.Tables[0].Rows[i]["StyleCSID"].ToString();
            string SettingName = ds.Tables[0].Rows[i]["SettingName"].ToString();

            if (ID == "1")
            {
                lblctrl1.Text = SettingName;
                ctrl1.Visible = true;
            }

            if (ID == "2")
            {
                lblctrl2.Text = SettingName;
                ctrl2.Visible = true;
            }

            if (ID == "3")
            {
                lblctrl3.Text = SettingName;
                ctrl3.Visible = true;
            }

            if (ID == "4")
            {
                lblctrl4.Text = SettingName;
                ctrl4.Visible = true;
            }

            if (ID == "5")
            {
                lblctrl5.Text = SettingName;
                ctrl5.Visible = true;
            }

            if (ID == "6")
            {
                lblctrl6.Text = SettingName;
                ctrl6.Visible = true;
            }

            if (ID == "7")
            {
                lblctrl7.Text = SettingName;
                ctrl7.Visible = true;
            }

            if (ID == "8")
            {
                lblctrl8.Text = SettingName;
                ctrl8.Visible = true;
            }

            if (ID == "9")
            {
                lblctrl9.Text = SettingName;
                ctrl9.Visible = true;
            }

            if (ID == "10")
            {
                lblctrl10.Text = SettingName;
                ctrl10.Visible = true;
            }

            if (ID == "11")
            {
                lblctrl11.Text = SettingName;
                ctrl11.Visible = true;
            }

            if (ID == "12")
            {
                lblctrl12.Text = SettingName;
                ctrl12.Visible = true;
            }

            if (ID == "13")
            {
                lblctrl13.Text = SettingName;
                ctrl13.Visible = true;
            }

            if (ID == "14")
            {
                lblctrl14.Text = SettingName;
                ctrl14.Visible = true;
            }

            if (ID == "15")
            {
                lblctrl15.Text = SettingName;
                ctrl15.Visible = true;
            }

            if (ID == "16")
            {
                lblctrl16.Text = SettingName;
                ctrl16.Visible = true;
            }

            if (ID == "17")
            {
                lblctrl17.Text = SettingName;
                ctrl17.Visible = true;
            }

            if (ID == "18")
            {
                lblctrl18.Text = SettingName;
                ctrl18.Visible = true;
            }

            if (ID == "19")
            {
                lblctrl19.Text = SettingName;
                ctrl19.Visible = true;
            }

            if (ID == "20")
            {
                lblctrl20.Text = SettingName;
                ctrl20.Visible = true;
            }
        }
    }

    public void Clear()
    {
        //txtContact.Text = string.Empty;
        //txtCity.Text = string.Empty;
        //txtEmail.Text = string.Empty;
    }

    protected void rptr_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        DataBase.Masters objEdit = new DataBase.Masters();

        if (e.CommandName.ToLower().Equals("edit"))
        {
            int ID = int.Parse(e.CommandArgument.ToString());
            hdnID.Value = e.CommandArgument.ToString();

            ctfrmDet.Visible = true;

            DataSet ds = objEdit.GetVendorByID(ID);


            //txtContact.Text = ds.Tables[0].Rows[0]["Contact"].ToString();
            //txtEmail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
            //txtCity.Text = ds.Tables[0].Rows[0]["City"].ToString();

            btnSave.Visible = false;
            btnUpdate.Visible = true;

        }
        objEdit = null;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ctfrmDet.Visible = true;
        dvItemCat.Visible = true;
        dvItemCol.Visible = true;
        btnSave.Visible = true;
        btnUpdate.Visible = false;
    }

    private SequenceType sequenceType = SequenceType.NumericToAlpha;

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataBase.ItemStyle objAdd = new DataBase.ItemStyle();

        #region Get Last Item Code

        string Stylecode = string.Empty;
        string LatestStylecode = string.Empty;

        DataBase.ItemStyle ObjGetLastStyleCode = new DataBase.ItemStyle();

        DataSet ds = ObjGetLastStyleCode.GetLastStyleCode();

       
        if (ds.Tables[0].Rows.Count > 0)
        {
            Stylecode = ds.Tables[0].Rows[0]["StyleCode"].ToString();
        }
        else
        {
            Stylecode = "0";
        }
       
        AlphaNumeric.RequiredLength = Convert.ToInt32("5");

        LatestStylecode = AlphaNumeric.NextKeyCode(Stylecode.Trim(), sequenceType);

        #endregion

        int ID = objAdd.AddItemStyle(txtTitle.Text, "", ddlItemcategory.SelectedValue, ddlCat2.SelectedValue, ddlCat3.SelectedValue, ddlCat4.SelectedValue, ddlCat5.SelectedValue, ddlcol1.SelectedValue, ddlcol2.SelectedValue, ddlcol3.SelectedValue, ddlcol4.SelectedValue, ddlcol5.SelectedValue, ddlcol6.SelectedValue, ddlcol7.SelectedValue, ddlcol8.SelectedValue, ddlcol9.SelectedValue, ddlcol10.SelectedValue, txtctrl1.Text.Trim(), txtctrl2.Text.Trim(), txtctrl3.Text.Trim(), txtctrl4.Text.Trim(), txtctrl5.Text.Trim(), txtctrl6.Text.Trim(), txtctrl7.Text.Trim(), txtctrl8.Text.Trim(), txtctrl9.Text.Trim(), txtctrl10.Text.Trim(), txtctrl11.Text.Trim(), txtctrl12.Text.Trim(), txtctrl13.Text.Trim(), txtctrl14.Text.Trim(), txtctrl15.Text.Trim(), txtctrl16.Text.Trim(), txtctrl17.Text.Trim(), txtctrl18.Text.Trim(), txtctrl19.Text.Trim(), txtctrl20.Text.Trim(), LatestStylecode);
        if (ID > 0)
        {
            Session["ItemStyleID"] = ID;

            //BindVendor();
            //Clear();
            // ctfrmDet.Visible = false;
            objAdd = null;
        }

        if(hdnItemCatID.Value !="")
        {
            Response.Redirect("StockUp.aspx");
        }
        else
        {
            Response.Redirect("ItemStyle.aspx");
        }
    }

    #region Class for Barcode generation

    public enum SequenceType
    {
        /// <summary>
        /// 00,01,...,09,0A,...0Z,10,11...,A0,A1,...,ZZ
        /// </summary>
        NumericToAlpha = 1,
        /// <summary>
        /// AA,AB,...,AZ,A0,...A9,BA,BB...ZZ,00,01,...99
        /// </summary>
        AlphaToNumeric = 2,
        /// <summary>
        /// A0,A1,...,A9,AA,...AZ,B0,B1...ZZ,00,01,...99
        /// </summary>
        AlphaNumeric = 3,
        /// <summary>
        /// 00,01,...99
        /// </summary>
        NumericOnly = 4,
        /// <summary>
        /// AA,AB,...,ZZ
        /// </summary>
        AlphaOnly = 5
    }

    public static class AlphaNumeric
    {
        private static byte[] aSCIIValues;
        private static int indexToCheck;

        private static int requiredLength = 2;
        public static int RequiredLength
        {
            get { return requiredLength; }
            set { requiredLength = value; }
        }

        public static string NextKeyCode(string KeyCode, SequenceType Sequence)
        {
            if (KeyCode.Length != requiredLength)
            {
                switch (Sequence)
                {
                    case SequenceType.NumericToAlpha:
                        return MakeCustomLengthString("0", requiredLength);
                    case SequenceType.AlphaToNumeric:
                        return MakeCustomLengthString("A", requiredLength);
                    case SequenceType.AlphaNumeric:
                        return "A" + MakeCustomLengthString("0", requiredLength - 1);
                    case SequenceType.NumericOnly:
                        return MakeCustomLengthString("0", requiredLength);
                    case SequenceType.AlphaOnly:
                        return MakeCustomLengthString("A", requiredLength);
                    default:
                        return "";
                }
            }

            //If reached to max
            switch (Sequence)
            {
                case SequenceType.NumericToAlpha:
                    if (KeyCode == MakeCustomLengthString("Z", requiredLength))
                        throw new OverflowException("Maximum number is reached");
                    break;
                case SequenceType.AlphaToNumeric:
                    if (KeyCode == MakeCustomLengthString("9", requiredLength))
                        throw new OverflowException("Maximum number is reached");
                    break;
                case SequenceType.AlphaNumeric:
                    if (KeyCode == MakeCustomLengthString("9", requiredLength))
                        throw new OverflowException("Maximum number is reached");
                    break;
                case SequenceType.NumericOnly:
                    if (KeyCode == MakeCustomLengthString("9", requiredLength))
                        throw new OverflowException("Maximum number is reached");
                    break;
                case SequenceType.AlphaOnly:
                    if (KeyCode == MakeCustomLengthString("Z", requiredLength))
                        throw new OverflowException("Maximum number is reached");
                    break;
                default:
                    break;
            }

            aSCIIValues = ASCIIEncoding.ASCII.GetBytes(KeyCode.ToUpper());

            indexToCheck = aSCIIValues.Length - 1;
            bool keepChecking = true;
            while (keepChecking)
            {
                aSCIIValues[indexToCheck] = next(aSCIIValues[indexToCheck], Sequence);
                if (aSCIIValues[indexToCheck] == SingleCharacterMaxValue(Sequence) && indexToCheck != 0)
                    indexToCheck--;
                else
                    keepChecking = false;
            }

            KeyCode = ASCIIEncoding.ASCII.GetString(aSCIIValues);
            return KeyCode;
        }

        private static byte next(int current, SequenceType sequence)
        {
            switch (sequence)
            {
                case SequenceType.NumericToAlpha:
                    if (current == 57)
                        current = 65;
                    else if (current == 90)
                        current = 48;
                    else
                        current++;
                    break;
                case SequenceType.AlphaToNumeric:
                    if (current == 90)
                        current = 48;
                    else if (current == 57)
                        current = 65;
                    else
                        current++;
                    break;
                case SequenceType.AlphaNumeric:
                    if (current == 57)
                        current = 65;
                    else if (current == 90)
                        current = 48;
                    else
                        current++;
                    break;
                case SequenceType.NumericOnly:
                    if (current == 57)
                        current = 48;
                    else
                        current++;
                    break;
                case SequenceType.AlphaOnly:
                    if (current == 90)
                        current = 65;
                    else
                        current++;
                    break;
                default:
                    break;
            }

            return Convert.ToByte(current);
        }

        private static string MakeCustomLengthString(string data, int length)
        {
            string result = "";
            for (int i = 1; i <= length; i++)
                result += data;

            return result;
        }

        private static int SingleCharacterMaxValue(SequenceType sequence)
        {
            int result = 0;
            switch (sequence)
            {
                case SequenceType.NumericToAlpha:
                    result = 48;
                    break;
                case SequenceType.AlphaToNumeric:
                    result = 65;
                    break;
                case SequenceType.AlphaNumeric:
                    result = 48;
                    break;
                case SequenceType.NumericOnly:
                    result = 48;
                    break;
                case SequenceType.AlphaOnly:
                    result = 65;
                    break;
                default:
                    break;
            }

            return result;
        }

        private static bool isAllNine()
        {
            bool result = true;
            for (int i = 0; i < aSCIIValues.Length; i++)
            {
                if (aSCIIValues[i] != 57)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

    }

    #endregion

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        DataBase.Masters objUpdate = new DataBase.Masters();

        //int Success = objUpdate.UpdateVendor("", txtContact.Text.Trim(), txtEmail.Text.Trim(), txtCity.Text.Trim(), hdnID.Value);

        //if (Success > 0)
        //{
        //    btnUpdate.Visible = false;
        //    btnSave.Visible = true;

        //    Clear();

        //    BindVendor();
        //    ctfrmDet.Visible = false;
        //Response.Redirect("AddBuyer.aspx");
        //}
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        btnSave.Text = "Save";
        ctfrmDet.Visible = false;
    }

    #region  On Change event

    protected void ddlItemcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCat2ByIcatIDCombo(ddlItemcategory.SelectedValue);
    }

    //protected void ddlCat1_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}

    protected void ddlCat2_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCat3ByCat2IDCombo(ddlCat2.SelectedValue);
    }

    protected void ddlCat3_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCat4ByCat3IDCombo(ddlCat3.SelectedValue);
    }

    protected void ddlCat4_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCat5ByCat4IDCombo(ddlCat4.SelectedValue);
    }

    #region Bind Dropdown

    public void BindCat2ByIcatIDCombo(string ICatID)
    {
        DataBase.StyleCategory ObjBind = new DataBase.StyleCategory();

        dvcat3.Visible = false;
        dvcat4.Visible = false;
        dvcat5.Visible = false;

        DataSet ds = ObjBind.BindCat2ByICatID(ICatID);

        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
        {
            string Settingname = ds.Tables[1].Rows[0]["SettingName"].ToString();
            lblCat2.Text = Settingname;

            ddlCat2.DataSource = ds.Tables[0];
            ddlCat2.DataTextField = "C2Name";
            ddlCat2.DataValueField = "Cat2ID";
            ddlCat2.DataBind();
            ddlCat2.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select " + Settingname, "0"));

            dvcat2.Visible = true;
        }
        else
        {
            dvcat2.Visible = false;
        }
    }

    public void BindCat3ByCat2IDCombo(string Cat2ID)
    {
        DataBase.StyleCategory ObjBind = new DataBase.StyleCategory();

        dvcat4.Visible = false;
        dvcat5.Visible = false;

        DataSet ds = ObjBind.BindCat3ByCat2ID(Cat2ID);

        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
        {
            string Settingname = ds.Tables[1].Rows[0]["SettingName"].ToString();
            lblCat3.Text = Settingname;

            ddlCat3.DataSource = ds.Tables[0];
            ddlCat3.DataTextField = "C3Name";
            ddlCat3.DataValueField = "Cat3ID";
            ddlCat3.DataBind();
            ddlCat3.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select " + Settingname, "0"));

            dvcat3.Visible = true;
        }
        else
        {
            dvcat3.Visible = false;

        }
    }

    public void BindCat4ByCat3IDCombo(string Cat3ID)
    {
        DataBase.StyleCategory ObjBind = new DataBase.StyleCategory();

        dvcat5.Visible = false;

        DataSet ds = ObjBind.BindCat4ByCat3ID(Cat3ID);

        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
        {
            string Settingname = ds.Tables[1].Rows[0]["SettingName"].ToString();
            lblCat4.Text = Settingname;

            ddlCat4.DataSource = ds.Tables[0];
            ddlCat4.DataTextField = "C4Name";
            ddlCat4.DataValueField = "Cat4ID";
            ddlCat4.DataBind();
            ddlCat4.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select " + Settingname, "0"));

            dvcat4.Visible = true;
        }
        else
        {
            dvcat4.Visible = false;

        }
    }

    public void BindCat5ByCat4IDCombo(string Cat4ID)
    {
        DataBase.StyleCategory ObjBind = new DataBase.StyleCategory();

        DataSet ds = ObjBind.BindCat5ByCat4ID(Cat4ID);

        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
        {
            string Settingname = ds.Tables[1].Rows[0]["SettingName"].ToString();
            lblCat5.Text = Settingname;

            ddlCat5.DataSource = ds.Tables[0];
            ddlCat5.DataTextField = "C5Name";
            ddlCat5.DataValueField = "Cat5ID";
            ddlCat5.DataBind();
            ddlCat5.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select " + Settingname, "0"));

            dvcat5.Visible = true;
        }
        else
        {
            dvcat5.Visible = false;
        }
    }

    #endregion

    #endregion


    protected void btnUpload_Click(object sender, EventArgs e)
    {
        DataBase.ItemStyle objAdd = new DataBase.ItemStyle();

        HttpFileCollection fileCollection = Request.Files;

        for (int i = 0; i < fileCollection.Count; i++)
        {
            HttpPostedFile uploadfile = fileCollection[i];

            string flName = Path.GetFileName(uploadfile.FileName);

            string GetImageExt1 = flName.Substring(flName.LastIndexOf(".") + 1).ToLower();

            string StyleID = Session["ItemStyleID"].ToString();

            string FileName = StyleID + "" + i + "" + "." + GetImageExt1;


            if (uploadfile.ContentLength > 0)
            {
                //string Path = Server.MapPath("~/UploadFiles/");
                //fluImage.SaveAs(Path + "\\" + FileName + "." + GetImageExt1);

                int ID = objAdd.AddItemImage(StyleID, FileName);

                //uploadfile.SaveAs(Server.MapPath("~/UploadFiles/") + FileName + "." + GetImageExt1);
                uploadfile.SaveAs(Server.MapPath("~/UploadFiles/") + FileName);
                lblMessage.Text += FileName + "Saved Successfully<br>";
            }
        }
    }
}