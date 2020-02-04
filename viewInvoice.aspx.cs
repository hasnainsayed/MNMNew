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
using System.Drawing;
using System.Drawing.Imaging;

public partial class viewInvoice : System.Web.UI.Page
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
            if (Session["invoiceId"]!= null)
            {
                string invoiceId = Session["invoiceId"].ToString();
                Session.Remove("invoiceId");
                invoiceCls obj = new invoiceCls();
                DataTable invoice = obj.getInvoicebyID(invoiceId);
                DataTable sales = obj.getSalesbyInvID(invoiceId);
                DataTable salesarchive = obj.getSalesbyArchiveInvID(invoiceId);
                sales.Merge(salesarchive);
                DataTable newSales = new DataTable() ;
                newSales.Columns.Add("srNo");
                newSales.Columns.Add("BarcodeNo");
                newSales.Columns.Add("Title");
                newSales.Columns.Add("checkNo", typeof(Int32));               
                newSales.Columns.Add("Qnty", typeof(Int32));
                newSales.Columns.Add("Pieces");
                newSales.Columns.Add("taxableamount");
                newSales.Columns.Add("totalAmount");
                newSales.Columns.Add("igstamnt");                
                newSales.Columns.Add("cgstamnt");
                newSales.Columns.Add("sgstamnt");
                newSales.Columns.Add("gstpercent");
                newSales.Columns.Add("sellingprice");

                newSales.Columns.Add("piecePerPacket");

                // merge columns
                /*for (int i = 1; i <= sales.Rows.Count; i++)
                {
                    DataRow[] foundAuthors = newSales.Select("Barcode like '" + BarcodeNo + "%'");
                }*/
                int i = 0;
                foreach(DataRow dRow in sales.Rows)
                {
                    DataRow[] foundAuthors = newSales.Select("BarcodeNo like '" + dRow["BarcodeNo"].ToString().Substring(0,dRow["BarcodeNo"].ToString().Length -3)  + "%' and sellingprice='" + dRow["sellingprice"] + "'");
                    if(foundAuthors.Length.Equals(0)) // add in new table
                    {
                        DataRow[] salesRow = sales.Select("BarcodeNo like '" + dRow["BarcodeNo"].ToString().Substring(0, dRow["BarcodeNo"].ToString().Length - 3) + "%' and sellingprice='" + dRow["sellingprice"] + "'");

                        

                        //object sumSellingPrice;
                        //sumSellingPrice = sales.Compute("Sum(sellingprice)", "BarcodeNo like '" + dRow["BarcodeNo"].ToString().Substring(0, dRow["BarcodeNo"].ToString().Length - 3) + "%' and sellingprice='" + dRow["sellingprice"] + "'");

                        object sumigstamnt;
                        sumigstamnt = sales.Compute("Sum(igstamnt)", "BarcodeNo like '" + dRow["BarcodeNo"].ToString().Substring(0, dRow["BarcodeNo"].ToString().Length - 3) + "%' and sellingprice='" + dRow["sellingprice"] + "'");

                        object sumsgstamnt;
                        sumsgstamnt = sales.Compute("Sum(sgstamnt)", "BarcodeNo like '" + dRow["BarcodeNo"].ToString().Substring(0, dRow["BarcodeNo"].ToString().Length - 3) + "%' and sellingprice='" + dRow["sellingprice"] + "'");

                        object sumcgstamnt;
                        sumcgstamnt = sales.Compute("Sum(cgstamnt)", "BarcodeNo like '" + dRow["BarcodeNo"].ToString().Substring(0, dRow["BarcodeNo"].ToString().Length - 3) + "%' and sellingprice='" + dRow["sellingprice"] + "'");

                        object sumtaxableamount;
                        sumtaxableamount = sales.Compute("Sum(taxableamount)", "BarcodeNo like '" + dRow["BarcodeNo"].ToString().Substring(0, dRow["BarcodeNo"].ToString().Length - 3) + "%' and sellingprice='" + dRow["sellingprice"] + "'");

                        object sumSellingPrice;
                        sumSellingPrice = sales.Compute("Sum(sellingprice)", "BarcodeNo like '" + dRow["BarcodeNo"].ToString().Substring(0, dRow["BarcodeNo"].ToString().Length - 3) + "%' and sellingprice='" + dRow["sellingprice"] + "'");

                        object sumpiecePerPacket;
                        sumpiecePerPacket = sales.Compute("Sum(piecePerPacket)", "BarcodeNo like '" + dRow["BarcodeNo"].ToString().Substring(0, dRow["BarcodeNo"].ToString().Length - 3) + "%' and sellingprice='" + dRow["sellingprice"] + "'");

                        object sumQnty;
                        sumQnty = sales.Compute("Count(sid)", "BarcodeNo like '" + dRow["BarcodeNo"].ToString().Substring(0, dRow["BarcodeNo"].ToString().Length - 3) + "%' and sellingprice='" + dRow["sellingprice"] + "'");


                        newSales.Rows.Add(++i, salesRow[0]["BarcodeNo"].ToString(), salesRow[0]["Title"].ToString(), salesRow[0]["checkNo"].ToString(), (Int32)sumQnty, sumpiecePerPacket.ToString(),
                            sumtaxableamount.ToString(),(Convert.ToDecimal(sumtaxableamount.ToString())+Convert.ToDecimal(sumigstamnt.ToString()) + Convert.ToDecimal(sumcgstamnt.ToString()) + Convert.ToDecimal(sumsgstamnt.ToString())).ToString(),
                    sumigstamnt.ToString(), sumcgstamnt.ToString(), sumsgstamnt.ToString(), salesRow[0]["gstpercent"].ToString(), 
                    salesRow[0]["sellingprice"].ToString(), salesRow[0]["piecePerPacket"].ToString());
                       
                    }
                }

                custname.Text = invoice.Rows[0]["custname"].ToString().ToUpper();
                phoneNo.Text = invoice.Rows[0]["phoneNo"].ToString();
                /*address1.Text = invoice.Rows[0]["address1"].ToString().ToUpper();
                address2.Text = invoice.Rows[0]["address2"].ToString().ToUpper();
                cityname.Text = invoice.Rows[0]["city"].ToString().ToUpper();
                statename.Text = (invoice.Rows[0]["statename"].ToString().ToUpper()).Split('-')[1];
                phoneNo.Text = invoice.Rows[0]["phoneNo"].ToString();

                custname1.Text = invoice.Rows[0]["custname"].ToString().ToUpper();
                address11.Text = invoice.Rows[0]["address1"].ToString().ToUpper();
                address21.Text = invoice.Rows[0]["address2"].ToString().ToUpper();
                cityname1.Text = invoice.Rows[0]["city"].ToString().ToUpper();
                statename1.Text = (invoice.Rows[0]["statename"].ToString().ToUpper()).Split('-')[1];
                phoneNo1.Text = invoice.Rows[0]["phoneNo"].ToString();*/

                invoicenum.Text = invoice.Rows[0]["invid"].ToString();

                totalamnt.Text = invoice.Rows[0]["total"].ToString();
                string[] breakNum = invoice.Rows[0]["total"].ToString().Split('.');
                NumberToWords nobj = new NumberToWords();
                string wordInt = nobj.changeCurrencyToWords(Convert.ToInt32(breakNum[0]));
                if(!breakNum[1].Equals("00"))
                {
                    string wordDecimal = nobj.changeCurrencyToWords(Convert.ToInt32(breakNum[1]));
                    wordInt += " and " + wordDecimal + " paise";
                }
                amntwords.Text = "Rs "+wordInt;
                invoicedate.Text = Convert.ToDateTime(invoice.Rows[0]["salesDate"]).ToString("dd-MMM-yyyy");

                /*courierName.Text = sales.Rows[0]["courierName"].ToString().ToUpper();
                salesIDgivenbyVloc.Text = sales.Rows[0]["salesidgivenbyvloc"].ToString();

                portal.Text = sales.Rows[0]["Location"].ToString().ToUpper();
                paymentMode.Text = invoice.Rows[0]["paymentMode"].ToString().ToUpper();

                salesABWNo.Text = sales.Rows[0]["salesAbwno"].ToString();
                orderDate.Text = Convert.ToDateTime(sales.Rows[0]["recordtimestamp"]).ToString("dd-MMM-yyyy");*/
                                
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

                object totalPiece;
                totalPiece = sales.Compute("Sum(piecePerPacket)", string.Empty);
                totalPieces.Text = totalPiece.ToString();

                object totQntys;
                //totQntys = newSales.Compute("Sum(Convert(Qnty, 'System.Int32')", "");
                totQntys = newSales.Compute("Sum(Qnty)", string.Empty);
                totQnty.Text = totQntys.ToString();

                
                stateID.Text = invoice.Rows[0]["state"].ToString();
                remarks.Text = invoice.Rows[0]["rmks"].ToString();

                /*if(invoice.Rows[0]["state"].ToString().Equals("27"))
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
                }*/

                DataView dv = newSales.DefaultView;
                dv.Sort = "checkNo asc";
                DataTable sortedDT = dv.ToTable();

                rpt_Invoice.DataSource = sortedDT;
                rpt_Invoice.DataBind();

                /*string barCode = sales.Rows[0]["salesidgivenbyvloc"].ToString();
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
                }*/
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