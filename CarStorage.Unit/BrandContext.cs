using CarStorage.Models;
using CarStorage.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStorage.Unit
{
    [TestFixture]
    public class BrandContext
    {
        [Test]
        public void TestRepository()
        {
            var mockBrand = new Mock<DbSet<Brand>>();
            var mockCar = new Mock<DbSet<Car>>();

            var mockContext = new Mock<CarDbContext>();
            mockContext.Setup(m => m.Brands).Returns(mockBrand.Object);
            mockContext.Setup(m => m.Cars).Returns(mockCar.Object);

            mockContext.Setup(c => c.Set<Brand>()).Returns(mockBrand.Object);
            mockContext.Setup(c => c.Set<Car>()).Returns(mockCar.Object);

            var brandService = new CarService(mockContext.Object);
            brandService.Change(new ViewModel.CarViewModel()
            {
                Id = 12,
                Brand = "Fiat",
                Country = "Poland",
                Prestige = Prestige.VeryHigh,
                Cars = new List<ViewModel.CarViewModel.Car>()
                {
                    new ViewModel.CarViewModel.Car(){ Id = 2,  Name = "Maluch" },
                    new ViewModel.CarViewModel.Car(){  Name = "Duży Fiat" },
                    new ViewModel.CarViewModel.Car(){  Name = "Polonez" },
                }
            });

            mockBrand.Verify(m => m.Attach(It.IsAny<Brand>()), Times.Once());

            mockCar.Verify(m => m.Add(It.IsAny<Car>()), Times.Exactly(2));
            mockCar.Verify(m => m.Attach(It.IsAny<Car>()), Times.Exactly(1));

            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void TestRepositoryGetValues()
        {
            var mockBrand = new Mock<DbSet<Brand>>();

            var queryableList = new List<Brand>
            {
               new Brand { Name = "Fiat" },
               new Brand { Name = "Volvo" }
            }.AsQueryable();
            mockBrand.As<IQueryable<Brand>>().Setup(m => m.Provider).Returns(queryableList.Provider);
            mockBrand.As<IQueryable<Brand>>().Setup(m => m.Expression).Returns(queryableList.Expression);
            mockBrand.As<IQueryable<Brand>>().Setup(m => m.ElementType).Returns(queryableList.ElementType);
            mockBrand.As<IQueryable<Brand>>().Setup(m => m.GetEnumerator()).Returns(queryableList.GetEnumerator());

            var mockContext = new Mock<CarDbContext>();
            mockContext.Setup(m => m.Brands).Returns(mockBrand.Object);

            mockContext.Setup(c => c.Set<Brand>()).Returns(mockBrand.Object);

            var brandService = new CarService(mockContext.Object);

            Assert.AreEqual(brandService.GetBrandByName("Fiat").Count(), 1);
            Assert.AreEqual(brandService.GetBrandByName("Mazda").Count(), 0);
        }
    }
}
