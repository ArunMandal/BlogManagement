using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BMService.Service;
using BMService.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogManagement.Controllers
{
    public class PostController : Controller
    {
     //   private IHostingEnvironment _hostingEnvironment;
        private readonly PostService _pstService;

        public PostController(PostService pstService)
        {
            _pstService = pstService;
          //  _hostingEnvironment = hostingEnvironment;

        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PostVM postVM, int blogid,IFormFile image)
        {
            if (ModelState.IsValid)
            {
                //file.SaveAs(physicalPath);

                _pstService.CreatePost(postVM, image);
              

             
               
                return RedirectToAction("Details", "Blog", new {id= postVM.BlogId });
            }

            return View(postVM);

        }


        // PostDetail

        public IActionResult PostDetail(int? id)
        {

           
               

             var posts=   _pstService.getPostDetail(id);




                //return RedirectToAction("Details", "Blog", new { id = postVM.BlogId });
                //return RedirectToAction("PostDetail");
            

            return View(posts);
            
        }
    }
}