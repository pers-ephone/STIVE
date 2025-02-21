using LibrarySTIVE.Models;
using Microsoft.EntityFrameworkCore;
using LibrarySTIVE.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using Client = MySqlX.XDevAPI.Client;

namespace PartieAPI_Projet_Solo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Définition des tables
        public DbSet<Article> Articles { get; set; }
        public DbSet<Famille> Familles { get; set; }
        public DbSet<Fournisseur> Fournisseurs { get; set; }
        public DbSet<Commande> Commande { get; set; }
        
        public DbSet<ArticlesInCommande> ArticlesInCommande { get; set; }

        public DbSet<ClientSTIVE> ClientSTIVE { get; set; }


    }
}
