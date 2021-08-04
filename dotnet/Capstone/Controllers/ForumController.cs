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
            throw new NotImplementedException();
        }
    }
}
