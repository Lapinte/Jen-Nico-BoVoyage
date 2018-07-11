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
    public class DestinationsController : ApiController
    {
        private BoVoyageDbContext db = new BoVoyageDbContext();

        /// <summary>
        /// Récupère et retourne la liste des Destinations
        /// </summary>
        /// <returns></returns>
        // GET: api/Destinations
        public IQueryable<Destination> GetDestinations()
        {
            return db.Destinations;
        }

        /// <summary>
        /// Récupère et retourne une Destination en fonction de son ID
        /// </summary>
        /// <returns></returns>
        // GET: api/Destinations/5
        [ResponseType(typeof(Destination))]
        public IHttpActionResult GetDestination(int id)
        {
            Destination destination = db.Destinations.Find(id);
            if (destination == null)
            {
                return NotFound();
            }

            return Ok(destination);
        }

        /// <summary>
        /// Récupère la liste des Destinations en fonction du continent, du pays, de la région et/ou de la description
        /// </summary>
        /// <param name="continent"></param>
        /// <param name="pays"></param>
        /// <param name="region"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        //GET: api/Destination/search
        [Route("api/Destinations/search")]
        public IQueryable<Destination> GetSearch(string continent = "", string pays = "", string region = "", string description = "")
        {
            IQueryable<Destination> liste = db.Destinations;
            if (!string.IsNullOrWhiteSpace(continent))
                liste = liste.Where(x => x.Continent.Contains(continent));
            if (!string.IsNullOrWhiteSpace(pays))
                liste = liste.Where(x => x.Pays.Contains(pays));
            if (!string.IsNullOrWhiteSpace(region))
                liste = liste.Where(x => x.Region.Contains(region));
            if (!string.IsNullOrWhiteSpace(description))
                liste = liste.Where(x => x.Description.Contains(description));

            return liste;
        }

        /// <summary>
        /// Modifie une Destination grâce à un objet JSON
        /// </summary>
        /// <returns></returns>
        // PUT: api/Destinations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDestination(int id, Destination destination)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != destination.ID)
            {
                return BadRequest();
            }

            db.Entry(destination).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DestinationExists(id))
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
        /// Ajoute une Destination grâce à un objet JSON
        /// </summary>
        /// <returns></returns>
        // POST: api/Destinations
        [ResponseType(typeof(Destination))]
        public IHttpActionResult PostDestination(Destination destination)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Destinations.Add(destination);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = destination.ID }, destination);
        }

        /// <summary>
        /// Supprime une Destination en fonction de son ID
        /// </summary>
        /// <returns></returns>
        // DELETE: api/Destinations/5
        [ResponseType(typeof(Destination))]
        public IHttpActionResult DeleteDestination(int id)
        {
            Destination destination = db.Destinations.Find(id);
            if (destination == null)
            {
                return NotFound();
            }

            db.Destinations.Remove(destination);
            db.SaveChanges();

            return Ok(destination);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DestinationExists(int id)
        {
            return db.Destinations.Count(e => e.ID == id) > 0;
        }
    }
}