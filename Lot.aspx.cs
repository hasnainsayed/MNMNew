using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;

public partial class Lot : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBag();
        }
    }

    public void BindBag()
    {
        DataBase.Masters ObjBind = new DataBase.Masters();

        DataSet ds = ObjBind.BindBag();

        if (ds.Tables[0].Rows.Count > 0)
        {
            GV.DataSource = ds;
            GV.DataBind();
        }
        ds.Dispose();
        ObjBind = null;
    }

    public void BindVendorCombo()
    {
        DataBase.Masters ObjBind = new DataBase.Masters();

        DataSet ds = ObjBind.BindVendor();

        ddlVendor.DataSource = ds;
        ddlVendor.DataTextField = "VendorName";
        ddlVendor.DataValueField = "VendorID";
        ddlVendor.DataBind();
        ddlVendor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Vendor", "0"));
    }

    protected void rptr_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        DataBase.Masters objEdit = new DataBase.Masters();

        if (e.CommandName.ToLower().Equals("edit"))
        {
            int ID = int.Parse(e.CommandArgument.ToString());
            hdnID.Value = e.CommandArgument.ToString();

            ctfrmDet.Visible = true;

            DataSet ds = objEdit.GetBagByID(ID);

            ddlVendor.SelectedValue = ds.Tables[0].Rows[0]["VendorID"].ToString();
            txtBags.Text = ds.Tables[0].Rows[0]["NoOfBag"].ToString();
            txtDescription.Text = ds.Tables[0].Rows[0]["BagDescription"].ToString();

            btnSave.Visible = false;
            btnUpdate.Visible = true;

        }
        objEdit = null;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ctfrmDet.Visible = true;
        btnSave.Visible = true;
        btnUpdate.Visible = false;

        BindVendorCombo();
    }

    private SequenceType sequenceType = SequenceType.NumericToAlpha;

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataBase.Masters objAdd = new DataBase.Masters();

        int BagID = objAdd.AddBag(ddlVendor.SelectedValue, txtBags.Text.Trim(), txtDescription.Text.Trim());

        if (BagID > 0)
        {
            int Count = int.Parse(txtBags.Text.Trim());
            int ID = 0;

            for (int i = 0; i < Count; i++)
            {
                #region Get Last Bag Code

                string BagCode = string.Empty;
                string LatestBagCode = string.Empty;

                DataBase.Masters ObjGetLastCode = new DataBase.Masters();

                DataSet ds = ObjGetLastCode.GetLastBagCode();


                if (ds.Tables[0].Rows.Count > 0)
                {
                    BagCode = ds.Tables[0].Rows[0]["BagCode"].ToString();
                }
                else
                {
                    BagCode = "0";
                }

                AlphaNumeric.RequiredLength = Convert.ToInt32("3");

                LatestBagCode = AlphaNumeric.NextKeyCode(BagCode.Trim(), sequenceType);

                #endregion

                ID = objAdd.AddBagTransaction(BagID, LatestBagCode);
            }
            if (ID > 0)
            {
                ddlVendor.SelectedValue = "0";
                txtBags.Text = "";
                txtDescription.Text = "";
                ctfrmDet.Visible = false;
                BindBag();
                objAdd = null;
            }
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

        int Success = objUpdate.UpdateBag(txtBags.Text.Trim(), hdnID.Value);

        if (Success > 0)
        {
            btnUpdate.Visible = false;
            btnSave.Visible = true;

            ddlVendor.SelectedValue = "0";
            txtBags.Text = "";
            txtDescription.Text = "";
            BindBag();
            ctfrmDet.Visible = false;
            //Response.Redirect("AddBuyer.aspx");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlVendor.SelectedValue = "0";
        txtBags.Text = "";
        txtDescription.Text = "";

        btnSave.Text = "Save";
        ctfrmDet.Visible = false;
    }
}