﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jen_Nico_BoVoyage.Models
{
    public class Destination : BaseModel
    {
        public string Continent { get; set; }

        public string Pays { get; set; }

        public string Region { get; set; }

        public string Description { get; set; }
    }
}