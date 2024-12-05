using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_sem4_api.Context;
using project_sem4_api.DTOs;
using project_sem4_api.Entities;
using project_sem4_api.Models;


namespace project_sem4_api.Controllers
{
    [ApiController]
    [Route("/api/v1/timeEmployee")]
    public class TimeEmployeeController : Controller
    {
        private readonly DataContext _context;
        public TimeEmployeeController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _context.TimeEmployees.ToListAsync();
            return Ok(employees);
        }



        // CREATE
        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Create( [FromBody] TimeEployeeModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var employee = new TimeEmployee
            {
                name = model.name,
                timeStart = model.timeStart,
                timeEnd = model.timeEnd
            };
            _context.TimeEmployees.Add(employee);
            await _context.SaveChangesAsync();
            return Ok(model);
        }

        // READ
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Get(int id)
        {
            var employee = await _context.TimeEmployees.FindAsync(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        // UPDATE
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Update(int id, [FromBody] TimeEployeeModel updatedModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingEmployee = await _context.TimeEmployees.FindAsync(id);
            if (existingEmployee == null)
                return NotFound();

            existingEmployee.name = updatedModel.name;
            existingEmployee.timeStart = updatedModel.timeStart;
            existingEmployee.timeEnd = updatedModel.timeEnd;

            await _context.SaveChangesAsync();
            return Ok(existingEmployee);
        }

        // DELETE
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.TimeEmployees.FindAsync(id);
            if (employee == null)
                return NotFound();

            _context.TimeEmployees.Remove(employee);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
