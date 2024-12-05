using Microsoft.AspNetCore.Mvc;
using project_sem4_api.Context;
using project_sem4_api.DTOs;

namespace project_sem4_api.Controllers
{
    [ApiController]
    [Route("/api/v1/statusNotification")]
    public class StatusNotifacationController : Controller
    {
        private readonly DataContext dbContext;

        public StatusNotifacationController(DataContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public IActionResult GetAllStatusNotification()
        {
            List<StatusNotifacationDTO> statusNotifacations = dbContext.StatusNotifacations
                .Select(notification => new StatusNotifacationDTO()
                {
                    id = notification.id,
                    name = notification.name
                })
                .ToList();
            return Ok(statusNotifacations);
        }
    }
}
