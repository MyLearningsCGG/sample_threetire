using Sample_Three_Tire.Entities;
using System.Data;

namespace Sample_Three_Tire.BAL.Interface
{
    public interface ILogin
    {
        DataSet ValidateUser(LoginUser loginUser);

    }
}
