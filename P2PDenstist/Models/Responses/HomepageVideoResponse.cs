using P2PDenstist.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P2PDenstist.Models.Responses
{
    public class HomepageVideoResponse
    {
        public string ressponseCode { get; set; }
        public string ressponseMessage { get; set; }
        public List<HomepageVideolist>homepageVideolists { get; set; }
    }
}