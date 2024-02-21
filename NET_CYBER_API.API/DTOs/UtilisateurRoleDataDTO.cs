using NET_CYBER_API.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace NET_CYBER_API.API.DTOs
{
    public class UtilisateurRoleDataDTO
    {
        [Required]
        public RoleEnum Role { get; set; }
    }
}
