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

public partial class notListed : System.Web.UI.Page
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
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void BindData()
    {
        try
        {
            locationCls obj = new locationCls();
            DataTable dt = obj.getVirtualLocation("2");
            virtualLocation.DataSource = dt;
            virtualLocation.DataBind();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void searchSKU_Click(object sender, EventArgs e)
    {
        try {
            
            reportCls r = new reportCls();
            DataTable st = r.getNotListedSKU(virtualLocation.SelectedValue);
            rtp_List.DataSource = st;
            rtp_List.DataBind();
            showData.Visible = true;
            
            //ViewState["getNotListedSKU"] = st;
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }
    
    public DataTable SetColumnsOrder(DataTable table, params String[] columnNames)
    {
        try
        {
            int columnIndex = 0;
            foreach (var columnName in columnNames)
            {
                table.Columns[columnName].SetOrdinal(columnIndex);
                columnIndex++;
                
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        return table;
    }

    protected void exportSKUNotListed_Click(object sender, EventArgs e)
    {
        try
        {
            styleCls obj = new styleCls();
            //DataTable loc = obj.getTableColwithID("Location","LocationID",Convert.ToInt32(virtualLocation.SelectedValue), "Location");
            //DataTable dt = (DataTable)ViewState["getNotListedSKU"];
            reportCls r = new reportCls();
            DataTable dt = r.getNotListedSKU(virtualLocation.SelectedValue);
            /*foreach (ListItem li in exportCheckBox.Items)
            {
                if (li.Selected.Equals(false))
                {
                    dt.Columns.Remove(li.Value.ToString());
                }
            }*/
            DataTable st = dt.Copy();

            if (virtualLocation.SelectedValue.Equals("7")) // snapdeal
            {
                //add extra columns
                st.Columns.AddRange(new DataColumn[]
                {
                new DataColumn("Create Group"),
                new DataColumn("Product Name"),
                new DataColumn("Weight (g)"),
                new DataColumn("Length (cm)"),
                new DataColumn("Height (cm)"),
                new DataColumn("Width (cm)"),
                new DataColumn("EAN"),
                new DataColumn("UPC"),
                new DataColumn("Running Type"),
                new DataColumn("Shoe Weight"),
                new DataColumn("Size/Dimensions of the Commodity"),
                new DataColumn("SizeChartID"),
                new DataColumn("Impoerter Name"),
                new DataColumn("Manufacturer's Name and Address"),
                new DataColumn("Consumer complaints-Name/Address/Phone/Email"),
                new DataColumn("Country of Origin or Manufacturer"),
                new DataColumn("Selling Price"),
                new DataColumn("Wooden Packaging"),
                new DataColumn("Inventory"),
                new DataColumn("Shipping Time in Days"),
                new DataColumn("Error Check"),
                new DataColumn("Image7"),
                new DataColumn("Image8"),
                new DataColumn("Image9"),
                new DataColumn("Image10"),
                new DataColumn("Image11"),
                new DataColumn("Image12"),
                });
                // add default value 
                st.Columns["Weight (g)"].DefaultValue = "250";
                st.Columns["Length (cm)"].DefaultValue = "20";
                st.Columns["Height (cm)"].DefaultValue = "16";
                st.Columns["Width (cm)"].DefaultValue = "18";
                st.Columns["Shoe Weight"].DefaultValue = "Light Weight";
                st.Columns["Wooden Packaging"].DefaultValue = "No";
                st.Columns["Inventory"].DefaultValue = "0";
                st.Columns["Shipping Time in Days"].DefaultValue = "2";
                st.Columns["Error Check"].DefaultValue = "OK";

                // remove extra db columns
                var toRemove = new string[] { "Control1", "Control2", "Control3", "Control4", "Control5", "Control6", "Control8", "UKSize", "ItemCategory","C3Name", "C4Name", "C5Name", "C6Name", "C7Name", "C8Name", "C9Name", "C10Name", "C11Name", "C12Name", "C13Name","Title","Cat2Name","Cat3Name"};
                foreach(string colName in toRemove)
                {
                    st.Columns.Remove(colName);
                }
                // rename db columns according to file 
                st.Columns["C1Name"].ColumnName = "Brand";
                st.Columns["sku"].ColumnName = "SKU Code";
                st.Columns["Control7"].ColumnName = "Description";
                st.Columns["C2Name"].ColumnName = "Color";
                st.Columns["mrp"].ColumnName = "MRP";
                                

                //arrange according to file format
                st = SetColumnsOrder(st , new string[] { "Create Group", "Brand", "SKU Code", "Product Name","Description", "Weight (g)", "Length (cm)", "Height (cm)", "Width (cm)", "EAN",
                "UPC","Color","Running Type","Size","Shoe Weight",
                "Size/Dimensions of the Commodity","SizeChartID","Impoerter Name","Manufacturer's Name and Address","Consumer complaints-Name/Address/Phone/Email",
                "Country of Origin or Manufacturer","MRP","Selling Price","Wooden Packaging","Inventory","Shipping Time in Days","Error Check","image1","image2","image3",
                    "image4","image5","image6","Image7","Image8","Image9","Image10","Image11","Image12"});
                
                

            }
            else if (virtualLocation.SelectedValue.Equals("10")) // fynd
            {
                //add extra columns
                st.Columns.AddRange(new DataColumn[]
                {
                new DataColumn("item_code"),
                new DataColumn("age(for kids products)"),
                new DataColumn("hsn_code"),
                new DataColumn("collection"),
                new DataColumn("season"),
                new DataColumn("sport_division"),
                new DataColumn("dimensions"),
                new DataColumn("capacity"),
                new DataColumn("package_contents"),
                new DataColumn("warranty"),
                new DataColumn("price_effective"),
                });

                // add default value 
                st.Columns["hsn_code"].DefaultValue = "64059000";
                st.Columns["dimensions"].DefaultValue = "11*6*6";

                // remove extra db columns
                var toRemove1 = new string[] { "UKSize", "Control1", "Control2", "Control3", "Control4", "Control5", "Control8", "C5Name","C8Name", "C9Name", "C11Name", "C12Name", "C13Name","image6","Cat3Name", "sp" };
                foreach (string colName in toRemove1)
                {
                    st.Columns.Remove(colName);
                }
                // rename db columns according to file 
                st.Columns["C1Name"].ColumnName = "brand";
                st.Columns["C3Name"].ColumnName = "gender";
                st.Columns["ItemCategory"].ColumnName = "category";
                st.Columns["sku"].ColumnName = "sku_code";
                st.Columns["cat2Name"].ColumnName = "subcategory";
                st.Columns["C2Name"].ColumnName = "colour";
                st.Columns["C4Name"].ColumnName = "material";
                st.Columns["C6Name"].ColumnName = "closure_type";
                st.Columns["C7Name"].ColumnName = "occassion";
                st.Columns["C10Name"].ColumnName = "toe_type";
                st.Columns["Control6"].ColumnName = "model_info";
                st.Columns["Control7"].ColumnName = "description";
                st.Columns["mrp"].ColumnName = "price_marked";

                //arrange according to file format
                st = SetColumnsOrder(st, new string[] { "item_code", "sku_code", "Size", "age(for kids products)","hsn_code",
                    "brand", "collection", "season", "gender", "category",
                "subcategory","colour","material","sport_division","occassion",
                "model_info","dimensions","capacity","description","Title",
                "package_contents","warranty","closure_type","toe_type","image1","image2","image3",
                    "image4","image5","price_marked","price_effective"});

            }
            else if (virtualLocation.SelectedValue.Equals("8")) // paytm
            {
                //add extra columns
                st.Columns.AddRange(new DataColumn[]
                {
               
                new DataColumn("Warranty Type"),
                new DataColumn("Warranty Period"),
                new DataColumn("Product Weight"),
                new DataColumn("Product Weight Unit"),
                new DataColumn("Calculated Product Weight (In Grams)"),
                new DataColumn("Packaging Length"),
                new DataColumn("Packaging Breadth"),
                new DataColumn("Packaging Height"),
                new DataColumn("Packaging Unit"),
                new DataColumn("Packaging Dimensions (LxBxH)"),
                new DataColumn("Calculated Volumetric Weight (In Kg)"),
                new DataColumn("Volumetric Weight Range (In Kg)"),
                new DataColumn("Season"),
                new DataColumn("Set Contents"),
                new DataColumn("Key Features 1"),
                new DataColumn("Key Features 2"),
                new DataColumn("Key Features 3"),
                new DataColumn("Key Features 4"),
                new DataColumn("EAN/UPC"),
                new DataColumn("International Product"),
                new DataColumn("Complex Group"),
                new DataColumn("Complex Dimension"),
                
                });

                // add default value 
                st.Columns["Warranty Type"].DefaultValue = "Manufacturer";
                st.Columns["Warranty Period"].DefaultValue = "1 Month";
                st.Columns["Calculated Product Weight (In Grams)"].DefaultValue = "450000";
                st.Columns["Packaging Length"].DefaultValue = "11";
                st.Columns["Packaging Breadth"].DefaultValue = "6";
                st.Columns["Packaging Height"].DefaultValue = "4";
                st.Columns["Packaging Unit"].DefaultValue = "Cm";
                st.Columns["Packaging Dimensions (LxBxH)"].DefaultValue = "4.33x2.36x1.58";
                st.Columns["Calculated Volumetric Weight (In Kg)"].DefaultValue = "0.05 Kg";

                // remove extra db columns
                var toRemove3 = new string[] { "Size", "Control1", "Control2", "Control4", "Control5", "Control8", "C11Name", "C12Name", "C13Name","ItemCategory"};
                foreach (string colName in toRemove3)
                {
                    st.Columns.Remove(colName);
                }

                // rename db columns according to file 
                st.Columns["C1Name"].ColumnName = "Brand";
                st.Columns["sku"].ColumnName = "Merchant SKU";
                st.Columns["title"].ColumnName = "Product Name";
                st.Columns["cat2Name"].ColumnName = "Sub Category";
                st.Columns["cat3Name"].ColumnName = "Type";
                st.Columns["Control7"].ColumnName = "Description";
                st.Columns["mrp"].ColumnName = "Maximum Retail Price";
                st.Columns["C2Name"].ColumnName = "Color";
                st.Columns["C4Name"].ColumnName = "Upper Material";
                st.Columns["C9Name"].ColumnName = "Ankle Height";
                st.Columns["C10Name"].ColumnName = "Toe Shape";
                st.Columns["Control3"].ColumnName = "Style Code";
                st.Columns["Control6"].ColumnName = "Style Name";
                st.Columns["image1"].ColumnName = "Main Image Link/ Folder Name";
                st.Columns["image2"].ColumnName = "Other Image 1";
                st.Columns["image3"].ColumnName = "Other Image 2";
                st.Columns["image4"].ColumnName = "Other Image 3";
                st.Columns["image5"].ColumnName = "Other Image 4";
                st.Columns["image6"].ColumnName = "Other Image 5";
                st.Columns["C5Name"].ColumnName = "Sole Material";
                st.Columns["C6Name"].ColumnName = "Fastening";
                st.Columns["C8Name"].ColumnName = "Pattern";
                st.Columns["C3Name"].ColumnName = "Gender";
                st.Columns["C7Name"].ColumnName = "Occassion";
                st.Columns["sp"].ColumnName = "Selling Price";
                st.Columns["UKSize"].ColumnName = "Size";
                                
                //arrange according to file format
                st = SetColumnsOrder(st, new string[] { "Product Name", "Merchant SKU", "Sub Category", "Type","Brand",
                    "Maximum Retail Price", "Selling Price", "Size", "Color", "Upper Material",
                "Ankle Height","Toe Shape","Style Code","Style Name","Warranty Type",
                "Warranty Period","Product Weight","Product Weight Unit","Calculated Product Weight (In Grams)","Packaging Length",
                "Packaging Breadth","Packaging Height","Packaging Unit","Packaging Dimensions (LxBxH)","Calculated Volumetric Weight (In Kg)",
                    "Volumetric Weight Range (In Kg)","Main Image Link/ Folder Name","Other Image 1","Other Image 2",
                    "Other Image 3","Other Image 4","Other Image 5","Sole Material","Fastening","Pattern","Season","Occassion","Set Contents","Description"
                ,"Key Features 1","Key Features 2","Key Features 3","Key Features 4","EAN/UPC","International Product","Complex Group","Complex Dimension","Gender"});
            }
            
            
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(st, "SKU");
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    //Response.ContentType = "application / vnd.ms-excel";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    string fname = "SKU_" + virtualLocation.SelectedItem.Text + "_" + DateTime.Now.ToString("MM/dd/yyyy");

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
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }
        
}