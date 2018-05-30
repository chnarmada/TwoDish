using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RestaurantApp.Models;

namespace RestaurantApp.Controllers
{
    public class AppDataController : ApiController
    {
        private RestaurantDB db = new RestaurantDB();

        // GET: api/AppData
        public IQueryable<AppData> GetAppData()
        {
            return db.AppData;
        }

        // GET: api/AppData/5
        [ResponseType(typeof(AppData))]
        public async Task<IHttpActionResult> GetAppData(int id)
        {
            AppData appData = await db.AppData.FindAsync(id);
            if (appData == null)
            {
                return NotFound();
            }

            return Ok(appData);
        }

        // PUT: api/AppData/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAppData(int id, AppData appData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appData.DishId)
            {
                return BadRequest();
            }

            db.Entry(appData).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppDataExists(id))
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

        // POST: api/AppData
        [ResponseType(typeof(AppData))]
        public async Task<IHttpActionResult> PostAppData(AppData appData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AppData.Add(appData);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AppDataExists(appData.DishId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = appData.DishId }, appData);
        }

        // DELETE: api/AppData/5
        [ResponseType(typeof(AppData))]
        public async Task<IHttpActionResult> DeleteAppData(int id)
        {
            AppData appData = await db.AppData.FindAsync(id);
            if (appData == null)
            {
                return NotFound();
            }

            db.AppData.Remove(appData);
            await db.SaveChangesAsync();

            return Ok(appData);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AppDataExists(int id)
        {
            return db.AppData.Count(e => e.DishId == id) > 0;
        }
    }
}