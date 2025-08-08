using EmployeeManagement.Core.IRepository;
using EmployeeManagement.Core.IService;
using EmployeeManagement.Model;
using EmployeeManagement.Repository.DataUtilities;

namespace EmployeeManagement.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepo employeeRepo;
        public EmployeeService(IEmployeeRepo _employeeRepo)
        {
            employeeRepo = _employeeRepo;
        }

        public IEnumerable<Employees> GetAllEmployees()
        {
            return employeeRepo.GetAllEmployees();  
        }

        public Employees Login(string email,string password)
        {
            return employeeRepo.Login(email,password);
        }

        public void Register(Employees employee)
        {
            employeeRepo.Register(employee);
        }
        public Employees CheckExistingEmployee(string email)
        {
            return employeeRepo.CheckExistingEmployee(email);
        }
        
        public void DeleteEmployee(long employeeId)
        {
            employeeRepo.DeleteEmployee(employeeId);
        }
        public Employees GetEmployeeById(long employeeId)
        {
            return employeeRepo.GetEmployeeById(employeeId);
        }
        public void UpdateEmployee(long employeeId,UpdateEmployee employee)
        {
            var existingEmployee = GetEmployeeById(employeeId);
            employee.EmployeeId = existingEmployee.EmployeeId;
            employee.FullName ??= existingEmployee.FullName;
            employee.Email ??= existingEmployee.Email;
            if (employee.PasswordHash == null)
            {
                employee.PasswordHash = existingEmployee.PasswordHash;
            }
            else
            {
                employee.PasswordHash = PasswordHasher.Hash(employee.PasswordHash);
            }
            if (employee.SSN != null)
            {
                employee.SSN = DataEncryption.EncryptData(employee.SSN);
            }
            else
            {
                employee.SSN = existingEmployee.SSN;
            }
            employee.Role ??= existingEmployee.Role;
            employeeRepo.UpdateEmployee(employee);
        }
    }
}
