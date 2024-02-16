using System.ComponentModel.DataAnnotations;

namespace NET_CYBER_API.API.DTOs
{
    public class TicketDataDTO
    {
        [Required(ErrorMessage = "Le champs Auteur est requis")]
        public string Auteur { get; set; }

        [Required(ErrorMessage = "Le champs Titre est requis")]
        [MaxLength(15, ErrorMessage = "Le champs Titre doit faire maximum 15 caractères")]
        public string Titre { get; set; }

        [MaxLength(200, ErrorMessage = "Le champs Description doit faire maximum 200 caractères")]
        public string? Description { get; set; }
    }
}
