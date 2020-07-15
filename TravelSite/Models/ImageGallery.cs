using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelSite.Models
{
    public class ImageGallery
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public DateTime Data { get; set; }
        public Guid TripId { get; set; }
        public byte[] Image { get; set; }
    }
}
