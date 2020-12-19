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
    public class UserController : ApiController
    {
        [HttpPost]
        [Obsolete]
        public UserAddedResponse signupNewUser(UserDetails userDetails)
        {
            UserAddedResponse userAdded = new UserAddedResponse();
            UserRepository userRepository = new UserRepository();
            userAdded = userRepository.usersadded(userDetails);
            return userAdded;
            
        }

        [HttpGet]
        public ListProfileListResponse getListingProfile(string domainname,string pagenumber)
        {
            ListProfileListResponse listProfileListResponse = new ListProfileListResponse();
            PageRepository pageRepository = new PageRepository();
            listProfileListResponse.responseCode = "200";
            listProfileListResponse.responseMessage = "getListprofile";
            listProfileListResponse.listingAdvert = pageRepository.listingAdvertList(domainname, pagenumber);
            listProfileListResponse.testimonials = pageRepository.testimonials(domainname, pagenumber);
            listProfileListResponse.speciazations = pageRepository.speciazations(domainname, pagenumber);
            return listProfileListResponse;
        }


        

        [HttpGet]
        public ProfileDetailsResponseModel claimListing()
        {
            ProfileDetailsResponseModel profileDetailsResponse = new ProfileDetailsResponseModel();
            List<UserDetails> userDetails = new List<UserDetails>();
            UserRepository userRepository = new UserRepository();
            userDetails = userRepository.userDetails();
            if (userDetails.Count <= 0)
            {
                profileDetailsResponse.responseCode = "200";
                profileDetailsResponse.responseMessage = "No data found";
                profileDetailsResponse.userDetails = userDetails;
            }
            else
            {
                profileDetailsResponse.responseCode = "200";
                profileDetailsResponse.responseMessage = "user details";
                profileDetailsResponse.userDetails = userDetails;
            }
            return profileDetailsResponse;
        }

        [HttpPost]
        public userValidationResponseModel validateUser(LoginRequestModel loginRequestModel)
        {
            userValidationResponseModel userValidationResponseModel = new userValidationResponseModel();
            UserRepository userRepository = new UserRepository();
            userValidationResponseModel.userDetails = userRepository.UserDetailsvalidate(loginRequestModel);
            if (userValidationResponseModel.userDetails.Count <= 0)
            {
                userValidationResponseModel.userDetails = userRepository.UserDetailsvalidate(loginRequestModel);
                userValidationResponseModel.responseCode = "200";
                userValidationResponseModel.responseMesssage = "Login failed";
            }
            else
            {
                userValidationResponseModel.userDetails = userRepository.UserDetailsvalidate(loginRequestModel);
                userValidationResponseModel.responseCode = "200";
                userValidationResponseModel.responseMesssage = "Login Successfully";
            }
            return userValidationResponseModel;
        }
        [HttpPost]
        public PasswordUpdateResponse updateProfile(PasswordUpdateRequest passwordUpdate)
        {
            PasswordUpdateResponse response = new PasswordUpdateResponse();
            UserRepository userRepository = new UserRepository();
            response = userRepository.passwordUpdateResponse(passwordUpdate);
            response.responseCode = "200";
            response.responseMessage = " Updated username and password successfully";
            return response;
        }
    }
}
