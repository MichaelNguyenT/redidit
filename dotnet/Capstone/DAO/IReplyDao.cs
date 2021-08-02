using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface IReplyDao
    {
        Reply GetReply(int postId, string username, string content, DateTime postedDate);
        Reply CreateReply(int postId, string username, string content);
    }
}
