using FA.JustBlog.Core.IRepositories;
using FA.JustBlog.Core.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FA.JustBlog.Areas.Admin.Controllers
{
    [Authorize]
    public class ManageCategoryController : Controller
    {
        private readonly ICategoryRepository icategoryRepository;

        public ManageCategoryController(ICategoryRepository icategoryRepository)
        {
            this.icategoryRepository = icategoryRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User, Contributor, Owner")]
        public ActionResult Index(int? i)
        {
            var listCategory = icategoryRepository.GetAll().ToList().ToPagedList(i ?? 1, 10);
            return View(listCategory);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Contributor, Owner")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                icategoryRepository.Add(category);
                return RedirectToAction("index");
            }

            return View(category);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Contributor, Owner")]
        public ActionResult Edit(int id)
        {
            var category = icategoryRepository.FindById(id);

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                icategoryRepository.Update(category);
                return RedirectToAction("index");
            }

            return View(category);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Owner")]
        public ActionResult Delete(int id)
        {

            icategoryRepository.DeleteById(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User, Contributor, Owner")]
        public ActionResult Detail(int id)
        {
            var category = icategoryRepository.FindById(id);

            return View(category);
        }
    }
}