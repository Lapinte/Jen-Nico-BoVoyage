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
    public class DossiersController : ApiController
    {
        private BoVoyageDbContext db = new BoVoyageDbContext();

        /// <summary>
        /// Récupère et retourne la liste des Dossiers
        /// </summary>
        /// <returns></returns>
        // GET: api/Dossiers
        public IQueryable<Dossier> GetDossiers()
        {
            return db.Dossiers;
        }


        /// <summary>
        /// Récupère et retourne un Dossier en fonction de son ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Dossiers/5
        [ResponseType(typeof(Dossier))]
        public IHttpActionResult GetDossier(int id)
        {
            Dossier dossier = db.Dossiers.Find(id);
            if (dossier == null)
            {
                return NotFound();
            }

            return Ok(dossier);
        }

        //GET: api/Dossiers/search
        [Route("api/Dossiers/search")]
        public IQueryable<Dossier> GetSearch(int? clientId = null, int? voyageId = null)
        {
            var query = db.Dossiers.Where(x => !x.Deleted);
            if (clientId != null)
                query = query.Where(x => x.ClientID == clientId);
            if (voyageId != null)
                query = query.Where(x => x.VoyageID == voyageId);

            return query;
        }


        /// <summary>
        /// Modifie un Dossier grâce à un objet JSON
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dossier"></param>
        /// <returns></returns>
        // PUT: api/Dossiers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDossier(int id, Dossier dossier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dossier.ID)
            {
                return BadRequest();
            }

            db.Entry(dossier).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DossierExists(id))
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
        /// Ajoute un Dossier grâce à un objet JSON
        /// </summary>
        /// <param name="dossier"></param>
        /// <returns></returns>
        // POST: api/Dossiers
        [ResponseType(typeof(Dossier))]
        public IHttpActionResult PostDossier(Dossier dossier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Dossiers.Add(dossier);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dossier.ID }, dossier);
        }

        
        /// <summary>
        /// Supprime un Dossier en fonction de son ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Dossiers/5
        [ResponseType(typeof(Dossier))]
        public IHttpActionResult DeleteDossier(int id)
        {
            Dossier dossier = db.Dossiers.Find(id);
            if (dossier == null)
            {
                return NotFound();
            }

            db.Dossiers.Remove(dossier);
            db.SaveChanges();

            return Ok(dossier);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DossierExists(int id)
        {
            return db.Dossiers.Count(e => e.ID == id) > 0;
        }
    }
}