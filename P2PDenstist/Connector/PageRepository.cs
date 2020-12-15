using MySql.Data.MySqlClient;
using P2PDenstist.Models;
using P2PDenstist.Models.Requests;
using P2PDenstist.Models.Responses;
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

        public List<Pagelayouts> homeListDetails(string domainname,string pagenumber) 
        {
            List<Pagelayouts> pagelayouts = new List<Pagelayouts>();
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {
                using (MySqlCommand mySqlCommand = sqlConnection.CreateCommand())
                {
                    mySqlCommand.CommandText = "SELECT *FROM tbl_pagelayouts";
                    mySqlCommand.CommandType = System.Data.CommandType.Text;
                    mySqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    using (MySqlDataReader sqlDataReader = mySqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            pagelayouts.Add(new Pagelayouts
                            {
                               layoutID=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_layoutid")),
                               pageID = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_pageid")),
                               pageImageURL = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_pageImageUrl")),
                               titleTexts = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_titleText")),
                               subTexts = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_subText")),
                               videoURL = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_videoUrl")),
                               pageLinkURL = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_pageLinkUrl")),
                               toolTips = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_toolTip")),
                            });
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return pagelayouts;
        }

        [Obsolete]
        public PageAddedResponse addedPageDetail(Pagelayouts pagelayouts)
        {
            PageAddedResponse addedResponse = new PageAddedResponse();
            List<Pagelayouts> pageList = new List<Pagelayouts>();
            int i = 0; string insertID = null;
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {
                using (MySqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "INSERT INTO tbl_pagelayouts(fld_pageid,fld_pageImageUrl,fld_titleText,fld_subText,fld_videoUrl,fld_pageLinkUrl,fld_toolTip)VALUES(?fld_pageid,?fld_pageImageUrl,?fld_titleText,?fld_subText,?fld_videoUrl,?fld_pageLinkUrl,?fld_toolTip)";
                    sqlConnection.Open();
                    sqlCommand.Parameters.Add("fld_pageid", pagelayouts.pageID);
                    sqlCommand.Parameters.Add("fld_pageImageUrl", pagelayouts.pageImageURL);
                    sqlCommand.Parameters.Add("fld_titleText", pagelayouts.titleTexts);
                    sqlCommand.Parameters.Add("fld_subText", pagelayouts.subTexts);
                    sqlCommand.Parameters.Add("fld_videoUrl", pagelayouts.videoURL);
                    sqlCommand.Parameters.Add("fld_pageLinkUrl", pagelayouts.pageLinkURL);
                    sqlCommand.Parameters.Add("fld_toolTip", pagelayouts.toolTips);
                    i = sqlCommand.ExecuteNonQuery();
                    insertID = sqlCommand.LastInsertedId.ToString();
                    sqlConnection.Close();
                }
            }
            if (i >= 1)
            {
                using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
                {
                    using (MySqlCommand mySqlCommand = sqlConnection.CreateCommand())
                    {
                        mySqlCommand.CommandText = "SELECT *FROM tbl_pagelayouts WHERE fld_layoutid='"+insertID+"'";
                        mySqlCommand.CommandType = System.Data.CommandType.Text;
                        mySqlCommand.Connection = sqlConnection;
                        sqlConnection.Open();
                        using (MySqlDataReader sqlDataReader = mySqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                pageList.Add(new Pagelayouts
                                {
                                    layoutID = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_layoutid")),
                                    pageID = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_pageid")),
                                    pageImageURL = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_pageImageUrl")),
                                    titleTexts = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_titleText")),
                                    subTexts = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_subText")),
                                    videoURL = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_videoUrl")),
                                    pageLinkURL = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_pageLinkUrl")),
                                    toolTips = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_toolTip")),
                                });
                            }
                        }
                        sqlConnection.Close();
                    }
                }
                addedResponse.pageID = insertID;
                addedResponse.responseCode = "200";
                addedResponse.responseMessage = "Page added Details";
                addedResponse.pagelayouts = pageList;
            }
                return addedResponse;
        }


        public List<ListingAdvert> listingAdvertList(string domainName,string pageno)
        {
            
            List<ListingAdvert> listingAdvertList = new List<ListingAdvert>();
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {
                using (MySqlCommand mySqlCommand = sqlConnection.CreateCommand())
                {
                    mySqlCommand.CommandText = "SELECT *FROM tbl_listingadvert";
                    mySqlCommand.CommandType = System.Data.CommandType.Text;
                    mySqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    using (MySqlDataReader sqlDataReader = mySqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            listingAdvertList.Add(new ListingAdvert
                            {
                                listingAdvertID=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_listingAdvertId")),
                                transactionID = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_transcationId")),
                                fromDate = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_fromDate")),
                                toDate = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_toDate")),
                                imageURL = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_imageUrl")),
                                linkURL = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_linkUrl")),
                                listweight = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_listWeight")),
                                userID = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_userID")),
                                lng = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_long")),
                                lat = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_lat")),
                                cDate = sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_cDate")),
                            });
                        }
                    }

                }
            }
                return listingAdvertList;
        }

        public List<testimonial> testimonials(string domainName,string pageNo)
        {
            List<testimonial> testimonials = new List<testimonial>();
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {
                using (MySqlCommand mySqlCommand = sqlConnection.CreateCommand())
                {
                    mySqlCommand.CommandText = "SELECT *FROM tbl_testmonial";
                    mySqlCommand.CommandType = System.Data.CommandType.Text;
                    mySqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    using (MySqlDataReader sqlDataReader = mySqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            testimonials.Add(new testimonial
                            {
                                testmonialID=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_testmonial_id")),
                                profileID=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_profile_id")),
                                testmonialText=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_testmonial_text")),
                                addedby=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_addedby")),
                                city=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_city")),
                                cdate=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_cdate")),
                            });
                        }
                    }
                }
            }
                return testimonials;
        }

        public List<SpeciazationModel> speciazations(string domainName,string pageNo)
        {
            List<SpeciazationModel> speciazations = new List<SpeciazationModel>();
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {
                using (MySqlCommand mySqlCommand = sqlConnection.CreateCommand())
                {
                    mySqlCommand.CommandText = "SELECT *FROM tbl_specializationcategory";
                    mySqlCommand.CommandType = System.Data.CommandType.Text;
                    mySqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    using (MySqlDataReader sqlDataReader = mySqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            speciazations.Add(new SpeciazationModel 
                            { 
                            categoryID=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_sCategoryId")),
                            specization=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_specilaization"))
                            });
                        }
                    }
                }
            }
             return speciazations;
        }

        public List<SpeciazationMaster>speciazationMasters(string domainname,string pageno)
        {
            List<SpeciazationMaster> speciaMaster = new List<SpeciazationMaster>();
            using (MySqlConnection sqlConnection = new MySqlConnection(connectstring))
            {
                using (MySqlCommand mySqlCommand = sqlConnection.CreateCommand())
                {
                    mySqlCommand.CommandText = "SELECT *FROM tbl_specializationmaster";
                    mySqlCommand.CommandType = System.Data.CommandType.Text;
                    mySqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    using (MySqlDataReader sqlDataReader = mySqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            speciaMaster.Add(new SpeciazationMaster
                            {
                                specizationID=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_specializationId")),
                                specizationName=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_specializationName")),
                                sCategory=sqlDataReader.GetString(sqlDataReader.GetOrdinal("fld_sCategory"))
                            });
                        }
                    }
                }
            }
            return speciaMaster;
        }


    }
}