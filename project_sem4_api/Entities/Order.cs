using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_sem4_api.Entities
{
    public class Order
    {
        [Key]
        public int id { get; set; }
        public string billNumber { get; set; }
        public int tableId { get; set; }
        [ForeignKey("tableId")]
        public Restaurant_Table Restaurant_Table { get; set; }
        public int paymentId { get; set; }
        [ForeignKey("paymentId")]
        public PaymentMethord PaymentMethord { get; set; }
        public int? eventId { get; set; }
        [ForeignKey("eventId")]
        public Event Event { get; set; }
        public int orderTypeId { get; set; }
        [ForeignKey("orderTypeId")]
        public OrderType OrderType { get; set; }
        public int statusId { get; set; }
        [ForeignKey("statusId")]
        public StatusOrder StatusOrder { get; set; }
        public int employeeId { get; set; }
        [ForeignKey("employeeId")]
        public Employee Employee { get; set; }
        public decimal originalPrice { get; set; }
        public decimal totalDiscount { get; set; }
        public decimal totalPrice { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
