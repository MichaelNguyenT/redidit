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
    public class ReplySqlDao : IReplyDao
    {
        private readonly string connectionString;
        public ReplySqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public Reply GetReply(int replyId)
        {
            Reply returnReply = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT reply_id, post_id, username, content, posted_date " +
                        "FROM replies " +
                        "WHERE reply_id = @replyId", conn);
                    cmd.Parameters.AddWithValue("@replyId", replyId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if(reader.Read())
                    {
                        returnReply = GetReplyFromReader(reader);
                    }
                }
            }
            catch  (SqlException e)
            {
                throw;
            }
            return returnReply;
        }

        public List<Reply> GetReplies(int postId)
        {
            List<Reply> replies = new List<Reply>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT reply_id, post_id, username, content, posted_date " +
                        "FROM replies " +
                        "WHERE post_id = @post_Id", conn);
                    cmd.Parameters.AddWithValue("@post_Id", postId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        replies.Add(GetReplyFromReader(reader));
                    }
                }
            }
            catch (SqlException e)
            {
                throw;
            }
            return replies;
        }

        public Reply CreateReply(int postId, string username, string content)
        {
            Reply returnReply = null;
            int newReplyId = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO replies (post_id, username, content, posted_date) " +
                        "OUTPUT INSERTED.*" +
                        "VALUES (@post_Id, @username, @content, GETDATE())", conn);
                    cmd.Parameters.AddWithValue("@post_Id", postId);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@content", content);
                    newReplyId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (SqlException e)
            {
                throw;
            }

            return GetReply(newReplyId);
        }

        public void DeleteReply(int replyId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM replies " +
                                                    "WHERE reply_id = @replyId", conn);
                    cmd.Parameters.AddWithValue("@replyId", replyId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                throw;
            }
        }

        private Reply GetReplyFromReader(SqlDataReader reader)
        {
            Reply r = new Reply()
            {
                ReplyId = Convert.ToInt32(reader["reply_id"]),
                PostId = Convert.ToInt32(reader["post_id"]),
                Username = Convert.ToString(reader["username"]),
                Content = Convert.ToString(reader["content"]),
                PostedDate = Convert.ToDateTime(reader["posted_date"])
            };
            return r;
        }

    }
}
