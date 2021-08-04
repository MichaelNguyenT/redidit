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
    public class ReplyController : ControllerBase
    {
        private readonly IReplyDao replyDao;

        public ReplyController(IReplyDao _replyDao)
        {
            replyDao = _replyDao;
        }

        [AllowAnonymous]
        [HttpGet("/post/{postId}")]
        public ActionResult<List<Reply>> GetReplies(int postId)
        {
            List<Reply> replies = new List<Reply>();
            replies = replyDao.GetReplies(postId);

            if(replies != null)
            {
                return Ok(replies);
            }
            return NotFound();
        }

        [HttpPost("/post")]
        public ActionResult<Reply> CreateReply(Reply reply)
        {
            var returnReply = replyDao.CreateReply(reply.PostId, reply.Username, reply.Content);

            if (returnReply != null)
            {
                return Ok(returnReply);
            }
            return NotFound();
        }

        [HttpDelete("/post/{replyId}")]
        public ActionResult DeleteReply(int replyId)
        {
            if (replyDao.GetReply(replyId) == null)
            {
                return NotFound();
            }
            replyDao.DeleteReply(replyId);
            return NoContent();
        }
    }
}