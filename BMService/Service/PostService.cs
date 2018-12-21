using BlogDOA.Context;
using BlogDOA.Entity;
using BMService.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace BMService.Service
{
    public class PostService
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly BloggingContext _db;

        public PostService(BloggingContext db , IHostingEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
        }

        public void CreatePost(PostVM postVM , IFormFile image)
        {

            string pic = System.Guid.NewGuid() + "_" + System.IO.Path.GetFileName(image.FileName);
            string folderpath = _hostingEnvironment.ContentRootPath;
            string filepath = folderpath + "\\wwwroot\\images\\" + pic;
            FileInfo file = new FileInfo(filepath);
            image.CopyToAsync(new FileStream(filepath, FileMode.Create));


            Post Postobj = new Post
            {
                
                PostId = postVM.PostId,
                Title = postVM.Title,
                Content = postVM.Content,
                BlogId = postVM.BlogId,
                Filename= pic,
                PostedBy=postVM.PostedBy,
                PostedDate= Convert.ToDateTime( DateTime.Now.ToString("dd MMMM yyyy "))

            };
            _db.Posts.Add(Postobj);
            _db.SaveChanges();

        }
    }
}
