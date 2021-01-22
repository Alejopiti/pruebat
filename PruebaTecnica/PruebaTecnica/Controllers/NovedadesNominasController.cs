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
using PruebaTecnica.Models;

namespace PruebaTecnica.Controllers
{
    public class NovedadesNominasController : ApiController
    {
        private pruebatecnicaEntities db = new pruebatecnicaEntities();

        // GET: api/NovedadesNominas
        public IQueryable<novedadesnomina> Getnovedadesnomina()
        {
            return db.novedadesnomina;
        }

        // GET: api/NovedadesNominas/5
        [ResponseType(typeof(novedadesnomina))]
        public IHttpActionResult Getnovedadesnomina(int id)
        {
            novedadesnomina novedadesnomina = db.novedadesnomina.Find(id);
            if (novedadesnomina == null)
            {
                return NotFound();
            }

            return Ok(novedadesnomina);
        }

        // PUT: api/NovedadesNominas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putnovedadesnomina(int id, novedadesnomina novedadesnomina)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != novedadesnomina.idnovedadnomina)
            {
                return BadRequest();
            }

            db.Entry(novedadesnomina).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!novedadesnominaExists(id))
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

        // POST: api/NovedadesNominas
        [ResponseType(typeof(novedadesnomina))]
        public IHttpActionResult Postnovedadesnomina(novedadesnomina novedadesnomina)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.novedadesnomina.Add(novedadesnomina);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = novedadesnomina.idnovedadnomina }, novedadesnomina);
        }

        // DELETE: api/NovedadesNominas/5
        [ResponseType(typeof(novedadesnomina))]
        public IHttpActionResult Deletenovedadesnomina(int id)
        {
            novedadesnomina novedadesnomina = db.novedadesnomina.Find(id);
            if (novedadesnomina == null)
            {
                return NotFound();
            }

            db.novedadesnomina.Remove(novedadesnomina);
            db.SaveChanges();

            return Ok(novedadesnomina);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool novedadesnominaExists(int id)
        {
            return db.novedadesnomina.Count(e => e.idnovedadnomina == id) > 0;
        }
    }
}