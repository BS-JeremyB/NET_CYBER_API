using Crypt = BCrypt.Net;
using NET_CYBER_API.BLL.CustomExceptions;
using NET_CYBER_API.BLL.Interfaces;
using NET_CYBER_API.DAL.Interfaces;
using NET_CYBER_API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace NET_CYBER_API.BLL.Services
{
    public class UtilisateurService : IUtilisateurService
    {
        private readonly IUtilisateurRepository _repository;
        private readonly IAuthService _auth;

        public UtilisateurService(IUtilisateurRepository repository, IAuthService auth)
        {
            _repository = repository;
            _auth = auth;
        }

        public Utilisateur Create(Utilisateur utilisateur)
        {
            utilisateur.Password = Crypt.BCrypt.HashPassword(utilisateur.Password);
            return _repository.Create(utilisateur);
        }

        public bool Delete(int id)
        {
            bool success = _repository.Delete(id);
            if (!success)
            {
                throw new NotFoundException($"L'id '{id}' n'a pas été trouvé");
            }
            return true;
        }

        public IEnumerable<Utilisateur> GetAll()
        {
            return _repository.GetAll();
        }

        public Utilisateur? GetByEmail(string email)
        {
            Utilisateur? utilisateur = _repository.GetByEmail(email);
            if (utilisateur is null)
            {
                throw new NotFoundException($"L'email '{email}' n'a pas été trouvé");
            }
            return utilisateur;
        }

        public Utilisateur? GetById(int id)
        {
            Utilisateur? utilisateur = _repository.GetById(id);
            if (utilisateur is null)
            {
                throw new NotFoundException($"L'id '{id}' n'a pas été trouvé");
            }
            return utilisateur;
        }

        public Utilisateur? Update(Utilisateur utilisateur)
        {
            Utilisateur? utilisateurToUpdate = _repository.GetById(utilisateur.Id);
            if (utilisateurToUpdate is null)
            {
                throw new NotFoundException($"L'id '{utilisateur.Id}' n'a pas été trouvé");
            }

            utilisateurToUpdate.Email = utilisateur.Email;
            utilisateurToUpdate.Nom = utilisateur.Nom;
            utilisateurToUpdate.Prenom = utilisateur.Prenom;
            _repository.Update(utilisateurToUpdate);

            return utilisateurToUpdate;
        }

        public string? Login(string email, string password)
        {
            Utilisateur? utilisateur = _repository.GetByEmail(email);
            if(utilisateur is null)
            {
                throw new NotFoundException($"L'email '{email}' n'a pas été trouvé");
            }

            if (Crypt.BCrypt.Verify(password, utilisateur.Password))
            {
                return _auth.GenerateToken(utilisateur);
            }
            throw new InvalidOperationException("Mot de passe incorrect");


        }
    }
}
