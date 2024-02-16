using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NET_CYBER_API.DAL.Data;
using NET_CYBER_API.Domain.Models;
using NET_CYBER_API.BLL.Interfaces;
using NET_CYBER_API.BLL.CustomExceptions;
using NET_CYBER_API.API.Tools.Errors;
using NET_CYBER_API.API.DTOs;
using NET_CYBER_API.API.Mappers;

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

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Ticket))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Ticket> Insert([FromBody] TicketDataDTO ticket)
        {
            Ticket ticketCreated = _service.Create(ticket.DTOToDomain());
            //CreatedAtAction, va rajouter dans location (dans les headers) la requête à faire pour avoir accès la ressource qui vient d'être créée
            //Donc on doit lui renseigner: 
                // en premier param, la méthode à appeler (ici notre Get avec Id)
                // en deuxième param, le(s) paramètre(s) dont à a besoin la méthode du premier param,
                // en troisième param, la ressource à mettre en réponse dans le json
            return CreatedAtAction(nameof(Get), new { id = ticketCreated.Id }, ticketCreated);
        }
        
    
    }
}
