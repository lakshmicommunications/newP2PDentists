using P2PDenstist.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P2PDenstist.Models.Responses
{
    public class userValidationResponseModel
    {
        public string responseCode { get; set; }
        public string responseMesssage { get; set; }
        public List<UserDetails>userDetails { get; set; }
    }
}