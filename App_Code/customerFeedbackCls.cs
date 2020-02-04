using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for customerFeedbackCls
/// </summary>
public class customerFeedbackCls
{
    string userId = HttpContext.Current.Session["login"].ToString();//id of logged in admin
    string userName = HttpContext.Current.Session["userName"].ToString();//name of logged in user
    public customerFeedbackCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable getFeedbacks(string fromDate, string toDate, string salesid, string barcode, string custname, 
        string phoneNo, bool dateRange, bool salesCheck, bool barcodeCheck, bool customerCheck, bool phoneNoCheck,bool followup, string fromDateF, string toDateF)
    {
        DataTable invTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("getFeedbacks");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            string dateR = string.Empty;
            string dateRA = string.Empty;
            if (dateRange.Equals(true))
            {
                dateR = " and (CAST (dispatchtimestamp AS date) between @fromDate and @toDate)";
                //dateRA = " and (CAST (dispatchtimestamp AS date) between @fromDate1 and @toDate1)";
                command.Parameters.AddWithValue("@fromDate", fromDate);
                command.Parameters.AddWithValue("@toDate", toDate);
                
            }
            if (followup.Equals(true))
            {
                //dateR = " and (CAST (dispatchtimestamp AS date) between @fromDate and @toDate)";
                dateRA = " and (CAST (follwUpTime AS date) between @fromDate1 and @toDate1)";
                command.Parameters.AddWithValue("@fromDate1", fromDateF);
                command.Parameters.AddWithValue("@toDate1", toDateF);

            }
            string query = string.Empty;
            //query += "select * from (select st.BarcodeNo,s.salesidgivenbyvloc,s.itemid,l.Location,t.*,s.saleschannelvlocid from (select * from ticket_master "+ dateR + " ) t inner join salesrecord s on s.sid=t.salesid " +
            //"inner join StockUpInward st on s.itemid != -1 and st.StockupID = s.itemid inner join Location l on l.LocationID = s.saleschannelvlocid " +
            //"union all " +
            // "select st1.BarcodeNo,s1.salesidgivenbyvloc,s1.itemid,l1.Location,t1.*,s1.saleschannelvlocid from (select * from ticket_master  " + dateRA + " ) t1 inner join salesrecord s1 on s1.sid=t1.salesid " +
            //"inner join ArchiveStockUpInward st1 on s1.archiveid != -1 and st1.ArchiveStockupID = s1.archiveid inner join Location l1 on l1.LocationID = s1.saleschannelvlocid ) a";

            query += "select a.sid,sty.Title,col1.C1Name,a.salesidgivenbyvloc,a.sellingprice,a.dispatchtimestamp,a.salesAbwno,st.BarcodeNo,i.custname,i.phoneNo,a.customerStatus," +
                "a.callingStatus,a.smsStatus,a.whatsappStatus,a.delStatus,a.deliveryDate,a.contactStatus,l1.username as call1user,a.callOneDateime,a.callOneStatus," +
                "l2.username as call2user,a.callTwoDatetime,a.callTwoStatus,l3.username as call3user,a.callThreeDatetime,a.callThreeStatus," +
                "l4.username as call4user,a.callFourDatetime,a.callFourStatus,l5.username as call5user,a.callFiveDatetime,a.callFiveStatus,a.follwUpTime,a.callingCount from " +
                "(select * from salesrecord s where archiveid != '-1' "+ dateR + "" + dateRA + " and s.saleschannelvlocid = 3) a" +
                " inner join ArchiveStockUpInward st on st.ArchiveStockupID = a.archiveid inner join invoice i on i.invid = a.invoiceid" +
                " inner join ItemStyle sty on sty.StyleID = st.StyleID inner join Column1 col1 on col1.Col1ID = sty.Col1 left join login l1 on l1.userid=a.callOneUserId left join login l2 on l2.userid=a.callTwoUserId left join login l3 on l3.userid=a.callThreeUserId left join login l4 on l4.userid=a.callFourUserId " +
                "left join login l5 on l5.userid = a.callFiveUserId";
            
            string conV = string.Empty;
            string where = "0";
            if (!customerCheck.Equals("-1") && customerCheck.Equals(true))
            {
                if (where.Equals("0"))
                {
                    conV += " where i.custname like '%@custname%'";
                    where = "1";
                }
                else
                {
                    conV += " and i.custname like '%@custname%'";
                }
                command.Parameters.AddWithValue("@custname", custname);
            }

            if (!salesid.Equals("") && salesCheck.Equals(true))
            {
                if (where.Equals("0"))
                {
                    conV += " where a.salesidgivenbyvloc=@salesid";
                    where = "1";
                }
                else
                {
                    conV += " and a.salesidgivenbyvloc=@salesid";
                }
                command.Parameters.AddWithValue("@salesid", salesid);
            }

            if (!barcode.Equals("") && barcodeCheck.Equals(true))
            {
                if (where.Equals("0"))
                {
                    conV += " where st.BarcodeNo=@barcode";
                    where = "1";
                }
                else
                {
                    conV += " and st.BarcodeNo=@barcode";
                }
                command.Parameters.AddWithValue("@barcode", barcode);
            }

            if (!phoneNoCheck.Equals("") && phoneNoCheck.Equals(true))
            {
                if (where.Equals("0"))
                {
                    conV += " where i.phoneNo like '%@phoneNo%'";
                    where = "1";
                }
                else
                {
                    conV += " and i.phoneNo like '%@phoneNo%'";
                }
                command.Parameters.AddWithValue("@phoneNo", phoneNo);
            }


            command.CommandText = query + conV;
            invTable.Load(command.ExecuteReader());

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        return invTable;
    }

    public int saveFeedback(string callingCount1, string sid, string custStatus, string conStatus,string delStatus,string delDate,string callingStatus1,string followUp)
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
        transaction = connection.BeginTransaction("saveFeedB");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            string query = "update salesrecord set delStatus=@deliveryStatus,customerStatus=@customerStatus,contactStatus=@contactStatus";
            command.Parameters.AddWithValue("@deliveryStatus", delStatus);
            command.Parameters.AddWithValue("@customerStatus", custStatus);
            command.Parameters.AddWithValue("@contactStatus", conStatus);
            if (callingCount1.Equals("0"))
            {
                query += ",follwUpTime=@follwUpTime,callOneStatus=@callOneStatus,callOneDateime=@callOneDateime,callOneUserId=@callOneUserId,callingStatus=@callingStatus,callingCount=@callingCount";
                if (followUp.Equals(""))
                {
                    command.Parameters.AddWithValue("@follwUpTime", followUp);
                }
                else
                {
                    command.Parameters.AddWithValue("@follwUpTime", Convert.ToDateTime(followUp).ToString("yyyy-MM-dd HH:mm:ss"));
                }
                
                command.Parameters.AddWithValue("@callOneStatus", conStatus+" and "+ custStatus + " - Next Follow Up : "+ Convert.ToDateTime(followUp).ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@callOneDateime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@callOneUserId", userId);
                command.Parameters.AddWithValue("@callingStatus", "Call1 - "+callingStatus1);
                command.Parameters.AddWithValue("@callingCount", 1);
            }
            if (callingCount1.Equals("1"))
            {
                query += ",follwUpTime=@follwUpTime,callTwoStatus=@callTwoStatus,callTwoDatetime=@callTwoDateime,callTwoUserId=@callTwoUserId,callingStatus+=@callingStatus,callingCount=@callingCount,deliveryDate=@deliveryDate";
                if (followUp.Equals(""))
                {
                    command.Parameters.AddWithValue("@follwUpTime", followUp);
                }
                else
                {
                    command.Parameters.AddWithValue("@follwUpTime", Convert.ToDateTime(followUp).ToString("yyyy-MM-dd HH:mm:ss"));
                }
                //command.Parameters.AddWithValue("@follwUpTime", Convert.ToDateTime(followUp).ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@callTwoStatus", conStatus + " and " + custStatus + " - Next Follow Up : " + Convert.ToDateTime(followUp).ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@callTwoDateime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@callTwoUserId", userId);
                command.Parameters.AddWithValue("@callingStatus", ",Call2 - " + callingStatus1);
                command.Parameters.AddWithValue("@callingCount", 2);
                command.Parameters.AddWithValue("@deliveryDate", Convert.ToDateTime(delDate).ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (callingCount1.Equals("2"))
            {
                query += ",follwUpTime=@follwUpTime,callThreeStatus=@callThreeStatus,callThreeDatetime=@callThreeDateime,callThreeUserId=@callThreeUserId,callingStatus+=@callingStatus,callingCount=@callingCount";
                command.Parameters.AddWithValue("@follwUpTime", Convert.ToDateTime(followUp).ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@callThreeStatus", conStatus + " and " + custStatus + " - Next Follow Up : " + Convert.ToDateTime(followUp).ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@callThreeDateime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@callThreeUserId", userId);
                command.Parameters.AddWithValue("@callingStatus", ",Call3 - " + callingStatus1);
                command.Parameters.AddWithValue("@callingCount", 3);
            }
            if (callingCount1.Equals("3"))
            {
                query += ",follwUpTime=@follwUpTime,callFourStatus=@callFourStatus,callFourDatetime=@callFourDatetime,callFourUserId=@callFourUserId,callingStatus+=@callingStatus,callingCount=@callingCount";
                command.Parameters.AddWithValue("@follwUpTime", Convert.ToDateTime(followUp).ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@callFourStatus", conStatus + " and " + custStatus + " - Next Follow Up : " + Convert.ToDateTime(followUp).ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@callFourDatetime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@callFourUserId", userId);
                command.Parameters.AddWithValue("@callingStatus", ",Call4 - " + callingStatus1);
                command.Parameters.AddWithValue("@callingCount", 4);
            }
            if (callingCount1.Equals("4"))
            {
                query += ",callFiveStatus=@callFiveStatus,callFiveDatetime=@callFiveDatetime,callFiveUserId=@callFiveUserId,callingStatus+=@callingStatus,callingCount=@callingCount";
                
                command.Parameters.AddWithValue("@callFiveStatus", conStatus + " and " + custStatus );
                command.Parameters.AddWithValue("@callFiveDatetime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@callFiveUserId", userId);
                command.Parameters.AddWithValue("@callingStatus", ",Call5 - " + callingStatus1);
                command.Parameters.AddWithValue("@callingCount", 5);
            }

            query += " where sid=@sid";
            command.Parameters.AddWithValue("@sid", sid);

            command.CommandText = query;

            command.ExecuteNonQuery();
            transaction.Commit();
            command.Parameters.Clear();

            if (connection.State == ConnectionState.Open)
                connection.Close();
            return 1;
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