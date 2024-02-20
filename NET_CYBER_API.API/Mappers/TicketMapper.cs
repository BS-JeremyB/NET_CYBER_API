using NET_CYBER_API.API.DTOs;
using NET_CYBER_API.Domain.Models;

namespace NET_CYBER_API.API.Mappers
{
    public static class TicketMapper
    {
        public static Ticket DTOToDomain(this TicketDataDTO ticket)
        {
            return new Ticket
            {
                Id = 0,
                Description = ticket.Description,
                Titre = ticket.Titre,

            };
        }

        public static TicketInfoDTO DomainToInfoDTO(this Ticket ticket)
        {
            return new TicketInfoDTO
            {
                Id = ticket.Id,
                Auteur = ticket.Auteur.DomainToInfoDTO(),
                Description = ticket.Description,
                Titre = ticket.Titre,
                DateCreation = ticket.DateCreation,
                DateCloture = ticket.DateCloture,
                EstComplete = ticket.EstComplete,
            };
        }

        public static IEnumerable<TicketInfoDTO> ticketInfoDTOs(this IEnumerable<Ticket> tickets)
        {
            List<TicketInfoDTO> ticketInfoDTOs = new List<TicketInfoDTO>();
            foreach (var ticket in tickets)
            {
                ticketInfoDTOs.Add(ticket.DomainToInfoDTO());
            }
            return ticketInfoDTOs;
        }
    }
}
