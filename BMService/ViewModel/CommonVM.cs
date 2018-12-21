using System;
using System.Collections.Generic;
using System.Text;


namespace BMService.ViewModel
{
     public  class BlogDetailVM : BlogVM
    {
        public List<PostVM> Posts { get; set; }
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Filename { get; set; }
        public string PostedBy { get; set; }
        public DateTime PostedDate { get; set; }


    }
}
