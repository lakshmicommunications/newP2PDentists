using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P2PDenstist.Models.Responses
{
    public class ListProfileListResponse
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public List<ListingAdvert> listingAdvert { get; set; }
        public List<testimonial> testimonials { get; set; }
        public List<SpeciazationModel> speciazations { get; set; }
    }
}