using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for styleCls
/// </summary>
public class styleCls
{
    string userId = HttpContext.Current.Session["login"].ToString();//id of logged in admin
    string userName = HttpContext.Current.Session["userName"].ToString();//id of logged in admin
    private SequenceType sequenceType = SequenceType.NumericToAlpha;
    public styleCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }

   
    public DataTable getLatestDealsTable(string table)
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
        transaction = connection.BeginTransaction("ltstDealSty");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "SELECT i.StyleID,i.StyleCode,i.Title FROM "+ table + " t INNER JOIN ItemStyle i ON i.StyleID=t.styleId";
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

    public DataTable getLatestStyle()
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
        transaction = connection.BeginTransaction("latestStyle");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select top 20 * from ItemStyle order by StyleID desc";
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

    public DataTable getCatName()
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
        transaction = connection.BeginTransaction("getCatName");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select * from ItemCatSetting";
            catTable.Load(command.ExecuteReader());

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();
            
        }
        catch(Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        return catTable;
    }

    public DataTable bindItemCategory()
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
        transaction = connection.BeginTransaction("bindItemCategory");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select * from ItemCategory";
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

    public DataTable bindCategoryTwo(int catId)
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
        transaction = connection.BeginTransaction("bindCategoryTwo");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select * from Category2 where ICatID=@ICatID";
            command.Parameters.AddWithValue("@ICatID",catId);
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
    
    public DataTable bindCategoryThree(int catId)
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
        transaction = connection.BeginTransaction("bindCategoryThree");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select * from Category3 where Cat2ID=@Cat2ID";
            command.Parameters.AddWithValue("@Cat2ID", catId);
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

    public DataTable bindCategoryFour(int catId)
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
        transaction = connection.BeginTransaction("bindCategoryFour");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select * from Category4 where Cat3ID=@Cat3ID";
            command.Parameters.AddWithValue("@Cat3ID", catId);
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

    public DataTable bindCategoryFive(int catId)
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
        transaction = connection.BeginTransaction("bindCategoryFive");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select * from Category5 where Cat4ID=@Cat4ID";
            command.Parameters.AddWithValue("@Cat4ID", catId);
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

    public DataTable getDataFieldName()
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
        transaction = connection.BeginTransaction("getDataFieldName");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            //command.CommandText = "select s.StyleCSID,concat('Control',s.StyleCSID) as ColumnNo,s.SettingName,s.IsAssigned,c.mandatory,c.optinal,c.Na from StyleColumnSettings s inner join checkStycolSetting c on c.colid=s.StyleCSID ";
            command.CommandText = "select s.StyleCSID,concat('Control',s.StyleCSID) as ColumnNo,s.SettingName,s.IsAssigned from StyleColumnSettings s";
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

    public DataTable getDropDown()
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
        transaction = connection.BeginTransaction("getDropDown");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select *,concat('Col',CTSettingID) as checkValue,defaultSelection from ColTableSetting";
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

    public DataTable getTable(string table)
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
            command.CommandText = "select * from "+ table;
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

    public DataTable SearchStyle(DataTable cat, DataTable field, DataTable drp)
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
        transaction = connection.BeginTransaction("SearchStyle");
        command.Connection = connection;
        command.Transaction = transaction;
        DataTable dt = new DataTable();
        try
        {
            
            command.CommandText = "select * from ItemStyle";
            string subQuery = string.Empty;
            foreach (DataRow row in cat.Rows)
            {
                subQuery += " and "+row["Column"]+"="+ row["Search"];
            }

            foreach (DataRow row in drp.Rows)
            {
                subQuery += " and " + row["Column"] + "=" + row["Search"];
            }

            foreach (DataRow row in field.Rows)
            {
                subQuery += " and " + row["Column"] + " LIKE '%" + row["Search"]+ "%'";
            }
            if (!subQuery.Equals("")) {
                command.CommandText += " where " + subQuery.Substring(4);
            }

            dt.Load(command.ExecuteReader());
            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();
            return dt;
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
                return dt;
            }
            catch (Exception ex2)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                RecordExceptionCls rex = new RecordExceptionCls();
                rex.recordException(ex2);
                return dt;

            }
        }
    }

    public int addRflNR(string size, string lot, string StyleID , string status,string mrp,string oldBarcode,
        string mfgDate, string piecePerPacket, string totalBarcode, string isSample,string lotPieces,string purchaseRate)
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
        transaction = connection.BeginTransaction("addRflNR");
        command.Connection = connection;
        command.Transaction = transaction;
        DataTable dt = new DataTable();
        try
        {
            // get size 
            command.CommandText = "select Size1,SizeCode from Size where SizeID=@getSize";
            command.Parameters.AddWithValue("@getSize", size);
            DataTable sizeDt = new DataTable();
            sizeDt.Load(command.ExecuteReader());

            // get style 
            command.CommandText = "select StyleCode from ItemStyle where StyleID=@getStyleID";
            command.Parameters.AddWithValue("@getStyleID", StyleID);
            DataTable styleDt = new DataTable();
            styleDt.Load(command.ExecuteReader());

            // get available pieces
            DataTable pieceCountDt = getPieceCountByLot(lot);
            int pieceCount = 0;
            if (!pieceCountDt.Rows.Count.Equals(0))
            {
                pieceCount = Convert.ToInt32(pieceCountDt.Rows[0]["pieces"]);
            }
            int res = 1;
            int pieceAvailable = Convert.ToInt32(lotPieces) - pieceCount;
            if(!(pieceAvailable<(Convert.ToInt32(totalBarcode)* Convert.ToInt32(piecePerPacket))))
            {
                // loop through all barcodes
                for (int i = 0; i < Convert.ToInt32(totalBarcode); i++)
                {
                    string isSampleEntry = "0";
                    if (i.Equals(0) && isSample.Equals("1")) // first entry mark as isSample Yes
                    {
                        isSampleEntry = "1";
                    }

                    DataTable stockUp = new DataTable();

                    command.CommandText = "select Top 1 LastBarcode from " +
                    "(select LastBarcode, StockupID from StockUpInward where StyleID = @StyleID and SizeID = @sizeID1 " +
                    "union all " +
                    "select LastBarcode, StockupID from ArchiveStockUpInward where StyleID = @StyleIDArch and SizeID = @sizeID1Arch  ) a  " +
                    "order by StockupID desc";
                    command.Parameters.AddWithValue("@StyleID", StyleID);
                    command.Parameters.AddWithValue("@sizeID1", size);
                    command.Parameters.AddWithValue("@StyleIDArch", StyleID);
                    command.Parameters.AddWithValue("@sizeID1Arch", size);

                    stockUp.Load(command.ExecuteReader());
                    string LastBarcode = "000";
                    if (!stockUp.Rows.Count.Equals(0))
                    {
                        AlphaNumeric.RequiredLength = Convert.ToInt32("3");  //000 - no. of digits needed
                        LastBarcode = AlphaNumeric.NextKeyCode(stockUp.Rows[0]["LastBarcode"].ToString(), sequenceType);

                    }

                    string BarcodeNo = styleDt.Rows[0]["StyleCode"].ToString() + "/" + sizeDt.Rows[0]["Size1"].ToString() + "/" + LastBarcode;

                    command.CommandText = "select BarcodeNo from StockUpInward where BarcodeNo = @sBarcodeNo union all select BarcodeNo from ArchiveStockUpInward where BarcodeNo = @aBarcodeNo";
                    command.Parameters.AddWithValue("@sBarcodeNo", BarcodeNo);
                    command.Parameters.AddWithValue("@aBarcodeNo", BarcodeNo);
                    DataTable barcodeCheck = new DataTable();
                    barcodeCheck.Load(command.ExecuteReader());

                    if (barcodeCheck.Rows.Count.Equals(0))
                    {
                        command.CommandText = "insert into StockUpInward (StyleID,BagID,SizeID,LastBarcode,Status,mrp,userId,BarcodeNo,oldBarcode,initialStatus,mfgDate,piecePerPacket,isSample,purchaseRate) " +
                        " values (@StyleID1,@BagID,@SizeID,@LastBarcode,@Status,@mrp,@userId,@BarcodeNo,@oldBarcode,@initialStatus,@mfgDate,@piecePerPacket,@isSample,@purchaseRate)";
                        command.Parameters.AddWithValue("@StyleID1", StyleID);
                        command.Parameters.AddWithValue("@BagID", lot);
                        command.Parameters.AddWithValue("@SizeID", size);
                        command.Parameters.AddWithValue("@LastBarcode", LastBarcode);
                        command.Parameters.AddWithValue("@Status", status);
                        command.Parameters.AddWithValue("@mrp", Convert.ToDecimal(mrp)* Convert.ToDecimal(piecePerPacket));
                        command.Parameters.AddWithValue("@userId", userId);
                        command.Parameters.AddWithValue("@BarcodeNo", BarcodeNo);
                        command.Parameters.AddWithValue("@oldBarcode", oldBarcode);
                        command.Parameters.AddWithValue("@initialStatus", status);
                        command.Parameters.AddWithValue("@mfgDate", Convert.ToDateTime(mfgDate).ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@piecePerPacket", piecePerPacket);
                        command.Parameters.AddWithValue("@isSample", isSampleEntry);
                        command.Parameters.AddWithValue("@purchaseRate", Convert.ToDecimal(purchaseRate) * Convert.ToDecimal(piecePerPacket)); 
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                        /*transaction.Commit();
                        command.Parameters.Clear();
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                        return 0;*/

                        
                       
                    }
                    else
                    {
                        /*transaction.Rollback();
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                        return -1;*/

                        res = 0;
                        break;

                    }
                    command.Parameters.Clear();

                }
               
            }
            else
            {
                /*transaction.Rollback();
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                return -1;*/
                res = 0;
               
            }


            DataTable pieceCountDt1 = new DataTable();
            command.CommandText = "SELECT ISNULL(SUM(counts),0) AS pieces FROM (SELECT ISNULL(SUM(s.piecePerPacket),0) AS counts FROM StockUpInward s WHERE s.BagID=@BagID11 " +
                "UNION ALL " +
                "SELECT ISNULL(SUM(a.piecePerPacket),0) AS counts FROM ArchiveStockUpInward a WHERE a.BagID = @BagID21) t";
            command.Parameters.AddWithValue("@BagID11", lot);
            command.Parameters.AddWithValue("@BagID21", lot);
            pieceCountDt1.Load(command.ExecuteReader());
            int pieceCount1 = 0;
            if (!pieceCountDt1.Rows.Count.Equals(0))
            {
                pieceCount1 = Convert.ToInt32(pieceCountDt1.Rows[0]["pieces"]);
            }
            if (pieceCount1 > Convert.ToInt32(lotPieces))
            {
                res = 0;
            }

            if (res.Equals(1)) // commit transaction
            {                
                transaction.Commit();
                command.Parameters.Clear();
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                return 0;
            }
            else
            {
                transaction.Rollback();
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                return -1;
            }
            

            
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

    public int addStyle(DataTable cat, DataTable field, DataTable drp , string title,string mrp,string image1, string image2, string image3, string image4, string image5, string image6)
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
        transaction = connection.BeginTransaction("addStyle");
        command.Connection = connection;
        command.Transaction = transaction;
        DataTable dt = new DataTable();
        try
        {
            DataTable stockUp = new DataTable();
            command.CommandText = "select Top 1 StyleCode from ItemStyle order by StyleID desc";
            stockUp.Load(command.ExecuteReader());
            string LastBarcode = "0000";
            if (!stockUp.Rows.Count.Equals(0))
            {
                AlphaNumeric.RequiredLength = Convert.ToInt32("4");  //000 - no. of digits needed
                LastBarcode = AlphaNumeric.NextKeyCode(stockUp.Rows[0]["StyleCode"].ToString(), sequenceType);

            }

            string catCol = string.Empty;
            string catColPara = string.Empty;
            string catColParaComm = string.Empty;
            string catColValue = string.Empty;
            foreach (DataRow row in cat.Rows)
            {
                catCol += "," + row["Column"];
                catColPara += " ,@" + row["Column"];
                catColValue += " ,'" + row["Search"]+"'";
                //catColSearch += " ,@" + row["Search"];
                catColParaComm += "command.Parameters.AddWithValue(@" + row["Column"] + "," + row["Search"] + ')' + ';';
                
            }

            string drpCol = string.Empty;
            string drpColPara = string.Empty;
            string drpColParaComm = string.Empty;
            string drpColValue = string.Empty;
            foreach (DataRow row in drp.Rows)
            {
                drpCol += "," + row["Column"];
                drpColPara += " ,@" + row["Column"];
                drpColValue += " ,'" + row["Search"] + "'";
                drpColParaComm += "command.Parameters.AddWithValue(@" + row["Column"] + "," + row["Search"] + ')' + ';';
                //catColParaComm += command.Parameters.AddWithValue("@"+ row["Column"], row["Search"])+";";
            }

            string fieldCol = string.Empty;
            string fieldColPara = string.Empty;
            string fieldColParaComm = string.Empty;
            string fieldColValue = string.Empty;
            foreach (DataRow row in field.Rows)
            {
                fieldCol += "," + row["Column"];
                fieldColPara += " ,@" + row["Column"];
                fieldColValue += " ,'" + row["Search"] + "'";
                fieldColParaComm += "command.Parameters.AddWithValue(@" + row["Column"] + "," + row["Search"] + ')' + ';';
                //catColParaComm += command.Parameters.AddWithValue("@"+ row["Column"], row["Search"])+";";
            }

            string columnss = string.Empty;
            columnss += "title,mrp,StyleCode,LoginID,image1,image2,image3,image4,image5,image6,logs";
            columnss += catCol + drpCol + fieldCol;
            
            string valuess = string.Empty;
            string logs = ",";
            if (mrp.Equals(""))
            {
                mrp = "0";
            }
            valuess += "'"+title+"','"+mrp+"','"+ LastBarcode+ "','"+userId+ "','" + image1 + "','" + image2 + "','" + image3 + "','" + image4 + "','" + image5 + "','" + image6 + "','" + logs + "'";
            valuess += catColValue + drpColValue + fieldColValue;

            /*if (!columnss.Equals(""))
            {
                columnss = columnss.Substring(2);
            }*/
            
            /*if (!valuess.Equals(""))
            {
                valuess = valuess.Substring(2);
            }
            */

            command.CommandText = "insert into ItemStyle (" + columnss + ") values (" + valuess + ")";
            /*catColParaComm;
            drpColParaComm;
            fieldColParaComm;*/

            command.ExecuteNonQuery();


            //dt.Load(command.ExecuteReader());
            transaction.Commit();
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

    public DataTable getPieceCountByLot(string lotId)
    {
        DataTable stock = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("styleId");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "SELECT ISNULL(SUM(counts),0) AS pieces FROM (SELECT ISNULL(SUM(s.piecePerPacket),0) AS counts FROM StockUpInward s WHERE s.BagID=@BagID1 " +
                "UNION ALL " +
                "SELECT ISNULL(SUM(a.piecePerPacket),0) AS counts FROM ArchiveStockUpInward a WHERE a.BagID = @BagID2) t";
            command.Parameters.AddWithValue("@BagID1", lotId);
            command.Parameters.AddWithValue("@BagID2", lotId);

            stock.Load(command.ExecuteReader());


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
        return stock;
    }

    public int editStyle(DataTable cat, DataTable field, DataTable drp, string title, string mrp,string StyleID,string image1, string image2, string image3, string image4, string image5, string image6,DataTable imageDel)
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
        transaction = connection.BeginTransaction("addStyle");
        command.Connection = connection;
        command.Transaction = transaction;
        DataTable dt = new DataTable();
        try
        {
            


            string catCol = string.Empty;
            
            foreach (DataRow row in cat.Rows)
            {
                catCol += "," + row["Column"]+ "='" + row["Search"] + "'";

            }

            string drpCol = string.Empty;
            
            foreach (DataRow row in drp.Rows)
            {
                drpCol += "," + row["Column"] + "='" + row["Search"] + "'";

            }

            string fieldCol = string.Empty;
            
            foreach (DataRow row in field.Rows)
            {
                fieldCol += "," + row["Column"] + "='" + row["Search"] + "'";

            }

            

            string columnss = string.Empty;
            string logs = userName + "#" +DateTime.Now+",";
            columnss += "title='" + title + "',mrp='" + mrp + "',logs=logs+'" + logs + "'";
            if (!image1.Equals(""))
            {
                columnss += ",image1='" + image1 + "'";
            }
            if (!image2.Equals(""))
            {
                columnss += ",image2='" + image2 + "'";
            }
            if (!image3.Equals(""))
            {
                columnss += ",image3='" + image3 + "'";
            }
            if (!image4.Equals(""))
            {
                columnss += ",image4='" + image4 + "'";
            }
            if (!image5.Equals(""))
            {
                columnss += ",image5='" + image5 + "'";
            }
            if (!image6.Equals(""))
            {
                columnss += ",image6='" + image6 + "'";
            }
            columnss += catCol + drpCol + fieldCol;
            columnss += " where StyleID='" + StyleID + "'";
            string valuess = string.Empty;
            if (mrp.Equals(""))
            {
                mrp = "0";
            }
            
            
            command.CommandText = "update ItemStyle set " + columnss;
            command.ExecuteNonQuery();

            // removing image... Added Later
            string newcolumnss = string.Empty;
            string nullImage = string.Empty;
            foreach (DataRow row in imageDel.Rows)
            {
                newcolumnss += ","+row["dbImageCol"] + "='"+ nullImage+"'";

            }
            if(!newcolumnss.Equals(""))
            {
                newcolumnss += " where StyleID='" + StyleID + "'";
                command.CommandText = "update ItemStyle set " + newcolumnss.Substring(1);
                command.ExecuteNonQuery();
            }
            

            /*catColParaComm;
            drpColParaComm;
            fieldColParaComm;*/

            


            //dt.Load(command.ExecuteReader());
            transaction.Commit();
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

    public int updateImages(DataTable dt)
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
        transaction = connection.BeginTransaction("uImg");
        command.Connection = connection;
        command.Transaction = transaction;
        
        try
        {

            string columnss = string.Empty;
            string logs = dt.Rows[0]["logDet"].ToString();
            columnss += "logs=logs+'" + logs + "'";
            if (!dt.Rows[0]["image1"].ToString().Equals(""))
            {
                columnss += ",image1='" + dt.Rows[0]["image1"].ToString() + "'";
            }
            if (!dt.Rows[0]["image2"].ToString().Equals(""))
            {
                columnss += ",image2='" + dt.Rows[0]["image2"].ToString() + "'";
            }
            if (!dt.Rows[0]["image3"].ToString().Equals(""))
            {
                columnss += ",image3='" + dt.Rows[0]["image3"].ToString() + "'";
            }
            if (!dt.Rows[0]["image4"].ToString().Equals(""))
            {
                columnss += ",image4='" + dt.Rows[0]["image4"].ToString() + "'";
            }
            if (!dt.Rows[0]["image5"].ToString().Equals(""))
            {
                columnss += ",image5='" + dt.Rows[0]["image5"].ToString() + "'";
            }
            if (!dt.Rows[0]["image6"].ToString().Equals(""))
            {
                columnss += ",image6='" + dt.Rows[0]["image6"].ToString() + "'";
            }
            
            columnss += " where StyleID='" + dt.Rows[0]["styleId"].ToString() + "'";
            string valuess = string.Empty;
            

            command.CommandText = "update ItemStyle set " + columnss;
            command.ExecuteNonQuery();

            // removing image... Added Later
            string newcolumnss = string.Empty;
            string nullImage = string.Empty;
            string columnss1 = string.Empty;

            columnss1 += "logs=logs+'" + logs + "'";
            if (dt.Rows[0]["image1Del"].ToString().Equals("1"))
            {
                columnss1 += ",image1='" + nullImage + "'";
            }
            if (dt.Rows[0]["image2Del"].ToString().Equals("1"))
            {
                columnss1 += ",image2='" + nullImage + "'";
            }
            if (dt.Rows[0]["image3Del"].ToString().Equals("1"))
            {
                columnss1 += ",image3='" + nullImage + "'";
            }
            if (dt.Rows[0]["image4Del"].ToString().Equals("1"))
            {
                columnss1 += ",image4='" + nullImage + "'";
            }
            if (dt.Rows[0]["image5Del"].ToString().Equals("1"))
            {
                columnss1 += ",image5='" + nullImage + "'";
            }
            if (dt.Rows[0]["image6Del"].ToString().Equals("1"))
            {
                columnss1 += ",image6='" + nullImage + "'";
            }

            //columnss1 += " where StyleID='" + dt.Rows[0]["styleId"].ToString() + "'";
            if (!columnss1.Equals(""))
            {
                columnss1 += " where StyleID='" + dt.Rows[0]["styleId"].ToString() + "'";
                command.CommandText = "update ItemStyle set " + columnss1;
                command.ExecuteNonQuery();
            }


            transaction.Commit();
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

    public string getGST(string styleId)
    {
        string gst = string.Empty;
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("getGST");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select tax from ItemCategory c inner join ItemStyle s on s.ItemCatID = c.ItemCategoryID where s.StyleID = @StyleID";
            command.Parameters.AddWithValue("@StyleID", styleId);
            gst = command.ExecuteScalar().ToString();

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        return gst;
    }

    public DataTable getStockUpInward(string styleId,string locationID)
    {
        DataTable stock = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("getStck");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            /*command.CommandText = "select si.StockupID,si.StyleID,s.StyleCode,sz.Size1,si.mrp,si.LastBarcode,concat(s.StyleCode,'/',sz.Size1,'/',si.LastBarcode) as fullBarcode,l.listprice,si.RackBarcode from " +
                " StockUpInward si inner join ItemStyle s on s.StyleID = si.StyleID" +
                " inner join Size sz on sz.SizeID = si.SizeID" +
                " inner join listchannelrecord l on l.itemid = si.StockupID" +
                " where si.status IN('RFL','NRM','NRD','NRR') and si.StyleID = @StyleID and l.saleschannelvlocid = @saleschannelvlocid";*/
            /* command.CommandText = "select si.StockupID,si.StyleID,s.StyleCode,sz.Size1,si.mrp,si.LastBarcode,concat(s.StyleCode,'/',sz.Size1,'/',si.LastBarcode) as fullBarcode,l.listprice,si.RackBarcode from " +
             " StockUpInward si inner join ItemStyle s on s.StyleID = si.StyleID" +
             " inner join Size sz on sz.SizeID = si.SizeID" +
             " inner join listchannelrecord l on l.styleId=si.StyleID and l.sizeId=si.SizeID" +
             " where si.status IN('RFL') and si.StyleID = @StyleID and l.saleschannelvlocid = @saleschannelvlocid";*/

            // made to add all status 19DEc2018
            command.CommandText = "select si.StockupID,si.StyleID,s.StyleCode,sz.Size1,si.mrp,si.LastBarcode,concat(s.StyleCode,'/',sz.Size1,'/',si.LastBarcode) as fullBarcode,'0' as listprice,si.RackBarcode,si.Status from " +
            " StockUpInward si inner join ItemStyle s on s.StyleID = si.StyleID" +
            " inner join Size sz on sz.SizeID = si.SizeID" +
            " where si.status IN('RFL','NRM','NRD','NRR') and si.StyleID = @StyleID  order by StyleID,sz.SizeID,si.StockupID"; 
             command.Parameters.AddWithValue("@StyleID", styleId);
            //command.Parameters.AddWithValue("@saleschannelvlocid", locationID);
            stock.Load(command.ExecuteReader());
           

            transaction.Commit();
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
        return stock;
    }

    

    public int addSales(DataTable field,string lblStyleID, string llblGSTPercent,
                string salesId, string custname, string address1, string address2,
                string city, string stateID , decimal sum, string virtualLocation,string paymentMode,string salesDate,string phoneNo)
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
        transaction = connection.BeginTransaction("addSales");
        command.Connection = connection;
        command.Transaction = transaction;
        
        try
        {
            int stats = 1;
            command.CommandText = "select Status from StockUpInward where StockupID=@checkStockupID";
            command.Parameters.AddWithValue("@checkStockupID", field.Rows[0]["StockupID"].ToString());
            DataTable check = new DataTable();
            check.Load(command.ExecuteReader());
            if (check.Rows[0]["Status"].Equals("SOLD"))
            {
                transaction.Rollback();
                stats= -2;
            }
            else
            {
                
                // FETCH HSN CODE
                DataTable hsnDt = new DataTable();
                command.CommandText = "SELECT h.* FROM ItemStyle s inner join ItemCategory c on c.ItemCategoryID=s.ItemCatID inner join hsnmaster h on h.hsnid=c.hsnid where s.StyleID=@stylehsn";
                command.Parameters.AddWithValue("@stylehsn", lblStyleID);
                hsnDt.Load(command.ExecuteReader());

                int invoiceid = 0;
                // insert in invoice
                command.CommandText = "insert into invoice (custname,address1,address2,city,state,total,paymentMode,salesDate,userId,phoneNo)" +
                    "values (@custname,@address1,@address2,@city,@state,@total,@paymentMode,@salesDate,@userId,@phoneNo)" +
                    "SELECT CAST(scope_identity() AS int)";
                command.Parameters.AddWithValue("@custname", custname);
                command.Parameters.AddWithValue("@address1", address1);
                command.Parameters.AddWithValue("@address2", address2);
                command.Parameters.AddWithValue("@city", city);
                command.Parameters.AddWithValue("@state", stateID);

                command.Parameters.AddWithValue("@total", sum);
                command.Parameters.AddWithValue("@paymentMode", paymentMode);
                command.Parameters.AddWithValue("@salesDate", Convert.ToDateTime(salesDate).ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@userid", userId);
                command.Parameters.AddWithValue("@phoneNo", phoneNo); 
                 invoiceid = (Int32)command.ExecuteScalar();
                int checkStatus = 0;
                // in one loop
                foreach (DataRow row in field.Rows)
                {
                    command.CommandText = "select * from StockUpInward where StockupID=@StockUpIDcheck and Status!=@Statuscheck";
                    command.Parameters.AddWithValue("@Statuscheck", "SOLD");
                    command.Parameters.AddWithValue("@StockUpIDcheck", row["StockUpId"]);
                    DataTable checkStock = new DataTable();
                    checkStock.Load(command.ExecuteReader());
                    if (checkStock.Rows.Count.Equals(0))
                    {
                        checkStatus = 1;
                    }
                    else
                    {
                        //calculate hsn
                        decimal lowhighpt = Convert.ToDecimal(hsnDt.Rows[0]["lowhighpt"]);
                        decimal ligst = Convert.ToDecimal(hsnDt.Rows[0]["ligst"]);
                        decimal higst = Convert.ToDecimal(hsnDt.Rows[0]["higst"]);
                        decimal taxableamnt = 0;
                        decimal igst = 0;
                        decimal cgst = 0;
                        decimal sgst = 0;
                        decimal gst = 0;
                        decimal amnt = ((Convert.ToDecimal(row["sp"])) - ((Convert.ToDecimal(row["sp"]) * ligst) / 100));
                        if (amnt <= lowhighpt)
                        {
                            taxableamnt = amnt;
                            igst = ((Convert.ToDecimal(row["sp"]) * ligst) / 100);
                            gst = ligst;
                        }
                        else
                        {
                            taxableamnt = ((Convert.ToDecimal(row["sp"])) - ((Convert.ToDecimal(row["sp"]) * higst) / 100));
                            igst = ((Convert.ToDecimal(row["sp"]) * higst) / 100);
                            gst = higst;
                        }
                        cgst = igst / 2;
                        sgst = igst / 2;
                        if (stateID.Equals("27"))
                        {
                            igst = 0;
                            cgst = cgst;
                            sgst = sgst;
                        }
                        else
                        {
                            igst = igst;
                            cgst = 0;
                            sgst = 0;
                        }
                        // insert in sales
                        command.CommandText = "insert into salesrecord" +
                            " (invoiceid,saleschannelvlocid,salesidgivenbyvloc," +
                            "sellingprice,status,itemid,gstpercent,taxableamount,cgstamnt," +
                            "sgstamnt,igstamnt,salesUserId) values " +
                            "(@invoiceid,@saleschannelvlocid,@salesidgivenbyvloc," +
                            "@sellingprice,@status,@itemid,@gstpercent,@taxableamount,@cgstamnt,@sgstamnt,@igstamnt,@salesUserId" +
                            ")";
                        command.Parameters.AddWithValue("@invoiceid", invoiceid);
                        command.Parameters.AddWithValue("@saleschannelvlocid", virtualLocation);
                        command.Parameters.AddWithValue("@salesidgivenbyvloc", salesId);
                        command.Parameters.AddWithValue("@sellingprice", Convert.ToDecimal(row["sp"]));
                        command.Parameters.AddWithValue("@status", "ND");
                        command.Parameters.AddWithValue("@itemid", row["StockUpId"]);
                        command.Parameters.AddWithValue("@gstpercent", gst);
                        command.Parameters.AddWithValue("@taxableamount", taxableamnt);
                        command.Parameters.AddWithValue("@igstamnt", igst);
                        command.Parameters.AddWithValue("@cgstamnt", cgst);
                        command.Parameters.AddWithValue("@sgstamnt", sgst);

                        command.Parameters.AddWithValue("@salesUserId", userId);
                        command.ExecuteNonQuery();
                        // update sold status in stockupinward
                        command.CommandText = "update StockUpInward set Status=@Status12 where StockUpID=@UStockUpID";
                        command.Parameters.AddWithValue("@Status12", "SOLD");
                        command.Parameters.AddWithValue("@UStockUpID", row["StockUpId"]);

                        command.ExecuteNonQuery();
                    }
                    
                    command.Parameters.Clear();
                   
                }

                if(checkStatus.Equals(Convert.ToInt32(0)))
                {
                    transaction.Commit();
                    stats = 1;
                }
                else
                {
                    transaction.Rollback();                    
                    stats = -3;
                }
                
            }
            

           
            if (connection.State == ConnectionState.Open)
                connection.Close();
            return stats;
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

    public DataTable getItemStyleByID(string styleId)
    {
        DataTable stock = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("styleId");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select i.*,c.ItemCategory,c2.C2Name,c3.C3Name,c4.C4Name,c5.C5Name from ItemStyle i left join ItemCategory c on c.ItemCategoryID=i.ItemCatID left join Category2 c2 on c2.Cat2ID=i.Cat2ID left join Category3 c3 on c3.Cat3ID=i.Cat3ID left join Category4 c4 on c4.Cat4ID=i.Cat4ID left join Category5 c5 on c5.Cat5ID=i.Cat5ID where i.StyleID = @StyleID";
            command.Parameters.AddWithValue("@StyleID", styleId);
           
            stock.Load(command.ExecuteReader());


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
        return stock;
    }

    public DataTable getDataFieldNameView(string styleId)
    {
        DataTable stock = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("styleId");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select s.StyleCSID,concat('Control',s.StyleCSID) as ColumnNo,s.SettingName,s.IsAssigned,"+
" (select case when ColumnNo = 'Control 1' THEN i.Control1"+
" when ColumnNo = 'Control 1' THEN i.Control1" +
" when ColumnNo = 'Control 2' THEN i.Control2" +
" when ColumnNo = 'Control 3' THEN i.Control3" +
" when ColumnNo = 'Control 4' THEN i.Control4" +
" when ColumnNo = 'Control 5' THEN i.Control5" +
" when ColumnNo = 'Control 6' THEN i.Control6" +
" when ColumnNo = 'Control 7' THEN i.Control7" +
" when ColumnNo = 'Control 8' THEN i.Control8" +
" when ColumnNo = 'Control 9' THEN i.Control9" +
" when ColumnNo = 'Control 10' THEN i.Control10" +
" when ColumnNo = 'Control 11' THEN i.Control11" +
" when ColumnNo = 'Control 12' THEN i.Control12" +
" when ColumnNo = 'Control 13' THEN i.Control13" +
" when ColumnNo = 'Control 14' THEN i.Control14" +
" when ColumnNo = 'Control 15' THEN i.Control15" +
" when ColumnNo = 'Control 16' THEN i.Control16" +
" when ColumnNo = 'Control 17' THEN i.Control17" +
" when ColumnNo = 'Control 18' THEN i.Control18" +
" when ColumnNo = 'Control 19' THEN i.Control19" +
" when ColumnNo = 'Control 20' THEN i.Control20" +
" else '' end from" +
" ItemStyle i where StyleID = @StyleID) as colVal" +
" from StyleColumnSettings s ";
            command.Parameters.AddWithValue("@StyleID", styleId);

            stock.Load(command.ExecuteReader());


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
        return stock;
    }

    public DataTable getTablewithID(string table,string column , int val)
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
            command.CommandText = "select * from " + table + " where "+ column+"=@column";
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

    public DataTable getTableColwithID(string table, string column, int val,string getColumns)
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
            command.CommandText = "select "+ getColumns + " from " + table + " where " + column + "=@column";
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

    public int addEditEAN(DataTable dt)
    {
        DataTable Table = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }
        int result = 0;
        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("addEditEAN");
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                
                if (dr["cmgfrom"].ToString().Equals("-1"))
                {
                    DataTable check = new DataTable();
                    command.CommandText = "select * from EAN e where e.styleid=@styleid and e.sizeid=@sizeid ";
                    command.Parameters.AddWithValue("@styleid", dr["styleid"]);
                    command.Parameters.AddWithValue("@sizeid", dr["sizeid"]);
                    check.Load(command.ExecuteReader());
                    command.Parameters.Clear();
                    if (check.Rows.Count.Equals(0))
                    {
                        command.CommandText = "insert into EAN (styleid,sizeid,EAN,itemcatid) values(@styleid,@sizeid,@EAN,@itemcatid)";
                    }
                }
                else
                {
                    command.CommandText = "update EAN set EAN=@EAN where id=@id";
                    command.Parameters.AddWithValue("@id", dr["cmgfrom"]);
                }
                command.Parameters.AddWithValue("@styleid", dr["styleid"]);
                command.Parameters.AddWithValue("@sizeid", dr["sizeid"]);
                command.Parameters.AddWithValue("@EAN", dr["ean"]);
                command.Parameters.AddWithValue("@itemcatid", dr["itemcatid"]);
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }

            
            
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


    public DataTable getstyleNsize(string styleId,string sizeid)
    {
        DataTable stock = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("getstyleNsize");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select id from EAN where styleid=@styleid and sizeid=@sizeid";
            command.Parameters.AddWithValue("@styleid", styleId);
            command.Parameters.AddWithValue("@sizeid", sizeid);
            stock.Load(command.ExecuteReader());


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
        return stock;
    }

    public DataTable getEAN(string styleId,string ItemCatID)
    {
        DataTable stock = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("getEAN");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            command.CommandText = "select  s.SizeID,s.Size1,e.id,e.EAN from size s inner join ItemCategory c on c.ItemCategoryID = s.ItemCategoryID inner join ItemStyle sty on sty.ItemCatID = c.ItemCategoryID left join EAN e on e.itemcatid=c.ItemCategoryID and e.styleid=sty.StyleID and e.sizeid=s.SizeID  where sty.StyleID =@StyleID and sty.ItemCatID=@ItemCatID";
            command.Parameters.AddWithValue("@StyleID", styleId);
            command.Parameters.AddWithValue("@ItemCatID", ItemCatID);
            
            stock.Load(command.ExecuteReader());


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
        return stock;
    }

    public DataTable styleListSearch(string styleCode)
    {
        DataTable stock = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("stSearch");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            
            command.CommandText = "select StyleID,Title,mrp,StyleCode from ItemStyle where styleCode like '%' + @styleCode + '%' order by StyleID desc ";
            command.Parameters.AddWithValue("@styleCode", styleCode);
            stock.Load(command.ExecuteReader());


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
        return stock;
    }

    public DataTable styleList()
    {
        DataTable stock = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("stList");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {
            
            command.CommandText = "select StyleID,Title,mrp,StyleCode from ItemStyle where image1='' order by StyleID desc ";

            stock.Load(command.ExecuteReader());


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
        return stock;
    }

    public DataTable styleDetails(string styleID)
    {
        DataTable stock = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("stDets");
        command.Connection = connection;
        command.Transaction = transaction;
        try
        {

            command.CommandText = "SELECT i.StyleID,i.Title,i.mrp,i.StyleCode,(SELECT C1Name FROM COLUMN1 WHERE Col1ID=i.Col1) AS brandName,(SELECT C2Name FROM COLUMN2 WHERE Col2ID=i.Col2) AS colorName," +
                "(SELECT C3Name FROM COLUMN3 WHERE Col3ID = i.Col3) AS genderName,(SELECT ItemCategory FROM ItemCategory WHERE ItemCategoryID = i.ItemCatID) AS categoryName," +
                "     (SELECT C2Name FROM Category2 WHERE Cat2ID = i.Cat2ID) AS subCategoryName, i.image1,i.image2,i.image3,i.image4,i.image5,i.image6 FROM ItemStyle i WHERE i.StyleID = @StyleID";
            command.Parameters.AddWithValue("@StyleID", styleID);
            stock.Load(command.ExecuteReader());


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
        return stock;
    }
}