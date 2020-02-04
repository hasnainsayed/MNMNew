using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["login"] == null)
            {
                Response.Redirect("login.aspx",true);
            }
            else
            {
                userNameOne.Text = Session["userName"].ToString();
                userNameTwo.Text = Session["userName"].ToString();
                if(!Session["uImage"].ToString().Equals(""))
                {
                    image1Display.Visible = true;
                    image1Display.ImageUrl = "http://finetouchimages.dzvdesk.com/Uploads/" + Session["uImage"].ToString();
                    image2Display.Visible = true;
                    image2Display.ImageUrl = "http://finetouchimages.dzvdesk.com/Uploads/" + Session["uImage"].ToString();
                }
                else
                {
                    image1DisplayStatic.Visible = true;
                    image2DisplayStatic.Visible = true;
                }
                if(Session["uType"].ToString().Equals("1")) // warehouse
                {
                    warehouseLoginSession.Visible = true;
                    salesChannelLoginSession.Visible = false;
                }else if(Session["uType"].ToString().Equals("2")) // saleschannel
                {
                    warehouseLoginSession.Visible = false;
                    salesChannelLoginSession.Visible = true;
                }
                BindCategorySetting();
                BindColumnSetting();
                BindMenu();
                checkAccess();
                
            }
        }
    }

    public void checkAccess()
    {
        try
        {
            //string abc = this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5);
            string fileName = Path.GetFileName(Request.PhysicalPath);
            fileName = fileName.Substring(0, fileName.Length-5);
            if(!fileName.Equals("Default") && !fileName.Equals("accessDenied"))
            {
                if(fileName.Equals("dump"))
                {
                    fileName = "dumps";
                }
                else if (fileName.Equals("rolemaster"))
                {
                    fileName = "rolemasters";
                }
                else if (fileName.Equals("physicalLoc"))
                {
                    fileName = "PhysicalLocation";
                }
                utilityCls obj = new utilityCls();
                DataTable dt = obj.getTableColwithID("roles", "roleId", Session["userrole"].ToString(), fileName);
                if(!dt.Rows.Count.Equals(0))
                {
                    if (dt.Rows[0][fileName].Equals("False"))
                    {
                        Response.Redirect("accessDenied.aspx", true);

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

    public void BindCategorySetting()
    {
        DataBase.StyleCategory ObjBind = new DataBase.StyleCategory();

        DataSet ds = ObjBind.BindCatSetting();

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            bool Chk = bool.Parse(ds.Tables[0].Rows[i]["IsAssigned"].ToString());

            if (Chk == true)
            {
                string ID = ds.Tables[0].Rows[i]["ICSettingID"].ToString();

                if (ID == "1")
                {
                    liCat2.Visible = true;
                    lblCat2.Text= ds.Tables[0].Rows[i]["SettingName"].ToString();
                }
                else if (ID == "2")
                {
                    liCat3.Visible = true;
                    lblCat3.Text = ds.Tables[0].Rows[i]["SettingName"].ToString();
                }
                else if (ID == "3")
                {
                    liCat4.Visible = true;
                    lblCat4.Text = ds.Tables[0].Rows[i]["SettingName"].ToString();
                }
                else if(ID == "4")
                {
                    liCat5.Visible = true;
                    lblCat5.Text = ds.Tables[0].Rows[i]["SettingName"].ToString();
                }
            }
        }

        ds.Dispose();
        ObjBind = null;
    }

    public void BindColumnSetting()
    {
        DataBase.StyleColumnTable ObjBind = new DataBase.StyleColumnTable();

        DataSet ds = ObjBind.BindColSetting();

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            bool Chk = bool.Parse(ds.Tables[0].Rows[i]["IsAssigned"].ToString());

            if (Chk == true)
            {
                string ID = ds.Tables[0].Rows[i]["CTSettingID"].ToString();

                if (ID == "1")
                {
                    liCol1.Visible = true;
                    lblCol1.Text = ds.Tables[0].Rows[i]["SettingName"].ToString();
                }
                else if (ID == "2")
                {
                    liCol2.Visible = true;
                    lblCol2.Text = ds.Tables[0].Rows[i]["SettingName"].ToString();
                }
                else if (ID == "3")
                {
                    liCol3.Visible = true;
                    lblCol3.Text = ds.Tables[0].Rows[i]["SettingName"].ToString();
                }
                else if (ID == "4")
                {
                    liCol4.Visible = true;
                    lblCol4.Text = ds.Tables[0].Rows[i]["SettingName"].ToString();
                }

                else if (ID == "5")
                {
                    liCol5.Visible = true;
                    lblCol5.Text = ds.Tables[0].Rows[i]["SettingName"].ToString();
                }
                else if (ID == "6")
                {
                    liCol6.Visible = true;
                    lblCol6.Text = ds.Tables[0].Rows[i]["SettingName"].ToString();
                }
                else if (ID == "7")
                {
                    liCol7.Visible = true;
                    lblCol7.Text = ds.Tables[0].Rows[i]["SettingName"].ToString();
                }
                else if (ID == "8")
                {
                    liCol8.Visible = true;
                    lblCol8.Text = ds.Tables[0].Rows[i]["SettingName"].ToString();
                }

                else if (ID == "9")
                {
                    liCol9.Visible = true;
                    lblCol9.Text = ds.Tables[0].Rows[i]["SettingName"].ToString();
                }
                else if (ID == "10")
                {
                    liCol10.Visible = true;
                    lblCol10.Text = ds.Tables[0].Rows[i]["SettingName"].ToString();
                }
                else if (ID == "11")
                {
                    liCol11.Visible = true;
                    lblCol11.Text = ds.Tables[0].Rows[i]["SettingName"].ToString();
                }
                else if (ID == "12")
                {
                    liCol12.Visible = true;
                    lblCol12.Text = ds.Tables[0].Rows[i]["SettingName"].ToString();
                }
                else if (ID == "13")
                {
                    liCol13.Visible = true;
                    lblCol13.Text = ds.Tables[0].Rows[i]["SettingName"].ToString();
                }
                else if (ID == "14")
                {
                    liCol14.Visible = true;
                    lblCol14.Text = ds.Tables[0].Rows[i]["SettingName"].ToString();
                }
                else if (ID == "15")
                {
                    liCol15.Visible = true;
                    lblCol15.Text = ds.Tables[0].Rows[i]["SettingName"].ToString();
                }
                else if (ID == "16")
                {
                    liCol16.Visible = true;
                    lblCol16.Text = ds.Tables[0].Rows[i]["SettingName"].ToString();
                }
                else if (ID == "17")
                {
                    liCol17.Visible = true;
                    lblCol17.Text = ds.Tables[0].Rows[i]["SettingName"].ToString();
                }
                else if (ID == "18")
                {
                    liCol18.Visible = true;
                    lblCol18.Text = ds.Tables[0].Rows[i]["SettingName"].ToString();
                }
                else if (ID == "19")
                {
                    liCol19.Visible = true;
                    lblCol19.Text = ds.Tables[0].Rows[i]["SettingName"].ToString();
                }
                else if (ID == "20")
                {
                    liCol20.Visible = true;
                    lblCol20.Text = ds.Tables[0].Rows[i]["SettingName"].ToString();
                }
            }
        }

        ds.Dispose();
        ObjBind = null;
    }

    public void BindMenu()
    {
        #region Active Class Menu
        String activepage = Request.RawUrl;

        #region Dashboard

        if (activepage.Contains("Default.aspx"))
        {
            Dashboard.Attributes.Add("Class", "active");
        }

        #endregion

        #region Masters

        else if (activepage.Contains("LocationType.aspx"))
        {
            liMasters.Attributes.Add("Class", "menu-open treeview active");
            liLocationType.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("LocationType2.aspx"))
        {
            liMasters.Attributes.Add("Class", "menu-open treeview active");
            liLocationType2.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("PhysicalLocation.aspx"))
        {
            liMasters.Attributes.Add("Class", "menu-open treeview active");
            liPLocation.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("VirtualLocation.aspx"))
        {
            liMasters.Attributes.Add("Class", "menu-open treeview active");
            liVLocation.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("VirtualLocationSetting.aspx"))
        {
            liMasters.Attributes.Add("Class", "menu-open treeview active");
            liVLocation.Attributes.Add("Class", "active");
        }

        //else if (activepage.Contains("SubLocation.aspx"))
        //{
        //    liMasters.Attributes.Add("Class", "active");
        //    liSLocation.Attributes.Add("Class", "active");
        //}

        //else if (activepage.Contains("Rack.aspx"))
        //{
        //    liMasters.Attributes.Add("Class", "active");
        //    liRack.Attributes.Add("Class", "active");
        //}

        //else if (activepage.Contains("Stack.aspx"))
        //{
        //    liMasters.Attributes.Add("Class", "active");
        //    liStack.Attributes.Add("Class", "active");
        //}

        else if (activepage.Contains("SubLocation.aspx"))
        {
            liMasters.Attributes.Add("Class", "menu-open treeview active");
            liPLocation.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("Rack.aspx"))
        {
            liMasters.Attributes.Add("Class", "menu-open treeview active");
            liPLocation.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("Stack.aspx"))
        {
            liMasters.Attributes.Add("Class", "menu-open treeview active");
            liPLocation.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("Vendors.aspx"))
        {
            liMasters.Attributes.Add("Class", "menu-open treeview active");
            liVendors.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("Lot.aspx"))
        {
            liMasters.Attributes.Add("Class", "menu-open treeview active");
            liLot.Attributes.Add("Class", "active");
        }
        else if (activepage.Contains("newLot.aspx"))
        {
            liMasters.Attributes.Add("Class", "menu-open treeview active");
            liNewLot.Attributes.Add("Class", "active");
        }
        else if (activepage.Contains("hsnCode.aspx"))
        {
            liMasters.Attributes.Add("Class", "menu-open treeview active");
            liHsn.Attributes.Add("Class", "active");
        }
        else if (activepage.Contains("courier.aspx"))
        {
            liMasters.Attributes.Add("Class", "menu-open treeview active");
            liCourier.Attributes.Add("Class", "active");
        }
        #endregion

        #region Style Category

        else if (activepage.Contains("ItemCategory.aspx"))
        {

            liStyleCategory.Attributes.Add("Class", "menu-open treeview active");
            liItemCat.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("Size.aspx"))
        {
            liStyleCategory.Attributes.Add("Class", "menu-open treeview active");
            liSize.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("Category2.aspx"))
        {
            liStyleCategory.Attributes.Add("Class", "menu-open treeview active");
            liCat2.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("Category3.aspx"))
        {
            liStyleCategory.Attributes.Add("Class", "menu-open treeview active");
            liCat3.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("Category4.aspx"))
        {
            liStyleCategory.Attributes.Add("Class", "menu-open treeview active");
            liCat4.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("Category5.aspx"))
        {
            liStyleCategory.Attributes.Add("Class", "menu-open treeview active");
            liCat5.Attributes.Add("Class", "active");
        }

        //else if (activepage.Contains("ColumnTable5.aspx"))
        //{
        //    liStyleCategory.Attributes.Add("Class", "active");
        //    liColumnTable5.Attributes.Add("Class", "active");
        //}

        #endregion

        #region Style Column

        else if (activepage.Contains("Column1.aspx"))
        {
            liColTable.Attributes.Add("Class", "menu-open treeview active");
            liCol1.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("Column2.aspx"))
        {
            liColTable.Attributes.Add("Class", "menu-open treeview active");
            liCol2.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("Column3.aspx"))
        {
            liColTable.Attributes.Add("Class", "menu-open treeview active");
            liCol3.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("Column4.aspx"))
        {
            liColTable.Attributes.Add("Class", "menu-open treeview active");
            liCol4.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("Column5.aspx"))
        {
            liColTable.Attributes.Add("Class", "menu-open treeview active");
            liCol5.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("Column6.aspx"))
        {
            liColTable.Attributes.Add("Class", "menu-open treeview active");
            liCol6.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("Column7.aspx"))
        {
            liColTable.Attributes.Add("Class", "menu-open treeview active");
            liCol7.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("Column8.aspx"))
        {
            liColTable.Attributes.Add("Class", "menu-open treeview active");
            liCol8.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("Column9.aspx"))
        {
            liColTable.Attributes.Add("Class", "menu-open treeview active");
            liCol9.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("Column10.aspx"))
        {
            liColTable.Attributes.Add("Class", "menu-open treeview active");
            liCol10.Attributes.Add("Class", "active");
        }
        else if (activepage.Contains("Column11.aspx"))
        {
            liColTable.Attributes.Add("Class", "menu-open treeview active");
            liCol11.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("Column12.aspx"))
        {
            liColTable.Attributes.Add("Class", "menu-open treeview active");
            liCol12.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("Column13.aspx"))
        {
            liColTable.Attributes.Add("Class", "menu-open treeview active");
            liCol13.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("Column14.aspx"))
        {
            liColTable.Attributes.Add("Class", "menu-open treeview active");
            liCol14.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("Column15.aspx"))
        {
            liColTable.Attributes.Add("Class", "menu-open treeview active");
            liCol15.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("Column16.aspx"))
        {
            liColTable.Attributes.Add("Class", "menu-open treeview active");
            liCol16.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("Column17.aspx"))
        {
            liColTable.Attributes.Add("Class", "menu-open treeview active");
            liCol17.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("Column18.aspx"))
        {
            liColTable.Attributes.Add("Class", "menu-open treeview active");
            liCol18.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("Column19.aspx"))
        {
            liColTable.Attributes.Add("Class", "menu-open treeview active");
            liCol19.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("Column20.aspx"))
        {
            liColTable.Attributes.Add("Class", "menu-open treeview active");
            liCol20.Attributes.Add("Class", "active");
        }

        #endregion

        #region Settings

        else if (activepage.Contains("CategorySetting.aspx"))
        {
            liSettings.Attributes.Add("Class", "menu-open treeview active");
            liCTS.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("ColumnTableSetting.aspx"))
        {
            liSettings.Attributes.Add("Class", "menu-open treeview active");
            liColTS.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("StyleColumnSetting.aspx"))
        {
            liSettings.Attributes.Add("Class", "menu-open treeview active");
            liSCS.Attributes.Add("Class", "active");
        }

        #endregion

        #region Mix

       
        #endregion

        #region Orders

        else if (activepage.Contains("IssueBag.aspx"))
        {
            liOrders.Attributes.Add("Class", "menu-open treeview active");
            liIssueBag.Attributes.Add("Class", "active");
        }

        else if (activepage.Contains("LR.aspx"))
        {
            liOrders.Attributes.Add("Class", "menu-open treeview active");
            liLR.Attributes.Add("Class", "active");
        }
        #endregion

        #region Item Style

        if (activepage.Contains("AddItemStyle.aspx"))
        {
            liItemStyle.Attributes.Add("Class", "active");
        }

        if (activepage.Contains("AddItemStyle.aspx"))
        {
            liAIS.Attributes.Add("Class", "active");
        }

        if (activepage.Contains("ViewInventory.aspx"))
        {
            liViewInventory.Attributes.Add("Class", "active");
        }

        #endregion

        #region Stockup

        if (activepage.Contains("StockUp.aspx"))
        {
            liStockUp.Attributes.Add("Class", "active");
        }

        #endregion

        #endregion
    }

    protected void signOut_Click(object sender, EventArgs e)
    {
        try
        {
            Session.Abandon();

            Response.Redirect("Login.aspx");
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }
        
}
