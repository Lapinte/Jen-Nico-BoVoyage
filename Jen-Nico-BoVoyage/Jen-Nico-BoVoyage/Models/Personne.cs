﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jen_Nico_BoVoyage.Models
{
    public class Personne : BaseModel
    {
        public string Civilite { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Adresse { get; set; }

        public string Telephone { get; set; }

        public DateTime DateNaissance { get; set; }
    }
}