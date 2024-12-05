using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_sem4_api.Context;
using project_sem4_api.DTOs;
using project_sem4_api.Entities;
using project_sem4_api.Models;
using System.Reflection.Metadata.Ecma335;

namespace project_sem4_api.Controllers
{
    [ApiController]
    [Route("api/v1/event")]
    public class EvenController : Controller
    {
        private readonly DataContext _context;
        public EvenController(DataContext context)
        {
            _context = context;
        }


        [HttpGet("getAll")]
        public IActionResult GetAll() {
            List<EvenDTO> evenDTOs = _context.Events
               .Select(e => new EvenDTO
               {
                   id = e.id,
                   name = e.name,
                   discount = e.discount,
                   dayStart = e.dayStart.ToString("dd/MM/yyyy"),
                   dayEnd = e.dayEnd.ToString("dd/MM/yyyy")
               })
               .ToList();
            return Ok(evenDTOs);
        }
        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult CreateEvent(EvenModel eModel) {
            if (ModelState.IsValid)
            {
                try
                {
                    Event newE = new Event()
                    {
                        name = eModel.name,
                        discount= eModel.discount,
                        dayStart = eModel.timeStart,
                        dayEnd = eModel.timeEnd
                    };
                    _context.Events.Add(newE);
                    _context.SaveChanges();
                    return Ok("Add new event succsess!");
                }
                catch (Exception ex) {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest("Add new error!");
        }
        [HttpGet("search/byname")]
        public IActionResult SearchEventsByName(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest("Search term is required.");
            }

            var events = _context.Events
                .Where(e => e.name.Contains(searchTerm))
                .Select(e => new EvenDTO
                {
                    id = e.id,
                    name = e.name,
                    dayStart = e.dayStart.ToString("dd/MM/yyyy"),
                    dayEnd = e.dayEnd.ToString("dd/MM/yyyy"),
                    discount = e.discount
                })
                .ToList();

            return Ok(events);
        }

       
        [HttpGet("search/discount")]
        public IActionResult SearchByDiscount(int discount)
        {
            var events = _context.Events
                .Where(e => e.discount == discount)
                .Select(e => new EvenDTO
                {
                    id = e.id,
                    name = e.name,
                    dayStart = e.dayStart.ToString("dd/MM/yyyy"),
                    dayEnd = e.dayEnd.ToString("dd/MM/yyyy"),
                    discount = e.discount
                })
                .ToList();

            return Ok(events);
        }
        [HttpGet("search/bymonth")]
        public IActionResult SearchEventsByMonth(int month, int year)
        {
            if (month < 1 || month > 12)
            {
                return BadRequest("Invalid month value!");
            }

            var events = _context.Events
                .Where(e => e.dayStart.Month == month && e.dayStart.Year == year)
                .Select(e => new EvenDTO
                {
                    id = e.id,
                    name = e.name,
                    discount = e.discount,
                    dayStart = e.dayStart.ToString("dd/MM/yyyy"),
                    dayEnd = e.dayEnd.ToString("dd/MM/yyyy")
                })
                .ToList();

            return Ok(events);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("{id}")]
        public IActionResult EditEvent(int id, [FromForm] Event updateEvent) {
            var existingEvent = _context.Events.Find(id);
            if (existingEvent == null)
            {
                return NotFound("Event not found.");
            }
            existingEvent.name = updateEvent.name;
            existingEvent.discount = updateEvent.discount;
            existingEvent.dayStart = updateEvent.dayStart;
            existingEvent.dayEnd = updateEvent.dayEnd;
            _context.SaveChanges();
            return Ok("Update event succsess!");
        }
        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id) { 
            var    eventId = _context.Events.Find(id);
            if(eventId == null)
            {
                return NotFound("Event not found!");
            }

            _context.Events.Remove(eventId);
            _context.SaveChanges();
            return Ok("Delete event succsess!");
        }
    }
}
