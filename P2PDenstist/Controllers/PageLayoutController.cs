using P2PDenstist.Connector;
using P2PDenstist.Models;
using P2PDenstist.Models.Responses;
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
            pagelayouts = pageRepository.homeListDetails(domainname, pageno);
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

        [HttpGet]
        public ListingAdvertResponse getFeaturedAdverts(string domainName,string pagenumber)
        {
            ListingAdvertResponse listingAdvertResponse = new ListingAdvertResponse();
            List<ListingAdvert> listingAdvert = new List<ListingAdvert>();
            PageRepository pageRepository = new PageRepository();
            listingAdvert = pageRepository.listingAdvertList(domainName, pagenumber);
            if (listingAdvert.Count <= 0)
            {
                listingAdvertResponse.responseCode = "200";
                listingAdvertResponse.responseMessage = "No data found";
                listingAdvertResponse.listingAdvert = listingAdvert;
            }
            else
            {
                listingAdvertResponse.responseCode = "200";
                listingAdvertResponse.responseMessage = "Listing details";
                listingAdvertResponse.listingAdvert = listingAdvert;
            }
            return listingAdvertResponse;
        }

        [HttpGet]
        public StripeDetailsModel stripeDetails()
        {
            StripeDetailsModel stripeDetails = new StripeDetailsModel();
            PageRepository pageRepository = new PageRepository();
            stripeDetails = pageRepository.stripeDetails();
            return stripeDetails;
        }


        /*[HttpPost]
        [Obsolete]
        public PageAddedResponse postHomePage(Pagelayouts pagelayouts)
        {
            PageAddedResponse pageAddedResponse = new PageAddedResponse();
            PageRepository pageRepository = new PageRepository();
            pageAddedResponse = pageRepository.addedPageDetail(pagelayouts);
            return pageAddedResponse;
        }*/
    }
}
