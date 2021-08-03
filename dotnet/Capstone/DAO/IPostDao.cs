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
        Post CreatePost(int forumId, string postTitle, string username, string content);
    }
}
