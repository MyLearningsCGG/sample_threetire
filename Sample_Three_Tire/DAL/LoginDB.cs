using Oracle.ManagedDataAccess.Client;
using Sample_Three_Tire.CommonUtilities;
using Sample_Three_Tire.Entities;
using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Web;

namespace Sample_Three_Tire.DAL
{
    public class LoginDB
    {
        DataSet ds = new DataSet();
        public DataSet GetEmpDetails(LoginUser loginUser)
        {
            OracleConnection Oracleconnection = new OracleConnection(CommonUtilities.Utilities.sqlConnectionProvider.GetConnection());
            try
            {
                Oracleconnection.Open();
                OracleCommand cmd = new OracleCommand("PROC_LOGIN", Oracleconnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = Oracleconnection;
                cmd.Parameters.Add("P_USERNAME", OracleDbType.Varchar2).Value = loginUser.UserId;
                cmd.Parameters.Add("P_PASSWORD", OracleDbType.Varchar2).Value = loginUser.Password;
                cmd.Parameters.Add("P_RECORDS", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataAdapter OracleDataAdapter = new OracleDataAdapter(cmd);
                OracleDataAdapter.Fill(ds);
            }
            catch (Exception ex)
            {
                ErrorLog.SendErrorToText(ex);
            }
            finally
            {
                Oracleconnection.Close();
            }
            return ds;
        }
    }
}