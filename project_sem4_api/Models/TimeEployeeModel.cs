using System.ComponentModel.DataAnnotations;

namespace project_sem4_api.Models
{
    public class TimeEployeeModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string name { get; set; }
        [Required(ErrorMessage = "Time start is required.")]
        public string timeStart { get; set; }
        [Required(ErrorMessage = "Time end is required.")]
        public string timeEnd { get; set; }
    }
}
