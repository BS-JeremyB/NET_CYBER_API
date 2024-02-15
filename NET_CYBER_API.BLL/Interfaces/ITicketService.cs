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
        Ticket? GetById(int id);
        Ticket? Create(Ticket ticket);
        Ticket? Update(Ticket ticket);
        bool Delete(Ticket ticket);
    }
}
