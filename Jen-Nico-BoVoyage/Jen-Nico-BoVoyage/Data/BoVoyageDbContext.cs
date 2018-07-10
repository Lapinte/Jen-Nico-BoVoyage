using Jen_Nico_BoVoyage.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Jen_Nico_BoVoyage.Data
{
    public class BoVoyageDbContext : DbContext
    {
        public BoVoyageDbContext() : base("BoVoyageAzure")
        {
        }

        public DbSet<Agence> Agences { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Voyage> Voyages { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Dossier> Dossiers { get; set; }
    }
}