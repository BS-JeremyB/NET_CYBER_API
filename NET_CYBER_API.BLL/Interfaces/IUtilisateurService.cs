using NET_CYBER_API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_CYBER_API.BLL.Interfaces
{
    public interface IUtilisateurService
    {
        IEnumerable<Utilisateur> GetAll();
        Utilisateur? GetById(int id);
        Utilisateur? GetByEmail(string email);
        Utilisateur Create(Utilisateur Utilisateur);
        Utilisateur? Update(Utilisateur Utilisateur);
        bool Delete(int id);
        string? Login(string email, string password);
    }
}
