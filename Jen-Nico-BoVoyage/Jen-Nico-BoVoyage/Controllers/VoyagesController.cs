using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Jen_Nico_BoVoyage.Data;
using Jen_Nico_BoVoyage.Models;

namespace Jen_Nico_BoVoyage.Controllers
{
    public class VoyagesController : ApiController
    {
        private BoVoyageDbContext db = new BoVoyageDbContext();


        /// <summary>
        /// Récupère et retourne la liste des Voyages
        /// </summary>
        /// <returns></returns>
        // GET: api/Voyages
        public IQueryable<Voyage> GetVoyages()
        {
            return db.Voyages;
        }

        /// <summary>
        /// Récupère et retourne la liste des Voyages en fonction de la Date d'aller, retour, des places disponibles, du tarif et de l'id de l'agence et de la destination 
        /// </summary>
        /// <param name="dateAller"></param>
        /// <param name="dateRetour"></param>
        /// <param name="placesDisponibles"></param>
        /// <param name="tarifToutCompris"></param>
        /// <param name="destinationId"></param>
        /// <param name="agenceId"></param>
        /// <returns></returns>
        //GET: api/Voyages/search
        [Route("api/Voyages/search")]
        public IQueryable<Voyage> GetSearch(DateTime? dateAller = null, DateTime? dateRetour = null, int? placesDisponibles = null, float? tarifToutCompris = null, int? destinationId = null, int? agenceId = null)
        {
            var query = db.Voyages.Where(x => !x.Deleted);
            if (dateAller != null)
                query = query.Where(x => x.DateAller == dateAller);
            if (dateRetour != null)
                query = query.Where(x => x.DateRetour == dateRetour);
            if (placesDisponibles != null)
                query = query.Where(x => x.PlacesDisponibles == placesDisponibles);
            if (tarifToutCompris != null)
                query = query.Where(x => x.TarifToutCompris == tarifToutCompris);
            if (destinationId != null)
                query = query.Where(x => x.DestinationID == destinationId);
            if (agenceId != null)
                query = query.Where(x => x.AgenceID == agenceId);

            return query;
        }

        /// <summary>
        /// Récupère et retourne un Voyage en fonction de son ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Voyages/5
        [ResponseType(typeof(Voyage))]
        public IHttpActionResult GetVoyage(int id)
        {
            Voyage voyage = db.Voyages.Find(id);
            if (voyage == null)
            {
                return NotFound();
            }

            return Ok(voyage);
        }


        /// <summary>
        /// Modifie un Voyage grâce à un objet JSON
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dossier"></param>
        /// <returns></returns>
        // PUT: api/Voyages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVoyage(int id, Voyage voyage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != voyage.ID)
            {
                return BadRequest();
            }

            db.Entry(voyage).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoyageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        /// <summary>
        /// Ajoute un Voyage grâce à un objet JSON
        /// </summary>
        /// <param name="dossier"></param>
        /// <returns></returns>
        // POST: api/Voyages
        [ResponseType(typeof(Voyage))]
        public IHttpActionResult PostVoyage(Voyage voyage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Voyages.Add(voyage);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = voyage.ID }, voyage);
        }


        /// <summary>
        /// Supprime un Voyage en fonction de son ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Voyages/5
        [ResponseType(typeof(Voyage))]
        public IHttpActionResult DeleteVoyage(int id)
        {
            Voyage voyage = db.Voyages.Find(id);
            if (voyage == null)
            {
                return NotFound();
            }

            db.Voyages.Remove(voyage);
            db.SaveChanges();

            return Ok(voyage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VoyageExists(int id)
        {
            return db.Voyages.Count(e => e.ID == id) > 0;
        }
    }
}