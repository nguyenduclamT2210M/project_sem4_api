using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_sem4_api.Context;
using project_sem4_api.DTOs;
using project_sem4_api.Entities;
using project_sem4_api.Models;

namespace project_sem4_api.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : Controller
    {
        private readonly DataContext _context;
        public OrderController(DataContext context)
        {
            _context = context;
        }


        [HttpPost]
        [Route("create")]
        public IActionResult CreateOrder( OrderModel createOModel)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        // Tạo số hóa đơn tiếp theo
                        string lastBillNum = _context.Orders
                            .OrderByDescending(order => order.id)
                            .Select(order => order.billNumber)
                            .FirstOrDefault();

                        string nextBillNum;
                        if (string.IsNullOrEmpty(lastBillNum))
                        {
                            nextBillNum = "B00000001";
                        }
                        else
                        {
                            string numberPart = lastBillNum.Substring(1); // Bỏ ký tự 'B'
                            int lastNumber = int.Parse(numberPart);
                            nextBillNum = "B" + (++lastNumber).ToString("D8");
                        }

                        // Tạo đơn hàng mới
                        Order newOrder = new Order
                        {
                            billNumber = nextBillNum,
                            tableId = createOModel.tableId,
                            paymentId = createOModel.paymentId,
                            eventId = createOModel.eventId,
                            orderTypeId = createOModel.orderTypeId,
                            statusId = createOModel.statusId,
                            employeeId = createOModel.employeeId,
                            originalPrice = createOModel.originalPrice,
                            totalDiscount = createOModel.totalDiscount,
                            totalPrice = createOModel.totalPrice
                        };
                        // Thêm đơn hàng vào cơ sở dữ liệu
                        _context.Orders.Add(newOrder);
                        _context.SaveChanges();  // Lưu đơn hàng trước khi lấy ID
                        
                        // Thêm các món vào chi tiết đơn hàng
                        // Kiểm tra xem có món ăn nào trong Details của đơn hàng không
                        if (createOModel.Details != null && createOModel.Details.Any())
                        {
                            // Nếu có món ăn, tạo các OrderItem và thêm vào cơ sở dữ liệu
                            foreach (var detail in createOModel.Details)
                            {
                                OrderItem newOrderDetail = new OrderItem
                                {
                                   orderId = newOrder.id,
                                    dishId = detail.dishId,
                                    quantity = detail.quantity,
                                    note = detail.note,
                                    price = detail.price
                                };

                                _context.OrderItems.Add(newOrderDetail);
                            }

                            _context.SaveChanges(); // Lưu các thay đổi vào cơ sở dữ liệu
                        }
                        else
                        {
                            // Nếu không có món ăn nào, trả về lỗi 400
                            return StatusCode(400, "No items added to the order.");
                        }

                        // Cam kết giao dịch (transaction)
                        transaction.Commit();



                        return Ok(new
                        {
                            Message = "Order created successfully!",
                            BillNumber = nextBillNum,
                            
                        });
                    }
                    catch (Exception ex)
                    {
                        // Nếu có lỗi, rollback giao dịch
                        transaction.Rollback();
                        return StatusCode(500, $"Error creating order: {ex.Message}");
                    }
                }
            }

            return BadRequest("Invalid order data!");
        }



        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = _context.Orders
               
                .FirstOrDefault(o => o.id == id);

            if (order == null)
                return NotFound("Order not found.");

            // Xóa các OrderItems liên quan
            var orderItems = _context.OrderItems.Where(oi => oi.orderId == id).ToList();
            _context.OrderItems.RemoveRange(orderItems);

            // Xóa Order
            _context.Orders.Remove(order);
            _context.SaveChanges();

            return Ok("Order deleted successfully.");
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _context.Orders
                .Include(o => o.Restaurant_Table)
                .Include(o => o.PaymentMethord)
                .Include(o => o.Event)
                .Include(o => o.OrderType)
                .Include(o => o.StatusOrder)
                .Include(o => o.Employee)
                .Include(o => o.OrderItems)
                .Select(o => new OrderDTO
                {
                    id = o.id,
                    billNumber = o.billNumber,
                    tableId = o.tableId,
                    tableName = o.Restaurant_Table.name,
                    paymentId = o.paymentId,
                    paymentMethodName = o.PaymentMethord.name,
                    eventId = o.eventId,
                    eventName = o.Event.name,
                    eventDiscount = o.Event.discount,
                    orderTypeId = o.orderTypeId,
                    orderTypeName = o.OrderType.name,
                    statusId = o.statusId,
                    statusName = o.StatusOrder.name,
                    employeeId = o.employeeId,
                    employeeName = o.Employee.name,
                    originalPrice = o.originalPrice,
                    totalDiscount = o.totalDiscount,
                    totalPrice = o.totalPrice,
                    foods = o.OrderItems.Select(oi => new OrderDetailDTO
                    {
                        dishId = oi.dishId,
                        note = oi.note,
                        quantity = oi.quantity,
                        price = oi.price
                    }).ToList()
                })
                .ToListAsync();

            return Ok(orders);
        }

        // GET: api/Orders/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Restaurant_Table)
                .Include(o => o.PaymentMethord)
                .Include(o => o.Event)
                .Include(o => o.OrderType)
                .Include(o => o.StatusOrder)
                .Include(o => o.Employee)
                .Include(o => o.OrderItems)
                .Select(o => new OrderDTO
                {
                    id = o.id,
                    billNumber = o.billNumber,
                    tableId = o.tableId,
                    tableName = o.Restaurant_Table.name,
                    paymentId = o.paymentId,
                    paymentMethodName = o.PaymentMethord.name,
                    eventId = o.eventId,
                    eventName = o.Event.name,
                    eventDiscount = o.Event.discount,
                    orderTypeId = o.orderTypeId,
                    orderTypeName = o.OrderType.name,
                    statusId = o.statusId,
                    statusName = o.StatusOrder.name,
                    employeeId = o.employeeId,
                    employeeName = o.Employee.name,
                    originalPrice = o.originalPrice,
                    totalDiscount = o.totalDiscount,
                    totalPrice = o.totalPrice,
                    foods = o.OrderItems.Select(oi => new OrderDetailDTO
                    {
                        dishId = oi.dishId,
                        note = oi.note,
                        quantity = oi.quantity,
                        price = oi.price

                    }).ToList()
                })
                .FirstOrDefaultAsync(o => o.id == id);

            if (order == null)
            {
                return NotFound(new { message = "Order not found" });
            }

            return Ok(order);
        }

        // GET: api/Orders/bill/{billNumber}
        [HttpGet("bill/{billNumber}")]
        public async Task<IActionResult> GetByBillNumber(string billNumber)
        {
            var order = await _context.Orders
                .Include(o => o.Restaurant_Table)
                .Include(o => o.PaymentMethord)
                .Include(o => o.Event)
                .Include(o => o.OrderType)
                .Include(o => o.StatusOrder)
                .Include(o => o.Employee)
                .Include(o => o.OrderItems)
                .Select(o => new OrderDTO
                {
                    id = o.id,
                    billNumber = o.billNumber,
                    tableId = o.tableId,
                    tableName = o.Restaurant_Table.name,
                    paymentId = o.paymentId,
                    paymentMethodName = o.PaymentMethord.name,
                    eventId = o.eventId,
                    eventName = o.Event.name,
                    eventDiscount = o.Event.discount,
                    orderTypeId = o.orderTypeId,
                    orderTypeName = o.OrderType.name,
                    statusId = o.statusId,
                    statusName = o.StatusOrder.name,
                    employeeId = o.employeeId,
                    employeeName = o.Employee.name,
                    originalPrice = o.originalPrice,
                    totalDiscount = o.totalDiscount,
                    totalPrice = o.totalPrice,
                    foods = o.OrderItems.Select(oi => new OrderDetailDTO
                    {
                        dishId = oi.dishId,
                        note = oi.note,
                        quantity = oi.quantity,
                        price = oi.price
                    }).ToList()
                })
                .FirstOrDefaultAsync(o => o.billNumber == billNumber);

            if (order == null)
            {
                return NotFound(new { message = "Order not found" });
            }

            return Ok(order);
        }

    }
}
