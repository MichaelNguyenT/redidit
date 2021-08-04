using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public int ForumId { get; set; }
        public string PostTitle { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }
        public int UpvoteCounter { get; set; } = 0;
        public int DownvoteCounter { get; set; } = 0;
        public DateTime PostedDate { get; set; }
        //public List<Reply> Replies { get; set; }
    }


}
