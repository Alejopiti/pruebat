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
    public class EstadosController : ApiController
    {
        private pruebatecnicaEntities db = new pruebatecnicaEntities();

        // GET: api/Estados
        public IQueryable<estados> Getestados()
        {
            return db.estados;
        }

        // GET: api/Estados/5
        [ResponseType(typeof(estados))]
        public IHttpActionResult Getestados(int id)
        {
            estados estados = db.estados.Find(id);
            if (estados == null)
            {
                return NotFound();
            }

            return Ok(estados);
        }

        // PUT: api/Estados/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putestados(int id, estados estados)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != estados.idestado)
            {
                return BadRequest();
            }

            db.Entry(estados).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!estadosExists(id))
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

        // POST: api/Estados
        [ResponseType(typeof(estados))]
        public IHttpActionResult Postestados(estados estados)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.estados.Add(estados);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = estados.idestado }, estados);
        }

        // DELETE: api/Estados/5
        [ResponseType(typeof(estados))]
        public IHttpActionResult Deleteestados(int id)
        {
            estados estados = db.estados.Find(id);
            if (estados == null)
            {
                return NotFound();
            }

            db.estados.Remove(estados);
            db.SaveChanges();

            return Ok(estados);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool estadosExists(int id)
        {
            return db.estados.Count(e => e.idestado == id) > 0;
        }
    }
}