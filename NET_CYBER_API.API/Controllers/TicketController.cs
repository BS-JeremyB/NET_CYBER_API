using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NET_CYBER_API.DAL.Data;
using NET_CYBER_API.Domain.Models;
using NET_CYBER_API.BLL.Interfaces;
using NET_CYBER_API.BLL.CustomExceptions;
using NET_CYBER_API.API.Tools.Errors;
using NET_CYBER_API.API.DTOs;
using NET_CYBER_API.API.Mappers;
using System.Net.Sockets;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using NET_CYBER_API.Domain.Enums;

namespace NET_CYBER_API.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _service;
        private readonly IUtilisateurService _userService;

        public TicketController(ITicketService service, IUtilisateurService userService)
        {
            _service = service;
            _userService = userService;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof( IEnumerable<TicketInfoDTO> ))]
        public IActionResult GetAll() 
        { 
            return Ok(_service.GetAll().DomainToInfoDTO());
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TicketInfoDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public ActionResult<Ticket> Get([FromRoute] int id) {

            try
            {
                Ticket ticket = _service.GetById(id);
                return Ok(ticket.DomainToInfoDTO());
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorResponse(StatusCodes.Status404NotFound, ex.Message));
            }
            
        }

        [HttpPost]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TicketInfoDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TicketInfoDTO> Insert([FromBody] TicketDataDTO ticket)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Claim? emailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

            Utilisateur? utilisateur = _userService.GetByEmail(emailClaim.Value);

            Ticket ticketToAdd = ticket.DTOToDomain();

            if(utilisateur is not null)
            {
                ticketToAdd.Auteur = utilisateur;
                TicketInfoDTO ticketCreated = _service.Create(ticketToAdd).DomainToInfoDTO();

                return CreatedAtAction(nameof(Get), new { id = ticketCreated.Id }, ticketCreated);
            }

            return BadRequest();
   
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]

        public ActionResult Delete([FromRoute] int id)
        {
            try
            {
                //if (!User.IsInRole("Admin"))
                //{
                //    return Forbid("Vous n'avez pas les accès");
                //}
                _service.Delete(id);
                return NoContent();
            }
            catch(NotFoundException ex)
            {
                return NotFound(new ErrorResponse(code : StatusCodes.Status404NotFound, message : ex.Message));

            }
                
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Ticket))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Ticket> Update([FromRoute] int id, [FromBody] TicketDataDTO ticket)
        {
            try
            {
                Ticket ticketToUpdate = ticket.DTOToDomain();
                ticketToUpdate.Id = id;

                Claim? idClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                int userId = int.Parse(idClaim.Value);

                Ticket ticketUpdated = _service.Update(userId, ticketToUpdate);

                return Ok(ticketUpdated.DomainToInfoDTO()); 

            }
            catch(NotFoundException ex)
            {
                return NotFound(new ErrorResponse(code: StatusCodes.Status404NotFound, message: ex.Message));

            }
            catch (NotAuthorizedException ex)
            {
                return Unauthorized(new ErrorResponse(code: StatusCodes.Status401Unauthorized, message: ex.Message));
            }
        }

      
        [HttpPut("Complete/{id}")]
        [Authorize(Roles = "Admin, Technicien")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Ticket))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public ActionResult<Ticket> Complete([FromRoute] int id)
        {
            try
            {
                Ticket ticketCompleted = _service.Complete(id);
                return Ok(ticketCompleted.DomainToInfoDTO());

            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorResponse(code: StatusCodes.Status404NotFound, message: ex.Message));

            }

        }
    }
}
