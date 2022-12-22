using System;
using System.Configuration;
using System.Text;
using System.Web;

namespace Sample_Three_Tire.CommonUtilities
{
    public class Utilities
    {
        public static class sqlConnectionProvider
        {
            public static string GetConnection()
            {
                return ConfigurationManager.ConnectionStrings["conStr"].ToString();
            }

        }
        public static string EncodePasswordToBase64(string strPassword)
        {
            try
            {
                byte[] encData_byte = System.Text.Encoding.UTF8.GetBytes(strPassword);
                return Convert.ToBase64String(encData_byte);
            }
            catch (Exception ex)
            {
                ErrorLog.SendErrorToText(ex);
                throw new Exception("strError in base64Encode" + ex.Message);
            }
        }

        public static string DecodePasswordBase64(string strPassword)
        {
            try
            {
                UTF8Encoding encoder = new UTF8Encoding();
                Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(strPassword);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                return new String(decoded_char);
            }
            catch (Exception ex)
            {
                ErrorLog.SendErrorToText(ex);
                throw new Exception("strError in base64Encode" + ex.Message); ;
            }


        }

        public static class mySession
        {
            public static int roleid = 0;

            /// <summary>
            /// Initiating session value and returning it..
            /// </summary>
            public static string GetSession()
            {
                string strUsernameSession = string.Empty;
                if (HttpContext.Current.Session["UserName"] != null)
                    strUsernameSession = HttpContext.Current.Session["UserName"].ToString().Trim();
                return strUsernameSession.Trim();
            }

            /// <summary>
            ///  Get the session of ROLEID value converting into Intiger and returning it..
            /// </summary>
            /// <returns>GetRoleId</returns>
            public static int GetRoleId()
            {
                if (HttpContext.Current.Session["RoleId"] != null)
                    roleid = Convert.ToInt16(HttpContext.Current.Session["RoleId"].ToString().Trim());
                else
                    roleid = 0;
                return roleid;
            }

            /// <summary>
            /// Get the session of USERID value converting into Intiger and returning it..
            /// </summary>
            /// <returns>GetUserID</returns>
            public static int GetUserID()
            {
                return Convert.ToInt16(HttpContext.Current.Session["USERID"].ToString());
            }

            /// <summary>
            ///  Get the session of ZONEID value converting into Intiger and returning it..
            /// </summary>
            /// <returns>GetUserID</returns>
            public static int GetZoneID()
            {
                return Convert.ToInt16(HttpContext.Current.Session["zoneid"].ToString());
            }

            /// <summary>
            ///  Get the session of CIRCLEID value converting into Intiger and returning it..
            /// </summary>
            /// <returns>GetUserID</returns>
            public static int GetCircleID()
            {
                return Convert.ToInt16(HttpContext.Current.Session["Circleid"].ToString());
            }

            /// <summary>
            ///  Get the session of WARDID value converting into Intiger and returning it..
            /// </summary>
            /// <returns>GetUserID</returns>
            public static int GetWardID()
            {
                return Convert.ToInt16(HttpContext.Current.Session["Wardid"].ToString());
            }

        }
    }
}