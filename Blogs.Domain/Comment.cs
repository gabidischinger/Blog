using System;
using System.Collections.Generic;
using System.Text;

namespace Blogs.Domain
{
    public class Comment
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public int PostID { get; set; }
        public virtual Post Post { get; set; }
        public int UserID { get; set; }
        public virtual User User { get; set; }
    }
}
