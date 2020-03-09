using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for bulkReportCls
/// </summary>
public class bulkReportCls
{
    public bulkReportCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable getRecord(string btnType, string frmDate, string toDate, string minval, string maxval, string commingfrom, string VendorID)
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
        transaction = connection.BeginTransaction("getVendorWisePurchaseRecord");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            //getVendorWiseStock
            if (btnType.Equals("VendorWise Stock"))
            {
                command.CommandText = "SELECT substring(c1name, CHARINDEX(':', c1name)+1, len(C1Name)-(CHARINDEX(':',c1name)-1)) AS 'VendorName', " +
                                    "SUM(pless30s) AS '0-30 Shop' , SUM(pless30w) AS '0-30 Warehouse' , SUM(a0to30) AS '0-30 LR' , " +
                                    "SUM(p31to60s) AS '31-60 Shop', SUM(p31to60w) AS '31-60 Warehouse', SUM(a31to60) AS '31-60 LR', " +
                                    "SUM(p61to90s) AS '61-90 Shop', SUM(p61to90w) AS '61-90 Warehouse', SUM(a61to90) AS '61-90 LR', " +
                                    "SUM(p91to120s) AS '91-120 Shop', SUM(p91to120w) AS '91-120 Warehouse', SUM(a91to120) AS '91-120 LR', " +
                                    "SUM(p121to150s) AS '121-150 Shop', SUM(p121to150w) AS '121-150 Warehouse', SUM(a121to150) AS '121-150 LR', " +
                                    "SUM(p151to180s) AS '151-180 Shop', SUM(p151to180w) AS '151-180 Warehouse', SUM(a151to180) AS '151-180 LR', " +
                                    "SUM(p181to240s) AS '181-240 Shop', SUM(p181to240w) AS '181-240 Warehouse', SUM(a181to240) AS '181-240 LR', " +
                                    "SUM(p241to300s) AS '241-300 Shop', SUM(p241to300w) AS '241-300 Warehouse', SUM(a241to300) AS '241-300 LR', " +
                                    "SUM(p301to360s) AS '301-360 Shop', SUM(p301to360w) AS '301-360 Warehouse', SUM(a301to360) AS '301-360 LR', " +
                                    "SUM(pmore360s) AS '360+  Shop', SUM(pmore360w) AS '360+ Warehouse', SUM(amore360) AS '360+ LR', " +
                                    "SUM(totals) AS 'Shop Total', SUM(totalw) AS 'Warehouse Total',SUM(totallr) AS 'LR Total', " +
                                    "SUM(totals) + SUM(totalw) + SUM(totallr) AS 'Grand Total' " +
                                    "FROM( " +
                                    "SELECT " +
                                    "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 30 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS pless30s, 0 AS pless30w, 0 AS a0to30, " +
                                    "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 30 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 60 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p31to60s, 0 AS p31to60w, 0 AS a31to60, " +
                                    "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 60 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 90 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p61to90s, 0 AS p61to90w, 0 AS a61to90, " +
                                    "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 90 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 120 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p91to120s, 0 AS p91to120w, 0 AS a91to120, " +
                                    "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 120 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 150 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p121to150s, 0 AS p121to150w, 0 AS a121to150, " +
                                    "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 150 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 180 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p151to180s, 0 AS p151to180w, 0 AS a151to180, " +
                                    "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 180 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 240 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p181to240s, 0 AS p181to240w, 0 AS a181to240, " +
                                    "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 240 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 300 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p241to300s, 0 AS p241to300w, 0 AS a241to300, " +
                                    "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 300 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 360 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p301to360s, 0 AS p301to360w, 0 AS a301to360, " +
                                    "(CASE WHEN((DATEDIFF(DAY, s.SystemDate, GETDATE())) > 360) Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS pmore360s, 0 AS pmore360w, 0 AS amore360, " +
                                    "(sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0))) AS totals, 0 AS totalw, 0 AS totallr, " +
                                    "c.c1name " +
                                    "FROM StockUpInward s " +
                                    "INNER JOIN ItemStyle i ON i.styleid = s.styleid and s.physicalid = 3 INNER JOIN COLUMN1 c ON c.col1id = i.Col1 INNER JOIN Lot l ON l.BagId=s.BagID INNER JOIN Vendor v ON v.VendorID=l.VendorID " +

                                    "GROUP BY s.SystemDate, c.c1name, s.travelCost,s.taxValue " +
                                    "UNION ALL " +
                                    "SELECT " +
                                    "0 as pless30s, (CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 30 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS pless30w, 0 AS a0to30, " +
                                    "0 AS p31to60s, (CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 30 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 60 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p31to60w, 0 AS a31to60, " +
                                    "0 AS p61to60s, (CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 60 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 90 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p61to90w, 0 AS a61to90, " +
                                    "0 AS p91to60s, (CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 90 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 120 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p91to120w, 0 AS a91to120, " +
                                    "0 AS p121to60s, (CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 120 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 150 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p121to150w, 0 AS a121to150, " +
                                    "0 AS p151to60s, (CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 150 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 180 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p151to180w, 0 AS a151to180, " +
                                    "0 AS p181to60s, (CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 180 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 240 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p181to240w, 0 AS a181to240, " +
                                    "0 AS p241to60s, (CASE WHEN(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 240 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 300 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p241to300w, 0 AS a241to300, " +
                                    "0 AS p301to60s, (CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 300 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 360 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p301to360w, 0 AS a301to360, " +
                                    "0 AS pmore360s, (CASE WHEN((DATEDIFF(DAY, s.SystemDate, GETDATE())) > 360) Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS pmore360w, 0 AS amore360, " +
                                    "0 AS totals, (sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0))) AS totalw, 0 AS totallr, " +
                                    "c.c1name " +
                                    "FROM StockUpInward s " +
                                    "INNER JOIN ItemStyle i ON i.styleid = s.styleid  AND s.physicalid <> 3 INNER JOIN COLUMN1 c ON c.col1id = i.Col1 INNER JOIN Lot l ON l.BagId=s.BagID INNER JOIN Vendor v ON v.VendorID=l.VendorID " +

                                    "GROUP BY s.SystemDate, c.c1name, s.travelCost,s.taxValue " +
                                    "UNION ALL " +
                                    "SELECT " +
                                    "0 as pless30s, 0 as pless30w, CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 30 Then SUM(l.totalAmount) Else 0 END AS a0to30, " +
                                    "0 AS p31to60s, 0 as p31to60w, CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 30 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 60 Then  SUM(l.totalAmount) Else 0 END AS a31to60, " +
                                    "0 AS p61to90s, 0 AS p61to90w, CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 60 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 90 Then  SUM(l.totalAmount) Else 0 END AS a61to90, " +
                                    "0 AS p91to120s, 0 AS p91to120w, CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 90 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 120 Then  SUM(l.totalAmount) Else 0 END AS a91to120, " +
                                    "0 AS p121to150s, 0 AS p121to150w, CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 120 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 150 Then  SUM(l.totalAmount) Else 0 END AS a121to150, " +
                                    "0 AS p151to180s, 0 AS p151to180w, CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 150 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 180 Then  SUM(l.totalAmount) Else 0 END AS a151to180, " +
                                    "0 AS p181to240s, 0 AS p181to240w, CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 180 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 240 Then  SUM(l.totalAmount) Else 0 END AS a181to240, " +
                                    "0 AS p241to300s, 0 AS p241to300w, CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 240 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 300 Then  SUM(l.totalAmount) Else 0 END AS a241to300, " +
                                    "0 AS p301to360s, 0 as p301to360w, CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 300 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 360 Then  SUM(l.totalAmount) Else 0 END AS a301to360, " +
                                    "0 AS pmore360s, 0 as pmore360w, CASE WHEN((DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 360 or l.invoiceDate IS NULL) Then SUM(l.totalAmount) Else 0 END AS amore360, " +
                                    "0 AS totals, 0 as totalw, SUM(l.totalAmount) AS totallr, " +
                                    "c.c1name " +
                                    "FROM Lot l INNER JOIN  lrListing lr ON lr.id = l.lrno INNER JOIN Vendor v ON v.VendorID = l.VendorID INNER JOIN Column1 c ON c.Col1ID=v.svid WHERE l.IsActive = 3 GROUP BY c.C1Name, invoiceDate " +
                                    ") a GROUP BY c1name ORDER BY c1name";
            }
            //getStock
            else if (btnType.Equals("Stock"))
            {
                command.CommandText = "SELECT '0-30' as Age, SUM(purchaseRate) AS 'Purchase Amount', ROUND((SELECT  SUM(purchaseRate) from StockUpInward S where S.DateTime>= DATEADD(DAY,-30,GETDATE()) and S.DateTime < GETDATE()) *100 / (Select SUM(purchaseRate) FROM stockupinward),2) AS Percentage from StockUpInward S where S.DateTime>= DATEADD(DAY,-30,GETDATE()) and S.DateTime <= GETDATE()UNION ALL SELECT '31-60' as Age, SUM(purchaseRate) AS 'Purchase Amount', ROUND((SELECT  SUM(purchaseRate) from StockUpInward S where S.DateTime>= DATEADD(DAY,-60,GETDATE()) and S.DateTime < DATEADD(DAY,-30,GETDATE())) *100 / (Select SUM(purchaseRate) FROM stockupinward),2) AS Percentage from StockUpInward S where S.DateTime>= DATEADD(DAY,-60,GETDATE()) and S.DateTime < DATEADD(DAY,-30,GETDATE())UNION ALL SELECT '61-90' as Age, SUM(purchaseRate) AS 'Purchase Amount', ROUND((SELECT  SUM(purchaseRate) from StockUpInward S where S.DateTime>= DATEADD(DAY,-90,GETDATE()) and S.DateTime < DATEADD(DAY,-60,GETDATE())) *100 / (Select SUM(purchaseRate) FROM stockupinward),2) AS Percentage from StockUpInward S where S.DateTime>= DATEADD(DAY,-90,GETDATE()) and S.DateTime < DATEADD(DAY,-60,GETDATE())UNION ALL SELECT '91-120' as Age, SUM(purchaseRate) AS 'Purchase Amount', ROUND((SELECT  SUM(purchaseRate) from StockUpInward S where S.DateTime>= DATEADD(DAY,-120,GETDATE()) and S.DateTime < DATEADD(DAY,-90,GETDATE())) *100 / (Select SUM(purchaseRate) FROM stockupinward),2) AS Percentage from StockUpInward S where S.DateTime>= DATEADD(DAY,-120,GETDATE()) and S.DateTime < DATEADD(DAY,-90,GETDATE()) UNION ALL SELECT '121-150' as Age, SUM(purchaseRate) AS 'Purchase Amount', ROUND((SELECT  SUM(purchaseRate) from StockUpInward S where S.DateTime>= DATEADD(DAY,-150,GETDATE()) and S.DateTime < DATEADD(DAY,-120,GETDATE())) *100 / (Select SUM(purchaseRate) FROM stockupinward),2) AS Percentage from StockUpInward S where S.DateTime>= DATEADD(DAY,-150,GETDATE()) and S.DateTime < DATEADD(DAY,-120,GETDATE())UNION ALL SELECT '151-180' as Age, SUM(purchaseRate) AS 'Purchase Amount', ROUND((SELECT  SUM(purchaseRate) from StockUpInward S where S.DateTime>= DATEADD(DAY,-180,GETDATE()) and S.DateTime < DATEADD(DAY,-150,GETDATE())) *100 / (Select SUM(purchaseRate) FROM stockupinward),2) AS Percentage from StockUpInward S where S.DateTime>= DATEADD(DAY,-180,GETDATE()) and S.DateTime < DATEADD(DAY,-150,GETDATE())UNION ALL SELECT '181-240' as Age, SUM(purchaseRate) AS 'Purchase Amount', ROUND((SELECT  SUM(purchaseRate) from StockUpInward S where S.DateTime>= DATEADD(DAY,-240,GETDATE()) and S.DateTime < DATEADD(DAY,-180,GETDATE())) *100 / (Select SUM(purchaseRate) FROM stockupinward),2) AS Percentage from StockUpInward S where S.DateTime>= DATEADD(DAY,-240,GETDATE()) and S.DateTime < DATEADD(DAY,-180,GETDATE())UNION ALL SELECT '241-300' as Age, SUM(purchaseRate) AS 'Purchase Amount', ROUND((SELECT  SUM(purchaseRate) from StockUpInward S where S.DateTime>= DATEADD(DAY,-300,GETDATE()) and S.DateTime < DATEADD(DAY,-240,GETDATE())) *100 / (Select SUM(purchaseRate) FROM stockupinward),2) AS Percentage from StockUpInward S where S.DateTime>= DATEADD(DAY,-300,GETDATE()) and S.DateTime < DATEADD(DAY,-240,GETDATE())UNION ALL SELECT '301-360' as Age, SUM(purchaseRate) AS 'Purchase Amount', ROUND((SELECT  SUM(purchaseRate) from StockUpInward S where S.DateTime>= DATEADD(DAY,-360,GETDATE()) and S.DateTime < DATEADD(DAY,-300,GETDATE())) *100 / (Select SUM(purchaseRate) FROM stockupinward),2) AS Percentage from StockUpInward S where S.DateTime>= DATEADD(DAY,-360,GETDATE()) and S.DateTime < DATEADD(DAY,-300,GETDATE())UNION ALL SELECT '360+' as Age, SUM(purchaseRate) AS 'Purchase Amount', ROUND((SELECT  SUM(purchaseRate) from StockUpInward S where S.DateTime <= DATEADD(DAY,-361,GETDATE())) *100 / (Select SUM(purchaseRate) FROM stockupinward),2) AS Percentage from StockUpInward S where S.DateTime <= DATEADD(DAY,-361,GETDATE())UNION ALL SELECT 'Grand Total' as Age, SUM(purchaseRate) AS 'Purchase Amount', ROUND((SELECT  SUM(purchaseRate) from StockUpInward ) *100 / (Select SUM(purchaseRate) FROM stockupinward),2) AS Percentage from StockUpInward";
            }
            //getVendorWiseStockShop
            else if (btnType.Equals("VendorWise Stock Shop"))
            {
                command.CommandText = "SELECT substring(c1name, CHARINDEX(':', c1name)+1, len(C1Name)-(CHARINDEX(':',c1name)-1)) AS 'Vendor', SUM(pless30) AS '0-30' , SUM(p31to60) AS '31-60', SUM(p61to90) AS '61-90', SUM(p91to120) AS '91-120', SUM(p121to150) AS '121-150', SUM(p151to180) AS '151-180', SUM(p181to240) AS '181-240', SUM(p241to300) AS '241-300', SUM(p301to360) AS '301-360', SUM(pmore360) AS '360+', SUM(total) AS 'Grand Total' " +
                    "FROM( " +
                    "SELECT " +
                    "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 30 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS pless30, " +
                    "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 30 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 60 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p31to60, " +
                    "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 60 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 90 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p61to90, " +
                    "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 90 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 120 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p91to120, " +
                    "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 120 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 150 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p121to150, " +
                    "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 150 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 180 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p151to180, " +
                    "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 180 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 240 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p181to240, " +
                    "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 240 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 300 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p241to300, " +
                    "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 300 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 360 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p301to360, " +
                    "(CASE WHEN((DATEDIFF(DAY, s.SystemDate, GETDATE())) > 360) Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS pmore360, " +
                    "(sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0))) AS total, " +
                    "c.c1name, s.purchaseRate, s.SystemDate " +
                    "FROM StockUpInward s " +
                    "INNER JOIN ItemStyle i ON i.styleid = s.styleid and s.physicalId = 3 INNER JOIN COLUMN1 c ON c.col1id = i.Col1 INNER JOIN Lot l ON l.BagId=s.BagID INNER JOIN Vendor v ON v.VendorID=l.VendorID  GROUP BY s.SystemDate, c.c1name, s.purchaseRate,s.travelCost,s.taxValue) a GROUP BY c1name ORDER BY c1name";
            }
            //getVendorWiseStockWarehouse
            else if (btnType.Equals("VendorWise Stock Warehouse"))
            {
                command.CommandText = "SELECT substring(c1name, CHARINDEX(':', c1name)+1, len(C1Name)-(CHARINDEX(':',c1name)-1)) AS 'Vendor', SUM(pless30) AS '0-30' , SUM(p31to60) AS '31-60', SUM(p61to90) AS '61-90', SUM(p91to120) AS '91-120', SUM(p121to150) AS '121-150', SUM(p151to180) AS '151-180', SUM(p181to240) AS '181-240', SUM(p241to300) AS '241-300', SUM(p301to360) AS '301-360', SUM(pmore360) AS '360+', SUM(total) AS 'Grand Total' " +
                     "FROM( " +
                     "SELECT " +
                     "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 30 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS pless30, " +
                     "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 30 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 60 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p31to60, " +
                     "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 60 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 90 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p61to90, " +
                     "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 90 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 120 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p91to120, " +
                     "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 120 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 150 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p121to150, " +
                     "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 150 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 180 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p151to180, " +
                     "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 180 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 240 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p181to240, " +
                     "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 240 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 300 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p241to300, " +
                     "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 300 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 360 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p301to360, " +
                     "(CASE WHEN((DATEDIFF(DAY, s.SystemDate, GETDATE())) > 360) Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS pmore360, " +
                     "(sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0))) AS total, " +
                     "c.c1name, s.purchaseRate, s.SystemDate " +
                     "FROM StockUpInward s " +
                     "INNER JOIN ItemStyle i ON i.styleid = s.styleid and s.physicalId <>3 INNER JOIN COLUMN1 c ON c.col1id = i.Col1 INNER JOIN Lot l ON l.BagId=s.BagID INNER JOIN Vendor v ON v.VendorID=l.VendorID  GROUP BY s.SystemDate, c.c1name, s.purchaseRate,s.travelCost,s.taxValue) a GROUP BY c1name ORDER BY c1name";
            }

            else if (btnType.Equals("Sales With Margin"))
            {
                command.CommandText = "SELECT substring(c1name, CHARINDEX(':', c1name)+1, len(C1Name)-(CHARINDEX(':',c1name)-1)) AS 'Vendor', " +
                      "SUM(pless30) AS 'Purchase 0-30' ,SUM(sless30) AS 'Sales 0-30', ROUND(((SUM(sless30) - SUM(pless30)) / nullif(SUM(sless30), 0)) * 100, 2) AS 'Margin 0-30', " +
                      "SUM(p31to60) AS 'Purchase 31-60',SUM(s31to60) AS 'Sales 31-60',ROUND(((SUM(s31to60) - SUM(p31to60)) / nullif(SUM(s31to60), 0)) * 100, 2) AS 'Margin 31-60', " +
                      "SUM(p61to90) AS 'Purchase 61-90', SUM(s61to90) AS 'Sales 61-90',ROUND(((SUM(s61to90) - SUM(p61to90)) / nullif(SUM(s61to90), 0)) * 100, 2) AS 'Margin 61-90', " +
                      "SUM(p91to120) AS 'Purchase 91-120',  SUM(s91to120) AS 'Sales 91-120',ROUND(((SUM(s91to120) - SUM(p91to120)) / nullif(SUM(s91to120), 0)) * 100, 2) AS 'Margin 91-120', " +
                      "SUM(p121to150) AS 'Purchase 121-150', SUM(s121to150) AS 'Sales 121-150',ROUND(((SUM(s121to150) - SUM(p121to150)) / nullif(SUM(s121to150), 0)) * 100, 2) AS 'Margin 121-150', " +
                      "SUM(p151to180) AS 'Purchase 151-180', SUM(s151to180) AS 'Sales 151-180',ROUND(((SUM(s151to180) - SUM(p151to180)) / nullif(SUM(s151to180), 0)) * 100, 2) AS 'Margin 151-180', " +
                      "SUM(p181to240) AS 'Purchase 181-240', SUM(s181to240) AS 'Sales 181-240',ROUND(((SUM(s181to240) - SUM(p181to240)) / nullif(SUM(s181to240), 0)) * 100, 2) AS 'Margin 181-240', " +
                      "SUM(p241to300) AS 'Purchase 241-300', SUM(s241to300) AS 'Sales 241-300',ROUND(((SUM(s241to300) - SUM(p241to300)) / nullif(SUM(s241to300), 0)) * 100, 2) AS 'Margin 241-300', " +
                      "SUM(p301to360) AS 'Purchase 301-360',  SUM(s301to360) AS 'Sales 301-360',ROUND(((SUM(s301to360) - SUM(p301to360)) / nullif(SUM(s301to360), 0)) * 100, 2) AS 'Margin 301-360', " +
                      "SUM(pmore360) AS 'Purchase 360+', SUM(smore360) AS 'Sales 360+',ROUND(((SUM(smore360) - SUM(pmore360)) / nullif(SUM(smore360), 0)) * 100, 2) AS 'Margin 360+', " +
                      "SUM(totalpurchase) AS 'Total Purchase', SUM(totalsales) AS 'Total Sales',ROUND(((SUM(totalsales) - SUM(totalpurchase)) / nullif(SUM(totalsales), 0)) * 100, 2) AS 'Total Margin' " +
                      "FROM( " +
                      "SELECT " +
                      "(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 30 Then sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0)) Else 0 END) AS pless30, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 30 Then s.sellingprice Else 0 END) AS sless30, " +
                      "(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 30 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 60 Then sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0)) Else 0 END) AS p31to60, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 30 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 60 THEN s.sellingprice Else 0 END) AS s31to60, " +
                      "(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 60 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 90 Then sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0)) Else 0 END) AS p61to90, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 60 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 90 Then s.sellingprice Else 0 END) AS s61to90, " +
                      "(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 90 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 120 Then sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0)) Else 0 END) AS p91to120, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 90 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 120 Then s.sellingprice Else 0 END) AS s91to120, " +
                      "(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 120 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 150 Then sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0)) Else 0 END) AS p121to150, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 120 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 150 Then s.sellingprice Else 0 END) AS s121to150, " +
                      "(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 150 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 180 Then sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0)) Else 0 END) AS p151to180, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 150 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 180 Then s.sellingprice Else 0 END) AS s151to180, " +
                      "(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 180 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 240 Then sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0)) Else 0 END) AS p181to240, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 180 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 240 Then s.sellingprice Else 0 END) AS s181to240, " +
                      "(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 240 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 300 Then sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0)) Else 0 END) AS p241to300, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 240 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 300 Then s.sellingprice Else 0 END) AS s241to300, " +
                      "(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 300 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 360 Then sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0)) Else 0 END) AS p301to360, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 300 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 360 Then s.sellingprice Else 0 END) AS s301to360, " +
                      "(CASE WHEN((DATEDIFF(DAY, a.SystemDate, GETDATE())) > 360) Then sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0)) Else 0 END) AS pmore360, SUM(CASE WHEN((DATEDIFF(DAY, a.SystemDate, GETDATE())) > 360) Then s.sellingprice Else 0 END) AS smore360, " +
                      "(sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0))) AS totalpurchase, SUM(s.sellingprice) AS totalsales, " +
                      "c.c1name, a.purchaseRate, a.SystemDate " +
                      "FROM ArchiveStockUpInward a " +
                      "INNER JOIN ItemStyle i ON i.styleid = a.styleid INNER JOIN COLUMN1 c ON c.col1id = i.Col1 inner join salesrecord s ON s.archiveid = a.ArchiveStockupId INNER  JOIN Vendor v ON v.svid=c.Col1ID WHERE s.returntimestamp IS NULL AND convert(date, s.salesDateTime) BETWEEN convert(date, @frmDate) AND convert(date, @toDate) GROUP BY a.SystemDate, c.c1name, a.purchaseRate,a.taxValue,a.travelCost) b GROUP BY c1name ORDER BY c1name ";
            }

            else if (btnType.Equals("Sales For Warehouse"))
            {
                command.CommandText = "SELECT substring(c1name, CHARINDEX(':', c1name)+1, len(C1Name)-(CHARINDEX(':',c1name)-1)) AS 'Vendor', " +
                      "SUM(pless30) AS 'Purchase 0-30' ,SUM(sless30) AS 'Sales 0-30', ROUND(((SUM(sless30) - SUM(pless30)) / nullif(SUM(sless30), 0)) * 100, 2) AS 'Margin 0-30', " +
                      "SUM(p31to60) AS 'Purchase 31-60',SUM(s31to60) AS 'Sales 31-60',ROUND(((SUM(s31to60) - SUM(p31to60)) / nullif(SUM(s31to60), 0)) * 100, 2) AS 'Margin 31-60', " +
                      "SUM(p61to90) AS 'Purchase 61-90', SUM(s61to90) AS 'Sales 61-90',ROUND(((SUM(s61to90) - SUM(p61to90)) / nullif(SUM(s61to90), 0)) * 100, 2) AS 'Margin 61-90', " +
                      "SUM(p91to120) AS 'Purchase 91-120',  SUM(s91to120) AS 'Sales 91-120',ROUND(((SUM(s91to120) - SUM(p91to120)) / nullif(SUM(s91to120), 0)) * 100, 2) AS 'Margin 91-120', " +
                      "SUM(p121to150) AS 'Purchase 121-150', SUM(s121to150) AS 'Sales 121-150',ROUND(((SUM(s121to150) - SUM(p121to150)) / nullif(SUM(s121to150), 0)) * 100, 2) AS 'Margin 121-150', " +
                      "SUM(p151to180) AS 'Purchase 151-180', SUM(s151to180) AS 'Sales 151-180',ROUND(((SUM(s151to180) - SUM(p151to180)) / nullif(SUM(s151to180), 0)) * 100, 2) AS 'Margin 151-180', " +
                      "SUM(p181to240) AS 'Purchase 181-240', SUM(s181to240) AS 'Sales 181-240',ROUND(((SUM(s181to240) - SUM(p181to240)) / nullif(SUM(s181to240), 0)) * 100, 2) AS 'Margin 181-240', " +
                      "SUM(p241to300) AS 'Purchase 241-300', SUM(s241to300) AS 'Sales 241-300',ROUND(((SUM(s241to300) - SUM(p241to300)) / nullif(SUM(s241to300), 0)) * 100, 2) AS 'Margin 241-300', " +
                      "SUM(p301to360) AS 'Purchase 301-360',  SUM(s301to360) AS 'Sales 301-360',ROUND(((SUM(s301to360) - SUM(p301to360)) / nullif(SUM(s301to360), 0)) * 100, 2) AS 'Margin 301-360', " +
                      "SUM(pmore360) AS 'Purchase 360+', SUM(smore360) AS 'Sales 360+',ROUND(((SUM(smore360) - SUM(pmore360)) / nullif(SUM(smore360), 0)) * 100, 2) AS 'Margin 360+', " +
                      "SUM(totalpurchase) AS 'Total Purchase', SUM(totalsales) AS 'Total Sales',ROUND(((SUM(totalsales) - SUM(totalpurchase)) / nullif(SUM(totalsales), 0)) * 100, 2) AS 'Total Margin' " +
                      "FROM( " +
                      "SELECT " +
                      "(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 30 Then sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0)) Else 0 END) AS pless30, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 30 Then s.sellingprice Else 0 END) AS sless30, " +
                      "(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 30 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 60 Then sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0)) Else 0 END) AS p31to60, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 30 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 60 THEN s.sellingprice Else 0 END) AS s31to60, " +
                      "(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 60 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 90 Then sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0)) Else 0 END) AS p61to90, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 60 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 90 Then s.sellingprice Else 0 END) AS s61to90, " +
                      "(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 90 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 120 Then sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0)) Else 0 END) AS p91to120, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 90 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 120 Then s.sellingprice Else 0 END) AS s91to120, " +
                      "(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 120 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 150 Then sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0)) Else 0 END) AS p121to150, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 120 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 150 Then s.sellingprice Else 0 END) AS s121to150, " +
                      "(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 150 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 180 Then sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0)) Else 0 END) AS p151to180, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 150 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 180 Then s.sellingprice Else 0 END) AS s151to180, " +
                      "(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 180 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 240 Then sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0)) Else 0 END) AS p181to240, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 180 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 240 Then s.sellingprice Else 0 END) AS s181to240, " +
                      "(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 240 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 300 Then sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0)) Else 0 END) AS p241to300, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 240 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 300 Then s.sellingprice Else 0 END) AS s241to300, " +
                      "(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 300 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 360 Then sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0)) Else 0 END) AS p301to360, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 300 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 360 Then s.sellingprice Else 0 END) AS s301to360, " +
                      "(CASE WHEN((DATEDIFF(DAY, a.SystemDate, GETDATE())) > 360) Then sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0)) Else 0 END) AS pmore360, SUM(CASE WHEN((DATEDIFF(DAY, a.SystemDate, GETDATE())) > 360) Then s.sellingprice Else 0 END) AS smore360, " +
                      "(sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0))) AS totalpurchase, SUM(s.sellingprice) AS totalsales, " +
                      "c.c1name, a.purchaseRate, a.SystemDate " +
                      "FROM ArchiveStockUpInward a " +
                      "INNER JOIN ItemStyle i ON i.styleid = a.styleid INNER JOIN COLUMN1 c ON c.col1id = i.Col1 INNER  JOIN Vendor v ON v.svid=c.Col1ID inner join salesrecord s ON s.archiveid = a.ArchiveStockupId inner join login l ON l.userid = a.userid WHERE l.physicalLocation = 1 and s.returntimestamp IS NULL AND convert(date, s.salesDateTime) BETWEEN convert(date, @frmDate) AND convert(date, @toDate) GROUP BY a.SystemDate, c.c1name, a.purchaseRate,a.taxValue,a.travelCost) b GROUP BY c1name ORDER BY c1name";
            }

            else if (btnType.Equals("Sales For Shop"))
            {
                command.CommandText = "SELECT c1name AS 'Vendor', " +
                       "SUM(pless30) AS 'Purchase 0-30' ,SUM(sless30) AS 'Sales 0-30', ROUND(((SUM(sless30) - SUM(pless30)) / nullif(SUM(sless30), 0)) * 100, 2) AS 'Margin 0-30', " +
                       "SUM(p31to60) AS 'Purchase 31-60',SUM(s31to60) AS 'Sales 31-60',ROUND(((SUM(s31to60) - SUM(p31to60)) / nullif(SUM(s31to60), 0)) * 100, 2) AS 'Margin 31-60', " +
                       "SUM(p61to90) AS 'Purchase 61-90', SUM(s61to90) AS 'Sales 61-90',ROUND(((SUM(s61to90) - SUM(p61to90)) / nullif(SUM(s61to90), 0)) * 100, 2) AS 'Margin 61-90', " +
                       "SUM(p91to120) AS 'Purchase 91-120',  SUM(s91to120) AS 'Sales 91-120',ROUND(((SUM(s91to120) - SUM(p91to120)) / nullif(SUM(s91to120), 0)) * 100, 2) AS 'Margin 91-120', " +
                       "SUM(p121to150) AS 'Purchase 121-150', SUM(s121to150) AS 'Sales 121-150',ROUND(((SUM(s121to150) - SUM(p121to150)) / nullif(SUM(s121to150), 0)) * 100, 2) AS 'Margin 121-150', " +
                       "SUM(p151to180) AS 'Purchase 151-180', SUM(s151to180) AS 'Sales 151-180',ROUND(((SUM(s151to180) - SUM(p151to180)) / nullif(SUM(s151to180), 0)) * 100, 2) AS 'Margin 151-180', " +
                       "SUM(p181to240) AS 'Purchase 181-240', SUM(s181to240) AS 'Sales 181-240',ROUND(((SUM(s181to240) - SUM(p181to240)) / nullif(SUM(s181to240), 0)) * 100, 2) AS 'Margin 181-240', " +
                       "SUM(p241to300) AS 'Purchase 241-300', SUM(s241to300) AS 'Sales 241-300',ROUND(((SUM(s241to300) - SUM(p241to300)) / nullif(SUM(s241to300), 0)) * 100, 2) AS 'Margin 241-300', " +
                       "SUM(p301to360) AS 'Purchase 301-360',  SUM(s301to360) AS 'Sales 301-360',ROUND(((SUM(s301to360) - SUM(p301to360)) / nullif(SUM(s301to360), 0)) * 100, 2) AS 'Margin 301-360', " +
                       "SUM(pmore360) AS 'Purchase 360+', SUM(smore360) AS 'Sales 360+',ROUND(((SUM(smore360) - SUM(pmore360)) / nullif(SUM(smore360), 0)) * 100, 2) AS 'Margin 360+', " +
                       "SUM(totalpurchase) AS 'Total Purchase', SUM(totalsales) AS 'Total Sales',ROUND(((SUM(totalsales) - SUM(totalpurchase)) / nullif(SUM(totalsales), 0)) * 100, 2) AS 'Total Margin' " +
                       "FROM( " +
                       "SELECT " +
                       "(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 30 Then sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0)) Else 0 END) AS pless30, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 30 Then s.sellingprice Else 0 END) AS sless30, " +
                       "(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 30 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 60 Then sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0)) Else 0 END) AS p31to60, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 30 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 60 THEN s.sellingprice Else 0 END) AS s31to60, " +
                       "(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 60 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 90 Then sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0)) Else 0 END) AS p61to90, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 60 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 90 Then s.sellingprice Else 0 END) AS s61to90, " +
                       "(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 90 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 120 Then sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0)) Else 0 END) AS p91to120, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 90 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 120 Then s.sellingprice Else 0 END) AS s91to120, " +
                       "(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 120 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 150 Then sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0)) Else 0 END) AS p121to150, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 120 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 150 Then s.sellingprice Else 0 END) AS s121to150, " +
                       "(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 150 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 180 Then sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0)) Else 0 END) AS p151to180, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 150 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 180 Then s.sellingprice Else 0 END) AS s151to180, " +
                       "(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 180 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 240 Then sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0)) Else 0 END) AS p181to240, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 180 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 240 Then s.sellingprice Else 0 END) AS s181to240, " +
                       "(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 240 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 300 Then sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0)) Else 0 END) AS p241to300, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 240 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 300 Then s.sellingprice Else 0 END) AS s241to300, " +
                       "(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 300 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 360 Then sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0)) Else 0 END) AS p301to360, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 300 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 360 Then s.sellingprice Else 0 END) AS s301to360, " +
                       "(CASE WHEN((DATEDIFF(DAY, a.SystemDate, GETDATE())) > 360) Then sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0)) Else 0 END) AS pmore360, SUM(CASE WHEN((DATEDIFF(DAY, a.SystemDate, GETDATE())) > 360) Then s.sellingprice Else 0 END) AS smore360, " +
                       "(sum(Isnull(a.purchaseRate,0))+sum(isnull(a.taxValue,0))+sum(isnull(a.travelCost,0))) AS totalpurchase, SUM(s.sellingprice) AS totalsales, " +
                       "c.c1name, a.purchaseRate, a.SystemDate " +
                       "FROM ArchiveStockUpInward a " +
                       "INNER JOIN ItemStyle i ON i.styleid = a.styleid INNER JOIN COLUMN1 c ON c.col1id = i.Col1 INNER  JOIN Vendor v ON v.svid=c.Col1ID inner join salesrecord s ON s.archiveid = a.ArchiveStockupId inner join login l ON l.userid = a.userid WHERE l.physicalLocation = 3 and  s.returntimestamp IS NULL AND convert(date, s.salesDateTime) BETWEEN convert(date, @frmDate) AND convert(date, @toDate) GROUP BY a.SystemDate, c.c1name, a.purchaseRate,a.taxValue,a.travelCost) b GROUP BY c1name ORDER BY c1name";
            }

            else if (btnType.Equals("Purchase With Margin"))
            {
                //command.CommandText = "SELECT substring(c1name, CHARINDEX(':', c1name)+1, len(C1Name)-(CHARINDEX(':',c1name)-1)) AS 'Vendor', " +
                //      "SUM(pless30) AS 'Purchase 0-30' , SUM(mless30) AS 'MRP 0-30' , ROUND(((SUM(mless30) - SUM(pless30)) / nullif(SUM(mless30), 0)) * 100, 2) AS 'Margin 0-30', " +
                //      "SUM(p31to60) AS 'Purchase 31-60', SUM(m31to60) AS 'MRP 31-60', ROUND(((SUM(m31to60) - SUM(p31to60)) / nullif(SUM(m31to60), 0)) * 100, 2) AS 'Margin 31-30', " +
                //      "SUM(p61to90) AS 'Purchase 61-90', SUM(m61to90) AS 'MRP 61-90', ROUND(((SUM(m61to90) - SUM(p61to90)) / nullif(SUM(m61to90), 0)) * 100, 2) AS 'Margin 61-30', " +
                //      "SUM(p91to120) AS 'Purchase 91-120', SUM(m91to120) AS 'MRP 91-120', ROUND(((SUM(m91to120) - SUM(p91to120)) / nullif(SUM(m91to120), 0)) * 100, 2) AS 'Margin 91-30', " +
                //      "SUM(p121to150) AS 'Purchase 121-150', SUM(m121to150) AS 'MRP 121-150', ROUND(((SUM(m121to150) - SUM(p121to150)) / nullif(SUM(m121to150), 0)) * 100, 2) AS 'Margin 121-30', " +
                //      "SUM(p151to180) AS 'Purchase 151-180', SUM(m151to180) AS 'MRP 151-180', ROUND(((SUM(m151to180) - SUM(p151to180)) / nullif(SUM(m151to180), 0)) * 100, 2) AS 'Margin 151-30', " +
                //      "SUM(p181to240) AS 'Purchase 181-240', SUM(m181to240) AS 'MRP 181-240', ROUND(((SUM(m181to240) - SUM(p181to240)) / nullif(SUM(m181to240), 0)) * 100, 2) AS 'Margin 181-30', " +
                //      "SUM(p241to300) AS 'Purchase 241-300', SUM(m241to300) AS 'MRP 241-300', ROUND(((SUM(m241to300) - SUM(p241to300)) / nullif(SUM(m241to300), 0)) * 100, 2) AS 'Margin 241-30', " +
                //      "SUM(p301to360) AS 'Purchase 301-360', SUM(m301to360) AS 'MRP 301-360', ROUND(((SUM(m301to360) - SUM(p301to360)) / nullif(SUM(m301to360), 0)) * 100, 2) AS 'Margin 301-30', " +
                //      "SUM(pmore360) AS 'Purchase 360+', SUM(mmore360) AS 'MRP 360+', ROUND(((SUM(mmore360) - SUM(pmore360)) / nullif(SUM(mmore360), 0)) * 100, 2) AS 'Margin 360+', " +
                //      "SUM(ptotal) AS 'Purchase Total', SUM(mtotal) AS 'MRP Total', ROUND(((SUM(mtotal) - SUM(ptotal)) / nullif(SUM(mtotal), 0)) * 100, 2) AS 'Total Margin' " +
                //      "FROM( " +
                //      "SELECT " +
                //      "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 30 Then (CASE WHEN isnull(v.istax,0)=1 THEN sum(s.purchaseRate)+((SELECT tax FROM tax)/100* sum(s.purchaseRate))+sum(ISNULL(s.travelCost,0)) ELSE sum(s.purchaseRate)+sum(isnull(s.travelCost,0)) END ) Else 0 END) AS pless30, SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 30 Then s.mrp Else 0 END) AS mless30, " +
                //      "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 30 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 60 Then (CASE WHEN isnull(v.istax,0)=1 THEN sum(s.purchaseRate)+((SELECT tax FROM tax)/100* sum(s.purchaseRate))+sum(ISNULL(s.travelCost,0)) ELSE sum(s.purchaseRate)+sum(isnull(s.travelCost,0)) END ) Else 0 END) AS p31to60, SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 30 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 60 Then s.mrp Else 0 END) AS m31to60, " +
                //      "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 60 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 90 Then (CASE WHEN isnull(v.istax,0)=1 THEN sum(s.purchaseRate)+((SELECT tax FROM tax)/100* sum(s.purchaseRate))+sum(ISNULL(s.travelCost,0)) ELSE sum(s.purchaseRate)+sum(isnull(s.travelCost,0)) END ) Else 0 END) AS p61to90, SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 60 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 90 Then s.mrp Else 0 END) AS m61to90, " +
                //      "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 90 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 120 Then (CASE WHEN isnull(v.istax,0)=1 THEN sum(s.purchaseRate)+((SELECT tax FROM tax)/100* sum(s.purchaseRate))+sum(ISNULL(s.travelCost,0)) ELSE sum(s.purchaseRate)+sum(isnull(s.travelCost,0)) END ) Else 0 END) AS p91to120, SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 90 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 120 Then s.mrp Else 0 END) AS m91to120, " +
                //      "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 120 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 150 Then (CASE WHEN isnull(v.istax,0)=1 THEN sum(s.purchaseRate)+((SELECT tax FROM tax)/100* sum(s.purchaseRate))+sum(ISNULL(s.travelCost,0)) ELSE sum(s.purchaseRate)+sum(isnull(s.travelCost,0)) END ) Else 0 END) AS p121to150, SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 120 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 150 Then s.mrp Else 0 END) AS m121to150, " +
                //      "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 150 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 180 Then (CASE WHEN isnull(v.istax,0)=1 THEN sum(s.purchaseRate)+((SELECT tax FROM tax)/100* sum(s.purchaseRate))+sum(ISNULL(s.travelCost,0)) ELSE sum(s.purchaseRate)+sum(isnull(s.travelCost,0)) END ) Else 0 END) AS p151to180, SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 150 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 180 Then s.mrp Else 0 END) AS m151to180, " +
                //      "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 180 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 240 Then (CASE WHEN isnull(v.istax,0)=1 THEN sum(s.purchaseRate)+((SELECT tax FROM tax)/100* sum(s.purchaseRate))+sum(ISNULL(s.travelCost,0)) ELSE sum(s.purchaseRate)+sum(isnull(s.travelCost,0)) END ) Else 0 END) AS p181to240, SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 180 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 240 Then s.mrp Else 0 END) AS m181to240, " +
                //      "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 240 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 300 Then (CASE WHEN isnull(v.istax,0)=1 THEN sum(s.purchaseRate)+((SELECT tax FROM tax)/100* sum(s.purchaseRate))+sum(ISNULL(s.travelCost,0)) ELSE sum(s.purchaseRate)+sum(isnull(s.travelCost,0)) END ) Else 0 END) AS p241to300, SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 240 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 300 Then s.mrp Else 0 END) AS m241to300, " +
                //      "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 300 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 360 Then (CASE WHEN isnull(v.istax,0)=1 THEN sum(s.purchaseRate)+((SELECT tax FROM tax)/100* sum(s.purchaseRate))+sum(ISNULL(s.travelCost,0)) ELSE sum(s.purchaseRate)+sum(isnull(s.travelCost,0)) END ) Else 0 END) AS p301to360, SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 300 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 360 Then s.mrp Else 0 END) AS m301to360, " +
                //      "(CASE WHEN((DATEDIFF(DAY, s.SystemDate, GETDATE())) > 360) Then (CASE WHEN isnull(v.istax,0)=1 THEN sum(s.purchaseRate)+((SELECT tax FROM tax)/100* sum(s.purchaseRate))+sum(ISNULL(s.travelCost,0)) ELSE sum(s.purchaseRate)+sum(isnull(s.travelCost,0)) END ) Else 0 END) AS pmore360, SUM(CASE WHEN((DATEDIFF(DAY, s.SystemDate, GETDATE())) > 360) Then s.mrp Else 0 END) AS mmore360, " +
                //      "((CASE WHEN isnull(v.istax,0)=1 THEN sum(s.purchaseRate)+((SELECT tax FROM tax)/100* sum(s.purchaseRate))+sum(ISNULL(s.travelCost,0)) ELSE sum(s.purchaseRate)+sum(isnull(s.travelCost,0)) END )) AS ptotal, SUM(s.mrp) AS mtotal, " +
                //      "c.c1name, s.purchaseRate, s.SystemDate " +
                //      "FROM StockUpInward s " +
                //      "INNER JOIN ItemStyle i ON i.styleid = s.styleid INNER JOIN COLUMN1 c ON c.col1id = i.Col1 INNER  JOIN Vendor v ON v.svid=c.Col1ID GROUP BY s.SystemDate, c.c1name, s.purchaseRate,v.istax,s.travelCost) a GROUP BY c1name ORDER BY c1name";
                command.CommandText = "SELECT substring(c1name, CHARINDEX(':', c1name)+1, len(C1Name)-(CHARINDEX(':',c1name)-1)) AS 'Vendor', " +
                      "SUM(pless30) AS 'Purchase 0-30' , SUM(mless30) AS 'MRP 0-30' , ROUND(((SUM(mless30) - SUM(pless30)) / nullif(SUM(mless30), 0)) * 100, 2) AS 'Margin 0-30', " +
                      "SUM(p31to60) AS 'Purchase 31-60', SUM(m31to60) AS 'MRP 31-60', ROUND(((SUM(m31to60) - SUM(p31to60)) / nullif(SUM(m31to60), 0)) * 100, 2) AS 'Margin 31-30', " +
                      "SUM(p61to90) AS 'Purchase 61-90', SUM(m61to90) AS 'MRP 61-90', ROUND(((SUM(m61to90) - SUM(p61to90)) / nullif(SUM(m61to90), 0)) * 100, 2) AS 'Margin 61-30', " +
                      "SUM(p91to120) AS 'Purchase 91-120', SUM(m91to120) AS 'MRP 91-120', ROUND(((SUM(m91to120) - SUM(p91to120)) / nullif(SUM(m91to120), 0)) * 100, 2) AS 'Margin 91-30', " +
                      "SUM(p121to150) AS 'Purchase 121-150', SUM(m121to150) AS 'MRP 121-150', ROUND(((SUM(m121to150) - SUM(p121to150)) / nullif(SUM(m121to150), 0)) * 100, 2) AS 'Margin 121-30', " +
                      "SUM(p151to180) AS 'Purchase 151-180', SUM(m151to180) AS 'MRP 151-180', ROUND(((SUM(m151to180) - SUM(p151to180)) / nullif(SUM(m151to180), 0)) * 100, 2) AS 'Margin 151-30', " +
                      "SUM(p181to240) AS 'Purchase 181-240', SUM(m181to240) AS 'MRP 181-240', ROUND(((SUM(m181to240) - SUM(p181to240)) / nullif(SUM(m181to240), 0)) * 100, 2) AS 'Margin 181-30', " +
                      "SUM(p241to300) AS 'Purchase 241-300', SUM(m241to300) AS 'MRP 241-300', ROUND(((SUM(m241to300) - SUM(p241to300)) / nullif(SUM(m241to300), 0)) * 100, 2) AS 'Margin 241-30', " +
                      "SUM(p301to360) AS 'Purchase 301-360', SUM(m301to360) AS 'MRP 301-360', ROUND(((SUM(m301to360) - SUM(p301to360)) / nullif(SUM(m301to360), 0)) * 100, 2) AS 'Margin 301-30', " +
                      "SUM(pmore360) AS 'Purchase 360+', SUM(mmore360) AS 'MRP 360+', ROUND(((SUM(mmore360) - SUM(pmore360)) / nullif(SUM(mmore360), 0)) * 100, 2) AS 'Margin 360+', " +
                      "SUM(ptotal) AS 'Purchase Total', SUM(mtotal) AS 'MRP Total', ROUND(((SUM(mtotal) - SUM(ptotal)) / nullif(SUM(mtotal), 0)) * 100, 2) AS 'Total Margin' " +
                      "FROM( " +
                      "SELECT " +
                      "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 30 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS pless30, SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 30 Then s.mrp Else 0 END) AS mless30, " +
                      "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 30 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 60 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p31to60, SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 30 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 60 Then s.mrp Else 0 END) AS m31to60, " +
                      "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 60 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 90 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p61to90, SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 60 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 90 Then s.mrp Else 0 END) AS m61to90, " +
                      "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 90 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 120 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p91to120, SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 90 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 120 Then s.mrp Else 0 END) AS m91to120, " +
                      "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 120 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 150 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p121to150, SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 120 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 150 Then s.mrp Else 0 END) AS m121to150, " +
                      "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 150 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 180 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p151to180, SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 150 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 180 Then s.mrp Else 0 END) AS m151to180, " +
                      "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 180 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 240 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p181to240, SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 180 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 240 Then s.mrp Else 0 END) AS m181to240, " +
                      "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 240 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 300 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p241to300, SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 240 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 300 Then s.mrp Else 0 END) AS m241to300, " +
                      "(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 300 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 360 Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS p301to360, SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 300 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 360 Then s.mrp Else 0 END) AS m301to360, " +
                      "(CASE WHEN((DATEDIFF(DAY, s.SystemDate, GETDATE())) > 360) Then sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) Else 0 END) AS pmore360, SUM(CASE WHEN((DATEDIFF(DAY, s.SystemDate, GETDATE())) > 360) Then s.mrp Else 0 END) AS mmore360, " +
                      "(sum(ISNULL(s.purchaseRate,0))+sum(ISNULL(s.taxValue,0))+sum(ISNULL(s.travelCost,0))) AS ptotal, SUM(s.mrp) AS mtotal, " +
                      "c.c1name, s.purchaseRate, s.SystemDate " +
                      "FROM StockUpInward s " +
                      "INNER JOIN ItemStyle i ON i.styleid = s.styleid INNER JOIN COLUMN1 c ON c.col1id = i.Col1 INNER JOIN Lot l ON l.BagId=s.BagID INNER JOIN Vendor v ON v.VendorID=l.VendorID GROUP BY s.SystemDate, c.c1name, s.purchaseRate,s.travelCost,s.travelCost) a GROUP BY c1name ORDER BY c1name";
            }

            else if (btnType.Equals("Stock LR"))
            {
                command.CommandText = "SELECT substring(VendorName, CHARINDEX(':', VendorName)+1, len(VendorName)-(CHARINDEX(':',VendorName)-1)) as VendorName,sum(a0to30) AS '0-30 Amount',  sum(p0to30) AS '0-30 Piece',SUM(a31to60) AS '31-60 Amount',SUM(p31to60) AS '31-60 Piece' " +
                                      ",SUM(a61to90) AS '61-90 Amount',SUM(p61to90) AS '61-90 Piece' ,SUM(a91to120) AS '91-120 Amount',SUM(a91to120) AS '91-120 Piece', " +
                                      "SUM(a121to150) AS '121-150 Amount',SUM(p121to150) AS '121-150 Piece',SUM(a151to180) AS '151-180 Amount',SUM(p151to180) AS '151-180 Piece', " +
                                      "SUM(a181to240) AS '181-240 Amount',SUM(p181to240) AS '181-240 Piece',SUM(a241to300) AS '241-300 Amount',SUM(p241to300) AS '241-300 Piece', " +
                                      "SUM(a301to360) AS '301-360 Amount',SUM(p301to360) AS '301-360 Piece',SUM(amore360) AS '360+ Amount', " +
                                      "SUM(pmore360) AS '360+ Piece',SUM(totalamt) AS 'Total Amount', SUM(totalpcs) AS 'Total Pieces' " +
                                      "FROM( " +
                                      "SELECT c.c1name as VendorName , SUM(l.totalAmount) AS Amount, SUM(l.totalPiece) AS Piece, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 30 Then SUM(l.totalAmount) Else 0 END AS a0to30, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 30 Then SUM(l.totalPiece) Else 0 END AS  p0to30, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 30 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 60 Then  SUM(l.totalAmount) Else 0 END AS a31to60, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 30 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 60 THEN  SUM(l.totalPiece) Else 0 END AS p31to60, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 60 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 90 Then  SUM(l.totalAmount) Else 0 END AS a61to90, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 60 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 90 THEN  SUM(l.totalPiece) Else 0 END AS p61to90, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 90 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 120 Then  SUM(l.totalAmount) Else 0 END AS a91to120, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 90 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 120 THEN  SUM(l.totalPiece) Else 0 END AS p91to120, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 120 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 150 Then  SUM(l.totalAmount) Else 0 END AS a121to150, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 120 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 150 THEN  SUM(l.totalPiece) Else 0 END AS p121to150, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 150 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 180 Then  SUM(l.totalAmount) Else 0 END AS a151to180, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 150 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 180 THEN  SUM(l.totalPiece) Else 0 END AS p151to180, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 180 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 240 Then  SUM(l.totalAmount) Else 0 END AS a181to240, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 180 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 240 THEN  SUM(l.totalPiece) Else 0 END AS p181to240, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 240 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 300 Then  SUM(l.totalAmount) Else 0 END AS a241to300, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 240 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 300 THEN  SUM(l.totalPiece) Else 0 END AS p241to300, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 300 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 360 Then  SUM(l.totalAmount) Else 0 END AS a301to360, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 300 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 360 Then  SUM(l.totalPiece) Else 0 END AS p301to360, " +
                                      "CASE WHEN((DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 360 or l.invoiceDate IS NULL) Then SUM(l.totalAmount) Else 0 END AS amore360, " +
                                      "CASE WHEN((DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 360 or l.invoiceDate IS NULL)  Then SUM(l.totalPiece) Else 0 END AS pmore360, " +
                                      "SUM(l.totalAmount) AS totalamt, SUM(l.totalPiece) AS totalpcs " +
                                      " " +
                                      "FROM Lot l INNER JOIN  lrListing lr ON lr.id = l.lrno INNER JOIN Vendor v ON v.VendorID = l.VendorID INNER JOIN Column1 c ON c.Col1ID=v.svid  WHERE l.IsActive = 3 GROUP BY c.c1name, invoiceDate " +
                                      ")dt GROUP BY VendorName";
            }

            else if (btnType.Equals("LR Wise Stock"))
            {
                command.CommandText = "SELECT substring(VendorName, CHARINDEX(':', VendorName)+1, len(VendorName)-(CHARINDEX(':',VendorName)-1)) as VendorName,lrno,sum(a0to30) AS '0-30 Amount',  sum(p0to30) AS '0-30 Piece',SUM(a31to60) AS '31-60 Amount',SUM(p31to60) AS '31-60 Piece' " +
                                      ",SUM(a61to90) AS '61-90 Amount',SUM(p61to90) AS '61-90 Piece' ,SUM(a91to120) AS '91-120 Amount',SUM(a91to120) AS '91-120 Piece', " +
                                      "SUM(a121to150) AS '121-150 Amount',SUM(p121to150) AS '121-150 Piece',SUM(a151to180) AS '151-180 Amount',SUM(p151to180) AS '151-180 Piece', " +
                                      "SUM(a181to240) AS '181-240 Amount',SUM(p181to240) AS '181-240 Piece',SUM(a241to300) AS '241-300 Amount',SUM(p241to300) AS '241-300 Piece', " +
                                      "SUM(a301to360) AS '301-360 Amount',SUM(p301to360) AS '301-360 Piece',SUM(amore360) AS '360+ Amount', " +
                                      "SUM(pmore360) AS '360+ Piece',SUM(totalamt) AS 'Total Amount', SUM(totalpcs) AS 'Total Pieces' " +
                                      "FROM( " +
                                      "SELECT lr.lrno,c.c1name as VendorName, SUM(l.totalAmount) AS Amount, SUM(l.totalPiece) AS Piece, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 30 Then SUM(l.totalAmount) Else 0 END AS a0to30, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 30 Then SUM(l.totalPiece) Else 0 END AS  p0to30, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 30 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 60 Then  SUM(l.totalAmount) Else 0 END AS a31to60, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 30 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 60 THEN  SUM(l.totalPiece) Else 0 END AS p31to60, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 60 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 90 Then  SUM(l.totalAmount) Else 0 END AS a61to90, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 60 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 90 THEN  SUM(l.totalPiece) Else 0 END AS p61to90, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 90 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 120 Then  SUM(l.totalAmount) Else 0 END AS a91to120, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 90 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 120 THEN  SUM(l.totalPiece) Else 0 END AS p91to120, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 120 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 150 Then  SUM(l.totalAmount) Else 0 END AS a121to150, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 120 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 150 THEN  SUM(l.totalPiece) Else 0 END AS p121to150, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 150 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 180 Then  SUM(l.totalAmount) Else 0 END AS a151to180, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 150 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 180 THEN  SUM(l.totalPiece) Else 0 END AS p151to180, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 180 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 240 Then  SUM(l.totalAmount) Else 0 END AS a181to240, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 180 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 240 THEN  SUM(l.totalPiece) Else 0 END AS p181to240, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 240 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 300 Then  SUM(l.totalAmount) Else 0 END AS a241to300, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 240 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 300 THEN  SUM(l.totalPiece) Else 0 END AS p241to300, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 300 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 360 Then  SUM(l.totalAmount) Else 0 END AS a301to360, " +
                                      "CASE When(DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 300 AND(DATEDIFF(DAY, l.invoiceDate, GETDATE())) <= 360 Then  SUM(l.totalPiece) Else 0 END AS p301to360, " +
                                      "CASE WHEN((DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 360 or l.invoiceDate IS NULL) Then SUM(l.totalAmount) Else 0 END AS amore360, " +
                                      "CASE WHEN((DATEDIFF(DAY, l.invoiceDate, GETDATE())) > 360 or l.invoiceDate IS NULL)  Then SUM(l.totalPiece) Else 0 END AS pmore360, " +
                                      "SUM(l.totalAmount) AS totalamt, SUM(l.totalPiece) AS totalpcs " +
                                      " " +
                                      "FROM Lot l INNER JOIN  lrListing lr ON lr.id = l.lrno INNER JOIN Vendor v ON v.VendorID = l.VendorID INNER JOIN Column1 c ON c.Col1ID=v.svid  WHERE l.IsActive = 3 GROUP BY lr.lrno,c.c1name, invoiceDate " +
                                      ")dt GROUP BY VendorName,lrno";
            }

            else if (btnType.Equals("Sales With Trader Note"))
            {
                command.CommandText = "SELECT substring(c1name, CHARINDEX(':', c1name)+1, len(C1Name)-(CHARINDEX(':',c1name)-1))  AS 'Vendor', SUM(pless30) AS 'Purchase 0-30' ,SUM(sless30) AS 'Sales 0-30', ROUND(((SUM(sless30) - SUM(pless30)) / nullif(SUM(sless30), 0)) * 100, 2) AS 'Margin 0-30', SUM(p31to60) AS 'Purchase 31-60',SUM(s31to60) AS 'Sales 31-60',ROUND(((SUM(s31to60) - SUM(p31to60)) / nullif(SUM(s31to60), 0)) * 100, 2) AS 'Margin 31-60', SUM(p61to90) AS 'Purchase 61-90', SUM(s61to90) AS 'Sales 61-90',ROUND(((SUM(s61to90) - SUM(p61to90)) / nullif(SUM(s61to90), 0)) * 100, 2) AS 'Margin 61-90', SUM(p91to120) AS 'Purchase 91-120',  SUM(s91to120) AS 'Sales 91-120',ROUND(((SUM(s91to120) - SUM(p91to120)) / nullif(SUM(s91to120), 0)) * 100, 2) AS 'Margin 91-120', SUM(p121to150) AS 'Purchase 121-150', SUM(s121to150) AS 'Sales 121-150',ROUND(((SUM(s121to150) - SUM(p121to150)) / nullif(SUM(s121to150), 0)) * 100, 2) AS 'Margin 121-150', SUM(p151to180) AS 'Purchase 151-180', SUM(s151to180) AS 'Sales 151-180',ROUND(((SUM(s151to180) - SUM(p151to180)) / nullif(SUM(s151to180), 0)) * 100, 2) AS 'Margin 151-180', SUM(p181to240) AS 'Purchase 181-240', SUM(s181to240) AS 'Sales 181-240',ROUND(((SUM(s181to240) - SUM(p181to240)) / nullif(SUM(s181to240), 0)) * 100, 2) AS 'Margin 181-240', SUM(p241to300) AS 'Purchase 241-300', SUM(s241to300) AS 'Sales 241-300',ROUND(((SUM(s241to300) - SUM(p241to300)) / nullif(SUM(s241to300), 0)) * 100, 2) AS 'Margin 241-300', SUM(p301to360) AS 'Purchase 301-360',  SUM(s301to360) AS 'Sales 301-360',ROUND(((SUM(s301to360) - SUM(p301to360)) / nullif(SUM(s301to360), 0)) * 100, 2) AS 'Margin 301-360', SUM(pmore360) AS 'Purchase 360+', SUM(smore360) AS 'Sales 360+',ROUND(((SUM(smore360) - SUM(pmore360)) / nullif(SUM(smore360), 0)) * 100, 2) AS 'Margin 360+', SUM(totalpurchase) AS 'Total Purchase', SUM(totalsales) AS 'Total Sales',ROUND(((SUM(totalsales) - SUM(totalpurchase)) / nullif(SUM(totalsales), 0)) * 100, 2) AS 'Total Margin' FROM( SELECT SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 30 Then a.purchaseRate Else 0 END) AS pless30, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 30 Then s.sellingprice Else 0 END) AS sless30, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 30 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 60 Then a.purchaseRate Else 0 END) AS p31to60, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 30 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 60 THEN s.sellingprice Else 0 END) AS s31to60, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 60 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 90 Then a.purchaseRate Else 0 END) AS p61to90, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 60 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 90 Then s.sellingprice Else 0 END) AS s61to90, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 90 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 120 Then a.purchaseRate Else 0 END) AS p91to120, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 90 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 120 Then s.sellingprice Else 0 END) AS s91to120, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 120 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 150 Then a.purchaseRate Else 0 END) AS p121to150, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 120 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 150 Then s.sellingprice Else 0 END) AS s121to150, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 150 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 180 Then a.purchaseRate Else 0 END) AS p151to180, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 150 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 180 Then s.sellingprice Else 0 END) AS s151to180, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 180 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 240 Then a.purchaseRate Else 0 END) AS p181to240, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 180 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 240 Then s.sellingprice Else 0 END) AS s181to240, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 240 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 300 Then a.purchaseRate Else 0 END) AS p241to300, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 240 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 300 Then s.sellingprice Else 0 END) AS s241to300, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 300 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 360 Then a.purchaseRate Else 0 END) AS p301to360, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 300 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 360 Then s.sellingprice Else 0 END) AS s301to360, SUM(CASE WHEN((DATEDIFF(DAY, a.SystemDate, GETDATE())) > 360) Then a.purchaseRate Else 0 END) AS pmore360, SUM(CASE WHEN((DATEDIFF(DAY, a.SystemDate, GETDATE())) > 360) Then s.sellingprice Else 0 END) AS smore360, SUM(a.purchaseRate) AS totalpurchase, SUM(s.sellingprice) AS totalsales, c.c1name, a.purchaseRate, a.SystemDate FROM ArchiveStockUpInward a INNER JOIN ItemStyle i ON i.styleid = a.styleid INNER JOIN COLUMN1 c ON c.col1id = i.Col1 inner join salesrecord s ON s.archiveid = a.ArchiveStockupId inner join login l ON l.userid = a.userid WHERE a.RackBarcode IS null  and s.returntimestamp IS NULL AND convert(date, s.salesDateTime) BETWEEN convert(date,@frmDate) AND convert(date,@toDate) GROUP BY a.SystemDate, c.c1name, a.purchaseRate) b GROUP BY c1name ORDER BY c1name";
            }

            else if (btnType.Equals("Stock Location"))
                {
                string where = "";
                string lrwhere = "";
                if(commingfrom.Equals("Less30"))
                {
                    where = " and DATEDIFF(DAY, s.SystemDate, GETDATE()) <= 30";
                    lrwhere = " and DATEDIFF(DAY, l.invoiceDate, GETDATE()) <= 30";
                }
                else if (commingfrom.Equals("More360"))
                {
                    where = " and DATEDIFF(DAY,s.SystemDate, GETDATE()) > 360";
                    lrwhere = " and DATEDIFF(DAY,l.invoiceDate, GETDATE()) > 360 or l.invoiceDate IS NULL ";
                }
                else if (commingfrom.Equals("Normal"))
                {
                    where = " and DATEDIFF(DAY,s.SystemDate, GETDATE()) > @minval AND (DATEDIFF(DAY,s.SystemDate, GETDATE())) <= @maxval";
                    lrwhere = " and DATEDIFF(DAY,l.invoiceDate, GETDATE()) > @minval AND (DATEDIFF(DAY,l.invoiceDate, GETDATE())) <= @maxval";
                }


                command.CommandText = "SELECT VendorName,Title,SUM(Amount) AS Amount,SUM(Quantity) AS Quantity,RackBarcode,StyleCode  " +
                                      "FROM ( " +
                                      "SELECT substring(c.c1name, CHARINDEX(':', c.c1name)+1, len(c.C1Name)-(CHARINDEX(':',c.c1name)-1)) AS  VendorName,i.Title,sum(isnull(s.purchaseRate,0))+sum(isnull(s.taxValue,0))+sum(ISNULL(s.travelCost,0)) AS Amount,COUNT(s.StockupID) AS Quantity,s.RackBarcode,i.StyleCode  " +
                                      "FROM StockUpInward s INNER JOIN ItemStyle i ON i.styleid = s.styleid "+ where + " INNER JOIN COLUMN1 c ON c.col1id = i.Col1  AND  c.Col1ID ="+VendorID+"  INNER JOIN Lot l ON l.BagId=s.BagID  INNER JOIN Vendor v ON v.VendorID=l.VendorID     " +
                                      "GROUP BY c.C1Name,s.RackBarcode,i.StyleCode,i.Title   " +
                                      "UNION ALL   " +
                                      "SELECT substring(c.c1name, CHARINDEX(':', c.c1name)+1, len(c.c1name)-(CHARINDEX(':',c.c1name)-1)) as VendorName,l.BagDescription AS Title,SUM(l.totalAmount) AS Amount,SUM(l.totalPiece) AS Quantity, 'LR' AS  RackBarcode,'' AS StyleCode  " +
                                      "FROM Lot l INNER JOIN  lrListing lr ON lr.id = l.lrno AND l.IsActive = 3 " + lrwhere + " INNER JOIN Vendor v ON v.VendorID = l.VendorID INNER JOIN Column1 c ON c.Col1ID=v.svid AND c.Col1ID=" + VendorID + "       " +
                                      "GROUP BY c.c1name,l.BagDescription " +
                                      ") dt " +
                                      "GROUP BY Title,VendorName,RackBarcode,StyleCode";
            }
            command.Parameters.AddWithValue("@frmDate", frmDate);
            command.Parameters.AddWithValue("@toDate", toDate);
            command.Parameters.AddWithValue("@minval", minval);
            command.Parameters.AddWithValue("@maxval", maxval);
            command.Parameters.AddWithValue("@VendorID", VendorID);
            catTable.Load(command.ExecuteReader());

            if ( btnType.Equals("VendorWise Stock Shop") || btnType.Equals("VendorWise Stock Warehouse"))
            {

                if (!catTable.Rows.Count.Equals(0))
                {
                    object one = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["0-30"])))).ToString();
                    object two = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["31-60"])))).ToString();
                    object three = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["61-90"])))).ToString();
                    object four = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["91-120"])))).ToString();
                    object five = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["121-150"])))).ToString();
                    object six = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["151-180"])))).ToString();
                    object seven = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["181-240"])))).ToString();
                    object eight = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["241-300"])))).ToString();
                    object nine = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["301-360"])))).ToString();
                    object ten = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["360+"])))).ToString();
                    object grandtotal = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Grand Total"])))).ToString();


                    catTable.Rows.Add("Total", Math.Round(Convert.ToDecimal(one), 2), Math.Round(Convert.ToDecimal(two), 2)
                        , Math.Round(Convert.ToDecimal(three), 2), Math.Round(Convert.ToDecimal(four), 2), Math.Round(Convert.ToDecimal(five), 2)
                        , Math.Round(Convert.ToDecimal(six), 2), Math.Round(Convert.ToDecimal(seven), 2), Math.Round(Convert.ToDecimal(eight), 2)
                        , Math.Round(Convert.ToDecimal(nine), 2), Math.Round(Convert.ToDecimal(ten), 2), Math.Round(Convert.ToDecimal(grandtotal), 2));
                }
            }
            else if (btnType.Equals("Sales For Warehouse") || btnType.Equals("Sales For Shop") || btnType.Equals("Sales With Margin") || btnType.Equals("Sales With Trader Note"))
            {
                if (!catTable.Rows.Count.Equals(0))
                {
                    object one = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Purchase 0-30"])))).ToString();
                    object two = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Sales 0-30"])))).ToString();

                    object three = "0";
                    if (!two.Equals("0.00"))
                    {
                        three = Math.Round(((Convert.ToDecimal(two) - Convert.ToDecimal(one)) / Convert.ToDecimal(two)) * 100, 2);
                    }

                    object four = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Purchase 31-60"])))).ToString();
                    object five = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Sales 31-60"])))).ToString();

                    object six = "0";
                    if (!five.Equals("0.00"))
                    {
                        six = Math.Round(((Convert.ToDecimal(five) - Convert.ToDecimal(four)) / Convert.ToDecimal(five)) * 100, 2);
                    }

                    object seven = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Purchase 61-90"])))).ToString();
                    object eight = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Sales 61-90"])))).ToString();

                    object nine = "0";
                    if (!eight.Equals("0.00"))
                    {
                        nine = Math.Round(((Convert.ToDecimal(eight) - Convert.ToDecimal(seven)) / Convert.ToDecimal(eight)) * 100, 2);
                    }

                    object ten = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Purchase 91-120"])))).ToString();
                    object elvn = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Sales 91-120"])))).ToString();

                    object twl = "0";
                    if (!elvn.Equals("0.00"))
                    {
                        twl = Math.Round(((Convert.ToDecimal(elvn) - Convert.ToDecimal(ten)) / Convert.ToDecimal(elvn)) * 100, 2);
                    }

                    object thr = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Purchase 121-150"])))).ToString();
                    object frty = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Sales 121-150"])))).ToString();

                    object ffty = "0";
                    if (!frty.Equals("0.00"))
                    {
                        ffty = Math.Round(((Convert.ToDecimal(frty) - Convert.ToDecimal(thr)) / Convert.ToDecimal(frty)) * 100, 2);
                    }

                    object sixty = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Purchase 151-180"])))).ToString();
                    object eighty = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Sales 151-180"])))).ToString();

                    object ninty = "0";
                    if (!eighty.Equals("0.00"))
                    {
                        ninty = Math.Round(((Convert.ToDecimal(eighty) - Convert.ToDecimal(sixty)) / Convert.ToDecimal(eighty)) * 100, 2);
                    }


                    object twnty = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Purchase 181-240"])))).ToString();
                    object twnty1 = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Sales 181-240"])))).ToString();

                    object twnty2 = "0";
                    if (!twnty1.Equals("0.00"))
                    {
                        twnty2 = Math.Round(((Convert.ToDecimal(twnty1) - Convert.ToDecimal(twnty)) / Convert.ToDecimal(twnty1)) * 100, 2);
                    }


                    object twnty3 = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Purchase 241-300"])))).ToString();
                    object twnty4 = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Sales 241-300"])))).ToString();

                    object twnty5 = "0";
                    if (!twnty4.Equals("0.00"))
                    {
                        twnty5 = Math.Round(((Convert.ToDecimal(twnty4) - Convert.ToDecimal(twnty3)) / Convert.ToDecimal(twnty4)) * 100, 2);
                    }

                    object twnty6 = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Purchase 301-360"])))).ToString();
                    object twnty7 = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Sales 301-360"])))).ToString();

                    object twnty8 = "0";
                    if (!twnty7.Equals("0.00"))
                    {
                        twnty8 = Math.Round(((Convert.ToDecimal(twnty7) - Convert.ToDecimal(twnty6)) / Convert.ToDecimal(twnty7)) * 100, 2);
                    }



                    object twnty9 = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Purchase 360+"])))).ToString();
                    object thrty = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Sales 360+"])))).ToString();

                    object thrty1 = "0";
                    if (!thrty.Equals("0.00"))
                    {
                        thrty1 = Math.Round(((Convert.ToDecimal(thrty) - Convert.ToDecimal(twnty9)) / Convert.ToDecimal(thrty)) * 100, 2);
                    }



                    object thrty2 = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Total Purchase"])))).ToString();
                    object thrty3 = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Total Sales"])))).ToString();
                    object thrty4 = "0";
                    if (!thrty3.Equals("0.00"))
                    {
                        thrty4 = Math.Round(((Convert.ToDecimal(thrty3) - Convert.ToDecimal(thrty2)) / Convert.ToDecimal(thrty3)) * 100, 2);
                    }


                    catTable.Rows.Add("Total", Math.Round(Convert.ToDecimal(one), 2), Math.Round(Convert.ToDecimal(two), 2)
                        , Math.Round(Convert.ToDecimal(three), 2), Math.Round(Convert.ToDecimal(four), 2), Math.Round(Convert.ToDecimal(five), 2)
                        , Math.Round(Convert.ToDecimal(six), 2), Math.Round(Convert.ToDecimal(seven), 2), Math.Round(Convert.ToDecimal(eight), 2)
                        , Math.Round(Convert.ToDecimal(nine), 2), Math.Round(Convert.ToDecimal(ten), 2), Math.Round(Convert.ToDecimal(elvn), 2)
                        , Math.Round(Convert.ToDecimal(twl), 2), Math.Round(Convert.ToDecimal(thr), 2), Math.Round(Convert.ToDecimal(frty), 2),
                        Math.Round(Convert.ToDecimal(ffty), 2), Math.Round(Convert.ToDecimal(sixty), 2), Math.Round(Convert.ToDecimal(eighty), 2)
                         , Math.Round(Convert.ToDecimal(ninty), 2), Math.Round(Convert.ToDecimal(twnty), 2), Math.Round(Convert.ToDecimal(twnty1), 2)
                        , Math.Round(Convert.ToDecimal(twnty2), 2), Math.Round(Convert.ToDecimal(twnty3), 2), Math.Round(Convert.ToDecimal(twnty4), 2)
                        , Math.Round(Convert.ToDecimal(twnty5), 2), Math.Round(Convert.ToDecimal(twnty6), 2), Math.Round(Convert.ToDecimal(twnty7), 2)
                        , Math.Round(Convert.ToDecimal(twnty8), 2), Math.Round(Convert.ToDecimal(twnty9), 2), Math.Round(Convert.ToDecimal(thrty), 2)
                        , Math.Round(Convert.ToDecimal(thrty1), 2), Math.Round(Convert.ToDecimal(thrty2), 2), Math.Round(Convert.ToDecimal(thrty3), 2), Math.Round(Convert.ToDecimal(thrty4), 2));

                }
            }


            else if (btnType.Equals("Purchase With Margin"))
            {
                if (!catTable.Rows.Count.Equals(0))
                {
                    object one = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Purchase 0-30"])))).ToString();
                    object two = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["MRP 0-30"])))).ToString();
                    object three = "0";
                    if (!two.Equals("0.00"))
                    {
                        three = Math.Round(((Convert.ToDecimal(two) - Convert.ToDecimal(one)) / Convert.ToDecimal(two)) * 100, 2);
                    }
                    object four = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Purchase 31-60"])))).ToString();
                    object five = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["MRP 31-60"])))).ToString();

                    object six = "0";
                    if (!five.Equals("0.00"))
                    {
                        six = Math.Round(((Convert.ToDecimal(five) - Convert.ToDecimal(four)) / Convert.ToDecimal(five)) * 100, 2);
                    }

                    object seven = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Purchase 61-90"])))).ToString();
                    object eight = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["MRP 61-90"])))).ToString();

                    object nine = "0";
                    if (!eight.Equals("0.00"))
                    {
                        nine = Math.Round(((Convert.ToDecimal(eight) - Convert.ToDecimal(seven)) / Convert.ToDecimal(eight)) * 100, 2);
                    }

                    object ten = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Purchase 91-120"])))).ToString();
                    object elvn = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["MRP 91-120"])))).ToString();

                    object twl = "0";
                    if (!elvn.Equals("0.00"))
                    {
                        twl = Math.Round(((Convert.ToDecimal(elvn) - Convert.ToDecimal(ten)) / Convert.ToDecimal(elvn)) * 100, 2);
                    }

                    object thr = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Purchase 121-150"])))).ToString();
                    object frty = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["MRP 121-150"])))).ToString();

                    object ffty = "0";
                    if (!frty.Equals("0.00"))
                    {
                        ffty = Math.Round(((Convert.ToDecimal(frty) - Convert.ToDecimal(thr)) / Convert.ToDecimal(frty)) * 100, 2);
                    }

                    object sixty = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Purchase 151-180"])))).ToString();
                    object eighty = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["MRP 151-180"])))).ToString();

                    object ninty = "0";
                    if (!eighty.Equals("0.00"))
                    {
                        ninty = Math.Round(((Convert.ToDecimal(eighty) - Convert.ToDecimal(sixty)) / Convert.ToDecimal(eighty)) * 100, 2);
                    }


                    object twnty = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Purchase 181-240"])))).ToString();
                    object twnty1 = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["MRP 181-240"])))).ToString();

                    object twnty2 = "0";
                    if (!twnty1.Equals("0.00"))
                    {
                        twnty2 = Math.Round(((Convert.ToDecimal(twnty1) - Convert.ToDecimal(twnty)) / Convert.ToDecimal(twnty1)) * 100, 2);
                    }


                    object twnty3 = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Purchase 241-300"])))).ToString();
                    object twnty4 = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["MRP 241-300"])))).ToString();

                    object twnty5 = "0";
                    if (!twnty4.Equals("0.00"))
                    {
                        twnty5 = Math.Round(((Convert.ToDecimal(twnty4) - Convert.ToDecimal(twnty3)) / Convert.ToDecimal(twnty4)) * 100, 2);
                    }

                    object twnty6 = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Purchase 301-360"])))).ToString();
                    object twnty7 = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["MRP 301-360"])))).ToString();

                    object twnty8 = "0";
                    if (!twnty7.Equals("0.00"))
                    {
                        twnty8 = Math.Round(((Convert.ToDecimal(twnty7) - Convert.ToDecimal(twnty6)) / Convert.ToDecimal(twnty7)) * 100, 2);
                    }



                    object twnty9 = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Purchase 360+"])))).ToString();
                    object thrty = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["MRP 360+"])))).ToString();
                    object thrty1 = "0";
                    if (!thrty.Equals("0.00"))
                    {
                        thrty1 = Math.Round(((Convert.ToDecimal(twnty9) - Convert.ToDecimal(thrty)) / Convert.ToDecimal(twnty9)) * 100, 2);
                    }

                    object thrty2 = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["Purchase Total"])))).ToString();
                    object thrty3 = (Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x["MRP Total"])))).ToString();
                    object thrty4 = "0";
                    if (!thrty3.Equals("0.00"))
                    {
                        thrty4 = Math.Round(((Convert.ToDecimal(thrty3) - Convert.ToDecimal(thrty2)) / Convert.ToDecimal(thrty3)) * 100, 2);
                    }


                    catTable.Rows.Add("Total", Math.Round(Convert.ToDecimal(one), 2), Math.Round(Convert.ToDecimal(two), 2)
                        , Math.Round(Convert.ToDecimal(three), 2), Math.Round(Convert.ToDecimal(four), 2), Math.Round(Convert.ToDecimal(five), 2)
                        , Math.Round(Convert.ToDecimal(six), 2), Math.Round(Convert.ToDecimal(seven), 2), Math.Round(Convert.ToDecimal(eight), 2)
                        , Math.Round(Convert.ToDecimal(nine), 2), Math.Round(Convert.ToDecimal(ten), 2), Math.Round(Convert.ToDecimal(elvn), 2)
                        , Math.Round(Convert.ToDecimal(twl), 2), Math.Round(Convert.ToDecimal(thr), 2), Math.Round(Convert.ToDecimal(frty), 2),
                        Math.Round(Convert.ToDecimal(ffty), 2), Math.Round(Convert.ToDecimal(sixty), 2), Math.Round(Convert.ToDecimal(eighty), 2)
                         , Math.Round(Convert.ToDecimal(ninty), 2), Math.Round(Convert.ToDecimal(twnty), 2), Math.Round(Convert.ToDecimal(twnty1), 2)
                        , Math.Round(Convert.ToDecimal(twnty2), 2), Math.Round(Convert.ToDecimal(twnty3), 2), Math.Round(Convert.ToDecimal(twnty4), 2)
                        , Math.Round(Convert.ToDecimal(twnty5), 2), Math.Round(Convert.ToDecimal(twnty6), 2), Math.Round(Convert.ToDecimal(twnty7), 2)
                        , Math.Round(Convert.ToDecimal(twnty8), 2), Math.Round(Convert.ToDecimal(twnty9), 2), Math.Round(Convert.ToDecimal(thrty), 2)
                        , Math.Round(Convert.ToDecimal(thrty1), 2), Math.Round(Convert.ToDecimal(thrty2), 2), Math.Round(Convert.ToDecimal(thrty3), 2), Math.Round(Convert.ToDecimal(thrty4), 2));
                }
            }


            else if (btnType.Equals("Stock LR") || btnType.Equals("VendorWise Stock") || btnType.Equals("LR Wise Stock"))
            {
                DataRow dr = catTable.NewRow();
                foreach (DataColumn dc in catTable.Columns)
                {
                    if (dc.ToString().Equals("VendorName"))
                    {
                        dr["VendorName"] = "Total";
                    }
                    else if(dc.ToString().Equals("lrno"))
                    {
                        dr["lrno"] = " ";
                    }
                    else
                    {

                        dr[dc] = Math.Round(Convert.ToDecimal(catTable.AsEnumerable().Sum(x => Convert.ToDecimal(x[dc]))), 2);

                    }
                }
                catTable.Rows.Add(dr);
            }
            else if (btnType.Equals("Stock Location"))
            {
                if (!catTable.Rows.Count.Equals(0))
                {
                    //sumObject = table.Compute("Sum(Amount)", string.Empty);
                    object sum = catTable.Compute("Sum(Amount)", string.Empty);
                    object count =catTable.Compute("Sum(Quantity)", string.Empty);
                    catTable.Rows.Add("Total", "", Math.Round(Convert.ToDecimal(sum), 2), Math.Round(Convert.ToDecimal(count), 2), "", "");
                }
            }

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

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

            }
            catch (Exception ex2)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex2);


            }
        }
        return catTable;
    }
}