using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.IO;


    /// <summary>
    /// Summary description for RecordExceptionCls
    /// </summary>
    public class RecordExceptionCls
    {

      

        public RecordExceptionCls()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void recordException(Exception e)
        {
            try
            {
                string makerid = "";
                string makername = "";
                
                try
                {
                   makerid=  HttpContext.Current.Session["loginid"].ToString();//id of logged in admin

                   makername = HttpContext.Current.Session["username"].ToString();//name of logged in admin
                }
                catch (Exception ex)
                {
                    makerid = "NA";//id of logged in admin

                    makername = "NA";//name of logged in admin
                }
                if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/err.log")))
                {

                    using (FileStream fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath("~/err.log"), FileMode.Append))
                    {
                        StreamWriter sw = new StreamWriter(fs);

                        sw.Write(System.DateTime.Now + " : " + e.ToString() + Environment.NewLine
                        + e.StackTrace + Environment.NewLine + Environment.NewLine);

                        sw.Close();
                    }
                }
                else
                {

                    using (FileStream fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath("~/err.log"), FileMode.OpenOrCreate))
                    {
                        StreamWriter sw = new StreamWriter(fs);
                        sw.Write(System.DateTime.Now + " [ #" + makerid + "-" + makername + " ]" + " : " + e.ToString() + Environment.NewLine + e.StackTrace + Environment.NewLine + Environment.NewLine);
                        sw.Close();
                    }

                }

                // MessageBox.Show(e.ToString());
            }
            catch (Exception ex)
            {
               
            }
        }
    }
