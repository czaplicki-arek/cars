using CarStorage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarStorage.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private CarDbContext context;
        private GenericRepository<Car> carRepository;
        private GenericRepository<Brand> brandRepository;

        public UnitOfWork(CarDbContext context)
        {
            this.context = context;
        }

        public GenericRepository<Car> CarRepository
        {
            get
            {
                if (this.carRepository == null)
                {
                    this.carRepository = new GenericRepository<Car>(context);
                }
                return carRepository;
            }
        }

        public GenericRepository<Brand> BrandRepository
        {
            get
            {

                if (this.brandRepository == null)
                {
                    this.brandRepository = new GenericRepository<Brand>(context);
                }
                return brandRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}