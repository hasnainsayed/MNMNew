using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
namespace DataBase
{

    public class login
    {

        public string spsql = "SP_Login";

        public DataSet AdminLogin(string UserName, string Password)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@MODE", "LOGIN");
            p[1] = new SqlParameter("@USERNAME", UserName);
            p[2] = new SqlParameter("@PASSWORD", Password);

            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }

        public DataSet ForgotPassword(string Email)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@MODE", "FORGOT_PASSWORD");
            p[1] = new SqlParameter("@EMAIL", Email);
           
            return DataBase.SqlHelper.ExecuteDataset(new SqlConnection(SqlHelper.GetConnectionString()), CommandType.StoredProcedure, spsql, p);
        }
    }
}