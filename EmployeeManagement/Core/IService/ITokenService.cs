using EmployeeManagement.Model;

namespace EmployeeManagement.Core.IService
{
    public interface ITokenService
    {
        string CreateToken(Employees employee);
    }
}
