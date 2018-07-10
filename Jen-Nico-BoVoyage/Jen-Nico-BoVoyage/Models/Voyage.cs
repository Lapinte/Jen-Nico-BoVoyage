using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jen_Nico_BoVoyage.Models
{
    public class Voyage : BaseModel
    {
        public DateTime DateAller { get; set; }

        public DateTime DateRetour { get; set; }

        public int PlacesDisponibles { get; set; }

        public float TarifToutCompris { get; set; }

        public int DestinationID { get; set; }
        [ForeignKey("DestinationID")]
        public Destination Destination { get; set; }

        public int AgenceID { get; set; }
        [ForeignKey("AgenceID")]
        public Agence Agence { get; set; }
    }
}