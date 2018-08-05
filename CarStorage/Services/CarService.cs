using CarStorage.Models;
using CarStorage.Repositories;
using CarStorage.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarStorage.Services
{
    public class CarService : IDisposable
    {
        UnitOfWork db;
        public CarService( CarDbContext context)
        {
            this.db = new UnitOfWork(context);
        }
        public void Change(CarViewModel value)
        {
            var brand = new Brand { Id = value.Id, Country = value.Country, Name = value.Brand, Prestige = value.Prestige };

            if (brand.Id > 0)
                db.BrandRepository.Update(brand);
            else
                db.BrandRepository.Insert(brand);

            foreach (var car in value.Cars)
            {
                var carEntity = new Car { Id = car.Id, Name = car.Name, Brand = brand };
                if (car.Id > 0)
                    db.CarRepository.Update(carEntity);
                else
                    db.CarRepository.Insert(carEntity);
            }

            db.Save();
        }

        public void Delete(object id)
        {
            db.BrandRepository.Delete(id);
            db.Save();
        }

        public Brand GetBrand(int id)
        {
            return db.BrandRepository.GetByID(id, "Cars");
        }

        public Car GetCar(int id)
        {
            return db.CarRepository.GetByID(id);
        }

        public IEnumerable<Brand> GetBrandByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return db.BrandRepository.Get(null, null, "Cars");

            return db.BrandRepository.Get(x => x.Name.Contains(name), null, "Cars");
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}