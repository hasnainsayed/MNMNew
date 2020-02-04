using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class barcodeCurrentStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //BindData();
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

    protected void getStatus_Click(object sender, EventArgs e)
    {
        try
        {
            statusControlCls obj = new statusControlCls();
            //DataTable dt = obj.getCurrentStatusOfBarcode(searchField.Text);
            DataTable dt = obj.getcommonDetailsOfBarcode(searchField.Text);
             BarcodeNo.Text = string.Empty;
             SystemDate.Text = string.Empty;
             Status.Text = string.Empty;
             StyleCode.Text = string.Empty;
             SKU.Text = string.Empty;
             mrp.Text = string.Empty;
             LotNo.Text = string.Empty;
             Brand.Text = string.Empty;
             Titles.Text = string.Empty;
             Size.Text = string.Empty;
             ItemCategory.Text = string.Empty;
             Cat02.Text = string.Empty;
             Cat03.Text = string.Empty;
             Cat04.Text = string.Empty;
             Cat05.Text = string.Empty;
             RackBarcode.Text = string.Empty;
             RackDate.Text = string.Empty;
             printed.Text = string.Empty;
             Colour.Text = string.Empty;
             FSN_No.Text = string.Empty;
             Article_No.Text = string.Empty;
             Search_Image_URL.Text = string.Empty;
             EAN.Text = string.Empty;
             Model_Name.Text = string.Empty;
             Description.Text = string.Empty;
             Comments.Text = string.Empty;
             Colour1.Text = string.Empty;
             Gender.Text = string.Empty;
             Upper_Material.Text = string.Empty;
             Sole_Material.Text = string.Empty;
             Closure.Text = string.Empty;
             Occasion.Text = string.Empty;
             Pattern.Text = string.Empty;
             Ankle_Height.Text = string.Empty;
             Toe_Shape.Text = string.Empty;
             Heel_Shape.Text = string.Empty;
             Heel_Height.Text = string.Empty;
             Back_Closure.Text = string.Empty;
             image1.Text = string.Empty;
             image2.Text = string.Empty;
             image3.Text = string.Empty;
             image4.Text = string.Empty;
             image5.Text = string.Empty;
             image6.Text = string.Empty;
             Barcode_Maker.Text = string.Empty;
             listDets.Text = string.Empty;
             Physical_Location.Text = string.Empty;
             stockAge.Text = string.Empty;
             lotAge.Text = string.Empty;
            lblEAN.Text = string.Empty;

             if (!dt.Rows.Count.Equals(0))
             {
                
                 BarcodeNo.Text = searchField.Text;
                 SystemDate.Text = dt.Rows[0]["SystemDate"].ToString();
                 //Status.Text = dt.Rows[0]["Status"].ToString();
                 StyleCode.Text = dt.Rows[0]["StyleCode"].ToString();
                 SKU.Text = dt.Rows[0]["SKU"].ToString();
                 mrp.Text = dt.Rows[0]["mrp"].ToString();
                 LotNo.Text = dt.Rows[0]["BagDescription"].ToString();
                 Brand.Text = dt.Rows[0]["C1Name"].ToString();
                 Titles.Text = dt.Rows[0]["Title"].ToString();
                 Size.Text = dt.Rows[0]["Size1"].ToString();
                 ItemCategory.Text = dt.Rows[0]["ItemCategory"].ToString();
                 Cat02.Text = dt.Rows[0]["C2Name"].ToString();
                 Cat03.Text = dt.Rows[0]["C3Name"].ToString();
                 Cat04.Text = dt.Rows[0]["C4Name"].ToString();
                 Cat05.Text = dt.Rows[0]["C5Name"].ToString();
                 RackBarcode.Text = dt.Rows[0]["RackBarcode"].ToString();
                 RackDate.Text = dt.Rows[0]["RackDate"].ToString();
                 printed.Text = dt.Rows[0]["printed"].ToString();

                 Colour.Text = dt.Rows[0]["Control1"].ToString();
                 FSN_No.Text = dt.Rows[0]["Control2"].ToString();
                 Article_No.Text = dt.Rows[0]["Control3"].ToString();
                 Search_Image_URL.Text = dt.Rows[0]["Control4"].ToString();
                 EAN.Text = dt.Rows[0]["Control5"].ToString();
                 Model_Name.Text = dt.Rows[0]["Control6"].ToString();
                 Description.Text = dt.Rows[0]["Control7"].ToString();
                 Comments.Text = dt.Rows[0]["Control8"].ToString();
                 Colour1.Text = dt.Rows[0]["col2Name"].ToString();
                 Gender.Text = dt.Rows[0]["col3Name"].ToString();
                 Upper_Material.Text = dt.Rows[0]["col4Name"].ToString();
                 Sole_Material.Text = dt.Rows[0]["col5Name"].ToString();
                 Closure.Text = dt.Rows[0]["C6Name"].ToString();
                 Occasion.Text = dt.Rows[0]["C7Name"].ToString();
                 Pattern.Text = dt.Rows[0]["C8Name"].ToString();
                 Ankle_Height.Text = dt.Rows[0]["C9Name"].ToString();
                 Toe_Shape.Text = dt.Rows[0]["C10Name"].ToString();
                 Heel_Shape.Text = dt.Rows[0]["C11Name"].ToString();
                 Heel_Height.Text = dt.Rows[0]["C12Name"].ToString();
                 Back_Closure.Text = dt.Rows[0]["C13Name"].ToString();
                 image1.Text = dt.Rows[0]["image1"].ToString();
                 image2.Text = dt.Rows[0]["image2"].ToString();
                 image3.Text = dt.Rows[0]["image3"].ToString();
                 image4.Text = dt.Rows[0]["image4"].ToString();
                 image5.Text = dt.Rows[0]["image5"].ToString();
                 image6.Text = dt.Rows[0]["image6"].ToString();
                 Barcode_Maker.Text = dt.Rows[0]["username"].ToString();                 
                 listDets.Text = dt.Rows[0]["listDets"].ToString();
                 Physical_Location.Text = dt.Rows[0]["Location"].ToString();
                 stockAge.Text = dt.Rows[0]["stockAge"].ToString();
                 lotAge.Text = dt.Rows[0]["lotAge"].ToString();
                lblEAN.Text = dt.Rows[0]["EAN"].ToString();
                initialStatus.Text = dt.Rows[0]["initialStatus"].ToString();
                DataTable salesdt = new DataTable();
                DataTable cancledt = new DataTable();
                salesdt = obj.getSalesFullDetails(searchField.Text);

                DataView dtview = new DataView(salesdt);
                string sortstring = "salesDateTime ASC"; // sorting in descending manner 
                dtview.Sort = sortstring;
                DataTable dtsort = dtview.ToTable();

                reportCls r = new reportCls();
                DataSet ds = r.barcodeStatusHistory(searchField.Text);
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("User");
                dt1.Columns.Add("Status");
                dt1.Columns.Add("DateTime", typeof(DateTime));

                DataTable createdby = ds.Tables["createdby"];
                dt1.Rows.Add(createdby.Rows[0]["username"], createdby.Rows[0]["initialStatus"], createdby.Rows[0]["SystemDate"]);

                DataTable locTable = ds.Tables["locTable"];
                foreach (DataRow row in locTable.Rows)
                {
                    if (!row["salesUser"].ToString().Equals(""))
                    {
                        dt1.Rows.Add(row["salesUser"].ToString(), "SOLD", row["salesDateTime"]);
                    }
                    if (!row["dispatchUser"].ToString().Equals(""))
                    {
                        dt1.Rows.Add(row["dispatchUser"].ToString(), "DISPATCHED", row["dispatchtimestamp"]);
                    }
                    if (!row["retUser"].ToString().Equals(""))
                    {
                        dt1.Rows.Add(row["retUser"].ToString(), "RETURNED", row["returntimestamp"]);
                        dt1.Rows.Add(row["retUser"].ToString(), row["changeStatus"].ToString(), row["returntimestamp"]);
                    }


                }

                DataTable cancle = ds.Tables["cancle"];
                foreach (DataRow row in cancle.Rows)
                {
                    if (!row["salesUser"].ToString().Equals(""))
                    {
                        dt1.Rows.Add(row["salesUser"].ToString(), "SOLD", row["salesDateTime"]);
                    }
                    if (!row["cancelUser"].ToString().Equals(""))
                    {
                        dt1.Rows.Add(row["cancelUser"].ToString(), "CANCELLED", row["cancelTimeStamp"]);
                        dt1.Rows.Add(row["cancelUser"].ToString(), row["changeStatus"].ToString(), row["cancelTimeStamp"]);
                    }

                }

                DataTable statusChange = ds.Tables["statusChange"];
                foreach (DataRow row in statusChange.Rows)
                {
                    if (!row["username"].ToString().Equals(""))
                    {
                        dt1.Rows.Add(row["username"].ToString(), row["barcodenewstus"].ToString(), row["datetime"]);
                    }

                }
                DataView dtview1 = new DataView(dt1);
                string sortstring1 = "DateTime ASC"; // sorting in descending manner 
                dtview1.Sort = sortstring1;
                DataTable dtsort1 = dtview1.ToTable();
                if (!dtsort1.Rows.Count.Equals(0))
                {
                    DataRow lastRow = dtsort1.Rows[dtsort1.Rows.Count - 1];
                    Status.Text = lastRow["Status"].ToString();
                }

                /*if (!dtsort.Rows.Count.Equals(0))
                {
                    DataRow lastRow = dtsort.Rows[dtsort.Rows.Count - 1];                    
                    if(lastRow["status"].ToString().Equals("RETURN"))
                    {
                        Status.Text = lastRow["changestatus"].ToString();
                    }
                    else
                    {
                        Status.Text = lastRow["status"].ToString();
                    }
                }
                else
                {
                    Status.Text = dt.Rows[0]["displayStatus"].ToString();
                }*/
                /*cancledt = obj.getCancleSalesFullDets(searchField.Text);*/




                sales_rpt.DataSource = dtsort;
                sales_rpt.DataBind();
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}