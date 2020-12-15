using P2PDenstist.Connector;
using P2PDenstist.Models.Requests;
using P2PDenstist.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace P2PDenstist.Controllers
{
    public class CategoryController : ApiController
    {
        [HttpPost]
        [Obsolete]
        public SpecialCategoryAddedResponse specializationMasteradded(SpeciazationMaster speciazationMaster)
        {
            SpecialCategoryAddedResponse specialCategoryAddedResponse = new SpecialCategoryAddedResponse();
            CategoryRepository categoryRepository = new CategoryRepository();
            specialCategoryAddedResponse = categoryRepository.SpecializationAdd(speciazationMaster);
            return specialCategoryAddedResponse;
        }

       
    }
}
