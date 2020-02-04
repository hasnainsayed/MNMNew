using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace DataBase
{
    public class StyleCategory
    {
        public string spsql = "SP_StyleCategory";

        #region Item Category

        public int AddItemCat(string ItemCategory, string HSNCode, string Tax, string hsnid)
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
            transaction = connection.BeginTransaction("additemcat");
            command.Connection = connection;
            command.Transaction = transaction;

            try
            {

                //insert into ArchiveStockUpInward
                command.CommandText = "INSERT INTO ItemCategory(ItemCategory,HSNCode,Tax,hsnid) VALUES(@ItemCategory, @HSNCode, @Tax,@hsnid)";
             
                command.Parameters.AddWithValue("@ItemCategory", ItemCategory);
                command.Parameters.AddWithValue("@HSNCode", HSNCode);
                command.Parameters.AddWithValue("@Tax", Tax);
                command.Parameters.AddWithValue("@hsnid", hsnid);
                command.ExecuteNonQuery();

                command.Parameters.Clear();


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
            //SqlParameter[] p = new SqlParameter[4];
            //p[0] = new SqlParameter("@MODE", "ADD_ITEMCAT");
            //p[1] = new SqlParameter("@ItemCategory", ItemCategory);
            //p[2] = new SqlParameter("@HSNCode", HSNCode);
            //p[3] = new SqlParameter("@Tax", Tax);

            //return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataTable BindItemCat()
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
                command.CommandText = "select ic.*,h.hsncode hsncodenew  from ItemCategory ic inner join hsnmaster h on h.hsnid=ic.hsnid";
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
            //SqlParameter[] p = new SqlParameter[1];
            //p[0] = new SqlParameter("@MODE", "BIND_ITEMCAT");
            //return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataTable BindHsnCodes()
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
                command.CommandText = "select * from  hsnmaster";
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
            //SqlParameter[] p = new SqlParameter[1];
            //p[0] = new SqlParameter("@MODE", "BIND_ITEMCAT");
            //return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataTable GetItemCatByID(int ItemCategoryID)
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
                command.CommandText = "SELECT * FROM ItemCategory WHERE ItemCategoryID = @ItemCategoryID";
                command.Parameters.AddWithValue("@ItemCategoryID", ItemCategoryID);
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
            //SqlParameter[] p = new SqlParameter[2];
            //p[0] = new SqlParameter("@MODE", "GET_ITEMCAT_BY_ID");
            //p[1] = new SqlParameter("@ItemCategoryID", ItemCategoryID);
            //return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int UpdateItemCat(string ItemCategory, string HSNCode, string Tax, string ItemCategoryID,string hsnid)
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
            transaction = connection.BeginTransaction("additemcat");
            command.Connection = connection;
            command.Transaction = transaction;

            try
            {

                //insert into ArchiveStockUpInward
                command.CommandText = "	UPDATE ItemCategory SET ItemCategory = @ItemCategory, HSNCode = @HSNCode,Tax = @Tax,hsnid=@hsnid WHERE ItemCategoryID = @ItemCategoryID";
                command.Parameters.AddWithValue("@ItemCategoryID", ItemCategoryID);
                command.Parameters.AddWithValue("@ItemCategory", ItemCategory);
                command.Parameters.AddWithValue("@HSNCode", HSNCode);
                command.Parameters.AddWithValue("@Tax", Tax);
                command.Parameters.AddWithValue("@hsnid", hsnid);
                command.ExecuteNonQuery();

                command.Parameters.Clear();


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

            //SqlParameter[] p = new SqlParameter[5];
            //p[0] = new SqlParameter("@MODE", "UPDATE_ITEMCAT");
            //p[1] = new SqlParameter("@ItemCategory", ItemCategory);
            //p[2] = new SqlParameter("@HSNCode", HSNCode);
            //p[3] = new SqlParameter("@Tax", Tax);
            //p[4] = new SqlParameter("@ItemCategoryID", ItemCategoryID);

            //return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        #endregion

        #region Category 2

        public int AddCat2(string C2Name, string C2Abbriviation, string ICatID)
        {
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@MODE", "ADD_CAT2");
            p[1] = new SqlParameter("@CategoryName", C2Name);
            p[2] = new SqlParameter("@Abbriviation", C2Abbriviation);
            p[3] = new SqlParameter("@ICatID", ICatID);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindCat2()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "BIND_CAT2");
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet GetCat2ByID(int CategoryID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "GET_CAT2_BY_ID");
            p[1] = new SqlParameter("@CategoryID", CategoryID);
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindCat2ByICatID(string ICatID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "BIND_CAT2_BY_ICATID");
            p[1] = new SqlParameter("@ICatID", ICatID);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int UpdateCat2(string C2Name, string C2Abbriviation, string CategoryID, string ICatID)
        {
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@MODE", "UPDATE_CAT2");
            p[1] = new SqlParameter("@CategoryName", C2Name);
            p[2] = new SqlParameter("@Abbriviation", C2Abbriviation);
            p[3] = new SqlParameter("@CategoryID", CategoryID);
            p[4] = new SqlParameter("@ICatID", ICatID);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        #endregion

        #region Category 3

        public int AddCat3(string C3Name, string C3Abbriviation, string ICatID)
        {
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@MODE", "ADD_CAT3");
            p[1] = new SqlParameter("@CategoryName", C3Name);
            p[2] = new SqlParameter("@Abbriviation", C3Abbriviation);
            p[3] = new SqlParameter("@ICatID", ICatID);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindCat3()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "BIND_CAT3");
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet GetCat3ByID(int CategoryID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "GET_CAT3_BY_ID");
            p[1] = new SqlParameter("@CategoryID", CategoryID);
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindCat3ByCat2ID(string Cat2ID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "BIND_CAT3_BY_CAT2ID");
            p[1] = new SqlParameter("@CategoryID", Cat2ID);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int UpdateCat3(string C3Name, string C3Abbriviation, string CategoryID, string ICatID)
        {
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@MODE", "UPDATE_CAT3");
            p[1] = new SqlParameter("@CategoryName", C3Name);
            p[2] = new SqlParameter("@Abbriviation", C3Abbriviation);
            p[3] = new SqlParameter("@CategoryID", CategoryID);
            p[4] = new SqlParameter("@ICatID", ICatID);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        #endregion

        #region Category 4

        public int AddCat4(string C4Name, string C4Abbriviation, string ICatID)
        {
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@MODE", "ADD_CAT4");
            p[1] = new SqlParameter("@CategoryName", C4Name);
            p[2] = new SqlParameter("@Abbriviation", C4Abbriviation);
            p[3] = new SqlParameter("@ICatID", ICatID);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindCat4()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "BIND_CAT4");
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet GetCat4ByID(int CategoryID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "GET_CAT4_BY_ID");
            p[1] = new SqlParameter("@CategoryID", CategoryID);
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindCat4ByCat3ID(string Cat2ID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "BIND_CAT4_BY_CAT3ID");
            p[1] = new SqlParameter("@CategoryID", Cat2ID);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int UpdateCat4(string C4Name, string C4Abbriviation, string CategoryID, string ICatID)
        {
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@MODE", "UPDATE_CAT4");
            p[1] = new SqlParameter("@CategoryName", C4Name);
            p[2] = new SqlParameter("@Abbriviation", C4Abbriviation);
            p[3] = new SqlParameter("@CategoryID", CategoryID);
            p[4] = new SqlParameter("@ICatID", ICatID);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        #endregion

        #region Category 5

        public int AddCat5(string C5Name, string C5Abbriviation, string ICatID)
        {
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@MODE", "ADD_CAT5");
            p[1] = new SqlParameter("@CategoryName", C5Name);
            p[2] = new SqlParameter("@Abbriviation", C5Abbriviation);
            p[3] = new SqlParameter("@ICatID", ICatID);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindCat5()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "BIND_CAT5");
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet GetCat5ByID(int CategoryID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "GET_CAT5_BY_ID");
            p[1] = new SqlParameter("@CategoryID", CategoryID);
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindCat5ByCat4ID(string Cat4ID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "BIND_CAT5_BY_CAT4ID");
            p[1] = new SqlParameter("@CategoryID", Cat4ID);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int UpdateCat5(string C5Name, string C5Abbriviation, string CategoryID, string ICatID)
        {
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@MODE", "UPDATE_CAT5");
            p[1] = new SqlParameter("@CategoryName", C5Name);
            p[2] = new SqlParameter("@Abbriviation", C5Abbriviation);
            p[3] = new SqlParameter("@CategoryID", CategoryID);
            p[4] = new SqlParameter("@ICatID", ICatID);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        #endregion

        #region Category Settings
       
        public DataSet BindCatSetting()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "BIND_CAT_SETTING");
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindCatSettingByID(int ID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "BIND_CAT_SETTING_BY_ID");
            p[1] = new SqlParameter("@ICSettingID", ID);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindCatTableBySetting(string SettingName)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "BIND_CAT_SETTING_BY_ID");
            p[1] = new SqlParameter("@ICSettingID", SettingName);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int UpdateCatSetting(string SettingName, bool IsAssigned, string ICSID)
        {
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@MODE", "UPDATE_CAT_SETTING");
            p[1] = new SqlParameter("@SettingName", SettingName);
            p[2] = new SqlParameter("@ICSettingID", ICSID);
            p[3] = new SqlParameter("@IsAssigned", IsAssigned);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        #endregion

    }
}