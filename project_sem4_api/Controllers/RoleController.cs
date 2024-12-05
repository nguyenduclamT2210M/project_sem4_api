using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_sem4_api.Context;
using project_sem4_api.DTOs;

namespace project_sem4_api.Controllers
{
    [ApiController]
    [Route("/api/v1/emp/roles")]
    //[Authorize]
    public class RoleController : Controller
    {
        private readonly DataContext dbContext;

        public RoleController(DataContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<RoleDTO> roles = dbContext.Roles
                .Select(role => new RoleDTO()
                {
                    id = role.id,
                    name = role.name
                })
                .ToList();
            return Ok(roles);
        }
    }
}
