using Dapper;
using EmployeeManagement.Core.IRepository;
using EmployeeManagement.Model;
using EmployeeManagement.Repository.DataUtilities;
using Microsoft.Data.SqlClient;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EmployeeManagement.Repository
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly IDbConnection _connection;
        private readonly IConfiguration configuration;

        public EmployeeRepo(IConfiguration _configuration)
        {
            configuration = _configuration;
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public IEnumerable<Employees> GetAllEmployees()
        {
            return _connection.Query<Employees>(SQLQueryConstants.GetAllEmployeesQUery).ToList();
        }

        public Employees Login(string email, string password)
        {
            var employeeDetails = _connection.QueryFirstOrDefault<Employees>(
                SQLQueryConstants.EmployeeLoginQuery, new { Email = email }
            );

            string inputPassword = PasswordHasher.Hash(password);
            if (employeeDetails != null && inputPassword.Equals(employeeDetails.PasswordHash))
            {
                employeeDetails.SSN = DataEncryption.DecryptData(employeeDetails.SSN);
                return employeeDetails;
            }
            return null;
        }

        public void Register(Employees employee)
        {
            employee.PasswordHash = PasswordHasher.Hash(employee.PasswordHash);
            employee.SSN = DataEncryption.EncryptData(employee.SSN);
            _connection.Execute(SQLQueryConstants.AddEmployeeQuery, employee);
        }

        public Employees CheckExistingEmployee(string email)
        {
            var emp = _connection.QueryFirstOrDefault<Employees>(
                SQLQueryConstants.GetEmployeeByEmail, new { Email = email }
            );
            return emp;
        }

        public Employees GetEmployeeById(long employeeId)
        {
            var employeeDetails = _connection.QueryFirstOrDefault<Employees>(
                SQLQueryConstants.GetEmployeeById, new { EmployeeId = employeeId });
            if (employeeDetails != null)
            {
                return employeeDetails;
            }
            return null;
        }
        public void UpdateEmployee(UpdateEmployee employee)
        {
            _connection.Execute(SQLQueryConstants.UpdateEmployeeQuery, employee);
        }
        public void DeleteEmployee(long employeeId)
        {
            _connection.Execute(SQLQueryConstants.DeleteEmployeeQuery, new { EmployeeId = employeeId });
        }
    }
}
