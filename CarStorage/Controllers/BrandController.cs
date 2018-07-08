using CarStorage.Models;
using CarStorage.Repositories;
using CarStorage.Services;
using CarStorage.ViewModel;
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
        CarDbContext db = new CarDbContext();

        public IEnumerable<Brand> Get()
        {
            using (var srv = new CarService(db))
            {
                return srv.GetBrandByName("Volvo");
            }
        }

        // GET: api/Car/5
        public Brand Get(int id)
        {
            using (var srv = new CarService(db))
            {
                return srv.GetBrand(id);
            }
        }

        // POST: api/Car
        public void Post([FromBody]CarViewModel value)
        {
            using (var srv = new CarService(db))
            {
                srv.Change(value);
            }
        }

        // PUT: api/Car/5
        public void Put(int id, [FromBody]CarViewModel value)
        {
            value.Id = id;
            using (var srv = new CarService(db))
            {
                srv.Change(value);
            }
        }

        // DELETE: api/Car/5
        public void Delete(int id)
        {
            using (var srv = new CarService(db))
            {
                srv.Delete(id);
            }
        }
    }
}
