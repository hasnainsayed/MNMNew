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


public partial class Consoledate : System.Web.UI.Page
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
                BindData();

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
            DataTable dt = obj.BindConsoledate(virtualLocation.SelectedValue);
            rpt_conso.DataSource = dt.DefaultView;
            rpt_conso.DataBind();


        }
        catch (Exception ex)
        {
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }
    }
    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
        try
        {
            object payable;
            object igst;
            object cgst;
            object sgst;
            object TCSigst;
            object TCScgst;
            object TCSsgst;
            object sp;
            object pmt_sp;
            object ligst;
            object higst;
            object cp;
            object channelcommision;
            object logistic;
            object mis;
            object penalty;
            object lowhightpoint;
            object state;
            object salesid;
            object count;
            object gateway;
            object tcsincgst;
            object tcsexcgst;
            object mrp;
            object otherchrg;
           


            decimal TotalGSt;
            decimal TotalDeduction_inc;
            decimal Totaldeduction_exc;
            decimal Setamount_inc;
            decimal setamount_exc;
            decimal Setamount_incper;
            decimal setamount_excper;
            decimal netprofit;
            decimal netprofitper;
           
            decimal cper;
            decimal sper;
            decimal iper;
            decimal totalIGST;
            decimal totalSGST;
            decimal totalCGST;
            decimal Totaltcs;
            decimal calnet;







            payment_reportCls obj = new payment_reportCls();
            DataTable table = obj.BindConsoledate(virtualLocation.SelectedValue);
            DataTable dt = new DataTable();
           
            dt.Columns.Add("Pmt Date",typeof(string));
            dt.Columns.Add("Sales Date",typeof(string));
            dt.Columns.Add("Sales Month",typeof(string));
            dt.Columns.Add("Sales Year", typeof(string));
            dt.Columns.Add("Channel(VL)", typeof(string));
            dt.Columns.Add("Invoice #", typeof(long));
            dt.Columns.Add("Order id#", typeof(string));
            dt.Columns.Add("StockUp id#", typeof(long));
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
            dt.Columns.Add("Pmt Sales Price", typeof(string));
            dt.Columns.Add("System Sales Price",typeof(decimal));
            dt.Columns.Add("Channel(VL) Commision", typeof(decimal));
            dt.Columns.Add("Channel Gateway", typeof(int));
            dt.Columns.Add("Channel Logistics", typeof(decimal));
            dt.Columns.Add("VL Penalty", typeof(decimal));
            dt.Columns.Add("VL Misc",typeof(decimal));
            dt.Columns.Add("Total  IGST", typeof(decimal));
            dt.Columns.Add("Total SGST", typeof(decimal));
            dt.Columns.Add("Total CSGT", typeof(decimal));
            dt.Columns.Add("Payable Amount", typeof(decimal));
            dt.Columns.Add("Total TCS", typeof(decimal));
            dt.Columns.Add("Total Deduction(inc GST)", typeof(decimal));
            dt.Columns.Add("Total Deduction(exl GST)", typeof(decimal));
            dt.Columns.Add("Settled Amount(inc GST)", typeof(decimal));
            dt.Columns.Add("Settled %(inc GST)", typeof(decimal));
            dt.Columns.Add("Settled Amount(exl GST)", typeof(decimal));
            dt.Columns.Add("Settled %(exl GST)", typeof(decimal));
            dt.Columns.Add("Net profit", typeof(decimal));
            dt.Columns.Add("Net Profit %", typeof(decimal));

            foreach(DataRow dr in table.Rows)
            {

                igst=dr["IGST"].ToString().Equals("") ? "0" : dr["IGST"].ToString();
                cgst = dr["CGST"].ToString().Equals("") ? "0" : dr["CGST"].ToString();
                sgst = dr["SGST"].ToString().Equals("") ? "0" : dr["SGST"].ToString();
                TCSigst = dr["TCS_IGST"].ToString().Equals("") ? "0" : dr["TCS_IGST"].ToString();
                TCScgst = dr["TCS_CGST"].ToString().Equals("") ? "0" : dr["TCS_CGST"].ToString();
                TCSsgst = dr["TCS_SGST"].ToString().Equals("") ? "0" : dr["TCS_SGST"].ToString();
                payable = dr["totaldec"].ToString().Equals("") ? "0" : dr["totaldec"].ToString();
                sp = dr["sys_sp"].ToString().Equals("") ? "0" : dr["sys_sp"].ToString();
                pmt_sp = dr["pmt_sp"].ToString().Equals("") ? "0" : dr["pmt_sp"].ToString();
                ligst = dr["ligst"].ToString().Equals("") ? "0" : dr["ligst"].ToString();
                higst = dr["higst"].ToString().Equals("") ? "0" : dr["higst"].ToString();
                lowhightpoint = dr["lowhighpt"].ToString().Equals("") ? "0" : dr["lowhighpt"].ToString();
                channelcommision = dr["chennelcommision"].ToString().Equals("") ? "0" : dr["chennelcommision"].ToString();
                tcsincgst = dr["tcsincgst"].ToString().Equals("") ? "0" : dr["tcsincgst"].ToString();
                tcsexcgst = dr["tcsexcgst"].ToString().Equals("") ? "0" : dr["tcsexcgst"].ToString();

                logistic = dr["logistic"].ToString().Equals("") ? "0" : dr["logistic"].ToString();
                penalty = dr["Vlpenelty"].ToString().Equals("") ? "0" : dr["Vlpenelty"].ToString();
                mis = dr["Mis"].ToString().Equals("") ? "0" : dr["Mis"].ToString();
                //state = dr["state"].ToString().Equals("") ? "0" : dr["state"].ToString();
                cp = dr["cp"].ToString().Equals("") ? "0" : dr["cp"].ToString();
                salesid = dr["salesid"].ToString().Equals("") ? "0" : dr["salesid"].ToString();
                count = dr["cunt"].ToString().Equals("") ? "0" : dr["cunt"].ToString();
                gateway = dr["gatewaycommission"].ToString().Equals("") ? "0" : dr["gatewaycommission"].ToString();
                mrp = dr["mrp"].ToString().Equals("") ? "0" : dr["mrp"].ToString();
                otherchrg = dr["otherchrg"].ToString().Equals("") ? "0" : dr["otherchrg"].ToString();

                Totaltcs = Convert.ToDecimal(tcsincgst) - Convert.ToDecimal(tcsexcgst);

                decimal amnt = ((Convert.ToDecimal(sp)) - ((Convert.ToDecimal(sp) * Convert.ToDecimal(ligst)) / 100));
                if (amnt <= Convert.ToDecimal(lowhightpoint))
                {
                    iper = Convert.ToDecimal(ligst);
                    cper = iper / 2;
                    sper = iper / 2;
                    
                }
                else
                {
                    iper = Convert.ToDecimal(higst);
                    cper = iper / 2;
                    sper = iper / 2;
                    
                }
                //if(state.ToString().Equals("27"))
                //{
                    
                //}
                //else
                //{

                //}
                if(salesid.Equals("29957520418")) 
                {

                }

                decimal finalsp = 0;
                if (count.ToString().Equals("1"))
                {
                    finalsp = Convert.ToDecimal(sp);
                    TotalGSt = Convert.ToDecimal(igst) + Convert.ToDecimal(sgst) + Convert.ToDecimal(cgst) + Convert.ToDecimal(TCSigst) + Convert.ToDecimal(TCSsgst) + Convert.ToDecimal(TCScgst);
                }
                else
                {
                    finalsp = Convert.ToDecimal(sp);
                    TotalGSt = Convert.ToDecimal(igst) + Convert.ToDecimal(sgst) + Convert.ToDecimal(cgst)+ Convert.ToDecimal(TCSigst) + Convert.ToDecimal(TCSsgst) + Convert.ToDecimal(TCScgst);
                }

                totalIGST = Convert.ToDecimal(igst) + Convert.ToDecimal(TCSigst);
                totalCGST = Convert.ToDecimal(cgst) + Convert.ToDecimal(TCScgst);
                totalSGST = Convert.ToDecimal(sgst) + Convert.ToDecimal(TCSsgst);
                if(count.ToString().Equals("1"))
                {
                    //TotalDeduction_inc = Math.Round(finalsp - TotalGSt - Convert.ToDecimal(channelcommision))- Convert.ToDecimal(logistic)) - Convert.ToDecimal(penalty)) - Convert.ToDecimal(gateway)) + Convert.ToDecimal(mis)), 2);
                    //Totaldeduction_exc = Math.Round(finalsp - Convert.ToDecimal(channelcommision)) - Convert.ToDecimal(logistic)) - Convert.ToDecimal(penalty)) - Convert.ToDecimal(gateway)) + Convert.ToDecimal(mis)), 2);
                    TotalDeduction_inc = Math.Round(Convert.ToDecimal(finalsp) - Convert.ToDecimal(payable) + Convert.ToDecimal(mis) - Convert.ToDecimal(otherchrg) - Totaltcs, 2);
                    Totaldeduction_exc = Math.Round(TotalDeduction_inc - TotalGSt, 2);
                }
                else
                {
                    TotalDeduction_inc = Math.Round(Convert.ToDecimal(payable) + Convert.ToDecimal(mis)-Convert.ToDecimal(otherchrg) - Totaltcs, 2);
                    Totaldeduction_exc = Math.Round(TotalDeduction_inc - TotalGSt, 2);
                }

                try
                {
                    calnet = Convert.ToDecimal(mrp) / Convert.ToDecimal(cp);
                }
                catch (Exception ex)
                {
                    calnet = 0;
                }

                Setamount_inc = Math.Round(finalsp - Math.Abs(TotalDeduction_inc) - Convert.ToDecimal(calnet),2);
                try
                {
                    Setamount_incper = Math.Round((Setamount_inc / finalsp) * 100, 2);
                }
                catch(Exception ex)
                {
                    Setamount_incper = 0;
                }

                setamount_exc = Math.Round(finalsp- Math.Abs(Totaldeduction_exc) - Convert.ToDecimal(calnet),2);
                try
                {
                    setamount_excper = Math.Round((setamount_exc / finalsp) * 100, 2);
                }
                catch (Exception ex)
                {
                    setamount_excper = 0;
                }
               
                netprofit =Math.Round(finalsp-Math.Abs(Convert.ToDecimal(channelcommision))-Math.Abs(Convert.ToDecimal(logistic))- calnet, 2);
                try
                {
                    netprofitper = Math.Round((netprofit / finalsp) * 100, 2);
                }
                catch (Exception ex)
                {
                    netprofitper = 0; 
                }
                

                DataRow row = dt.NewRow();
                row["Pmt Date"]=dr["pmt_date"];
                row["Sales Date"]=dr["sys_date"];
                row["Sales Month"] = dr["sys_Month"];
                row["Sales Year"] = dr["sys_year"];
                row["Channel(VL)"] = dr["Location"];
                row["Invoice #"] = dr["invoiceid"];
                row["Order id#"] = dr["salesid"];
                row["StockUp id#"] = dr["stockupId"];
                row["Payment Status"] = dr["payment_status"];
                row["Pmt Effecting Status"] = dr["pmt_effecting_status"];
                row["System Status"] = dr["System_status"];
                row["Lot#"] = dr["Lotno"];
                row["Brand"] = dr["brand"];
                row["Title"] = dr["Title"];
                row["Size"] = dr["Size1"];
                row["Article"] = dr["Articel"];
                row["Age Bucket(Lot)"] = dr["lotAge"];
                row["Age Bucket(Barcode)"] = dr["stockAge"];
                row["MRP"] = dr["mrp"];
                row["CP"] = calnet;
                row["Pmt Sales Price"] = dr["pmt_sp"];
                row["System Sales Price"] = dr["sys_sp"];
                row["Channel(VL) Commision"] = dr["chennelcommision"];
                row["Channel Gateway"] = dr["gatewaycommission"];
                row["Channel Logistics"] = dr["logistic"];
                row["VL Penalty"] = dr["Vlpenelty"];
                row["VL Misc"] = dr["Mis"];
                row["Total  IGST"] = totalIGST;
                row["Total SGST"] = totalSGST;
                row["Total CSGT"] = totalCGST;
               
                row["Payable Amount"] =Convert.ToDecimal(payable);
                row["Total TCS"] = Math.Round(Totaltcs, 2);
                row["Total Deduction(inc GST)"] = TotalDeduction_inc;
                row["Total Deduction(exl GST)"]= Totaldeduction_exc;
                row["Settled Amount(inc GST)"] = Setamount_inc;
                row["Settled %(inc GST)"] = Setamount_incper; ;
                row["Settled Amount(exl GST)"] = setamount_exc;
                row["Settled %(exl GST)"] = setamount_excper;
                row["Net profit"] = netprofit;
                row["Net Profit %"] = netprofitper;
                dt.Rows.Add(row);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Consoledate Report");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                //Response.ContentType = "application / vnd.ms-excel";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fname = virtualLocation.SelectedItem.Text +" Consoledate-Repo "+ DateTime.Now.ToString("dd-MM-yyyy-HH:mm:ss");
               

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
        catch(Exception ex)
        {
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }
    }
    protected void rpt_conso_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        int cunt = 0;
        try
        {
            object payable;
            object igst;
            object cgst;
            object sgst;
            object TCSigst;
            object TCScgst;
            object TCSsgst;
            object sp;
            object pmt_sp;
            object ligst;
            object higst;
            object cp;
            object channelcommision;
            object logistic;
            object mis;
            object penalty;
            object lowhightpoint;
            object state;
            object salesid;
            object count;
            object gateway;
            object tcsincgst;
            object tcsexcgst;
            object mrp;

            decimal TotalGSt;
            decimal TotalDeduction_inc;
            decimal Totaldeduction_exc;
            decimal Setamount_inc;
            decimal setamount_exc;
            decimal Setamount_incper;
            decimal setamount_excper;
            decimal netprofit;
            decimal netprofitper;

            decimal cper;
            decimal sper;
            decimal iper;
            decimal totalIGST;
            decimal totalSGST;
            decimal totalCGST;
            decimal Totaltcs;
            decimal calnet = 0;


                igst=((DataRowView)e.Item.DataItem)["IGST"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["IGST"].ToString();
                cgst = ((DataRowView)e.Item.DataItem)["CGST"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["CGST"].ToString();
                sgst = ((DataRowView)e.Item.DataItem)["SGST"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["SGST"].ToString();
                TCSigst = ((DataRowView)e.Item.DataItem)["TCS_IGST"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["TCS_IGST"].ToString();
                TCScgst = ((DataRowView)e.Item.DataItem)["TCS_CGST"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["TCS_CGST"].ToString();
                TCSsgst = ((DataRowView)e.Item.DataItem)["TCS_SGST"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["TCS_SGST"].ToString();
                payable = ((DataRowView)e.Item.DataItem)["totaldec"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["totaldec"].ToString();
                sp = ((DataRowView)e.Item.DataItem)["sys_sp"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["sys_sp"].ToString();
                pmt_sp = ((DataRowView)e.Item.DataItem)["pmt_sp"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["pmt_sp"].ToString();
                ligst = ((DataRowView)e.Item.DataItem)["ligst"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["ligst"].ToString();
                higst = ((DataRowView)e.Item.DataItem)["higst"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["higst"].ToString();
                lowhightpoint = ((DataRowView)e.Item.DataItem)["lowhighpt"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["lowhighpt"].ToString();
                channelcommision = ((DataRowView)e.Item.DataItem)["chennelcommision"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["chennelcommision"].ToString();
                tcsincgst = ((DataRowView)e.Item.DataItem)["tcsincgst"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["tcsincgst"].ToString();
                tcsexcgst = ((DataRowView)e.Item.DataItem)["tcsexcgst"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["tcsexcgst"].ToString();

                logistic = ((DataRowView)e.Item.DataItem)["logistic"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["logistic"].ToString();
                penalty = ((DataRowView)e.Item.DataItem)["Vlpenelty"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["Vlpenelty"].ToString();
                mis = ((DataRowView)e.Item.DataItem)["Mis"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["Mis"].ToString();
                //state = ((DataRowView)e.Item.DataItem)["state"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["state"].ToString();
                cp = ((DataRowView)e.Item.DataItem)["cp"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["cp"].ToString();
                salesid = ((DataRowView)e.Item.DataItem)["salesid"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["salesid"].ToString();
                count = ((DataRowView)e.Item.DataItem)["cunt"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["cunt"].ToString();
                gateway = ((DataRowView)e.Item.DataItem)["gatewaycommission"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["gatewaycommission"].ToString();
                mrp = ((DataRowView)e.Item.DataItem)["mrp"].ToString().Equals("") ? "0" : ((DataRowView)e.Item.DataItem)["mrp"].ToString();

                Totaltcs = Convert.ToDecimal(tcsincgst) - Convert.ToDecimal(tcsexcgst);

                decimal amnt = ((Convert.ToDecimal(sp)) - ((Convert.ToDecimal(sp) * Convert.ToDecimal(ligst)) / 100));
                if (amnt <= Convert.ToDecimal(lowhightpoint))
                {
                    iper = Convert.ToDecimal(ligst);
                    cper = iper / 2;
                    sper = iper / 2;
                    
                }
                else
                {
                    iper = Convert.ToDecimal(higst);
                    cper = iper / 2;
                    sper = iper / 2;
                    
                }
                //if(state.ToString().Equals("27"))
                //{
                    
                //}
                //else
                //{

                //}
                if(salesid.Equals("29957520418")) 
                {

                }

                decimal finalsp = 0;
                if (count.ToString().Equals("1"))
                {
                    finalsp = Convert.ToDecimal(sp);
                    TotalGSt = Convert.ToDecimal(igst) + Convert.ToDecimal(sgst) + Convert.ToDecimal(cgst) + Convert.ToDecimal(TCSigst) + Convert.ToDecimal(TCSsgst) + Convert.ToDecimal(TCScgst);
                }
                else
                {
                    finalsp = Convert.ToDecimal(sp);
                    TotalGSt = Convert.ToDecimal(igst) + Convert.ToDecimal(sgst) + Convert.ToDecimal(cgst)+ Convert.ToDecimal(TCSigst) + Convert.ToDecimal(TCSsgst) + Convert.ToDecimal(TCScgst);
                }

                totalIGST = Convert.ToDecimal(igst) + Convert.ToDecimal(TCSigst);
                totalCGST = Convert.ToDecimal(cgst) + Convert.ToDecimal(TCScgst);
                totalSGST = Convert.ToDecimal(sgst) + Convert.ToDecimal(TCSsgst);
                if(count.ToString().Equals("1"))
                {
                    //TotalDeduction_inc = Math.Round(finalsp - TotalGSt - Convert.ToDecimal(channelcommision))- Convert.ToDecimal(logistic)) - Convert.ToDecimal(penalty)) - Convert.ToDecimal(gateway)) + Convert.ToDecimal(mis)), 2);
                    //Totaldeduction_exc = Math.Round(finalsp - Convert.ToDecimal(channelcommision)) - Convert.ToDecimal(logistic)) - Convert.ToDecimal(penalty)) - Convert.ToDecimal(gateway)) + Convert.ToDecimal(mis)), 2);
                    TotalDeduction_inc = Math.Round(Convert.ToDecimal(finalsp) - Convert.ToDecimal(payable) + Convert.ToDecimal(mis) - Totaltcs, 2);
                    Totaldeduction_exc = Math.Round(TotalDeduction_inc - TotalGSt, 2);
                }
                else
                {
                    TotalDeduction_inc = Math.Round(Convert.ToDecimal(payable) + Convert.ToDecimal(mis)- Totaltcs, 2);
                    Totaldeduction_exc = Math.Round(TotalDeduction_inc - TotalGSt, 2);
                }
            try
            {
                calnet = Convert.ToDecimal(mrp) / Convert.ToDecimal(cp);
            }
            catch (Exception ex)
            {
                calnet = 0;
            }


            Setamount_inc = Math.Round(finalsp - Math.Abs(TotalDeduction_inc) - Convert.ToDecimal(calnet),2);
                try
                {
                    Setamount_incper = Math.Round((Math.Abs(Setamount_inc) / finalsp) * 100, 2);
                }
                catch(Exception ex)
                {
                    Setamount_incper = 0;
                }

                setamount_exc = Math.Round(finalsp- Totaldeduction_exc- Convert.ToDecimal(calnet),2);
                try
                {
                    setamount_excper = Math.Round((setamount_exc / finalsp) * 100, 2);
                }
                catch (Exception ex)
                {
                    setamount_excper = 0;
                }
           


                netprofit =Math.Round(finalsp-Math.Abs(Convert.ToDecimal(channelcommision))- Math.Abs(Convert.ToDecimal(logistic))- calnet, 2);
                try
                {
                    netprofitper = Math.Round((netprofit / finalsp) * 100, 2);
                }
                catch (Exception ex)
                {
                    netprofitper = 0; 
                }
                
            Label lbltotdecinc = e.Item.FindControl("lbltotdecinc") as Label;
            Label lbltotdecexc = e.Item.FindControl("lbltotdecexc") as Label;
           
            Label setamtinc = e.Item.FindControl("setamtinc") as Label;
            Label setperinc = e.Item.FindControl("setperinc") as Label;
            Label setamtexc = e.Item.FindControl("setamtexc") as Label;
            Label setperexc = e.Item.FindControl("setperexc") as Label;
            Label netpro = e.Item.FindControl("netpro") as Label;
            Label lblnetper = e.Item.FindControl("lblnetper") as Label;
            Label cplbl = e.Item.FindControl("cplbl") as Label;

            lbltotdecinc.Text = TotalDeduction_inc.ToString();
            lbltotdecexc.Text = Totaldeduction_exc.ToString();
            setamtinc.Text = Setamount_inc.ToString();
            setperinc.Text = Setamount_incper.ToString();
            setamtexc.Text = setamount_exc.ToString();
            setperexc.Text = setamount_excper.ToString();
            netpro.Text = netprofit.ToString();
            lblnetper.Text = netprofitper.ToString();
            cplbl.Text = Convert.ToString(calnet);
            cunt++;

        }
        catch (Exception ex)
        {
            
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        try
        {
            //BindData();
            maindev.Visible = true;
        }
        catch(Exception ex)
        {
            RecordExceptionCls obj = new RecordExceptionCls();
            obj.recordException(ex);
        }
    }
}