using Microsoft.AspNetCore.Mvc;
using project_sem4_api.Context;
using project_sem4_api.DTOs;

namespace project_sem4_api.Controllers
{
    [ApiController]
    [Route("/api/v1/statusDish")]
    public class StatusDishController : Controller
    {
        private readonly DataContext dbContext;

        public StatusDishController(DataContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public IActionResult GetAllStatusDish()
        {
            List<StatusDishDTO> statusDishes = dbContext.StatusDishes
                .Select(dish => new StatusDishDTO()
                {
                    id = dish.Id,
                    name = dish.name
                })
                .ToList();
            return Ok(statusDishes);
        }
    }
}
