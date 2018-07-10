﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jen_Nico_BoVoyage.Models
{
    public class Participant : Personne
    {
        public int NumeroUnique { get; set; }
        public float Reduction { get; set; }

        [ForeignKey("IdDossierReservation")]
        public DossiersReservation DossiersReservation { get; set; }
        public int IdDossierReservation { get; set; }
    }
}