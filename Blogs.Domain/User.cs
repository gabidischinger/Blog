using System.Collections;
using System.Collections.Generic;

namespace Blogs.Domain
{
    public class User
    {
        public int ID { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public string Name { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
    }
}