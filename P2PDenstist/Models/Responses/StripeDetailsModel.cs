using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P2PDenstist.Models.Responses
{
    public class StripeDetailsModel
    {
        public string responseCode { get; set; }
        public string responsemessage { get; set; }
        public string secretKey { get; set; }
        public string accessKey { get; set; }
    }
}