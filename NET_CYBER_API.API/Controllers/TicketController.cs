using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NET_CYBER_API.DAL.Data;
using NET_CYBER_API.Domain.Models;
using NET_CYBER_API.BLL.Interfaces;

namespace NET_CYBER_API.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _service;

        public TicketController(ITicketService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Ticket>> GetAll()
        {
            return Ok(FakeDB.tickets);
        }

        [HttpGet("{id}")]
        
        public ActionResult<Ticket> GetById(int id)
        {
            Ticket? ticket = FakeDB.tickets.SingleOrDefault(t => t.Id == id);
            return ticket is not null ? Ok(ticket) : );
        }

        [HttpPost]
        public ActionResult Create(Ticket ticket)
        {
            FakeDB.tickets.Add(ticket);
            return Ok();
        }

        [HttpPut]

        public ActionResult Update(int id,Ticket ticket)
        {
            FakeDB.tickets[id-1] = ticket;
            return Ok();
        }

        [HttpDelete]
        [Route("toto/delete/{id}")]
        public ActionResult Delete(int id)
        {
            FakeDB.tickets.RemoveAt(id);
            return Ok();
        }

    }
}
