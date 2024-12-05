using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace project_sem4_api.Models
{
    public class EvenModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string name { get; set; }
        [Required(ErrorMessage = "discount is required.")]
        public Decimal discount { get; set; }
        [Required(ErrorMessage = "Time start is required.")]
        public DateTime timeStart { get; set; }
        [Required(ErrorMessage = "Time end is required.")]
        public DateTime timeEnd { get; set; }
    }
}
