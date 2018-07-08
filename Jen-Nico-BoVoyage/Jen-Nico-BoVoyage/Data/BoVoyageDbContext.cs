using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Jen_Nico_BoVoyage.Data
{
    public class BoVoyageDbContext : DbContext
    {
        public BoVoyageDbContext() : base("BoVoyage")
        {
        }

        //public DbSet<Agence> Agences { get; set; }
    }
}