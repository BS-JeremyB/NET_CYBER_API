using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NET_CYBER_API.API.DTOs;
using NET_CYBER_API.API.Mappers;
using NET_CYBER_API.API.Tools.Errors;
using NET_CYBER_API.BLL.CustomExceptions;
using NET_CYBER_API.BLL.Interfaces;
using NET_CYBER_API.DAL.Interfaces;
using NET_CYBER_API.Domain.Models;

namespace NET_CYBER_API.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateurController : ControllerBase
    {

        private readonly IUtilisateurService _service;
        private readonly IAuthService _auth;

        public UtilisateurController(IUtilisateurService service, IAuthService auth)
        {
            _service = service;
            _auth = auth;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Utilisateur>))]
        public ActionResult<IEnumerable<Utilisateur>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Utilisateur))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public ActionResult<Utilisateur> Get([FromRoute] int id)
        {
            try
            {
                Utilisateur? utilisateur = _service.GetById(id);
                return Ok(utilisateur);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorResponse(StatusCodes.Status404NotFound, ex.Message));
            }
        }

        [HttpGet("{email}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Utilisateur))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public ActionResult<Utilisateur> GetByEmail([FromRoute] string email)
        {
            try
            {
                Utilisateur? utilisateur = _service.GetByEmail(email);
                return Ok(utilisateur);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorResponse(StatusCodes.Status404NotFound, ex.Message));
            }
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Utilisateur))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Utilisateur> Insert([FromBody] UtilisateurDataDTO utilisateur)
        {
            Utilisateur utilisateurCreated = _service.Create(utilisateur.DTOToDomain());
    
            return CreatedAtAction(nameof(Get), new { id = utilisateurCreated.Id }, utilisateurCreated);
        }


        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]

        public ActionResult Delete([FromRoute] int id)
        {
            try
            {
                _service.Delete(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorResponse(code: StatusCodes.Status404NotFound, message: ex.Message));

            }

        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Utilisateur))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Utilisateur> Update([FromRoute] int id, [FromBody] UtilisateurDataDTO utilisateur)
        {
            try
            {
                Utilisateur utilisateurToUpdate = utilisateur.DTOToDomain();

                utilisateurToUpdate.Id = id;
                Utilisateur? utilisateurUpdated = _service.Update(utilisateurToUpdate);
                return Ok(utilisateurUpdated);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorResponse(code: StatusCodes.Status404NotFound, message: ex.Message));

            }catch(Exception ex)
            {
                return BadRequest(new ErrorResponse(code: StatusCodes.Status400BadRequest, message: ex.Message));
            }
        }

        //login controller that use logindataDTO
        [HttpPost("login")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Utilisateur))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public ActionResult<Utilisateur> Login([FromBody] LoginDataDTO login)
        {
            
            string? token = _service.Login(login.Email, login.Password);

            if(token is not null)
            {
                return Ok(token);
            }

            return BadRequest(StatusCodes.Status400BadRequest);
        }

        [HttpPut("role/{id}")]
        [Authorize(Roles = "Admin")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Utilisateur))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
        public ActionResult<Utilisateur> UpdateRole([FromRoute] int id, [FromBody] UtilisateurRoleDataDTO utilisateur)
        {
            try
            {
                Utilisateur utilisateurToUpdate = utilisateur.DTOToDomain();

                utilisateurToUpdate.Id = id;
                Utilisateur? utilisateurUpdated = _service.UpdateRole(utilisateurToUpdate);
                return Ok(utilisateurUpdated);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorResponse(code: StatusCodes.Status404NotFound, message: ex.Message));

            }
            catch (NotAuthorizedException ex)
            {
                return Unauthorized(new ErrorResponse(code: StatusCodes.Status401Unauthorized, message: ex.Message));
            }
        }
    }
}
