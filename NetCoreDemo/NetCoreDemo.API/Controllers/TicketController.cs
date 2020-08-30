using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreDemo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreDemo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private TicketContext _context;
        public TicketController(TicketContext context)
        {
            _context = context;
            if (_context.TicketItems.Count() == 0)
            {
                var uuid = Guid.NewGuid().ToString();
                _context.TicketItems.Add(new TicketItem { Concert = "Beyonce", Artist = "汉克斯", Available = true });
                _context.TicketItems.Add(new TicketItem { Concert = "Rush", Artist = "Tom", Available = true });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public List<TicketItem> GetAll()
        {
            return _context.TicketItems.AsNoTracking().ToList();
        }

        [HttpGet("{id}", Name = "GetTicket")]
        public IActionResult GetById(long id)
        {
            var ticket = _context.TicketItems.FirstOrDefault(t => t.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }
            return new ObjectResult(ticket);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TicketItem ticket)
        {
            if (ticket == null)
            {
                return BadRequest();
            }

            _context.TicketItems.Add(ticket);
            _context.SaveChanges();

            return CreatedAtRoute("GetTicket", new { id = ticket.Id }, ticket);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody]TicketItem ticket)
        {
            if (ticket == null || ticket.Id != id)
            {
                return BadRequest("提供的数据有误");
            }
            var tic = _context.TicketItems.FirstOrDefault(t => t.Id == id);
            if (tic == null)
            {
                return NotFound();
            }

            tic.Concert = ticket.Concert;
            tic.Artist = ticket.Artist;
            tic.Available = ticket.Available;
            _context.TicketItems.Update(tic);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var tics = _context.TicketItems.FirstOrDefault(t => t.Id == id);
            if (tics == null)
            {
                return BadRequest();
            }
            _context.TicketItems.Remove(tics);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}