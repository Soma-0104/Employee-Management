namespace EmployeeManagement.Repository.DataUtilities
{
    public class SQLQueryConstants
    {
        public static string AddEmployeeQuery = @"
            Insert into Employees (FullName,Email,SSN,PasswordHash,Role)
            Values(@FullName,@Email,@SSN,@PasswordHash,@Role)";

        public static string EmployeeLoginQuery = @"
            Select EmployeeId,FullName,Email,SSN,Role,PasswordHash from Employees where Email=@Email and Is_Deleted=0";
        
        public static string GetAllEmployeesQUery = @"
            Select EmployeeId,FullName,Email,SSN,Role,PasswordHash,Created_Time_Stamp,Updated_Time_Stamp,
            Role from Employees where Is_Deleted=0";
        
        public static string GetEmployeeByEmail = @"
            Select FullName from Employees Where Email=@Email and Is_Deleted=0";
        
        public static string GetEmployeeById = @"
            Select EmployeeId,FullName,Email,SSN,Role,PasswordHash,Created_Time_Stamp,Updated_Time_Stamp,
            Role from Employees where EmployeeID = @EmployeeId and Is_Deleted=0";
        
        public static string DeleteEmployeeQuery = @"
            Update Employees 
            SET Is_Deleted=1,
            Updated_Time_Stamp = GETDATE() 
            where EmployeeId = @EmployeeId and Is_Deleted=0";
        
        public const string UpdateEmployeeQuery = @"
            UPDATE Employees
            SET 
            FullName = @FullName,
            Email = @Email,
            PasswordHash=@PasswordHash,
            Role = @Role,
            SSN = @SSN,
            Updated_Time_Stamp = GETDATE()
            WHERE EmployeeId = @EmployeeId and Is_Deleted=0";
    }
}
