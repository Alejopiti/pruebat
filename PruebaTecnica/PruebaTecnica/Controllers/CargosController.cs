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
    public class CargosController : ApiController
    {
        private pruebatecnicaEntities db = new pruebatecnicaEntities();

        // GET: api/Cargos
        public IQueryable<cargos> Getcargos()
        {
            return db.cargos;
        }

        // GET: api/Cargos/5
        [ResponseType(typeof(cargos))]
        public IHttpActionResult Getcargos(int id)
        {
            cargos cargos = db.cargos.Find(id);
            if (cargos == null)
            {
                return NotFound();
            }

            return Ok(cargos);
        }

        // PUT: api/Cargos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcargos(int id, cargos cargos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cargos.idcargo)
            {
                return BadRequest();
            }

            db.Entry(cargos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!cargosExists(id))
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

        // POST: api/Cargos
        [ResponseType(typeof(cargos))]
        public IHttpActionResult Postcargos(cargos cargos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.cargos.Add(cargos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cargos.idcargo }, cargos);
        }

        // DELETE: api/Cargos/5
        [ResponseType(typeof(cargos))]
        public IHttpActionResult Deletecargos(int id)
        {
            cargos cargos = db.cargos.Find(id);
            if (cargos == null)
            {
                return NotFound();
            }

            db.cargos.Remove(cargos);
            db.SaveChanges();

            return Ok(cargos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool cargosExists(int id)
        {
            return db.cargos.Count(e => e.idcargo == id) > 0;
        }
    }
}