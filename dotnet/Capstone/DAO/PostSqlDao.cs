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

        public Post GetPost(int postId)
        { //maybe narrow down to just GetPosts? -- use hidden postId to pass if 100% necessary? -DONE
            //Changed this to retrieve a single post from a postID.
            Post returnPost = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT post_id, forum_id, post_title, username, content, upvote_counter, downvote_counter, posted_date " +
                        "FROM posts " +
                        "WHERE post_id = @post_ID;", conn);
                    cmd.Parameters.AddWithValue("@post_ID", postId);
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
            List<Post> posts = new List<Post>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT post_id, forum_id, post_title, username, content, upvote_counter, downvote_counter, posted_date " +
                        "FROM posts " +
                        "WHERE forum_id = @forumId", conn);
                    cmd.Parameters.AddWithValue("forumId", forumId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        posts.Add(GetPostFromReader(reader));
                    }
                }
            }
            catch (SqlException e)
            {
                throw;
            }
            return posts;
        }

        public Post CreatePost(int forumId, string postTitle, string username, string content)
        {
            Post returnPost = null;
            int newPostId = 0;

            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO posts (forum_id, post_title, username, content, upvote_counter, downvote_counter, posted_date) " +
                        "VALUES (@forum_Id, @post_title, @username, @content, 0, 0, GETDATE())", conn);
                    cmd.Parameters.AddWithValue("@forum_Id", forumId);
                    cmd.Parameters.AddWithValue("@post_title", postTitle);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@content", content);
                    //cmd.ExecuteNonQuery();
                    newPostId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (SqlException e)
            {
                throw;
            }

            return GetPost(newPostId);
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
