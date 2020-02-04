using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
namespace DataBase
{
    public class StyleColumnTable
    {
        public string spsql = "SP_StyleColumnTable";

        #region Column 1

        public int AddCol1(string Name)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "ADD_COL1");
            p[1] = new SqlParameter("@ColumnName", Name);
            
            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int AddCol1New(string C1Name, string image1,string brandStatus, string ID)
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
            transaction = connection.BeginTransaction("AddCol1New");
            command.Connection = connection;
            command.Transaction = transaction;

            try
            {
                int result = 0;
                command.Parameters.AddWithValue("@C1Name", C1Name);
                command.Parameters.AddWithValue("@image1", image1);
                command.Parameters.AddWithValue("@brandStatus", brandStatus);
                if (ID.Equals("0"))
                {
                    command.CommandText = "INSERT INTO Column1 (C1Name,brandImage,brandStatus) " +
                    " Values (@C1Name,@image1,@brandStatus)";
                    

                }
                else
                {
                    string query = "update Column1 set C1Name=@C1Name,brandStatus=@brandStatus";
                    if(!image1.Equals(""))
                    {
                        query += ",brandImage=@image1";
                    }
                    query += " where Col1ID=@Col1ID";
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@Col1ID", ID);
                }

                command.ExecuteNonQuery();
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


        public DataSet BindCol1()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "BIND_COL1");
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

      
        public DataSet GetCol1ByID(int ColumnID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "GET_COL1_BY_ID");
            p[1] = new SqlParameter("@ColumnID", ColumnID);
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int UpdateCol1(string Name, string ColumnID)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@MODE", "UPDATE_COL1");
            p[1] = new SqlParameter("@ColumnName", Name);
            p[2] = new SqlParameter("@ColumnID", ColumnID);
          
            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        #endregion

        #region Column 2

        public int AddCol2(string Name)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "ADD_COL2");
            p[1] = new SqlParameter("@ColumnName", Name);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindCol2()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "BIND_COL2");
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet GetCol2ByID(int ColumnID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "GET_COL2_BY_ID");
            p[1] = new SqlParameter("@ColumnID", ColumnID);
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int UpdateCol2(string Name, string ColumnID)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@MODE", "UPDATE_COL2");
            p[1] = new SqlParameter("@ColumnName", Name);
            p[2] = new SqlParameter("@ColumnID", ColumnID);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        #endregion

        #region Column 3

        public int AddCol3(string Name)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "ADD_COL3");
            p[1] = new SqlParameter("@ColumnName", Name);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindCol3()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "BIND_COL3");
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet GetCol3ByID(int ColumnID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "GET_COL3_BY_ID");
            p[1] = new SqlParameter("@ColumnID", ColumnID);
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int UpdateCol3(string Name, string ColumnID)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@MODE", "UPDATE_COL3");
            p[1] = new SqlParameter("@ColumnName", Name);
            p[2] = new SqlParameter("@ColumnID", ColumnID);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        #endregion

        #region Column 4

        public int AddCol4(string Name)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "ADD_COL4");
            p[1] = new SqlParameter("@ColumnName", Name);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindCol4()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "BIND_COL4");
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet GetCol4ByID(int ColumnID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "GET_COL4_BY_ID");
            p[1] = new SqlParameter("@ColumnID", ColumnID);
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int UpdateCol4(string Name, string ColumnID)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@MODE", "UPDATE_COL4");
            p[1] = new SqlParameter("@ColumnName", Name);
            p[2] = new SqlParameter("@ColumnID", ColumnID);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        #endregion

        #region Column 5

        public int AddCol5(string Name)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "ADD_COL5");
            p[1] = new SqlParameter("@ColumnName", Name);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindCol5()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "BIND_COL5");
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet GetCol5ByID(int ColumnID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "GET_COL5_BY_ID");
            p[1] = new SqlParameter("@ColumnID", ColumnID);
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int UpdateCol5(string Name, string ColumnID)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@MODE", "UPDATE_COL5");
            p[1] = new SqlParameter("@ColumnName", Name);
            p[2] = new SqlParameter("@ColumnID", ColumnID);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        #endregion

        #region Column 6

        public int AddCol6(string Name)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "ADD_COL6");
            p[1] = new SqlParameter("@ColumnName", Name);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindCol6()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "BIND_COL6");
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet GetCol6ByID(int ColumnID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "GET_COL6_BY_ID");
            p[1] = new SqlParameter("@ColumnID", ColumnID);
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int UpdateCol6(string Name, string ColumnID)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@MODE", "UPDATE_COL6");
            p[1] = new SqlParameter("@ColumnName", Name);
            p[2] = new SqlParameter("@ColumnID", ColumnID);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        #endregion

        #region Column 7

        public int AddCol7(string Name)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "ADD_COL7");
            p[1] = new SqlParameter("@ColumnName", Name);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindCol7()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "BIND_COL7");
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet GetCol7ByID(int ColumnID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "GET_COL7_BY_ID");
            p[1] = new SqlParameter("@ColumnID", ColumnID);
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int UpdateCol7(string Name, string ColumnID)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@MODE", "UPDATE_COL7");
            p[1] = new SqlParameter("@ColumnName", Name);
            p[2] = new SqlParameter("@ColumnID", ColumnID);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        #endregion

        #region Column 8

        public int AddCol8(string Name)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "ADD_COL8");
            p[1] = new SqlParameter("@ColumnName", Name);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindCol8()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "BIND_COL8");
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet GetCol8ByID(int ColumnID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "GET_COL8_BY_ID");
            p[1] = new SqlParameter("@ColumnID", ColumnID);
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int UpdateCol8(string Name, string ColumnID)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@MODE", "UPDATE_COL8");
            p[1] = new SqlParameter("@ColumnName", Name);
            p[2] = new SqlParameter("@ColumnID", ColumnID);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        #endregion

        #region Column 9

        public int AddCol9(string Name)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "ADD_COL9");
            p[1] = new SqlParameter("@ColumnName", Name);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindCol9()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "BIND_COL9");
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet GetCol9ByID(int ColumnID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "GET_COL9_BY_ID");
            p[1] = new SqlParameter("@ColumnID", ColumnID);
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int UpdateCol9(string Name, string ColumnID)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@MODE", "UPDATE_COL9");
            p[1] = new SqlParameter("@ColumnName", Name);
            p[2] = new SqlParameter("@ColumnID", ColumnID);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        #endregion

        #region Column 10

        public int AddCol10(string Name)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "ADD_COL10");
            p[1] = new SqlParameter("@ColumnName", Name);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindCol10()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "BIND_COL10");
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet GetCol10ByID(int ColumnID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "GET_COL10_BY_ID");
            p[1] = new SqlParameter("@ColumnID", ColumnID);
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int UpdateCol10(string Name, string ColumnID)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@MODE", "UPDATE_COL10");
            p[1] = new SqlParameter("@ColumnName", Name);
            p[2] = new SqlParameter("@ColumnID", ColumnID);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        #endregion

        #region Column Table Settings

        public DataSet BindColSetting()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "BIND_COL_SETTING");
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindAssignedColSetting()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "BIND_ASSIGNED_COL_SETTING");
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int UpdateColSetting(string SettingName, bool IsAssigned, string CSID)
        {
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@MODE", "UPDATE_COL_SETTING");
            p[1] = new SqlParameter("@SettingName", SettingName);
            p[2] = new SqlParameter("@CTSettingID", CSID);
            p[3] = new SqlParameter("@IsAssigned", IsAssigned);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        #endregion

        #region Column Control Settings

        public DataSet BindColControlSetting()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "BIND_COL_CONTROL_SETTING");
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int AddVirtualLocationSetting(string SettingName, bool IsAssigned, string StyleCSID, string LocationID)
        {
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@MODE", "ADD_VIRTUAL_LOCATION_SETTING");
            p[1] = new SqlParameter("@SettingName", SettingName);
            p[2] = new SqlParameter("@StyleCSID", StyleCSID);
            p[3] = new SqlParameter("@IsAssigned", IsAssigned);
            p[4] = new SqlParameter("@LocationID", LocationID);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int UpdateColControlSetting(string SettingName, bool IsAssigned, string StyleCSID)
        {
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@MODE", "UPDATE_COL_CONTROL_SETTING");
            p[1] = new SqlParameter("@SettingName", SettingName);
            p[2] = new SqlParameter("@StyleCSID", StyleCSID);
            p[3] = new SqlParameter("@IsAssigned", IsAssigned);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindAssignedColControlSetting()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "BIND_ASSIGN_COL_CONTROL_SETTING");
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindVirtualLocationSetting(string LocationID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "BIND_VIRTUAL_LOCATION_SETTING");
            p[1] = new SqlParameter("@LocationID", LocationID);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        #endregion

        #region Item style search

        public DataSet BindCol1ByItemCatID(string ItemcatID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "BIND_COL1_BY_ITEMCATID");
            p[1] = new SqlParameter("@ItemCatID", ItemcatID);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindCol2ByItemCatID(string ItemcatID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "BIND_COL2_BY_ITEMCATID");
            p[1] = new SqlParameter("@ItemCatID", ItemcatID);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindCol3ByItemCatID(string ItemcatID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "BIND_COL3_BY_ITEMCATID");
            p[1] = new SqlParameter("@ItemCatID", ItemcatID);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindCol4ByItemCatID(string ItemcatID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "BIND_COL4_BY_ITEMCATID");
            p[1] = new SqlParameter("@ItemCatID", ItemcatID);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindCol5ByItemCatID(string ItemcatID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "BIND_COL5_BY_ITEMCATID");
            p[1] = new SqlParameter("@ItemCatID", ItemcatID);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindCol6ByItemCatID(string ItemcatID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "BIND_COL6_BY_ITEMCATID");
            p[1] = new SqlParameter("@ItemCatID", ItemcatID);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindCol7ByItemCatID(string ItemcatID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "BIND_COL7_BY_ITEMCATID");
            p[1] = new SqlParameter("@ItemCatID", ItemcatID);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindCol8ByItemCatID(string ItemcatID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "BIND_COL8_BY_ITEMCATID");
            p[1] = new SqlParameter("@ItemCatID", ItemcatID);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindCol9ByItemCatID(string ItemcatID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "BIND_COL9_BY_ITEMCATID");
            p[1] = new SqlParameter("@ItemCatID", ItemcatID);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindCol10ByItemCatID(string ItemcatID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "BIND_COL10_BY_ITEMCATID");
            p[1] = new SqlParameter("@ItemCatID", ItemcatID);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }
       
        #endregion
    }
}