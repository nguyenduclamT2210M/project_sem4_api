using System.ComponentModel.DataAnnotations;

namespace project_sem4_api.Entities
{
    public class Role
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
