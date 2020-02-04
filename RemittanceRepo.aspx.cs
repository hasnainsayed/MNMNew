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

public partial class RemittanceRepo : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                locationCls obj = new locationCls();
                DataTable dt = obj.getVirtualLocation("2");
                virtualLocation.DataSource = dt;
                virtualLocation.DataBind();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "firedtSearch();", true);
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }
    }
    public void BindData()
    {
        try
        {
            payment_reportCls obj = new payment_reportCls();
            DataTable dt = obj.BindRemittance(virtualLocation.SelectedValue);

            rpt_salesidnotexits.DataSource = dt;
            rpt_salesidnotexits.DataBind();

            ////
        }
        catch (Exception ex)
        {
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }
    }
    protected void rpt_salesidnotexits_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            object IGST = 0;
            object SGST = 0;
            object CGSt = 0;
            object selling_price =0;
            object cp = 0;
            object logictic = 0;
            object spincgst = 0;
            object spexcgst = 0;
            object chnellcommsion = 0;
            object Utax = 0;
            object percent = 0;
            object gateway = 0;
            object mis = 0;

            decimal DectIncgst = 0;
            decimal Dectexcgst = 0;
            decimal TotalDectIncgst = 0;
            decimal TotalDectexcgst = 0;
            decimal SetllAmtinc = 0;
            decimal SetllAmtexc = 0;
            decimal SetllAmtincper = 0;
            decimal SetllAmtexcper = 0;
            decimal Netpro = 0;
            decimal Netper = 0;
            decimal TOtalGst = 0;


            IGST =((DataRowView)e.Item.DataItem)["totIGST"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["totIGST"].ToString();
            SGST = ((DataRowView)e.Item.DataItem)["totSGST"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["totSGST"].ToString();
            CGSt = ((DataRowView)e.Item.DataItem)["totCGST"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["totCGST"].ToString();
            cp = ((DataRowView)e.Item.DataItem)["cp"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["cp"].ToString();
            chnellcommsion = ((DataRowView)e.Item.DataItem)["channel_commsion"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["channel_commsion"].ToString();
            gateway = ((DataRowView)e.Item.DataItem)["Channel_Gateway"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["Channel_Gateway"].ToString();
            selling_price = ((DataRowView)e.Item.DataItem)["sys_sp"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["sys_sp"].ToString();
            spincgst = ((DataRowView)e.Item.DataItem)["spincgst"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["spincgst"].ToString();
            spexcgst = ((DataRowView)e.Item.DataItem)["spexcgst"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["spexcgst"].ToString();
            logictic = ((DataRowView)e.Item.DataItem)["VL_Logistics"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["VL_Logistics"].ToString();
            Utax = ((DataRowView)e.Item.DataItem)["utax"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["utax"].ToString();
            percent = ((DataRowView)e.Item.DataItem)["percentage"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["percentage"].ToString();

            TOtalGst = (Convert.ToDecimal(selling_price) * Convert.ToDecimal(percent)) / 100;




            Label lbltotdecinc = e.Item.FindControl("lbltotdecinc") as Label;
            Label lbltotdecexc = e.Item.FindControl("lbltotdecexc") as Label;
            Label setamtinc = e.Item.FindControl("setamtinc") as Label;
            Label setperinc = e.Item.FindControl("setperinc") as Label;
            Label setamtexc = e.Item.FindControl("setamtexc") as Label;
            Label setperexc = e.Item.FindControl("setperexc") as Label;
            Label netpro = e.Item.FindControl("netpro") as Label;
            Label lblnetper = e.Item.FindControl("lblnetper") as Label;
            //Total Deduction Inc(GST)
            DectIncgst = Math.Abs(Convert.ToDecimal(selling_price)) - Math.Abs(Convert.ToDecimal(chnellcommsion)) - Math.Abs(Convert.ToDecimal(logictic))-Math.Abs(Convert.ToDecimal(gateway));

            TotalDectIncgst= Math.Round(Convert.ToDecimal(selling_price) - Convert.ToDecimal(DectIncgst),2);

            //Total Deduction exc(GST)
            Dectexcgst = Math.Abs(Convert.ToDecimal(selling_price))  - Math.Abs(Convert.ToDecimal(chnellcommsion)) - Math.Abs(Convert.ToDecimal(logictic)) - Math.Abs(Convert.ToDecimal(TOtalGst));


            TotalDectexcgst = Math.Round(Convert.ToDecimal(selling_price) - Convert.ToDecimal(Dectexcgst), 2);

            //Settled Amount (sales_price - total deduction -cp)
            SetllAmtinc = Math.Round(Convert.ToDecimal(selling_price) - TotalDectIncgst - Convert.ToDecimal(cp), 2);//incgst

            SetllAmtexc = Math.Round(Convert.ToDecimal(selling_price) - TotalDectexcgst - Convert.ToDecimal(cp),2);//exc gst
            try
            {

                SetllAmtincper = Math.Round((SetllAmtinc / Convert.ToDecimal(selling_price)) * 100, 2);
                //Math.Round((SetllAmtinc / Convert.ToDecimal(spincgst)) * 100, 2);
            }
            catch (Exception ex)
            {
                SetllAmtincper = 0;
            }

            //settled Percentage (setamt/salesprice*100)Exc GSt
            try
            {
                SetllAmtexcper = Math.Round((SetllAmtexc / (Convert.ToDecimal(selling_price)- TOtalGst)) * 100, 2);
            }
            catch (Exception ex)
            {
                SetllAmtexcper = 0;
            }

            //Net Profit (sales Price -commission -cp)
            Netpro = Math.Round(Convert.ToDecimal(selling_price) - Convert.ToDecimal(chnellcommsion) - Convert.ToDecimal(cp), 2);

            //Net Percentages ((netprofit/salesprice)*100)
            try
            {

                Netper = Math.Round((Netpro / Convert.ToDecimal(selling_price)) * 100, 2);
            }
            catch (Exception ex)
            {
                Netper = 0;
            }

            lbltotdecinc.Text = Convert.ToString(TotalDectIncgst);
            lbltotdecexc.Text = Convert.ToString(TotalDectexcgst);
            setamtinc.Text = Convert.ToString(SetllAmtinc);
            setperinc.Text = Convert.ToString(SetllAmtincper);
            setamtexc.Text = Convert.ToString(SetllAmtexc);
            setperexc.Text = Convert.ToString(SetllAmtexcper);
            netpro.Text = Convert.ToString(Netpro);
            lblnetper.Text = Convert.ToString(Netper);
            //setamtinc.Text = "";
            //setperinc.Text = "";
            //setamtexc.Text = "";
            //setperexc.Text = "";
            //netpro.Text = "";
            //lblnetper.Text = "";
        }
        catch(Exception ex)
        {
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }
    }

    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
        try
        {
            payment_reportCls obj = new payment_reportCls();
            DataTable dtExcel = obj.BindRemittance(virtualLocation.SelectedValue);
            if (!dtExcel.Rows.Count.Equals(0))
            {
                object IGST = 0;
                object SGST = 0;
                object CGSt = 0;
                object selling_price = 0;
                object cp = 0;
                object logictic = 0;
                object spincgst = 0;
                object spexcgst = 0;
                object chnellcommsion = 0;
                object Utax = 0;
                object percent = 0;

                decimal DectIncgst = 0;
                decimal Dectexcgst = 0;
                decimal TotalDectIncgst = 0;
                decimal TotalDectexcgst = 0;
                decimal SetllAmtinc = 0;
                decimal SetllAmtexc = 0;
                decimal SetllAmtincper = 0;
                decimal SetllAmtexccper = 0;
                decimal Netpro = 0;
                decimal Netper = 0;
                decimal TOtalGst = 0;
                object gateway = 0;
                object mis = 0;
                object penlty = 0;
                object paybleamnt = 0;
                object salesid;
                 object mrp;
                decimal calnet = 0;

                DataTable dt = new DataTable();

                dt.Columns.Add("Pmt Date", typeof(string));
                dt.Columns.Add("Pmt Month", typeof(string));
                dt.Columns.Add("Pmt Year", typeof(string));
                dt.Columns.Add("Sales Date", typeof(string));
                dt.Columns.Add("Sales Month", typeof(string));
                dt.Columns.Add("Sales Year", typeof(string));
                dt.Columns.Add("Channel(VL)", typeof(string));
                dt.Columns.Add("Invoice #", typeof(Int32));
                dt.Columns.Add("Order id#", typeof(string));
                dt.Columns.Add("StockUp id#", typeof(Int32));
                dt.Columns.Add("Payment Status", typeof(string));
                dt.Columns.Add("Pmt Effecting Status", typeof(string));
                dt.Columns.Add("System Status", typeof(string));
                dt.Columns.Add("Lot#", typeof(string));
                dt.Columns.Add("Brand", typeof(string));
                dt.Columns.Add("Title", typeof(string));
                dt.Columns.Add("Size", typeof(string));
                dt.Columns.Add("Article", typeof(string));
                dt.Columns.Add("Age Bucket(Lot)", typeof(Int32));
                dt.Columns.Add("Age Bucket(Barcode)", typeof(Int32));
                dt.Columns.Add("MRP", typeof(decimal));
                dt.Columns.Add("CP", typeof(decimal));
                dt.Columns.Add("Pmt Sales Price", typeof(decimal));
                dt.Columns.Add("System Sales Price", typeof(decimal));
                dt.Columns.Add("Channel(VL) Commision", typeof(decimal));
                dt.Columns.Add("Channel Gateway", typeof(decimal));
                dt.Columns.Add("Channel Logistics", typeof(decimal));
                dt.Columns.Add("VL Penalty", typeof(decimal));
                dt.Columns.Add("VL Misc", typeof(decimal));
                dt.Columns.Add("Total  IGST", typeof(decimal));
                dt.Columns.Add("Total SGST", typeof(decimal));
                dt.Columns.Add("Total CSGT", typeof(decimal));
                dt.Columns.Add("Total Deduction(inc GST)", typeof(decimal));
                dt.Columns.Add("Total Deduction(exl GST)", typeof(decimal));
                dt.Columns.Add("Settled Amount(inc GST)", typeof(decimal));
                dt.Columns.Add("Settled %(inc GST)", typeof(decimal));
                dt.Columns.Add("Settled Amount(exl GST)", typeof(decimal));
                dt.Columns.Add("Settled %(exl GST)", typeof(decimal));
                dt.Columns.Add("Net profit", typeof(decimal));
                dt.Columns.Add("Net Profit %", typeof(decimal));

                foreach (DataRow dr in dtExcel.Rows)
                {
                    //decimal 
                    salesid = dr["salesid"].ToString().Equals("") ? "0" : dr["salesid"].ToString();
                    Utax = dr["utax"].ToString().Equals("") ? "0" : dr["utax"].ToString();
                    cp = dr["cp"].ToString().Equals("") ? "0" : dr["cp"].ToString();
                    percent = dr["percentage"].ToString().Equals("") ? "0" : dr["percentage"].ToString();
                    IGST = dr["totIGST"].ToString().Equals("") ? "0" : dr["totIGST"].ToString();
                    SGST = dr["totSGST"].ToString().Equals("") ? "0" : dr["totSGST"].ToString();
                    CGSt = dr["totCGST"].ToString().Equals("") ? "0" : dr["totCGST"].ToString();
                    cp = dr["cp"].ToString().Equals("") ? "0" : dr["cp"].ToString();
                    chnellcommsion = dr["channel_commsion"].ToString().Equals("") ? "0" : dr["channel_commsion"].ToString();
                    gateway = dr["Channel_Gateway"].ToString().Equals("") ? "0" : dr["Channel_Gateway"].ToString();
                    paybleamnt = dr["Payable_Amoun"].ToString().Equals("") ? "0" : dr["Payable_Amoun"].ToString();
                    selling_price = dr["pmt_sp"].ToString().Equals("") ? "0" : dr["pmt_sp"].ToString();
                    spexcgst = dr["spexcgst"].ToString().Equals("") ? "0" : dr["spexcgst"].ToString();
                    spincgst = dr["spincgst"].ToString().Equals("") ? "0" : dr["spincgst"].ToString();
                    logictic = dr["VL_Logistics"].ToString().Equals("") ? "0" : dr["VL_Logistics"].ToString();
                    penlty = dr["VLPenalty"].ToString().Equals("") ? "0" : dr["VLPenalty"].ToString();
                    mis = dr["misccharges"].ToString().Equals("") ? "0" : dr["misccharges"].ToString();
                    mrp = dr["mrp"].ToString().Equals("") ? "0" : dr["mrp"].ToString();
                    if (cp.ToString().Equals("-1"))
                    {
                        decimal Total = Math.Abs(Convert.ToDecimal(IGST)) + Math.Abs(Convert.ToDecimal(SGST)) + Math.Abs(Convert.ToDecimal(CGSt));
                        TOtalGst = Total;
                    }
                    else
                    {
                        decimal Total = (Convert.ToDecimal(selling_price) * Convert.ToDecimal(percent)) / 100;
                        TOtalGst = Total + Math.Abs(Convert.ToDecimal(Utax));
                    }
                    if (salesid.ToString().Equals("7221826380"))
                    {

                    }

                    DectIncgst = Math.Round(Math.Abs(Convert.ToDecimal(paybleamnt)) + Math.Abs(Convert.ToDecimal(mis)), 2);
                    TotalDectIncgst = Math.Abs(Convert.ToDecimal(selling_price)) - DectIncgst;

                    TotalDectexcgst = Math.Round(Convert.ToDecimal(TotalDectIncgst) - Convert.ToDecimal(TOtalGst), 2);
                    try
                    {
                        calnet = Convert.ToDecimal(mrp) / Convert.ToDecimal(cp);
                    }
                    catch (Exception ex)
                    {
                        calnet = 0;
                    }

                    SetllAmtinc = Math.Round(Math.Abs(Convert.ToDecimal(selling_price)) - TotalDectIncgst - Convert.ToDecimal(calnet), 2);//incgst

                    SetllAmtexc = Math.Round(Math.Abs(Convert.ToDecimal(selling_price)) - TotalDectexcgst - Convert.ToDecimal(calnet), 2);//exc gst

                    
                    try
                    {

                        SetllAmtincper = Math.Round((SetllAmtinc / Math.Abs(Convert.ToDecimal(selling_price))) * 100, 2);
                        //Math.Round((SetllAmtinc / Convert.ToDecimal(spincgst)) * 100, 2);
                    }
                    catch (Exception ex)
                    {
                        SetllAmtincper = 0;
                    }
                    try
                    {
                        SetllAmtexccper = Math.Round((SetllAmtexc / Math.Abs((Convert.ToDecimal(selling_price)) - TOtalGst)) * 100, 2);
                    }
                    catch (Exception ex)
                    {
                        SetllAmtexccper = 0;
                    }
                    
                    Netpro = Math.Round(Math.Abs(Convert.ToDecimal(selling_price)) - Math.Abs(Convert.ToDecimal(chnellcommsion)) - Math.Abs(Convert.ToDecimal(logictic)) - Math.Abs(Convert.ToDecimal(calnet)), 2);
                    try
                    {
                        Netper = Math.Round(Netpro / Math.Abs(Convert.ToDecimal(selling_price)) * 100, 2);
                    }
                    catch(Exception ex)
                    {
                        Netper = 0;
                    }

                    DataRow row = dt.NewRow();
                    row["Pmt Date"] = dr["pmt_date"];
                    row["Pmt Month"] = dr["pmt_Month"];
                    row["Pmt Year"] = dr["pmt_year"];
                    row["Sales Date"] = dr["date"];
                    row["Sales Month"] = dr["Month"];
                    row["Sales Year"] = dr["year"];
                    row["Channel(VL)"] = dr["Location"];
                    row["Invoice #"] = dr["invoiceid"];
                    row["Order id#"] = dr["salesid"];
                    row["StockUp id#"] = dr["stockupId"];
                    row["Payment Status"] = dr["payment_status"];
                    row["Pmt Effecting Status"] = dr["pmt_effecting_status"];
                    row["System Status"] = dr["sys_status"];
                    row["Lot#"] = dr["Lotno"];
                    row["Brand"] = dr["brand"];
                    row["Title"] = dr["Title"];
                    row["Size"] = dr["Size"];
                    row["Article"] = dr["articel"];
                    row["Age Bucket(Lot)"] = dr["lotAge"];
                    row["Age Bucket(Barcode)"] = dr["stockAge"];
                    row["MRP"] = dr["mrp"];
                    row["CP"] = calnet;
                    row["Pmt Sales Price"] = dr["pmt_sp"];
                    row["System Sales Price"] = dr["sys_sp"];
                    row["Channel(VL) Commision"] = dr["channel_commsion"];
                    row["Channel Gateway"] = dr["Channel_Gateway"];
                    row["Channel Logistics"] = dr["VL_Logistics"];
                    row["VL Penalty"] = dr["VLPenalty"];
                    row["VL Misc"] = mis;
                    row["Total  IGST"] = TOtalGst;
                    row["Total SGST"] = dr["totSGST"];
                    row["Total CSGT"] = dr["totCGST"];
                    row["Total Deduction(inc GST)"] = TotalDectIncgst;
                    row["Total Deduction(exl GST)"] = TotalDectexcgst;
                    row["Settled Amount(inc GST)"] = SetllAmtinc;
                    row["Settled %(inc GST)"] = SetllAmtincper;
                    row["Settled Amount(exl GST)"] = SetllAmtexc;
                    row["Settled %(exl GST)"] = SetllAmtexccper;
                    row["Net profit"] = Netpro;
                    row["Net Profit %"] = Netper;
                    //row["Total Deduction(exl GST)"]= TotalDectexcgst;
                    //row["Settled Amount"] = SetllAmtinc;
                    //row["Settled %"] = SetllAmtincper;
                    //row["Net profit"] = Netpro;
                    //row["Net Profit %"] = Netper;
                    dt.Rows.Add(row);
                }




                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "Remittance Report");
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    string fname = virtualLocation.SelectedItem.Text+" Remittance-Repo_" + DateTime.Now.ToString("dd-MM-yyyy-HH:mm:ss");

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
            else
            {

            }
        }
        catch(Exception ex)
        {
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        BindData();
        maindiv.Visible = true;
    }
}