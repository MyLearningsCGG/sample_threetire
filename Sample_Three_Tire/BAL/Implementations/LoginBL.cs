using Sample_Three_Tire.BAL.Interface;
using Sample_Three_Tire.DAL;
using Sample_Three_Tire.Entities;
using System;
using System.Data;

namespace Sample_Three_Tire.BAL.Implementations
{
   
    public class LoginBL : ILogin
    {
        LoginDB loginDB = new LoginDB();
        public DataSet ValidateUser(LoginUser loginUser)
        {
            return loginDB.GetEmpDetails(loginUser);
        }
    }
}