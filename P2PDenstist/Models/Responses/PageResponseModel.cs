﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P2PDenstist.Models
{
    public class PageResponseModel
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public List<Pagelayouts>pagelayouts { get; set; }
    }
}