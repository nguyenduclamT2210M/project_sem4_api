using Microsoft.AspNetCore.Mvc;
using project_sem4_api.Context;
using project_sem4_api.DTOs;

namespace project_sem4_api.Controllers
{
    [ApiController]
    [Route("/api/v1/orderType")]
    public class OrderTypeController : Controller
    {
        private readonly DataContext dbContext;

        public OrderTypeController(DataContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public IActionResult GetAllOrderType()
        {
            List<OrderTypeDTO> orderTypes = dbContext.OrderTypes
                .Select(type => new OrderTypeDTO()
                {
                    id = type.id,
                    name = type.name
                })
                .ToList();
            return Ok(orderTypes);
        }
    }
}
