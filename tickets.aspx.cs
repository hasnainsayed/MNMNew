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
using ClosedXML;
using System.Data.SqlClient;
using ClosedXML.Excel;
using System.Net;

public partial class tickets : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindData();
                if (Session["ticketSuccfail"] != null)
                {
                    if (Session["ticketSuccfail"].ToString().Equals("Ticket Generated Successfully") || Session["ticketSuccfail"].ToString().Equals("Return Images Update Successfully")) 
                    {
                        divSucc.InnerText = Session["ticketSuccfail"].ToString();
                        divSucc.Visible = true;
                        divError.Visible = false;
                        Session.Remove("ticketSuccfail");
                    }
                    else
                    {
                        divError.InnerText = Session["ticketSuccfail"].ToString();
                        divError.Visible = true;
                        divSucc.Visible = false;
                        Session.Remove("ticketSuccfail");
                    }

                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "firedtSearch();", true);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey132", "firedate();", true);
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void BindData()
    {
        try
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            fromDate.Text = startDate.ToString("MM/dd/yyyy");
            toDate.Text = endDate.ToString("MM/dd/yyyy");

            locationCls lobj = new locationCls();
            DataTable loc = lobj.getVirtualLocation("2");
            vLoc.DataSource = loc;
            vLoc.DataBind();
            vLoc.Items.Insert(0, new ListItem("All", "-1"));

            getTickets();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    public void getTickets()
    {
        try
        {
            divSucc.Visible = false;
            divError.Visible = false;
            ticketCls obj = new ticketCls();
            DataTable dt = obj.getTickets(fromDate.Text,toDate.Text,vLoc.SelectedValue,salesid.Text,barcode.Text,ticketNo.Text,ticketCheck.Checked,vLocCheck.Checked,salesCheck.Checked,barcodeCheck.Checked,dateRange.Checked);

            DataTable Pending = new DataTable();
            DataRow[] pendingRes = dt.Select("ticketStatus = 'Pending'");
            if (pendingRes.Any())
            {
                Pending = pendingRes.CopyToDataTable();
            }            
            rpt_Pending.DataSource = Pending;
            rpt_Pending.DataBind();

            DataTable Success = new DataTable();
            DataRow[] successRes = dt.Select("ticketStatus = 'Success'");
            if (successRes.Any())
            {
                Success = successRes.CopyToDataTable();
            }            
            rpt_Success.DataSource = Success;
            rpt_Success.DataBind();

            DataTable Rejected = new DataTable();
            DataRow[] rejectedRes = dt.Select("ticketStatus = 'Rejected'");
            if (rejectedRes.Any())
            {
                Rejected = rejectedRes.CopyToDataTable();
            }            
            rpt_Rejected.DataSource = Rejected;
            rpt_Rejected.DataBind();

            if (Session["saveTicketSuccess"] != null)
            {
                if (Session["saveTicketSuccess"].ToString().Equals("Ticket Status Changed Successfully"))
                {
                    divSucc.InnerText = Session["saveTicketSuccess"].ToString();
                    divSucc.Visible = true;
                    divError.Visible = false;
                    Session.Remove("saveTicketSuccess");
                }
                else
                {
                    divError.InnerText = Session["saveTicketSuccess"].ToString();
                    divError.Visible = true;
                    divSucc.Visible = false;
                    Session.Remove("saveTicketSuccess");
                }

            }

            divSubmitError.Visible = false;
            showReject.Visible = false;
            showSuccess.Visible = false;
            showVideo.Visible = false;
            mainList.Visible = true;
            showImages.Visible = false;
            
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("addTicket.aspx", true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void searchTicket_Click(object sender, EventArgs e)
    {
        try
        {
            getTickets();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void successTicket_Click(object sender, EventArgs e)
    {
        try
        {
            clearData();
            LinkButton btn = ((LinkButton)(sender));

            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));

            Label tid = (Label)rp1.FindControl("tid");
            displayTicketId.Text = tid.Text;

            Label ticketNo = (Label)rp1.FindControl("ticketNo");
            displayTicketNo.Text = ticketNo.Text;

            Label BarcodeNo = (Label)rp1.FindControl("BarcodeNo");
            displayBarcode.Text = BarcodeNo.Text;

            Label Location = (Label)rp1.FindControl("Location");
            displayVloc.Text = Location.Text;

            Label salesidgivenbyvloc = (Label)rp1.FindControl("salesidgivenbyvloc");
            Dsalesidgivenbyvloc.Text = salesidgivenbyvloc.Text;

            Label issueDate = (Label)rp1.FindControl("issueDate");
            displayIssueDate.Text = issueDate.Text;

            replyDate.Text = DateTime.Now.ToString("MM/dd/yyyy");

            remittanceRecDate.Text = DateTime.Now.ToString("MM/dd/yyyy");

            showSuccess.Visible = true;
            mainList.Visible = false;
            showReject.Visible = false;
            showVideo.Visible = false;
            showImages.Visible = false;
            /*replyDate.Text = DateTime.Now.ToString("MM/dd/yyyy");

            remittance.Text = "";

            rflError.Text = string.Empty;
            rflError.Visible = false;
            ModalPopupExtender1.Show();*/
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void rejectTicket_Click(object sender, EventArgs e)
    {
        try
        {
            try
            {
                clearData();
                LinkButton btn = ((LinkButton)(sender));

                RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));

                Label tid = (Label)rp1.FindControl("tid");
                displayTicketIdR.Text = tid.Text;

                Label ticketNo = (Label)rp1.FindControl("ticketNo");
                displayTicketNoR.Text = ticketNo.Text;

                Label BarcodeNo = (Label)rp1.FindControl("BarcodeNo");
                displayBarcodeR.Text = BarcodeNo.Text;

                Label Location = (Label)rp1.FindControl("Location");
                displayVlocR.Text = Location.Text;

                Label salesidgivenbyvloc = (Label)rp1.FindControl("salesidgivenbyvloc");
                DsalesidgivenbyvlocR.Text = salesidgivenbyvloc.Text;

                Label issueDate = (Label)rp1.FindControl("issueDate");
                displayIssueDateR.Text = issueDate.Text;

                replyDateR.Text = DateTime.Now.ToString("MM/dd/yyyy");

                reasons.Text = string.Empty;

                showSuccess.Visible = false;
                mainList.Visible = false;
                showReject.Visible = true;
                showVideo.Visible = false;
                showImages.Visible = false;
                /*replyDate.Text = DateTime.Now.ToString("MM/dd/yyyy");

                remittance.Text = "";

                rflError.Text = string.Empty;
                rflError.Visible = false;
                ModalPopupExtender1.Show();*/
            }
            catch (Exception ex)
            {
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex);
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void SaveSuccessTicket_Click(object sender, EventArgs e)
    {

    }

    protected void btnSuccessCancle_Click(object sender, EventArgs e)
    {
        try
        {
            clearData();
            getTickets();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void btnSaveSuccess_Click(object sender, EventArgs e)
    {
        try
        {
            string err = string.Empty;
            if(remittance.Text.Equals(""))
            {
                err += "Please Enter Remittance <br>";
            }
            if (replyDate.Text.Equals(""))
            {
                err += "Please Enter Reply Date <br>";
            }
            if (err.Equals(""))
            {
                ticketCls obj = new ticketCls();
                int success = obj.saveTicktSuccess(displayTicketId.Text,replyDate.Text,remittanceRecDate.Text,remittance.Text);
                if (success.Equals(1))
                {
                    Session["saveTicketSuccess"] = "Ticket Status Changed Successfully";
                }
                else
                {
                    Session["saveTicketSuccess"] = "Ticket Status Changing Failed";
                }
                getTickets();
            }
            else
            {
                divSubmitError.InnerHtml = err;
                divSubmitError.Visible = true;
            }
            
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void clearData()
    {
        try
        {
            displayTicketId.Text = string.Empty;
            displayTicketIdR.Text = string.Empty;
            reasons.Text = string.Empty;
            remittance.Text = string.Empty;
            divSucc.Visible = false;
            divError.Visible = false;
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
   
    protected void btnSaveReject_Click(object sender, EventArgs e)
    {
        try
        {
            string err = string.Empty;
            if (replyDateR.Text.Equals(""))
            {
                err += "Please Enter Reply Date <br>";
            }
            if (reasons.Text.Equals(""))
            {
                err += "Please Enter Reason <br>";
            }
           
            if (err.Equals(""))
            {
                ticketCls obj = new ticketCls();
                int success = obj.saveTicktReject(displayTicketIdR.Text, replyDateR.Text, reasons.Text);
                if (success.Equals(1))
                {
                    Session["saveTicketSuccess"] = "Ticket Status Changed Successfully";
                }
                else
                {
                    Session["saveTicketSuccess"] = "Ticket Status Changing Failed";
                }
                getTickets();
            }
            else
            {
                divSubmitError.InnerHtml = err;
                divSubmitError.Visible = true;
            }

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void video_Click(object sender, EventArgs e)
    {
        try
        {
            clearData();
            LinkButton btn = ((LinkButton)(sender));

            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));

            Label tid = (Label)rp1.FindControl("tid");
            vTid.Text = tid.Text;

            Label ticketNo = (Label)rp1.FindControl("ticketNo");
            vTicketNo.Text = ticketNo.Text;

            Label BarcodeNo = (Label)rp1.FindControl("BarcodeNo");
            vBarcodeNo.Text = BarcodeNo.Text;

            Label Location = (Label)rp1.FindControl("Location");
            vVLoc.Text = Location.Text;

            Label salesidgivenbyvloc = (Label)rp1.FindControl("salesidgivenbyvloc");
            vVLoc.Text = salesidgivenbyvloc.Text;

            Label issueDate = (Label)rp1.FindControl("issueDate");
            vIssuedate.Text = issueDate.Text;

            rVideo1.Text = string.Empty;

            rVideo2.Text = string.Empty;

            Label rVideo11 = (Label)rp1.FindControl("rVideo1");
            rVideo1.Text = rVideo11.Text;

            Label rVideo12 = (Label)rp1.FindControl("rVideo2");
            rVideo2.Text = rVideo12.Text;

            showVideo.Visible = true;
            mainList.Visible = false;
            showReject.Visible = false;
            showSuccess.Visible = false;
            showImages.Visible = false;
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void saveVideo_Click(object sender, EventArgs e)
    {
        try
        {
            ticketCls obj = new ticketCls();
            int success = obj.saveVideo(vTid.Text, rVideo1.Text, rVideo2.Text);
            if (success.Equals(1))
            {
                Session["editVideoLinkS"] = "Video Link Updated Successfully";
            }
            else
            {
                Session["editVideoLinkS"] = "Video Link Updated Successfully";
            }
            getTickets();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
      
    protected void exportTicket_Click(object sender, EventArgs e)
    {
        try
        {
            ticketCls obj = new ticketCls();
            DataTable dt = obj.getTickets(fromDate.Text, toDate.Text, vLoc.SelectedValue, salesid.Text, barcode.Text, ticketNo.Text, ticketCheck.Checked, vLocCheck.Checked, salesCheck.Checked, barcodeCheck.Checked, dateRange.Checked);
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Tickets");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                //Response.ContentType = "application / vnd.ms-excel";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fname = "Tickets" + DateTime.Now.ToString("MM/dd/yyyy_HH:mm:ss");

                Response.AddHeader("content-disposition", "attachment;filename=" + fname + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void rImages_Click(object sender, EventArgs e)
    {
        try
        {
            clearData();
            LinkButton btn = ((LinkButton)(sender));

            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));

            Label tid = (Label)rp1.FindControl("tid");
            iTid.Text = tid.Text;

            Label ticketNo = (Label)rp1.FindControl("ticketNo");
            iTicketNo.Text = ticketNo.Text;

            Label BarcodeNo = (Label)rp1.FindControl("BarcodeNo");
            iBarcodeNo.Text = BarcodeNo.Text;

            Label Location = (Label)rp1.FindControl("Location");
            iVLoc.Text = Location.Text;

            Label salesidgivenbyvloc = (Label)rp1.FindControl("salesidgivenbyvloc");
            iVLoc.Text = salesidgivenbyvloc.Text;

            Label issueDate = (Label)rp1.FindControl("issueDate");
            iIssuedate.Text = issueDate.Text;

            rVideo1.Text = string.Empty;

            rVideo2.Text = string.Empty;

            
            
            //string imagelink = "http://finetouchimages.dzvdesk.com/Uploads/";

            Label rimage1 = (Label)rp1.FindControl("rimage1");
            FileUpload1Display.Visible = false;
            if (!rimage1.Text.Equals(""))
            {
                FileUpload1Display.Visible = true;
                FileUpload1Display.ImageUrl =  rimage1.Text;
            }

            Label rimage2 = (Label)rp1.FindControl("rimage2");
            FileUpload2Display.Visible = false;
            if (!rimage2.Text.Equals(""))
            {
                FileUpload2Display.Visible = true;
                FileUpload2Display.ImageUrl = rimage2.Text;
            }

            Label rimage3 = (Label)rp1.FindControl("rimage3");
            FileUpload3Display.Visible = false;
            if (!rimage3.Text.Equals(""))
            {
                FileUpload3Display.Visible = true;
                FileUpload3Display.ImageUrl =  rimage3.Text;
            }

            Label rimage4 = (Label)rp1.FindControl("rimage4");
            FileUpload4Display.Visible = false;
            if (!rimage4.Text.Equals(""))
            {
                FileUpload4Display.Visible = true;
                FileUpload4Display.ImageUrl =  rimage4.Text;
            }

            Label rimage5 = (Label)rp1.FindControl("rimage5");
            FileUpload5Display.Visible = false;
            if (!rimage5.Text.Equals(""))
            {
                FileUpload5Display.Visible = true;
                FileUpload5Display.ImageUrl =  rimage5.Text;
            }

            Label rimage6 = (Label)rp1.FindControl("rimage6");
            FileUpload6Display.Visible = false;
            if (!rimage6.Text.Equals(""))
            {
                FileUpload6Display.Visible = true;
                FileUpload6Display.ImageUrl = rimage6.Text;
            }

            Label rimage7 = (Label)rp1.FindControl("rimage7");
            FileUpload7Display.Visible = false;
            if (!rimage7.Text.Equals(""))
            {
                FileUpload7Display.Visible = true;
                FileUpload7Display.ImageUrl = rimage7.Text;
            }

            Label rimage8 = (Label)rp1.FindControl("rimage8");
            FileUpload8Display.Visible = false;
            if (!rimage8.Text.Equals(""))
            {
                FileUpload8Display.Visible = true;
                FileUpload8Display.ImageUrl = rimage8.Text;
            }

            Label rimage9 = (Label)rp1.FindControl("rimage9");
            FileUpload9Display.Visible = false;
            if (!rimage9.Text.Equals(""))
            {
                FileUpload9Display.Visible = true;
                FileUpload9Display.ImageUrl = rimage9.Text;
            }

            Label rimage10 = (Label)rp1.FindControl("rimage10");
            FileUpload10Display.Visible = false;
            if (!rimage10.Text.Equals(""))
            {
                FileUpload10Display.Visible = true;
                FileUpload10Display.ImageUrl = rimage10.Text;
            }

            showVideo.Visible = false;
            mainList.Visible = false;
            showReject.Visible = false;
            showSuccess.Visible = false;
            showImages.Visible = true;

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
            request.Credentials = new NetworkCredential("images@finetouchimages.dzvdesk.com", "0v!$$S%m*Fuj");
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

    protected void saveImages_Click(object sender, EventArgs e)
    {
        try
        {
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
            ticketCls obj = new ticketCls();
            int success = obj.updateReturnImages(iTid.Text, image1, image2, image3, image4, image5,
                image6, image7, image8, image9, image10);
            if (success.Equals(1))
            {
                Session["saveTicketSuccess"] = "Return Images Update Successfully";
            }
            else
            {
                Session["saveTicketSuccess"] = "Return Images Update Failed";
            }
            getTickets();

        }
        catch(Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}