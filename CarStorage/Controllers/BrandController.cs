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

        public IEnumerable<CarViewModel> Get(string name = null)
        {
            using (var srv = new CarService(db)) 
            {
                var brands = srv.GetBrandByName(name);

                return brands.Select(b => new CarViewModel
                {
                    Brand = b.Name,
                    Country = b.Country,
                    Prestige = b.Prestige,
                    Id = b.Id,
                    Cars = b.Cars.Select(c => new CarViewModel.Car
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).ToList()
                });
            }
        }

        // GET: api/Car/5
        public CarViewModel Get(int id)
        {
            using (var srv = new CarService(db))
            {
                var b = srv.GetBrand(id);

                return new CarViewModel
                {
                    Brand = b.Name,
                    Country = b.Country,
                    Prestige = b.Prestige,
                    Id = b.Id,
                    Cars = b.Cars.Select(c => new CarViewModel.Car
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).ToList()
                };
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
