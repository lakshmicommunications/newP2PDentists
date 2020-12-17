using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P2PDenstist.Models.Requests
{
    public class PasswordUpdateRequest
    {
        public string userID { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
    }
}