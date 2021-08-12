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
        public string ImageURL { get; set; } = "http://static1.squarespace.com/static/55ef2da9e4b03f6e1ef0cd28/t/5cddb9fb5ed3ff0001d64d24/1558034940526/Mark.jpg?format=1500w";
    }


}
