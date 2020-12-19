using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P2PDenstist.Models.Requests
{
    public class TestimonialsRequest
    {
        public string testmonialId { get; set; }
        public string profileId { get; set; }
        public string testmonialText { get; set; }
        public string addedby { get; set; }
        public string city { get; set; }
        public string currentDate { get; set; }
    }
}