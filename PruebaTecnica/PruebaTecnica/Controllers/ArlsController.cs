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
    public class ArlsController : ApiController
    {
        private pruebatecnicaEntities db = new pruebatecnicaEntities();

        // GET: api/Arls
        public IQueryable<arl> Getarl()
        {
            return db.arl;
        }

        // GET: api/Arls/5
        [ResponseType(typeof(arl))]
        public IHttpActionResult Getarl(int id)
        {
            arl arl = db.arl.Find(id);
            if (arl == null)
            {
                return NotFound();
            }

            return Ok(arl);
        }

        // PUT: api/Arls/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putarl(int id, arl arl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != arl.idarl)
            {
                return BadRequest();
            }

            db.Entry(arl).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!arlExists(id))
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

        // POST: api/Arls
        [ResponseType(typeof(arl))]
        public IHttpActionResult Postarl(arl arl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.arl.Add(arl);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = arl.idarl }, arl);
        }

        // DELETE: api/Arls/5
        [ResponseType(typeof(arl))]
        public IHttpActionResult Deletearl(int id)
        {
            arl arl = db.arl.Find(id);
            if (arl == null)
            {
                return NotFound();
            }

            db.arl.Remove(arl);
            db.SaveChanges();

            return Ok(arl);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool arlExists(int id)
        {
            return db.arl.Count(e => e.idarl == id) > 0;
        }
    }
}