using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Collections.Specialized;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
/// <summary>
/// Summary description for dispatchCls
/// </summary>
public class dispatchCls
{
    string userId = HttpContext.Current.Session["login"].ToString();//id of logged in admin

    public dispatchCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int dispatchItem(string StockupID ,string salesCourier, string salesAbwno, string invoiceId, string custname, string address1, string address2, string city, string stateID,string phoneNo)
    {
        
        
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("disItems");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {

            //insert into ArchiveStockUpInward
            command.CommandText = "INSERT INTO ArchiveStockUpInward (StyleID, UserID, BagID, SizeID, DateTime, LastBarcode, StyleCode, BarcodeNo, RFLQty, RejectQty, Status, LocationID, ExpiredDate, ListingDate, ModeOfPayment, SoldAmount, DispatchedDate, SalesDate, RackBarcode, RackDate, CancelReason, CancelDate, ListedUserID, Picked, BulkID, BulkDate, MoneyIn, MoneyOut, ItemID, SalesID, RecordNo, CourierTransactionID, mrp, StockupID,SystemDate,printed,oldBarcode,initialStatus,labels,mfgDate,labelUserId,physicalId,subLocId,rackId,stackId,piecePerPacket,isSample)" +
                " SELECT StyleID, UserID, BagID, SizeID, DateTime, LastBarcode, StyleCode, BarcodeNo, RFLQty, RejectQty, Status, LocationID, ExpiredDate, ListingDate, ModeOfPayment, SoldAmount, DispatchedDate, SalesDate, RackBarcode, RackDate, CancelReason, CancelDate, ListedUserID, Picked, BulkID, BulkDate, MoneyIn, MoneyOut, ItemID, SalesID, RecordNo, CourierTransactionID, mrp, StockupID,SystemDate,printed,oldBarcode,initialStatus,labels,mfgDate,labelUserId,physicalId,subLocId,rackId,stackId,piecePerPacket,isSample FROM StockUpInward WHERE StockupID=@StockupID " +
                " SELECT CAST(scope_identity() AS int)";
            command.Parameters.AddWithValue("@StockupID", StockupID);
           int archiveid = (Int32)command.ExecuteScalar();

            //Delete from StockUpInward
            command.CommandText = "DELETE FROM StockUpInward WHERE StockupID=@StockupID ";
            command.ExecuteNonQuery();

            //get sales details
            // get gst amount.. 
            command.CommandText = "select * from salesrecord where itemid=@ssStockupID and status='ND'";
            command.Parameters.AddWithValue("@ssStockupID", StockupID);
            DataTable salesDT = new DataTable();
            salesDT.Load(command.ExecuteReader());
            decimal igst = Convert.ToDecimal(salesDT.Rows[0]["igstamnt"]);
            decimal cgst = Convert.ToDecimal(salesDT.Rows[0]["cgstamnt"]);
            decimal sgst = Convert.ToDecimal(salesDT.Rows[0]["sgstamnt"]);
            decimal gst = igst + cgst + sgst;
            if (stateID.Equals("27"))
            {                
                cgst = (gst) / 2;
                sgst = (gst) / 2;
                igst = Convert.ToDecimal(0.0);
            }
            else
            {
                igst = gst;
                cgst = Convert.ToDecimal(0.0);
                sgst = Convert.ToDecimal(0.0);
            }

            //update sales record
            command.CommandText = "update salesrecord set status=@status,itemid=@itemid,archiveid=@archiveid,dispatchtimestamp=@dispatchtimestamp,follwUpTime=@follwUpTime,dispatchuserid=@dispatchuserid,salesCourier=@salesCourier,salesAbwno=@salesAbwno,igstamnt=@igstamnt,cgstamnt=@cgstamnt,sgstamnt=@sgstamnt WHERE itemid=@StockupID and status='ND'";

            command.Parameters.AddWithValue("@status", "DISPATCHED");
            command.Parameters.AddWithValue("@itemid", -1);
            command.Parameters.AddWithValue("@archiveid", archiveid);        
            command.Parameters.AddWithValue("@dispatchtimestamp", DateTime.Now);
            command.Parameters.AddWithValue("@follwUpTime", DateTime.Now);
            command.Parameters.AddWithValue("@dispatchuserid", userId);
            command.Parameters.AddWithValue("@salesCourier", salesCourier);
            command.Parameters.AddWithValue("@salesAbwno", salesAbwno);
            command.Parameters.AddWithValue("@igstamnt", igst);
            command.Parameters.AddWithValue("@cgstamnt", cgst);
            command.Parameters.AddWithValue("@sgstamnt", sgst);
            command.ExecuteNonQuery();

            //update invoice table
            command.CommandText = "update invoice set custname=@custname," +
                "address1=@address1,address2=@address2,city=@city,state=@state,phoneNo=@phoneNo where invid=@invid";
            command.Parameters.AddWithValue("@custname", custname);
            command.Parameters.AddWithValue("@address1", address1);
            command.Parameters.AddWithValue("@address2", address2);
            command.Parameters.AddWithValue("@city", city);
            command.Parameters.AddWithValue("@state", stateID);
            command.Parameters.AddWithValue("@phoneNo", phoneNo);
            command.Parameters.AddWithValue("@invid", invoiceId);
            command.ExecuteNonQuery();
            command.Parameters.Clear();

            // get location details
            DataTable locationDets = new DataTable();
            command.CommandText = "select sendSMS,Location from Location where LocationID=@LocationIDs";
            command.Parameters.AddWithValue("@LocationIDs", salesDT.Rows[0]["saleschannelvlocid"].ToString());
            locationDets.Load(command.ExecuteReader());

            // sms service
            DataTable sms = new DataTable();
            command.CommandText = "select * from smsService where smsid = @smsid";
            command.Parameters.AddWithValue("@smsid", 1);
            sms.Load(command.ExecuteReader());

            transaction.Commit();

            if (locationDets.Rows[0]["sendSMS"].ToString().Equals("Yes"))
            {               
                string msg = sms.Rows[0]["smsMessage"].ToString().Replace("{NAME}", custname);
                msg = msg.Replace("PORTAL", locationDets.Rows[0]["Location"].ToString());
                msg = msg.Replace("{SALESID}", salesDT.Rows[0]["salesidgivenbyvloc"].ToString());
                msg = msg.Replace("{ABWNO}", salesAbwno);
                string res = sendSMS(phoneNo, msg, sms.Rows[0]["apikey"].ToString(), sms.Rows[0]["smsSender"].ToString());
                //string newres = "[" + res + "]";
                /*string newres = "[{'balance':8,'batch_id':659716215,'cost':1,'num_messages':1,'message':{'num_parts':1,'sender':'TXTLCL','content':'This is your message'},'receipt_url':'','custom':'','messages':[{'id':'1830093154','recipient':918879858257}],'status':'success'}]";
                JavaScriptSerializer js = new JavaScriptSerializer();
                dynamic movie = js.Deserialize<dynamic>(newres);
                
                DataTable retSms = JsonConvert.DeserializeObject<DataTable>(movie[8]);*/

                //string str = "[{'balance':8,'batch_id':659716215,'cost':1,'num_messages':1,'message':{'num_parts':1,'sender':'TXTLCL','content':'This is your message'},'receipt_url':'','custom':'','messages':[{'id':'1830093154','recipient':918879858257}],'status':'success'}]";

                //string ext = res.Substring(res.LastIndexOf("'status':") + 1);
                string smsStats = "Yes";

                if (res.Contains("failure"))
                {
                    smsStats = "No";
                }

                // Start a local transaction.
                transaction = connection.BeginTransaction("sendSMS");
                command.Connection = connection;
                command.Transaction = transaction;
                command.CommandText = "update salesrecord set smsStatus=@smsStatus WHERE archiveid=@archiveIDsms and status='DISPATCHED'";
                command.Parameters.AddWithValue("@archiveIDsms", archiveid);
                command.Parameters.AddWithValue("@smsStatus", smsStats);
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            command.Parameters.Clear();
            if (connection.State == ConnectionState.Open)
                connection.Close();
            return 0;
        }
        catch (Exception ex)
        {
            try
            {
                transaction.Rollback();
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex);
                return -1;
            }
            catch (Exception ex2)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex2);
                return -1;

            }
        }

    }

    public string sendSMS(string numbers,string msg,string apikey, string smsSender)
    {     
        string result = string.Empty;
        try
        {
            String message = HttpUtility.UrlEncode(msg);
            using (var wb = new WebClient())
            {
                byte[] response = wb.UploadValues("https://api.textlocal.in/send/", new NameValueCollection()
                {
                {"apikey" , apikey},
                {"numbers" , numbers},
                {"message" , msg},
                {"sender" , smsSender}
                //{ "test","true"}
                });
                result = System.Text.Encoding.UTF8.GetString(response);                
            }
         
        }
        catch (Exception ex2)
        {
            RecordExceptionCls rex = new RecordExceptionCls();
            rex.recordException(ex2);
        }       
        return result;
    }

    /*public string SendSMS()
    {
        Uri targetUri = new Uri("http://sms.innovatorwebsolutions.com/smsstatuswithid.aspx?mobile=9819019621&pass=Kabrastan420&senderid=KAZMIO&to=8879858257&msg=hellosaima");
        HttpWebRequest webRequest = (HttpWebRequest)System.Net.HttpWebRequest.Create(targetUri);
        webRequest.Method = WebRequestMethods.Http.Get;
        try
        {
            string webResponse = string.Empty;
            using (HttpWebResponse getresponse =(HttpWebResponse) webRequest.GetResponse())
            {
                using (StreamReader reader = new
                StreamReader(getresponse.GetResponseStream()))
                {
                    webResponse = reader.ReadToEnd();
                    reader.Close();
                }
                getresponse.Close();
            }
            return webResponse;
        }
        catch (System.Net.WebException ex)
        {
            return "Request-Timeout";
        }
        catch (Exception ex)
        {
            return "error";
        }
        finally { webRequest.Abort(); }
    }*/

    public int cancleItem(string StockupID,string invoiceId,string sid,string status, string Barcode, string reasons, string cancleReason)
    {
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("canItems");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            string printed = "No";
            if(status.Equals("RFL")) // no change in status
            {
                // update stockupInwatd Status
                command.CommandText = "update StockUpInward set status=@status where StockupID=@StockupID";
                command.Parameters.AddWithValue("@StockupID", StockupID);
                command.Parameters.AddWithValue("@status", status);
                command.ExecuteNonQuery();
            }
            else
            {
                command.CommandText = "update StockUpInward set status=@status1,printed=@printed where StockupID=@StockupID111";
                command.Parameters.AddWithValue("@StockupID111", StockupID);
                command.Parameters.AddWithValue("@status1", status);
                command.Parameters.AddWithValue("@printed", printed);
                command.ExecuteNonQuery();
            }
            

            command.CommandText = "select * from salesrecord WHERE sid=@sid12";
            command.Parameters.AddWithValue("@sid12", sid);
            DataTable dt = new DataTable();
            dt.Load(command.ExecuteReader());

            // select data from sales id and add to transaction
            command.CommandText = "insert into cancelTrans (sid,invoiceid,saleschannelvlocid,salesidgivenbyvloc,sellingprice,status,itemid,archiveid,recordtimestamp,dispatchtimestamp,dispatchuserid,gstpercent," +
                "taxableamount,cgstamnt,sgstamnt,igstamnt,salesCourier,salesAbwno,returntimestamp,returnuserid,returnCourier,returnAbwno,reasons,remarks,salesDateTime,salesUserId,canReason,cancelId," +
                "cancleReason,changeStatus,rImage1,rImage2,rImage3,rImage4,rImage5,rImage6,rImage7,rImage8,rImage9,rImage10,rVideo1,rVideo2)" +
                    " values " +
                    "(@sid1,@invoiceid1,@saleschannelvlocid,@salesidgivenbyvloc,@sellingprice,@status2,@itemid,@archiveid," +
                    "@recordtimestamp,@dispatchtimestamp,@dispatchuserid,@gstpercent," +
                    "@taxableamount,@cgstamnt,@sgstamnt,@igstamnt,@salesCourier,@salesAbwno,@returntimestamp," +
                    "@returnuserid,@returnCourier,@returnAbwno,@reasons,@remarks,@salesDateTime,@salesUserId," +
                    "@canReason,@cancelId,@cancleReason,@changeStatus,@rImage1,@rImage2,@rImage3,@rImage4,@rImage5,@rImage6,@rImage7,@rImage8,@rImage9,@rImage10,@rVideo1,@rVideo2)";
            command.Parameters.AddWithValue("@sid1", sid);
            command.Parameters.AddWithValue("@saleschannelvlocid", dt.Rows[0]["saleschannelvlocid"].ToString());
            command.Parameters.AddWithValue("@invoiceid1", dt.Rows[0]["invoiceid"].ToString());
            command.Parameters.AddWithValue("@salesidgivenbyvloc", dt.Rows[0]["salesidgivenbyvloc"].ToString());
            command.Parameters.AddWithValue("@sellingprice", Convert.ToDecimal(dt.Rows[0]["sellingprice"].ToString()));
            command.Parameters.AddWithValue("@status2", dt.Rows[0]["status"].ToString());
            command.Parameters.AddWithValue("@itemid", dt.Rows[0]["itemid"].ToString());
            command.Parameters.AddWithValue("@archiveid", dt.Rows[0]["archiveid"].ToString());
            command.Parameters.AddWithValue("@recordtimestamp", Convert.ToDateTime(dt.Rows[0]["recordtimestamp"]).ToString("yyyy-MM-dd HH:mm:ss.mmm"));
            command.Parameters.AddWithValue("@dispatchtimestamp", dt.Rows[0]["dispatchtimestamp"]);
            command.Parameters.AddWithValue("@dispatchuserid", dt.Rows[0]["dispatchuserid"].ToString());
            command.Parameters.AddWithValue("@gstpercent", dt.Rows[0]["gstpercent"].ToString());
            command.Parameters.AddWithValue("@taxableamount", dt.Rows[0]["taxableamount"].ToString());
            command.Parameters.AddWithValue("@cgstamnt", dt.Rows[0]["cgstamnt"].ToString());
            command.Parameters.AddWithValue("@sgstamnt", dt.Rows[0]["sgstamnt"].ToString());
            command.Parameters.AddWithValue("@igstamnt", dt.Rows[0]["igstamnt"].ToString());
            command.Parameters.AddWithValue("@salesCourier", dt.Rows[0]["salesCourier"].ToString());
            command.Parameters.AddWithValue("@salesAbwno", dt.Rows[0]["salesAbwno"].ToString());
            command.Parameters.AddWithValue("@returntimestamp", dt.Rows[0]["returntimestamp"]);
            command.Parameters.AddWithValue("@returnuserid", dt.Rows[0]["returnuserid"].ToString());
            command.Parameters.AddWithValue("@returnCourier", dt.Rows[0]["returnCourier"].ToString());
            command.Parameters.AddWithValue("@returnAbwno", dt.Rows[0]["returnAbwno"].ToString());
            command.Parameters.AddWithValue("@reasons", dt.Rows[0]["reasons"].ToString());
            command.Parameters.AddWithValue("@remarks", dt.Rows[0]["remarks"].ToString());
            command.Parameters.AddWithValue("@salesDateTime", Convert.ToDateTime(dt.Rows[0]["salesDateTime"]).ToString("yyyy-MM-dd HH:mm:ss.mmm"));
            command.Parameters.AddWithValue("@salesUserId", dt.Rows[0]["salesUserId"].ToString());
            command.Parameters.AddWithValue("@canReason", reasons);
            command.Parameters.AddWithValue("@cancelId", userId);
            command.Parameters.AddWithValue("@cancleReason", cancleReason);
            command.Parameters.AddWithValue("@changeStatus", status);
            command.Parameters.AddWithValue("@rImage1", dt.Rows[0]["rImage1"].ToString());
            command.Parameters.AddWithValue("@rImage2", dt.Rows[0]["rImage2"].ToString());
            command.Parameters.AddWithValue("@rImage3", dt.Rows[0]["rImage3"].ToString());
            command.Parameters.AddWithValue("@rImage4", dt.Rows[0]["rImage4"].ToString());
            command.Parameters.AddWithValue("@rImage5", dt.Rows[0]["rImage5"].ToString());
            command.Parameters.AddWithValue("@rImage6", dt.Rows[0]["rImage6"].ToString());
            command.Parameters.AddWithValue("@rImage7", dt.Rows[0]["rImage7"].ToString());
            command.Parameters.AddWithValue("@rImage8", dt.Rows[0]["rImage8"].ToString());
            command.Parameters.AddWithValue("@rImage9", dt.Rows[0]["rImage9"].ToString());
            command.Parameters.AddWithValue("@rImage10", dt.Rows[0]["rImage10"].ToString());
            command.Parameters.AddWithValue("@rVideo1", dt.Rows[0]["rVideo1"].ToString());
            command.Parameters.AddWithValue("@rVideo2", dt.Rows[0]["rVideo2"].ToString());
            command.ExecuteNonQuery();

            // delete from salesrecord
            command.CommandText = "DELETE FROM salesrecord WHERE sid=@sid";
            command.Parameters.AddWithValue("@sid", sid);
            command.ExecuteNonQuery();

            //  find current total cost and total records
            command.CommandText = "select isnull(count(sid),0) as cnt,isnull(sum(sellingprice),0) as totAmnt from salesrecord where invoiceid=@invoiceid";
            command.Parameters.AddWithValue("@invoiceid", invoiceId);
            DataTable dt1 = new DataTable();
            dt1.Load(command.ExecuteReader());

            command.CommandText = "update invoice set total=@total where invid=@invid1";
            command.Parameters.AddWithValue("@total", Convert.ToDecimal(dt1.Rows[0]["totAmnt"]));
            command.Parameters.AddWithValue("@invid1", invoiceId);
            command.ExecuteNonQuery();

            /*if (Convert.ToInt32(dt1.Rows[0]["cnt"]).Equals(Convert.ToInt32(0)))
            {
                // if total record is 0 than delete invoice

                command.CommandText = "DELETE FROM invoice WHERE invid=@invid";
                command.Parameters.AddWithValue("@invid", invoiceId);
                command.ExecuteNonQuery();
            }
            else
            {
                command.CommandText = "update invoice set total=@total where invid=@invid1";
                command.Parameters.AddWithValue("@total", Convert.ToDecimal(dt1.Rows[0]["totAmnt"]));
                command.Parameters.AddWithValue("@invid1", invoiceId);
                command.ExecuteNonQuery();
            }*/
                          
                transaction.Commit();
                command.Parameters.Clear();
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                return 0;
            
            
            
        }
        catch (Exception ex)
        {
            try
            {
                transaction.Rollback();
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex);
                return -1;
            }
            catch (Exception ex2)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex2);
                return -1;

            }
        }
    }
}