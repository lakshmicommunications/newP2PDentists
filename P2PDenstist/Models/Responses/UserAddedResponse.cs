using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P2PDenstist.Models.Responses
{
    public class UserAddedResponse
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public string userID { get; set; }
    }
}