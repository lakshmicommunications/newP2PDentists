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

        [HttpPost]
        public UpdateResponseModel updateSpecialization(SpeciazationMaster speciazationMaster)
        {
            UpdateResponseModel updateResponseModel = new UpdateResponseModel();
            CategoryRepository categoryRepository = new CategoryRepository();
            updateResponseModel = categoryRepository.updateResponseModel(speciazationMaster);
            updateResponseModel.responseCode = "200";
            updateResponseModel.message = "Updated successfully";
            return updateResponseModel;
        }

       [HttpGet]
       public PackageListDetailsModel packageList()
        {
            PackageListDetailsModel packageListmodel = new PackageListDetailsModel();
            List<PackageListmodel> packageListmodels = new List<PackageListmodel>();
            PageRepository pageRepository = new PageRepository();
            packageListmodels = pageRepository.packageListmodels();
            if (packageListmodels.Count <= 0)
            {
                packageListmodel.responseCode = "200";
                packageListmodel.responseMessage = "No data found";
                packageListmodel.packageListmodels = packageListmodels;
            }
            else
            {
                packageListmodel.responseCode = "200";
                packageListmodel.responseMessage = "Package List details";
                packageListmodel.packageListmodels = packageListmodels;
            }
            return packageListmodel;
        }

        [HttpGet]
        public SpecizationListDetailsModel getSpecializationList(string domainname, string pagenumber)
        {
            SpecizationListDetailsModel specizationListDetailsModel = new SpecizationListDetailsModel();
            PageRepository pageRepository = new PageRepository();
            specizationListDetailsModel.responseCode = "200";
            specizationListDetailsModel.responseMessage = "SpecializationDetails";
            specizationListDetailsModel.speciazationsMaster = pageRepository.speciazationMasters(domainname, pagenumber);
            specizationListDetailsModel.speciazationsCategory = pageRepository.speciazations(domainname, pagenumber);
            return specizationListDetailsModel;
        }
        
        [HttpGet]
        public SpecizationListDetailsModel getSpecialization(string domainname, string pagenumber)
        {
            SpecizationListDetailsModel specizationListDetailsModel = new SpecizationListDetailsModel();
            PageRepository pageRepository = new PageRepository();
            specizationListDetailsModel.responseCode = "200";
            specizationListDetailsModel.responseMessage = "SpecializationDetails";
            specizationListDetailsModel.speciazationsMaster = pageRepository.speciazationMasters(domainname, pagenumber);
            specizationListDetailsModel.speciazationsCategory = pageRepository.speciazations(domainname, pagenumber);
            return specizationListDetailsModel;
        }

        [HttpGet]
        public CategoryDetailsResponseModel specialCategoryListModel()
        {
            CategoryDetailsResponseModel categoryDetailsResponseModel = new CategoryDetailsResponseModel();
            List<SpeciazationModel> spCategory = new List<SpeciazationModel>();
            PageRepository pageRepository = new PageRepository();
            spCategory = pageRepository.CategorySpecial();
            if (spCategory.Count <= 0)
            {
                categoryDetailsResponseModel.responseCode = "200";
                categoryDetailsResponseModel.responseMessage = "Nodata found";
                categoryDetailsResponseModel.spCateogory = spCategory;
            }
            else
            {
                categoryDetailsResponseModel.responseCode = "200";
                categoryDetailsResponseModel.responseMessage = "Category special details";
                categoryDetailsResponseModel.spCateogory = spCategory;
            }
            return categoryDetailsResponseModel;
        }

        [HttpPost]
        [Obsolete]
        public TestimonialAddedResponse testimonialAdded(TestimonialsRequest testimonialsRequest)
        {
            TestimonialAddedResponse testimonialAddedResponse = new TestimonialAddedResponse();
            CategoryRepository categoryRepository = new CategoryRepository();
            testimonialAddedResponse = categoryRepository.testimonialAdded(testimonialsRequest);
            return testimonialAddedResponse;
        }

        [HttpPost]
        public UpdateResponseModel updateTestimonials(TestimonialsRequest testimonialsRequest)
        {
            UpdateResponseModel updateResponseModel = new UpdateResponseModel();
            CategoryRepository categoryRepository = new CategoryRepository();
            updateResponseModel = categoryRepository.testimonialsUpdated(testimonialsRequest);
            updateResponseModel.responseCode = "200";
            updateResponseModel.message = "Updated Testimonial details successfully";
            return updateResponseModel;
        }


    }
}
