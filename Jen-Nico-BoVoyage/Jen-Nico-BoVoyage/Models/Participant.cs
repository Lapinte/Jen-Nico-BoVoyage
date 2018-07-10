﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jen_Nico_BoVoyage.Models
{
    public class Participant : Personne
    {
        [ForeignKey("DossierID")]
        public Dossier Dossier { get; set; }
        public int DossierID { get; set; }
    }
}