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

public partial class returnItem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                courierCls cObj = new courierCls();
                DataTable courierDt = cObj.getCourier();
                courier.DataSource = courierDt;
                courier.DataBind();
                if (Session["returnSuccfail"] != null)
                {
                    if (Session["returnSuccfail"].ToString().Equals("Return Completed Successfully"))
                    {
                        divSucc.InnerText = Session["returnSuccfail"].ToString();
                        divSucc.Visible = true;
                        divError.Visible = false;
                        Session.Remove("returnSuccfail");
                    }
                    else
                    {
                        divError.InnerText = Session["returnSuccfail"].ToString();
                        divError.Visible = true;
                        divSucc.Visible = false;
                        Session.Remove("returnSuccfail");
                    }

                }
                bindLatestReturns();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "firedtSearch();", true);
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    public void bindLatestReturns()
    {
        try
        {
            returnItemCls obj = new returnItemCls();
            DataTable dt = new DataTable();
            dt = obj.getLatestReturns();
            rpt_Return.DataSource = dt;
            rpt_Return.DataBind();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void searchSoldItem_Click(object sender, EventArgs e)
    {
        try
        {
            returnItemCls obj = new returnItemCls();
            string search = searchBy.Text;
            string searchFields = searchField.Text;
            DataTable searchDt = new DataTable();
            if (search.Equals("1")) // by barcode
            {
                searchDt = obj.dispatchByBarcodeSales(searchFields, "BarcodeNo");
            }
            else if (search.Equals("2"))  // by salesid
            {
                searchDt = obj.dispatchByBarcodeSales(searchFields, "salesidgivenbyvloc");
            }
            else if (search.Equals("3"))
            {
                searchDt = obj.websiteSales(searchFields, "BarcodeNo");
            }else
            {
                searchDt = obj.websiteSales(searchFields, "salesidgivenbyvloc");
            }
            if (searchDt.Rows.Count.Equals(0))
            {
                noData.Visible = true;
                showItem.Visible = false;
                markReturn.Visible = false;
                latest20.Visible = false;
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
                latest20.Visible = false;
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void return_Click(object sender, EventArgs e)
    {
        try
        {
            displayStockupID.Text = string.Empty;
            displaysalesid.Text = string.Empty;
            displayArchiveStockupID.Text = string.Empty;

            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));

            Label salesid = (Label)rp1.FindControl("salesid");
            Label ArchiveStockupID = (Label)rp1.FindControl("ArchiveStockupID");
            Label StockupID = (Label)rp1.FindControl("StockupID");
            Label Barcode = (Label)rp1.FindControl("Barcode");
            displayBarcode.Text = Barcode.Text;

            Label ItemCategory = (Label)rp1.FindControl("ItemCategory");
            displayCategory.Text = ItemCategory.Text;

            displayArchiveStockupID.Text = ArchiveStockupID.Text;
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
            string imagelink = "http://mnmimages.dzvdesk.com/Uploads/";
            if (!image1.Text.Equals(""))
            {
                displayimage1Display.Visible = true;
                displayimage1Display.ImageUrl = imagelink + image1.Text;
            }

            // get warehouse location
            DphyLocation.Text = string.Empty;
            utilityCls obj = new utilityCls();
            DataTable retDt = obj.getTableColwithID("ArchiveStockUpInward", "ArchiveStockupID", displayArchiveStockupID.Text, "physicalId");
            if(!retDt.Rows.Count.Equals(0))
            {
                DataTable locDt = obj.getTableColwithID("Location", "LocationID", retDt.Rows[0]["physicalId"].ToString(), "Location");
                if(!locDt.Rows.Count.Equals(0))
                {
                    DphyLocation.Text = locDt.Rows[0]["Location"].ToString();
                }
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

    protected void btnReturnList_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("returnItem.aspx", true);
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

    public string fileToStream(FileUpload fileupload)
    {
        string retFile = string.Empty;
        try
        {
            if (fileupload.HasFile)
            {
                byte[] fileBytes = null;

                //Read the FileName and convert it to Byte array.
                string fileName1 = Path.GetFileName(fileupload.FileName);
                string newFileName1 = Path.Combine(Path.GetDirectoryName(fileName1)
                    , string.Concat(Path.GetFileNameWithoutExtension(fileName1)
                                   , DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")
                                   , Path.GetExtension(fileName1)
                                   )
                    );

                using (BinaryReader fileStream = new BinaryReader(fileupload.PostedFile.InputStream))
                {
                    fileBytes = fileStream.ReadBytes(fileupload.PostedFile.ContentLength);
                    fileStream.Close();
                }
                int succ1 = uploadFTP(newFileName1, fileBytes);
                //fileupload.PostedFile.InputStream.Dispose();
                if (succ1 == 1)
                {
                    retFile = newFileName1;
                }
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
        return retFile;
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (awbNo.Text.Equals(""))
            {
                divError.InnerText = "Please Enter Return ABW No.";
                divError.Visible = true;
            }
            else if (remarks.Text.Equals(""))
            {
                divError.InnerText = "Please Enter Remarks";
                divError.Visible = true;
            }
            else
            {
                string errors = string.Empty;
                string video1 = rVideo1Link.Text;
                string video2 = rVideo2Link.Text;
                /*string video1 = fileToStream(FileUploadV1);
                string video2 = fileToStream(FileUploadV2);*/
                string image1 = fileToStream(FileUpload1);
                string image2 = fileToStream(FileUpload2);
                string image3 = fileToStream(FileUpload3);
                string image4 = fileToStream(FileUpload4);
                string image5 = fileToStream(FileUpload5);
                string image6 = fileToStream(FileUpload6);
                string image7 = fileToStream(FileUpload7);
                string image8 = fileToStream(FileUpload8);
                string image9 = fileToStream(FileUpload9);
                string image10 = fileToStream(FileUpload10);

                returnItemCls obj = new returnItemCls();
                int success = obj.markReturn(displayStockupID.Text, displayArchiveStockupID.Text, displaysalesid.Text, returnStatus.SelectedValue, courier.SelectedValue, awbNo.Text, reasons.SelectedValue, remarks.Text,
                    video1, video2, image1, image2, image3, image4, image5, image6, image7, image8, image9, image10);

                if (success.Equals(-1))
                {
                    Session["returnSuccfail"] = "Return RolledBack";

                }
                else if (success.Equals(-2))
                {
                    Session["returnSuccfail"] = "Return Failed";
                }
                else if (success.Equals(1))
                {
                    Session["returnSuccfail"] = "Return Completed Successfully";
                }
                else
                {
                    Session["returnSuccfail"] = "Some Error";
                }
                //divError.Visible = true;
                Response.Redirect("returnItem.aspx", true);
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
            LinkButton btn = e.Item.FindControl("return") as LinkButton;
            ScriptManager.GetCurrent(this).RegisterPostBackControl(btn);
            Label image1 = e.Item.FindControl("image1") as Label;
            string imagelink = "http://mnmimages.dzvdesk.com/Uploads/";
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