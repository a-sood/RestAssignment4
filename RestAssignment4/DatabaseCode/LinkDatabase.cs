using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestAssignment4.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;


namespace LinkShortener.Models.Database
{
    public partial class LinkDatabase : AbstractDatabase
    {
        private LinkDatabase() { }

        public static LinkDatabase getInstance()
        {
            if(instance == null)
            {
                instance = new LinkDatabase();
            }
            return instance;
        }

        /// <summary>
        /// Gets a long URL based on the id of the short url
        /// </summary>
        /// <param name="companyName">The id of the short url</param>
        /// <throws type="ArgumentException">Throws an argument exception if the short url id does not refer to anything in the database</throws>
        /// <returns>The long url the given short url refers to</returns>
        public List<CompanyReview> readReview(string companyName)
        {
            string query = @"SELECT * FROM " + dbname + ".reviews "
                + "WHERE companyname= '" + companyName + "';";

            if(openConnection() == true)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                //JObject result = new JObject();
                List<CompanyReview> reviewList = new List<CompanyReview>();
                while(reader.Read())
                {
                    CompanyReview singleReview = new CompanyReview();
                    singleReview.CompanyName = reader.GetString("companyname");
                    singleReview.UserName = reader.GetString("username");
                    singleReview.Review = reader.GetString("review");
                    singleReview.Stars = reader.GetInt32("stars");
                    singleReview.TimeStamp = reader.GetString("timestamp");

                    //var mergeSettings = new JsonMergeSettings { MergeArrayHandling = MergeArrayHandling.Concat };
                    reviewList.Add(singleReview);
                    //result.Merge(JObject.Parse(JsonConvert.SerializeObject(singleReview)), mergeSettings);
                    

                }
                reader.Close();
                closeConnection();
                return reviewList;
               
            }
            else
            {
                throw new Exception("Could not connect to database.");
            }
        }

        /// <summary>
        /// Saves the longURL to the database to be accessed later via the id that is returned.
        /// </summary>
        /// <param name="longURL">The longURL to be saved</param>
        /// <returns>The id of the url</returns>
        public void SaveReview(CompanyReview data)
        {
            string query = @"INSERT INTO " + dbname + ".reviews(companyName, username, review, stars, timestamp) "
                + @"VALUES('" + data.CompanyName + "' , '" +  data.UserName + "' , '" + data.Review + "' , " + data.Stars +  ", '" + data.TimeStamp + @"'); ";

            if(openConnection() == true)
            {
                
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.ExecuteNonQuery();

                    //command.CommandText = "SELECT * FROM " + dbname + ".shortenedLinks WHERE id = LAST_INSERT_ID();";

                    //MySqlDataReader reader = command.ExecuteReader();

                    //if(reader.Read() == true)
                    //{
                    //    string result = reader.GetInt64("id").ToString();
                    //    closeConnection();
                    //    return result.ToString();
                    //}
                    //else
                    //{
                    //    closeConnection();
                    //    throw new Exception("Error: LAST_INSERT_ID() did not work as intended.");
                    //}
                    closeConnection();
             
            }
            else
            {
                throw new Exception("Could not connect to database");
            }
        }
    }

    public partial class LinkDatabase : AbstractDatabase
    {
        private static LinkDatabase instance = null;

        private const String dbname = "Rest";
        public override String databaseName { get; } = dbname;

        protected override Table[] tables { get; } =
        {
            // This represents the database schema
            new Table
            (
                dbname,
                "reviews",
                new Column[]
                {
                    new Column
                    (
                        "id", "INT(64)",
                        new string[]
                        {
                            "NOT NULL",
                            "UNIQUE",
                            "AUTO_INCREMENT"
                        }, true
                    ),
                    new Column
                    (
                        "companyname", "VARCHAR(300)",
                        new string[]
                        {
                            "NOT NULL"
                        }, false
                    ),
                    new Column
                    (
                        "username", "VARCHAR(300)",
                        new string[]
                        {
                            "NOT NULL"
                        }, false
                    ),
                    new Column
                    (
                        "review", "VARCHAR(300)",
                        new string[]
                        {
                            "NOT NULL"
                        }, false
                    ),
                    new Column
                    (
                        "stars", "INT(32)",
                        new string[]
                        {
                            "NOT NULL"
                        }, false
                    ),
                    new Column
                    (
                        "timestamp", "VARCHAR(50)",
                        new string[]
                        {
                            "NOT NULL"
                        }, false
                    )
                }
            )
        };
    }
}