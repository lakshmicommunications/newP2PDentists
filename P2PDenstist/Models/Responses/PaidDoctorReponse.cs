using P2PDenstist.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P2PDenstist.Models.Responses
{
    public class PaidDoctorReponse
    {
        public string responseCode { get; set; }
        public string responsemessage { get; set; }

        public List<DoctorregisterModel> doctorregisterModels { get; set; }
    }
}