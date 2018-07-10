using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jen_Nico_BoVoyage.Models
{
    public class Dossier : BaseModel
    {
        public string NumeroCarteBancaire { get; set; }

        public float PrixTotal { get; set; }

        public bool Assurance { get; set; }

        public int VoyageID { get; set; }
        [ForeignKey("VoyageID")]
        public Voyage Voyage { get; set; }

        public int ClientID { get; set; }
        [ForeignKey("ClientID")]
        public Client Client { get; set; }
    }
}