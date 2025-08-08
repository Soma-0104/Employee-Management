using EmployeeManagement.Model;

namespace EmployeeManagement.Core.IRepository
{
    public interface ITokenRepo
    {
        string CreateToken(Employees employee);
    }
}
