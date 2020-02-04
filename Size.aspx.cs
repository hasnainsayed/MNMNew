using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;

public partial class Size : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindSize();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "firedtSearch();", true);
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

    public void BindItemCategoryCombo()
    {
        DataBase.StyleCategory ObjBind = new DataBase.StyleCategory();

        DataTable ds = ObjBind.BindItemCat();
        ddlItemCategory.DataTextField = "ItemCategory";
        ddlItemCategory.DataValueField = "ItemCategoryID";
        ddlItemCategory.DataSource = ds;        
        ddlItemCategory.DataBind();
        ddlItemCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Item Category", "0"));
    }

    public void Clear()
    {
        txtSize1.Text = string.Empty;
        txtSize2.Text = string.Empty;
        txtSize3.Text = string.Empty;
        txtSize4.Text = string.Empty;
        //ddlItemCategory.SelectedValue = "0";
        rpt_Length.DataSource = new DataTable();
        rpt_Length.DataBind();
        ctfrmDet.Visible = false;
    }

    protected void rptr_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        DataBase.Masters objEdit = new DataBase.Masters();

        if (e.CommandName.ToLower().Equals("edit"))
        {
            BindItemCategoryCombo();
           
            int ID = int.Parse(e.CommandArgument.ToString());
            hdnID.Text = e.CommandArgument.ToString();

            ctfrmDet.Visible = true;

            DataSet ds = objEdit.GetSizeByID(ID);
            hdnID.Text = ID.ToString();

            txtSize1.Text = ds.Tables[0].Rows[0]["Size1"].ToString();
            txtSize2.Text = ds.Tables[0].Rows[0]["Size2"].ToString();
            txtSize3.Text = ds.Tables[0].Rows[0]["Size3"].ToString();
            txtSize4.Text = ds.Tables[0].Rows[0]["Size4"].ToString();
            ddlItemCategory.SelectedValue = ds.Tables[0].Rows[0]["ItemCategoryID"].ToString();

            styleCls sObj = new styleCls();
            DataTable st = sObj.getTablewithID("sizeLength","sizeId", ID);
            rpt_Length.DataSource = st;
            rpt_Length.DataBind();
           
            btnSave.Visible = false;
            btnUpdate.Visible = true;

        }
       
        objEdit = null;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Clear();
        BindItemCategoryCombo();
        bindLengthRpt();
        ctfrmDet.Visible = true;
        btnSave.Visible = true;
        btnUpdate.Visible = false;
        hdnID.Text = "0";

    }

    private void bindLengthRpt()
    {
        try
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("brandId"); dt1.Columns.Add("lengths"); dt1.Columns.Add("pId");
            foreach (RepeaterItem items in rpt_Length.Items)
            {
                DropDownList brandId = (DropDownList)items.FindControl("brandId");
                TextBox lengths = (TextBox)items.FindControl("lengths");
                TextBox pId = (TextBox)items.FindControl("pid");
                dt1.Rows.Add(brandId.SelectedValue, lengths.Text, pId.Text);
            }
            dt1.Rows.Add(-1,"",0);
            rpt_Length.DataSource = dt1;
            rpt_Length.DataBind();
        }
        catch(Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    private SequenceType sequenceType = SequenceType.NumericToAlpha;

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataBase.Masters objAdd = new DataBase.Masters();

        #region Get Last Size Code

        string SizeCode = string.Empty;
        string LatestSizeCode = string.Empty;

        DataBase.Masters ObjGetLastStyleCode = new DataBase.Masters();

        DataSet ds = ObjGetLastStyleCode.GetLastSizeCode();


        if (ds.Tables[0].Rows.Count > 0)
        {
            SizeCode = ds.Tables[0].Rows[0]["SizeCode"].ToString();
        }
        else
        {
            SizeCode = "0";
        }

        AlphaNumeric.RequiredLength = Convert.ToInt32("2");

        LatestSizeCode = AlphaNumeric.NextKeyCode(SizeCode.Trim(), sequenceType);

        #endregion

        DataTable dt = new DataTable();
        dt.Columns.Add("brandId"); dt.Columns.Add("lengths"); dt.Columns.Add("pId");
        foreach (RepeaterItem items in rpt_Length.Items)
        {
            DropDownList brandId = (DropDownList)items.FindControl("brandId");
            TextBox lengths = (TextBox)items.FindControl("lengths");
            TextBox pId = (TextBox)items.FindControl("pId");
            dt.Rows.Add(brandId.SelectedValue, lengths.Text, pId.Text);
        }

        sizeCls sobj = new sizeCls();
        int success = sobj.addEditSize(ddlItemCategory.SelectedValue, txtSize1.Text.Trim(), txtSize2.Text.Trim(), txtSize3.Text.Trim(), txtSize4.Text.Trim(), LatestSizeCode, hdnID.Text,dt);
        BindSize();
        Clear();
        /*int ID = objAdd.AddSize(ddlItemCategory.SelectedValue,txtSize1.Text.Trim(),txtSize2.Text.Trim(),txtSize3.Text.Trim(),txtSize4.Text.Trim(),LatestSizeCode);
        if (ID > 0)
        {
            Clear();
            ctfrmDet.Visible = false;
            BindSize();
            objAdd = null;
        }*/

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
        /*DataBase.Masters objUpdate = new DataBase.Masters();

        int Success = objUpdate.UpdateSize(ddlItemCategory.SelectedValue, txtSize1.Text.Trim(), txtSize2.Text.Trim(), txtSize3.Text.Trim(), txtSize4.Text.Trim(), hdnID.Value);

        if (Success > 0)
        {
            btnUpdate.Visible = false;
            btnSave.Visible = true;

            Clear();

            BindSize();
            ctfrmDet.Visible = false;
            //Response.Redirect("AddBuyer.aspx");
        }*/
        DataTable dt = new DataTable();
        dt.Columns.Add("brandId"); dt.Columns.Add("lengths"); dt.Columns.Add("pId");
        foreach (RepeaterItem items in rpt_Length.Items)
        {
            DropDownList brandId = (DropDownList)items.FindControl("brandId");
            TextBox lengths = (TextBox)items.FindControl("lengths");
            TextBox pId = (TextBox)items.FindControl("pId");
            dt.Rows.Add(brandId.SelectedValue, lengths.Text, pId.Text);
        }

        sizeCls sobj = new sizeCls();
        int success = sobj.addEditSize(ddlItemCategory.SelectedValue, txtSize1.Text.Trim(), txtSize2.Text.Trim(), txtSize3.Text.Trim(), txtSize4.Text.Trim(),string.Empty, hdnID.Text, dt);
        BindSize();
        Clear();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Response.Redirect("Location.aspx");

        btnSave.Text = "Save";
        ctfrmDet.Visible = false;
        rpt_Length.DataSource = new DataTable();
        rpt_Length.DataBind();
    }

    #endregion

    protected void rpt_Length_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            string brand = ((DataRowView)e.Item.DataItem)["brandId"].ToString();
            styleCls obj = new styleCls();
            DataTable dt = obj.getTable("Column1");
            DropDownList drp = (DropDownList)e.Item.FindControl("brandId");
            
            drp.DataSource = dt;
            drp.DataBind();
            drp.Items.Insert(0, new ListItem("---- Select ----", "-1"));
            drp.SelectedValue = brand;
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void addBrands_Click(object sender, EventArgs e)
    {
        try
        {
            bindLengthRpt();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}