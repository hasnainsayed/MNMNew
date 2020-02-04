using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class addToStock : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if(!IsPostBack)
            {
                lotCls lObj = new lotCls();
                DataTable lot = lObj.getActiveLot();
                lot_RPT.DataSource = lot;
                lot_RPT.DataBind();
                string lotId = string.Empty;
                int lotPiece = 0;
                if (!lot.Rows.Count.Equals(0))
                {
                    foreach (RepeaterItem itemEquipment in lot_RPT.Items)
                    {
                        RadioButton lotRadio = (RadioButton)itemEquipment.FindControl("lotRadio");
                        Label lotLblHdn = (Label)itemEquipment.FindControl("lotLblHdn");
                        Label lotPieceHdn = (Label)itemEquipment.FindControl("lotPiece");
                        lotId = lotLblHdn.Text;
                        lotPiece = Convert.ToInt32(lotPieceHdn.Text);
                        lotRadio.Checked = true;
                        break;
                    }

                    // get pieces count
                    setRFLLotPieces(lotId, lotPiece);
                }

                // bind Sizes
                /*sizeCls sObj = new sizeCls();
                DataTable size = sObj.getSizeByStyle(StyleID.Text);*/
                /*styleCls uObj = new styleCls();
                DataTable size = uObj.getTable("Size");
                size_Rpt.DataSource = size;
                size_Rpt.DataBind();*/
            }
            
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void lotRadio_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            //Uncheck all RadioButtons of the rptCustomer
            foreach (RepeaterItem item in lot_RPT.Items)
            {
                ((RadioButton)item.FindControl("lotRadio")).Checked = false;
            }

            //Set the new selected row
            RadioButton rb = (RadioButton)sender;
            RepeaterItem row = (RepeaterItem)rb.NamingContainer;
            ((RadioButton)row.FindControl("lotRadio")).Checked = true;

            // set the available pieces
            int lotPiece = 0;
            string lotId = string.Empty;
            Label lotLblHdn = (Label)row.FindControl("lotLblHdn");
            Label lotPieceHdn = (Label)row.FindControl("lotPiece");
            lotId = lotLblHdn.Text;
            lotPiece = Convert.ToInt32(lotPieceHdn.Text);
            setRFLLotPieces(lotId, lotPiece);
            //ModalPopupExtender1.Show();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }
        
    private void setRFLLotPieces(string lotId, int lotPiece)
    {
        try
        {
            lotPieces.Text = string.Empty;
            barcodePiece.Text = string.Empty;
            avlPiece.Text = string.Empty;
            
            //get pieces count
            styleCls stObj = new styleCls();
            DataTable pieceCountDt = stObj.getPieceCountByLot(lotId);
            int pieceCount = 0;
            if (!pieceCountDt.Rows.Count.Equals(0))
            {
                pieceCount = Convert.ToInt32(pieceCountDt.Rows[0]["pieces"]);
            }

            int pieceAvailable = lotPiece - pieceCount;

            lotPieces.Text = lotPiece.ToString();
            barcodePiece.Text = pieceCount.ToString();
            avlPiece.Text = pieceAvailable.ToString();
                       
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string lot = string.Empty;
            foreach (RepeaterItem itemEquipment in lot_RPT.Items)
            {
                RadioButton lotRadioBtn = (RadioButton)itemEquipment.FindControl("lotRadio");
                Label lotLblHdn = (Label)itemEquipment.FindControl("lotLblHdn");
                if (lotRadioBtn.Checked)
                {
                    lot = lotLblHdn.Text;
                    break;
                }
            }

            var barcodeArray = barcodeNo.Text.Split('-');
            string err = string.Empty;
            if (!barcodeNo.Text.Equals(""))
            {

                double size101 = Base36.Decode(barcodeArray[1].ToString());
                // check for pieces entering in lot 
                if (!size101.ToString().Equals("") && !quantity.Text.Equals(""))
                {
                    if ((Convert.ToInt32(size101.ToString()) * Convert.ToInt32(quantity.Text)) > Convert.ToInt32(avlPiece.Text))
                    {
                        err += "Total No. of pieces are more than available";
                    }
                }

                if (err.Equals(""))
                {
                    styleCls sObj = new styleCls();


                    //get style id
                    utilityCls obj = new utilityCls();
                    DataTable styleDt = obj.getTableColwithID("ItemStyle", "Control9", barcodeArray[0].ToString(), "StyleID");

                    if (!styleDt.Rows.Count.Equals(0))
                    {
                        double size10 = Base36.Decode(barcodeArray[1].ToString());
                        DataTable sizeDt = obj.getTableColwithID("Size", "Size1", size10.ToString(), "SizeId");
                        if (!sizeDt.Rows.Count.Equals(0))
                        {
                            double mrps = Base36.Decode(barcodeArray[2].ToString());
                            int success = sObj.addRflNR(sizeDt.Rows[0]["SizeId"].ToString(), lot, styleDt.Rows[0]["StyleID"].ToString(),
                                "RFL", mrps.ToString(), string.Empty, DateTime.Now.ToString(), size10.ToString(), quantity.Text, "0", lotPieces.Text,purchaseRate.Text);
                            if (!success.Equals(-1))
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Succesfully added to Inventory!');window.location ='addToStock.aspx';", true);
                                successfailure.Text = "Succesfully added to Inventory";
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Adding to Inventory FAILED!');window.location ='addToStock.aspx';", true);
                                successfailure.Text = " Adding to Inventory FAILED"; }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Style Not matched !');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Total No. of pieces are more than available !');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please enter barcode !');", true);
            }

            
            
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }
}