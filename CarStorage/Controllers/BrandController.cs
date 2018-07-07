using CarStorage.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarStorage.Controllers
{
    public class BrandController : ApiController
    {
        public IEnumerable<Brand> Get()
        {
            using(var db = new CarDbContext())
            {
                return db.Brands.AsEnumerable();
            }
        }

        // GET: api/Car/5
        public Brand Get(int id)
        {
            using (var db = new CarDbContext())
            {
                return db.Brands.Find(id);
            }
        }

        // POST: api/Car
        public void Post([FromBody]Brand value)
        {
            using (var db = new CarDbContext())
            {
                var brand = db.Brands.Add(value);
                db.SaveChanges();
            }
        }

        // PUT: api/Car/5
        public void Put(int id, [FromBody]Brand value)
        {
            value.Id = id;
            using (var db = new CarDbContext())
            {
                db.Entry(value).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        // DELETE: api/Car/5
        public void Delete(int id)
        {
            using (var db = new CarDbContext())
            {
                var brand = db.Brands.Find(id);
                db.Brands.Remove(brand);
                db.SaveChanges();
            }
        }
    }
}
