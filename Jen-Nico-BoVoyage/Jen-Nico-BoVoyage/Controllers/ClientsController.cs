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
    public class ClientsController : ApiController
    {
        private BoVoyageDbContext db = new BoVoyageDbContext();

        /// <summary>
        /// Récupère et retourne la liste des Clients
        /// </summary>
        /// <returns></returns>
        // GET: api/Clients
        public IQueryable<Client> GetClients()
        {
            return db.Clients;
        }

        /// <summary>
        /// Récupère et retourne un Client en fonction de son ID
        /// </summary>
        /// <returns></returns>
        // GET: api/Clients/5
        [ResponseType(typeof(Client))]
        public IHttpActionResult GetClient(int id)
        {
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        /// <summary>
        /// Récupère la liste des Clients en fonction de la civilité, du nom, du prénom, de l'adresse, du téléphone, de la date de naissance et/ou de l'email
        /// </summary>
        /// <param name="civilite"></param>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <param name="adresse"></param>
        /// <param name="telephone"></param>
        /// <param name="dateNaissance"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        //GET: api/Clients/search
        [Route("api/Clients/search")]
        public IQueryable<Client> GetSearch(string civilite = "", string nom = "", string prenom = "", string adresse = "", string telephone = "", DateTime? dateNaissance = null, string email="")
        {
            var liste = db.Clients.Where(x => !x.Deleted);
            if (!string.IsNullOrWhiteSpace(civilite))
                liste = liste.Where(x => x.Civilite.Contains(civilite));
            if (!string.IsNullOrWhiteSpace(nom))
                liste = liste.Where(x => x.Nom.Contains(nom));
            if (!string.IsNullOrWhiteSpace(prenom))
                liste = liste.Where(x => x.Prenom.Contains(prenom));
            if (!string.IsNullOrWhiteSpace(adresse))
                liste = liste.Where(x => x.Adresse.Contains(adresse));
            if (!string.IsNullOrWhiteSpace(telephone))
                liste = liste.Where(x => x.Telephone.Contains(telephone));
            if (dateNaissance != null)
                liste = liste.Where(x => x.DateNaissance == dateNaissance);
            if (!string.IsNullOrWhiteSpace(email))
                liste = liste.Where(x => x.Email.Contains(email));

            return liste;
        }


        /// <summary>
        /// Modifie un Client grâce à un objet JSON
        /// </summary>
        /// <returns></returns>
        // PUT: api/Clients/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClient(int id, Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.ID)
            {
                return BadRequest();
            }

            db.Entry(client).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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
        /// Ajoute un Client grâce à un objet JSON
        /// </summary>
        /// <returns></returns>
        // POST: api/Clients
        [ResponseType(typeof(Client))]
        public IHttpActionResult PostClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Clients.Add(client);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = client.ID }, client);
        }

        /// <summary>
        /// Supprime un Client en fonction de son ID
        /// </summary>
        /// <returns></returns>
        // DELETE: api/Clients/5
        [ResponseType(typeof(Client))]
        public IHttpActionResult DeleteClient(int id)
        {
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            db.Clients.Remove(client);
            db.SaveChanges();

            return Ok(client);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientExists(int id)
        {
            return db.Clients.Count(e => e.ID == id) > 0;
        }
    }
}