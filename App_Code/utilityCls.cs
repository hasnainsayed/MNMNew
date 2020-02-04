using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using ClosedXML;
using ClosedXML.Excel;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web.UI.WebControls;


/// <summary>
/// Summary description for utilityCls
/// </summary>
public class utilityCls
{
    public utilityCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public object Response { get; private set; }

    public void generateExcel(DataTable Exceldt)
    {
        try
        {

            //using (XLWorkbook wb = new XLWorkbook())
            //{
            //    wb.Worksheets.Add(Exceldt, "Dump");
            //    Response.Clear();
            //    Response.Buffer = true;
            //    Response.Charset = "";
            //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //    string fname = "Dump_" + DateTime.Now();

            //    Response.AddHeader("content-disposition", "attachment;filename=" + fname + ".xlsx");
            //    using (MemoryStream MyMemoryStream = new MemoryStream())
            //    {
            //        wb.SaveAs(MyMemoryStream);
            //        MyMemoryStream.WriteTo(Response.OutputStream);
            //        Response.Flush();
            //        Response.End();
            //    }
            //}


        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    public string datatableToHtml(DataTable dt)
    {
        //Building an HTML string.
        StringBuilder html = new StringBuilder();
        try
        {
            //Table start.
            html.Append("<table border = '1'>");

            //Building the Header row.
            html.Append("<tr>");
            foreach (DataColumn column in dt.Columns)
            {
                html.Append("<th>");
                html.Append(column.ColumnName);
                html.Append("</th>");
            }
            html.Append("</tr>");

            //Building the Data rows.
            foreach (DataRow row in dt.Rows)
            {
                html.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    html.Append("<td>");
                    html.Append(row[column.ColumnName]);
                    html.Append("</td>");
                }
                html.Append("</tr>");
            }

            //Table end.
            html.Append("</table>");
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
        }
        return html.ToString();
    }

    public void showCapARAPopUpSingle_Click(object sender, EventArgs e, Panel Panel1,string styles, string barcode)
    {
        try
        {
            Repeater rtp_ListMaster = (Repeater)Panel1.FindControl("rtp_ListMaster");
            DataTable dtSortempty = new DataTable();
            rtp_ListMaster.DataSource = dtSortempty;
            rtp_ListMaster.DataBind();
           
            Repeater rpt_StyleMaster = (Repeater)Panel1.FindControl("rpt_StyleMaster");
            DataTable styledt = new DataTable();
            rpt_StyleMaster.DataSource = styledt;
            rpt_StyleMaster.DataBind();

            reportCls r = new reportCls();
            DataTable style = r.getStyleDets(styles);

           
            styledt.Columns.Add("Details");
            styledt.Columns.Add("User");
            styledt.Columns.Add("Dets");
            styledt.Rows.Add("Created", style.Rows[0]["username"].ToString(), Convert.ToDateTime(style.Rows[0]["SystemDate"]).ToString("dd MMM yyyy HH:m:ss tt"));
            if (!style.Rows[0]["logs"].ToString().Equals(","))
            {
                string log = style.Rows[0]["logs"].ToString().Remove(style.Rows[0]["logs"].ToString().Length - 1, 1);
                string[] stylesArr = (log.Substring(1).Split(','));
                foreach (var line in stylesArr)
                {
                    string[] split = line.Split('#');
                    styledt.Rows.Add("Edited", split[0], Convert.ToDateTime(split[1]).ToString("dd MMM yyyy HH:m:ss tt"));
                }
            }

            DataSet ds = r.barcodeStatusHistory(barcode);
            DataTable dt = new DataTable();

            dt.Columns.Add("User");
            dt.Columns.Add("Status");
            dt.Columns.Add("DateTime", typeof(DateTime));

            DataTable createdby = ds.Tables["createdby"];
            dt.Rows.Add(createdby.Rows[0]["username"], createdby.Rows[0]["initialStatus"], createdby.Rows[0]["SystemDate"]);

            DataTable locTable = ds.Tables["locTable"];
            foreach (DataRow row in locTable.Rows)
            {
                if (!row["salesUser"].ToString().Equals(""))
                {
                    dt.Rows.Add(row["salesUser"].ToString(), "SOLD", row["salesDateTime"]);
                }
                if (!row["dispatchUser"].ToString().Equals(""))
                {
                    dt.Rows.Add(row["dispatchUser"].ToString(), "DISPATCHED", row["dispatchtimestamp"]);
                }
                if (!row["retUser"].ToString().Equals(""))
                {
                    dt.Rows.Add(row["retUser"].ToString(), "RETURNED", row["returntimestamp"]);
                    dt.Rows.Add(row["retUser"].ToString(), row["changeStatus"].ToString(), row["returntimestamp"]);
                }

            }

            DataTable cancle = ds.Tables["cancle"];
            foreach (DataRow row in cancle.Rows)
            {
                if (!row["salesUser"].ToString().Equals(""))
                {
                    dt.Rows.Add(row["salesUser"].ToString(), "SOLD", row["salesDateTime"]);
                }
                if (!row["cancelUser"].ToString().Equals(""))
                {
                    dt.Rows.Add(row["cancelUser"].ToString(), "CANCELLED", row["cancelTimeStamp"]);
                    dt.Rows.Add(row["cancelUser"].ToString(), row["changeStatus"].ToString(), row["cancelTimeStamp"]);
                }

            }
            DataView dtview = new DataView(dt);
            string sortstring = "DateTime ASC"; // sorting in descending manner 
            dtview.Sort = sortstring;
            DataTable dtsort = dtview.ToTable();

            
            rpt_StyleMaster.DataSource = styledt;
            rpt_StyleMaster.DataBind();

            
            rtp_ListMaster.DataSource = dtsort;
            rtp_ListMaster.DataBind();

            Label lblBarcodeMaster = (Label)Panel1.FindControl("lblBarcodeMaster");
            lblBarcodeMaster.Text = barcode;
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    public DataTable getTableColwithID(string table, string column, string val, string getColumns)
    {
        DataTable catTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("getTable");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select " + getColumns + " from " + table + " where " + column + "=@column";
            command.Parameters.AddWithValue("column", val);
            catTable.Load(command.ExecuteReader());

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        return catTable;
    }

    

    public int saveCustomer(int customerId, string custFirstname, string custLastName,
        string emailAddress, string phoneNo, string address, string city, string state, string pincode,string makerId, string logs)
    {
        int success = -1;
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.Connection = connection;

        try
        {
            command.CommandText = "saveCustomer";
            command.Parameters.AddWithValue("@customerId", customerId);
            command.Parameters.AddWithValue("@custFirstname", custFirstname);
            command.Parameters.AddWithValue("@custLastName", custLastName);
            command.Parameters.AddWithValue("@emailAddress", emailAddress);            
            command.Parameters.AddWithValue("@phoneNo", phoneNo);
            command.Parameters.AddWithValue("@address", address);
            command.Parameters.AddWithValue("@city", city);
            command.Parameters.AddWithValue("@state", state);
            command.Parameters.AddWithValue("@pincode", pincode);
            command.Parameters.AddWithValue("@makerId", makerId);
            command.Parameters.AddWithValue("@logs", logs);
            command.Parameters.Add("@result", SqlDbType.Char, 100);
            command.Parameters["@result"].Direction = ParameterDirection.Output;
            command.ExecuteNonQuery();
            success = Convert.ToInt32(command.Parameters["@result"].Value.ToString());

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        return success;
    }
}