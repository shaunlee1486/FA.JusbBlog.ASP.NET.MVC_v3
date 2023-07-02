using FA.JustBlog.Areas.Admin.Models;
using FA.JustBlog.Core.IRepositories;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FA.JustBlog.Areas.Admin.Controllers
{
    [Authorize]
    public class ManagePostController : Controller
    {
        private readonly IPostRepository ipostRepository;

        public ManagePostController(IPostRepository ipostRepository)
        {
            this.ipostRepository = ipostRepository;
        }

        [NonAction]
        private void ReveseTag(IPagedList<Post> listPost)
        {
            foreach (var post in listPost)
            {
                foreach (var item in post.Tags)
                {
                    post.TagValues += item.Name + ", ";
                }
                post.TagValues = post.TagValues.Trim(' ', ',');
            }
        }
        

        [HttpGet]
        [Authorize(Roles = "Admin, User, Contributor, Owner")]
        public ActionResult Index(int? i, string search = null)
        {

            search = search ?? "";
            IQueryable<Post> allResults = ipostRepository.GetAllPosts();
            IQueryable<Post> resultGroup = allResults.OrderByDescending(r => r.Title);
            //IQueryable<Post> resultGroup = allResults.Where(c => c.Title.Contains(""));
            var listPost = resultGroup.ToPagedList(i ?? 1, 1);
            ReveseTag(listPost);
            return View(listPost);
        }

        public ActionResult LatestPost(int? i)
        {
            var listPost = ipostRepository.GetAll().ToList().ToPagedList(i ?? 1, 10);
            ReveseTag(listPost);
            return View("Index", listPost);
        }

        public ActionResult MostViewedPosts()
        {
            return Content("not found");
        }

        public ActionResult MostInterestingPosts()
        {
            return Content("not found");
        }

        public ActionResult PublishedPosts()
        {
            return Content("not found");
        }

        public ActionResult UnPublishedPosts()
        {
            return Content("not found");
        }

        [NonAction]
        public void ModifiedPost()
        {
            var category = new CategoryRepository();
            SelectList categoryList = new SelectList(category.GetAll(), "CategoryId", "CategoryName");
            ViewBag.CategoryList = categoryList;
            var tag = new TagRepository();
            MultiSelectList tagList = new MultiSelectList(tag.GetAll(), "Name", "Name");
            ViewBag.TagList = tagList;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Contributor, Owner")]
        public ActionResult Create()
        {
            ModifiedPost();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModelPost viewPost)
        {
            if (ModelState.IsValid)
            {
                viewPost.PostedOn = DateTime.Now;
                var mapperConfig = new AutoMapper.MapperConfiguration(config =>
                {
                    config.CreateMap<ViewModelPost, Post>().ForMember(d => d.TagValues, o => o.MapFrom(s => string.Join(",", s.TagMulti)));
                });
                var mapper = mapperConfig.CreateMapper();
                var post = mapper.Map<ViewModelPost, Post>(viewPost);
                ipostRepository.AddPost(post);
                return RedirectToAction("index");
            }

            return View(viewPost);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Contributor, Owner")]
        public ActionResult Edit(int id)
        {
            var post = ipostRepository.FindById(id);
            foreach (var item in post.Tags)
            {
                post.TagValues += item.Name + ",";
            }
            post.TagValues = post.TagValues.Trim(',');

            var mapperConfig = new AutoMapper.MapperConfiguration(config =>
            {
                config.CreateMap<Post, ViewModelPost>().ForMember(d => d.TagMulti, o => o.MapFrom(s => s.TagValues.Split(',')));
            });
            var mapper = mapperConfig.CreateMapper();
            var viewPost = mapper.Map<Post, ViewModelPost>(post);
            ModifiedPost();
            return View(viewPost);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ViewModelPost viewPost)
        {
            if (ModelState.IsValid)
            {
                viewPost.PostedOn = DateTime.Now;
                var mapperConfig = new AutoMapper.MapperConfiguration(config =>
                {
                    config.CreateMap<ViewModelPost, Post>().ForMember(d => d.TagValues, o => o.MapFrom(s => string.Join(",", s.TagMulti)));
                });
                var mapper = mapperConfig.CreateMapper();
                var post = mapper.Map<ViewModelPost, Post>(viewPost);

                ipostRepository.UpdatePost(post);
                return RedirectToAction("index");
            }

            return View(viewPost);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Owner")]
        public ActionResult Delete(int id)
        {
            ipostRepository.DeleteById(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User, Contributor, Owner")]
        public ActionResult Detail(int id)
        {
            var post = ipostRepository.FindById(id);
            foreach (var item in post.Tags)
            {
                post.TagValues += item.Name + ",";
            }
            post.TagValues = post.TagValues.Trim(',');

            var mapperConfig = new AutoMapper.MapperConfiguration(config =>
            {
                config.CreateMap<Post, ViewModelPost>().ForMember(d => d.TagMulti, o => o.MapFrom(s => s.TagValues.Split(',')));
            });
            var mapper = mapperConfig.CreateMapper();
            var viewPost = mapper.Map<Post, ViewModelPost>(post);
            ModifiedPost();
            return View(viewPost);
        }
    }
}