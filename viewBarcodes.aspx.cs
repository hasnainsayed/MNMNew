using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class viewBarcodes : System.Web.UI.Page
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
            if (Session["invoiceId"] != null)
            {
                string invoiceId = Session["invoiceId"].ToString();
                Session.Remove("invoiceId");
                invoiceCls obj = new invoiceCls();
                DataTable invoice = obj.getInvoicebyID(invoiceId);
                DataTable sales = obj.getSalesbyInvID(invoiceId);
                DataTable salesarchive = obj.getSalesbyArchiveInvID(invoiceId);
                sales.Merge(salesarchive, true, MissingSchemaAction.Ignore);

                custname.Text = invoice.Rows[0]["custname"].ToString().ToUpper();
                address1.Text = invoice.Rows[0]["address1"].ToString().ToUpper();
                address2.Text = invoice.Rows[0]["address2"].ToString().ToUpper();
                cityname.Text = invoice.Rows[0]["city"].ToString().ToUpper();
                statename.Text = (invoice.Rows[0]["statename"].ToString().ToUpper()).Split('-')[1];
                phoneNo.Text = invoice.Rows[0]["phoneNo"].ToString();

                custname1.Text = invoice.Rows[0]["custname"].ToString().ToUpper();
                address11.Text = invoice.Rows[0]["address1"].ToString().ToUpper();
                address21.Text = invoice.Rows[0]["address2"].ToString().ToUpper();
                cityname1.Text = invoice.Rows[0]["city"].ToString().ToUpper();
                statename1.Text = (invoice.Rows[0]["statename"].ToString().ToUpper()).Split('-')[1];
                phoneNo1.Text = invoice.Rows[0]["phoneNo"].ToString();

                invoicenum.Text = invoice.Rows[0]["invid"].ToString();
                shippingCharge.Text = "0";
                if (!invoice.Rows[0]["shippingCharge"].ToString().Equals(""))
                {
                    shippingCharge.Text = invoice.Rows[0]["shippingCharge"].ToString();
                }

                if (!sales.Rows.Count.Equals(0))
                {
                    object sellingprice;
                    sellingprice = sales.Compute("Sum(sellingprice)", string.Empty);
                    if (!sellingprice.ToString().Equals(""))
                    {
                        sellingprice = Convert.ToDecimal(sellingprice) + Convert.ToDecimal(shippingCharge.Text);
                        totalamnt.Text = sellingprice.ToString();
                        string[] breakNum = sellingprice.ToString().Split('.');
                        NumberToWords nobj = new NumberToWords();
                        string wordInt = nobj.changeCurrencyToWords(Convert.ToInt32(breakNum[0]));
                        if (!breakNum[1].Equals("00"))
                        {
                            string wordDecimal = nobj.changeCurrencyToWords(Convert.ToInt32(breakNum[1]));
                            wordInt += " and " + wordDecimal + " paise";
                        }
                        amntwords.Text = "Rs " + wordInt;
                    }
                }
                else
                {
                    amntwords.Text = "Rs ";
                    totalamnt.Text = string.Empty;
                }
                invoicedate.Text = Convert.ToDateTime(invoice.Rows[0]["salesDate"]).ToString("dd-MMM-yyyy");

                courierName.Text = sales.Rows[0]["courierName"].ToString().ToUpper();
                salesIDgivenbyVloc.Text = sales.Rows[0]["salesidgivenbyvloc"].ToString();

                portal.Text = sales.Rows[0]["Location"].ToString().ToUpper();
                paymentMode.Text = invoice.Rows[0]["paymentMode"].ToString().ToUpper();

                salesABWNo.Text = sales.Rows[0]["salesAbwno"].ToString();
                orderDate.Text = Convert.ToDateTime(sales.Rows[0]["recordtimestamp"]).ToString("dd-MMM-yyyy");

                object sumigst;
                sumigst = sales.Compute("Sum(igstamnt)", string.Empty);
                totIgst.Text = sumigst.ToString();

                object sumcgst;
                sumcgst = sales.Compute("Sum(cgstamnt)", string.Empty);
                totCgast.Text = sumcgst.ToString();

                object sumsgst;
                sumsgst = sales.Compute("Sum(sgstamnt)", string.Empty);
                totSgst.Text = sumsgst.ToString();

                object sumTaxable;
                sumTaxable = sales.Compute("Sum(taxableamount)", string.Empty);
                totTaxableAmnt.Text = sumTaxable.ToString();

                totQnty.Text = sales.Rows.Count.ToString();

                stateID.Text = invoice.Rows[0]["state"].ToString();

                if (invoice.Rows[0]["state"].ToString().Equals("27"))
                {
                    cgstFooter.Visible = true;
                    cgstHeader.Visible = true;
                    sgstFooter.Visible = true;
                    sgstHeader.Visible = true;
                }
                else
                {
                    igstFooter.Visible = true;
                    igstHeader.Visible = true;
                }

                rpt_Invoice.DataSource = sales;
                rpt_Invoice.DataBind();

                string barCode = sales.Rows[0]["salesidgivenbyvloc"].ToString();
                System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                using (Bitmap bitMap = new Bitmap(barCode.Length * 40, 80))
                {
                    using (Graphics graphics = Graphics.FromImage(bitMap))
                    {
                        Font oFont = new Font("IDAutomationHC39M", 16);
                        PointF point = new PointF(2f, 2f);
                        SolidBrush blackBrush = new SolidBrush(Color.Black);
                        SolidBrush whiteBrush = new SolidBrush(Color.White);
                        graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                        graphics.DrawString("*" + barCode + "*", oFont, blackBrush, point);
                    }
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] byteImage = ms.ToArray();

                        Convert.ToBase64String(byteImage);
                        imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    }
                    plBarCode.Controls.Add(imgBarCode);
                }

                if (!sales.Rows[0]["salesAbwno"].ToString().Equals(""))
                {
                    string barCode1 = sales.Rows[0]["salesAbwno"].ToString();
                    System.Web.UI.WebControls.Image imgBarCode1 = new System.Web.UI.WebControls.Image();
                    using (Bitmap bitMap = new Bitmap(barCode1.Length * 40, 80))
                    {
                        using (Graphics graphics = Graphics.FromImage(bitMap))
                        {
                            Font oFont = new Font("IDAutomationHC39M", 16);
                            PointF point = new PointF(2f, 2f);
                            SolidBrush blackBrush = new SolidBrush(Color.Black);
                            SolidBrush whiteBrush = new SolidBrush(Color.White);
                            graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                            graphics.DrawString("*" + barCode1 + "*", oFont, blackBrush, point);
                        }
                        using (MemoryStream ms = new MemoryStream())
                        {
                            bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            byte[] byteImage = ms.ToArray();

                            Convert.ToBase64String(byteImage);
                            imgBarCode1.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                        }
                        plBarCode1.Controls.Add(imgBarCode1);
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


    protected void rpt_Invoice_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            HtmlTableCell igstMiddle = (HtmlTableCell)e.Item.FindControl("igstMiddle");
            HtmlTableCell cgstMiddle = (HtmlTableCell)e.Item.FindControl("cgstMiddle");
            HtmlTableCell sgstMiddle = (HtmlTableCell)e.Item.FindControl("sgstMiddle");
            if (stateID.Text.ToString().Equals("27"))
            {
                cgstMiddle.Visible = true;
                sgstMiddle.Visible = true;
            }
            else
            {
                igstMiddle.Visible = true;
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}