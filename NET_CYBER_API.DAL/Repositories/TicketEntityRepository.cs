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
    public class TicketEntityRepository : ITicketRepository
    {
    private readonly DataContext _context;

        public TicketEntityRepository(DataContext context)
        {
            _context = context;
        }

        public Ticket? Complete(int id)
        {
            throw new NotImplementedException();
        }

        public Ticket Create(Ticket ticket)
        {
            _context.tickets.Add(ticket);
            _context.SaveChanges();
            return ticket;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Ticket> GetAll()
        {
            return _context.tickets;
        }

        public Ticket? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Ticket? Update(Ticket ticket)
        {
            throw new NotImplementedException();
        }
    }
}
