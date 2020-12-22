using Newtonsoft.Json;
using P2PDenstist.Connector;
using P2PDenstist.Models.Requests;
using P2PDenstist.Models.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
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


        [HttpPost]
        public ListingProfileAddedResponse addedGeneralinfo(ListingProfileRequest listingProfileRequest)
        {
            ListingProfileAddedResponse listingProfileAddedResponse = new ListingProfileAddedResponse();
            UserRepository userRepository = new UserRepository();
            listingProfileAddedResponse = userRepository.addedResponse(listingProfileRequest);
            return listingProfileAddedResponse;
        }

        [HttpPost]
        public UpdateResponseModel updateGenralInfo(ListingProfileUpdateRequest profileUpdateRequest)
        {
            UpdateResponseModel updateResponseModel = new UpdateResponseModel();
            UserRepository userRepository = new UserRepository();
            updateResponseModel = userRepository.updateListingProfile(profileUpdateRequest);
            updateResponseModel.responseCode = "200";
            updateResponseModel.message = "Listing profile has been uploaded successfully.";
            return updateResponseModel;
        }


        [HttpGet]
        public UpdateResponseModel deleteLogo(String profileID)
        {
            UpdateResponseModel updateResponseModel = new UpdateResponseModel();
            UserRepository userRepository = new UserRepository();
            updateResponseModel = userRepository.imageDeleteListingURL(profileID);
            updateResponseModel.responseCode = "200";
            updateResponseModel.message = "Listing profile has been deleted successfully.";
            return updateResponseModel;

        }
        [HttpGet]
        public UpdateResponseModel deleteVideo(String profileID)
        {
            UpdateResponseModel updateResponseModel = new UpdateResponseModel();
            UserRepository userRepository = new UserRepository();
            updateResponseModel = userRepository.VideoDeleteListingURL(profileID);
            updateResponseModel.responseCode = "200";
            updateResponseModel.message = "Listing profile has been deleted successfully.";
            return updateResponseModel;

        }

        [HttpPost]
        public  async Task<UpdateResponseModel>updateLogo()
        {
            UpdateResponseModel updateResponseModel = new UpdateResponseModel();
            UserRepository userRepository = new UserRepository();
            string fileName = null, fileName1 = null, filePath = null;
            var httpRequest = HttpContext.Current.Request;
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    ImageUpdateListingProfile imageUpdateListingProfile = new ImageUpdateListingProfile();
                    var param = httpRequest.Params;
                    var ImageRequestDetails = param["logodetails"];
                    var updateValue = JsonConvert.DeserializeObject<ImageUpdateListingProfile>(ImageRequestDetails);

                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        fileName = Regex.Replace(postedFile.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault(), "[^A-Za-z0-9_. ]+", "");
                        fileName1 = Regex.Replace(fileName, " ", string.Empty);
                        var domainDir = HttpContext.Current.Server.MapPath("~/upload_images/" + updateValue.profileID + "/");
                        if (!Directory.Exists(domainDir))
                        {
                            Directory.CreateDirectory(domainDir);
                        }
                        filePath = HttpContext.Current.Server.MapPath("~/upload_images/" + updateValue.profileID + "/" +  fileName1);
                        postedFile.SaveAs(filePath);
                        updateValue.imageURL = "/upload_images/" + updateValue.profileID + "/" + fileName1;
                    }
                    UserRepository repository = new UserRepository();
                    updateResponseModel = repository.imageUpdateListingURL(updateValue);

                }

            }
            catch (Exception e)
            {
                updateResponseModel.message = e.ToString();
            }
            updateResponseModel.responseCode = "200";
            updateResponseModel.message = " Updated successfully.";
            return updateResponseModel;
        }
        
        [HttpPost]
        public  async Task<UpdateResponseModel> updateVideo()
        {
            UpdateResponseModel updateResponseModel = new UpdateResponseModel();
            UserRepository userRepository = new UserRepository();
            string fileName = null, fileName1 = null, filePath = null;
            var httpRequest = HttpContext.Current.Request;
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    ImageUpdateListingProfile imageUpdateListingProfile = new ImageUpdateListingProfile();
                    var param = httpRequest.Params;
                    var ImageRequestDetails = param["videodetails"];
                    var updateValue = JsonConvert.DeserializeObject<ImageUpdateListingProfile>(ImageRequestDetails);

                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        fileName = Regex.Replace(postedFile.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault(), "[^A-Za-z0-9_. ]+", "");
                        fileName1 = Regex.Replace(fileName, " ", string.Empty);
                        var domainDir = HttpContext.Current.Server.MapPath("~/upload_videos/" + updateValue.profileID + "/");
                        if (!Directory.Exists(domainDir))
                        {
                            Directory.CreateDirectory(domainDir);
                        }
                        filePath = HttpContext.Current.Server.MapPath("~/upload_videos/" + updateValue.profileID + "/" +  fileName1);
                        postedFile.SaveAs(filePath);
                        updateValue.imageURL = "/upload_videos/" + updateValue.profileID + "/" + fileName1;
                    }
                    UserRepository repository = new UserRepository();
                    updateResponseModel = repository.videoUpdateListingURL(updateValue);
                    
                }

            }
            catch (Exception e)
            {
                updateResponseModel.message = e.ToString();
            }
            updateResponseModel.responseCode = "200";
            updateResponseModel.message = " Updated successfully.";
            return updateResponseModel;
        }
        public async Task<ImageAddedResponse> updatePhoto()
        {
            ImageAddedResponse imageAddedResponse = new ImageAddedResponse();
            UserRepository userRepository = new UserRepository();
            string fileName = null, fileName1 = null, filePath = null;
            var httpRequest = HttpContext.Current.Request;
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    ImageAddRequest imageAddRequest = new ImageAddRequest();
                    var param = httpRequest.Params;
                    var ImageRequestDetails = param["imagedetails"];
                    var updateValue = JsonConvert.DeserializeObject<ImageAddRequest>(ImageRequestDetails);
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        fileName = Regex.Replace(postedFile.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault(), "[^A-Za-z0-9_. ]+", "");
                        fileName1 = Regex.Replace(fileName, " ", string.Empty);
                        var domainDir = HttpContext.Current.Server.MapPath("~/upload_images/" + updateValue.profileID + "/");
                        if (!Directory.Exists(domainDir))
                        {
                            Directory.CreateDirectory(domainDir);
                        }
                        filePath = HttpContext.Current.Server.MapPath("~/upload_images/" + updateValue.profileID + "/" + fileName1);
                        postedFile.SaveAs(filePath);
                        updateValue.imageURL = "/upload_images/" + updateValue.profileID + "/" + fileName1;
                    }
                    UserRepository repository = new UserRepository();
                    imageAddedResponse = repository.imageAddedResponse(updateValue);
                }
            }
            catch(Exception r)
            {
                imageAddedResponse.responseMessage = r.ToString();
            }
            return imageAddedResponse;
        }

        public UpdateResponseModel deletePhoto(string imageID)
        {
            UpdateResponseModel updateResponseModel = new UpdateResponseModel();
            UserRepository userRepository = new UserRepository();
            updateResponseModel = userRepository.deletePhotoimage(imageID);
            updateResponseModel.responseCode = "200";
            updateResponseModel.message = "Photo image deleted sucesssfully";
            return updateResponseModel;
        }
    }
}
