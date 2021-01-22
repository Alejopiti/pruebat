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
    public class ContratosLaboralesController : ApiController
    {
        private pruebatecnicaEntities db = new pruebatecnicaEntities();

        // GET: api/ContratosLaborales
        public IQueryable<contratoslaborales> Getcontratoslaborales()
        {
            return db.contratoslaborales;
        }

        // GET: api/ContratosLaborales/5
        [ResponseType(typeof(contratoslaborales))]
        public IHttpActionResult Getcontratoslaborales(int id)
        {
            contratoslaborales contratoslaborales = db.contratoslaborales.Find(id);
            if (contratoslaborales == null)
            {
                return NotFound();
            }

            return Ok(contratoslaborales);
        }

        // PUT: api/ContratosLaborales/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcontratoslaborales(int id, contratoslaborales contratoslaborales)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contratoslaborales.idcontrato)
            {
                return BadRequest();
            }

            db.Entry(contratoslaborales).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!contratoslaboralesExists(id))
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

        // POST: api/ContratosLaborales
        [ResponseType(typeof(contratoslaborales))]
        public IHttpActionResult Postcontratoslaborales(contratoslaborales contratoslaborales)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.contratoslaborales.Add(contratoslaborales);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = contratoslaborales.idcontrato }, contratoslaborales);
        }

        // DELETE: api/ContratosLaborales/5
        [ResponseType(typeof(contratoslaborales))]
        public IHttpActionResult Deletecontratoslaborales(int id)
        {
            contratoslaborales contratoslaborales = db.contratoslaborales.Find(id);
            if (contratoslaborales == null)
            {
                return NotFound();
            }

            db.contratoslaborales.Remove(contratoslaborales);
            db.SaveChanges();

            return Ok(contratoslaborales);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool contratoslaboralesExists(int id)
        {
            return db.contratoslaborales.Count(e => e.idcontrato == id) > 0;
        }
    }
}