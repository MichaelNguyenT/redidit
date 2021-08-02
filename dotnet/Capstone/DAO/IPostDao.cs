using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    interface IPostDao
    {
        Post GetPost(string postTitle, string username, string content, int upvoteCounter, int downvoteCounter, DateTime postedDate);
        Post CreatePost(string postTitle, string username, string content);
    }
}
