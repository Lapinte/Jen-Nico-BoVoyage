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

        // GET: api/Dossiers
        public IQueryable<Dossier> GetDossiers()
        {
            return db.Dossiers;
        }

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