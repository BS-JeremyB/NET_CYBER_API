using NET_CYBER_API.BLL.Interfaces;
using NET_CYBER_API.DAL.Interfaces;
using NET_CYBER_API.Domain.Models;
using NET_CYBER_API.BLL.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_CYBER_API.BLL.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _repository;

        public TicketService(ITicketRepository repository)
        {
            _repository = repository;
        }

        public Ticket? Create(Ticket ticket)
        {
            throw new NotImplementedException();

        }

        public bool Delete(Ticket ticket)
        {
            throw new NotImplementedException();
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

        public Ticket? Update(Ticket ticket)
        {
            throw new NotImplementedException();
        }
    }
}
