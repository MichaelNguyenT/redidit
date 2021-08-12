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

                    SqlCommand cmd = new SqlCommand("SELECT post_id, forum_id, post_title, username, content, upvote_counter, downvote_counter, posted_date, image_url " +
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

                    SqlCommand cmd = new SqlCommand("SELECT post_id, forum_id, post_title, username, content, upvote_counter, downvote_counter, posted_date, image_url " +
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
                throw e;
            }
            return posts;
        }

        public List<Post> GetTodaysPopularPosts()
        {
            List<Post> popular = new List<Post>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT TOP (10) *,(upvote_counter - downvote_counter) AS combined_votes " +
                        "FROM posts " +
                        "WHERE posted_date >= DATEADD(hh, -24, GETDATE()) AND (upvote_counter - downvote_counter) > 0 " +
                        "ORDER BY combined_votes DESC;", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        popular.Add(GetPostFromReader(reader));
                    }
                }
            }
            catch (SqlException e)
            {
                throw e;
            }

            return popular;
        }

        public Post CreatePost(int forumId, string postTitle, string username, string content, string imageURL)
        {
            int newPostId = 0;

            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO posts (forum_id, post_title, username, content, upvote_counter, downvote_counter, posted_date, image_url) " +
                        "OUTPUT INSERTED.*" +
                        "VALUES (@forum_Id, @post_title, @username, @content, 0, 0, GETDATE(), @image_URL)", conn);
                    cmd.Parameters.AddWithValue("@forum_Id", forumId);
                    cmd.Parameters.AddWithValue("@post_title", postTitle);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@content", content);
                    cmd.Parameters.AddWithValue("@image_URL", imageURL);
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

        public void DeletePost(int postId) //deletes post and all replies associated with that postId
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("BEGIN TRANSACTION "+
                                                        "DELETE FROM replies "+
                                                        "WHERE post_id = @postId "+
                                                        "DELETE FROM posts "+
                                                        "WHERE post_id = @postId " +
                                                    "COMMIT TRANSACTION", conn);
                    cmd.Parameters.AddWithValue("@postId", postId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                throw;
            }
        }

        public void UpdateUpvoteCounter(int postId, int userId) 
        {
            int newUp = 0;
            int newDown = 0;
            Post post = GetPost(postId);
            int voteStatus = CheckUserVoteStatus(userId, postId); //voteStatus: 0 = downvote, 1 = upvote, 2 = default state

            if (voteStatus == 0)
            {
                newUp = post.UpvoteCounter + 1;
                newDown = post.DownvoteCounter - 1;
                UpdateUserVoteStatus(userId, postId, 1);
            }
            else if(voteStatus == 1)
            {
                newUp = post.UpvoteCounter - 1;
                newDown = post.DownvoteCounter;
                UpdateUserVoteStatus(userId, postId, 2);
            }
            else if (voteStatus == 2)
            {
                newUp = post.UpvoteCounter + 1;
                newDown = post.DownvoteCounter;
                UpdateUserVoteStatus(userId, postId, 1);
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE posts " +
                                                    "SET upvote_counter = @upvoteCounter, " +
                                                    "downvote_counter = @downvoteCounter "+
                                                    "WHERE post_id = @postId", conn);
                    cmd.Parameters.AddWithValue("@upvoteCounter", newUp);
                    cmd.Parameters.AddWithValue("@downvoteCounter", newDown);
                    cmd.Parameters.AddWithValue("@postId", postId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }

        public void UpdateDownvoteCounter(int postId, int userId)
        {
            int newUp = 0;
            int newDown = 0;
            Post post = GetPost(postId);
            int voteStatus = CheckUserVoteStatus(userId, postId); //voteStatus: 0 = downvote, 1 = upvote, 2 = default state

            if (voteStatus == 0)
            {
                newUp = post.UpvoteCounter;
                newDown = post.DownvoteCounter - 1;
                UpdateUserVoteStatus(userId, postId, 2);
            }
            else if (voteStatus == 1)
            {
                newUp = post.UpvoteCounter - 1;
                newDown = post.DownvoteCounter + 1;
                UpdateUserVoteStatus(userId, postId, 0);
            }
            else if (voteStatus == 2)
            {
                newUp = post.UpvoteCounter;
                newDown = post.DownvoteCounter + 1;
                UpdateUserVoteStatus(userId, postId, 0);
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE posts " +
                                                    "SET upvote_counter = @upvoteCounter, " +
                                                    "downvote_counter = @downvoteCounter " +
                                                    "WHERE post_id = @postId", conn);
                    cmd.Parameters.AddWithValue("@upvoteCounter", newUp);
                    cmd.Parameters.AddWithValue("@downvoteCounter", newDown);
                    cmd.Parameters.AddWithValue("@postId", postId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }

        public void AddVoteToVoteTable(int postId, int userId, int upOrDown)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO user_vote_posts (user_id, post_id, vote) " +
                                                    "VALUES (@userId, @postId, @vote)", conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@postId", postId);
                    cmd.Parameters.AddWithValue("@vote", upOrDown);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void DeleteVoteFromVoteTable(int userId, int postId)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM user_vote_posts " +
                                                    "WHERE user_id = @userId AND post_id = @postId", conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@postId", postId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int CheckForUserVote(int userId, int postId)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(user_id) " +
                                                    "FROM user_vote_posts " +
                                                    "WHERE user_id = @userId AND post_id = @postId", conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@postId", postId);
                    int userHasVoted = Convert.ToInt32(cmd.ExecuteScalar());
                    return userHasVoted;
                }
            }
            catch
            {
                throw;
            }
        }

        public int CheckUserVoteStatus(int userId, int postId)
        {
            try
            {
                int voteStatus = 2;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT vote "+
                                                    "FROM user_vote_posts "+
                                                    "WHERE user_id = @userId AND post_id = @postId", conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@postId", postId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        voteStatus = Convert.ToInt32(reader["vote"]);
                    }
                    return voteStatus;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void UpdateUserVoteStatus(int userId, int postId, int voteStatus)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE user_vote_posts " +
                                                    "SET vote = @voteStatus " +
                                                    "WHERE user_id = @userId AND post_id = @postId", conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@postId", postId);
                    cmd.Parameters.AddWithValue("@voteStatus", voteStatus);
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
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
                PostedDate = Convert.ToDateTime(reader["posted_date"]),
                ImageURL = Convert.ToString(reader["image_url"])
            };

            return p;
        }
    }
}
