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
        Forum CreateForum(string forumTitle, string forumPicture);
        void PromoteToModerator(int userId, int forumId);
        bool CheckUserModeratorForum(int userId, int forumId);
        void DeleteForum(int forumId);
        void AddFavoriteForum(int userId, int forumId);
        List<Forum> GetFavoriteForums(int userId);
        Forum GetFavoriteForum(int userId, int forumId);
    }
}
