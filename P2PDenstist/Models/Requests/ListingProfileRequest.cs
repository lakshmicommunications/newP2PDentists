using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P2PDenstist.Models.Requests
{
    public class ListingProfileRequest
    {  
        public string profileID { get; set; }
        public string userID { get; set; }
        public string businessname { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string salutation { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string address1 { get; set; }
        public string city{ get; set; }
        public string state{ get; set; }
        public string country{ get; set; }
        public string postCode{ get; set; }
        public string hasp2p{ get; set; }
        public string hasVideocall{ get; set; }
        public string WebsiteURL{ get; set; }
        public string VideoURL{ get; set; }
        public string logoURL{ get; set; }
        public string lng{ get; set; }
        public string lat{ get; set; }
        public string hrs{ get; set; }
        public string start_rating{ get; set; }
        public string open_now{ get; set; }
        public string available_now{ get; set; }
        public string subcription_id{ get; set; }
        public string imageURL{ get; set; }
        public string listWeight { get; set; }
        public string currentDate { get; set; }
    }
}