using project_sem4_api.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_sem4_api.DTOs
{
    public class OrderDTO
    {
        
            public int id { get; set; }
            public string billNumber { get; set; }
            public int tableId { get; set; }
            public string tableName { get; set; } // Tên bàn

            public int paymentId { get; set; }
            public string paymentMethodName { get; set; } // Tên phương thức thanh toán

            public int? eventId { get; set; }
            public string? eventName { get; set; } // Tên sự kiện
            public Decimal? eventDiscount { get; set; }
            public int orderTypeId { get; set; }
            public string orderTypeName { get; set; } // Loại đơn hàng
        

            public int statusId { get; set; }
            public string statusName { get; set; } // Trạng thái

            public int employeeId { get; set; }
            public string employeeName { get; set; } // Tên nhân viên

            public decimal originalPrice { get; set; }
            public decimal totalDiscount { get; set; }
            public decimal totalPrice { get; set; }

            public List<OrderDetailDTO> foods { get; set; } // Danh sách món ăn
        }

    
}
