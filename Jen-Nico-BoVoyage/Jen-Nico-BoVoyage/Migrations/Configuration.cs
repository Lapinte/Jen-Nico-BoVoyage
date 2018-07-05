using Jen_Nico_BoVoyage.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Jen_Nico_BoVoyage.Migrations
{
    public class Configuration : DbMigrationsConfiguration<BoVoyageDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}