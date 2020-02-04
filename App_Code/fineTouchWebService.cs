using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using Newtonsoft.Json;
using DataBase;
using System.Data.SqlClient;

/// <summary>
/// Summary description for fineTouchWebService
/// </summary>
//[WebService(Namespace = "http://tempuri.org/")]
[WebService(Namespace = "http://finetouch.dzvdesk.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class fineTouchWebService : System.Web.Services.WebService
{

    public fineTouchWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    public bool authtticateWebserviceCall(string splPassKey)
    {
        try
        {
            bool authenticate = false;
            if (splPassKey.Equals("ftko0ji9hu8"))
            {
                authenticate = true;
            }

            return authenticate;
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
            return false;
        }
    }

    [WebMethod]
    public string login(string login)
    {
        try
        {
            DataTable dtloginCred = new DataTable();

            //[{"splPassKey":"ftko0ji9hu8","username":"saima","password":"saimaaq1sw2de3"}]

            dtloginCred = JsonConvert.DeserializeObject<DataTable>(login);

            if (authtticateWebserviceCall(dtloginCred.Rows[0]["splPassKey"].ToString()))
            {
                login objlogin = new login();
                DataSet dtlogin = objlogin.AdminLogin(dtloginCred.Rows[0]["username"].ToString(), dtloginCred.Rows[0]["password"].ToString());
                if (dtlogin.Tables[0].Rows.Count > 0)
                {

                    DataTable dt = new DataTable();
                    dt.Columns.Add("userId");
                    dt.Columns.Add("userName");
                    dt.Columns.Add("userRole");
                    dt.Rows.Add(dtlogin.Tables[0].Rows[0]["userid"].ToString(),
                        dtlogin.Tables[0].Rows[0]["username"].ToString(), dtlogin.Tables[0].Rows[0]["userrole"].ToString());

                    //[{"userId":"25","userName":"saima","userRole":"1"}]

                    return JsonConvert.SerializeObject(dt);
                }
                else
                    return JsonConvert.SerializeObject("Username or Password is Incorrect");
            }
            else
            {
                return JsonConvert.SerializeObject("Authetication Failed");
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
            return JsonConvert.SerializeObject("ERROR");
        }
    }

    [WebMethod]
    public string lotList(string splKey)
    {
        try
        {
            // "ftko0ji9hu8"
            splKey = JsonConvert.DeserializeObject<string>(splKey);
            if (authtticateWebserviceCall(splKey))
            {

                newLotCls obj = new newLotCls();
                DataTable dt = obj.getLot();
                return JsonConvert.SerializeObject(dt);
                //[{"BagId":1,"VendorID":1,"NoOfBags":"1","BagDescription":"1","IsActive":1,"amount":0.00,"mrp":5500.00,"discount":10.00,"Desc":null,"invoiceNo":"1","invoiceDate":"2019-09-07T00:00:00","taxAmount":111.00,"procurementAmount":111.00,"miscAmount":111.00,"oldsftqty":"0","oldsftamt":0.0,"Percentage":10.00,"totalPiece":20,"totalAmount":100.00,"paidStatus":true,"VendorName":"DreamzVision"},{"BagId":2,"VendorID":1,"NoOfBags":null,"BagDescription":"1","IsActive":1,"amount":0.00,"mrp":0.00,"discount":0.00,"Desc":null,"invoiceNo":"abc","invoiceDate":"2019-09-12T00:00:00","taxAmount":0.00,"procurementAmount":0.00,"miscAmount":0.00,"oldsftqty":null,"oldsftamt":null,"Percentage":null,"totalPiece":10,"totalAmount":250.00,"paidStatus":true,"VendorName":"DreamzVision"},{"BagId":3,"VendorID":1,"NoOfBags":null,"BagDescription":"test","IsActive":1,"amount":0.00,"mrp":0.00,"discount":0.00,"Desc":null,"invoiceNo":"2","invoiceDate":"2019-09-12T00:00:00","taxAmount":0.00,"procurementAmount":0.00,"miscAmount":0.00,"oldsftqty":null,"oldsftamt":null,"Percentage":null,"totalPiece":25,"totalAmount":258.00,"paidStatus":false,"VendorName":"DreamzVision"},{"BagId":5,"VendorID":1,"NoOfBags":null,"BagDescription":"a","IsActive":1,"amount":0.00,"mrp":0.00,"discount":0.00,"Desc":null,"invoiceNo":"aa","invoiceDate":"2019-09-16T00:00:00","taxAmount":0.00,"procurementAmount":0.00,"miscAmount":0.00,"oldsftqty":null,"oldsftamt":null,"Percentage":null,"totalPiece":1,"totalAmount":20.00,"paidStatus":false,"VendorName":"DreamzVision"},{"BagId":6,"VendorID":2,"NoOfBags":null,"BagDescription":"v1","IsActive":1,"amount":0.00,"mrp":0.00,"discount":0.00,"Desc":null,"invoiceNo":"1","invoiceDate":"2019-09-16T00:00:00","taxAmount":0.00,"procurementAmount":0.00,"miscAmount":0.00,"oldsftqty":null,"oldsftamt":null,"Percentage":null,"totalPiece":100,"totalAmount":250.00,"paidStatus":false,"VendorName":"Vendor"}]
            }
            else
            {
                return JsonConvert.SerializeObject("Authetication Failed");
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
            return JsonConvert.SerializeObject("ERROR");
        }
    }

    [WebMethod(EnableSession = true)]
    public string styleListSearch(string splKey, string styleCode)
    {
        HttpContext.Current.Session["login"] = "AppDummy";
        HttpContext.Current.Session["userName"] = "AppDummy";
        try
        {

            // "ftko0ji9hu8","00A1"
            splKey = JsonConvert.DeserializeObject<string>(splKey);
            if (authtticateWebserviceCall(splKey))
            {
                
                styleCode = JsonConvert.DeserializeObject<string>(styleCode);
                styleCls obj = new styleCls();
                DataTable dt = obj.styleListSearch(styleCode);
                
                return JsonConvert.SerializeObject(dt);
            }
            else
            {
                return JsonConvert.SerializeObject("Authetication Failed");
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
            return JsonConvert.SerializeObject("ERROR");
        }
        finally
        {
            Session.Abandon();
        }
    }

    [WebMethod(EnableSession = true)]
    public string styleList(string splKey)
    {
        HttpContext.Current.Session["login"] = "AppDummy";
        HttpContext.Current.Session["userName"] = "AppDummy";
        try
        {
            // "ftko0ji9hu8"
            splKey = JsonConvert.DeserializeObject<string>(splKey);
            if (authtticateWebserviceCall(splKey))
            {
                
                styleCls obj = new styleCls();
                DataTable dt = obj.styleList();
                
                return JsonConvert.SerializeObject(dt);
            }
            else
            {
                return JsonConvert.SerializeObject("Authetication Failed");
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
            return JsonConvert.SerializeObject("ERROR");
        }
        finally
        {
            Session.Abandon();
        }
    }

    [WebMethod(EnableSession = true)]
    public string styleDetails(string splKey, string styleId)
    {
        HttpContext.Current.Session["login"] = "AppDummy";
        HttpContext.Current.Session["userName"] = "AppDummy";
        try
        {
            // "ftko0ji9hu8","00A1"
            splKey = JsonConvert.DeserializeObject<string>(splKey);
            if (authtticateWebserviceCall(splKey))
            {
                
                styleId = JsonConvert.DeserializeObject<string>(styleId);
                styleCls obj = new styleCls();
                DataTable dt = obj.styleDetails(styleId);               
                return JsonConvert.SerializeObject(dt);
            }
            else
            {
                return JsonConvert.SerializeObject("Authetication Failed");
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
            return JsonConvert.SerializeObject("ERROR");
        }
        finally
        {
            Session.Abandon();
        }
    }

    [WebMethod(EnableSession = true)]
    public string imageLink(string splKey)
    {
        HttpContext.Current.Session["login"] = "AppDummy";
        HttpContext.Current.Session["userName"] = "AppDummy";
        try
        {
            // "ftko0ji9hu8"
            splKey = JsonConvert.DeserializeObject<string>(splKey);
            if (authtticateWebserviceCall(splKey))
            {
                
                styleCls obj = new styleCls();
                DataTable dt = obj.getTable("imagePaths");
                
                return JsonConvert.SerializeObject(dt);
            }
            else
            {
                return JsonConvert.SerializeObject("Authetication Failed");
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
            return JsonConvert.SerializeObject("ERROR");
        }
        finally
        {
            Session.Abandon();
        }
    }

    [WebMethod]
    public string addEditLot(string splKey, string lotDets)
    {
        try
        {
            // "ftko0ji9hu8","00A1"
            splKey = JsonConvert.DeserializeObject<string>(splKey);
            if (authtticateWebserviceCall(splKey))
            {


                /*DataTable lotDets = new DataTable();
                lotDets.Columns.Add("lotId");
                lotDets.Columns.Add("vendorID");
                lotDets.Columns.Add("totalAmount");
                lotDets.Columns.Add("BagDescription");
                lotDets.Columns.Add("invoiceNo");
                lotDets.Columns.Add("invoiceDate");
                lotDets.Columns.Add("totalPiece");
                lotDets.Columns.Add("userId");
                lotDets.Columns.Add("image1");
                lotDets.Rows.Add("0", "1", "10", "wbser", "12", "2019-01-03", "12","1","");*/
                //string check = JsonConvert.SerializeObject(lotDets);

                DataTable dt = JsonConvert.DeserializeObject<DataTable>(lotDets);
                newLotCls obj = new newLotCls();
                string res = string.Empty;
                if (!dt.Rows.Count.Equals(0))
                {
                    if (dt.Rows[0]["lotId"].ToString().Equals("0"))
                    {

                        int Success = obj.addLot(dt.Rows[0]["vendorID"].ToString(), dt.Rows[0]["totalAmount"].ToString(),
                            dt.Rows[0]["BagDescription"].ToString(), dt.Rows[0]["invoiceNo"].ToString(), dt.Rows[0]["invoiceDate"].ToString(),
                            dt.Rows[0]["totalPiece"].ToString(), dt.Rows[0]["userId"].ToString(), dt.Rows[0]["image1"].ToString(),"","");

                        if (Success.Equals(-1))
                        {

                            res = "Lot Adding Failed";
                        }
                        else
                        {
                            res = "Lot Added Successfully - #" + Success;
                        }


                    }
                    else
                    {
                        int Success = obj.updateLot(Convert.ToInt32(dt.Rows[0]["lotId"]), dt.Rows[0]["vendorID"].ToString(), dt.Rows[0]["totalAmount"].ToString(),
                            dt.Rows[0]["BagDescription"].ToString(), dt.Rows[0]["invoiceNo"].ToString(), dt.Rows[0]["invoiceDate"].ToString(),
                            dt.Rows[0]["totalPiece"].ToString(), dt.Rows[0]["image1"].ToString(),"","");

                        if (Success.Equals(-1))
                        {

                            res = "Lot Update Failed";
                        }
                        else
                        {
                            res = "Lot Updated Successfully - #" + Success;
                        }
                    }


                }
                else
                {

                    res = "ERROR";
                }

                return res;
            }
            else
            {
                return JsonConvert.SerializeObject("Authetication Failed");
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
            return JsonConvert.SerializeObject("ERROR");
        }
    }

    [WebMethod(EnableSession = true)]
    public string getStates(string splKey)
    {
        try
        {
            // "ftko0ji9hu8"
            splKey = JsonConvert.DeserializeObject<string>(splKey);
            if (authtticateWebserviceCall(splKey))
            {
                HttpContext.Current.Session["login"] = "AppDummy";
                HttpContext.Current.Session["userName"] = "AppDummy";
                styleCls obj = new styleCls();
                DataTable dt = obj.getTable("stateCode");
                Session.Abandon();
                return JsonConvert.SerializeObject(dt);
                //[{"BagId":1,"VendorID":1,"NoOfBags":"1","BagDescription":"1","IsActive":1,"amount":0.00,"mrp":5500.00,"discount":10.00,"Desc":null,"invoiceNo":"1","invoiceDate":"2019-09-07T00:00:00","taxAmount":111.00,"procurementAmount":111.00,"miscAmount":111.00,"oldsftqty":"0","oldsftamt":0.0,"Percentage":10.00,"totalPiece":20,"totalAmount":100.00,"paidStatus":true,"VendorName":"DreamzVision"},{"BagId":2,"VendorID":1,"NoOfBags":null,"BagDescription":"1","IsActive":1,"amount":0.00,"mrp":0.00,"discount":0.00,"Desc":null,"invoiceNo":"abc","invoiceDate":"2019-09-12T00:00:00","taxAmount":0.00,"procurementAmount":0.00,"miscAmount":0.00,"oldsftqty":null,"oldsftamt":null,"Percentage":null,"totalPiece":10,"totalAmount":250.00,"paidStatus":true,"VendorName":"DreamzVision"},{"BagId":3,"VendorID":1,"NoOfBags":null,"BagDescription":"test","IsActive":1,"amount":0.00,"mrp":0.00,"discount":0.00,"Desc":null,"invoiceNo":"2","invoiceDate":"2019-09-12T00:00:00","taxAmount":0.00,"procurementAmount":0.00,"miscAmount":0.00,"oldsftqty":null,"oldsftamt":null,"Percentage":null,"totalPiece":25,"totalAmount":258.00,"paidStatus":false,"VendorName":"DreamzVision"},{"BagId":5,"VendorID":1,"NoOfBags":null,"BagDescription":"a","IsActive":1,"amount":0.00,"mrp":0.00,"discount":0.00,"Desc":null,"invoiceNo":"aa","invoiceDate":"2019-09-16T00:00:00","taxAmount":0.00,"procurementAmount":0.00,"miscAmount":0.00,"oldsftqty":null,"oldsftamt":null,"Percentage":null,"totalPiece":1,"totalAmount":20.00,"paidStatus":false,"VendorName":"DreamzVision"},{"BagId":6,"VendorID":2,"NoOfBags":null,"BagDescription":"v1","IsActive":1,"amount":0.00,"mrp":0.00,"discount":0.00,"Desc":null,"invoiceNo":"1","invoiceDate":"2019-09-16T00:00:00","taxAmount":0.00,"procurementAmount":0.00,"miscAmount":0.00,"oldsftqty":null,"oldsftamt":null,"Percentage":null,"totalPiece":100,"totalAmount":250.00,"paidStatus":false,"VendorName":"Vendor"}]
            }
            else
            {
                return JsonConvert.SerializeObject("Authetication Failed");
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
            return JsonConvert.SerializeObject("ERROR");
        }
    }

    [WebMethod]
    public string saveCustomer(string splKey, string custDets)
    {
        try
        {
            splKey = JsonConvert.DeserializeObject<string>(splKey);
            if (authtticateWebserviceCall(splKey))
            {
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(custDets);
                newLotCls obj = new newLotCls();
                string res = string.Empty;
                if (!dt.Rows.Count.Equals(0))
                {
                    utilityCls uobj = new utilityCls();
                    int success = uobj.saveCustomer(Convert.ToInt32(dt.Rows[0]["customerId"]), dt.Rows[0]["custFirstname"].ToString(),
                           dt.Rows[0]["custLastName"].ToString(), dt.Rows[0]["emailAddress"].ToString(), dt.Rows[0]["phoneNo"].ToString(),
                           dt.Rows[0]["address"].ToString(), dt.Rows[0]["city"].ToString(), dt.Rows[0]["state"].ToString(), dt.Rows[0]["pincode"].ToString(), dt.Rows[0]["makerId"].ToString(), dt.Rows[0]["logDet"].ToString());
                    if (success.Equals(1)) // email already exist
                    {
                        res = "Phone No. Already Exist";

                    }
                    else if (success.Equals(2) || success.Equals(-1))// transaction rolled back
                    {
                        res = "Some Error Occured";
                    }
                    else if (success.Equals(3))// inserted
                    {
                        res = "Customer Details Added Successfully";
                    }
                    else if (success.Equals(4))// updated
                    {
                        res = "Customer Details Updated Successfully";

                    }
                }
                else
                {

                    res = "ERROR";
                }
                
                return res;
            }
            else
            {
                return JsonConvert.SerializeObject("Authetication Failed");
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
            return JsonConvert.SerializeObject("ERROR");
        }
    }

    [WebMethod]
    public string customerExist(string splKey, string phoneNo)
    {        
        try
        {
            // "ftko0ji9hu8","8956231470"
            splKey = JsonConvert.DeserializeObject<string>(splKey);
            if (authtticateWebserviceCall(splKey))
            {

                string phones = JsonConvert.DeserializeObject<string>(phoneNo);
                utilityCls obj = new utilityCls();
                DataTable dt = obj.getTableColwithID("websiteCustomer", "phoneNo", phones,"*");
                return JsonConvert.SerializeObject(dt);
            }
            else
            {
                return JsonConvert.SerializeObject("Authetication Failed");
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
            return JsonConvert.SerializeObject("ERROR");
        }
        
    }

    [WebMethod(EnableSession = true)]
    public string updateImages(string splKey, string imageDt)
    {
        HttpContext.Current.Session["login"] = "AppDummy";
        HttpContext.Current.Session["userName"] = "AppDummy";
        try
        {
            // "ftko0ji9hu8","00A1"
            splKey = JsonConvert.DeserializeObject<string>(splKey);
            if (authtticateWebserviceCall(splKey))
            {

                DataTable dt = JsonConvert.DeserializeObject<DataTable>(imageDt);
                styleCls obj = new styleCls();
                int success = obj.updateImages(dt);
                if(success.Equals(0))
                {
                    return JsonConvert.SerializeObject("Images Updated/Deleted Successfully");
                }
                else
                {
                    return JsonConvert.SerializeObject("Images Update Failed");
                }
            }
            else
            {
                return JsonConvert.SerializeObject("Authetication Failed");
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
            return JsonConvert.SerializeObject("ERROR");
        }
        finally
        {
            Session.Abandon();
        }
    }

    [WebMethod(EnableSession = true)]
    public string generateToken(string splKey, string customerId,string makerId)
    {
        HttpContext.Current.Session["login"] = "AppDummy";
        HttpContext.Current.Session["userName"] = "AppDummy";
        try
        {            
            splKey = JsonConvert.DeserializeObject<string>(splKey);
            if (authtticateWebserviceCall(splKey))
            {
                string makerId1 = JsonConvert.DeserializeObject<string>(makerId);
                string customerId1 = JsonConvert.DeserializeObject<string>(customerId);
                invoiceCls obj = new invoiceCls();
                int res = obj.generateToken(customerId1, makerId1);
                return JsonConvert.SerializeObject(res);
            }
            else
            {
                return JsonConvert.SerializeObject("Authetication Failed");
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
            return JsonConvert.SerializeObject("ERROR");
        }
        finally
        {
            Session.Abandon();
        }
    }

    [WebMethod(EnableSession = true)]
    public string previewToken(string splKey, string barcodeDet)
    {
        HttpContext.Current.Session["login"] = "AppDummy";
        HttpContext.Current.Session["userName"] = "AppDummy";
        try
        {
           /* DataTable s = new DataTable();
            s.Columns.Add("BarcodeNo");
            s.Rows.Add("0000/8/000");
            s.Rows.Add("0000/8/001");
            s.Rows.Add("0000/8/002");
            return JsonConvert.SerializeObject(s);*/
            splKey = JsonConvert.DeserializeObject<string>(splKey);
            if (authtticateWebserviceCall(splKey))
            {
                DataTable barcodeDt = JsonConvert.DeserializeObject<DataTable>(barcodeDet);
                barcodeDt.Columns.Add("mrp");
                barcodeDt.Columns.Add("piecePerPacket");
                utilityCls obj = new utilityCls();
                if(!barcodeDt.Rows.Count.Equals(0))
                {
                    foreach (DataRow dRow in barcodeDt.Rows)
                    {
                        DataTable dt = obj.getTableColwithID("StockUpInward", "BarcodeNo", dRow["BarcodeNo"].ToString(), "mrp,piecePerPacket");
                        if (!dt.Rows.Count.Equals(0))
                        {
                            dRow["mrp"] = dt.Rows[0]["mrp"].ToString();
                            dRow["piecePerPacket"] = dt.Rows[0]["piecePerPacket"].ToString();
                        }
                    }
                }
                
                
                return JsonConvert.SerializeObject(barcodeDt);
            }
            else
            {
                return JsonConvert.SerializeObject("Authetication Failed");
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
            return JsonConvert.SerializeObject("ERROR");
        }
        finally
        {
            Session.Abandon();
        }
    }

    [WebMethod(EnableSession = true)]
    public string tokenGenList(string splKey)
    {
        HttpContext.Current.Session["login"] = "AppDummy";
        HttpContext.Current.Session["userName"] = "AppDummy";
        try
        {
            // "ftko0ji9hu8"
            splKey = JsonConvert.DeserializeObject<string>(splKey);
            if (authtticateWebserviceCall(splKey))
            {

                invoiceCls obj = new invoiceCls();
                DataTable dt = obj.getTokenGenList();

                return JsonConvert.SerializeObject(dt);
            }
            else
            {
                return JsonConvert.SerializeObject("Authetication Failed");
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
            return JsonConvert.SerializeObject("ERROR");
        }
        finally
        {
            Session.Abandon();
        }
    }

    [WebMethod(EnableSession = true)]
    public string getTokenBarcodes(string splKey, string tokenId, string makerId)
    {
        HttpContext.Current.Session["login"] = "AppDummy";
        HttpContext.Current.Session["userName"] = "AppDummy";
        try
        {
            /* DataTable s = new DataTable();
             s.Columns.Add("BarcodeNo");
             s.Rows.Add("0000/8/000");
             s.Rows.Add("0000/8/001");
             s.Rows.Add("0000/8/002");
             return JsonConvert.SerializeObject(s);*/
            splKey = JsonConvert.DeserializeObject<string>(splKey);
            if (authtticateWebserviceCall(splKey))
            {
                string invoiceId = JsonConvert.DeserializeObject<string>(tokenId);
                string tokenGenBy = JsonConvert.DeserializeObject<string>(makerId);
                
                invoiceCls obj = new invoiceCls();

                // check the status of token 
                utilityCls uObj = new utilityCls();
                DataTable invoice = uObj.getTableColwithID("invoice","invid", invoiceId,"*");
                if(invoice.Rows.Count.Equals(0))
                {
                    return JsonConvert.SerializeObject("Wrong Token Number");
                }
                else
                {
                    if (invoice.Rows[0]["invoiceStatus"].ToString().Equals("Tellying"))
                    {
                        return JsonConvert.SerializeObject("Invoice is being Tellied");
                    }
                    else if (invoice.Rows[0]["invoiceStatus"].ToString().Equals("TokenGen"))
                    {
                        // change invoice status to tokenMaking
                        int res = obj.changeInvoiceStatus(invoiceId, "TokenMaking", tokenGenBy, "tokenMadeBy", "tokenMakingDatetime");
                        if(res.Equals(1))
                        {
                            DataTable barcodes = uObj.getTableColwithID("StockUpInward", "tokenId", invoiceId, "BarcodeNo");
                            return JsonConvert.SerializeObject(barcodes);
                        }
                        else
                        {
                            return JsonConvert.SerializeObject("invoice Status Change Failed. Please Try Again");
                        }

                    }
                    else if(invoice.Rows[0]["invoiceStatus"].ToString().Equals("TokenMaking") && invoice.Rows[0]["tokenGenBy"].ToString().Equals(makerId))
                    {
                        DataTable barcodes = uObj.getTableColwithID("StockUpInward", "tokenId", invoiceId, "BarcodeNo");
                        return JsonConvert.SerializeObject(barcodes);
                    }
                    else
                    {
                        DataTable user = uObj.getTableColwithID("login", "userid", tokenGenBy, "username");
                        return JsonConvert.SerializeObject("Token is being made by "+ user.Rows[0]["username"].ToString());
                    }
                }

            }
            else
            {
                return JsonConvert.SerializeObject("Authetication Failed");
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
            return JsonConvert.SerializeObject("ERROR");
        }
        finally
        {
            Session.Abandon();
        }
    }

    [WebMethod(EnableSession = true)]
    public string tokenMadeList(string splKey)
    {
        HttpContext.Current.Session["login"] = "AppDummy";
        HttpContext.Current.Session["userName"] = "AppDummy";
        try
        {
            // "ftko0ji9hu8"
            splKey = JsonConvert.DeserializeObject<string>(splKey);
            if (authtticateWebserviceCall(splKey))
            {

                invoiceCls obj = new invoiceCls();
                DataTable dt = obj.getTokenMadeList();

                return JsonConvert.SerializeObject(dt);
            }
            else
            {
                return JsonConvert.SerializeObject("Authetication Failed");
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
            return JsonConvert.SerializeObject("ERROR");
        }
        finally
        {
            Session.Abandon();
        }
    }

    [WebMethod(EnableSession = true)]
    public string saveToken(string splKey,string tokenId, string barcodeDet,string makerId,string status)
    {
        HttpContext.Current.Session["login"] = "AppDummy";
        HttpContext.Current.Session["userName"] = "AppDummy";
        try
        {
            splKey = JsonConvert.DeserializeObject<string>(splKey);
            if (authtticateWebserviceCall(splKey))
            {
                string invoiceId = JsonConvert.DeserializeObject<string>(tokenId);
                string tokenMadeId = JsonConvert.DeserializeObject<string>(makerId);
                DataTable barcodeDt = JsonConvert.DeserializeObject<DataTable>(barcodeDet);                
                invoiceCls obj = new invoiceCls();
                string res = obj.saveToken(invoiceId,barcodeDt, tokenMadeId, status);
                return JsonConvert.SerializeObject(res);
            }
            else
            {
                return JsonConvert.SerializeObject("Authetication Failed");
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
            return JsonConvert.SerializeObject("ERROR");
        }
        finally
        {
            Session.Abandon();
        }
    }

    [WebMethod(EnableSession = true)]
    public string getBarcodesForTellying(string splKey, string tokenId, string makerId)
    {
        HttpContext.Current.Session["login"] = "AppDummy";
        HttpContext.Current.Session["userName"] = "AppDummy";
        try
        {
            /* DataTable s = new DataTable();
             s.Columns.Add("BarcodeNo");
             s.Rows.Add("0000/8/000");
             s.Rows.Add("0000/8/001");
             s.Rows.Add("0000/8/002");
             return JsonConvert.SerializeObject(s);*/
            splKey = JsonConvert.DeserializeObject<string>(splKey);
            if (authtticateWebserviceCall(splKey))
            {
                string invoiceId = JsonConvert.DeserializeObject<string>(tokenId);
                string telliedById = JsonConvert.DeserializeObject<string>(makerId);

                invoiceCls obj = new invoiceCls();

                // check the status of token 
                utilityCls uObj = new utilityCls();
                DataTable invoice = uObj.getTableColwithID("invoice", "invid", invoiceId, "*");
                if (invoice.Rows.Count.Equals(0))
                {
                    return JsonConvert.SerializeObject("Wrong Token Number");
                }
                else
                {
                    if (invoice.Rows[0]["invoiceStatus"].ToString().Equals("Invoiced"))
                    {
                        return JsonConvert.SerializeObject("Token is Invoiced");
                    }
                    else if (invoice.Rows[0]["invoiceStatus"].ToString().Equals("Tellied"))
                    {
                        return JsonConvert.SerializeObject("Token is Tellied");
                    }
                    else if (invoice.Rows[0]["invoiceStatus"].ToString().Equals("TokenMade"))
                    {
                        // change invoice status to tokenMaking
                        int res = obj.changeInvoiceStatus(invoiceId, "Tellying", telliedById, "telliedById", "tellyDateTime");
                        if (res.Equals(1))
                        {
                            DataTable barcodes = uObj.getTableColwithID("StockUpInward", "tokenId", invoiceId, "BarcodeNo,checkNo,piecePerPacket,mrp");
                            return JsonConvert.SerializeObject(barcodes);
                        }
                        else
                        {
                            return JsonConvert.SerializeObject("Token Status Change Failed. Please Try Again");
                        }

                    }
                    else if (invoice.Rows[0]["invoiceStatus"].ToString().Equals("Tellying") && invoice.Rows[0]["telliedById"].ToString().Equals(makerId))
                    {
                        DataTable barcodes = uObj.getTableColwithID("StockUpInward", "tokenId", invoiceId, "BarcodeNo,checkNo,piecePerPacket,mrp");
                        return JsonConvert.SerializeObject(barcodes);
                    }
                    else
                    {
                        DataTable user = uObj.getTableColwithID("login", "userid", telliedById, "username");
                        return JsonConvert.SerializeObject("Token is being tellied by " + user.Rows[0]["username"].ToString());
                    }
                }

            }
            else
            {
                return JsonConvert.SerializeObject("Authetication Failed");
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
            return JsonConvert.SerializeObject("ERROR");
        }
        finally
        {
            Session.Abandon();
        }
    }

    [WebMethod(EnableSession = true)]
    public string saveInvoice(string splKey, string tokenId, string barcodeDet, string makerId, string remarks)
    {
        HttpContext.Current.Session["login"] = "AppDummy";
        HttpContext.Current.Session["userName"] = "AppDummy";
        try
        {
            string splKey1 = splKey;
            splKey = JsonConvert.DeserializeObject<string>(splKey);
            if (authtticateWebserviceCall(splKey))
            {
                string invoiceId = JsonConvert.DeserializeObject<string>(tokenId);
                string tokenMadeId = JsonConvert.DeserializeObject<string>(makerId);
                DataTable barcodeDt = JsonConvert.DeserializeObject<DataTable>(barcodeDet);
                invoiceCls obj = new invoiceCls();
                string retData = saveToken(splKey1, tokenId, barcodeDet, makerId, "Tellying");
                if(!retData.Equals("ERROR") && !retData.Equals("Authetication Failed"))
                {
                    //DataTable barcodeDtNew = JsonConvert.DeserializeObject<DataTable>(retData);
                    string res = obj.saveInvoice(invoiceId, barcodeDt, tokenMadeId,remarks);
                    return JsonConvert.SerializeObject(res);
                }
                else
                {
                    return JsonConvert.SerializeObject("Saving Invoice Failed");
                }
                
            }
            else
            {
                return JsonConvert.SerializeObject("Authetication Failed");
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
            return JsonConvert.SerializeObject("ERROR");
        }
        finally
        {
            Session.Abandon();
        }
    }

    [WebMethod(EnableSession = true)]
    public string vendorList(string splKey)
    {
        HttpContext.Current.Session["login"] = "AppDummy";
        HttpContext.Current.Session["userName"] = "AppDummy";
        try
        {
            // "ftko0ji9hu8"
            splKey = JsonConvert.DeserializeObject<string>(splKey);
            if (authtticateWebserviceCall(splKey))
            {

                styleCls obj = new styleCls();
                DataTable dt = obj.getTable("Vendor");

                return JsonConvert.SerializeObject(dt);
            }
            else
            {
                return JsonConvert.SerializeObject("Authetication Failed");
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
            return JsonConvert.SerializeObject("ERROR");
        }
        finally
        {
            Session.Abandon();
        }
    }

[WebMethod(EnableSession = true)]
    public string pickList(string splKey)
    {
        HttpContext.Current.Session["login"] = "AppDummy";
        HttpContext.Current.Session["userName"] = "AppDummy";
        try
        {
            // "ftko0ji9hu8"
            splKey = JsonConvert.DeserializeObject<string>(splKey);
            if (authtticateWebserviceCall(splKey))
            {

                pickListCls obj = new pickListCls();
                DataTable dt = obj.getPickList();

                return JsonConvert.SerializeObject(dt);
            }
            else
            {
                return JsonConvert.SerializeObject("Authetication Failed");
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
            return JsonConvert.SerializeObject("ERROR");
        }
        finally
        {
            Session.Abandon();
        }
    }

    [WebMethod(EnableSession = true)]
    public string generatePickList(string splKey, string customerId, string makerId)
    {
        HttpContext.Current.Session["login"] = "AppDummy";
        HttpContext.Current.Session["userName"] = "AppDummy";
        try
        {
            splKey = JsonConvert.DeserializeObject<string>(splKey);
            if (authtticateWebserviceCall(splKey))
            {
                string makerId1 = JsonConvert.DeserializeObject<string>(makerId);
                string customerId1 = JsonConvert.DeserializeObject<string>(customerId);
                pickListCls obj = new pickListCls();
                int res = obj.generatePickList(customerId1, makerId1);
                return JsonConvert.SerializeObject(res);
            }
            else
            {
                return JsonConvert.SerializeObject("Authetication Failed");
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
            return JsonConvert.SerializeObject("ERROR");
        }
        finally
        {
            Session.Abandon();
        }
    }

    [WebMethod(EnableSession = true)]
    public string getPickListBarcodes(string splKey, string tokenId)
    {
        HttpContext.Current.Session["login"] = "AppDummy";
        HttpContext.Current.Session["userName"] = "AppDummy";
        try
        {
          
            splKey = JsonConvert.DeserializeObject<string>(splKey);
            if (authtticateWebserviceCall(splKey))
            {
                string invoiceId = JsonConvert.DeserializeObject<string>(tokenId);
               

                pickListCls obj = new pickListCls();

                // check the status of token 
                utilityCls uObj = new utilityCls();
                DataTable invoice = uObj.getTableColwithID("invoice", "invid", invoiceId, "*");
                if (invoice.Rows.Count.Equals(0))
                {
                    return JsonConvert.SerializeObject("Wrong Token Number");
                }
                else
                {                    
                        DataTable barcodes = uObj.getTableColwithID("pickListTrans", "pickListFId", invoiceId, "*");
                    DataTable barcodeDt = new DataTable();
                    barcodeDt.Columns.Add("SKU");
                    barcodeDt.Columns.Add("Qty");
                    barcodeDt.Columns.Add("mrp");
                    barcodeDt.Columns.Add("count");
                    barcodeDt.Columns.Add("racks");
                        if (!barcodes.Rows.Count.Equals(0))
                        {
                        
                            foreach (DataRow dRow in barcodes.Rows)
                            {
                                string[] splitstyle = dRow["SKU"].ToString().Split('/');

                                DataTable dt = obj.getPickListData(splitstyle[0].ToString(), splitstyle[1].ToString());
                                if (!dt.Rows.Count.Equals(0))
                                {
                                  
                                barcodeDt.Rows.Add(dRow["SKU"].ToString(), dRow["quantity"].ToString(), dRow["quotedPrice"].ToString(), dt.Rows[0]["countMrp"].ToString(),
                                    dt.Rows[0]["racks"].ToString());
                                }
                            }

                           
                        }
                    return JsonConvert.SerializeObject(barcodeDt);                    
                }

            }
            else
            {
                return JsonConvert.SerializeObject("Authetication Failed");
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
            return JsonConvert.SerializeObject("ERROR");
        }
        finally
        {
            Session.Abandon();
        }
    }

    [WebMethod(EnableSession = true)]
    public string previewPickList(string splKey, string barcodeDet)
    {
        HttpContext.Current.Session["login"] = "AppDummy";
        HttpContext.Current.Session["userName"] = "AppDummy";
        try
        {
            /* DataTable s = new DataTable();
             s.Columns.Add("BarcodeNo");
             s.Rows.Add("0000/8/000");
             s.Rows.Add("0000/8/001");
             s.Rows.Add("0000/8/002");
             return JsonConvert.SerializeObject(s);*/
            splKey = JsonConvert.DeserializeObject<string>(splKey);
            if (authtticateWebserviceCall(splKey))
            {
                DataTable barcodeDt = JsonConvert.DeserializeObject<DataTable>(barcodeDet);
                //barcodeDt.Columns.Add("mrp");
                //barcodeDt.Columns.Add("count");
                //barcodeDt.Columns.Add("racks");
                utilityCls obj = new utilityCls();
                pickListCls pObj = new pickListCls();
                if (!barcodeDt.Rows.Count.Equals(0))
                {
                    foreach (DataRow dRow in barcodeDt.Rows)
                    {
                        string[] splitstyle = dRow["SKU"].ToString().Split('/');

                        DataTable dt = pObj.getPickListData(splitstyle[0].ToString(), splitstyle[1].ToString());
                        if (!dt.Rows.Count.Equals(0))
                        {
                            dRow["mrp"] = dt.Rows[0]["maxMrp"].ToString();
                            dRow["count"] = dt.Rows[0]["countMrp"].ToString();
                            dRow["racks"] = dt.Rows[0]["racks"].ToString();
                        }
                    }

                    return JsonConvert.SerializeObject(barcodeDt);
                }
                }
                else
                {
                    return JsonConvert.SerializeObject("Authetication Failed");
                }
            return JsonConvert.SerializeObject("ERROR");
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
            return JsonConvert.SerializeObject("ERROR");
        }
        finally
        {
            Session.Abandon();
        }
    }

    [WebMethod(EnableSession = true)]
    public string savePickList(string splKey, string barcodeDet,string pickListId,string userName,string userId)
    {
        HttpContext.Current.Session["login"] = "AppDummy";
        HttpContext.Current.Session["userName"] = "AppDummy";
        try
        {
            /* DataTable s = new DataTable();
             s.Columns.Add("BarcodeNo");
             s.Rows.Add("0000/8/000");
             s.Rows.Add("0000/8/001");
             s.Rows.Add("0000/8/002");
             return JsonConvert.SerializeObject(s);*/
            splKey = JsonConvert.DeserializeObject<string>(splKey);
            if (authtticateWebserviceCall(splKey))
            {
                //barcodeDet = "[{"BarcodeNo":"08AD/10/006","SKU":"08AD/10","Qty":5,"mrp":null,"count":null,"racks":null},{"BarcodeNo":"08AD/11/005","SKU":"08AD/11","Qty":1,"mrp":null,"count":null,"racks":null}]";
                DataTable barcodeDt = JsonConvert.DeserializeObject<DataTable>(barcodeDet);
                //barcodeDt.Columns.Add("mrp");
                //barcodeDt.Columns.Add("count");
                //barcodeDt.Columns.Add("racks");
                utilityCls obj = new utilityCls();
                pickListCls pObj = new pickListCls();
                if (!barcodeDt.Rows.Count.Equals(0))
                {
                    /*foreach (DataRow dRow in barcodeDt.Rows)
                    {
                        string[] splitstyle = dRow["BarcodeNo"].ToString().Split('/');

                        DataTable dt = pObj.getPickListData(splitstyle[0].ToString(), splitstyle[1].ToString());
                        if (!dt.Rows.Count.Equals(0))
                        {
                            dRow["mrp"] = dt.Rows[0]["maxMrp"].ToString();
                            dRow["count"] = dt.Rows[0]["countMrp"].ToString();
                            dRow["racks"] = dt.Rows[0]["racks"].ToString();
                        }
                    }*/
                    string res = pObj.savePickList(barcodeDt, pickListId, userName,userId);
                    return JsonConvert.SerializeObject(res);
                }
            }
            else
            {
                return JsonConvert.SerializeObject("Authetication Failed");
            }
            return JsonConvert.SerializeObject("ERROR");
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
            return JsonConvert.SerializeObject("ERROR");
        }
        finally
        {
            Session.Abandon();
        }
    }

[WebMethod(EnableSession = true)]
    public string getCustomer(string splKey)
    {
        try
        {
            // "ftko0ji9hu8"
            splKey = JsonConvert.DeserializeObject<string>(splKey);
            if (authtticateWebserviceCall(splKey))
            {
                HttpContext.Current.Session["login"] = "AppDummy";
                HttpContext.Current.Session["userName"] = "AppDummy";
                styleCls obj = new styleCls();
                DataTable dt = obj.getTable("websiteCustomer");
                Session.Abandon();
                return JsonConvert.SerializeObject(dt);
                //[{"BagId":1,"VendorID":1,"NoOfBags":"1","BagDescription":"1","IsActive":1,"amount":0.00,"mrp":5500.00,"discount":10.00,"Desc":null,"invoiceNo":"1","invoiceDate":"2019-09-07T00:00:00","taxAmount":111.00,"procurementAmount":111.00,"miscAmount":111.00,"oldsftqty":"0","oldsftamt":0.0,"Percentage":10.00,"totalPiece":20,"totalAmount":100.00,"paidStatus":true,"VendorName":"DreamzVision"},{"BagId":2,"VendorID":1,"NoOfBags":null,"BagDescription":"1","IsActive":1,"amount":0.00,"mrp":0.00,"discount":0.00,"Desc":null,"invoiceNo":"abc","invoiceDate":"2019-09-12T00:00:00","taxAmount":0.00,"procurementAmount":0.00,"miscAmount":0.00,"oldsftqty":null,"oldsftamt":null,"Percentage":null,"totalPiece":10,"totalAmount":250.00,"paidStatus":true,"VendorName":"DreamzVision"},{"BagId":3,"VendorID":1,"NoOfBags":null,"BagDescription":"test","IsActive":1,"amount":0.00,"mrp":0.00,"discount":0.00,"Desc":null,"invoiceNo":"2","invoiceDate":"2019-09-12T00:00:00","taxAmount":0.00,"procurementAmount":0.00,"miscAmount":0.00,"oldsftqty":null,"oldsftamt":null,"Percentage":null,"totalPiece":25,"totalAmount":258.00,"paidStatus":false,"VendorName":"DreamzVision"},{"BagId":5,"VendorID":1,"NoOfBags":null,"BagDescription":"a","IsActive":1,"amount":0.00,"mrp":0.00,"discount":0.00,"Desc":null,"invoiceNo":"aa","invoiceDate":"2019-09-16T00:00:00","taxAmount":0.00,"procurementAmount":0.00,"miscAmount":0.00,"oldsftqty":null,"oldsftamt":null,"Percentage":null,"totalPiece":1,"totalAmount":20.00,"paidStatus":false,"VendorName":"DreamzVision"},{"BagId":6,"VendorID":2,"NoOfBags":null,"BagDescription":"v1","IsActive":1,"amount":0.00,"mrp":0.00,"discount":0.00,"Desc":null,"invoiceNo":"1","invoiceDate":"2019-09-16T00:00:00","taxAmount":0.00,"procurementAmount":0.00,"miscAmount":0.00,"oldsftqty":null,"oldsftamt":null,"Percentage":null,"totalPiece":100,"totalAmount":250.00,"paidStatus":false,"VendorName":"Vendor"}]
            }
            else
            {
                return JsonConvert.SerializeObject("Authetication Failed");
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
            return JsonConvert.SerializeObject("ERROR");
        }
    }

    [WebMethod(EnableSession = true)]
    public string getCustomerWithID(string splKey,string custId)
    {
        try
        {
            // "ftko0ji9hu8"
            splKey = JsonConvert.DeserializeObject<string>(splKey);
            custId = JsonConvert.DeserializeObject<string>(custId);
            if (authtticateWebserviceCall(splKey))
            {
                HttpContext.Current.Session["login"] = "AppDummy";
                HttpContext.Current.Session["userName"] = "AppDummy";
                styleCls obj = new styleCls();
                DataTable dt = obj.getTableColwithID("websiteCustomer","webCustId", Convert.ToInt32(custId), "phoneNo");
                Session.Abandon();
                return JsonConvert.SerializeObject(dt);
                //[{"BagId":1,"VendorID":1,"NoOfBags":"1","BagDescription":"1","IsActive":1,"amount":0.00,"mrp":5500.00,"discount":10.00,"Desc":null,"invoiceNo":"1","invoiceDate":"2019-09-07T00:00:00","taxAmount":111.00,"procurementAmount":111.00,"miscAmount":111.00,"oldsftqty":"0","oldsftamt":0.0,"Percentage":10.00,"totalPiece":20,"totalAmount":100.00,"paidStatus":true,"VendorName":"DreamzVision"},{"BagId":2,"VendorID":1,"NoOfBags":null,"BagDescription":"1","IsActive":1,"amount":0.00,"mrp":0.00,"discount":0.00,"Desc":null,"invoiceNo":"abc","invoiceDate":"2019-09-12T00:00:00","taxAmount":0.00,"procurementAmount":0.00,"miscAmount":0.00,"oldsftqty":null,"oldsftamt":null,"Percentage":null,"totalPiece":10,"totalAmount":250.00,"paidStatus":true,"VendorName":"DreamzVision"},{"BagId":3,"VendorID":1,"NoOfBags":null,"BagDescription":"test","IsActive":1,"amount":0.00,"mrp":0.00,"discount":0.00,"Desc":null,"invoiceNo":"2","invoiceDate":"2019-09-12T00:00:00","taxAmount":0.00,"procurementAmount":0.00,"miscAmount":0.00,"oldsftqty":null,"oldsftamt":null,"Percentage":null,"totalPiece":25,"totalAmount":258.00,"paidStatus":false,"VendorName":"DreamzVision"},{"BagId":5,"VendorID":1,"NoOfBags":null,"BagDescription":"a","IsActive":1,"amount":0.00,"mrp":0.00,"discount":0.00,"Desc":null,"invoiceNo":"aa","invoiceDate":"2019-09-16T00:00:00","taxAmount":0.00,"procurementAmount":0.00,"miscAmount":0.00,"oldsftqty":null,"oldsftamt":null,"Percentage":null,"totalPiece":1,"totalAmount":20.00,"paidStatus":false,"VendorName":"DreamzVision"},{"BagId":6,"VendorID":2,"NoOfBags":null,"BagDescription":"v1","IsActive":1,"amount":0.00,"mrp":0.00,"discount":0.00,"Desc":null,"invoiceNo":"1","invoiceDate":"2019-09-16T00:00:00","taxAmount":0.00,"procurementAmount":0.00,"miscAmount":0.00,"oldsftqty":null,"oldsftamt":null,"Percentage":null,"totalPiece":100,"totalAmount":250.00,"paidStatus":false,"VendorName":"Vendor"}]
            }
            else
            {
                return JsonConvert.SerializeObject("Authetication Failed");
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex);
            return JsonConvert.SerializeObject("ERROR");
        }
    }
}



