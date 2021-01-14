using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P2PDenstist.Models.Requests
{
    public class GeneralInfoModel
    {
        public string profileID { get; set; }
        public string userID { get; set; }
        public string businessName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string salutation { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string postal { get; set; }
       
    }
}