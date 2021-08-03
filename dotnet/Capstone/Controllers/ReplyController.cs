using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Capstone.DAO;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//namespace Capstone.Controllers
//{
//    [Route("[/]")]
//    [ApiController]
//    [Authorize]
//    public class ReplyController : ControllerBase
//    {
//        private readonly IReplyDao replyDao;

//        public ReplyController(IReplyDao _replyDao)
//        {
//            replyDao = _replyDao;
//        }

//        [AllowAnonymous]
//        [HttpGet("/forum/{forumId}/post/{postId}")]
//        public ActionResult<List<Reply>>
//    }
//}