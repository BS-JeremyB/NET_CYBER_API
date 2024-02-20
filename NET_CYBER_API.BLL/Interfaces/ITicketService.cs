﻿using NET_CYBER_API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_CYBER_API.BLL.Interfaces
{
    public interface ITicketService
    {
        IEnumerable<Ticket> GetAll();
        Ticket GetById(int id);
        Ticket Create(Ticket ticket);
        Ticket? Update(int userId,Ticket ticket);

        Ticket? Complete(int id);
        bool Delete(int id);
    }
}
