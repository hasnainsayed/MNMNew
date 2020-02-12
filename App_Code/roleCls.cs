using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for roleCls
/// </summary>
public class roleCls
{
    string userName = HttpContext.Current.Session["userName"].ToString();//id of logged in admin
    public roleCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public int addEditRole(string roleName, string LocationType, string PhysicalLocation, string VirtualLocation, string Vendors, string newLot
                    , string hsnCode, string courier, string usermaster, string rolemasters,string ColumnTableSetting,string StyleColumnSetting, string ItemCategory, string
                    Size, string Category2, string Category3, string Category4, string Category5, string
                    Column1, string Column2, string Column3, string Column4, string Column5, string Column6, string Column7, string Column8, string
                    Column9, string Column10, string Column11, string Column12, string Column13, string Column14, string Column15, string
                    Column16, string Column17, string Column18, string Column19, string Column20, string
                    ItemStyleSearchAdd, string addStyle, string addRFLNR, string ViewInventory, string listing, string sellingInd, string invoice, string
                    returnItem, string statusControl, string cancleBarcode, string changeMRP, string deleteWrongBarcode, string
                    pickDispatch, string pickCancel, string notListed, string skuInventory, string dumps, string
                    salesDump, string dispatchedDump, string warehouseMap, string userWork, string barcodeStatus, string
                    styleStatus, string ReportsModule, string bulkListing, string saleScan, string saleExcel, string dispatchScan, string dispatchExcel, string hdnID,string pickList, string tickets, string addTicket)
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
        transaction = connection.BeginTransaction("roles");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            int result = 0;
            command.Parameters.AddWithValue("@roleName", roleName);
            command.Parameters.AddWithValue("@LocationType", LocationType);
            command.Parameters.AddWithValue("@PhysicalLocation", PhysicalLocation);
            command.Parameters.AddWithValue("@VirtualLocation", VirtualLocation);
            command.Parameters.AddWithValue("@Vendors", Vendors);
            command.Parameters.AddWithValue("@newLot", newLot);
            command.Parameters.AddWithValue("@hsnCode", hsnCode);
            command.Parameters.AddWithValue("@courier", courier);
            command.Parameters.AddWithValue("@usermaster", usermaster);
            command.Parameters.AddWithValue("@rolemasters", rolemasters);
            command.Parameters.AddWithValue("@ItemCategory", ItemCategory);
            command.Parameters.AddWithValue("@Size", Size);
            command.Parameters.AddWithValue("@Category2", Category2);
            command.Parameters.AddWithValue("@Category3", Category3);
            command.Parameters.AddWithValue("@Category4", Category4);
            command.Parameters.AddWithValue("@Category5", Category5);
            command.Parameters.AddWithValue("@Column1", Column1);
            command.Parameters.AddWithValue("@Column2", Column2);
            command.Parameters.AddWithValue("@Column3", Column3);
            command.Parameters.AddWithValue("@Column4", Column4);
            command.Parameters.AddWithValue("@Column5", Column5);
            command.Parameters.AddWithValue("@Column6", Column6);
            command.Parameters.AddWithValue("@Column7", Column7);
            command.Parameters.AddWithValue("@Column8", Column8);
            command.Parameters.AddWithValue("@Column9", Column9);
            command.Parameters.AddWithValue("@Column10", Column10);
            command.Parameters.AddWithValue("@Column11", Column11);
            command.Parameters.AddWithValue("@Column12", Column12);
            command.Parameters.AddWithValue("@Column13", Column13);
            command.Parameters.AddWithValue("@Column14", Column14);
            command.Parameters.AddWithValue("@Column15", Column15);
            command.Parameters.AddWithValue("@Column16", Column16);
            command.Parameters.AddWithValue("@Column17", Column17);
            command.Parameters.AddWithValue("@Column18", Column18);
            command.Parameters.AddWithValue("@Column19", Column19);
            command.Parameters.AddWithValue("@Column20", Column20);
            command.Parameters.AddWithValue("@ItemStyleSearchAdd", ItemStyleSearchAdd);
            command.Parameters.AddWithValue("@addStyle", addStyle);
            command.Parameters.AddWithValue("@addRFLNR", addRFLNR);
            command.Parameters.AddWithValue("@ViewInventory", ViewInventory);
            command.Parameters.AddWithValue("@listing", listing);
            command.Parameters.AddWithValue("@sellingInd", sellingInd);
            command.Parameters.AddWithValue("@invoice", invoice);
            command.Parameters.AddWithValue("@returnItem", returnItem);
            command.Parameters.AddWithValue("@statusControl", statusControl);
            command.Parameters.AddWithValue("@cancleBarcode", cancleBarcode);
            command.Parameters.AddWithValue("@changeMRP", changeMRP);
            command.Parameters.AddWithValue("@deleteWrongBarcode", deleteWrongBarcode);
            command.Parameters.AddWithValue("@pickDispatch", pickDispatch);
            command.Parameters.AddWithValue("@pickCancel", pickCancel);
            command.Parameters.AddWithValue("@notListed", notListed);
            command.Parameters.AddWithValue("@skuInventory", skuInventory);
            command.Parameters.AddWithValue("@dumps", dumps);
            command.Parameters.AddWithValue("@salesDump", salesDump);
            command.Parameters.AddWithValue("@dispatchedDump", dispatchedDump);
            command.Parameters.AddWithValue("@warehouseMap", warehouseMap);
            command.Parameters.AddWithValue("@userWork", userWork);
            command.Parameters.AddWithValue("@barcodeStatus", barcodeStatus);
            command.Parameters.AddWithValue("@styleStatus", styleStatus);
            command.Parameters.AddWithValue("@ReportsModule", ReportsModule);
            command.Parameters.AddWithValue("@bulkListing", bulkListing);
            command.Parameters.AddWithValue("@saleScan", saleScan);
            command.Parameters.AddWithValue("@saleExcel", saleExcel);
            command.Parameters.AddWithValue("@dispatchScan", dispatchScan);
            command.Parameters.AddWithValue("@dispatchExcel", dispatchExcel);
            command.Parameters.AddWithValue("@ColumnTableSetting", ColumnTableSetting);
            command.Parameters.AddWithValue("@StyleColumnSetting", StyleColumnSetting);
            string logs = ","+userName + ":" + DateTime.Now;
            command.Parameters.AddWithValue("@logs", logs);
            command.Parameters.AddWithValue("@pickList", pickList);
            command.Parameters.AddWithValue("@tickets", tickets);
            command.Parameters.AddWithValue("@addTicket", addTicket);
            if (hdnID.Equals("0"))
            {
                command.CommandText = "INSERT INTO roles (roleName,LocationType,PhysicalLocation,VirtualLocation,Vendors,newLot,hsnCode,courier,usermaster,rolemasters,ItemCategory,Size,Category2,Category3,Category4,Category5," +
                    "Column1,Column2,Column3,Column4,Column5,Column6,Column7,Column8," +
                    "Column9,Column10,Column11,Column12,Column13,Column14,Column15," +
                    "Column16,Column17,Column18,Column19,Column20," +
                    "ItemStyleSearchAdd,addStyle,addRFLNR,ViewInventory,listing,sellingInd,invoice," +
                    "returnItem,statusControl,cancleBarcode,changeMRP,deleteWrongBarcode," +
                    "pickDispatch,pickCancel,notListed,skuInventory,dumps," +
                    "salesDump,dispatchedDump,warehouseMap,userWork,barcodeStatus," +
                    "styleStatus,ReportsModule,bulkListing,saleScan,saleExcel,dispatchScan,dispatchExcel,ColumnTableSetting,StyleColumnSetting,logs,pickList,tickets,addTicket) values (@roleName,@LocationType,@PhysicalLocation,@VirtualLocation,@Vendors,@newLot,@hsnCode,@courier,@usermaster,@rolemasters,@ItemCategory,@Size,@Category2,@Category3,@Category4,@Category5," +
                    "@Column1,@Column2,@Column3,@Column4,@Column5,@Column6,@Column7,@Column8," +
                    "@Column9,@Column10,@Column11,@Column12,@Column13,@Column14,@Column15," +
                    "@Column16,@Column17,@Column18,@Column19,@Column20," +
                    "@ItemStyleSearchAdd,@addStyle,@addRFLNR,@ViewInventory,@listing,@sellingInd,@invoice," +
                    "@returnItem,@statusControl,@cancleBarcode,@changeMRP,@deleteWrongBarcode," +
                    "@pickDispatch,@pickCancel,@notListed,@skuInventory,@dumps," +
                    "@salesDump,@dispatchedDump,@warehouseMap,@userWork,@barcodeStatus," +
                    "@styleStatus,@ReportsModule,@bulkListing,@saleScan,@saleExcel,@dispatchScan,@dispatchExcel,@ColumnTableSetting,@StyleColumnSetting,@logs,@pickList,@tickets,@addTicket)";
                

                command.ExecuteNonQuery();
            }
            else
            {
                command.CommandText = "update roles set roleName=@roleName,LocationType=@LocationType," +
                    "PhysicalLocation=@PhysicalLocation,VirtualLocation=@VirtualLocation," +
                    "Vendors=@Vendors,newLot=@newLot,hsnCode=@hsnCode," +
                    "courier=@courier,usermaster=@usermaster,rolemasters=@rolemasters," +
                    "ItemCategory=@ItemCategory,Size=@Size,Category2=@Category2,Category3=@Category3,Category4=@Category4,Category5=@Category5," +
                    "Column1=@Column1,Column2=@Column2,Column3=@Column3,Column4=@Column4,Column5=@Column5,Column6=@Column6," +
                    "Column7=@Column7,Column8=@Column8," +
                    "Column9=@Column9,Column10=@Column10,Column11=@Column11,Column12=@Column12,Column13=@Column13,Column14=@Column14,Column15=@Column15," +
                    "Column16=@Column16,Column17=@Column17,Column18=@Column18,Column19=@Column19,Column20=@Column20," +
                    "ItemStyleSearchAdd=@ItemStyleSearchAdd,addStyle=@addStyle,addRFLNR=@addRFLNR," +
                    "ViewInventory=@ViewInventory,listing=@listing,sellingInd=@sellingInd,invoice=@invoice," +
                    "returnItem=@returnItem,statusControl=@statusControl,cancleBarcode=@cancleBarcode,changeMRP=@changeMRP," +
                    "deleteWrongBarcode=@deleteWrongBarcode," +
                    "pickDispatch=@pickDispatch,pickCancel=@pickCancel,notListed=@notListed,skuInventory=@skuInventory,dumps=@dumps," +
                    "salesDump=@salesDump,dispatchedDump=@dispatchedDump,warehouseMap=@warehouseMap,userWork=@userWork,barcodeStatus=@barcodeStatus," +
                    "styleStatus=@styleStatus, ReportsModule=@ReportsModule,bulkListing=@bulkListing,saleScan=@saleScan,saleExcel=@saleExcel," +
                    "dispatchScan=@dispatchScan,dispatchExcel=@dispatchExcel,ColumnTableSetting=@ColumnTableSetting," +
                    "StyleColumnSetting=@StyleColumnSetting,logs=logs+'" + logs + "',pickList=@pickList,tickets=@tickets,addTicket=@addTicket where roleId=@roleId";
                command.Parameters.AddWithValue("@roleId", hdnID);
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
}