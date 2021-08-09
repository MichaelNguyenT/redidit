using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Security;
using Capstone.Security.Models;

namespace Capstone.DAO
{
    public class ForumSqlDao : IForumDao
    {
        private readonly string connectionString;
        public ForumSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Forum CreateForum(string forumTitle)
        {
            Forum returnForum = null;
            int newForumId = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO forums_list (forum_title) " +
                        "OUTPUT INSERTED.*" +
                        "VALUES (@forum_title)", conn);
                    cmd.Parameters.AddWithValue("@forum_title", forumTitle);

                    newForumId = Convert.ToInt32(cmd.ExecuteScalar());
                    //promoteToModerator(GetUser(Environment.UserName), newForumId);
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            return GetForum(newForumId);
        }

        public void PromoteToModerator(int userId, int forumId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO user_moderator_forum (user_id, forum_id) " +
                        "VALUES (@user_id, @forum_id);", conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    cmd.Parameters.AddWithValue("@forum_id", forumId);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public bool CheckUserModeratorForum(int userId, int forumId)
        {
            bool isUserModForForum;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * " +
                        "FROM user_moderator_forum " +
                        "WHERE user_id = @user_id AND forum_id = @forum_id;", conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    cmd.Parameters.AddWithValue("@forum_id", forumId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        isUserModForForum = true;
                    }
                    else
                    {
                        isUserModForForum = false;
                    }
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            return isUserModForForum;
        }
        


        //Optional to implement in the future, is not implemented in the ForumController
        public void DeleteForum(int forumId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("BEGIN TRANSACTION " +
                                                        "DELETE FROM replies WHERE post_id IN " + //delete replies in forum (tied to posts)
                                                            "(SELECT post_id FROM posts WHERE forum_id = @forum_id) " +
                                                        "DELETE FROM user_vote_posts WHERE post_id IN " + //delete votes in forum (tied to posts)
                                                            "(SELECT post_id FROM posts WHERE forum_id = @forum_id) " +
                                                        "DELETE FROM posts WHERE forum_id = @forum_id " + //delete posts in forum
                                                        "DELETE FROM user_favorite_forum WHERE forum_id = @forum_id " + //delete favorites in forum
                                                        "DELETE FROM user_moderator_forum WHERE forum_id = @forum_id " + //delete mods in forum
                                                        "DELETE FROM forums_list WHERE forum_id = @forum_id " + //finally deletes the forum
                                                    "COMMIT TRANSACTION", conn);
                    cmd.Parameters.AddWithValue("@forum_id", forumId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public Forum GetForum(int forumId)
        {
            Forum forum = new Forum();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT forum_id, forum_title " +
                                                    "FROM forums_list " +
                                                    "WHERE forum_id = @forumId;", conn);
                    cmd.Parameters.AddWithValue("@forumId", forumId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        forum = GetForumFromReader(reader);
                    }
                }
            }
            catch (SqlException e)
            {
                throw;
            }
            return forum;
        }

        public List<Forum> GetForums()
        {
            List<Forum> forums = new List<Forum>();
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT forum_id, forum_title "+
                                                    "FROM forums_list", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        forums.Add(GetForumFromReader(reader));
                    }
                }
            }
            catch(SqlException e)
            {
                throw;
            }
            return forums;
        }

        public void AddFavoriteForum(int userId, int forumId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO user_favorite_forum (user_id, forum_id) " +
                        "VALUES (@user_id, @forum_id);", conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    cmd.Parameters.AddWithValue("@forum_id", forumId);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        //Return a forum that is in the favorites table. 
        public Forum GetFavoriteForum(int userId, int forumId)
        {
            int returnForumId = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT forum_id " +
                                                    "FROM user_favorite_forum " +
                                                    "WHERE forum_id = @forumId AND user_id = @userId", conn);
                    cmd.Parameters.AddWithValue("@forumId", forumId);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        returnForumId = GetForumIdFromReader(reader); //GetForumIdFromReader
                    }
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            return GetForum(returnForumId);
        }

        public List<Forum> GetFavoriteForums(int userId)
        {
            int returnForumId = 0;
            List<Forum> forums = new List<Forum>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT forum_id " +
                        "FROM user_favorite_forum " +
                        "WHERE user_id = @user_id;", conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        returnForumId = GetForumIdFromReader(reader);
                        forums.Add(GetForum(returnForumId));
                    }
                }
            }
            catch (SqlException e)
            {
                throw e;
            }

            return forums;
        }


        private Forum GetForumFromReader(SqlDataReader reader)
        {
            Forum f = new Forum()
            {
                ForumId = Convert.ToInt32(reader["forum_id"]),
                ForumTitle = Convert.ToString(reader["forum_title"])
            };
            return f;
        }

        private int GetForumIdFromReader(SqlDataReader reader)
        {
            int returnForumId = Convert.ToInt32(reader["forum_id"]);
            return returnForumId;
        }
    }
}
