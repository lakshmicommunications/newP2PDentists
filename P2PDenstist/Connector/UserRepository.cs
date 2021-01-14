using MySql.Data.MySqlClient;
using P2PDenstist.Models.Requests;
using P2PDenstist.Models.Responses;
using P2PDenstist.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P2PDenstist.Connector
{
    public class UserRepository
    {
        private string connectstring;
        public UserRepository()
        {
            var constants = new Constants();
            connectstring = constants.CONNECT_STRING_URL;
        }

        [Obsolete]
        public UserAddedResponse usersadded(UserDetails userDetails)
        {
            UserAddedResponse userAdded = new UserAddedResponse();
            int i = 0; string insertID = null;
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {
                using (MySqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "INSERT INTO tbl_users(fld_profileId,fld_username,fld_password,fld_userRole,fld_lastLogin,fld_imageUrl,fld_logoUrl,tbl_cDate)VALUES(?fld_profileId,?fld_username,?fld_password,?fld_userRole,?fld_lastLogin,?fld_imageUrl,?fld_logoUrl,?tbl_cDate)";
                    sqlConnection.Open();
                    sqlCommand.Parameters.Add("fld_profileId", userDetails.profileID);
                    sqlCommand.Parameters.Add("fld_username", userDetails.userName);
                    sqlCommand.Parameters.Add("fld_password", userDetails.password);
                    sqlCommand.Parameters.Add("fld_userRole", userDetails.userRole);
                    sqlCommand.Parameters.Add("fld_lastLogin", userDetails.lastLogin);
                    sqlCommand.Parameters.Add("fld_imageUrl", userDetails.imageURL);
                    sqlCommand.Parameters.Add("fld_logoUrl", userDetails.logoURL);
                    sqlCommand.Parameters.Add("tbl_cDate", userDetails.cDate);
                    i = sqlCommand.ExecuteNonQuery();
                    insertID = sqlCommand.LastInsertedId.ToString();
                    sqlConnection.Close();
                }
            }
            if (i >= 1)
            {
                userAdded.responseCode = "200";
                userAdded.responseMessage = "User details added";
                userAdded.userID = insertID;
            }

             return userAdded;
        }


        public List<UserDetails> userDetails()
        {
            List<UserDetails> userDetails = new List<UserDetails>();
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {
                using (MySqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT *FROM tbl_users";
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    using (MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            userDetails.Add(new UserDetails
                            {
                             userID=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_userId")),
                             profileID=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_profileId")),
                             userName=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_username")),
                             password=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_password")),
                             userRole=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_userRole")),
                             lastLogin=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_lastLogin")),
                             imageURL=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_imageUrl")),
                             logoURL=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_logoUrl")),
                             cDate=sqlDataReader.GetString(sqlDataReader.GetOrdinal("tbl_cDate")),
                            });
                        }
                    }
                    sqlConnection.Close();
                }
            }
                return userDetails;
        }

        [Obsolete]
        public userValidationResponseModel UserDetailsvalidate(LoginRequestModel loginRequest)
        {
            userValidationResponseModel userValidationResponseModel = new userValidationResponseModel();
            List<UserDetails> userDetails = new List<UserDetails>();
            List<SessionModel> sessions = new List<SessionModel>();
            userDetails.Clear(); sessions.Clear();
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {

                int i = 0; string insertID = null;
                
                {
                    using (MySqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = "INSERT INTO tbl_session_management(fld_session_token,fld_date_time)VALUES(?fld_session_token,?fld_date_time)";
                        sqlConnection.Open();
                        sqlCommand.Parameters.Add("fld_session_token", Guid.NewGuid().ToString());
                        sqlCommand.Parameters.Add("fld_date_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        i = sqlCommand.ExecuteNonQuery();
                        insertID = sqlCommand.LastInsertedId.ToString();
                        sqlConnection.Close();
                    }
                }
                if (i >= 1)
                {
                    using (MySqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = "SELECT *FROM tbl_session_management WHERE fld_session_id='" + insertID+"'";
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.Connection = sqlConnection;
                        sqlConnection.Open();
                        using (MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                sessions.Add(new SessionModel
                                {
                                    sessionID=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_session_id")),
                                    sessionToken=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_session_token")),
                                    sessionDatetime=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_date_time")),
                                });
                            }
                        }
                        sqlConnection.Close();
                    }
                   
                }
                if (sessions.Count <= 0)
                {

                }
                else
                {
                    string tokenID = null;
                    foreach(var Item in sessions)
                    {
                        tokenID = Item.sessionToken;
                    }
                    using (MySqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = "SELECT *FROM tbl_users WHERE fld_username='" + loginRequest.email + "'" + " AND fld_password='" + loginRequest.password + "'";
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.Connection = sqlConnection;
                        sqlConnection.Open();
                        using (MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                userDetails.Add(new UserDetails
                                {
                                    userID = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_userId")),
                                    profileID = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_profileId")),
                                    userName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_username")),
                                    password = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_password")),
                                    userRole = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_userRole")),
                                    lastLogin = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_lastLogin")),
                                    imageURL = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_imageUrl")),
                                    logoURL = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_logoUrl")),
                                    cDate = sqlDataReader.GetString(sqlDataReader.GetOrdinal("tbl_cDate")),
                                    sessionToken = tokenID
                                });
                            }
                        }
                        sqlConnection.Close();
                    }
                }
                
            }
            if (userDetails.Count <= 0)
            {
                userValidationResponseModel.responseCode = "200";
                userValidationResponseModel.responseMesssage = "Invalid username and password";
                userValidationResponseModel.userDetails = userDetails;
            }
            else
            {
                userValidationResponseModel.responseCode = "200";
                userValidationResponseModel.responseMesssage = "Login successfully";
                userValidationResponseModel.userDetails = userDetails;
            }
            
            return userValidationResponseModel;
        }

        public PasswordUpdateResponse passwordUpdateResponse(PasswordUpdateRequest passwordUpdate)
        {
            PasswordUpdateResponse passwordUpdateResponse = new PasswordUpdateResponse();
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {
                using (MySqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    try
                    {
                        sqlCommand.CommandText = " UPDATE tbl_users SET fld_username='" + passwordUpdate.userName + "'" + "," + " fld_password='" + passwordUpdate.password + "'" + " WHERE fld_userId='" + passwordUpdate.userID + "'" + "";
                        sqlConnection.Open();
                        using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                            }
                        }
                        sqlConnection.Close();
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("" + e.ToString());
                    }
                }
            }

            return passwordUpdateResponse;
        }

        public List<ProfileRequestDetailsLocationWise> profileRequestDetailsLocationWises(String lat,string lng)
        {
            List<ProfileRequestDetailsLocationWise> profileRequestDetailsLocationWises = new List<ProfileRequestDetailsLocationWise>();
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {
                using (MySqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = " SELECT *FROM tbl_listingprofile";
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    using (MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            profileRequestDetailsLocationWises.Add(new ProfileRequestDetailsLocationWise
                            {
                                profileID=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_profileId")),
                                userID=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_userId")),
                                BusinessName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_businessName")),
                                address = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_address1")),
                                address1 = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_address2")),                          
                                city = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_city")),
                                lat = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_lat")),
                                lng = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_long")),
                            });
                        }
                    }
                }
            }

                return profileRequestDetailsLocationWises;
        }

       
        [Obsolete]
        public ListingProfileAddedResponse addedResponse(ListingProfileRequest listingProfileRequest)
        {
            ListingProfileAddedResponse addedRespon = new ListingProfileAddedResponse();
            int i = 0; string insertID;
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {
                using (MySqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "INSERT INTO tbl_listingprofile(fld_userId,fld_businessName,fld_fName,fld_lName,fld_salutation,fld_Tele,fld_email,fld_address1,fld_address2,fld_city,fld_state,fld_country,fld_postcode,fld_hasP2p,fld_hasVideoCall,fld_websiteUrl,fld_videoUrl,fld_logoUrl,fld_long,fld_lat,fld_hrs,fld_startRating,fld_openNow,fld_availableNow,fld_subscriptionId,fld_imageUrl,fld_listWeight,fld_cDate)VALUES(?fld_userId,?fld_businessName,?fld_fName,?fld_lName,?fld_salutation,?fld_Tele,?fld_email,?fld_address1,?fld_address2,?fld_city,?fld_state,?fld_country,?fld_postcode,?fld_hasP2p,?fld_hasVideoCall,?fld_websiteUrl,?fld_videoUrl,?fld_logoUrl,?fld_long,?fld_lat,?fld_hrs,?fld_startRating,?fld_openNow,?fld_availableNow,?fld_subscriptionId,?fld_imageUrl,?fld_listWeight,?fld_cDate)";
                    sqlConnection.Open();
                    sqlCommand.Parameters.Add("fld_userId", listingProfileRequest.userID);
                    sqlCommand.Parameters.Add("fld_businessName", listingProfileRequest.businessname);
                    sqlCommand.Parameters.Add("fld_fName", listingProfileRequest.firstname);
                    sqlCommand.Parameters.Add("fld_lName", listingProfileRequest.lastname);
                    sqlCommand.Parameters.Add("fld_salutation", listingProfileRequest.salutation);
                    sqlCommand.Parameters.Add("fld_Tele", listingProfileRequest.phone);
                    sqlCommand.Parameters.Add("fld_email", listingProfileRequest.email);
                    sqlCommand.Parameters.Add("fld_address1", listingProfileRequest.address);
                    sqlCommand.Parameters.Add("fld_address2", listingProfileRequest.address1);
                    sqlCommand.Parameters.Add("fld_city", listingProfileRequest.city);
                    sqlCommand.Parameters.Add("fld_state", listingProfileRequest.state);
                    sqlCommand.Parameters.Add("fld_country", listingProfileRequest.country);
                    sqlCommand.Parameters.Add("fld_postcode", listingProfileRequest.postCode);
                    sqlCommand.Parameters.Add("fld_hasP2p", listingProfileRequest.hasp2p);
                    sqlCommand.Parameters.Add("fld_hasVideoCall", listingProfileRequest.hasVideocall);
                    sqlCommand.Parameters.Add("fld_websiteUrl", listingProfileRequest.WebsiteURL);
                    sqlCommand.Parameters.Add("fld_videoUrl", listingProfileRequest.VideoURL);
                    sqlCommand.Parameters.Add("fld_logoUrl", listingProfileRequest.logoURL);
                    sqlCommand.Parameters.Add("fld_long", listingProfileRequest.lng);
                    sqlCommand.Parameters.Add("fld_lat", listingProfileRequest.lat);
                    sqlCommand.Parameters.Add("fld_startRating", listingProfileRequest.start_rating);
                    sqlCommand.Parameters.Add("fld_openNow", listingProfileRequest.open_now);
                    sqlCommand.Parameters.Add("fld_availableNow", listingProfileRequest.available_now);
                    sqlCommand.Parameters.Add("fld_subscriptionId", listingProfileRequest.subcription_id);
                    sqlCommand.Parameters.Add("fld_imageUrl", listingProfileRequest.imageURL);
                    sqlCommand.Parameters.Add("fld_listWeight", listingProfileRequest.listWeight);
                    sqlCommand.Parameters.Add("fld_cDate", listingProfileRequest.currentDate);
                    i = sqlCommand.ExecuteNonQuery();
                    insertID = sqlCommand.LastInsertedId.ToString();
                    sqlConnection.Close();
                }
            }
            if (i >= 1)
            {
                addedRespon.responseCode = "200";
                addedRespon.responseMessage = "response code Added successfully";
                addedRespon.listingProfileID = insertID;

            }

            return addedRespon;
        }

        [Obsolete]
        public DoctorAddedResponseModel doctorAddedResponseModel(DoctorregisterModel doctorregisterModel)
        {
            DoctorAddedResponseModel doctorAddedResponseModel = new DoctorAddedResponseModel();
            int i = 0; string insertID;
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {
                using (MySqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "INSERT INTO tbl_doctor_registeration(fld_clinic_name,fld_first_name,fld_last_name,fld_phone,fld_email,fld_password,fld_subcription_status)VALUES(?fld_clinic_name,?fld_first_name,?fld_last_name,?fld_phone,?fld_email,?fld_password,?fld_subcription_status)";
                    sqlConnection.Open();
                    sqlCommand.Parameters.Add("fld_clinic_name", doctorregisterModel.clinicName);
                    sqlCommand.Parameters.Add("fld_first_name", doctorregisterModel.firstName);
                    sqlCommand.Parameters.Add("fld_last_name", doctorregisterModel.lastName);
                    sqlCommand.Parameters.Add("fld_phone", doctorregisterModel.phone);
                    sqlCommand.Parameters.Add("fld_email", doctorregisterModel.email);
                    sqlCommand.Parameters.Add("fld_password", doctorregisterModel.password);
                    sqlCommand.Parameters.Add("fld_subcription_status", doctorregisterModel.subscription_status);
                    i = sqlCommand.ExecuteNonQuery();
                    insertID = sqlCommand.LastInsertedId.ToString();
                    sqlConnection.Close();
                }
            }
            if (i >= 1)
            {
                doctorAddedResponseModel.responseCode = "200";
                doctorAddedResponseModel.responseMessage = "response code Added successfully";
                doctorAddedResponseModel.doctorID = insertID;

            }
            return doctorAddedResponseModel;
        }

        public List<DoctorregisterModel> doctorregisters()
        {
            List<DoctorregisterModel> doctorregisters = new List<DoctorregisterModel>();
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {
                using (MySqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = " SELECT *FROM tbl_doctor_registeration LIMIT 3";
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    using (MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            doctorregisters.Add(new DoctorregisterModel
                            {
                                doctorID=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_id")),
                                clinicName=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_clinic_name")),
                                firstName=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_first_name")),
                                lastName=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_last_name")),
                                phone=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_phone")),
                                email=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_email")),
                                password=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_password")),
                                subscription_status=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_subcription_status")),
                            });
                        }
                    }
                }
            }
                return doctorregisters;
        }

        [Obsolete]
        public AddBannerDetailsResponse addBannerDetails(AddBannerRequestModel addBannerRequestModel)
        {

            AddBannerDetailsResponse addBannerDetails = new AddBannerDetailsResponse();
            int i = 0; string insertID;
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {
                using (MySqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "INSERT INTO tbl_banner(fld_profile_id,fld_banner_url,fld_page_id)VALUES(?fld_profile_id,?fld_banner_url,?fld_page_id)";
                    sqlConnection.Open();
                    sqlCommand.Parameters.Add("fld_profile_id", addBannerRequestModel.profileID);
                    sqlCommand.Parameters.Add("fld_banner_url", addBannerRequestModel.bannerID);
                    sqlCommand.Parameters.Add("fld_page_id", addBannerRequestModel.pageID);
                    i = sqlCommand.ExecuteNonQuery();
                    insertID = sqlCommand.LastInsertedId.ToString();
                    sqlConnection.Close();
                }
            }
            if (i >= 1)
            {
                addBannerDetails.bannerID = insertID;
                addBannerDetails.reponseMessage = "Banner added successfully";
                addBannerDetails.responseCode = "200";
                
            }
                return addBannerDetails;
        }


        public UpdateResponseModel updateListingProfile(ListingProfileUpdateRequest listingProfileUpdateRequest)
        {
            UpdateResponseModel updateResponseModel = new UpdateResponseModel();
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {
                using (MySqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = " UPDATE tbl_listingprofile SET fld_salutation='" + listingProfileUpdateRequest.saluation + "'" + "," +
                            " fld_fName='" + listingProfileUpdateRequest.firstName + "'" +
                            " fld_lName='" + listingProfileUpdateRequest.lastName + "'" +
                            " fld_email='" + listingProfileUpdateRequest.email + "'" +
                            " fld_Tele='" + listingProfileUpdateRequest.phone + "'" +
                            " fld_address1='" + listingProfileUpdateRequest.address + "'" +
                            " fld_address2='" + listingProfileUpdateRequest.address1 + "'" +
                            " fld_city='" + listingProfileUpdateRequest.city + "'" +
                            " fld_state='" + listingProfileUpdateRequest.province + "'" +
                            " fld_country='" + listingProfileUpdateRequest.country + "'" +
                            " fld_postcode='" + listingProfileUpdateRequest.postalcode + "'" +
                            " WHERE fld_profileId='" + listingProfileUpdateRequest.profileID + "'";
                    sqlConnection.Open();
                    using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                        }
                    }
                    sqlConnection.Close();
                }
            }
                return updateResponseModel;
        }


        public List<HomepageVideolist> homepageVideolists(string profileID)
        {
            HomepageVideoResponse homepageVideoResponse = new HomepageVideoResponse();
            List<HomepageVideolist> homepageVideolists = new List<HomepageVideolist>();
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {
                using (MySqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = " SELECT *FROM tbl_listingprofile  WHERE fld_profileid='" + profileID + "'";
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    using (MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            homepageVideolists.Add(new HomepageVideolist
                            {
                                profileID=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_profileId")),
                                VideoURL=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_videoUrl"))

                            });
                        }
                    }
                }
            }
                return homepageVideolists;
        }

        public UpdateResponseModel imageUpdateListingURL(ImageUpdateListingProfile imageUpdate)
        {
            UpdateResponseModel updateResponseModel = new UpdateResponseModel();
            List<SessionModel> sessionListDetails = new List<SessionModel>();
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {
                using (MySqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    if (imageUpdate.sessionToken != null)
                    {
                        sessionListDetails.Clear();
                        sqlCommand.CommandText = " SELECT *FROM tbl_session_management  WHERE fld_session_token='" + imageUpdate.sessionToken + "'";
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.Connection = sqlConnection;
                        sqlConnection.Open();
                        using (MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                sessionListDetails.Add(new SessionModel
                                {
                                    sessionDatetime = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_date_time")),
                                    sessionToken = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_session_token"))
                                });
                            }
                        }
                        sqlConnection.Close();
                        if (sessionListDetails.Count <= 0)
                        {
                            updateResponseModel.responseCode = "200";
                            updateResponseModel.message = "Session token invalid";
                        }
                        else
                        {
                         sqlCommand.CommandText = " UPDATE tbl_listingprofile SET fld_logoUrl='" + imageUpdate.imageURL + "'" +
                        " WHERE fld_profileId='" + imageUpdate.profileID + "'";
                            sqlConnection.Open();
                            using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                            {
                                while (reader.Read())
                                {

                                }
                            }
                            sqlConnection.Close();
                            updateResponseModel.responseCode = "200";
                            updateResponseModel.message = " Updated successfully.";
                        }
                    }
                    else
                    {
                        updateResponseModel.responseCode = "400";
                        updateResponseModel.message = "invalid parameter parsing";
                    }


                }
            }

            return updateResponseModel;
        }      
        
        public UpdateResponseModel videoUpdateListingURL(ImageUpdateListingProfile imageUpdate)
        {
            UpdateResponseModel updateResponseModel = new UpdateResponseModel();
            List<SessionModel> sessionListDetails = new List<SessionModel>();
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {
                using (MySqlCommand sqlCommand = sqlConnection.CreateCommand())

                {
                    if (imageUpdate.sessionToken != null)

                    {
                        sessionListDetails.Clear();
                        sqlCommand.CommandText = " SELECT *FROM tbl_session_management  WHERE fld_session_token='" + imageUpdate.sessionToken + "'";
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.Connection = sqlConnection;
                        sqlConnection.Open();
                        using (MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                sessionListDetails.Add(new SessionModel
                                {
                                    sessionDatetime=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_date_time")),
                                    sessionToken=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_session_token"))
                                });
                            }
                        }
                        sqlConnection.Close();
                        if (sessionListDetails.Count <= 0)
                        {
                            updateResponseModel.responseCode = "200";
                            updateResponseModel.message = "Session token invalid";
                        }
                        else
                        {
                            sqlCommand.CommandText = " UPDATE tbl_listingprofile SET fld_videoUrl='" + imageUpdate.imageURL + "'" +
                              " WHERE fld_profileId='" + imageUpdate.profileID + "'";
                            sqlConnection.Open();
                            using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                            {
                                while (reader.Read())
                                {

                                }
                            }
                            sqlConnection.Close();

                            updateResponseModel.responseCode = "200";
                            updateResponseModel.message = " Updated successfully.";
                        }



                    }
                    else
                    {
                        updateResponseModel.responseCode = "400";
                        updateResponseModel.message = "invalid parameter parsing";
                    }

                   
                }
            }
           

            return updateResponseModel;
        }
         public UpdateResponseModel imageDeleteListingURL(String profileID)
        {
            UpdateResponseModel updateResponseModel = new UpdateResponseModel();
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {
                using (MySqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = " UPDATE tbl_listingprofile SET fld_imageUrl='" + " "+ "'" +
                        " WHERE fld_profileId='" + profileID + "'";
                    sqlConnection.Open();
                    using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                        }
                    }
                    sqlConnection.Close();
                }
            }

            return updateResponseModel;
        }
         public UpdateResponseModel VideoDeleteListingURL(String profileID)
        {
            UpdateResponseModel updateResponseModel = new UpdateResponseModel();
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {
                using (MySqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = " UPDATE tbl_listingprofile SET fld_videoUrl='" + " "+ "'" +
                        " WHERE fld_profileId='" + profileID + "'";
                    sqlConnection.Open();
                    using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                        }
                    }
                    sqlConnection.Close();
                }
            }

            return updateResponseModel;
        }

        [Obsolete]
        public ImageAddedResponse imageAddedResponse(ImageAddRequest imageAddRequest)
        {
            ImageAddedResponse imageAdded = new ImageAddedResponse();
            int i = 0; string insertID;
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {
                using (MySqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "INSERT INTO tbl_images(fld_imageUrl,fld_profileId)VALUES(?fld_imageUrl,?fld_profileId)";
                    sqlConnection.Open();
                    sqlCommand.Parameters.Add("fld_imageUrl", imageAddRequest.imageURL);
                    sqlCommand.Parameters.Add("fld_profileId", imageAddRequest.profileID);
                    i = sqlCommand.ExecuteNonQuery();
                    insertID = sqlCommand.LastInsertedId.ToString();
                    sqlConnection.Close();
                }
            }
            if (i >= 1)
            {
                imageAdded.responseCode = "200";
                imageAdded.responseMessage = "Image added";
                imageAdded.imageAddedID = insertID;

            }
             return imageAdded;
        }

        public List<ImageAddRequest> imageList()
        {
            List<ImageAddRequest> images = new List<ImageAddRequest>();
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {
                using (MySqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = " SELECT *FROM tbl_images";
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    using (MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            images.Add(new ImageAddRequest
                            {
                                imageID=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_imageId")),
                                imageURL=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_imageUrl")),
                                profileID=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_profileId")),
                            });
                        }
                    }
                }
            }
                return images;
        }

        public UpdateResponseModel deletePhotoimage(string imageID)
        {
            UpdateResponseModel updateResponseModel = new UpdateResponseModel();
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {
                using (MySqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "DELETE FROM tbl_images WHERE fld_imageId='"+imageID+"'";
                    sqlConnection.Open();
                    using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                        }
                    }
                    sqlConnection.Close();
                }
            }
                return updateResponseModel;
        }
    }
}