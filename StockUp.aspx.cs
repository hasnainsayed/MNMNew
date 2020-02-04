using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class StockUp : System.Web.UI.Page
{
    int ItemBoundCounter = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindItemCategoryCombo();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "firedtSearch();", true);
        }
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

    public void BindCatSettingCombo()
    {
        DataBase.StyleColumnTable ObjBind = new DataBase.StyleColumnTable();

        DataSet ds = ObjBind.BindAssignedColSetting();

        ddlColSettings.DataSource = ds;
        ddlColSettings.DataTextField = "SettingName";
        ddlColSettings.DataValueField = "TableName";
        ddlColSettings.DataBind();
        ddlColSettings.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
    }

    public void BindColumnTableCombo(string TableName, string SettingName)
    {
        DataBase.StyleColumnTable ObjBind = new DataBase.StyleColumnTable();
        DataSet ds = new DataSet();

        dvcat.Visible = true;

        string Name = string.Empty;
        string ID = string.Empty;

        if (TableName == "Column 1")
        {
            ds = ObjBind.BindCol1ByItemCatID(ddlItemcategory.SelectedValue);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCol.DataSource = ds;
                ddlCol.DataTextField = "C1Name";
                ddlCol.DataValueField = "Col1ID";


                Name = ds.Tables[0].Rows[0]["C1Name"].ToString();
                ID = ds.Tables[0].Rows[0]["Col1ID"].ToString();
            }
        }

        else if (TableName == "Column 2")
        {
            ds = ObjBind.BindCol2ByItemCatID(ddlItemcategory.SelectedValue);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCol.DataSource = ds;
                ddlCol.DataTextField = "C2Name";
                ddlCol.DataValueField = "Col2ID";


                Name = ds.Tables[0].Rows[0]["C2Name"].ToString();
                ID = ds.Tables[0].Rows[0]["Col2ID"].ToString();
            }
        }

        else if (TableName == "Column 3")
        {
            ds = ObjBind.BindCol3ByItemCatID(ddlItemcategory.SelectedValue);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCol.DataSource = ds;
                ddlCol.DataTextField = "C3Name";
                ddlCol.DataValueField = "Col3ID";

                Name = ds.Tables[0].Rows[0]["C3Name"].ToString();
                ID = ds.Tables[0].Rows[0]["Col3ID"].ToString();
            }
            else
            {

            }
        }

        else if (TableName == "Column 4")
        {
            ds = ObjBind.BindCol4ByItemCatID(ddlItemcategory.SelectedValue);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCol.DataSource = ds;
                ddlCol.DataTextField = "C4Name";
                ddlCol.DataValueField = "Col4ID";

                Name = ds.Tables[0].Rows[0]["C4Name"].ToString();
                ID = ds.Tables[0].Rows[0]["Col4ID"].ToString();
            }
        }

        else if (TableName == "Column 5")
        {
            ds = ObjBind.BindCol5ByItemCatID(ddlItemcategory.SelectedValue);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCol.DataSource = ds;
                ddlCol.DataTextField = "C5Name";
                ddlCol.DataValueField = "Col5ID";

                Name = ds.Tables[0].Rows[0]["C5Name"].ToString();
                ID = ds.Tables[0].Rows[0]["Col5ID"].ToString();
            }
        }

        else if (TableName == "Column 6")
        {
            ds = ObjBind.BindCol6ByItemCatID(ddlItemcategory.SelectedValue);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCol.DataSource = ds;
                ddlCol.DataTextField = "C6Name";
                ddlCol.DataValueField = "Col6ID";

                Name = ds.Tables[0].Rows[0]["C6Name"].ToString();
                ID = ds.Tables[0].Rows[0]["Col6ID"].ToString();
            }
        }

        else if (TableName == "Column 7")
        {
            ds = ObjBind.BindCol7ByItemCatID(ddlItemcategory.SelectedValue);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCol.DataSource = ds;
                ddlCol.DataTextField = "C7Name";
                ddlCol.DataValueField = "Col7ID";

                Name = ds.Tables[0].Rows[0]["C7Name"].ToString();
                ID = ds.Tables[0].Rows[0]["Col7ID"].ToString();
            }
        }

        else if (TableName == "Column 8")
        {
            ds = ObjBind.BindCol8ByItemCatID(ddlItemcategory.SelectedValue);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCol.DataSource = ds;
                ddlCol.DataTextField = "C8Name";
                ddlCol.DataValueField = "Col8ID";

                Name = ds.Tables[0].Rows[0]["C8Name"].ToString();
                ID = ds.Tables[0].Rows[0]["Col8ID"].ToString();
            }
        }

        else if (TableName == "Column 9")
        {
            ds = ObjBind.BindCol9ByItemCatID(ddlItemcategory.SelectedValue);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCol.DataSource = ds;
                ddlCol.DataTextField = "C9Name";
                ddlCol.DataValueField = "Col9ID";

                Name = ds.Tables[0].Rows[0]["C9Name"].ToString();
                ID = ds.Tables[0].Rows[0]["Col9ID"].ToString();
            }
        }

        else if (TableName == "Column 10")
        {
            ds = ObjBind.BindCol10ByItemCatID(ddlItemcategory.SelectedValue);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCol.DataSource = ds;
                ddlCol.DataTextField = "C10Name";
                ddlCol.DataValueField = "Col01ID";

                Name = ds.Tables[0].Rows[0]["C10Name"].ToString();
                ID = ds.Tables[0].Rows[0]["Col10ID"].ToString();
            }
        }

        //ds = ObjBind.BindCatTableBySetting(SettingName);

        lblCol.Text = SettingName;

        ddlCol.DataBind();
        ddlCol.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select " + SettingName + "", "0"));
    }

    public void BindItemStyleShowAll(string ItemCategoryID)
    {
        DataBase.ItemStyle ObjBind = new DataBase.ItemStyle();

        DataSet ds = ObjBind.BindItemStyleSearchAll(ItemCategoryID);
        
        Session["ds"] = ds;

        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[1].Rows.Count > 0)
            {
                int Counter = ds.Tables[1].Rows.Count;
                int lblCount = 0;

                if (Counter > lblCount)
                {
                    lblcol1.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col1.Visible = true;

                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol2.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col2.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol3.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col3.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol4.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col4.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol5.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col5.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol6.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col6.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol7.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col7.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol8.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col8.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol9.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col9.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol10.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col10.Visible = true;
                    lblCount++;
                }

            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                GV.DataSource = ds.Tables[0];
                GV.DataBind();
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                int Counter = ds.Tables[2].Rows.Count;
                int lblCount = 0;

                if (Counter > lblCount)
                {
                    lblcontrol1.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control1.Visible = true;

                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol2.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control2.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol3.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control3.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol4.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control4.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol5.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control5.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol6.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control6.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol7.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control7.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol8.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control8.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol9.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control9.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol10.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control10.Visible = true;
                    lblCount++;
                }

                if (Counter > lblCount)
                {
                    lblcontrol11.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control11.Visible = true;

                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol12.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control12.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol13.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control13.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol14.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control14.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol15.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control15.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol16.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control16.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol17.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control17.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol18.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control18.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol19.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control19.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol20.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control20.Visible = true;
                    lblCount++;
                }
            }
        }
       
        ds.Dispose();
        ObjBind = null;

        loader.Visible = false;
    }

    public void BindItemStyleSearchByFixedValue(string ItemCategoryID, string TableName1, string ColTableID )
    {
        DataBase.ItemStyle ObjBind = new DataBase.ItemStyle();
        DataSet ds = new DataSet();

        string TableName = TableName1;

        if (TableName == "Column 1")
        {
            ds = ObjBind.BindItemStyleSearchByCol1Table(ItemCategoryID, ColTableID);
        }
        else if (TableName == "Column 2")
        {
            ds = ObjBind.BindItemStyleSearchByCol2Table(ItemCategoryID, ColTableID);
        }
        else if (TableName == "Column 3")
        {
            ds = ObjBind.BindItemStyleSearchByCol3Table(ItemCategoryID, ColTableID);
        }
        else if (TableName == "Column 4")
        {
            ds = ObjBind.BindItemStyleSearchByCol4Table(ItemCategoryID, ColTableID);
        }
        else if (TableName == "Column 5")
        {
            ds = ObjBind.BindItemStyleSearchByCol5Table(ItemCategoryID, ColTableID);
        }
        else if (TableName == "Column 6")
        {
            ds = ObjBind.BindItemStyleSearchByCol6Table(ItemCategoryID, ColTableID);
        }
        else if (TableName == "Column 7")
        {
            ds = ObjBind.BindItemStyleSearchByCol7Table(ItemCategoryID, ColTableID);
        }
        else if (TableName == "Column 8")
        {
            ds = ObjBind.BindItemStyleSearchByCol8Table(ItemCategoryID, ColTableID);
        }
        else if (TableName == "Column 9")
        {
            ds = ObjBind.BindItemStyleSearchByCol9Table(ItemCategoryID, ColTableID);
        }
        else if (TableName == "Column 10")
        {
            ds = ObjBind.BindItemStyleSearchByCol10Table(ItemCategoryID, ColTableID);
        }

       // DataSet ds = ObjBind.BindItemStyleSearchAll(ItemCategoryID);

        Session["ds"] = ds;

        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[1].Rows.Count > 0)
            {
                int Counter = ds.Tables[1].Rows.Count;
                int lblCount = 0;

                if (Counter > lblCount)
                {
                    lblcol1.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col1.Visible = true;

                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol2.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col2.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol3.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col3.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol4.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col4.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol5.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col5.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol6.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col6.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol7.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col7.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol8.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col8.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol9.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col9.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol10.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col10.Visible = true;
                    lblCount++;
                }

            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                GV.DataSource = ds.Tables[0];
                GV.DataBind();
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                int Counter = ds.Tables[2].Rows.Count;
                int lblCount = 0;

                if (Counter > lblCount)
                {
                    lblcontrol1.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control1.Visible = true;

                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol2.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control2.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol3.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control3.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol4.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control4.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol5.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control5.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol6.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control6.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol7.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control7.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol8.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control8.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol9.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control9.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol10.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control10.Visible = true;
                    lblCount++;
                }

                if (Counter > lblCount)
                {
                    lblcontrol11.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control11.Visible = true;

                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol12.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control12.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol13.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control13.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol14.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control14.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol15.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control15.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol16.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control16.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol17.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control17.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol18.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control18.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol19.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control19.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol20.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control20.Visible = true;
                    lblCount++;
                }
            }
        }
        ds.Dispose();
        ObjBind = null;
    }

    public void BindItemStyleWildCardSearch(string ItemCategoryID, string SearchText)
    {
        DataBase.ItemStyle ObjBind = new DataBase.ItemStyle();

        DataSet ds = ObjBind.BindItemStyleWildCardSearch(ItemCategoryID, SearchText);

        Session["ds"] = ds;

        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[1].Rows.Count > 0)
            {
                int Counter = ds.Tables[1].Rows.Count;
                int lblCount = 0;

                if (Counter > lblCount)
                {
                    lblcol1.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col1.Visible = true;

                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol2.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col2.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol3.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col3.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol4.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col4.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol5.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col5.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol6.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col6.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol7.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col7.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol8.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col8.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol9.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col9.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcol10.Text = ds.Tables[1].Rows[lblCount]["SettingName"].ToString();
                    col10.Visible = true;
                    lblCount++;
                }

            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                GV.DataSource = ds.Tables[0];
                GV.DataBind();
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                int Counter = ds.Tables[2].Rows.Count;
                int lblCount = 0;

                if (Counter > lblCount)
                {
                    lblcontrol1.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control1.Visible = true;

                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol2.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control2.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol3.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control3.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol4.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control4.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol5.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control5.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol6.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control6.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol7.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control7.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol8.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control8.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol9.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control9.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol10.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control10.Visible = true;
                    lblCount++;
                }

                if (Counter > lblCount)
                {
                    lblcontrol11.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control11.Visible = true;

                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol12.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control12.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol13.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control13.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol14.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control14.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol15.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control15.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol16.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control16.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol17.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control17.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol18.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control18.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol19.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control19.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    lblcontrol20.Text = ds.Tables[2].Rows[lblCount]["SettingName"].ToString();
                    control20.Visible = true;
                    lblCount++;
                }
            }
        }
        ds.Dispose();
        ObjBind = null;
    }

    protected void rptr_ItemBound(object sender, RepeaterItemEventArgs e)
    {
        //ItemBoundCounter++;

        //if (ItemBoundCounter == 1)
        {
            HtmlTableCell icol1 = (HtmlTableCell)e.Item.FindControl("col1");
            HtmlTableCell icol2 = (HtmlTableCell)e.Item.FindControl("col2");
            HtmlTableCell icol3 = (HtmlTableCell)e.Item.FindControl("col3");
            HtmlTableCell icol4 = (HtmlTableCell)e.Item.FindControl("col4");
            HtmlTableCell icol5 = (HtmlTableCell)e.Item.FindControl("col5");
            HtmlTableCell icol6 = (HtmlTableCell)e.Item.FindControl("col6");
            HtmlTableCell icol7 = (HtmlTableCell)e.Item.FindControl("col7");
            HtmlTableCell icol8 = (HtmlTableCell)e.Item.FindControl("col8");
            HtmlTableCell icol9 = (HtmlTableCell)e.Item.FindControl("col9");
            HtmlTableCell icol10 = (HtmlTableCell)e.Item.FindControl("col10");

            HtmlTableCell icontrol1 = (HtmlTableCell)e.Item.FindControl("control1");
            HtmlTableCell icontrol2 = (HtmlTableCell)e.Item.FindControl("control2");
            HtmlTableCell icontrol3 = (HtmlTableCell)e.Item.FindControl("control3");
            HtmlTableCell icontrol4 = (HtmlTableCell)e.Item.FindControl("control4");
            HtmlTableCell icontrol5 = (HtmlTableCell)e.Item.FindControl("control5");
            HtmlTableCell icontrol6 = (HtmlTableCell)e.Item.FindControl("control6");
            HtmlTableCell icontrol7 = (HtmlTableCell)e.Item.FindControl("control7");
            HtmlTableCell icontrol8 = (HtmlTableCell)e.Item.FindControl("control8");
            HtmlTableCell icontrol9 = (HtmlTableCell)e.Item.FindControl("control9");
            HtmlTableCell icontrol10 = (HtmlTableCell)e.Item.FindControl("control10");

            HtmlTableCell icontrol11 = (HtmlTableCell)e.Item.FindControl("control11");
            HtmlTableCell icontrol12 = (HtmlTableCell)e.Item.FindControl("control12");
            HtmlTableCell icontrol13 = (HtmlTableCell)e.Item.FindControl("control13");
            HtmlTableCell icontrol14 = (HtmlTableCell)e.Item.FindControl("control14");
            HtmlTableCell icontrol15 = (HtmlTableCell)e.Item.FindControl("control15");
            HtmlTableCell icontrol16 = (HtmlTableCell)e.Item.FindControl("control16");
            HtmlTableCell icontrol17 = (HtmlTableCell)e.Item.FindControl("control17");
            HtmlTableCell icontrol18 = (HtmlTableCell)e.Item.FindControl("control18");
            HtmlTableCell icontrol19 = (HtmlTableCell)e.Item.FindControl("control19");
            HtmlTableCell icontrol20 = (HtmlTableCell)e.Item.FindControl("control20");

            DataSet ds = (DataSet)Session["ds"];

            if (ds.Tables[1].Rows.Count > 0)
            {
                int Counter = ds.Tables[1].Rows.Count;
                int lblCount = 0;

                if (Counter > lblCount)
                {
                    icol1.Visible = true;

                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icol2.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icol3.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icol4.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icol5.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icol6.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icol7.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icol8.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icol9.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icol10.Visible = true;
                    lblCount++;
                }

            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                int Counter = ds.Tables[2].Rows.Count;
                int lblCount = 0;

                if (Counter > lblCount)
                {
                    icontrol1.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol2.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol3.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol4.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol5.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol6.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol7.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol8.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol9.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol10.Visible = true;
                    lblCount++;
                }

                if (Counter > lblCount)
                {
                    icontrol11.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol12.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol13.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol14.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol15.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol16.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol17.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol18.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol19.Visible = true;
                    lblCount++;
                }
                if (Counter > lblCount)
                {
                    icontrol20.Visible = true;
                    lblCount++;
                }
            }
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        dvItemCat.Visible = true;
        loader.Visible = true;

        if (rbdShowall.Checked == true)
        {
            BindItemStyleShowAll(ddlItemcategory.SelectedValue);
        }
        if(rbdFixedvalue.Checked == true)
        {
            BindItemStyleSearchByFixedValue(ddlItemcategory.SelectedValue, ddlColSettings.SelectedValue, ddlCol.SelectedValue);
        }
        if(rbdwildcard.Checked ==true)
        {
            BindItemStyleWildCardSearch(ddlItemcategory.SelectedValue, txtSearch.Text);
        }
    }

    protected void rbdFixedvalue_CheckedChanged(object sender, EventArgs e)
    {
        rbdwildcard.Checked = false;
        rbdShowall.Checked = false;

        dvddl.Visible = true;
        dvtxt.Visible = false;

        BindCatSettingCombo();

        btnSearch.Visible = false;
    }

    protected void rbdwildcard_CheckedChanged(object sender, EventArgs e)
    {
        rbdFixedvalue.Checked = false;
        rbdShowall.Checked = false;

        dvtxt.Visible = true;
        dvddl.Visible = false;
        dvcat.Visible = false;

        btnSearch.Visible = true;
    }

    protected void rbdShowall_CheckedChanged(object sender, EventArgs e)
    {
        rbdFixedvalue.Checked = false;
        rbdwildcard.Checked = false;

        dvtxt.Visible = false;
        dvddl.Visible = false;
        dvcat.Visible = false;

        btnSearch.Visible = true;
    }

    public void Clear()
    {
        dvrbd1.Visible = true;
        dvrbd2.Visible = true;
        dvrbd3.Visible = true;

        btnAddStyle.Visible = true;

        rbdFixedvalue.Checked = false;
        rbdwildcard.Checked = false;
        rbdShowall.Checked = false;

        dvddl.Visible = false;
        dvcat.Visible = false;

        ddlColSettings.SelectedValue = "0";
        ddlCol.SelectedValue = "0";
    }

    protected void ddlItemcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        //dvrbd1.Visible = true;
        //dvrbd2.Visible = true;
        //dvrbd3.Visible = true;
        Clear();

        btnAddStyle.Visible = true;
        btnSearch.Visible = false;
    }

    protected void ddlColSettings_SelectedIndexChanged(object sender, EventArgs e)
    {
        dvcat.Visible = true;
        BindColumnTableCombo(ddlColSettings.SelectedValue, ddlColSettings.SelectedItem.Text.ToString());
    }

    protected void btnAddStyle_Click(object sender, EventArgs e)
    {
        Session["ItemCategoryID"] = ddlItemcategory.SelectedValue;

        Response.Redirect("ItemStyle.aspx");
    }

    protected void ddlCol_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCol.SelectedValue != "0")
        {
            btnSearch.Visible = true;
        }
        else
        {
            btnSearch.Visible = false;
        }
    }
}