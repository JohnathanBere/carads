using NHibernate.Linq;
using simpproj.Areas.Admin.ViewModels;
using simpproj.Infrastructure;
using simpproj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace simpproj.Areas.Admin.Controllers
{
    /* 
    *Somewhat similar to Laravel's middleware restricting use
    */
    [Authorize(Roles = "admin")]
    [SelectedTab("posts")]
    public class PostsController : Controller
    {
        private const int PostsPerPage = 5;

        public ActionResult Index(int page = 1)
        {
            var totalPostCount = Database.Session.Query<Post>().Count();
            var currentPostPage = Database.Session.Query<Post>()
                .OrderByDescending(c => c.CreatedAt)
                .Skip((page - 1) * PostsPerPage)
                .Take(PostsPerPage)
                .ToList();

            return View(new PostsIndex
            {
                Posts = new PagedData<Post>(currentPostPage, totalPostCount, page, PostsPerPage)
            });
        }

        public ActionResult New()
        {
            return View("Form", new PostsForm
            {
                IsNew = true
            });
        }

        public ActionResult Edit(int id)
        {
            var post = Database.Session.Load<Post>(id);
            // Returns a not found resource as it shouldn't be possible to 
            // Edit posts that currently exist.
            if (post == null)
                HttpNotFound();
            // Returns a view with existing db entries with corresponding attributes
            // IsNew boolean property is set to false, indicating the selelction of
            // An existing post.
            return View("Form", new PostsForm
            {
                IsNew = false,
                PostId = id,
                Content = post.Content,
                Slug = post.Slug,
                Title = post.Title
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Form(PostsForm form)
        {
            form.IsNew = form.PostId == null;

            if (!ModelState.IsValid)
                return View(form);

            // Flow control to check if post exists or not. If it exists, instantiate
            // With the creator being the person currently logged in.
            Post post;
            if (form.IsNew)
            {
                post = new Post
                {
                    CreatedAt = DateTime.UtcNow,
                    User = Auth.User,
                };
            }
            else
            {
                post = Database.Session.Load<Post>(form.PostId);

                if (post == null)
                    return HttpNotFound();

                post.UpdatedAt = DateTime.UtcNow;
            }

            // Model is binded to the form based on the entries on the latter mentioned
            post.Title = form.Title;
            post.Slug = form.Slug;
            post.Content = form.Content;

            // Utilise ORM library for the create or update of a post.
            Database.Session.SaveOrUpdate(post);

            return RedirectToAction("Index");
        }
    }
}