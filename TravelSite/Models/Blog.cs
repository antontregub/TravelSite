using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TravelSite.Models
{
    public class Blog
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ShortContent { get; set; }
        public string Content { get; set; }
        public string Tag { get; set; }
        public  byte[] MainPhoto { get; set; }
        public DateTime Date { get; set; }

        public List<Review> Reviews { get; set; }
    }

    public class BlogCreate
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ShortContent { get; set; }
        public string Content { get; set; }
        public string Tag { get; set; }
        public IFormFile MainPhoto { get; set; }
        public DateTime Date { get; set; }

        public List<Review> Reviews { get; set; }
    }
}
