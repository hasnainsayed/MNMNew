using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


/// <summary>
/// Summary description for lotPaymentCls
/// </summary>
public class lotPaymentCls
{
    public lotPaymentCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable getPaymentByLot(string lotId)
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
        transaction = connection.BeginTransaction("getLotById");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "SELECT * FROM (SELECT *,CASE WHEN paymentMode = 1 THEN 'Cash' when paymentMode = 2 THEN 'Transfer' when paymentMode = 3 THEN 'Cheque' ELSE '' END AS modeName,(SELECT centreName FROM accountingCentre WHERE centreId=paymentCentre) AS centreName  FROM lotPayment where lotId=@lotId " +
                "UNION ALL " +
                "SELECT g.ghostId AS paymentId,g.ghostDate AS paymentDate,'' AS paymentMode,'' AS paymentTransaction, g.ghostAmount AS paymentAmount,'Adjusted from Suspense Amount' AS paymentRemarks, g.lotId,g.makerId,g.entryDateTime AS entryDatetime,'' AS paymentCentre,'' AS modeName,'' AS centreName,'' as vendorId " +
                "FROM ghostPayment g WHERE lotId = @lotId1) a ORDER BY paymentDate desc";
            command.Parameters.AddWithValue("@lotId", lotId);
            command.Parameters.AddWithValue("@lotId1", lotId);
            catTable.Load(command.ExecuteReader());

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
        return catTable;
    }

    public DataTable getPaymentByInvoice(string invoiceId)
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
        transaction = connection.BeginTransaction("getInvPay");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "SELECT * FROM (SELECT *,CASE WHEN paymentMode = 1 THEN 'Cash' when paymentMode = 2 THEN 'Transfer' when paymentMode = 3 THEN 'Cheque' ELSE '' END AS modeName,(SELECT centreName FROM accountingCentre WHERE centreId=paymentCentre) AS centreName  FROM invoicePayment where invoiceId=@lotId " +
                "UNION ALL " +
                "SELECT g.ghostId AS paymentId,g.ghostDate AS paymentDate,'' AS paymentMode,'' AS paymentTransaction, g.ghostAmount AS paymentAmount,'Adjusted from Suspense Amount' AS paymentRemarks, g.invoiceId,g.makerId,g.entryDateTime AS entryDatetime,'' AS paymentCentre,'' AS modeName,'' AS centreName,'' as vendorId " +
                "FROM ghostInvoicePayment g WHERE invoiceId = @lotId1) a ORDER BY paymentDate desc";
            command.Parameters.AddWithValue("@lotId", invoiceId);
            command.Parameters.AddWithValue("@lotId1", invoiceId);
            catTable.Load(command.ExecuteReader());

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
        return catTable;
    }

    public DataTable getSuspenseReceivable(string custId)
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
        transaction = connection.BeginTransaction("getInvPay");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "SELECT * FROM ( SELECT p.paymentDate, p.paymentTransaction, p.paymentAmount, p.paymentRemarks, p.invoiceId, CASE WHEN paymentMode = 1 THEN 'Cash' when paymentMode = 2 THEN 'Transfer' when paymentMode = 3 THEN 'Cheque' ELSE '' END AS modeName, (SELECT centreName FROM accountingCentre WHERE centreId = paymentCentre) AS centreName " +
                "FROM invoicePayment p " +
                "WHERE customerId = @customerId AND p.invoiceId IS NULL UNION ALL " +
                "  SELECT g.ghostDate AS paymentDate,'' AS paymentTransaction, CONCAT('-', g.ghostAmount) AS paymentAmount,'Adjusted from Suspense Amount' AS paymentRemarks, g.invoiceId,'' AS modeName,'' AS centreName " +
                "FROM ghostInvoicePayment g " +
                "INNER JOIN invoice iv ON iv.invid = g.invoiceId AND iv.webCustomer=@customerId1) a  ORDER BY paymentDate ASC";
            command.Parameters.AddWithValue("@customerId", custId);
            command.Parameters.AddWithValue("@customerId1", custId);
            catTable.Load(command.ExecuteReader());

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
        return catTable;
    }

    public DataTable getSuspensePayable(string venId)
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
        transaction = connection.BeginTransaction("getInvPay");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "SELECT * FROM(SELECT p.paymentDate, p.paymentTransaction, p.paymentAmount, p.paymentRemarks, p.lotId as invoiceId, CASE WHEN paymentMode = 1 THEN 'Cash' when paymentMode = 2 THEN 'Transfer' when paymentMode = 3 THEN 'Cheque' ELSE '' END AS modeName, (SELECT centreName FROM accountingCentre WHERE centreId = paymentCentre) AS centreName " +
                "FROM lotPayment p " +
                "WHERE p.vendorId = @vendorId AND p.lotId IS NULL UNION ALL  SELECT g.ghostDate AS paymentDate,'' AS paymentTransaction, CONCAT('-', g.ghostAmount) AS paymentAmount,'Adjusted from Suspense Amount' AS paymentRemarks, g.lotId as invoiceId,'' AS modeName,'' AS centreName " +
                "FROM ghostPayment g " +
                "INNER JOIN Lot iv ON iv.BagId = g.lotId AND iv.VendorID = @vendorId1) a ORDER BY paymentDate ASC";
            command.Parameters.AddWithValue("@vendorId", venId);
            command.Parameters.AddWithValue("@vendorId1", venId);
            catTable.Load(command.ExecuteReader());

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
        return catTable;
    }

    public DataTable getReceivable(string custId)
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
        transaction = connection.BeginTransaction("rec");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "SELECT ISNULL(SUM(totalAmount-paidAmount-ghostAmount),0) AS pendingAmount,totalAmount,invoiceNo,invoiceDate,dayDifference,paymentStatus, ISNULL(SUM(paidAmount+ghostAmount),0) AS totalPaidAmount " +
                "FROM(SELECT l.total as totalAmount, l.invid as invoiceNo, l.salesDate as invoiceDate, paymentStatus, " +
                "            CASE WHEN l.paymentStatus = 'paid' THEN '' ELSE DATEDIFF(DAY, l.salesDate, GETDATE()) END AS dayDifference, (" +
                "SELECT ISNULL(SUM(p.paymentAmount), 0) FROM invoicePayment p WHERE p.invoiceId = l.invid) AS paidAmount,( " +
                "SELECT ISNULL(SUM(g.ghostAmount), 0) FROM ghostInvoicePayment g WHERE g.invoiceId = l.invid) AS ghostAmount " +
                "FROM invoice l " +
                "WHERE l.webCustomer = @custId) a GROUP BY totalAmount, invoiceNo, invoiceDate, dayDifference, paymentStatus";
            command.Parameters.AddWithValue("@custId", custId);
            catTable.Load(command.ExecuteReader());

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
        return catTable;
    }

    public DataTable getVendorPayable(string vendorId)
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
        transaction = connection.BeginTransaction("venPay");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "SELECT ISNULL(SUM(totalAmount-paidAmount-ghostAmount),0) AS pendingAmount,BagId,BagDescription,totalAmount,invoiceNo,invoiceDate,dayDifference,paymentStatus,ISNULL(SUM(paidAmount+ghostAmount),0) AS totalPaidAmount FROM (SELECT l.BagId,l.BagDescription,l.totalAmount,l.invoiceNo,l.invoiceDate,CASE WHEN l.paidStatus='1' THEN '' ELSE DATEDIFF(Day, l.invoiceDate, GETDATE()) END AS dayDifference,CASE WHEN l.paidStatus='1' THEN 'Paid' ELSE 'Unpaid' END AS paymentStatus,(SELECT ISNULL(SUM(p.paymentAmount), 0) FROM lotPayment p WHERE p.lotId = l.BagId) AS paidAmount,(SELECT ISNULL(SUM(g.ghostAmount), 0) FROM ghostPayment g WHERE g.lotId = l.BagId) AS ghostAmount FROM Lot l WHERE VendorID = @vendorId) a GROUP BY BagId, BagDescription, totalAmount, invoiceNo, invoiceDate, dayDifference, paymentStatus";
            command.Parameters.AddWithValue("@vendorId", vendorId);
            catTable.Load(command.ExecuteReader());

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
        return catTable;
    }

    public DataTable getAllPayables()
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
        transaction = connection.BeginTransaction("allPay");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "SELECT VendorID,VendorName,isnull(SUM(checkOneAmount+checkTwoAmount+checkThreeAmount+checkFourAmount),0) AS totalAmount,isnull(SUM(checkOneAmount),0) AS checkOneAmount,isnull(SUM(checkTwoAmount),0) AS checkTwoAmount,isnull(SUM(checkThreeAmount),0) AS checkThreeAmount,isnull(SUM(checkFourAmount),0) AS checkFourAmount FROM (SELECT l.VendorID,v.VendorName,isnull(sum(l.totalAmount),0) AS totalAmount," +
                "case WHEN((DATEDIFF(Day, l.invoiceDate, GETDATE()) <= (SELECT c.checkOne FROM paymentCheckPoint c)) AND(DATEDIFF(DAY, l.invoiceDate, GETDATE()) > 0)) THEN isnull(sum(l.totalAmount),0) ELSE 0 END AS checkOneAmount," +
                "case WHEN((DATEDIFF(Day, l.invoiceDate, GETDATE()) <= (SELECT c.checkTwo FROM paymentCheckPoint c)) AND(DATEDIFF(DAY, l.invoiceDate, GETDATE()) > (SELECT c.checkOne FROM paymentCheckPoint c))) THEN isnull(sum(l.totalAmount),0) ELSE 0 END AS checkTwoAmount," +
                "case WHEN((DATEDIFF(Day, l.invoiceDate, GETDATE()) <= (SELECT c.checkThree FROM paymentCheckPoint c)) AND(DATEDIFF(DAY, l.invoiceDate, GETDATE()) > (SELECT c.checkTwo FROM paymentCheckPoint c))) THEN isnull(sum(l.totalAmount),0) ELSE 0 END AS checkThreeAmount," +
                "case WHEN(DATEDIFF(DAY, l.invoiceDate, GETDATE()) > (SELECT c.checkThree FROM paymentCheckPoint c)) THEN isnull(sum(l.totalAmount),0) ELSE 0 END AS checkFourAmount," +
                "(SELECT ISNULL(SUM(p.paymentAmount), 0) FROM lotPayment p WHERE p.vendorId = l.VendorID and l.paidStatus = 0 AND p.lotId IS not null) AS paidAmount,(SELECT ISNULL(SUM(p.paymentAmount), 0) FROM lotPayment p WHERE p.vendorId = l.VendorID and p.lotId IS null) AS suspenseAmount FROM Lot l INNER JOIN Vendor v ON v.VendorID = l.VendorID and l.paidStatus = 0 GROUP BY l.VendorID,v.VendorName,v.VendorID,l.paidStatus,invoiceDate) a GROUP BY VendorID, VendorName ORDER BY totalAmount desc";
            catTable.Load(command.ExecuteReader());

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
        return catTable;
    }

    public int saveAddPayment(string paymentCentre, string paymentDate, string paymentMode, string paymentAmount,
        string paymentRemarks, string paymentTransaction, string makerId, string vendorId)
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
        transaction = connection.BeginTransaction("addPay");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            int result = 0;

            command.CommandText = "insert into lotPayment (paymentDate,paymentMode,paymentTransaction,paymentAmount,paymentRemarks,paymentCentre,vendorId) values " +
                "(@paymentDate,@paymentMode,@paymentTransaction,@paymentAmount,@paymentRemarks,@paymentCentre,@vendorId)";
            command.Parameters.AddWithValue("@paymentDate", Convert.ToDateTime(paymentDate).ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@paymentMode", paymentMode);
            command.Parameters.AddWithValue("@paymentTransaction", paymentTransaction);
            command.Parameters.AddWithValue("@paymentAmount", Convert.ToDecimal(paymentAmount));
            command.Parameters.AddWithValue("@paymentRemarks", paymentRemarks);            
            command.Parameters.AddWithValue("@makerId", makerId);
            command.Parameters.AddWithValue("@paymentCentre", paymentCentre);
            command.Parameters.AddWithValue("@vendorId", vendorId);
            command.ExecuteNonQuery();

            // get all unpaid LOT
            DataTable Lot = new DataTable();
            command.CommandText = "select BagId,totalAmount from Lot where VendorID=@vendorId and paidStatus=@paidStatus";
            command.Parameters.AddWithValue("@paidStatus", 0);
            Lot.Load(command.ExecuteReader());

            decimal suspense = Convert.ToDecimal(paymentAmount);

            foreach (DataRow dRow in Lot.Rows)
            {
                string LotId = dRow["BagId"].ToString();
                decimal totalAmount = Convert.ToDecimal(dRow["totalAmount"]);

                DataTable suspenseAmount = new DataTable();
                command.CommandText = "SELECT ISNULL(SUM(suspenseAmount),0) AS suspenseAmount FROM (SELECT ISNULL(SUM(paymentAmount),0) AS suspenseAmount FROM lotPayment WHERE lotId=@lotId " +
                    "union ALL " +
                    "SELECT ISNULL(SUM(ghostAmount),0) AS suspenseAmount FROM ghostPayment WHERE lotId = @lotId1) a";
                command.Parameters.AddWithValue("@lotId1", LotId);
                command.Parameters.AddWithValue("@lotId", LotId);
                suspenseAmount.Load(command.ExecuteReader());

                decimal pendingAmount = totalAmount - Convert.ToDecimal(suspenseAmount.Rows[0]["suspenseAmount"]);
                if(!suspense.Equals(0))
                {
                    if (suspense >= pendingAmount)
                    {

                        // add to ghost
                        command.CommandText = "insert into ghostPayment (ghostAmount,lotId,makerId,ghostDate) " +
                            "values (@ghostAmount,@lotId22,@makerId3,@paymentDate1)";
                        command.Parameters.AddWithValue("@makerId3", makerId);
                        command.Parameters.AddWithValue("@lotId22", LotId);
                        command.Parameters.AddWithValue("@ghostAmount", pendingAmount);
                        command.Parameters.AddWithValue("@paymentDate1", Convert.ToDateTime(paymentDate).ToString("yyyy-MM-dd"));
                        command.ExecuteNonQuery();

                        // mark paidStatus as paid
                        command.CommandText = "update Lot set paidStatus=@paidStatus1 where BagId=@BagId";
                        command.Parameters.AddWithValue("@paidStatus1", 1);
                        command.Parameters.AddWithValue("@BagId", LotId);
                        command.ExecuteNonQuery();
                        suspense = suspense - pendingAmount;
                    }
                    else
                    {
                        // add to ghost
                        command.CommandText = "insert into ghostPayment (ghostAmount,lotId,makerId,ghostDate) " +
                            "values (@ghostAmount1,@lotId21,@makerId11,@paymentDate2)";
                        command.Parameters.AddWithValue("@makerId11", makerId);
                        command.Parameters.AddWithValue("@lotId21", LotId);
                        command.Parameters.AddWithValue("@ghostAmount1", suspense);
                        command.Parameters.AddWithValue("@paymentDate2", Convert.ToDateTime(paymentDate).ToString("yyyy-MM-dd"));
                        command.ExecuteNonQuery();

                        suspense = 0;
                    }
                    command.Parameters.Clear();
                }
                else
                {
                    break;
                }
                
            }
                        
            command.Parameters.Clear();
            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

            return result;
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

    public int saveLotPayment(string lotId, string paymentCentre, string paymentDate, string paymentMode,
        string paymentAmount, string paymentRemarks,string paymentTransaction,string makerId,string vendorId,string lotAmount)
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
        transaction = connection.BeginTransaction("addEditCen");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            int result = 0;

            command.CommandText = "insert into lotPayment (paymentDate,paymentMode,paymentTransaction,paymentAmount,paymentRemarks,lotId,paymentCentre,vendorId) values " +
                "(@paymentDate,@paymentMode,@paymentTransaction,@paymentAmount,@paymentRemarks,@lotId,@paymentCentre,@vendorId)";
            command.Parameters.AddWithValue("@paymentDate", Convert.ToDateTime(paymentDate).ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@paymentMode", paymentMode);
            command.Parameters.AddWithValue("@paymentTransaction", paymentTransaction);
            command.Parameters.AddWithValue("@paymentAmount", Convert.ToDecimal(paymentAmount));
            command.Parameters.AddWithValue("@paymentRemarks", paymentRemarks);
            command.Parameters.AddWithValue("@lotId", lotId); 
            command.Parameters.AddWithValue("@makerId", makerId);
            command.Parameters.AddWithValue("@paymentCentre", paymentCentre);
            command.Parameters.AddWithValue("@vendorId", vendorId);
            command.ExecuteNonQuery();

            // check for suspense amount and adjust it in ghost entry
            DataTable suspenseAmount = new DataTable();
            command.CommandText = "SELECT ISNULL(SUM(suspenseAmount),0) AS suspenseAmount FROM (SELECT ISNULL(SUM(paymentAmount),0) AS suspenseAmount FROM lotPayment WHERE lotId=@lotId " +
                "union ALL " +
                "SELECT ISNULL(SUM(ghostAmount),0) AS suspenseAmount FROM ghostPayment WHERE lotId = @lotId1) a";
            command.Parameters.AddWithValue("@lotId1", lotId);
            suspenseAmount.Load(command.ExecuteReader());
           
            if((Convert.ToDecimal(suspenseAmount.Rows[0]["suspenseAmount"]) + Convert.ToDecimal(paymentAmount)) >=Convert.ToDecimal(lotAmount))
            {

                // mark paidStatus as paid
                command.CommandText = "update Lot set paidStatus=@paidStatus where BagId=@BagId";
                command.Parameters.AddWithValue("@paidStatus", 1);
                command.Parameters.AddWithValue("@BagId", lotId);
                command.ExecuteNonQuery();
            }

            command.Parameters.Clear();
            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

            return result;
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

    public int saveAddCustPayment(string customerId, string paymentDate, string paymentRemarks, DataTable dtProgLang,
         string makerId)
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
        transaction = connection.BeginTransaction("addCusPay");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            int result = 0;
            decimal suspense = Convert.ToDecimal(0);
            foreach (DataRow drow in dtProgLang.Rows)
            {
                suspense += Convert.ToDecimal(drow["paymentAmount"]);
                command.CommandText = "insert into invoicePayment (paymentDate,paymentMode,paymentTransaction,paymentAmount,paymentRemarks,paymentCentre,customerId,makerId) values " +
                "(@paymentDate,@paymentMode,@paymentTransaction,@paymentAmount,@paymentRemarks,@paymentCentre,@customerId,@makerId)";
                command.Parameters.AddWithValue("@paymentDate", Convert.ToDateTime(paymentDate).ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@paymentMode", drow["paymentMode"]);
                command.Parameters.AddWithValue("@paymentTransaction", drow["paymentTransaction"]);
                command.Parameters.AddWithValue("@paymentAmount", Convert.ToDecimal(drow["paymentAmount"]));
                command.Parameters.AddWithValue("@paymentRemarks", paymentRemarks);
                command.Parameters.AddWithValue("@makerId", makerId);
                command.Parameters.AddWithValue("@paymentCentre", drow["paymentCentre"]);
                command.Parameters.AddWithValue("@customerId", customerId);
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }
            

            // get all unpaid invoices
            DataTable Lot = new DataTable();
            command.CommandText = "select invid,total from invoice where webCustomer=@webCustomer and paymentStatus=@paymentStatus and invoiceStatus=@invoiceStatus";
            command.Parameters.AddWithValue("@paymentStatus", "unpaid");
            command.Parameters.AddWithValue("@webCustomer", customerId);
            command.Parameters.AddWithValue("@invoiceStatus", "Invoiced"); 
            Lot.Load(command.ExecuteReader());

            //decimal suspense = Convert.ToDecimal(paymentAmount);

            foreach (DataRow dRow in Lot.Rows)
            {
                string invid = dRow["invid"].ToString();
                decimal total = Convert.ToDecimal(dRow["total"]);

                DataTable suspenseAmount = new DataTable();
                command.CommandText = "SELECT ISNULL(SUM(suspenseAmount),0) AS suspenseAmount FROM (SELECT ISNULL(SUM(paymentAmount),0) AS suspenseAmount FROM invoicePayment WHERE invoiceId=@invoiceId " +
                    "union ALL " +
                    "SELECT ISNULL(SUM(ghostAmount),0) AS suspenseAmount FROM ghostinvoicePayment WHERE invoiceId = @invoiceId1) a";
                command.Parameters.AddWithValue("@invoiceId1", invid);
                command.Parameters.AddWithValue("@invoiceId", invid);
                suspenseAmount.Load(command.ExecuteReader());

                decimal pendingAmount = total - Convert.ToDecimal(suspenseAmount.Rows[0]["suspenseAmount"]);
                if (!suspense.Equals(0))
                {
                    if (suspense >= pendingAmount)
                    {

                        // add to ghost
                        command.CommandText = "insert into ghostinvoicePayment (ghostAmount,invoiceId,makerId,ghostDate) " +
                            "values (@ghostAmount,@invoiceId22,@makerId3,@paymentDate1)";
                        command.Parameters.AddWithValue("@makerId3", makerId);
                        command.Parameters.AddWithValue("@invoiceId22", invid);
                        command.Parameters.AddWithValue("@ghostAmount", pendingAmount);
                        command.Parameters.AddWithValue("@paymentDate1", Convert.ToDateTime(paymentDate).ToString("yyyy-MM-dd"));
                        command.ExecuteNonQuery();

                        // mark paidStatus as paid
                        command.CommandText = "update invoice set paymentStatus=@paymentStatus1 where invid=@BagId";
                        command.Parameters.AddWithValue("@paymentStatus1", "paid");
                        command.Parameters.AddWithValue("@BagId", invid);
                        command.ExecuteNonQuery();
                        suspense = suspense - pendingAmount;
                    }
                    else
                    {
                        // add to ghost
                        command.CommandText = "insert into ghostinvoicePayment (ghostAmount,invoiceId,makerId,ghostDate) " +
                            "values (@ghostAmount1,@lotId21,@makerId11,@paymentDate2)";
                        command.Parameters.AddWithValue("@makerId11", makerId);
                        command.Parameters.AddWithValue("@lotId21", invid);
                        command.Parameters.AddWithValue("@ghostAmount1", suspense);
                        command.Parameters.AddWithValue("@paymentDate2", Convert.ToDateTime(paymentDate).ToString("yyyy-MM-dd"));
                        command.ExecuteNonQuery();

                        suspense = 0;
                    }
                    command.Parameters.Clear();
                }
                else
                {
                    break;
                }

            }

            command.Parameters.Clear();
            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

            return result;
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

    public int saveInvoicePayment(string customerId, string paymentDate, string paymentRemarks, DataTable dtProgLang,
         string makerId,string invoiceId,string totalAmount)
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
        transaction = connection.BeginTransaction("addCusPay");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            int result = 0;
            decimal suspense = Convert.ToDecimal(0);
            foreach (DataRow drow in dtProgLang.Rows)
            {
                suspense += Convert.ToDecimal(drow["paymentAmount"]);
                command.CommandText = "insert into invoicePayment (paymentDate,paymentMode,paymentTransaction,paymentAmount,paymentRemarks,paymentCentre,customerId,makerId,invoiceId) values " +
                "(@paymentDate,@paymentMode,@paymentTransaction,@paymentAmount,@paymentRemarks,@paymentCentre,@customerId,@makerId,@invoiceId)";
                command.Parameters.AddWithValue("@paymentDate", Convert.ToDateTime(paymentDate).ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@paymentMode", drow["paymentMode"]);
                command.Parameters.AddWithValue("@paymentTransaction", drow["paymentTransaction"]);
                command.Parameters.AddWithValue("@paymentAmount", Convert.ToDecimal(drow["paymentAmount"]));
                command.Parameters.AddWithValue("@paymentRemarks", paymentRemarks);
                command.Parameters.AddWithValue("@makerId", makerId);
                command.Parameters.AddWithValue("@paymentCentre", drow["paymentCentre"]);
                command.Parameters.AddWithValue("@customerId", customerId);
                command.Parameters.AddWithValue("@invoiceId", invoiceId);
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }


            // if amount is completed than mark invoice as paid

            DataTable suspenseAmount = new DataTable();
            command.CommandText = "SELECT ISNULL(SUM(suspenseAmount),0) AS suspenseAmount FROM (SELECT ISNULL(SUM(paymentAmount),0) AS suspenseAmount FROM invoicePayment WHERE invoiceId=@invoiceId1 " +
                "union ALL " +
                "SELECT ISNULL(SUM(ghostAmount),0) AS suspenseAmount FROM ghostInvoicePayment WHERE invoiceId = @invoiceId2) a";
            command.Parameters.AddWithValue("@invoiceId1", invoiceId);
            command.Parameters.AddWithValue("@invoiceId2", invoiceId);
            suspenseAmount.Load(command.ExecuteReader());

            if ((Convert.ToDecimal(suspenseAmount.Rows[0]["suspenseAmount"]) + Convert.ToDecimal(suspense)) >= Convert.ToDecimal(totalAmount))            
            {
                command.CommandText = "update invoice set paymentStatus=@paymentStatus where invId=@invId";
                command.Parameters.AddWithValue("@paymentStatus","paid");
                command.Parameters.AddWithValue("@invId", invoiceId);
            }
            

            command.Parameters.Clear();
            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

            return result;
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