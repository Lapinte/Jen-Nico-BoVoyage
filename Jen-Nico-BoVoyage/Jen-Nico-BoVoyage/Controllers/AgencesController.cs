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
    public class AgencesController : ApiController
    {
        private BoVoyageDbContext db = new BoVoyageDbContext();

        /// <summary>
        /// Récupère et retourne la liste des Agences
        /// </summary>
        /// <returns></returns>
        // GET: api/Agences
        public IQueryable<Agence> GetAgences()
        {
            return db.Agences;
        }

        /// <summary>
        /// Récupère et retourne une Agence en fonction de son ID
        /// </summary>
        /// <returns></returns>
        // GET: api/Agences/5
        [ResponseType(typeof(Agence))]
        public IHttpActionResult GetAgence(int id)
        {
            Agence agence = db.Agences.Find(id);
            if (agence == null)
            {
                return NotFound();
            }

            return Ok(agence);
        }

        /// <summary>
        /// Récupère et retourne la liste des Agences en fontion de leur nom
        /// </summary>
        /// <remarks>exemple: /search?nom=jeanne</remarks>
        /// <param nom="nom"></param>
        /// <returns></returns>
        [Route("api/Agences/search")]
        public IQueryable<Agence> GetSearch(string nom = "")
        {
            var liste = db.Agences.Where(x => !x.Deleted);
            if (!string.IsNullOrWhiteSpace(nom))
                liste = liste.Where(x => x.Nom.Contains(nom));

            return liste;
        }

        /// <summary>
        /// Modifie une Agence grâce à un objet JSON
        /// </summary>
        /// <returns></returns>
        // PUT: api/Agences/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAgence(int id, Agence agence)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != agence.ID)
            {
                return BadRequest();
            }

            db.Entry(agence).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgenceExists(id))
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
        /// Ajoute une Agence grâce à un objet JSON
        /// </summary>
        /// <returns></returns>
        // POST: api/Agences
        [ResponseType(typeof(Agence))]
        public IHttpActionResult PostAgence(Agence agence)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Agences.Add(agence);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = agence.ID }, agence);
        }

        /// <summary>
        /// Supprime une Agence en fonction de son ID
        /// </summary>
        /// <returns></returns>
        // DELETE: api/Agences/5
        [ResponseType(typeof(Agence))]
        public IHttpActionResult DeleteAgence(int id)
        {
            Agence agence = db.Agences.Find(id);
            if (agence == null)
            {
                return NotFound();
            }

            db.Agences.Remove(agence);
            db.SaveChanges();

            return Ok(agence);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AgenceExists(int id)
        {
            return db.Agences.Count(e => e.ID == id) > 0;
        }
    }
}