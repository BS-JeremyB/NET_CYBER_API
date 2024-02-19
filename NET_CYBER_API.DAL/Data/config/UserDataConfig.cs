using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NET_CYBER_API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Password = "Test1234=",
                Role = Domain.Enums.RoleEnum.Admin


            });
        }
    }
}
