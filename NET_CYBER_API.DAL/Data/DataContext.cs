using Microsoft.EntityFrameworkCore;
using NET_CYBER_API.DAL.Data.config;
using NET_CYBER_API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_CYBER_API.DAL.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }

        public DbSet<Utilisateur> utilisateurs { get; set; }
        public DbSet<Ticket> tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfiguration(new UserDataConfig());
        }
    }
}
