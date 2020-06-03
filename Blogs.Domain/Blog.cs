using System;
using System.Collections;
using System.Collections.Generic;

namespace Blogs.Domain
{
    public class Blog
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public virtual User Owner { get; set; }
        public int OwnerID { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
