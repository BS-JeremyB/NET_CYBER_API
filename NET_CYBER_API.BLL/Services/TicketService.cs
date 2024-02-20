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

        public Ticket Create(Ticket ticket)
        {
            return _repository.Create(ticket);
        }

        public bool Delete(int id)
        {
            try
            {
                bool success = _repository.Delete(id);
                if(!success)
                {
                    throw new NotFoundException($"Le ticket {id} n'a pas été trouvé");
                }
                return success;

            }
            catch(InvalidOperationException ex)
            {
                throw new NotSingleException($"Ceci n'est pas sensé arriver mais padchance, y'a 2 fois le ticket {id} dans la DB");
            }
        }

        public IEnumerable<Ticket> GetAll()
        {
            return _repository.GetAll();
        }

        public Ticket GetById(int id)
        {
            Ticket? ticket;
            try
            {
                ticket = _repository.GetById(id);
                if (ticket is null)
                {
                    throw new NotFoundException($"Le ticket {id} n'a pas été trouvé");
                }
                return ticket;
            }
            catch(InvalidOperationException ex) {
                throw new NotSingleException("Alors c'était vraiment très très peu probable mais y'a 2 fois l'id en DB");

            }
            
                       
        }

        public Ticket? Update(int userId, Ticket ticket)
        {


            try
            {
                Ticket? ticketToUpdate = _repository.GetById(ticket.Id);
                if(ticketToUpdate is null)
                {
                    throw new NotFoundException($"Le ticket {ticket.Id} n'a pas été trouvé");
                }
                if(ticketToUpdate.Auteur.Id != userId)
                {
                    throw new NotAuthorizedException($"Le ticket {ticket.Id} n'appartient pas à l'utilisateur {userId}");
                }
                ticketToUpdate.Titre = ticket.Titre;
                ticketToUpdate.Description = ticket.Description;
                return _repository.Update(ticketToUpdate);
            }
            catch(InvalidOperationException ex)
            {
                throw new NotSingleException($"Ceci n'est pas sensé arriver mais padchance, y'a 2 fois le ticket {ticket.Id} dans la DB");
            }
        }

        public Ticket? Complete(int id)
        {
            try
            {
                Ticket? ticketToUpdate = _repository.GetById(id);
                if (ticketToUpdate is null)
                {
                    throw new NotFoundException($"Le ticket {id} n'a pas été trouvé");
                }
                ticketToUpdate.DateCloture = DateTime.Now;
                ticketToUpdate.EstComplete = true;
                return _repository.Complete(ticketToUpdate);
            }
            catch (InvalidOperationException ex)
            {
                throw new NotSingleException($"Ceci n'est pas sensé arriver mais padchance, y'a 2 fois le ticket {id} dans la DB");
            }
        }
    }
}
