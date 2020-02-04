using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net;

public partial class addTicket : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                issueDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
               

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "firedtSearch();", true);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey1", "firedate();", true);
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void searchReturnItem_Click(object sender, EventArgs e)
    {
        try
        {
            ticketCls obj = new ticketCls();
            string search = searchBy.Text;
            string searchFields = searchField.Text;
            DataTable searchDt = new DataTable();
            if (search.Equals("1")) // by barcode
            {
                searchDt = obj.returnByBarcodeSales(searchFields, "BarcodeNo");
            }
            else // by salesid
            {
                searchDt = obj.returnByBarcodeSales(searchFields, "salesidgivenbyvloc");
            }
            if (searchDt.Rows.Count.Equals(0))
            {
                noData.Visible = true;
                showItem.Visible = false;
                markReturn.Visible = false;
            }
            else
            {
                rptShowItem.DataSource = searchDt;
                rptShowItem.DataBind();
                showItem.Visible = true;
                noData.Visible = false;
                searchDisplay.Text = searchField.Text;
                mainDiv.Visible = false;
                markReturn.Visible = false;
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void addNewTicket_Click(object sender, EventArgs e)
    {
        try
        {
            displayStockupID.Text = string.Empty;
            displaysalesid.Text = string.Empty;
            
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));

            Label salesid = (Label)rp1.FindControl("salesid");

            ticketCls obj = new ticketCls();
            DataTable dt = obj.getReturnImages(salesid.Text);

            Label StockupID = (Label)rp1.FindControl("StockupID");
            Label Barcode = (Label)rp1.FindControl("Barcode");
            Label ItemCategory = (Label)rp1.FindControl("ItemCategory");

            displayBarcode.Text = Barcode.Text;
            displayCategory.Text = ItemCategory.Text;
            displayStockupID.Text = StockupID.Text;
            displaysalesid.Text = salesid.Text;

            Label SsalesAbwno = (Label)rp1.FindControl("salesAbwno");
            DsalesAbwno.Text = SsalesAbwno.Text;

            Label Ssalesidgivenbyvloc = (Label)rp1.FindControl("salesidgivenbyvloc");
            Dsalesidgivenbyvloc.Text = Ssalesidgivenbyvloc.Text;

            Label ScourierName = (Label)rp1.FindControl("courierName");
            DcourierName.Text = ScourierName.Text;

            Label SsalesDateTime = (Label)rp1.FindControl("salesDateTime");
            DsalesDateTime.Text = SsalesDateTime.Text;

            Label SsoldBy = (Label)rp1.FindControl("soldBy");
            DsoldBy.Text = SsoldBy.Text;

            Label Sdispatchtimestamp = (Label)rp1.FindControl("dispatchtimestamp");
            Ddispatchtimestamp.Text = Sdispatchtimestamp.Text;

            Label SdispatchedBy = (Label)rp1.FindControl("dispatchedBy");
            DdispatchedBy.Text = SdispatchedBy.Text;

            Label Scustname = (Label)rp1.FindControl("custname");
            Dcustname.Text = Scustname.Text;

            Label Sinvid = (Label)rp1.FindControl("invid");
            Dinvid.Text = Sinvid.Text;

            Label SpaymentMode = (Label)rp1.FindControl("paymentMode");
            DpaymentMode.Text = SpaymentMode.Text.ToUpper();

            Label SLocation = (Label)rp1.FindControl("Location");
            DLocation.Text = SLocation.Text;

            Label SC1Name = (Label)rp1.FindControl("C1Name");
            DC1Name.Text = SC1Name.Text;

            Label image1 = (Label)rp1.FindControl("image1");
            displayimage1Display.Visible = false;
            string imagelink = "http://finetouchimages.dzvdesk.com/Uploads/";
            if (!image1.Text.Equals(""))
            {
                displayimage1Display.Visible = true;
                displayimage1Display.ImageUrl = imagelink + image1.Text;
            }

            Label SreturnAbwno = (Label)rp1.FindControl("returnAbwno");
            DreturnAbwno.Text = SreturnAbwno.Text;

            Label SretCourier = (Label)rp1.FindControl("retCourier");
            DretCourier.Text = SretCourier.Text;

            Label Sreturntimestamp = (Label)rp1.FindControl("returntimestamp");
            Dreturntimestamp.Text = Sreturntimestamp.Text;

            Label SreturnedBy = (Label)rp1.FindControl("returnedBy");
            DreturnedBy.Text = SreturnedBy.Text;

            Label Sremarks = (Label)rp1.FindControl("remarks");
            Dremarks.Text = Sremarks.Text;

            if (!dt.Rows[0]["rImage1"].ToString().Equals(""))
            {
                rImage1.Visible = true;
                rImage1.ImageUrl = imagelink + dt.Rows[0]["rImage1"].ToString();
                rImage1Link.Text = imagelink + dt.Rows[0]["rImage1"].ToString();
            }
            

            if (!dt.Rows[0]["rImage2"].ToString().Equals(""))
            {
                rImage2.Visible = true;
                rImage2.ImageUrl = imagelink + dt.Rows[0]["rImage2"].ToString();
                rImage2Link.Text = imagelink + dt.Rows[0]["rImage2"].ToString();
            }
            

            if (!dt.Rows[0]["rImage3"].ToString().Equals(""))
            {
                rImage3.Visible = true;
                rImage3.ImageUrl = imagelink + dt.Rows[0]["rImage3"].ToString();
                rImage3Link.Text = imagelink + dt.Rows[0]["rImage3"].ToString();
            }
            

            if (!dt.Rows[0]["rImage4"].ToString().Equals(""))
            {
                rImage4.Visible = true;
                rImage4.ImageUrl = imagelink + dt.Rows[0]["rImage4"].ToString();
                rImage4Link.Text = imagelink + dt.Rows[0]["rImage4"].ToString();
            }
            

            if (!dt.Rows[0]["rImage5"].ToString().Equals(""))
            {
                rImage5.Visible = true;
                rImage5.ImageUrl = imagelink + dt.Rows[0]["rImage5"].ToString();
                rImage5Link.Text = imagelink + dt.Rows[0]["rImage5"].ToString();
            }
            

            if (!dt.Rows[0]["rImage6"].ToString().Equals(""))
            {
                rImage6.Visible = true;
                rImage6.ImageUrl = imagelink + dt.Rows[0]["rImage6"].ToString();
                rImage6Link.Text = imagelink + dt.Rows[0]["rImage6"].ToString();
            }
            

            if (!dt.Rows[0]["rImage7"].ToString().Equals(""))
            {
                rImage7.Visible = true;
                rImage7.ImageUrl = imagelink + dt.Rows[0]["rImage7"].ToString();
                rImage7Link.Text = imagelink + dt.Rows[0]["rImage7"].ToString();
            }
            

            if (!dt.Rows[0]["rImage8"].ToString().Equals(""))
            {
                rImage8.Visible = true;
                rImage8.ImageUrl = imagelink + dt.Rows[0]["rImage8"].ToString();
                rImage8Link.Text = imagelink + dt.Rows[0]["rImage8"].ToString();
            }
            

            if (!dt.Rows[0]["rImage9"].ToString().Equals(""))
            {
                rImage9.Visible = true;
                rImage9.ImageUrl = imagelink + dt.Rows[0]["rImage9"].ToString();
                rImage9Link.Text = imagelink + dt.Rows[0]["rImage9"].ToString();
            }
            

            if (!dt.Rows[0]["rImage10"].ToString().Equals(""))
            {
                rImage10.Visible = true;
                rImage10.ImageUrl = imagelink + dt.Rows[0]["rImage10"].ToString();
                rImage10Link.Text = imagelink + dt.Rows[0]["rImage10"].ToString();
            }
            

            if (!dt.Rows[0]["rVideo1"].ToString().Equals(""))
            {
                /*rVideo1.Visible = true;
                rVideo1.Attributes.Add("src", dt.Rows[0]["rVideo1"].ToString());*/
                //rVideo1Link.Text = imagelink + dt.Rows[0]["rVideo1"].ToString();
                rVideo1Link.Text = dt.Rows[0]["rVideo1"].ToString();
            }
            

            if (!dt.Rows[0]["rVideo2"].ToString().Equals(""))
            {
                /*rVideo2.Visible = true;
                rVideo2.Attributes.Add("src", dt.Rows[0]["rVideo2"].ToString());*/
                //rVideo2Link.Text = imagelink + dt.Rows[0]["rVideo2"].ToString();
                rVideo2Link.Text = dt.Rows[0]["rVideo2"].ToString();
            }
            

            showItem.Visible = false;
            markReturn.Visible = true;
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void btnAddTicket_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("addTicket.aspx",true);
            
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            divError.InnerHtml = string.Empty;
            divSucc.InnerHtml = string.Empty;
            divError.Visible = false;
            divSucc.Visible = false;
            string err = string.Empty;
            if(ticketNo.Text.Equals(""))
            {
                err += "Please Enter Ticket No.";
            }

            if(err.Equals(""))
            {
                ticketCls obj = new ticketCls();
                int success = obj.saveTicket(displaysalesid.Text,ticketNo.Text,issueDate.Text,description.Text);
                if (success.Equals(-1))
                {
                    Session["ticketSuccfail"] = "Ticket Generation RolledBack";

                }               
                else if (success.Equals(1))
                {
                    Session["ticketSuccfail"] = "Ticket Generated Successfully";
                }
                else
                {
                    Session["ticketSuccfail"] = "Some Error";
                }
                //divError.Visible = true;
                Response.Redirect("tickets.aspx", true);
            }
            else
            {
                divError.InnerHtml = err;
                divError.Visible = true;
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void rptShowItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            /*LinkButton btn = e.Item.FindControl("return") as LinkButton;
            ScriptManager.GetCurrent(this).RegisterPostBackControl(btn);*/
            Label image1 = e.Item.FindControl("image1") as Label;
            string imagelink = "http://finetouchimages.dzvdesk.com/Uploads/";
            if (!image1.Text.Equals(""))
            {
                Image image1Display = e.Item.FindControl("image1Display") as Image;
                image1Display.Visible = true;
                image1Display.ImageUrl = imagelink + image1.Text;
            }

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}