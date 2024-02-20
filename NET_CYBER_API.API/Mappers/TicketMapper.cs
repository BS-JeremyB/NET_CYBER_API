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
    }
}
