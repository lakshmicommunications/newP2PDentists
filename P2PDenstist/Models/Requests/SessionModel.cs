using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P2PDenstist.Models.Requests
{
    public class SessionModel
    {
        public string sessionID { get; set; }
        public string sessionToken { get; set; }
        public string sessionDatetime { get; set; }
    }
}