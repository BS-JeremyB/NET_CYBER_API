using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_CYBER_API.Domain.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Auteur { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public DateTime DateCreation { get; set; } = DateTime.Now;
        public DateTime? DateCloture { get; set; }
        public bool EstComplete { get; set; } = false;
    }
}
