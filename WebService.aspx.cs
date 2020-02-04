using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

public partial class WebService : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //string jsondata = Request.QueryString["BarcodeList"];
            Response.Clear();
            //Response.ContentType = "application/json; charset=utf-8";
            Response.Write("test");
            Response.End();

            /*string json = @"[{  
                'ID': '1',  
                'Name': 'Manas',  
                'Address': 'India',
                'data': '[datum0, datum1, datumN]'
            }]";

            string jsonarray = @"[{
                                'name':'John',
                                'cars':[ 'Ford', 'BMW', 'Fiat' ]
                                }]";*/


            /*DataTable dt = new DataTable();
            dt.Columns.Add("Barcode");
            dt.Rows.Add("NJI/10/000");
            dt.Rows.Add("NJI/10/000");
            dt.Rows.Add("NJI/10/000");
            dt.Rows.Add("NJI/10/000");

            string jBrcode = JsonConvert.SerializeObject(dt);

            DataTable st = new DataTable();
            st.Columns.Add("Rackcode");
            st.Columns.Add("Barcode");
            st.Rows.Add("//A-A1", jBrcode);

            string jBrcodenew = JsonConvert.SerializeObject(st);

            //string kString =@"[{'Rackcode':'//A-A1','Barcode':'[{\'Barcode\':\'NJI/10/000\'},{\'Barcode\':\'NJI/10/000\'},{\'Barcode\':\'NJI/10/000\'},{\'Barcode\':\'NJI/10/000\'}]'}]";
            //string u = "{'ValidateUser':[{'UserName':'','loginType':'','Error_Code':'400','Description':'Bad Request'}]}";

            DataTable empObj = JsonConvert.DeserializeObject<DataTable>(kString);            

            DataTable barcodes = JsonConvert.DeserializeObject<DataTable>(empObj.Rows[0]["Barcode"].ToString());

            webserviceCls obj = new webserviceCls();
            string result = obj.updateRack(empObj.Rows[0]["Rackcode"].ToString(), barcodes);

            string jsonSuccess = JsonConvert.SerializeObject(result);
            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(jsonSuccess);
            Response.End();*/

        }
        catch (Exception ex)
        {

        }
        
    }
}