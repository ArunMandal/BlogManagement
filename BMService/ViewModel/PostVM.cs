using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace BMService.ViewModel
{
   public class PostVM
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        //[UIHint("tinymce_jquery_full"), AllowHtml]
        [UIHint("tinymce_jquery_full")]
        public string Content { get; set; }
        public int BlogId { get; set; }
        public string Filename { get; set; }
        public string PostedBy { get; set; }
        public string PostedDate { get; set; }




    }
}
