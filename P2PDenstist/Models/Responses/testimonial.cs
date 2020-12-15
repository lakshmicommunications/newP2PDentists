using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P2PDenstist.Models.Responses
{
    public class testimonial
    {
        public string testmonialID { get; set; }
        public string profileID { get; set; }
        public string testmonialText { get; set; }
        public string addedby { get; set; }
        public string city { get; set; }
        public string cdate { get; set; }
    }
}