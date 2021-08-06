using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface IPostDao
    {
        Post GetPost(int postId);
        List<Post> GetPosts(int forumId);
        Post CreatePost(int forumId, string postTitle, string username, string content, string imageURL = "");
        void DeletePost(int postId);
        void UpdateUpvoteCounter(int postId, int userId);
        void UpdateDownvoteCounter(int postId, int userId);
        void AddVoteToVoteTable(int postId, int userId, int upOrDown);
        void DeleteVoteFromVoteTable(int userId, int postId);
        bool CheckUserVoteStatus(int userId, int postId);
    }
}
