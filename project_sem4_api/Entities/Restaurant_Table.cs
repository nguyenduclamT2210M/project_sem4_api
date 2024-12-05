using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_sem4_api.Entities
{
    public class Restaurant_Table
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public int statusId { get; set; }
        [ForeignKey("statusId")]
        public StatusTable StatusTable { get; set; }
    }
}
