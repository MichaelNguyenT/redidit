using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface IReplyDao
    {
        Reply GetReply(int replyId);
        List<Reply> GetReplies(int postId);
        Reply CreateReply(int postId, string username, string content);
        void DeleteReply(int replyId);
    }
}
