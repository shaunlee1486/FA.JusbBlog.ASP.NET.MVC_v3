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
    public class ManageCommentController : Controller
    {
        private ICommentRepository icommentRepository;
        public ManageCommentController(ICommentRepository icommentRepository)
        {
            this.icommentRepository = icommentRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User, Contributor, Owner")]
        public ActionResult Index(int? i)
        {
            var listComment = icommentRepository.GetAll().ToList().ToPagedList(i ?? 1, 10);
            return View(listComment);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Owner")]
        public ActionResult Create()
        {
            var post = new PostRepository();
            SelectList postList = new SelectList(post.GetAll(), "Id", "Title");
            ViewBag.postList = postList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.CommentTime = DateTime.Now;

                icommentRepository.AddComment(comment);
                return RedirectToAction("index");
            }

            return View(comment);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Contributor, Owner")]
        public ActionResult Edit(int id)
        {
            var comment = icommentRepository.FindById(id);
            var post = new PostRepository();
            SelectList postList = new SelectList(post.GetAll(), "Id", "Title");
            ViewBag.postList = postList;
            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.CommentTime = DateTime.Now;
                icommentRepository.Update(comment);
                return RedirectToAction("index");
            }

            return View(comment);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Owner")]
        public ActionResult Delete(int id)
        {
            icommentRepository.DeleteById(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User, Contributor, Owner")]
        public ActionResult Detail(int id)
        {
            var comment = icommentRepository.FindById(id);
            var post = new PostRepository();
            SelectList postList = new SelectList(post.GetAll(), "Id", "Title");
            ViewBag.postList = postList;
            return View(comment);
        }
    }
}