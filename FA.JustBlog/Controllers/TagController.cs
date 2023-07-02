using FA.JustBlog.Core.IRepositories;
using FA.JustBlog.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FA.JustBlog.Controllers
{
    public class TagController : Controller
    {

        private ITagRepository itagRepository;


        public TagController(ITagRepository itagRepository)
        {
            this.itagRepository = itagRepository;
        }

        [ChildActionOnly]
        public ActionResult PopurlarTags()
        {
            var popularTags = itagRepository.GetAll();

            return PartialView("_PopurlarTags", popularTags);
        }

        // GET: Tag
        public ActionResult Index()
        {
            return View();
        }
    }
}