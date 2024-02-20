using NET_CYBER_API.DAL.Data;
using NET_CYBER_API.DAL.Interfaces;
using NET_CYBER_API.Domain.Models;


namespace NET_CYBER_API.DAL.Repositories
{
    public class UtilisateurEntityRepository : IUtilisateurRepository
    {
        private readonly DataContext _context;

        public UtilisateurEntityRepository(DataContext context)
        {
            _context = context;
        }

        public Utilisateur Create(Utilisateur utilisateur)
        {
            _context.utilisateurs.Add(utilisateur);
            _context.SaveChanges();
            return utilisateur;
        }

        public bool Delete(int id)
        {

            Utilisateur? utilisateur = _context.utilisateurs.FirstOrDefault(x => x.Id == id);
            if (utilisateur is null)
            {
                return false;
            }
            _context.utilisateurs.Remove(utilisateur);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Utilisateur> GetAll()
        {
            return _context.utilisateurs;
        }

        public Utilisateur? GetByEmail(string email)
        {
            return _context.utilisateurs.FirstOrDefault(x => x.Email == email);  
        }

        public Utilisateur? GetById(int id)
        {
            return _context.utilisateurs.FirstOrDefault(x => x.Id == id);
        }

        public Utilisateur Update(Utilisateur Utilisateur)
        {
            _context.utilisateurs.Update(Utilisateur);
            _context.SaveChanges();
            return Utilisateur;
        }
    }
}
