using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BMService.ViewModel
{
    public class BlogVM
    {
        public int BlogId { get; set; }

        //[DisplayName("My URL")]
        [Required, StringLength(100, ErrorMessage = "Url too large !!", MinimumLength = 1)]
        public string Url { get; set; }

        [Required]
        public int Count { get; set; }
    }
}
