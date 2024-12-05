using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_sem4_api.Context;
using project_sem4_api.Entities;
using project_sem4_api.Models;

namespace project_sem4_api.Controllers
{
    [ApiController]
    [Route("/api/v1/notification")]
    public class NotificationController : Controller
    {
        private readonly DataContext _context;
        public NotificationController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Notifications
                .Include(x => x.StatusNotifacation)
                .Include(x => x.Restaurant_Table)
                    .ThenInclude(rt => rt.StatusTable) // Bao gồm cả dữ liệu từ bảng StatusTable
                .ToListAsync();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(NotifacationModel model, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            StatusNotifacation statusNotifacation = _context.StatusNotifacations.Find(model.statusId);
            if (statusNotifacation == null)
            {
                return BadRequest("Status notification id not fount.");
            }

            Restaurant_Table  table = _context.Restaurant_Tables.Find(model.tableId);
            if(table == null)
            {
                return BadRequest("Table id not fount.");
            }
            var notification = new Notification
            {
                message = model.message,
                statusId = model.statusId,
                tableId = model.tableId
            };
           
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
            return Ok(model);
        }


    }
}
