using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;

public partial class StockUp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //BindSize();
        }
    }

    #region Size

    public void BindSize()
    {
        DataBase.Masters ObjBind = new DataBase.Masters();

        DataSet ds = ObjBind.BindSize();

        if (ds.Tables[0].Rows.Count > 0)
        {
            GV.DataSource = ds;
            GV.DataBind();
        }
        ds.Dispose();
        ObjBind = null;
    }

    public void BindStyleCombo()
    {
        DataBase.ItemStyle ObjBind = new DataBase.ItemStyle();

        DataSet ds = ObjBind.GetStyleCombo();

        ddlStyle.DataSource = ds;
        ddlStyle.DataTextField = "Title";
        ddlStyle.DataValueField = "StyleID";
        ddlStyle.DataBind();
        ddlStyle.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Style", "0"));
    }

    public void BindLotCombo()
    {
        DataBase.Masters ObjBind = new DataBase.Masters();

        DataSet ds = ObjBind.BindBag();

        ddlLot.DataSource = ds;
        ddlLot.DataTextField = "BAGDETAILS";
        ddlLot.DataValueField = "BTID";
        ddlLot.DataBind();
        ddlLot.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Bag", "0"));
    }

    public void BindSizeCombo()
    {
        DataBase.Masters ObjBind = new DataBase.Masters();

        DataSet ds = ObjBind.BindSize();

        ddlSize.DataSource = ds;
        ddlSize.DataTextField = "Size1";
        ddlSize.DataValueField = "SizeID";
        ddlSize.DataBind();
        ddlSize.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Size", "0"));
    }

    public void Clear()
    {
        ddlStyle.SelectedValue = "0";
    }

    protected void rptr_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        DataBase.Masters objEdit = new DataBase.Masters();

        if (e.CommandName.ToLower().Equals("edit"))
        {
            BindStyleCombo();

            int ID = int.Parse(e.CommandArgument.ToString());
            hdnID.Value = e.CommandArgument.ToString();

            ctfrmDet.Visible = true;

            DataSet ds = objEdit.GetSizeByID(ID);

            ddlStyle.SelectedValue = ds.Tables[0].Rows[0]["ItemCategoryID"].ToString();

            btnSave.Visible = false;
            btnUpdate.Visible = true;
        }

        objEdit = null;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        BindStyleCombo();
        BindLotCombo();
        BindSizeCombo();
        ctfrmDet.Visible = true;
        btnSave.Visible = true;
        btnUpdate.Visible = false;
    }

    private SequenceType sequenceType = SequenceType.NumericToAlpha;

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataBase.StockUp objAdd = new DataBase.StockUp();

        #region  Barcode Generation

        string Stylecode = string.Empty;
        string SizeCode = string.Empty;
        string LastBarcode = string.Empty;

        string LatestBarcode = string.Empty;

        DataBase.StockUp ObjGetLastStyleCode = new DataBase.StockUp();

        DataSet ds = ObjGetLastStyleCode.GetLastBarCodeStyleCode(ddlStyle.SelectedValue,ddlSize.SelectedValue);

        if (ds.Tables[0].Rows.Count > 0)
        {
            LastBarcode = ds.Tables[0].Rows[0]["LastBarcode"].ToString();
        }
        else
        {
            LastBarcode = "0";
        }


        if (ds.Tables[1].Rows.Count > 0)
        {
            Stylecode = ds.Tables[1].Rows[0]["StyleCode"].ToString();
        }

        if (ds.Tables[2].Rows.Count > 0)
        {
            SizeCode = ds.Tables[2].Rows[0]["SizeCode"].ToString();
        }

        string Size = string.Empty;

        Size = ddlSize.SelectedValue;

        AlphaNumeric.RequiredLength = Convert.ToInt32("3");

        LatestBarcode = AlphaNumeric.NextKeyCode(LastBarcode.Trim(), sequenceType);

        string GeneratedBarcode = Stylecode + "/" + SizeCode + "/" + LatestBarcode;

        string PrintBarcode = Stylecode + "/" + Size + "/" + LatestBarcode;

        #endregion

        int ID = objAdd.AddStockUp(ddlStyle.SelectedValue, ddlLot.SelectedValue, ddlSize.SelectedValue, LatestBarcode, Stylecode, GeneratedBarcode);

        if (ID > 0)
        {
            Clear();
            ctfrmDet.Visible = false;
            BindSize();
            objAdd = null;
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

        //int Success = objUpdate.UpdateSize(ddlItemCategory.SelectedValue, txtSize1.Text.Trim(), txtSize2.Text.Trim(), txtSize3.Text.Trim(), txtSize4.Text.Trim(), hdnID.Value);

        //if (Success > 0)
        {
            btnUpdate.Visible = false;
            btnSave.Visible = true;

            Clear();

            BindSize();
            ctfrmDet.Visible = false;
            //Response.Redirect("AddBuyer.aspx");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Response.Redirect("Location.aspx");

        btnSave.Text = "Save";
        ctfrmDet.Visible = false;
    }

    #endregion
}