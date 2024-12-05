using Microsoft.AspNetCore.Mvc;
using project_sem4_api.Context;
using project_sem4_api.DTOs;

namespace project_sem4_api.Controllers
{
    [ApiController]
    [Route("/api/v1/statusTable")]
    public class StatusTableController : Controller
    {
        private readonly DataContext dbContext;

        public StatusTableController(DataContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public IActionResult GetAllStatusTable()
        {
            List<StatusTableDTO> statusTables = dbContext.StatusTables
                .Select(tables => new StatusTableDTO()
                {
                    id = tables.id,
                    name = tables.name
                })
                .ToList();
            return Ok(statusTables);
        }
    }
}
