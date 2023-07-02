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
    public class ManageTagController : Controller
    {
        private readonly ITagRepository itagRepository;

        public ManageTagController(ITagRepository itagRepository)
        {
            this.itagRepository = itagRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User, Contributor, Owner")]
        public ActionResult Index(int? i)
        {
            var listTag = itagRepository.GetAll().ToList().ToPagedList(i ?? 1, 10);
            return View(listTag);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Contributor, Owner")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tag tag)
        {
            if (ModelState.IsValid)
            {
                itagRepository.Add(tag);
                return RedirectToAction("index");
            }

            return View(tag);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Contributor, Owner")]
        public ActionResult Edit(string name)
        {
            var tag = itagRepository.FindTag(name);
            
            return View(tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tag tag)
        {
            if (ModelState.IsValid)
            {
                itagRepository.Update(tag);
                return RedirectToAction("index");
            }

            return View(tag);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Owner")]
        public ActionResult Delete(string name)
        {

            itagRepository.DeleteTag(itagRepository.FindTag(name));
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User, Contributor, Owner")]
        public ActionResult Detail(string name)
        {
            var tag = itagRepository.FindTag(name);

            return View(tag);
        }
    }
}