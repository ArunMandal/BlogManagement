using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDOA.Entity
{
 public   class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }
        public string Filename { get; set; }
        public string PostedBy { get; set; }
        public DateTime PostedDate { get; set; }
        public Blog Blog { get; set; }

    }
}
