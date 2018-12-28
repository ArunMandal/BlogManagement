using BlogDOA.Context;
using System;
using System.Collections.Generic;
using System.Text;
using BMService.ViewModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BlogDOA.Entity;
using Microsoft.EntityFrameworkCore.Internal;
using System.Text.RegularExpressions;

namespace BMService.Service
{
    public class BlogService
    {
        private readonly BloggingContext _db;

        public BlogService(BloggingContext db)
        {
            _db = db;
        }
        //Get Blogs
        public List<BlogVM> GetBlogsList()
        {
            var blgList = _db.Blogs
                                .Select(c => new BlogVM
                                {
                                    BlogId = c.BlogId,
                                    Url = c.Url
                                });
            return blgList.ToList();
        }

        //Create blogs
        public void CreateBlog(BlogVM blogVM)
        {
            Blog blogurl = new Blog
            {
                Url = blogVM.Url
            };
            _db.Blogs.Add(blogurl);
            _db.SaveChanges();

        }

        //Get Posts
        public BlogDetailVM getPostByBID(int? id)
        {
            //string s = Regex.Replace(title, "<.*?>", String.Empty);
            var blogs = _db.Blogs.Include(x => x.Posts).FirstOrDefault(x => x.BlogId == id);
            List<PostVM> posts = new List<PostVM>();
            posts = blogs.Posts.OrderByDescending(x => x.PostId).Select(x => new PostVM()
            {
                BlogId = x.BlogId,
                //  Content = x.Content.Substring(0,100)+"...",

               // Content= Regex.Replace(x.Content, "<.*?>", String.Empty).Trim().Substring(0, 10) + "...",
                Content = Regex.Replace(x.Content, "<.*?>", String.Empty).Trim().Length >100? Regex.Replace(x.Content, "<.*?>", String.Empty).Trim().Substring(0, 100) + "...": Regex.Replace(x.Content, "<.*?>", String.Empty),
                //Content = x.Content,
                PostId = x.PostId,
                Title = x.Title,
                Filename = x.Filename,
                PostedBy = x.PostedBy,
                PostedDate =x.PostedDate.ToString("dd MMMM yyyy")
            }).ToList();


            var blgList = _db.Blogs.Include(x => x.Posts).Select(x => new BlogDetailVM()
            {
                BlogId = x.BlogId,
                Url = x.Url,
                Posts = posts

            }).FirstOrDefault(x => x.BlogId == id);

            return blgList;

        }

        //Edit Blogs
        public void EditBlog(BlogVM blogVM)
        {
            Blog blogurl = new Blog
            {
                Url = blogVM.Url,
                BlogId = blogVM.BlogId
            };
            _db.Blogs.Update(blogurl);
            _db.SaveChanges();

        }

        //Delete Blogs
        public void DeleteBlog(int id)
        {
            Blog blogurl = new Blog
            {
                BlogId = id
            };
            _db.Blogs.Remove(blogurl);
            _db.SaveChanges();
            //var blog = _db.Blogs.FindAsync(id);
            //_db.Blogs.Remove(blog);

            //var blog = await _context.Blogs.FindAsync(id);
            //_context.Blogs.Remove(blog);
            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));


        }





    }
}
