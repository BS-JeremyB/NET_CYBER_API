using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_CYBER_API.Domain.Models
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Utilisateur Auteur { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public DateTime DateCreation { get; set; } = DateTime.Now;
        public DateTime? DateCloture { get; set; }
        public bool EstComplete { get; set; } = false;

    }
}
