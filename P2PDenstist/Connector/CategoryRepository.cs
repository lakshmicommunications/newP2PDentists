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
    }
}