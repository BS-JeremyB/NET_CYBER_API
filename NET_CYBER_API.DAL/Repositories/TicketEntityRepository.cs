﻿using Microsoft.EntityFrameworkCore;
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

        public Ticket? Complete(Ticket ticket)
        {
            _context.tickets.Update(ticket);
            _context.SaveChanges();
            return ticket;
        }

        public Ticket Create(Ticket ticket)
        {
            _context.tickets.Add(ticket);
            _context.SaveChanges();
            return ticket;
        }

        public bool Delete(int id)
        {
            Ticket ticketToDelete = GetById(id);
            if(ticketToDelete is not null)
            {
                _context.tickets.Remove(ticketToDelete);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<Ticket> GetAll()
        {
            return _context.tickets.Include(t => t.Auteur);
        }

        public Ticket? GetById(int id)
        {
            return _context.tickets.Include(t => t.Auteur).FirstOrDefault(t => t.Id == id);
        }

        public Ticket? Update(Ticket ticket)
        {
            _context.tickets.Update(ticket);
            _context.SaveChanges();
            return ticket;
        }
    }
}
