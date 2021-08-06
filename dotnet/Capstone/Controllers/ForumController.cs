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

        public ForumController(IForumDao _forumDao)
        {
            forumDao = _forumDao;
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
            //forumDao.promoteToModerator();
            if (returnForum != null)
            {
                return Ok(returnForum);
            }
            return NotFound();
        }
    }
}
