using System.ComponentModel.DataAnnotations;

namespace NET_CYBER_API.API.DTOs
{
    public class UtilisateurDataDTO
    {
        public string? Nom {  get; set; }
        public string? Prenom { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=!]).{8,25}$", ErrorMessage = "Le mot de passe doit contenir 1 Maj, 1 Min, 1 chiffre, 1 char spécial")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Les mots de passes de correspondent pas")]
        public string PasswordControl { get; set; }
    }
}
