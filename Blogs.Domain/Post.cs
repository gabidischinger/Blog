using System;
using System.Collections;
using System.Collections.Generic;

namespace Blogs.Domain
{
    public class Post
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int OwnerID { get; set; }
        public virtual User Owner { get; set; }
        public int BlogID { get; set; }
        public virtual Blog Blog { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}