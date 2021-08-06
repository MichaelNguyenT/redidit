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
    public class ForumController : ControllerBase
    {
        private readonly IForumDao forumDao;
        private readonly IUserDao userDao;

        public ForumController(IForumDao _forumDao, IUserDao _userDao)
        {
            forumDao = _forumDao;
            userDao = _userDao;
        }

        [AllowAnonymous]
        [HttpGet("/")]
        public ActionResult<Forum> GetForums()
        {
            List<Forum> forums = new List<Forum>();
            forums = forumDao.GetForums();

            if (forums != null)
            {
                return Ok(forums);
            }
            return NotFound();
        }

        [HttpPost("/")]
        public ActionResult<Forum> CreateForum(Forum forum)
        {
            var returnForum = forumDao.CreateForum(forum.ForumTitle);        
            forumDao.PromoteToModerator(GetUserId(), forum.ForumId);

            if (returnForum != null)
            {
                return Ok(returnForum);
            }
            return NotFound();
        }

        [HttpGet("/favorites/{userId}")]
        public IActionResult GetFavorites(int userId)
        {
            List<Forum> forums = new List<Forum>();
            forums = forumDao.GetFavoriteForums(userId);

            if (forums != null)
            {
                return Ok(forums);
            }
            return NotFound();
        }

        [HttpPost("/favorites/user{userId}/forum{forumId}")]
        public IActionResult AddFavorite(int userId, int forumId)
        {
            forumDao.AddFavoriteForum(userId, forumId);

            var returnForum = forumDao.GetFavoriteForum(userId, forumId);

            if (returnForum != null)
            {
                return Ok();
            }
            return BadRequest(new { message = "An error occurred: The forum was not correctly added to favorites." });
        }

        [HttpPost("/promote/user{userId}/forum{forumId}")]
        public ActionResult PromoteUserToModerator(int userId, int forumId)
        {
            int currentUserId = GetUserId();
            if (userDao.CheckAdmin(currentUserId) || forumDao.CheckUserModeratorForum(currentUserId, forumId))
            {
                forumDao.PromoteToModerator(userId, forumId);
            }
            else
            {
                return Unauthorized();
            }
            
            if (forumDao.CheckUserModeratorForum(userId, forumId))
            {
                return Ok();
            }
            return NoContent();
        }

        [HttpDelete("/forum{forumId}/delete")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteForum(int forumId)
        {
            var returnForum = forumDao.GetForum(forumId);

            if (returnForum != null)
            {
                forumDao.DeleteForum(forumId);
                return Ok();
            }
            return BadRequest(new { message = "An error occurred: The forum could not be deleted." });
        }
    }
}
