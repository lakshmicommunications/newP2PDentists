using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P2PDenstist.Models.Responses
{
    public class DoctorAddedResponseModel
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public string doctorID { get; set; }
        
    }
}