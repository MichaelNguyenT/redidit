using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Reply
    {
        public int ReplyId { get; set; }
        public int PostId { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }
        public DateTime PostedDate { get; set; }
    }
}
