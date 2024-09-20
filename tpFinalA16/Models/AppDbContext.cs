using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace tpFinalA16.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Commande> Commandes { get; set; }
        public DbSet<Ligne_commande> Ligne_commandes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public AppDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDBcIntegrated Security=True");
            //}
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Data Source=localhost,1433;Initial Catalog=vertcite;User ID=sa;Password=Your_password123;Trust Server Certificate=True"
                    );
            }
        }
    }
}
