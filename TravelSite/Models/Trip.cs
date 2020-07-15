using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelSite.Models
{
    public class Trip
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public Guid IdMainPhoto { get; set; }
        public DateTime DataStart { get; set; }
        public DateTime DataFinish { get; set; }
    }
}
