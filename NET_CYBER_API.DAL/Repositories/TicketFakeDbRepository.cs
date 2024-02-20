using NET_CYBER_API.DAL.Data;
using NET_CYBER_API.DAL.Interfaces;
using NET_CYBER_API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NET_CYBER_API.DAL.Repositories
{
    public class TicketFakeDbRepository 
    {
        public Ticket? Create(Ticket ticket)
        {
            FakeDB.tickets.Add(ticket);
            return ticket;
        }

        public bool Delete(int id)
        {
            return FakeDB.tickets.Remove(GetById(id));
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
            Ticket? ticketToUpdate = GetById(ticket.Id);
            if(ticketToUpdate is not null) { 
                ticketToUpdate.Auteur = ticket.Auteur;
                ticketToUpdate.Titre = ticket.Titre;
                ticketToUpdate.Description = ticket.Description ?? ticketToUpdate.Description;
            }
            return ticketToUpdate;
            
        }

        public Ticket? Complete(int id)
        {
            Ticket? ticketToComplete = GetById(id);
            if( ticketToComplete is not null ) {
                ticketToComplete.DateCloture = DateTime.Now;
                ticketToComplete.EstComplete = true;
            }
            return ticketToComplete;
        }
    }
}
