﻿using P2PDenstist.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P2PDenstist.Models.Responses
{
    public class ProfileDetailsResponseModel
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public List<UserDetails>userDetails { get; set; }
    }
}