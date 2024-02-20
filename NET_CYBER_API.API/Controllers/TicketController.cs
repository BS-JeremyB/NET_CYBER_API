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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof( IEnumerable<Ticket> ))]
        public ActionResult<IEnumerable<Ticket>> GetAll() 
        { 
            return Ok(_service.GetAll().ticketInfoDTOs());
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
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TicketInfoDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TicketInfoDTO> Insert([FromBody] TicketDataDTO ticket)
        {
            var emailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            Utilisateur? utilisateur = _userService.GetByEmail(emailClaim.Value);
            Ticket ticketToAdd = ticket.DTOToDomain();
            if(utilisateur is not null)
            {
                ticketToAdd.Auteur = utilisateur;
                TicketInfoDTO ticketCreated = _service.Create(ticketToAdd).DomainToInfoDTO();

                return CreatedAtAction(nameof(Get), new { id = ticketCreated.Id }, ticketCreated);
            }

            return BadRequest();


            //CreatedAtAction, va rajouter dans location (dans les headers) la requête à faire pour avoir accès la ressource qui vient d'être créée
            //Donc on doit lui renseigner: 
                // en premier param, la méthode à appeler (ici notre Get avec Id)
                // en deuxième param, le(s) paramètre(s) dont à a besoin la méthode du premier param,
                // en troisième param, la ressource à mettre en réponse dans le json

            //from connected user with jwt get the email

            
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorResponse))]

        public ActionResult Delete([FromRoute] int id)
        {
            try
            {
                _service.Delete(id);
                return NoContent();
            }
            catch(NotFoundException ex)
            {
                return NotFound(new ErrorResponse(code : StatusCodes.Status404NotFound, message : ex.Message));

            }
            catch(NotSingleException ex)
            {
                return Conflict(new ErrorResponse(code: StatusCodes.Status409Conflict, message: ex.Message));
            }
                
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Ticket))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Ticket> Update([FromRoute] int id, [FromBody] TicketDataDTO ticket)
        {
            try
            {
                Ticket ticketToUpdate = ticket.DTOToDomain();
                // l'Id du ticket après mapping vaut 0, il faut qu'on envoie l'id reçu en route puisque notre Update du service attend juste un ticket et non id + ticket
                ticketToUpdate.Id = id;
                Claim? idClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                int userId = int.Parse(idClaim.Value);
                Ticket ticketUpdated = _service.Update(userId, ticketToUpdate);
                return Ok(ticketUpdated.DomainToInfoDTO()); 
                // return NoContent(); //On renvoie juste un code de  succès sans fournir l'objet modifié //Les deux sont bonnes
            }
            catch(NotFoundException ex)
            {
                return NotFound(new ErrorResponse(code: StatusCodes.Status404NotFound, message: ex.Message));

            }
            catch(NotSingleException ex)
            {
                return Conflict(new ErrorResponse(code: StatusCodes.Status409Conflict, message: ex.Message));
            }
            catch (NotAuthorizedException ex)
            {
                return Conflict(new ErrorResponse(code: StatusCodes.Status403Forbidden, message: ex.Message));
            }
        }

        //[HttpPatch("{id}")] 
        // Ou 
        //Attention on a déjà un PUT, on doit donc rajouter un segment dans la route pour avoir deux différents
        [HttpPut("Complete/{id}")]
        [Authorize(Roles = "Admin, Technicien")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Ticket))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorResponse))]
        public ActionResult<Ticket> Complete([FromRoute] int id)
        {
            try
            {
                Ticket ticketCompleted = _service.Complete(id);
                return Ok(ticketCompleted);
                // return NoContent(); //On renvoie juste un code de  succès sans fournir l'objet modifié //Les deux sont bonnes
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorResponse(code: StatusCodes.Status404NotFound, message: ex.Message));

            }
            catch (NotSingleException ex)
            {
                return Conflict(new ErrorResponse(code: StatusCodes.Status409Conflict, message: ex.Message));
            }

        }


    }
}
