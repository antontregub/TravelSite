using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelSite.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Mark { get; set; }
        public string Text { get; set; }
        public Guid TravelId { get; set; }
        public DateTime Data { get; set; }
        public Guid ParentId { get; set; }
    }

    public class ReviewAndTrip
    {
        public Trip Trip { get; set; }
        public Review NewReview { get; set; }
    }
}
