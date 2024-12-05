using System.ComponentModel.DataAnnotations;

namespace project_sem4_api.Models
{
    public class NotifacationModel
    {

        [Required(ErrorMessage = "Message is required.")]
        public string message { get; set; }

        [Required(ErrorMessage = "StatusId is required.")]
        public int statusId { get; set; }

        [Required(ErrorMessage = "TableId is required.")]
        public int tableId { get; set; }
    }
}
