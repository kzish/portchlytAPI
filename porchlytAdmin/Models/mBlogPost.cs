using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portchlytAPI.Models
{
    public class mBlogPost
    {
        public int blog_post_number { get; set; }//auto increment
        public string _id { get; set; } = Guid.NewGuid().ToString();
        public DateTime date { get; set; }= DateTime.Now;
        public string headline_image { get; set; }//image to appear on the head of the blog
        public string blog_title { get; set; }//head line title
    }
}
