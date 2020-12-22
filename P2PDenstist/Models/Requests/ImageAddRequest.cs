using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P2PDenstist.Models.Requests
{
    public class ImageAddRequest
    {
        public string imageID { get; set; }
        public string imageURL { get; set; }
        public string profileID { get; set; }
    }
}