using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Data.SqlClient;
using Capstone.Models;
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
        public Reply GetReply(int postId, string username, string content, DateTime postedDate)
        {
            Reply returnReply = null; 

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("  SELECT * " +
                        "FROM replies " +
                        "WHERE post_id = @postId", conn);
                    cmd.Parameters.AddWithValue("@postId", postId);
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

        public Reply CreateReply(int postId, string username, string content)
        {
            throw new NotImplementedException();
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
