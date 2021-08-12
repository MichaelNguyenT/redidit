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

        [AllowAnonymous]
        [HttpGet("/popular")]
        public ActionResult<List<Post>> GetPopularToday()
        {
            List<Post> popular = new List<Post>();
            popular = postDao.GetTodaysPopularPosts();

            if (popular != null)
            {
                return Ok(popular);
            }
            return NotFound();
        }

        [AllowAnonymous]
        [HttpGet("/posts/{postId}")]
        public ActionResult<Post> GetPost(int postId)
        {
            Post post = new Post();
            post = postDao.GetPost(postId);

            if(post != null)
            {
                return Ok(post);
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
            else if(userDao.CheckAdmin(GetUserId()) || forumDao.CheckUserModeratorForum(GetUserId(), postDao.GetPost(postId).ForumId))
            {
                postDao.DeletePost(postId);
                return NoContent();
            }
            return Unauthorized();
        }

        [HttpPut("/upvotes{postId}")]
        public ActionResult UpdateUpvoteCounter(int postId) //front end only needs to pass postId
        {
              int currentUserId = GetUserId();
              if (postDao.GetPost(postId) != null)
              {
                int userHasVote = postDao.CheckForUserVote(currentUserId, postId);
                int voteStatus = postDao.CheckUserVoteStatus(currentUserId, postId); //voteStatus: 0 = downvote, 1 = upvote, 2 = default state
                if (userHasVote == 0)
                {
                    postDao.AddVoteToVoteTable(postId, currentUserId, 2);
                    if (voteStatus == 0)
                    {
                        postDao.UpdateUpvoteCounter(postId, currentUserId);
                        return Ok("plusminus"); //first is upvote second is downvote
                    }
                    else if (voteStatus == 1)
                    {
                        postDao.UpdateUpvoteCounter(postId, currentUserId);
                        return Ok("minusno");
                    }
                    else if (voteStatus == 2)
                    {
                        postDao.UpdateUpvoteCounter(postId, currentUserId);
                        return Ok("plusno");
                    }
                }
                else
                {
                    if (voteStatus == 0)
                    {
                        postDao.UpdateUpvoteCounter(postId, currentUserId);
                        return Ok("plusminus"); //first is upvote second is downvote
                    }
                    else if (voteStatus == 1)
                    {
                        postDao.UpdateUpvoteCounter(postId, currentUserId);
                        return Ok("minusno");
                    }
                    else if (voteStatus == 2)
                    {
                        postDao.UpdateUpvoteCounter(postId, currentUserId);
                        return Ok("plusno");
                    }
                }
            }
            return BadRequest(new { message = "An error occurred: Counters could not be updated."  });
        }

        [HttpPut("/downvotes{postId}")]
        public ActionResult UpdateDownvoteCounter(int postId, int downvoteCounter)
        {
            int currentUserId = GetUserId();
            if (postDao.GetPost(postId) != null)
            {
                int userHasVote = postDao.CheckForUserVote(currentUserId, postId);
                int voteStatus = postDao.CheckUserVoteStatus(currentUserId, postId); //voteStatus: 0 = downvote, 1 = upvote, 2 = default state
                if (userHasVote == 0)
                {
                    postDao.AddVoteToVoteTable(postId, currentUserId, 2);
                    if (voteStatus == 0)
                    {
                        postDao.UpdateDownvoteCounter(postId, currentUserId);
                        return Ok("nominus"); //first is upvote second is downvote
                    }
                    else if (voteStatus == 1)
                    {
                        postDao.UpdateDownvoteCounter(postId, currentUserId);
                        return Ok("minusplus");
                    }
                    else if (voteStatus == 2)
                    {
                        postDao.UpdateDownvoteCounter(postId, currentUserId);
                        return Ok("noplus");
                    }
                }
                else
                {
                    if (voteStatus == 0)
                    {
                        postDao.UpdateDownvoteCounter(postId, currentUserId);
                        return Ok("nominus"); //first is upvote second is downvote
                    }
                    else if (voteStatus == 1)
                    {
                        postDao.UpdateDownvoteCounter(postId, currentUserId);
                        return Ok("minusplus");
                    }
                    else if (voteStatus == 2)
                    {
                        postDao.UpdateDownvoteCounter(postId, currentUserId);
                        return Ok("noplus");
                    }
                }
            }
            return BadRequest(new { message = "An error occurred: Counters could not be updated." });
        }
    }
}
