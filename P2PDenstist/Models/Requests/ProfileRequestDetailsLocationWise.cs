using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P2PDenstist.Models.Requests
{
    public class ProfileRequestDetailsLocationWise
    {
        public string profileID { get; set; }
        public string userID { get; set; }
        public string BusinessName { get; set; }
        public string address { get; set; }
        public string address1 { get; set; }
        public string city { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
    }
}