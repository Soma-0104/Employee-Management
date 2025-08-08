using EmployeeManagement.Model;

namespace EmployeeManagement.Core.IRepository
{
    public interface IEmployeeRepo
    {
        void Register(Employees employee);
        Employees Login(string email, string password);
        IEnumerable<Employees> GetAllEmployees();
        Employees CheckExistingEmployee(string email);
        void DeleteEmployee(long employeeId);
        Employees GetEmployeeById(long employeeId);
        void UpdateEmployee(UpdateEmployee employee);
    }
}
