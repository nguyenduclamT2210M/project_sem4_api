using System.ComponentModel.DataAnnotations;

namespace project_sem4_api.Entities
{
    public class StatusDish
    {
        [Key]
        public int Id { get; set; }
        public string name {  get; set; }
    }
}
