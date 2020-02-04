using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace DataBase
{
    public class ItemStyle
    {
        public string spsql = "SP_ItemStyle";

        #region Item Style

        public int AddItemStyle(string ItemTitle, string Image, string ICatID, string Cat2ID, string Cat3ID, string Cat4ID, string Cat5ID, string Col1ID, string Col2ID, string Col3ID, string Col4ID, string Col5ID, string Col6ID, string Col7ID, string Col8ID, string Col9ID, string Col10ID, string Ctrl1, string Ctrl2, string Ctrl3, string Ctrl4, string Ctrl5, string Ctrl6, string Ctrl7, string Ctrl8, string Ctrl9, string Ctrl10, string Ctrl11, string Ctrl12, string Ctrl13, string Ctrl14, string Ctrl15, string Ctrl16, string Ctrl17, string Ctrl18, string Ctrl19, string Ctrl20, string LatestStylecode)
        {
            SqlParameter[] p = new SqlParameter[40];
            p[0] = new SqlParameter("@MODE", "ADD_ITEMSTYLE");
            p[1] = new SqlParameter("@Title", ItemTitle);
            p[2] = new SqlParameter("@Image", Image);
            p[3] = new SqlParameter("@ICatID", ICatID);
            p[4] = new SqlParameter("@Cat2ID", Cat2ID);
            p[5] = new SqlParameter("@Cat3ID", Cat3ID);
            p[6] = new SqlParameter("@Cat4ID", Cat4ID);
            p[7] = new SqlParameter("@Cat5ID", Cat5ID);
            p[8] = new SqlParameter("@Col1ID", Col1ID);
            p[9] = new SqlParameter("@Col2ID", Col2ID);
            p[10] = new SqlParameter("@Col3ID", Col3ID);
            p[11] = new SqlParameter("@Col4ID", Col4ID);
            p[12] = new SqlParameter("@Col5ID", Col5ID);
            p[13] = new SqlParameter("@Col6ID", Col6ID);
            p[14] = new SqlParameter("@Col7ID", Col7ID);
            p[15] = new SqlParameter("@Col8ID", Col8ID);
            p[16] = new SqlParameter("@Col9ID", Col9ID);
            p[17] = new SqlParameter("@Col10ID", Col10ID);
            p[18] = new SqlParameter("@Ctrl1", Ctrl1);
            p[19] = new SqlParameter("@Ctrl2", Ctrl2);
            p[20] = new SqlParameter("@Ctrl3", Ctrl3);
            p[21] = new SqlParameter("@Ctrl4", Ctrl4);
            p[22] = new SqlParameter("@Ctrl5", Ctrl5);
            p[23] = new SqlParameter("@Ctrl6", Ctrl6);
            p[24] = new SqlParameter("@Ctrl7", Ctrl7);
            p[25] = new SqlParameter("@Ctrl8", Ctrl8);
            p[26] = new SqlParameter("@Ctrl9", Ctrl9);
            p[27] = new SqlParameter("@Ctrl10", Ctrl10);
            p[28] = new SqlParameter("@Ctrl11", Ctrl11);
            p[29] = new SqlParameter("@Ctrl12", Ctrl12);
            p[30] = new SqlParameter("@Ctrl13", Ctrl13);
            p[31] = new SqlParameter("@Ctrl14", Ctrl14);
            p[32] = new SqlParameter("@Ctrl15", Ctrl15);
            p[33] = new SqlParameter("@Ctrl16", Ctrl16);
            p[34] = new SqlParameter("@Ctrl17", Ctrl17);
            p[35] = new SqlParameter("@Ctrl18", Ctrl18);
            p[36] = new SqlParameter("@Ctrl19", Ctrl19);
            p[37] = new SqlParameter("@Ctrl20", Ctrl20);
            p[38] = new SqlParameter("@LatestStylecode", LatestStylecode);

            // return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);

            p[39] = new SqlParameter("@RETURNVALUE", SqlDbType.Int);
            p[39].Direction = ParameterDirection.Output;
            DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, "SP_AddData", p);
            return int.Parse(p[39].Value.ToString());
        }

        public int AddItemImage(string ItemStyleID, string Image)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@MODE", "ADD_ITEMSTYLE_IMAGE");
            p[1] = new SqlParameter("@ItemStyleID", ItemStyleID);
            p[2] = new SqlParameter("@Image", Image);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet GetItemCatByID(int ItemCategoryID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "GET_ITEMCAT_BY_ID");
            p[1] = new SqlParameter("@ItemCategoryID", ItemCategoryID);
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet GetLastStyleCode()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "GET_STYLECODE");
           
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int UpdateItemCat(string ItemCategory, string HSNCode, string Tax, string ItemCategoryID)
        {
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@MODE", "UPDATE_ITEMCAT");
            p[1] = new SqlParameter("@ItemCategory", ItemCategory);
            p[2] = new SqlParameter("@HSNCode", HSNCode);
            p[3] = new SqlParameter("@Tax", Tax);
            p[4] = new SqlParameter("@ItemCategoryID", ItemCategoryID);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet GetStyleCombo()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "GET_STYLE_COMBO");

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        #endregion

        #region Search ItemStyle

        public DataSet BindSettingsAll()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "GET_SETTINGS");
          
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindItemStyleSearchAll(string ItemCategoryID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "GET_ITEM_STYLE_SEARCH_ALL");
            p[1] = new SqlParameter("@ItemCategoryID", ItemCategoryID);
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindItemStyleWildCardSearch(string ItemCategoryID, string SearchText)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@MODE", "GET_ITEM_STYLE_WILDCARD_SEARCH");
            p[1] = new SqlParameter("@ItemCategoryID", ItemCategoryID);
            p[2] = new SqlParameter("@SearchText", SearchText);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindItemStyleSearchByCol1Table(string ItemCategoryID, string Col1TableID)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@MODE", "GET_ITEM_STYLE_SEARCH_BY_COL1TABLE");
            p[1] = new SqlParameter("@ItemCategoryID", ItemCategoryID);
            p[2] = new SqlParameter("@Col1ID", Col1TableID);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindItemStyleSearchByCol2Table(string ItemCategoryID, string Col2TableID)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@MODE", "GET_ITEM_STYLE_SEARCH_BY_COL2TABLE");
            p[1] = new SqlParameter("@ItemCategoryID", ItemCategoryID);
            p[2] = new SqlParameter("@Col2ID", Col2TableID);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindItemStyleSearchByCol3Table(string ItemCategoryID, string Col3TableID)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@MODE", "GET_ITEM_STYLE_SEARCH_BY_COL3TABLE");
            p[1] = new SqlParameter("@ItemCategoryID", ItemCategoryID);
            p[2] = new SqlParameter("@Col3ID", Col3TableID);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindItemStyleSearchByCol4Table(string ItemCategoryID, string Col4TableID)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@MODE", "GET_ITEM_STYLE_SEARCH_BY_COL4TABLE");
            p[1] = new SqlParameter("@ItemCategoryID", ItemCategoryID);
            p[2] = new SqlParameter("@Col4ID", Col4TableID);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindItemStyleSearchByCol5Table(string ItemCategoryID, string Col5TableID)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@MODE", "GET_ITEM_STYLE_SEARCH_BY_COL5TABLE");
            p[1] = new SqlParameter("@ItemCategoryID", ItemCategoryID);
            p[2] = new SqlParameter("@Col5ID", Col5TableID);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindItemStyleSearchByCol6Table(string ItemCategoryID, string Col6TableID)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@MODE", "GET_ITEM_STYLE_SEARCH_BY_COL6TABLE");
            p[1] = new SqlParameter("@ItemCategoryID", ItemCategoryID);
            p[2] = new SqlParameter("@Col6ID", Col6TableID);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindItemStyleSearchByCol7Table(string ItemCategoryID, string Col7TableID)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@MODE", "GET_ITEM_STYLE_SEARCH_BY_COL7TABLE");
            p[1] = new SqlParameter("@ItemCategoryID", ItemCategoryID);
            p[2] = new SqlParameter("@Col7ID", Col7TableID);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindItemStyleSearchByCol8Table(string ItemCategoryID, string Col8TableID)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@MODE", "GET_ITEM_STYLE_SEARCH_BY_COL8TABLE");
            p[1] = new SqlParameter("@ItemCategoryID", ItemCategoryID);
            p[2] = new SqlParameter("@Col8ID", Col8TableID);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindItemStyleSearchByCol9Table(string ItemCategoryID, string Col9TableID)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@MODE", "GET_ITEM_STYLE_SEARCH_BY_COL9TABLE");
            p[1] = new SqlParameter("@ItemCategoryID", ItemCategoryID);
            p[2] = new SqlParameter("@Col9ID", Col9TableID);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindItemStyleSearchByCol10Table(string ItemCategoryID, string Col10TableID)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@MODE", "GET_ITEM_STYLE_SEARCH_BY_COL10TABLE");
            p[1] = new SqlParameter("@ItemCategoryID", ItemCategoryID);
            p[2] = new SqlParameter("@Col10ID", Col10TableID);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        #endregion
    }
}