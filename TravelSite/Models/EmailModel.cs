using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelSite.Models
{
    public class EmailModel
    {
        public string Email { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }

    public class Email
    {
        public EmailModel Emails { get; set; }
        public List<Trip> Trips { get; set; }
    }
}
