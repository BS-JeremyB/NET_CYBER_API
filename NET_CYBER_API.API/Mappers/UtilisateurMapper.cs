using NET_CYBER_API.API.DTOs;
using NET_CYBER_API.Domain.Models;

namespace NET_CYBER_API.API.Mappers
{
    public static class UtilisateurMapper
    {
        public static Utilisateur DTOToDomain(this UtilisateurDataDTO utilisateur)
        {
            return new Utilisateur
            {
                Email = utilisateur.Email,
                Nom = utilisateur.Nom,
                Prenom = utilisateur.Prenom,
                Password = utilisateur.Password,
                Role = Domain.Enums.RoleEnum.Employee
            };
        }

        public static UtilisateurInfoDTO DomainToInfoDTO(this Utilisateur utilisateur)
        {
            return new UtilisateurInfoDTO
            {
                Email = utilisateur.Email,
                Id = utilisateur.Id,
            };
        }
    }
}
