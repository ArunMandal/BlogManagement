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
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
        // getPostDetail
        public PostVM getPostDetail(int? id)
        {

            // var posts = _db.Posts.FirstOrDefaultAsync(m => m.PostId == id);

            var blgList = _db.Posts.Select(x => new PostVM()
            {

                BlogId = x.BlogId,
                Content = x.Content,
                PostId = x.PostId,
                Title = x.Title,
                Filename = x.Filename,
                PostedBy = x.PostedBy,
                PostedDate = x.PostedDate.ToString("dd MMMM yyyy")

            }).FirstOrDefault(x => x.PostId == id);

            //var posts = _db.Posts.Include(p => p.Blog);
            PostVM p = new PostVM();


            //var post = await _context.Posts
            //    .Include(p => p.Blog)
            //    .FirstOrDefaultAsync(m => m.PostId == id);



            ////var blogs = _db.Blogs.Include(x => x.Posts).FirstOrDefault(x => x.BlogId == id);
            //List<PostVM> posts = new List<PostVM>();
            //posts = blogs.Posts.OrderByDescending(x => x.PostId).Select(x => new PostVM()
            //{
            //    BlogId = x.BlogId,
            //    Content = x.Content.Substring(0, 100) + "...",
            //    PostId = x.PostId,
            //    Title = x.Title,
            //    Filename = x.Filename,
            //    PostedBy = x.PostedBy,
            //    PostedDate = x.PostedDate.ToString("dd MMMM yyyy")
            //}).ToList();


            //var blgList = _db.Blogs.Include(x => x.Posts).Select(x => new BlogDetailVM()
            //{
            //    BlogId = x.BlogId,
            //    Url = x.Url,
            //    Posts = posts

            //}).FirstOrDefault(x => x.BlogId == id);

            return blgList;

        }






    }
}
