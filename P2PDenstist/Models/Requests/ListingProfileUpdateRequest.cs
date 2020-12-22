using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P2PDenstist.Models.Requests
{
    public class ListingProfileUpdateRequest
    {
        public string profileID { get; set; }
        public string saluation { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address{ get; set; }
        public string address1{ get; set; }
        public string city{ get; set; }
        public string province{ get; set; }
        public string country{ get; set; }
        public string postalcode{ get; set; }
    }
}