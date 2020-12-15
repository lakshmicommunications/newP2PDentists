using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P2PDenstist.Models.Responses
{
    public class PageAddedResponse
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public string pageID { get; set; }
        public List<Pagelayouts>pagelayouts { get; set; }
    }
}