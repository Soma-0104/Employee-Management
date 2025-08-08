using EmployeeManagement.Core.IService;
using EmployeeManagement.Model;
using EmployeeManagement.Repository.DataUtilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        private readonly ITokenService tokenService;

        public EmployeeController(IEmployeeService _employeeService, ITokenService _tokenService)
        {
            employeeService = _employeeService;
            tokenService = _tokenService;
        }

        #region Employee Login
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(EmployeeLogin emp)
        {
            var employeeDetails = employeeService.Login(emp.email, emp.password);

            if (employeeDetails == null)
                return Unauthorized("Invalid Credentials");

            var token = tokenService.CreateToken(employeeDetails);

            return Ok(new { message = "Login Successfull", employeeDetails, token });
        }
        #endregion
        #region Employee Registration
        [AllowAnonymous]
        //[Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public IActionResult Register(Employees employees)
        {
            var existingEmployee = employeeService.CheckExistingEmployee(employees.Email);
            if (existingEmployee != null)
                return Conflict("Employee already Exists.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            employeeService.Register(employees);

            return Ok(new { message = "Registered Successfully" });
        }
        #endregion
        #region DeleteEmployee
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{employeeId}")]
        public IActionResult DeleteEmployee(long employeeId)
        {
            var checkExistance = employeeService.GetEmployeeById(employeeId);
            if (checkExistance == null)
                return BadRequest("Employee Not found");

            employeeService.DeleteEmployee(employeeId);

            return Ok(new { message = "Employee Deleted Successfully" });
        }
        #endregion

        #region GetEmployee by Id
        [Authorize(Roles = "Admin,User")]
        [HttpGet("getEmployee/{employeeId}")]
        public IActionResult GetEmployeeById(long employeeId)
        {
            var employeeDetails = employeeService.GetEmployeeById(employeeId);
            if (employeeDetails != null)
                return Ok(employeeDetails);

            return BadRequest("Employee Not found");
        }
        #endregion

        #region Get All Employees
        [Authorize(Roles = "Admin,User")]
        [HttpGet("allEmployees")]
        public IActionResult GetAllEmployees()
        {
            var employeeDetails = employeeService.GetAllEmployees();
            if (employeeDetails != null)
                return Ok(employeeDetails);
            return BadRequest("Employees Not found");
        }
        #endregion
        #region Update Employee
        [Authorize(Roles = "Admin")]
        [HttpPut("update/{employeeId}")]
        public IActionResult UpdateEmployee(long employeeId,UpdateEmployee employee)
        {
            var existingEmployee = GetEmployeeById(employeeId);
            if (existingEmployee == null)
                return BadRequest("Employee not found");
            employeeService.UpdateEmployee(employeeId,employee);
            return Ok(new {messsage="Updated Successfully"});
        }
        #endregion
    }
}
