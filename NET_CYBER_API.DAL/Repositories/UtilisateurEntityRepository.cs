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
    public class UtilisateurEntityRepository : IUtilisateurRepository
    {
        private readonly DataContext _context;

        public UtilisateurEntityRepository(DataContext context)
        {
            _context = context;
        }

        public Utilisateur Create(Utilisateur Utilisateur)
        {
            _context.Add(Utilisateur);
            _context.SaveChanges();
            return Utilisateur;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Utilisateur> GetAll()
        {
            throw new NotImplementedException();
        }

        public Utilisateur GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Utilisateur GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Utilisateur Update(Utilisateur Utilisateur)
        {
            throw new NotImplementedException();
        }
    }
}
