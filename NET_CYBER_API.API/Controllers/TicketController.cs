using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NET_CYBER_API.DAL.Data;
using NET_CYBER_API.Domain.Models;
using NET_CYBER_API.BLL.Interfaces;
using NET_CYBER_API.BLL.CustomExceptions;
using NET_CYBER_API.API.Tools.Errors;

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
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof( IEnumerable<Ticket> ))]
        public ActionResult<IEnumerable<Ticket>> GetAll() 
        { 
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Ticket))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorResponse))]
        public ActionResult<Ticket> Get([FromRoute] int id) {

            //Avant gestion erreurs custom
            //Ticket? ticket = _service.GetById(id);
            //if(ticket is null)
            //{
            //    return NotFound("Ticket not found");
            //}
            //return Ok(ticket);
            try
            {
                Ticket ticket = _service.GetById(id);
                return Ok(ticket);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorResponse(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (NotSingleException ex)
            {
                return Conflict(new ErrorResponse(StatusCodes.Status409Conflict, ex.Message)); 
                //Http Error 409 - Conflict - La requête ne peut être traitée à la suite d'un conflit avec l'état actuel du serveur. 
            }
            
        }
        
    
    }
}
