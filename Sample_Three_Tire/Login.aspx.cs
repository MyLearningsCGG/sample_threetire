using Sample_Three_Tire.BAL.Implementations;
using Sample_Three_Tire.BAL.Interface;
using Sample_Three_Tire.CommonUtilities;
using Sample_Three_Tire.Entities;
using System;
using System.Data;
using System.Web.UI;

namespace Sample_Three_Tire
{
    public partial class Login : System.Web.UI.Page
    {
        LoginBL objVadidateUser = new LoginBL();
        ILogin iVadidateUser;
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        private void GetEmpDetails()
        {
            try
            {
                LoginUser loginUser = new LoginUser();
                int roleId = 0, UserId = 0;
                string EncodePassword = CommonUtilities.Utilities.EncodePasswordToBase64(txtPassword.Text.Trim());
                loginUser.UserId = txtUserID.Text.Trim();
                loginUser.Password = EncodePassword;
                LoginBL objLogin = new LoginBL(); // Calling  valid user details implementation...
                iVadidateUser = objLogin; // Intializing user Details Interface....

                DataSet _userDetails = objLogin.ValidateUser(loginUser);

                if (_userDetails != null && _userDetails.Tables.Count > 0)
                {
                    if (_userDetails.Tables[0].Rows.Count > 0)
                    {
                        Session["UserName"] = _userDetails.Tables[0].Rows[0]["UNAME"].ToString();
                        Session["UserPassword"] = _userDetails.Tables[0].Rows[0]["USERPASSWORD"].ToString();

                        roleId = Convert.ToInt32(_userDetails.Tables[0].Rows[0]["ROLEID"]);
                        UserId = Convert.ToInt32(_userDetails.Tables[0].Rows[0]["USERID"]);
                        Session["RoleId"] = roleId;
                        Session["USERID"] = UserId;

                        if (roleId != 0)
                        {

                            if (roleId == 2 ) //2 is CLERK , 3 is ESTATE_OFFIICER
                                Response.Redirect("Dashboard.aspx", false);
                            else if (roleId==1)
                                Response.Redirect("Dashboard.aspx", false);
                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Not Athorizsed...');", true);
                                txtPassword.Text = string.Empty;
                                txtUserID.Text = string.Empty;
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), this.ClientID, "alert('Invalid User Name or Password');", true);
                            txtPassword.Text = string.Empty;
                            txtUserID.Text = string.Empty;

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "ajax", "InvalidUseralertMessage();", true);
                    }

                }
                // 2=Clerk,3=Officer

            }

            catch (Exception ex)
            {
                ErrorLog.SendErrorToText(ex);
                string strScript = "<script>";
                strScript += "alert('An Error Has Occurred....! Please login again');";
                strScript += "window.location='../Login.aspx';";
                strScript += "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ajax", strScript, false);
            }
            finally
            {

            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserID.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                string strScript4 = "<script>";
                strScript4 += "alert('Please Enter UserName/Password');";
                strScript4 += "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ajax", strScript4, false);
            }
            else
            {
                GetEmpDetails();
            }
        }
    }
}