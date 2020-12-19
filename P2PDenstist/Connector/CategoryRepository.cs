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
    public class CategoryRepository
    {
        private string connectstring;
        public CategoryRepository()
        {
            var constants = new Constants();
            connectstring = constants.CONNECT_STRING_URL;
        }

        [Obsolete]
        public SpecialCategoryAddedResponse SpecializationAdd(SpeciazationMaster specailMaster)
        {
            SpecialCategoryAddedResponse specialCategoryAddedResponse = new SpecialCategoryAddedResponse();
            int i = 0; string insertID = null;
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {
                using (MySqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "INSERT INTO tbl_specializationmaster(fld_specializationName,fld_sCategory)VALUES(?fld_specializationName,?fld_sCategory)";
                    sqlConnection.Open();
                    sqlCommand.Parameters.Add("fld_specializationName", specailMaster.specizationName);
                    sqlCommand.Parameters.Add("fld_sCategory", specailMaster.sCategory);
                    i = sqlCommand.ExecuteNonQuery();
                    insertID = sqlCommand.LastInsertedId.ToString();
                    sqlConnection.Close();
                }

            }

            if (i >= 1)
            {
                specialCategoryAddedResponse.responseCode = "200";
                specialCategoryAddedResponse.responseMessage = "SpecialCategory master added";
                specialCategoryAddedResponse.SpecailCategoryID = insertID;

            }
            return specialCategoryAddedResponse;
        }

        public UpdateResponseModel updateResponseModel(SpeciazationMaster speciazationMaster)
        {
            UpdateResponseModel updateResponseModel = new UpdateResponseModel();
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {
                using (MySqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    try
                    {
                        sqlCommand.CommandText = " UPDATE tbl_specializationmaster SET fld_specializationName='" + speciazationMaster.specizationName + "'" + "," +
                            " fld_sCategory='" + speciazationMaster.sCategory + "'" + " WHERE fld_specializationId='" + speciazationMaster.specizationID + "'";
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

                    }
                }
            }
                return updateResponseModel;
        }

        [Obsolete]
        public TestimonialAddedResponse testimonialAdded(TestimonialsRequest testimonialsRequest)
        {
            TestimonialAddedResponse testimonialAddedResponse = new TestimonialAddedResponse();
            int i = 0; string insertID = null;
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {
                using (MySqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "INSERT INTO tbl_specializationmaster(fld_profile_id,fld_testmonial_text,fld_addedby,fld_city,fld_cdate)VALUES(?fld_profile_id,?fld_testmonial_text,?fld_addedby,?fld_city,?fld_cdate)";
                    sqlConnection.Open();
                    sqlCommand.Parameters.Add("fld_profile_id", testimonialsRequest.profileId);
                    sqlCommand.Parameters.Add("fld_testmonial_text", testimonialsRequest.testmonialText);
                    sqlCommand.Parameters.Add("fld_addedby", testimonialsRequest.addedby);
                    sqlCommand.Parameters.Add("fld_city", testimonialsRequest.city);
                    sqlCommand.Parameters.Add("fld_cdate", testimonialsRequest.currentDate);
                    i = sqlCommand.ExecuteNonQuery();
                    insertID = sqlCommand.LastInsertedId.ToString();
                    sqlConnection.Close();
                }
            }
            if (i >= 1)
            {
                testimonialAddedResponse.responseCode = "200";
                testimonialAddedResponse.responseMessage = "testimonial added suceesfully";
                testimonialAddedResponse.TestimonialAddedID = insertID;

            }
            return testimonialAddedResponse;
        }

        public UpdateResponseModel testimonialsUpdated(TestimonialsRequest testimonialsRequest)
        {
            UpdateResponseModel updateResponse = new UpdateResponseModel();
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {
                using (MySqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    try
                    {
                        sqlCommand.CommandText = " UPDATE tbl_testmonial SET fld_profile_id='" + testimonialsRequest.profileId + "'" + "," +
                         " fld_testmonial_text='" + testimonialsRequest.testmonialText + "'" + ","+
                         " fld_addedby='" + testimonialsRequest.addedby + "'" + ","+
                         " fld_city='" + testimonialsRequest.city + "'" + ","+
                         " fld_cdate='" + testimonialsRequest.currentDate + "'" + ""+
                         " WHERE fld_testmonial_id='" + testimonialsRequest.testmonialId + "'";
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
                return updateResponse;
        }



    }
}