using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace DataBase
{
    public class StockUp
    {
        public string spsql = "SP_ItemStyle";

        #region StockUp

        public int AddStockUp(string StyleID, string LotID, string Size, string LastBarcode, string StyleCode, string Barcode)
        {
            SqlParameter[] p = new SqlParameter[7];
            p[0] = new SqlParameter("@MODE", "ADD_STOCKUP");
            p[1] = new SqlParameter("@StyleID", StyleID);
            p[2] = new SqlParameter("@LotID", LotID);
            p[3] = new SqlParameter("@SizeID", Size);
            p[4] = new SqlParameter("@LastBarcode", LastBarcode);
            p[5] = new SqlParameter("@StyleCode", StyleCode);
            p[6] = new SqlParameter("@Barcode", Barcode);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
           
        }


        public DataSet GetStockUpByID(int ItemCategoryID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "GET_ITEMCAT_BY_ID");
            p[1] = new SqlParameter("@ItemCategoryID", ItemCategoryID);
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

        #endregion

        public DataSet GetLastBarCodeStyleCode(string StyleID, string SizeID)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@MODE", "GET_STOCKUP_BARCODE");
            p[1] = new SqlParameter("@StyleID", StyleID);
            p[2] = new SqlParameter("@SizeID", SizeID);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindStockUpItem(string Status)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "GET_STOCKUP_ITEM");
            p[1] = new SqlParameter("@Status", Status);
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, "SP_Stockup", p);
        }
    }
}