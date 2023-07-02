using FA.JustBlog.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FA.JustBlog.Controllers
{
    public class PostController : Controller
    {

        private IPostRepository ipostRepository;

        public PostController(IPostRepository ipostRepository)
        {
            this.ipostRepository = ipostRepository;
        }
        // GET: Post
        public ActionResult Index()
        {
            var listPost = ipostRepository.GetAllPosts();
            return View(listPost);
        }

        public ActionResult Details(int year, int month, string title)
        {
            var p = ipostRepository.FindPost(year, month, title);
            if (p == null)


            {
                return HttpNotFound();
            }
            return View(p);
        }

        //public ActionResult LastestPosts()
        //{
        //    return View(post.GetLatestPost(1));
        //}

        [ChildActionOnly]
        public PartialViewResult MostViewedPost()
        {
            ViewBag.PartialName = "Most viewed posts!";
            var mostViewedPosts = ipostRepository.GetMostViewedPost(5);
            //// Map from Post model to PostSummary view model

            return PartialView("_ListPosts", mostViewedPosts);
        }

        [ChildActionOnly]
        public PartialViewResult LatestPosts()
        {
            ViewBag.PartialName = "Latest posts!";
            var latestPosts = ipostRepository.GetLatestPost(5);
            return PartialView("_ListPosts", latestPosts);
        }

        [ActionName("ShowCategory")]
        public ActionResult ShowPostWithCategory(string category)
        {
            var categoryList = ipostRepository.GetPostsByCategory(category);
            return View(categoryList);
        }

        [ChildActionOnly]
        public ActionResult AboutCard()
        {
            return PartialView("_PartialAboutCard");
        }
    }
}