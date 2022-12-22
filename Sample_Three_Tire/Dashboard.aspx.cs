using Sample_Three_Tire.CommonUtilities;
using System;
using System.Web.UI;

namespace Sample_Three_Tire
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                try
                {
                    if (CommonUtilities.Utilities.mySession.GetRoleId() != 0)
                    {
                        if (Session["RoleId"].ToString() == "3")
                        {

                        }
                    }
                    else
                    {
                        string strScript = "<script>";
                        strScript += "alert('Session Expired or Access Denied please login');";
                        strScript += "window.location='Login.aspx';";
                        strScript += "</script>";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ajax", strScript, false);
                        Session.Abandon();// To Clear All Session Values....
                    }
                }
                catch (Exception ex)
                {

                    ErrorLog.SendErrorToText(ex);
                    string strScript = "<script>";
                    strScript += "alert('An Error Has Occurred....! Please login again');";
                    strScript += "window.location='Error.Html';";
                    strScript += "</script>";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ajax", strScript, false);
                    Session.Abandon();
                }
            }
        }
    }
}