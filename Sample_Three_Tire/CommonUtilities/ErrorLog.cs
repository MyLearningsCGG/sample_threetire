using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using context = System.Web.HttpContext;

namespace Sample_Three_Tire.CommonUtilities
{
    public class ErrorLog
    {
        private static String ErrorlineNo, Errormsg, extype, exurl, hostIp, ErrorLocation;
        public static void SendErrorToText(Exception ex)
        {
            var line = Environment.NewLine + Environment.NewLine;

            ErrorlineNo = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
            Errormsg = ex.GetType().Name.ToString();
            extype = ex.GetType().ToString();
            exurl = context.Current.Request.Url.ToString();
            ErrorLocation = ex.Message.ToString();

            try
            {
                string strFilepath = context.Current.Server.MapPath("~/Error_Log/");  //Text File Path

                if (!Directory.Exists(strFilepath))
                {
                    Directory.CreateDirectory(strFilepath);

                }
                strFilepath = strFilepath + DateTime.Today.ToString("dd-MM-yy") + ".txt";   //Text File Name
                if (!File.Exists(strFilepath))
                {

                    File.Create(strFilepath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strFilepath))
                {
                    string error = "Log Written Date:" + " " + DateTime.Now.ToString() + line + "Error Line No :" + " " + ErrorlineNo + line + "Error Message:" + " " + Errormsg + line + "Exception Type:" + " " + extype + line + "Error Location :" + " " + ErrorLocation + line + " Error Page Url:" + " " + exurl + line + "User Host IP:" + " " + hostIp + line;
                    sw.WriteLine("-----------Exception Details on " + " " + DateTime.Now.ToString() + "-----------------");
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                    sw.WriteLine(line);
                    sw.WriteLine(error);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    sw.WriteLine(line);
                    sw.Flush();
                    sw.Close();

                }

            }
            catch (Exception e)
            {
                e.ToString();

            }
        }
    }
}