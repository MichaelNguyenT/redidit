using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }
        public int UpvoteCounter { get; set; }
        public int DownvoteCounter { get; set; }
        public DateTime PostedDate { get; set; }
    }


}
