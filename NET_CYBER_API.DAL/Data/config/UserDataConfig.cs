using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NET_CYBER_API.Domain.Models;
using Crypt = BCrypt.Net;

namespace NET_CYBER_API.DAL.Data.config
{
    public class UserDataConfig : IEntityTypeConfiguration<Utilisateur>
    {
        public void Configure(EntityTypeBuilder<Utilisateur> builder)
        {
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasData(new Utilisateur
            {
                Id = 1,
                Email = "doejohn@mail.be",
                Nom = "doe",
                Prenom = "john",
                Password = Crypt.BCrypt.HashPassword("Test1234="),
                Role = Domain.Enums.RoleEnum.Admin
            });
        }
    }
}
