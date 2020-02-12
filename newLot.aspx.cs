using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;

public partial class newLot : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindData();
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

    

    protected void BindData()
    {
        try
        {
            newLotCls obj = new newLotCls();
            DataTable dt = obj.getLot();
            GV.DataSource = dt;
            GV.DataBind();
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
            totalPiece.Text = string.Empty;
            BagDescription.Text = string.Empty;
            invoiceNo.Text = string.Empty;
            invoiceDate.Text = string.Empty;
            totalAmount.Text = string.Empty;
            hdnID.Text = string.Empty;
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            clearData();
            btnSave.Text = "Save";
            styleCls obj = new styleCls();
            DataTable dt = obj.getTable("Vendor");
            vendorID.DataSource = dt;
            vendorID.DataBind();
            devCapone.Visible = false;
            divGBora.Visible = false;

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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                newLotCls obj = new newLotCls();
                string image1 = string.Empty;
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
                    int height = 0;
                    int width = 0; decimal size = 0;
                    using (BinaryReader fileStream = new BinaryReader(FileUpload1.PostedFile.InputStream))
                    {
                        fileBytes = fileStream.ReadBytes(FileUpload1.PostedFile.ContentLength);
                        System.Drawing.Image img = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);
                        height = img.Height;
                        width = img.Width;
                        size = Math.Round(((decimal)FileUpload1.PostedFile.ContentLength / (decimal)1024), 2);
                        fileStream.Close();
                    }




                    int succ1 = uploadFTP(newFileName1, fileBytes);

                    if (succ1 == 1)
                    {
                        image1 = newFileName1;
                    }

                }

                int Success = obj.addLot(vendorID.SelectedValue, totalAmount.Text, BagDescription.Text, invoiceNo.Text, invoiceDate.Text, totalPiece.Text, Session["login"].ToString(), image1,lrno.SelectedValue,travelCost.Text);
                if (Success != -1)
                {
                    totalAmount.Text = string.Empty;
                    travelCost.Text = string.Empty;
                    BagDescription.Text = string.Empty;
                    totalPiece.Text = string.Empty;
                    btnSave.Text = "Save";
                    hdnID.Text = string.Empty;
                    styleCls objS = new styleCls();
                    DataTable dt = objS.getTable("Vendor");
                    vendorID.DataSource = dt;
                    vendorID.DataBind();

                    DataTable dt1 = objS.getTable("lrListing");
                    lrno.DataSource = dt1;
                    lrno.DataBind();
                    devCapone.Visible = false;

                    BindData();
                }

                else
                {

                }

            }
            else if (btnSave.Text == "Update")
            {
                newLotCls obj = new newLotCls();
                string image1 = string.Empty;
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
                    int height = 0;
                    int width = 0; decimal size = 0;
                    using (BinaryReader fileStream = new BinaryReader(FileUpload1.PostedFile.InputStream))
                    {
                        fileBytes = fileStream.ReadBytes(FileUpload1.PostedFile.ContentLength);
                        System.Drawing.Image img = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);
                        height = img.Height;
                        width = img.Width;
                        size = Math.Round(((decimal)FileUpload1.PostedFile.ContentLength / (decimal)1024), 2);
                        fileStream.Close();
                    }




                    int succ1 = uploadFTP(newFileName1, fileBytes);

                    if (succ1 == 1)
                    {
                        image1 = newFileName1;
                    }

                }
                int Success = obj.updateLot(Convert.ToInt32(hdnID.Text), vendorID.SelectedValue, totalAmount.Text, BagDescription.Text, invoiceNo.Text, invoiceDate.Text, totalPiece.Text, image1, lrno.SelectedValue, travelCost.Text);
                if (Success != -1)
                {
                    totalAmount.Text = string.Empty;
                    BagDescription.Text = string.Empty;
                    totalPiece.Text = string.Empty;
                    btnSave.Text = "Save";
                    hdnID.Text = string.Empty;
                    styleCls objS = new styleCls();
                    DataTable dt = objS.getTable("Vendor");
                    vendorID.DataSource = dt;
                    vendorID.DataBind();

                    devCapone.Visible = false;
                    BindData();
                }
                else
                {

                }
            }
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
            clearData();
            invoiceDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            btnSave.Text = "Save";
            styleCls obj = new styleCls();
            DataTable dt = obj.getTable("Vendor");
            vendorID.DataSource = dt;
            vendorID.DataBind();

            DataTable dt1 = obj.getTable("lrListing");
            lrno.DataSource = dt1;
            lrno.DataBind();

            devCapone.Visible = true;

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void active_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            Label BagID = (Label)rp1.FindControl("lblBagId");
            newLotCls objL = new newLotCls();
            int success = objL.changeLotStatus(Convert.ToInt32(BagID.Text), "0");
            BindData();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void inactive_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            Label BagID = (Label)rp1.FindControl("lblBagId");
            newLotCls objL = new newLotCls();
            int success = objL.changeLotStatus(Convert.ToInt32(BagID.Text), "1");
            //Response.Redirect("newLot.aspx",true);
            BindData();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void GV_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            string IsActive = ((DataRowView)e.Item.DataItem)["IsActive"].ToString();
            LinkButton active = (LinkButton)e.Item.FindControl("active");
            LinkButton inactive = (LinkButton)e.Item.FindControl("inactive");
            LinkButton edit = (LinkButton)e.Item.FindControl("edit");
            Label status = (Label)e.Item.FindControl("status");
            if(IsActive.Equals("2"))
            {
                status.Visible = true;
                status.Text = "Bora Generated";
                edit.Visible = true;
            }
            else if(IsActive.Equals("3"))
            {
                status.Visible = true;
                inactive.Attributes.Remove("class");
                inactive.Attributes.Add("class", "btn btn-sm btn-default");
                inactive.Visible = true;
                inactive.Text = "Loaded";
                
                edit.Visible = true;
            }
            else if (IsActive.Equals("1"))
            {
                active.Visible = true;
            }
            else if (IsActive.Equals("0"))
            {
                inactive.Visible = true;
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void edit_Click(object sender, EventArgs e)
    {
        try
        {
            clearData();
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            Label BagID = (Label)rp1.FindControl("lblBagId");
            newLotCls objL = new newLotCls();
            DataTable lot = objL.getLotById(BagID.Text);
            totalAmount.Text = lot.Rows[0]["totalAmount"].ToString();
            BagDescription.Text = lot.Rows[0]["BagDescription"].ToString();
            btnSave.Text = "Update";
            hdnID.Text = lot.Rows[0]["BagID"].ToString();
            styleCls obj = new styleCls();
            DataTable dt = obj.getTable("Vendor");
            vendorID.DataSource = dt;
            vendorID.DataBind();
            vendorID.SelectedValue = lot.Rows[0]["vendorID"].ToString();

            DataTable dt1 = obj.getTable("lrListing");
            lrno.DataSource = dt1;
            lrno.DataBind();
            if(!lot.Rows[0]["lrno"].ToString().Equals(""))
            {
                lrno.SelectedValue = lot.Rows[0]["lrno"].ToString();
            }
            
            travelCost.Text= lot.Rows[0]["travelCost"].ToString();

            if (lot.Rows[0]["invoiceDate"].ToString().Equals(""))
            {
                invoiceDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            }
            else
            {
                invoiceDate.Text = Convert.ToDateTime(lot.Rows[0]["invoiceDate"]).ToString("MM/dd/yyyy");
            }

            invoiceNo.Text = lot.Rows[0]["invoiceNo"].ToString();
            totalPiece.Text = lot.Rows[0]["totalPiece"].ToString();
            lotImageDisplay.ImageUrl = string.Empty;
            if (!lot.Rows[0]["lotImage"].ToString().Equals(""))
            {
                storedProcedureCls obj1 = new storedProcedureCls();
                DataTable imagePath = obj1.getTableWithCondition("imagePaths", "pathId", "1", "pathId", "desc");
                if (!imagePath.Rows.Count.Equals(0))
                {
                    if (!imagePath.Rows[0]["paths"].ToString().Equals("") && !lot.Rows[0]["lotImage"].ToString().Equals(""))
                    {
                        lotImageDisplay.Visible = true;
                        lotImageDisplay.ImageUrl = imagePath.Rows[0]["paths"].ToString() + lot.Rows[0]["lotImage"].ToString();

                    }
                }

            }

            devCapone.Visible = true;

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void makePayment_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            Label BagID = (Label)rp1.FindControl("lblBagId");
            Session["makeLotPayment"] = BagID.Text;
            Response.Redirect("lotPayment.aspx", true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void addPayment_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("addPayment.aspx", true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void generateBora_Click(object sender, EventArgs e)
    {
        try
        {
            clearData();            
            btnSave.Text = "Save";
            styleCls obj = new styleCls();
            DataTable dt = obj.getTable("Vendor");
            vendorID1.DataSource = dt;
            vendorID1.DataBind();
            divGBora.Visible = true;
            devCapone.Visible = false;

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void saveBora_Click(object sender, EventArgs e)
    {
        try
        {
            utilityCls uObj = new utilityCls();
            string years = uObj.getYearCode(DateTime.Now.ToString("yyyy"));
            string month = uObj.getCurrentMonth(DateTime.Now.ToString("MMMM"));
            string msg = string.Empty;
            if (month == "ERROR" || years == "ERROR")
            {
                msg = "Month/Year Conversion Error";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + msg + "');", true);
            }
            else
            {
                newLotCls obj = new newLotCls();
                string res = obj.saveBora(years, month, vendorID1.SelectedValue, noOfBora.Text,Session["login"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", "alert('" + res + "');", true);
                divGBora.Visible = false;
            }
            BindData();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void deleteLot_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            Label BagID = (Label)rp1.FindControl("lblBagId");
            newLotCls obj = new newLotCls();
            string res = obj.deleteLot(BagID.Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", "alert('" + res + "');", true);
            BindData();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void generateMulBora_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("multipleBora.aspx",true);
        }
        catch(Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}