using EmployeeManagement.Model;

namespace EmployeeManagement.Core.IService
{
    public interface IEmployeeService
    {
        void Register(Employees employee);
        Employees Login(string email, string password);
        IEnumerable<Employees> GetAllEmployees();
        Employees CheckExistingEmployee(string email);
        void DeleteEmployee(long employeeID);
        Employees GetEmployeeById(long employeeId);
        void UpdateEmployee(long employeeId,UpdateEmployee employee);


    }
}
