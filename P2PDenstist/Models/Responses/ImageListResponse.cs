﻿using P2PDenstist.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P2PDenstist.Models.Responses
{
    public class ImageListResponse
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public List<ImageAddRequest> imageListDetails { get; set; }
    }
}