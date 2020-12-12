using MySql.Data.MySqlClient;
using P2PDenstist.Models;
using P2PDenstist.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P2PDenstist.Connector
{
    public class PageRepository
    {
        private string connectstring;

        public PageRepository()
        {
            var constants = new Constants();
            connectstring = constants.CONNECT_STRING_URL;
        }

        public List<Pagelayouts> getHomePage(string domainname,string pagenumber) 
        {
            List<Pagelayouts> pagelayouts = new List<Pagelayouts>();
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {
                using (MySqlCommand mySqlCommand = sqlConnection.CreateCommand())
                {
                    mySqlCommand.CommandText = "SELECT *FROM pagelayouts";
                    mySqlCommand.CommandType = System.Data.CommandType.Text;
                    mySqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    using (MySqlDataReader sqlDataReader = mySqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            pagelayouts.Add(new Pagelayouts
                            {
                                layoutID = sqlDataReader.GetString(sqlDataReader.GetOrdinal("layoutid")),
                                pageID = sqlDataReader.GetString(sqlDataReader.GetOrdinal("pageid")),
                                pageImageURL = sqlDataReader.GetString(sqlDataReader.GetOrdinal("pageImageUrl")),
                                titleTexts = sqlDataReader.GetString(sqlDataReader.GetOrdinal("titleText")),
                                subTexts = sqlDataReader.GetString(sqlDataReader.GetOrdinal("subText")),
                                videoURL = sqlDataReader.GetString(sqlDataReader.GetOrdinal("videoUrl")),
                                pageLinkURL = sqlDataReader.GetString(sqlDataReader.GetOrdinal("pageLinkUrl")),
                                toolTips = sqlDataReader.GetString(sqlDataReader.GetOrdinal("toolTip")),
                            });
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return pagelayouts;
        }
    }
}