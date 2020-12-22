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
                }
            }
                return userDetails;
        }

        public List<UserDetails>UserDetailsvalidate(LoginRequestModel loginRequest)
        {
            List<UserDetails> userDetails = new List<UserDetails>();
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {
                using (MySqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT *FROM tbl_users WHERE fld_username='"+loginRequest.email+"'"+ " AND fld_password='"+loginRequest.password+"'";
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
                            });
                        }
                    }
                }
            }
                return userDetails;
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


        public UpdateResponseModel imageUpdateListingURL(ImageUpdateListingProfile imageUpdate)
        {
            UpdateResponseModel updateResponseModel = new UpdateResponseModel();
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {
                using (MySqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = " UPDATE tbl_listingprofile SET fld_imageUrl='" + imageUpdate.imageURL + "'" +
                        " WHERE fld_profileId='" + imageUpdate.profileID + "'";
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
        
        public UpdateResponseModel videoUpdateListingURL(ImageUpdateListingProfile imageUpdate)
        {
            UpdateResponseModel updateResponseModel = new UpdateResponseModel();
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {
                using (MySqlCommand sqlCommand = sqlConnection.CreateCommand())
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