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
    public class TipoDocumentosController : ApiController
    {
        private pruebatecnicaEntities db = new pruebatecnicaEntities();

        // GET: api/TipoDocumentos
        public IQueryable<tipodocumento> Gettipodocumento()
        {
            return db.tipodocumento;
        }

        // GET: api/TipoDocumentos/5
        [ResponseType(typeof(tipodocumento))]
        public IHttpActionResult Gettipodocumento(int id)
        {
            tipodocumento tipodocumento = db.tipodocumento.Find(id);
            if (tipodocumento == null)
            {
                return NotFound();
            }

            return Ok(tipodocumento);
        }

        // PUT: api/TipoDocumentos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttipodocumento(int id, tipodocumento tipodocumento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipodocumento.idtipodocumento)
            {
                return BadRequest();
            }

            db.Entry(tipodocumento).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tipodocumentoExists(id))
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

        // POST: api/TipoDocumentos
        [ResponseType(typeof(tipodocumento))]
        public IHttpActionResult Posttipodocumento(tipodocumento tipodocumento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tipodocumento.Add(tipodocumento);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tipodocumento.idtipodocumento }, tipodocumento);
        }

        // DELETE: api/TipoDocumentos/5
        [ResponseType(typeof(tipodocumento))]
        public IHttpActionResult Deletetipodocumento(int id)
        {
            tipodocumento tipodocumento = db.tipodocumento.Find(id);
            if (tipodocumento == null)
            {
                return NotFound();
            }

            db.tipodocumento.Remove(tipodocumento);
            db.SaveChanges();

            return Ok(tipodocumento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tipodocumentoExists(int id)
        {
            return db.tipodocumento.Count(e => e.idtipodocumento == id) > 0;
        }
    }
}