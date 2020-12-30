using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P2PDenstist.Models.Requests
{
    public class AddBannerRequestModel
    {
        public string bannerID { get; set; }
        public string profileID { get; set; }
        public string bannerURl { get; set; }
        public string pageID { get; set; }
    }
}