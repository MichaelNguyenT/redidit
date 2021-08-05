using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IForumDao
    {
        Forum GetForum(int forumId);
        List<Forum> GetForums();
        Forum CreateForum(string forumTitle);
        void promoteToModerator(int userId, int forumId);
        void DeleteForum(int forumId);
    }
}
