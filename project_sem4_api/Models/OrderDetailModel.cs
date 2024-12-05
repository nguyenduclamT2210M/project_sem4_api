using System.ComponentModel.DataAnnotations;

namespace project_sem4_api.Models
{
    public class OrderDetailModel
    {
       
        [Required(ErrorMessage = "Dish id is required.")]
        public int dishId { get; set; }
        [Required(ErrorMessage = "Quantity is required.")]
        public int quantity { get; set; }
        [Required(ErrorMessage = "Price is required.")]
        public decimal price { get; set; }
       
        public string? note { get; set; }
    }
}
