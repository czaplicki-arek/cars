using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarStorage.Models
{
    public class Car : BaseEntity
    {
        public string Name { get; set; }
        public Brand Brand { get; set; }
    }
}