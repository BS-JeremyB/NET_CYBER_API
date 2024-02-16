using NET_CYBER_API.DAL.Data;
using NET_CYBER_API.DAL.Interfaces;
using NET_CYBER_API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_CYBER_API.DAL.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        public Ticket? Create(Ticket ticket)
        {
            FakeDB.tickets.Add(ticket);
            return ticket;
        }

        public bool Delete(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Ticket> GetAll()
        {
            return FakeDB.tickets;
        }

        public Ticket? GetById(int id)
        {      
            return FakeDB.tickets.SingleOrDefault(t => t.Id == id);
           
        }

        public Ticket? Update(Ticket ticket)
        {
            throw new NotImplementedException();
        }
    }
}
