using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NET_CYBER_API.API.DTOs;
using NET_CYBER_API.API.Mappers;
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

        public UtilisateurController(IUtilisateurService service)
        {
            _service = service;
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created,Type = typeof(Utilisateur))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult Insert([FromBody]UtilisateurDataDTO utilisateur)
        {
            if(ModelState.IsValid)
            {
                _service.Create(utilisateur.DTOToDomain());
                return Ok();

            }
            return BadRequest();
        }

    }
}
