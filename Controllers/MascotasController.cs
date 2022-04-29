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
using PruebaTecnica.Backend.Models;

namespace PruebaTecnica.Backend.Controllers
{
    public class MascotasController : ApiController
    {
        

        private TecnicaEntities db = new TecnicaEntities();

        // GET: api/Mascotas
        public IQueryable<Mascota> GetMascota()
        {
            return db.Mascota;
        }

        // GET: api/Mascotas/5
        [ResponseType(typeof(Mascota))]
        public IHttpActionResult GetMascota(int id)
        {
            Mascota mascota = db.Mascota.Find(id);
            if (mascota == null)
            {
                return NotFound();
            }

            return Ok(mascota);
        }

        // PUT: api/Mascotas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMascota(int id, Mascota mascota)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mascota.ID)
            {
                return BadRequest();
            }

            db.Entry(mascota).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MascotaExists(id))
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

        // POST: api/Mascotas
        [ResponseType(typeof(Mascota))]
        public IHttpActionResult PostMascota(Mascota mascota)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Mascota.Add(mascota);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = mascota.ID }, mascota);
        }

        // DELETE: api/Mascotas/5
        [ResponseType(typeof(Mascota))]
        public IHttpActionResult DeleteMascota(int id)
        {
            Mascota mascota = db.Mascota.Find(id);
            if (mascota == null)
            {
                return NotFound();
            }

            db.Mascota.Remove(mascota);
            db.SaveChanges();

            return Ok(mascota);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MascotaExists(int id)
        {
            return db.Mascota.Count(e => e.ID == id) > 0;
        }
    }
}