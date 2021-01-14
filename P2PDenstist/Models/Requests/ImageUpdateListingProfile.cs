using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P2PDenstist.Models.Requests
{
    public class ImageUpdateListingProfile
    {
        public string profileID
        {
            get;set;
        }

        public string imageURL
        {
            get;set;
        }


        public string sessionToken { get; set; }

    }
}