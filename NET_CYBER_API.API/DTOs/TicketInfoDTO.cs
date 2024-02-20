using NET_CYBER_API.Domain.Models;

namespace NET_CYBER_API.API.DTOs
{
    public class TicketInfoDTO
    {
        public int Id { get; set; }
        public UtilisateurInfoDTO Auteur { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public DateTime DateCreation { get; set; } 
        public DateTime? DateCloture { get; set; }
        public bool EstComplete { get; set; } 
    }
}
