using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;


/// <summary>
/// Summary description for sooperBuyWebService
/// </summary>
[WebService(Namespace = "http://sooperbuy.dzvdesk.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class sooperBuyWebService : System.Web.Services.WebService
{

    public sooperBuyWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]    
    public string packAndRack(string abc)
    {
        DataTable empObj = JsonConvert.DeserializeObject<DataTable>(abc);
        
        DataTable barcodes = JsonConvert.DeserializeObject<DataTable>(empObj.Rows[0]["Barcode"].ToString());

        webserviceCls obj = new webserviceCls();
        
        string result = obj.updateRack(empObj.Rows[0]["Rackcode"].ToString(), barcodes);
       
        DataTable dt = new DataTable();
        dt.Columns.Add("result"); dt.Rows.Add(result);
        return JsonConvert.SerializeObject(dt);

        //HttpContext.Current.Response.Write(JsonConvert.SerializeObject(dt));

        /*string jsonSuccess = JsonConvert.SerializeObject(result);
       DataTable dt = new DataTable();
       dt.Columns.Add("result"); dt.Rows.Add(result);
       return (JsonConvert.SerializeObject(dt));*/
        //return abc;
    }

}
