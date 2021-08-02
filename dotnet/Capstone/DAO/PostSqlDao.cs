using System;
using System.Data.SqlClient;
using Capstone.Models;
using Capstone.Security;
using Capstone.Security.Models;

namespace Capstone.DAO
{
    public class PostSqlDao : IPostDao
    {
        private readonly string connectionString;

        public PostSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Post GetPost(string postTitle, string username, string content, int upvoteCounter, int downvoteCounter, DateTime postedDate)
        {
            Post returnPost = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("", conn);
                    cmd.Parameters.AddWithValue("", postTitle);
                    cmd.Parameters.AddWithValue("", postedDate);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        returnPost = GetPostFromReader(reader);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnPost;
        }

        public Post CreatePost(string postTitle, string username, string content)
        {
            return null;
        }

        private Post GetPostFromReader(SqlDataReader reader)
        {
            Post p = new Post()
            {
                PostId = Convert.ToInt32(reader[""]),
                PostTitle = Convert.ToString(reader[""]),
                Username = Convert.ToString(reader[""]),
                Content = Convert.ToString(reader[""]),
                UpvoteCounter = Convert.ToInt32(reader[""]),
                DownvoteCounter = Convert.ToInt32(reader[""]),
                PostedDate = Convert.ToDateTime(reader[""])
            };

            return p;
        }
    }
}
