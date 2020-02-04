using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
namespace DataBase
{
    public class Masters
    {
        public string spsql = "SP_Masters";

        #region Location Type

        public int AddLoacationType(string Name)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "ADD_LOCATIONTYPE");
            p[1] = new SqlParameter("@Name", Name);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindLocationType()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "BIND_LOCATIONTYPE");
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet GetLocationTypeByID(int LocationTypeID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "GET_LOCATIONTYPE_BY_ID");
            p[1] = new SqlParameter("@LocationTypeID", LocationTypeID);
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int UpdateLocationType(string Name, string LocationTypeID)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@MODE", "UPDATE_LOCATIONTYPE");
            p[1] = new SqlParameter("@Name", Name);
            p[2] = new SqlParameter("@LocationTypeID", LocationTypeID);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int DeleteSupplierr(int BuyerID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "DELETE_Vendor");
            p[1] = new SqlParameter("@VendorID", BuyerID);
            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        #endregion

        #region Location Type2

        public int AddLoacationType2(string Name)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "ADD_LOCATIONTYPE2");
            p[1] = new SqlParameter("@Name", Name);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindLocationType2()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "BIND_LOCATIONTYPE2");
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet GetLocationTypeByID2(int LocationTypeID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "GET_LOCATIONTYPE_BY_ID2");
            p[1] = new SqlParameter("@LocationTypeID", LocationTypeID);
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int UpdateLocationType2(string Name, string LocationTypeID)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@MODE", "UPDATE_LOCATIONTYPE2");
            p[1] = new SqlParameter("@Name", Name);
            p[2] = new SqlParameter("@LocationTypeID", LocationTypeID);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }
       
        #endregion

        #region Location

        public int AddLocation(string Location, string LocationTypeID, string LocationTypeID2, string Contact, string Address)
        {
            SqlParameter[] p = new SqlParameter[6];
            p[0] = new SqlParameter("@MODE", "ADD_LOCATION");
            p[1] = new SqlParameter("@Location", Location);
            p[2] = new SqlParameter("@LocationTypeID", LocationTypeID);
            p[3] = new SqlParameter("@LTypeID2", LocationTypeID2);
            p[4] = new SqlParameter("@Contact", Contact);
            p[5] = new SqlParameter("@Address", Address);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindLocation()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "BIND_LOCATION");
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindLocationByLocation2typeID(string LocationType2ID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "BIND_LOCATION_BY_LOCATION2TYPEID");
            p[1] = new SqlParameter("@LocationTypeID", LocationType2ID);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet GetLocationByID(int LocationID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "GET_LOCATION_BY_ID");
            p[1] = new SqlParameter("@LocationID", LocationID);
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int UpdateLocation(string Location, string LocationTypeID, string LocationTypeID2, string LocationID, string Contact, string Address)
        {
            SqlParameter[] p = new SqlParameter[7];
            p[0] = new SqlParameter("@MODE", "UPDATE_LOCATION");
            p[1] = new SqlParameter("@Location", Location);
            p[2] = new SqlParameter("@LocationTypeID", LocationTypeID);
            p[3] = new SqlParameter("@LTypeID2", LocationTypeID2);
            p[4] = new SqlParameter("@LocationID", LocationID);
            p[5] = new SqlParameter("@Contact", Contact);
            p[6] = new SqlParameter("@Address", Address);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        #endregion

        #region Sub Location

        public int AddSubLoacation(string Sublocation, string LocationID)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@MODE", "ADD_SUBLOCATION");
            p[1] = new SqlParameter("@Sublocation", Sublocation);
            p[2] = new SqlParameter("@LocationID", LocationID);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindSubLocation()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "BIND_SUBLOCATION");
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindSubLocationByLocID(string LocationID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "BIND_SUBLOCATION_BY_LOCATIONID");
            p[1] = new SqlParameter("@LocationID", LocationID);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet GetSubLocationByID(int SublocationID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "GET_SUBLOCATION_BY_ID");
            p[1] = new SqlParameter("@SublocationID", SublocationID);
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int UpdateSubLocation(string Sublocation, string SublocationID, string LocationID)
        {
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@MODE", "UPDATE_SUBLOCATION");
            p[1] = new SqlParameter("@Sublocation", Sublocation);
            p[2] = new SqlParameter("@SublocationID", SublocationID);
            p[3] = new SqlParameter("@LocationID", LocationID);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        #endregion

        #region Rack

        public int AddRack(string Rack, string SublocationID)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@MODE", "ADD_Rack");
            p[1] = new SqlParameter("@Rack", Rack);
            p[2] = new SqlParameter("@Sublocation", SublocationID);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindRack()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "BIND_Rack");
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindRackBySublocationID(string SublocationID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "BIND_RACK_BY_SLOCTATIONID");
            p[1] = new SqlParameter("@SublocationID", SublocationID);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet GetRackByID(int RackID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "GET_Rack_BY_ID");
            p[1] = new SqlParameter("@RackID", RackID);
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int UpdateRack(string Rack, string RackID, string SublocationID)
        {
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@MODE", "UPDATE_Rack");
            p[1] = new SqlParameter("@Rack", Rack);
            p[2] = new SqlParameter("@RackID", RackID);
            p[3] = new SqlParameter("@SublocationID", SublocationID);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        #endregion

        #region Stack

        public int AddStack(string Stack, string RackID)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@MODE", "ADD_Stack");
            p[1] = new SqlParameter("@Stack", Stack);
            p[2] = new SqlParameter("@RackID", RackID);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindStack()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "BIND_Stack");
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindStackByRackID(string RackID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "BIND_STACK_BY_RACKID");
            p[1] = new SqlParameter("@RackID", RackID);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet GetStackByID(int StackID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "GET_Stack_BY_ID");
            p[1] = new SqlParameter("@StackID", StackID);
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int UpdateStack(string Stack, string StackID, string RackID)
        {
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@MODE", "UPDATE_Stack");
            p[1] = new SqlParameter("@Stack", Stack);
            p[2] = new SqlParameter("@StackID", StackID);
            p[3] = new SqlParameter("@RackID", RackID);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        #endregion

        #region Vendor

        public int AddVendor(string VendorName, string Contact, string Email, string City)
        {
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@MODE", "ADD_VENDOR");
            p[1] = new SqlParameter("@VendorName", VendorName);
            p[2] = new SqlParameter("@Contact", Contact);
            p[3] = new SqlParameter("@Email", Email);
            p[4] = new SqlParameter("@City", City);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindVendor()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "BIND_VENDOR");
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet GetVendorByID(int VendorID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "GET_VENDOR_BY_ID");
            p[1] = new SqlParameter("@VendorID", VendorID);
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int UpdateVendor(string VendorName, string Contact, string Email, string City, string VendorID)
        {
            SqlParameter[] p = new SqlParameter[6];
            p[0] = new SqlParameter("@MODE", "UPDATE_VENDOR");
            p[1] = new SqlParameter("@VendorName", VendorName);
            p[2] = new SqlParameter("@Contact", Contact);
            p[3] = new SqlParameter("@Email", Email);
            p[4] = new SqlParameter("@City", City);
            p[5] = new SqlParameter("@VendorID", VendorID);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        #endregion

        #region Size

        public int AddSize(string ItemcatID, string Size1, string Size2, string Size3, string Size4, string LatestSizeCode)
        {
            SqlParameter[] p = new SqlParameter[7];
            p[0] = new SqlParameter("@MODE", "ADD_SIZE");
            p[1] = new SqlParameter("@ItemCategoryID", ItemcatID);
            p[2] = new SqlParameter("@Size1", Size1);
            p[3] = new SqlParameter("@Size2", Size2);
            p[4] = new SqlParameter("@Size3", Size3);
            p[5] = new SqlParameter("@Size4", Size4);
            p[6] = new SqlParameter("@LatestSizeCode", LatestSizeCode);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindSize()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "BIND_SIZE");
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet GetSizeByID(int SizeID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "GET_SIZE_BY_ID");
            p[1] = new SqlParameter("@SizeID", SizeID);
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int UpdateSize(string ItemcatID, string Size1, string Size2, string Size3, string Size4, string SizeID)
        {
            SqlParameter[] p = new SqlParameter[7];
            p[0] = new SqlParameter("@MODE", "UPDATE_SIZE");
            p[1] = new SqlParameter("@ItemCategoryID", ItemcatID);
            p[2] = new SqlParameter("@Size1", Size1);
            p[3] = new SqlParameter("@Size2", Size2);
            p[4] = new SqlParameter("@Size3", Size3);
            p[5] = new SqlParameter("@Size4", Size4);
            p[6] = new SqlParameter("@SizeID", SizeID);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet GetLastSizeCode()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "GET_SIZECODE");

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        #endregion

        #region Bag

        public int AddBag(string VendorID, string NoOfBag, string Description)
        {
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@MODE", "ADD_BAG");
            p[1] = new SqlParameter("@VendorID", VendorID);
            p[2] = new SqlParameter("@NoOfBag", NoOfBag);
            p[3] = new SqlParameter("@BagDescription", Description);

            p[4] = new SqlParameter("@RETURNVALUE", SqlDbType.Int);
            p[4].Direction = ParameterDirection.Output;
            DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, "SP_AddData", p);
            return int.Parse(p[4].Value.ToString());

            //return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int AddBagTransaction(int BagID, string BagCode)
        {
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@MODE", "ADD_BAG_TRANSACTION");
            p[1] = new SqlParameter("@BagID", BagID);
            p[2] = new SqlParameter("@BagCode", BagCode);
                     
            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet BindBag()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "BIND_BAG");
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet GetBagByID(int LotID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "GET_BAG_BY_ID");
            p[1] = new SqlParameter("@LotId", LotID);
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public int UpdateBag(string Name, string LotID)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@MODE", "UPDATE_BAG");
            p[1] = new SqlParameter("@Name", Name);
            p[2] = new SqlParameter("@LotId", LotID);

            return DataBase.SqlHelper.ExecuteNonQuery(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet GetLastBagCode()
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MODE", "GET_BAGCODE");

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        #endregion
    }
}