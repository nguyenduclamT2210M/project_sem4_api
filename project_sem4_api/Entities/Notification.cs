using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_sem4_api.Entities
{
    public class Notification
    {
        [Key]
        public int id { get; set; }
        public string message { get; set; }
        public int? statusId { get; set; }
        [ForeignKey("statusId")]
        public StatusNotifacation StatusNotifacation { get; set; }  
        public int tableId { get; set; }
        [ForeignKey("tableId")]
        public Restaurant_Table Restaurant_Table { get; set; }
    }
}
