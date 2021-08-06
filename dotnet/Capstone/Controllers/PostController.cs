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
        private readonly IForumDao forumDao;
        private readonly IUserDao userDao;

        public PostController(IPostDao _postDao, IReplyDao _replyDao, IForumDao _forumDao, IUserDao _userDao)
        {
            postDao = _postDao;
            replyDao = _replyDao;
            forumDao = _forumDao;
            userDao = _userDao;
        }

        [AllowAnonymous]
        [HttpGet("/forum/{forumId}")]
        public ActionResult<List<Post>> GetPosts(int forumId)
        {
            List<Post> posts = new List<Post>();
            posts = postDao.GetPosts(forumId);

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
            var returnPost = postDao.CreatePost(post.ForumId, post.PostTitle, post.Username, post.Content, post.ImageURL);

            if (returnPost != null)
            {
                return Ok(post);
            }
            return NotFound();
        }

        [HttpDelete("/forum/{postId}")]
        public ActionResult DeletePost(int postId)
        {
            if (postDao.GetPost(postId) == null)
            {
                return NotFound();
            }
            else if(!forumDao.CheckUserModeratorForum(GetUserId(), postDao.GetPost(postId).ForumId)
                || userDao.CheckAdmin(GetUserId())
            {
                return Unauthorized();
            }
            postDao.DeletePost(postId);
            return NoContent();
        }

        [HttpPut("/post{postId}/upvotes{upvoteCounter}")]
        public ActionResult UpdateUpvoteCounter(int postId) //front end only needs to pass postId
        {
            int currentUserId = GetUserId();

            if (postDao.GetPost(postId) != null && postDao.CheckUserVoteStatus(GetUserId(), postId) == false)
            {
                postDao.UpdateUpvoteCounter(postId, currentUserId);
                return Ok();
            }
            else if (postDao.GetPost(postId) != null && postDao.CheckUserVoteStatus(GetUserId(), postId) == true)
            {

            }
            return BadRequest(new { message = "An error occurred: Counters could not be updated."  });
        }

        [HttpPut("/post{postId}/downvotes{downvoteCounter}")]
        public ActionResult UpdateDownvoteCounter(int postId, int downvoteCounter)
        {
            if (postDao.GetPost(postId) != null)
            {
                postDao.UpdateDownvoteCounter(postId, downvoteCounter);
                return Ok();
            }
            return BadRequest(new { message = "An error occurred: Counters could not be updated." });
        }
    }
}
