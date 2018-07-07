using CarStorage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarStorage.Controllers
{
    public class BrandController : ApiController
    {
        public IEnumerable<Models.Brand> Get()
        {
            using(var db = new Models.CarDbContext())
            {
                return new List<Brand>();// db.Brands.AsEnumerable();
            }
        }

        // GET: api/Car/5
        public string Get(int id)
        {
            return AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
        }

        // POST: api/Car
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Car/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Car/5
        public void Delete(int id)
        {
        }
    }
}
