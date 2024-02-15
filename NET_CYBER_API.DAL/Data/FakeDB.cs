using NET_CYBER_API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_CYBER_API.DAL.Data
{
    public class FakeDB
    {
        public static List<Ticket> tickets = new List<Ticket>
        {
            new Ticket
            {
                Id = 1,
                Auteur = "Jean",
                Titre = "Problème de connexion",
                Description = "Je n'arrive pas à me connecter à mon compte",
                DateCreation = new DateTime(2021, 1, 1),
                DateCloture = new DateTime(2021, 1, 2),
                EstComplete = true
            },
            new Ticket
            {
                Id = 2,
                Auteur = "Paul",
                Titre = "Problème de mot de passe",
                Description = "Je n'arrive pas à me souvenir de mon mot de passe",
                DateCreation = new DateTime(2021, 1, 2),
                DateCloture = null,
                EstComplete = false
            },
            new Ticket
            {
                Id = 3,
                Auteur = "Jacques",
                Titre = "Problème de paiement",
                Description = "Je n'arrive pas à payer ma facture",
                DateCreation = new DateTime(2021, 1, 3),
                DateCloture = null,
                EstComplete = false
            },
            new Ticket
            {
                Id = 4,
                Auteur = "Jeanne",
                Titre = "Problème de connexion",
                Description = "Je n'arrive pas à me connecter à mon compte",
                DateCreation = new DateTime(2021, 1, 4),
                DateCloture = null,
                EstComplete = false
            },
            new Ticket
            {
                Id = 5,
                Auteur = "Pauline",
                Titre = "Problème de mot de passe",
                Description = "Je n'arrive pas à me souvenir de mon mot de passe",
                DateCreation = new DateTime(2021, 1, 5),
                DateCloture = null,
                EstComplete = false
            },
        };
    }
}
