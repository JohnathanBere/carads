using NHibernate.Linq;
using simpproj.Areas.Admin.ViewModels;
using simpproj.Infrastructure;
using simpproj.Infrastructure.Extensions;
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

            var baseQuery = Database.Session.Query<Post>().OrderByDescending(f => f.CreatedAt);

            // Assortment of posts by post ID
            var postIds = baseQuery
                .Skip((page - 1) * PostsPerPage)
                .Take(PostsPerPage)
                .Select(p => p.Id)
                .ToArray();

            // Pagination of assorted posts
            var currentPostPage = baseQuery
                .Where(p => postIds.Contains(p.Id))
                .OrderByDescending(c => c.CreatedAt)
                .FetchMany(f => f.Tags)
                .Fetch(f => f.User)
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
                IsNew = true,
                Tags = Database.Session.Query<Tag>().Select(tag => new TagCheckbox
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    IsChecked = false
                }).ToList()
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
                Title = post.Title,
                Tags = Database.Session.Query<Tag>().Select(tag => new TagCheckbox
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    IsChecked = post.Tags.Contains(tag)
                }).ToList()
            });
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Form(PostsForm form)
        {
            form.IsNew = form.PostId == null;

            if (!ModelState.IsValid)
                return View(form);

            var selectedTags = ReconcileTags(form.Tags).ToList();

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

                foreach (var tag in selectedTags)
                    post.Tags.Add(tag);
            }
            else
            {
                post = Database.Session.Load<Post>(form.PostId);

                if (post == null)
                    return HttpNotFound();

                post.UpdatedAt = DateTime.UtcNow;

                foreach (var toAdd in selectedTags.Where(t => !post.Tags.Contains(t)))
                    post.Tags.Add(toAdd);

                foreach (var toRemove in selectedTags.Where(t => !selectedTags.Contains(t)).ToList())
                    post.Tags.Remove(toRemove);
            }

            // Model is binded to the form based on the entries on the latter mentioned
            post.Title = form.Title;
            post.Slug = form.Slug;
            post.Content = form.Content;

            // Utilise ORM library for the create or update of a post.
            Database.Session.SaveOrUpdate(post);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Trash(int id)
        {
            var post = Database.Session.Load<Post>(id);

            if (post == null)
                HttpNotFound();

            post.DeletedAt = DateTime.UtcNow;
            Database.Session.Update(post);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var post = Database.Session.Load<Post>(id);

            if (post == null)
                HttpNotFound();

            Database.Session.Delete(post);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Restore(int id)
        {
            var post = Database.Session.Load<Post>(id);

            if (post == null)
                HttpNotFound();

            post.DeletedAt = null;
            Database.Session.Update(post);
            return RedirectToAction("Index");
        }

        private IEnumerable<Tag> ReconcileTags(IEnumerable<TagCheckbox> tags)
        {
            foreach (var tag in tags.Where(t => t.IsChecked))
            {
                if ( tag.Id != null)
                {
                    yield return Database.Session.Load<Tag>(tag.Id);
                    continue;
                }

                var existingTag = Database.Session.Query<Tag>().FirstOrDefault(t => t.Name == tag.Name);
                // For when a user attempts to add a tag that already exists in the database
                if (existingTag != null)
                {
                    yield return existingTag;
                    continue;
                }

                var newTag = new Tag
                {
                    Name = tag.Name,
                    Slug = tag.Name.Slugify()
                };

                Database.Session.Save(newTag);
                yield return newTag;
            }
        }
    }
}