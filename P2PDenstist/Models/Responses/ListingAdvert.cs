using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P2PDenstist.Models.Responses
{
    public class ListingAdvert
    {
        public string listingAdvertID { get; set; }
        public string transactionID { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string imageURL { get; set; }
        public string linkURL { get; set; }
        public string listweight { get; set; }
        public string userID { get; set; }
        public string lng { get; set; }
        public string lat { get; set; }
        public string cDate { get; set; }
    }
}