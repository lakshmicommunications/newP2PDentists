using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P2PDenstist.Models.Requests
{
    public class UserDetails
    {
        public string userID { get; set; }
        public string profileID { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string userRole { get; set; }
        public string lastLogin { get; set; }
        public string imageURL{ get; set; }
        public string logoURL{ get; set; }
        public string cDate{ get; set; }
    }
}