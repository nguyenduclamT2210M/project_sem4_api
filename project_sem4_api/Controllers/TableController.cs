using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_sem4_api.Context;
using project_sem4_api.Entities;
using project_sem4_api.Models;

namespace project_sem4_api.Controllers
{
    [ApiController]
    [Route("/api/v1/table")]
    public class TableController : Controller
    {
        private readonly DataContext _context;
        public TableController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Restaurant_Tables
                .Include(x => x.StatusTable) // Bao gồm bảng StatusTable
                .ToListAsync();

            return Ok(result);
        }

        [HttpGet("search/{statusName}")]
        public async Task<IActionResult> SearchByStatusName(string statusName)
        {
            var result = await _context.Restaurant_Tables
                .Include(x => x.StatusTable) // Bao gồm bảng StatusTable
                .Where(x => x.StatusTable.name.Contains(statusName)) // Điều kiện tìm kiếm theo tên
                .ToListAsync();

            return Ok(result);
        }



        // CREATE
        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Create( TableModel model,int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            StatusTable statusTable = _context.StatusTables.Find(model.statusId);
            if(statusTable == null)
            {
                return BadRequest("Status Id not fount");
            }
            var table = new Restaurant_Table
            {
                name = model.name,
               statusId = model.statusId,
            };
            _context.Restaurant_Tables.Add(table);
            await _context.SaveChangesAsync();
            return Ok(model);
        }

        // READ
        [HttpGet("{id}")]
       
        public async Task<IActionResult> Get(int id)
        {
            var employee = await _context.Restaurant_Tables.FindAsync(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        // UPDATE
        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,  TableModel updatedModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updateTable = await _context.Restaurant_Tables.FindAsync(id);
            if (updateTable == null)
                return NotFound();

            updateTable.name = updatedModel.name;
            updateTable.statusId = updatedModel.statusId;

            await _context.SaveChangesAsync();
            return Ok(updateTable);
        }

        // DELETE
        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.Restaurant_Tables.FindAsync(id);
            if (employee == null)
                return NotFound();

            _context.Restaurant_Tables.Remove(employee);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
