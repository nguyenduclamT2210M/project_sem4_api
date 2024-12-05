using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_sem4_api.Entities
{
    public class OrderItem
    {
        [Key]
        public int id { get; set; }
        public int orderId { get; set; }
        [ForeignKey("orderId")]
        public Order Order { get; set; }
        public int dishId { get; set; }
        [ForeignKey("dishId")]
        public Dish Dish { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }  
        public string? note { get; set; }
    }
}
