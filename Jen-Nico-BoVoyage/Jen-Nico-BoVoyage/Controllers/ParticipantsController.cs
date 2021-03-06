﻿using System;
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
    public class ParticipantsController : ApiController
    {
        private BoVoyageDbContext db = new BoVoyageDbContext();

        /// <summary>
        /// Récupère et retourne la liste des Participants
        /// </summary>
        /// <returns></returns>
        // GET: api/Participants
        public IQueryable<Participant> GetParticipants()
        {
            return db.Participants.Include(x => x.Dossier);
        }

        /// <summary>
        /// Récupère et retourne un Participant en fonction de son ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Participants/5
        [ResponseType(typeof(Participant))]
        public IHttpActionResult GetParticipant(int id)
        {
            Participant participant = db.Participants.Find(id);
            if (participant == null)
            {
                return NotFound();
            }

            return Ok(participant);
        }

        /// <summary>
        /// Récupère et retourne la liste des Participants en fontion de la Civilité, Nom, Prénom, Adresse, Telephone et la Date de Naissance 
        /// </summary>
        /// <param name="civilite"></param>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <param name="adresse"></param>
        /// <param name="telephone"></param>
        /// <param name="dateNaissance"></param>
        /// <returns></returns>
        //GET: api/Participants/search
        [Route("api/Participants/search")]
        public IQueryable<Participant> GetSearch(string civilite ="", string nom ="", string prenom ="", string adresse ="", string telephone ="", DateTime? dateNaissance = null)
        {
            var query = db.Participants.Where(x => !x.Deleted);
            if (!string.IsNullOrWhiteSpace(nom))
                query = query.Where(x => x.Nom.Contains(nom));
            if (!string.IsNullOrWhiteSpace(prenom))
                query = query.Where(x => x.Prenom.Contains(prenom));
            if (!string.IsNullOrWhiteSpace(adresse))
                query = query.Where(x => x.Adresse.Contains(adresse));
            if (!string.IsNullOrWhiteSpace(telephone))
                query = query.Where(x => x.Telephone.Contains(telephone));
            if (dateNaissance != null)
                query = query.Where(x => x.DateNaissance == dateNaissance);

            return query;
        }

        /// <summary>
        /// Modifie un Participant grâce à un objet JSON
        /// </summary>
        /// <param name="id"></param>
        /// <param name="participant"></param>
        /// <returns></returns>
        // PUT: api/Participants/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutParticipant(int id, Participant participant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != participant.ID)
            {
                return BadRequest();
            }

            db.Entry(participant).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipantExists(id))
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
        /// Ajoute un Participant grâce à un objet JSON
        /// </summary>
        /// <param name="participant"></param>
        /// <returns></returns>
        // POST: api/Participants
        [ResponseType(typeof(Participant))]
        public IHttpActionResult PostParticipant(Participant participant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Participants.Add(participant);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = participant.ID }, participant);
        }

        /// <summary>
        /// Supprime un Participant en fonction de son ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Participants/5
        [ResponseType(typeof(Participant))]
        public IHttpActionResult DeleteParticipant(int id)
        {
            Participant participant = db.Participants.Find(id);
            if (participant == null)
            {
                return NotFound();
            }

            db.Participants.Remove(participant);
            db.SaveChanges();

            return Ok(participant);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParticipantExists(int id)
        {
            return db.Participants.Count(e => e.ID == id) > 0;
        }
    }
}