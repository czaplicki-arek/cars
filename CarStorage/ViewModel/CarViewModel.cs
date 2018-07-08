using CarStorage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarStorage.ViewModel
{
    public class CarViewModel
    {
        public class Car
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public int Id { get; set; }
        public string Brand { get; set; }
        public string Country { get; set; }
        public Prestige Prestige { get; set; }

        public List<Car> Cars { get; set; }
    }
}