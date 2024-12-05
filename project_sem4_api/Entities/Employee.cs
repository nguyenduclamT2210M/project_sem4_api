using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_sem4_api.Entities
{
    public class Employee
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public int roleId { get; set; }
        [ForeignKey("roleId")]
        public Role Role { get; set; }
        public int timeEmployeeId { get; set; }
        [ForeignKey("timeEmployeeId")]
        public TimeEmployee TimeEmployee { get; set; }
    }
}
