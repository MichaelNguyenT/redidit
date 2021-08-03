using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Capstone.DAO;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Controllers
{
    [Route("[/]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly IPostDao postDao;
        private readonly IReplyDao replyDao;

        public PostController(IPostDao _postDao, IReplyDao _replyDao)
        {
            postDao = _postDao;
            replyDao = _replyDao;
        }

        [AllowAnonymous]
        [HttpGet("/forum/{forumId}")]
        public ActionResult<List<Post>> GetPosts(int forumId)
        {
            List<Post> posts = new List<Post>();
            posts = postDao.GetPosts(forumId);

            //foreach (Post post in posts)
            //{
            //    List<Reply> replies = new List<Reply>();

            //    var replyResponses = replyDao.GetReplies(post.PostId);

            //    foreach (Reply reply in replyResponses)
            //    {
            //        replies.Add(reply);
            //    }
                
            //}

            if (posts != null)
            {
                return Ok(posts);
            }
            return NotFound();
        }

        [HttpPost("/forum")]
        public ActionResult<Post> CreatePost(Post post)
        {
            //var returnPost = postDao.CreatePost(forumId, postTitle, username, content);
            var returnPost = postDao.CreatePost(post.ForumId, post.PostTitle, post.Username, post.Content);

            if (returnPost != null)
            {
                return Ok(post);
            }
            return NotFound();
        }
    }
}
