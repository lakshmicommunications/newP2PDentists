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

    }
}