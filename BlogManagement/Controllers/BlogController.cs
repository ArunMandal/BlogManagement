using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogDOA.Entity;
using BMService.Service;
using BMService.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BlogManagement.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogService _blgService;

        public BlogController(BlogService blgService)
        {
            _blgService = blgService;

        }
        //Get Blogs
        public IActionResult Index()
        {
            var ked = _blgService.GetBlogsList();

            //   return View(_blgService.GetBlogsList());

            return View(ked);
        }
       
        
        // GET: Blogs/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BlogVM blogVM, string url,int? BlogId)
        {
            //return View(blogVM);
            if (ModelState.IsValid)
            {
                if (BlogId == null)
                {
                    _blgService.CreateBlog(blogVM);
                }

                else
                    _blgService.EditBlog(blogVM);



                return RedirectToAction("Index");
            }

            return View(blogVM);

        }
        
         //Get Posts
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var blog = await _context.Blogs
            //    .FirstOrDefaultAsync(m => m.BlogId == id);
            var blog = _blgService.getPostByBID(id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        //Delete Blogs
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {


                if (id == null)
                {
                    return NotFound();
                }

                _blgService.DeleteBlog(id);
                return RedirectToAction("Index");

            }
            return View();
        }



        //public  IActionResult BlogPosts(int? id)
        //{
        //    var posts =_blgService.getPostByBID(id);
        //    if (posts == null)
        //    {
        //        return NotFound();
        //    }

        //    return View("Details", posts);
        //}
    }
}