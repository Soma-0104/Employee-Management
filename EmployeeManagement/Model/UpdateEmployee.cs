using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Model
{
    public class UpdateEmployee
    {
        public long EmployeeId { get; set; }

        [StringLength(40, ErrorMessage = "Name can't exceed 50 characters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name should only contain alphabets")]
        public string ?FullName { get; set; }

        [StringLength(50, ErrorMessage = "Email can't exceed 50 characters")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@(gmail\.com|yahoo\.com|outlook\.com)$", ErrorMessage = "Email should end with gmail.com,yahoo.com or outlook.com")]
        public string ?Email { get; set; }


        [RegularExpression(@"^(?!000|666|9\d{2})\d{3}-(?!00)\d{2}-(?!0000)\d{4}$",
            ErrorMessage = "Invalid SSN format.")]
        public string ?SSN { get; set; }

        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{};':"",.<>/?\\|]).{8,}$",
        ErrorMessage = "Password must include uppercase, lowercase, digit, and special character.")]
        [StringLength(16, ErrorMessage = "Password can't exceed 16 characters")]
        public string ?PasswordHash { get; set; }

        [StringLength(10, ErrorMessage = "Name can't exceed 10 characters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name should only contain alphabets")]
        public string ? Role { get; set; }

    }
}
