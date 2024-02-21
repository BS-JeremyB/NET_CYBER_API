using NET_CYBER_API.BLL.Interfaces;
using NET_CYBER_API.DAL.Interfaces;
using NET_CYBER_API.Domain.Models;
using NET_CYBER_API.BLL.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace NET_CYBER_API.BLL.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _repository;

        public TicketService(ITicketRepository repository)
        {
            _repository = repository;
        }

        public Ticket? Complete(int id)
        {
            Ticket? ticketToUpdate = _repository.GetById(id);
            if(ticketToUpdate is null)
            {
                throw new NotFoundException($"Le ticket {id} n'a pas été trouvé");
            }

            ticketToUpdate.DateCloture = DateTime.Now;
            ticketToUpdate.EstComplete = true;

            return _repository.Complete(ticketToUpdate);
        }

        public Ticket Create(Ticket ticket)
        {
             return _repository.Create(ticket);
        }

        public bool Delete(int id)
        {
            bool isDeleted = _repository.Delete(id);
            if(!isDeleted)
            {
                throw new NotFoundException($"Le ticket {id} n'a pas été trouvé");
            }
            return isDeleted;
        }

        public IEnumerable<Ticket> GetAll()
        {
            return _repository.GetAll();
        }

        public Ticket GetById(int id)
        {
            Ticket? ticket = _repository.GetById(id);
            if(ticket is null)
            {
                throw new NotFoundException($"Le ticket {id} n'a pas été trouvé");
            }
            return ticket;
        }

        public Ticket? Update(int userId, Ticket ticket)
        {
            Ticket? ticketToUpdate = _repository.GetById(ticket.Id);
            if( ticketToUpdate is null)
            {
                throw new NotFoundException($"Le ticket {ticket.Id} n'a pas été trouvé");
            }

            if(ticketToUpdate.Auteur.Id != userId)
            {
                throw new NotAuthorizedException($"Vous n'êtes pas le propriétaire du ticket {ticket.Id}");
            }

            ticketToUpdate.Titre = ticket.Titre;
            ticketToUpdate.Description = ticket.Description;

            return _repository.Update(ticketToUpdate);
        }
    }
}
