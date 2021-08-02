using System;
using System.Data.SqlClient;
using Capstone.Models;
using Capstone.Security;
using Capstone.Security.Models;
using System.Collections.Generic;

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

                    SqlCommand cmd = new SqlCommand("SELECT * " +
                        "FROM posts " +
                        "WHERE post_title = @title AND posted_date = @date; ", conn);
                    cmd.Parameters.AddWithValue("@title", postTitle);
                    cmd.Parameters.AddWithValue("@date", postedDate);
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

        public List<Post> GetPosts(int forumId)
        {
            return null;
        }

        public Post CreatePost(int forumId, string postTitle, string username, string content)
        {
            Post returnPost = null;


            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO posts (forum_id, post_title, username, content, upvote_counter, downvote_counter) " +
                        "VALUES (@forum_Id, @title, @username, @content, 0, 0)");
                    cmd.Parameters.AddWithValue("@forum_Id", forumId);
                    cmd.Parameters.AddWithValue("@title", postTitle);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@content", content);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {

                throw;
            }

            return returnPost;
        }

        private Post GetPostFromReader(SqlDataReader reader)
        {
            Post p = new Post()
            {
                PostId = Convert.ToInt32(reader["post_id"]),
                ForumId = Convert.ToInt32(reader["forum_id"]),
                PostTitle = Convert.ToString(reader["post_title"]),
                Username = Convert.ToString(reader["username"]),
                Content = Convert.ToString(reader["content"]),
                UpvoteCounter = Convert.ToInt32(reader["upvote_counter"]),
                DownvoteCounter = Convert.ToInt32(reader["downvote_counter"]),
                PostedDate = Convert.ToDateTime(reader["posted_date"])
            };

            return p;
        }
    }
}
