using project_sem4_api.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_sem4_api.Models
{
    public class OrderModel
    {
        [Required(ErrorMessage = "Bill number is required.")]
        public  string billNumber { get; set; }
        [Required(ErrorMessage = "TableId is required.")]
        public int tableId { get; set; }
        [Required(ErrorMessage = "Payment id is required.")]
        public int paymentId { get; set; }
        
        public int? eventId { get; set; }
        [Required(ErrorMessage = "Order typr is required.")]
        public int orderTypeId { get; set; }
        [Required(ErrorMessage = "Status Id is required.")]
        public int statusId { get; set; }
        [Required(ErrorMessage = "Employee Id is required.")]
        public int employeeId { get; set; }
        [Required(ErrorMessage = "OriginalPrice is required.")]
        public decimal originalPrice { get; set; }
        public decimal totalDiscount { get; set; }
        [Required(ErrorMessage = "TotalPrice is required.")]
        public decimal totalPrice { get; set; }
        [Required(ErrorMessage = "Chi tiết đơn hàng là bắt buộc.")]
        public List<OrderDetailModel> Details { get; set; }
    }
}
