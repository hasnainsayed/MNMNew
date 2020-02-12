﻿using System;
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

    public DataTable getRecord(string btnType, string frmDate, string toDate)
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
                command.CommandText = "SELECT c1name AS 'Vendor', SUM(pless30) AS '0-30' , SUM(p31to60) AS '31-60', SUM(p61to90) AS '61-90', SUM(p91to120) AS '91-120', SUM(p121to150) AS '121-150', SUM(p151to180) AS '151-180', SUM(p181to240) AS '181-240', SUM(p241to300) AS '241-300', SUM(p301to360) AS '301-360', SUM(pmore360) AS '360+', SUM(total) AS 'Grand Total' FROM (SELECT SUM(CASE When (DATEDIFF(DAY, s.SystemDate, GETDATE()) )<=30 Then s.purchaseRate Else 0 END ) AS pless30 ,SUM(CASE When (DATEDIFF(DAY, s.SystemDate, GETDATE()) )>30 AND (DATEDIFF(DAY, s.SystemDate, GETDATE()) )<=60 Then s.purchaseRate Else 0 END ) AS p31to60 ,SUM(CASE When (DATEDIFF(DAY, s.SystemDate, GETDATE()) )>60 AND (DATEDIFF(DAY, s.SystemDate, GETDATE()) )<=90 Then s.purchaseRate Else 0 END ) AS p61to90 ,SUM(CASE When (DATEDIFF(DAY, s.SystemDate, GETDATE()) )>90 AND (DATEDIFF(DAY, s.SystemDate, GETDATE()) )<=120 Then s.purchaseRate Else 0 END ) AS p91to120 ,SUM(CASE When (DATEDIFF(DAY, s.SystemDate, GETDATE()) )>120 AND (DATEDIFF(DAY, s.SystemDate, GETDATE()) )<=150 Then s.purchaseRate Else 0 END ) AS p121to150 ,SUM(CASE When (DATEDIFF(DAY, s.SystemDate, GETDATE()) )>150 AND (DATEDIFF(DAY, s.SystemDate, GETDATE()) )<=180 Then s.purchaseRate Else 0 END ) AS p151to180 ,SUM(CASE When (DATEDIFF(DAY, s.SystemDate, GETDATE()) )>180 AND (DATEDIFF(DAY, s.SystemDate, GETDATE()) )<=240 Then s.purchaseRate Else 0 END ) AS p181to240 , SUM(CASE When (DATEDIFF(DAY, s.SystemDate, GETDATE()) )>240 AND (DATEDIFF(DAY, s.SystemDate, GETDATE()) )<=300 Then s.purchaseRate Else 0 END ) AS p241to300 ,SUM(CASE When (DATEDIFF(DAY, s.SystemDate, GETDATE()) )>300 AND (DATEDIFF(DAY, s.SystemDate, GETDATE()) )<=360 Then s.purchaseRate Else 0 END ) AS p301to360 , SUM(CASE WHEN ((DATEDIFF(DAY, s.SystemDate, GETDATE()) )>360 ) Then s.purchaseRate Else 0 END ) AS pmore360 ,SUM(s.purchaseRate) AS total,c.c1name,s.purchaseRate,s.SystemDate FROM StockUpInward s INNER JOIN ItemStyle i ON i.styleid=s.styleid INNER JOIN COLUMN1 c ON c.col1id = i.Col1 GROUP BY s.SystemDate, c.c1name, s.purchaseRate) a GROUP BY c1name ORDER BY c1name;";
            }
            //getStock
            else if (btnType.Equals("Stock"))
            {
                command.CommandText = "SELECT '0-30' as Age, SUM(purchaseRate) AS 'Purchase Amount', ROUND((SELECT  SUM(purchaseRate) from StockUpInward S where S.DateTime>= DATEADD(DAY,-30,GETDATE()) and S.DateTime < GETDATE()) *100 / (Select SUM(purchaseRate) FROM stockupinward),2) AS Percentage from StockUpInward S where S.DateTime>= DATEADD(DAY,-30,GETDATE()) and S.DateTime <= GETDATE()UNION ALL SELECT '31-60' as Age, SUM(purchaseRate) AS 'Purchase Amount', ROUND((SELECT  SUM(purchaseRate) from StockUpInward S where S.DateTime>= DATEADD(DAY,-60,GETDATE()) and S.DateTime < DATEADD(DAY,-30,GETDATE())) *100 / (Select SUM(purchaseRate) FROM stockupinward),2) AS Percentage from StockUpInward S where S.DateTime>= DATEADD(DAY,-60,GETDATE()) and S.DateTime < DATEADD(DAY,-30,GETDATE())UNION ALL SELECT '61-90' as Age, SUM(purchaseRate) AS 'Purchase Amount', ROUND((SELECT  SUM(purchaseRate) from StockUpInward S where S.DateTime>= DATEADD(DAY,-90,GETDATE()) and S.DateTime < DATEADD(DAY,-60,GETDATE())) *100 / (Select SUM(purchaseRate) FROM stockupinward),2) AS Percentage from StockUpInward S where S.DateTime>= DATEADD(DAY,-90,GETDATE()) and S.DateTime < DATEADD(DAY,-60,GETDATE())UNION ALL SELECT '91-120' as Age, SUM(purchaseRate) AS 'Purchase Amount', ROUND((SELECT  SUM(purchaseRate) from StockUpInward S where S.DateTime>= DATEADD(DAY,-120,GETDATE()) and S.DateTime < DATEADD(DAY,-90,GETDATE())) *100 / (Select SUM(purchaseRate) FROM stockupinward),2) AS Percentage from StockUpInward S where S.DateTime>= DATEADD(DAY,-120,GETDATE()) and S.DateTime < DATEADD(DAY,-90,GETDATE()) UNION ALL SELECT '121-150' as Age, SUM(purchaseRate) AS 'Purchase Amount', ROUND((SELECT  SUM(purchaseRate) from StockUpInward S where S.DateTime>= DATEADD(DAY,-150,GETDATE()) and S.DateTime < DATEADD(DAY,-120,GETDATE())) *100 / (Select SUM(purchaseRate) FROM stockupinward),2) AS Percentage from StockUpInward S where S.DateTime>= DATEADD(DAY,-150,GETDATE()) and S.DateTime < DATEADD(DAY,-120,GETDATE())UNION ALL SELECT '151-180' as Age, SUM(purchaseRate) AS 'Purchase Amount', ROUND((SELECT  SUM(purchaseRate) from StockUpInward S where S.DateTime>= DATEADD(DAY,-180,GETDATE()) and S.DateTime < DATEADD(DAY,-150,GETDATE())) *100 / (Select SUM(purchaseRate) FROM stockupinward),2) AS Percentage from StockUpInward S where S.DateTime>= DATEADD(DAY,-180,GETDATE()) and S.DateTime < DATEADD(DAY,-150,GETDATE())UNION ALL SELECT '181-240' as Age, SUM(purchaseRate) AS 'Purchase Amount', ROUND((SELECT  SUM(purchaseRate) from StockUpInward S where S.DateTime>= DATEADD(DAY,-240,GETDATE()) and S.DateTime < DATEADD(DAY,-180,GETDATE())) *100 / (Select SUM(purchaseRate) FROM stockupinward),2) AS Percentage from StockUpInward S where S.DateTime>= DATEADD(DAY,-240,GETDATE()) and S.DateTime < DATEADD(DAY,-180,GETDATE())UNION ALL SELECT '241-300' as Age, SUM(purchaseRate) AS 'Purchase Amount', ROUND((SELECT  SUM(purchaseRate) from StockUpInward S where S.DateTime>= DATEADD(DAY,-300,GETDATE()) and S.DateTime < DATEADD(DAY,-240,GETDATE())) *100 / (Select SUM(purchaseRate) FROM stockupinward),2) AS Percentage from StockUpInward S where S.DateTime>= DATEADD(DAY,-300,GETDATE()) and S.DateTime < DATEADD(DAY,-240,GETDATE())UNION ALL SELECT '301-360' as Age, SUM(purchaseRate) AS 'Purchase Amount', ROUND((SELECT  SUM(purchaseRate) from StockUpInward S where S.DateTime>= DATEADD(DAY,-360,GETDATE()) and S.DateTime < DATEADD(DAY,-300,GETDATE())) *100 / (Select SUM(purchaseRate) FROM stockupinward),2) AS Percentage from StockUpInward S where S.DateTime>= DATEADD(DAY,-360,GETDATE()) and S.DateTime < DATEADD(DAY,-300,GETDATE())UNION ALL SELECT '360+' as Age, SUM(purchaseRate) AS 'Purchase Amount', ROUND((SELECT  SUM(purchaseRate) from StockUpInward S where S.DateTime <= DATEADD(DAY,-361,GETDATE())) *100 / (Select SUM(purchaseRate) FROM stockupinward),2) AS Percentage from StockUpInward S where S.DateTime <= DATEADD(DAY,-361,GETDATE())UNION ALL SELECT 'Grand Total' as Age, SUM(purchaseRate) AS 'Purchase Amount', ROUND((SELECT  SUM(purchaseRate) from StockUpInward ) *100 / (Select SUM(purchaseRate) FROM stockupinward),2) AS Percentage from StockUpInward";
            }
            //getVendorWiseStockShop
            else if (btnType.Equals("VendorWise Stock Shop"))
            {
                command.CommandText = "SELECT c1name AS 'Vendor', SUM(pless30) AS '0-30' , SUM(p31to60) AS '31-60', SUM(p61to90) AS '61-90', SUM(p91to120) AS '91-120', SUM(p121to150) AS '121-150', SUM(p151to180) AS '151-180', SUM(p181to240) AS '181-240', SUM(p241to300) AS '241-300', SUM(p301to360) AS '301-360', SUM(pmore360) AS '360+', SUM(total) AS 'Grand Total' "+
                    "FROM( " +
                    "SELECT " +
                    "SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 30 Then s.purchaseRate Else 0 END) AS pless30, " +
                    "SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 30 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 60 Then s.purchaseRate Else 0 END) AS p31to60, " +
                    "SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 60 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 90 Then s.purchaseRate Else 0 END) AS p61to90, " +
                    "SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 90 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 120 Then s.purchaseRate Else 0 END) AS p91to120, " +
                    "SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 120 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 150 Then s.purchaseRate Else 0 END) AS p121to150, " +
                    "SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 150 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 180 Then s.purchaseRate Else 0 END) AS p151to180, " +
                    "SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 180 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 240 Then s.purchaseRate Else 0 END) AS p181to240, " +
                    "SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 240 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 300 Then s.purchaseRate Else 0 END) AS p241to300, " +
                    "SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 300 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 360 Then s.purchaseRate Else 0 END) AS p301to360, " +
                    "SUM(CASE WHEN((DATEDIFF(DAY, s.SystemDate, GETDATE())) > 360) Then s.purchaseRate Else 0 END) AS pmore360, " +
                    "SUM(s.purchaseRate) AS total, " +
                    "c.c1name, s.purchaseRate, s.SystemDate " +
                    "FROM StockUpInward s " +
                    "INNER JOIN ItemStyle i ON i.styleid = s.styleid INNER JOIN COLUMN1 c ON c.col1id = i.Col1 WHERE s.physicalId = 3 GROUP BY s.SystemDate, c.c1name, s.purchaseRate) a GROUP BY c1name ORDER BY c1name";
            }
            //getVendorWiseStockWarehouse
            else if (btnType.Equals("VendorWise Stock Warehouse"))
            {
                command.CommandText = "SELECT c1name AS 'Vendor', SUM(pless30) AS '0-30' , SUM(p31to60) AS '31-60', SUM(p61to90) AS '61-90', SUM(p91to120) AS '91-120', SUM(p121to150) AS '121-150', SUM(p151to180) AS '151-180', SUM(p181to240) AS '181-240', SUM(p241to300) AS '241-300', SUM(p301to360) AS '301-360', SUM(pmore360) AS '360+', SUM(total) AS 'Grand Total' " +
                     "FROM( " +
                     "SELECT " +
                     "SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 30 Then s.purchaseRate Else 0 END) AS pless30, " +
                     "SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 30 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 60 Then s.purchaseRate Else 0 END) AS p31to60, " +
                     "SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 60 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 90 Then s.purchaseRate Else 0 END) AS p61to90, " +
                     "SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 90 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 120 Then s.purchaseRate Else 0 END) AS p91to120, " +
                     "SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 120 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 150 Then s.purchaseRate Else 0 END) AS p121to150, " +
                     "SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 150 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 180 Then s.purchaseRate Else 0 END) AS p151to180, " +
                     "SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 180 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 240 Then s.purchaseRate Else 0 END) AS p181to240, " +
                     "SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 240 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 300 Then s.purchaseRate Else 0 END) AS p241to300, " +
                     "SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 300 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 360 Then s.purchaseRate Else 0 END) AS p301to360, " +
                     "SUM(CASE WHEN((DATEDIFF(DAY, s.SystemDate, GETDATE())) > 360) Then s.purchaseRate Else 0 END) AS pmore360, " +
                     "SUM(s.purchaseRate) AS total, " +
                     "c.c1name, s.purchaseRate, s.SystemDate " +
                     "FROM StockUpInward s " +
                     "INNER JOIN ItemStyle i ON i.styleid = s.styleid INNER JOIN COLUMN1 c ON c.col1id = i.Col1 WHERE s.physicalId = 1 GROUP BY s.SystemDate, c.c1name, s.purchaseRate) a GROUP BY c1name ORDER BY c1name";
            }

            else if (btnType.Equals("Sales With Margin"))
            {
                command.CommandText = "SELECT c1name AS 'Vendor', " +
                      "SUM(pless30) AS 'Purchase 0-30' ,SUM(sless30) AS 'Sales 0-30', ROUND(((SUM(sless30) - SUM(pless30)) / nullif(SUM(sless30), 0)) * 100, 2) AS 'Margin 0-30', " +
                      "SUM(p31to60) AS 'Purchase 31-60',SUM(s31to60) AS 'Sales 31-60',ROUND(((SUM(s31to60) - SUM(p31to60)) / nullif(SUM(s31to60), 0)) * 100, 2) AS 'Margin 31-60', " +
                      "SUM(p61to90) AS 'Purchase 61-90', SUM(s61to90) AS 'Sales 61-90',ROUND(((SUM(s61to90) - SUM(p61to90)) / nullif(SUM(s61to90), 0)) * 100, 2) AS 'Margin 61-90', " +
                      "SUM(p91to120) AS 'Purchase 91-120',  SUM(s91to120) AS 'Sales 91-120',ROUND(((SUM(s91to120) - SUM(p91to120)) / nullif(SUM(s91to120), 0)) * 100, 2) AS 'Margin 91-120', " +
                      "SUM(p121to150) AS 'Purchase 121-150', SUM(s121to150) AS 'Sales 121-150',ROUND(((SUM(s121to150) - SUM(p121to150)) / nullif(SUM(s121to150), 0)) * 100, 2) AS 'Margin 121-150', " +
                      "SUM(p151to180) AS 'Purchase 151-180', SUM(s151to180) AS 'Sales 151-180',ROUND(((SUM(s151to180) - SUM(p151to180)) / nullif(SUM(s151to180), 0)) * 100, 2) AS 'Margin 151-180', " +
                      "SUM(p181to240) AS 'Purchase 181-240', SUM(s181to240) AS 'Sales 181-240',ROUND(((SUM(s181to240) - SUM(p181to240)) / nullif(SUM(s181to240), 0)) * 100, 2) AS 'Margin 181-240', " +
                      "SUM(p241to300) AS 'Purchase 241-300', SUM(s241to300) AS 'Sales 241-300',ROUND(((SUM(s241to300) - SUM(p241to300)) / nullif(SUM(s241to300), 0)) * 100, 2) AS 'Margin 240-300', " +
                      "SUM(p301to360) AS 'Purchase 301-360',  SUM(s301to360) AS 'Sales 301-360',ROUND(((SUM(s301to360) - SUM(p301to360)) / nullif(SUM(s301to360), 0)) * 100, 2) AS 'Margin 300-360', " +
                      "SUM(pmore360) AS 'Purchase 360+', SUM(smore360) AS 'Sales 360+',ROUND(((SUM(smore360) - SUM(pmore360)) / nullif(SUM(smore360), 0)) * 100, 2) AS 'Margin 360+', " +
                      "SUM(totalpurchase) AS 'Total Purchase', SUM(totalsales) AS 'Total Sales',ROUND(((SUM(totalsales) - SUM(totalpurchase)) / nullif(SUM(totalsales), 0)) * 100, 2) AS 'Total Margin' " +
                      "FROM( " +
                      "SELECT " +
                      "SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 30 Then a.purchaseRate Else 0 END) AS pless30, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 30 Then s.sellingprice Else 0 END) AS sless30, " +
                      "SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 30 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 60 Then a.purchaseRate Else 0 END) AS p31to60, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 30 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 60 THEN s.sellingprice Else 0 END) AS s31to60, " +
                      "SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 60 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 90 Then a.purchaseRate Else 0 END) AS p61to90, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 60 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 90 Then s.sellingprice Else 0 END) AS s61to90, " +
                      "SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 90 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 120 Then a.purchaseRate Else 0 END) AS p91to120, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 90 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 120 Then s.sellingprice Else 0 END) AS s91to120, " +
                      "SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 120 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 150 Then a.purchaseRate Else 0 END) AS p121to150, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 120 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 150 Then s.sellingprice Else 0 END) AS s121to150, " +
                      "SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 150 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 180 Then a.purchaseRate Else 0 END) AS p151to180, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 150 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 180 Then s.sellingprice Else 0 END) AS s151to180, " +
                      "SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 180 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 240 Then a.purchaseRate Else 0 END) AS p181to240, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 180 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 240 Then s.sellingprice Else 0 END) AS s181to240, " +
                      "SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 240 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 300 Then a.purchaseRate Else 0 END) AS p241to300, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 240 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 300 Then s.sellingprice Else 0 END) AS s241to300, " +
                      "SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 300 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 360 Then a.purchaseRate Else 0 END) AS p301to360, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 300 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 360 Then s.sellingprice Else 0 END) AS s301to360, " +
                      "SUM(CASE WHEN((DATEDIFF(DAY, a.SystemDate, GETDATE())) > 360) Then a.purchaseRate Else 0 END) AS pmore360, SUM(CASE WHEN((DATEDIFF(DAY, a.SystemDate, GETDATE())) > 360) Then s.sellingprice Else 0 END) AS smore360, " +
                      "SUM(a.purchaseRate) AS totalpurchase, SUM(s.sellingprice) AS totalsales, " +
                      "c.c1name, a.purchaseRate, a.SystemDate " +
                      "FROM ArchiveStockUpInward a " +
                      "INNER JOIN ItemStyle i ON i.styleid = a.styleid INNER JOIN COLUMN1 c ON c.col1id = i.Col1 inner join salesrecord s ON s.archiveid = a.ArchiveStockupId WHERE s.returntimestamp IS NULL AND convert(date, s.salesDateTime) BETWEEN convert(date, @frmDate) AND convert(date, @toDate) GROUP BY a.SystemDate, c.c1name, a.purchaseRate) b GROUP BY c1name ORDER BY c1name ";
            }

            else if (btnType.Equals("Sales For Warehouse"))
            {
                command.CommandText = "SELECT c1name AS 'Vendor', " +
                      "SUM(pless30) AS 'Purchase 0-30' ,SUM(sless30) AS 'Sales 0-30', ROUND(((SUM(sless30) - SUM(pless30)) / nullif(SUM(sless30), 0)) * 100, 2) AS 'Margin 0-30', " +
                      "SUM(p31to60) AS 'Purchase 31-60',SUM(s31to60) AS 'Sales 31-60',ROUND(((SUM(s31to60) - SUM(p31to60)) / nullif(SUM(s31to60), 0)) * 100, 2) AS 'Margin 31-60', " +
                      "SUM(p61to90) AS 'Purchase 61-90', SUM(s61to90) AS 'Sales 61-90',ROUND(((SUM(s61to90) - SUM(p61to90)) / nullif(SUM(s61to90), 0)) * 100, 2) AS 'Margin 61-90', " +
                      "SUM(p91to120) AS 'Purchase 91-120',  SUM(s91to120) AS 'Sales 91-120',ROUND(((SUM(s91to120) - SUM(p91to120)) / nullif(SUM(s91to120), 0)) * 100, 2) AS 'Margin 91-120', " +
                      "SUM(p121to150) AS 'Purchase 121-150', SUM(s121to150) AS 'Sales 121-150',ROUND(((SUM(s121to150) - SUM(p121to150)) / nullif(SUM(s121to150), 0)) * 100, 2) AS 'Margin 121-150', " +
                      "SUM(p151to180) AS 'Purchase 151-180', SUM(s151to180) AS 'Sales 151-180',ROUND(((SUM(s151to180) - SUM(p151to180)) / nullif(SUM(s151to180), 0)) * 100, 2) AS 'Margin 151-180', " +
                      "SUM(p181to240) AS 'Purchase 181-240', SUM(s181to240) AS 'Sales 181-240',ROUND(((SUM(s181to240) - SUM(p181to240)) / nullif(SUM(s181to240), 0)) * 100, 2) AS 'Margin 181-240', " +
                      "SUM(p241to300) AS 'Purchase 241-300', SUM(s241to300) AS 'Sales 241-300',ROUND(((SUM(s241to300) - SUM(p241to300)) / nullif(SUM(s241to300), 0)) * 100, 2) AS 'Margin 240-300', " +
                      "SUM(p301to360) AS 'Purchase 301-360',  SUM(s301to360) AS 'Sales 301-360',ROUND(((SUM(s301to360) - SUM(p301to360)) / nullif(SUM(s301to360), 0)) * 100, 2) AS 'Margin 300-360', " +
                      "SUM(pmore360) AS 'Purchase 360+', SUM(smore360) AS 'Sales 360+',ROUND(((SUM(smore360) - SUM(pmore360)) / nullif(SUM(smore360), 0)) * 100, 2) AS 'Margin 360+', " +
                      "SUM(totalpurchase) AS 'Total Purchase', SUM(totalsales) AS 'Total Sales',ROUND(((SUM(totalsales) - SUM(totalpurchase)) / nullif(SUM(totalsales), 0)) * 100, 2) AS 'Total Margin' " +
                      "FROM( " +
                      "SELECT " +
                      "SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 30 Then a.purchaseRate Else 0 END) AS pless30, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 30 Then s.sellingprice Else 0 END) AS sless30, " +
                      "SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 30 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 60 Then a.purchaseRate Else 0 END) AS p31to60, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 30 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 60 THEN s.sellingprice Else 0 END) AS s31to60, " +
                      "SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 60 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 90 Then a.purchaseRate Else 0 END) AS p61to90, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 60 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 90 Then s.sellingprice Else 0 END) AS s61to90, " +
                      "SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 90 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 120 Then a.purchaseRate Else 0 END) AS p91to120, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 90 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 120 Then s.sellingprice Else 0 END) AS s91to120, " +
                      "SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 120 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 150 Then a.purchaseRate Else 0 END) AS p121to150, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 120 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 150 Then s.sellingprice Else 0 END) AS s121to150, " +
                      "SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 150 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 180 Then a.purchaseRate Else 0 END) AS p151to180, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 150 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 180 Then s.sellingprice Else 0 END) AS s151to180, " +
                      "SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 180 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 240 Then a.purchaseRate Else 0 END) AS p181to240, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 180 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 240 Then s.sellingprice Else 0 END) AS s181to240, " +
                      "SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 240 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 300 Then a.purchaseRate Else 0 END) AS p241to300, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 240 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 300 Then s.sellingprice Else 0 END) AS s241to300, " +
                      "SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 300 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 360 Then a.purchaseRate Else 0 END) AS p301to360, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 300 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 360 Then s.sellingprice Else 0 END) AS s301to360, " +
                      "SUM(CASE WHEN((DATEDIFF(DAY, a.SystemDate, GETDATE())) > 360) Then a.purchaseRate Else 0 END) AS pmore360, SUM(CASE WHEN((DATEDIFF(DAY, a.SystemDate, GETDATE())) > 360) Then s.sellingprice Else 0 END) AS smore360, " +
                      "SUM(a.purchaseRate) AS totalpurchase, SUM(s.sellingprice) AS totalsales, " +
                      "c.c1name, a.purchaseRate, a.SystemDate " +
                      "FROM ArchiveStockUpInward a " +
                      "INNER JOIN ItemStyle i ON i.styleid = a.styleid INNER JOIN COLUMN1 c ON c.col1id = i.Col1 inner join salesrecord s ON s.archiveid = a.ArchiveStockupId inner join login l ON l.userid = a.userid WHERE l.physicalLocation = 1 and s.returntimestamp IS NULL AND convert(date, s.salesDateTime) BETWEEN convert(date, @frmDate) AND convert(date, @toDate) GROUP BY a.SystemDate, c.c1name, a.purchaseRate) b GROUP BY c1name ORDER BY c1name";
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
                       "SUM(p241to300) AS 'Purchase 241-300', SUM(s241to300) AS 'Sales 241-300',ROUND(((SUM(s241to300) - SUM(p241to300)) / nullif(SUM(s241to300), 0)) * 100, 2) AS 'Margin 240-300', " +
                       "SUM(p301to360) AS 'Purchase 301-360',  SUM(s301to360) AS 'Sales 301-360',ROUND(((SUM(s301to360) - SUM(p301to360)) / nullif(SUM(s301to360), 0)) * 100, 2) AS 'Margin 300-360', " +
                       "SUM(pmore360) AS 'Purchase 360+', SUM(smore360) AS 'Sales 360+',ROUND(((SUM(smore360) - SUM(pmore360)) / nullif(SUM(smore360), 0)) * 100, 2) AS 'Margin 360+', " +
                       "SUM(totalpurchase) AS 'Total Purchase', SUM(totalsales) AS 'Total Sales',ROUND(((SUM(totalsales) - SUM(totalpurchase)) / nullif(SUM(totalsales), 0)) * 100, 2) AS 'Total Margin' " +
                       "FROM( " +
                       "SELECT " +
                       "SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 30 Then a.purchaseRate Else 0 END) AS pless30, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 30 Then s.sellingprice Else 0 END) AS sless30, " +
                       "SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 30 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 60 Then a.purchaseRate Else 0 END) AS p31to60, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 30 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 60 THEN s.sellingprice Else 0 END) AS s31to60, " +
                       "SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 60 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 90 Then a.purchaseRate Else 0 END) AS p61to90, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 60 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 90 Then s.sellingprice Else 0 END) AS s61to90, " +
                       "SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 90 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 120 Then a.purchaseRate Else 0 END) AS p91to120, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 90 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 120 Then s.sellingprice Else 0 END) AS s91to120, " +
                       "SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 120 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 150 Then a.purchaseRate Else 0 END) AS p121to150, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 120 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 150 Then s.sellingprice Else 0 END) AS s121to150, " +
                       "SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 150 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 180 Then a.purchaseRate Else 0 END) AS p151to180, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 150 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 180 Then s.sellingprice Else 0 END) AS s151to180, " +
                       "SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 180 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 240 Then a.purchaseRate Else 0 END) AS p181to240, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 180 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 240 Then s.sellingprice Else 0 END) AS s181to240, " +
                       "SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 240 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 300 Then a.purchaseRate Else 0 END) AS p241to300, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 240 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 300 Then s.sellingprice Else 0 END) AS s241to300, " +
                       "SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 300 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 360 Then a.purchaseRate Else 0 END) AS p301to360, SUM(CASE When(DATEDIFF(DAY, a.SystemDate, GETDATE())) > 300 AND(DATEDIFF(DAY, a.SystemDate, GETDATE())) <= 360 Then s.sellingprice Else 0 END) AS s301to360, " +
                       "SUM(CASE WHEN((DATEDIFF(DAY, a.SystemDate, GETDATE())) > 360) Then a.purchaseRate Else 0 END) AS pmore360, SUM(CASE WHEN((DATEDIFF(DAY, a.SystemDate, GETDATE())) > 360) Then s.sellingprice Else 0 END) AS smore360, " +
                       "SUM(a.purchaseRate) AS totalpurchase, SUM(s.sellingprice) AS totalsales, " +
                       "c.c1name, a.purchaseRate, a.SystemDate " +
                       "FROM ArchiveStockUpInward a " +
                       "INNER JOIN ItemStyle i ON i.styleid = a.styleid INNER JOIN COLUMN1 c ON c.col1id = i.Col1 inner join salesrecord s ON s.archiveid = a.ArchiveStockupId inner join login l ON l.userid = a.userid WHERE l.physicalLocation = 3 s.returntimestamp IS NULL AND convert(date, s.salesDateTime) BETWEEN convert(date, @frmDate) AND convert(date, @toDate) GROUP BY a.SystemDate, c.c1name, a.purchaseRate) b GROUP BY c1name ORDER BY c1name";
            }

            else if (btnType.Equals("Purchase With Margin"))
            {
                command.CommandText = "SELECT c1name AS 'Vendor', " +
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
                      "SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 30 Then s.purchaseRate Else 0 END) AS pless30, SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 30 Then s.mrp Else 0 END) AS mless30, " +
                      "SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 30 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 60 Then s.purchaseRate Else 0 END) AS p31to60, SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 30 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 60 Then s.mrp Else 0 END) AS m31to60, " +
                      "SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 60 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 90 Then s.purchaseRate Else 0 END) AS p61to90, SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 60 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 90 Then s.mrp Else 0 END) AS m61to90, " +
                      "SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 90 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 120 Then s.purchaseRate Else 0 END) AS p91to120, SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 90 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 120 Then s.mrp Else 0 END) AS m91to120, " +
                      "SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 120 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 150 Then s.purchaseRate Else 0 END) AS p121to150, SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 120 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 150 Then s.mrp Else 0 END) AS m121to150, " +
                      "SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 150 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 180 Then s.purchaseRate Else 0 END) AS p151to180, SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 150 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 180 Then s.mrp Else 0 END) AS m151to180, " +
                      "SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 180 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 240 Then s.purchaseRate Else 0 END) AS p181to240, SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 180 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 240 Then s.mrp Else 0 END) AS m181to240, " +
                      "SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 240 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 300 Then s.purchaseRate Else 0 END) AS p241to300, SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 240 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 300 Then s.mrp Else 0 END) AS m241to300, " +
                      "SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 300 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 360 Then s.purchaseRate Else 0 END) AS p301to360, SUM(CASE When(DATEDIFF(DAY, s.SystemDate, GETDATE())) > 300 AND(DATEDIFF(DAY, s.SystemDate, GETDATE())) <= 360 Then s.mrp Else 0 END) AS m301to360, " +
                      "SUM(CASE WHEN((DATEDIFF(DAY, s.SystemDate, GETDATE())) > 360) Then s.purchaseRate Else 0 END) AS pmore360, SUM(CASE WHEN((DATEDIFF(DAY, s.SystemDate, GETDATE())) > 360) Then s.mrp Else 0 END) AS mmore360, " +
                      "SUM(s.purchaseRate) AS ptotal, SUM(s.mrp) AS mtotal, " +
                      "c.c1name, s.purchaseRate, s.SystemDate " +
                      "FROM StockUpInward s " +
                      "INNER JOIN ItemStyle i ON i.styleid = s.styleid INNER JOIN COLUMN1 c ON c.col1id = i.Col1 GROUP BY s.SystemDate, c.c1name, s.purchaseRate) a GROUP BY c1name ORDER BY c1name";
            }
                command.Parameters.AddWithValue("@frmDate", frmDate);
            command.Parameters.AddWithValue("@toDate", toDate);
            catTable.Load(command.ExecuteReader());

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