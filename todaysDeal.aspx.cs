using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net;
using System.Text;
using ClosedXML.Excel;
public partial class todaysDeal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                styleCls obj = new styleCls();
                DataTable dt = obj.getLatestDealsTable("dealProduct");
                rpt_Style.DataSource = dt;
                rpt_Style.DataBind();
                totalBarcodes.Text = dt.Rows.Count.ToString();
                //bindStyles();
                if (Session["dealProductResult"] != null)
                {
                    divUpdAlert.InnerText = Session["dealProductResult"].ToString();
                    divUpdAlert.Visible = true;
                    Session.Remove("dealProductResult");
                }
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    private void bindStyles()
    {
        try
        {
            DataTable dtProgLang = new DataTable();
            dtProgLang.Columns.Add("styleId");
            foreach (RepeaterItem itemEquipment in rpt_Style.Items)
            {

                DropDownList style_drop = (DropDownList)itemEquipment.FindControl("style_drop");
                dtProgLang.Rows.Add(style_drop.SelectedValue); // Add Data 

            }

            dtProgLang.Rows.Add("-1");

            rpt_Style.DataSource = dtProgLang;
            rpt_Style.DataBind();



        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtProgLang = new DataTable();
            dtProgLang.Columns.Add("dropId");
            dtProgLang.Columns.Add("subDropId");
            foreach (RepeaterItem itemEquipment in rpt_Style.Items)
            {
                Label style_drop = (Label)itemEquipment.FindControl("StyleID");
                dtProgLang.Rows.Add(style_drop.Text, "-1"); // Add Data 
            }
            storedProcedureCls obj = new storedProcedureCls();
            string result = obj.addLatestProducts(dtProgLang,"dealProduct");
            Session["dealProductResult"] = result;
            Response.Redirect("todaysDeal.aspx", true);
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void rpt_Style_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            string styleId = ((DataRowView)e.Item.DataItem)["styleId"].ToString();

            DropDownList style_drop = e.Item.FindControl("style_drop") as DropDownList;
            styleCls obj = new styleCls();
            DataTable dt = obj.getTable("ItemStyle");
            style_drop.DataSource = dt;
            style_drop.DataBind();
            style_drop.Items.Insert(0, new ListItem("---- Select ----", "-1"));
            style_drop.SelectedValue = styleId;

        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void dropdownAdd_Click(object sender, EventArgs e)
    {
        try
        {
            bindStyles();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void addCode_Click(object sender, EventArgs e)
    {
        try
        {
            //check if code exist
            storedProcedureCls obj = new storedProcedureCls();
            DataTable dt = obj.getTableWithCondition("ItemStyle", "StyleCode", styleCode.Text, "StyleCode", "desc");
            if (!dt.Rows.Count.Equals(0))
            {
                DataTable dtProgLang = new DataTable();
                dtProgLang.Columns.Add("StyleId");
                dtProgLang.Columns.Add("StyleCode");
                dtProgLang.Columns.Add("Title");
                foreach (RepeaterItem itemEquipment in rpt_Style.Items)
                {

                    Label StyleId = (Label)itemEquipment.FindControl("StyleId");
                    Label StyleCode = (Label)itemEquipment.FindControl("StyleCode");
                    Label Title = (Label)itemEquipment.FindControl("Title");
                    dtProgLang.Rows.Add(StyleId.Text, StyleCode.Text, Title.Text); // Add Data 

                }
                String searchBarcode = styleCode.Text;
                bool contains = dtProgLang.AsEnumerable().Any(row => searchBarcode == row.Field<String>("StyleCode"));
                if (contains.Equals(false))
                {
                    dtProgLang.Rows.Add(dt.Rows[0]["StyleId"].ToString(), dt.Rows[0]["StyleCode"].ToString(), dt.Rows[0]["Title"].ToString());

                    rpt_Style.DataSource = dtProgLang;
                    rpt_Style.DataBind();

                    totalBarcodes.Text = dtProgLang.Rows.Count.ToString();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Duplicate Style Code');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Invalid Style Code');", true);
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void uploadExcel_Click(object sender, EventArgs e)
    {
        try
        {
            storedProcedureCls obj = new storedProcedureCls();
            if (FileUpload1.HasFile)
            {
                //Create a new DataTable.
                DataTable dt = new DataTable();
                dt.Columns.Add("StyleId");
                dt.Columns.Add("StyleCode");
                dt.Columns.Add("Title");
                dt.Columns.Add("Status");



                foreach (RepeaterItem itemEquipment in rpt_Style.Items)
                {

                    Label StyleId = (Label)itemEquipment.FindControl("StyleId");
                    Label StyleCode = (Label)itemEquipment.FindControl("StyleCode");
                    Label Title = (Label)itemEquipment.FindControl("Title");
                    dt.Rows.Add(StyleId.Text, StyleCode.Text, Title.Text); // Add Data 

                }

                bool checkDuplicate = false;
                string dupBarcodes = string.Empty;
                using (XLWorkbook workBook = new XLWorkbook(FileUpload1.PostedFile.InputStream))
                {
                    //Read the first Sheet from Excel file.
                    IXLWorksheet workSheet = workBook.Worksheet(1);



                    //Loop through the Worksheet rows.
                    bool firstRow = true;

                    foreach (IXLRow row in workSheet.Rows())
                    {
                        //Use the first row to add columns to DataTable.
                        if (firstRow)
                        {
                            // dont add frst row ie header
                            /*foreach (IXLCell cell in row.Cells())
                            {
                                dt.Columns.Add(cell.Value.ToString());
                            }*/
                            firstRow = false;
                        }
                        else
                        {
                            //Add rows to DataTable.
                            dt.Rows.Add();
                            int i = 0;
                            foreach (IXLCell cell in row.Cells())
                            {

                                String searchStyle = cell.Value.ToString();
                                bool contains = dt.AsEnumerable().Any(rows => cell.Value.ToString() == rows.Field<String>("StyleCode"));

                                Boolean? check = true;
                                DataTable styleDt = obj.getTableWithCondition("ItemStyle", "StyleCode", searchStyle, "StyleID", "desc");



                                if (contains.Equals(false) && !(styleDt.Rows.Count.Equals(0)))
                                {
                                    dt.Rows[dt.Rows.Count - 1][0] = styleDt.Rows[0]["StyleId"].ToString();
                                    dt.Rows[dt.Rows.Count - 1][1] = styleDt.Rows[0]["StyleCode"].ToString();
                                    dt.Rows[dt.Rows.Count - 1][2] = styleDt.Rows[0]["Title"].ToString();
                                    dt.Rows[dt.Rows.Count - 1][3] = "";


                                }
                                else
                                {
                                    if (contains.Equals(true))
                                    {
                                        dt.Rows[dt.Rows.Count - 1][0] = "";
                                        dt.Rows[dt.Rows.Count - 1][1] = cell.Value.ToString();
                                        dt.Rows[dt.Rows.Count - 1][2] = "";
                                        dt.Rows[dt.Rows.Count - 1][3] = "Duplicate";
                                    }
                                    else if (check.Equals(null))
                                    {
                                        dt.Rows[dt.Rows.Count - 1][0] = "";
                                        dt.Rows[dt.Rows.Count - 1][1] = cell.Value.ToString();
                                        dt.Rows[dt.Rows.Count - 1][2] = "";
                                        dt.Rows[dt.Rows.Count - 1][3] = "Exception Occured";
                                    }
                                    else if (styleDt.Rows.Count.Equals(0))
                                    {
                                        dt.Rows[dt.Rows.Count - 1][0] = "";
                                        dt.Rows[dt.Rows.Count - 1][1] = cell.Value.ToString();
                                        dt.Rows[dt.Rows.Count - 1][2] = "";
                                        dt.Rows[dt.Rows.Count - 1][3] = "Style Code does not Exist";
                                    }
                                    checkDuplicate = true;
                                    //dupBarcodes += ','+ cell.Value.ToString();
                                    //break;
                                }
                                //dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();

                                //cell.Value.ToString();
                                i++;
                            }
                        }


                    }
                }

                if (checkDuplicate.Equals(false))
                {
                    rpt_Style.DataSource = dt;
                    rpt_Style.DataBind();

                    totalBarcodes.Text = dt.Rows.Count.ToString();
                }
                else
                {

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, "latestProduct");
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        //Response.ContentType = "application / vnd.ms-excel";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        string fname = "latestProduct" + DateTime.Now.ToString("MM/dd/yyyy_HH:mm:ss");

                        Response.AddHeader("content-disposition", "attachment;filename=" + fname + ".xlsx");
                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "duplicateValues", "alert('Issue in Uploading Excel.Please Check Downloaded Excel');", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "selectFile", "alert('Please Select Excel File');", true);
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }

    protected void delStyle_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = ((LinkButton)(sender));
            RepeaterItem rp1 = ((RepeaterItem)(btn.NamingContainer));
            int index = rp1.ItemIndex;
            DataTable dtProgLang = new DataTable();
            dtProgLang.Columns.Add("StyleId");
            dtProgLang.Columns.Add("StyleCode");
            dtProgLang.Columns.Add("Title");
            foreach (RepeaterItem itemEquipment in rpt_Style.Items)
            {

                Label StyleId = (Label)itemEquipment.FindControl("StyleId");
                Label StyleCode = (Label)itemEquipment.FindControl("StyleCode");
                Label Title = (Label)itemEquipment.FindControl("Title");
                if (!itemEquipment.ItemIndex.Equals(index))
                {
                    dtProgLang.Rows.Add(StyleId.Text, StyleCode.Text, Title.Text); // Add Data 
                }


            }
            rpt_Style.DataSource = dtProgLang;
            rpt_Style.DataBind();
            totalBarcodes.Text = dtProgLang.Rows.Count.ToString();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
    }
}