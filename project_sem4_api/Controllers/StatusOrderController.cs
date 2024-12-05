using Microsoft.AspNetCore.Mvc;
using project_sem4_api.Context;
using project_sem4_api.DTOs;

namespace project_sem4_api.Controllers
{
    [ApiController]
    [Route("/api/v1/statusOrder")]
    public class StatusOrderController : Controller
    {
        private readonly DataContext dbContext;

        public StatusOrderController(DataContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public IActionResult GetAllStatusOrder()
        {
            List<StatusOrderDTO> statusOrder = dbContext.StatusOrders
                .Select(order => new StatusOrderDTO()
                {
                    id = order.id,
                    name = order.name
                })
                .ToList();
            return Ok(statusOrder);
        }
    }
}
