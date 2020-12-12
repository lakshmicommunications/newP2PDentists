using P2PDenstist.Connector;
using P2PDenstist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace P2PDenstist.Controllers
{
    public class PageLayoutController : ApiController
    {
        [HttpGet]
        public PageResponseModel getHomePage(string domainname,string pageno)
        {
            PageResponseModel pageResponseModel = new PageResponseModel();
            List<Pagelayouts> pagelayouts = new List<Pagelayouts>();
            PageRepository pageRepository = new PageRepository();
            pagelayouts = pageRepository.getHomePage(domainname, pageno);
            if (pagelayouts.Count <= 0)
            {
                pageResponseModel.responseCode = "200";
                pageResponseModel.responseMessage = "No data found";
                pageResponseModel.pagelayouts = pagelayouts;
            }
            else
            {
                pageResponseModel.responseCode = "200";
                pageResponseModel.responseMessage = "Home page details";
                pageResponseModel.pagelayouts = pagelayouts;
            }
            return pageResponseModel;
        }
    }
}
