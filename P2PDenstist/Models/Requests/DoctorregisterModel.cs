using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P2PDenstist.Models.Requests
{
    public class DoctorregisterModel
    {
        public string doctorID { get; set; }
        public string clinicName { get; set; }
        public string phone { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string subscription_status { get; set; }
    }
}