using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CarStorage.Models
{
    public class CarDbContext : DbContext 
    {
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Car> Cars { get; set; }

        public CarDbContext()
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().HasRequired(s => s.Brand);

            base.OnModelCreating(modelBuilder);
        }
    }
}