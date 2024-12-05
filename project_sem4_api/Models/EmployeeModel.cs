using project_sem4_api.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_sem4_api.Models
{
    public class EmployeeModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string name { get; set; }
        [Required(ErrorMessage = "email is required.")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$", ErrorMessage = "Incorrect email.")]
        public string email { get; set; }
        [Required(ErrorMessage = "PassWord is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string password { get; set; }
        [Required(ErrorMessage = "Phone is required.")]
        public string phone { get; set; }
        [Required(ErrorMessage = "RoleId is required.")]
        public int roleId { get; set; }
        [Required(ErrorMessage = "Time eployee id is required.")]
        public int timeEmployeeId { get; set; }
        
    }
}
