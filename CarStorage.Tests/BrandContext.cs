using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarStorage.Repositories;
using CarStorage.Models;
using Moq;
using System.Data.Entity;
using CarStorage.Services;
using System.Linq;

namespace CarStorage.Tests
{
    /// <summary>
    /// Summary description for BrandContext
    /// </summary>
    [TestClass]
    public class BrandContext
    {
        public BrandContext()
        {
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
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

        [TestMethod]
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