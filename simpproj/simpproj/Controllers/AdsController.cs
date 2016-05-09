using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate.Linq;
using simpproj.Models;
using simpproj.ViewModels;
using simpproj.Infrastructure;
using simpproj.Infrastructure.Extensions;
using System.Text.RegularExpressions;

namespace simpproj.Controllers
{
    [SelectedTab("ads")]
    public class AdsController : Controller
    {
        private const int AdsPerPage = 5;

        public ActionResult Index(int page = 1)
        {
            // var totalAdCount = Database.Session.Query<Ad>().Count();

            var baseQuery = Database.Session.Query<Ad>().OrderByDescending(f => f.CreatedAt);

            var totalAdCount = baseQuery.Count();

            // Assortment of ads by ad ID
            var adIds = baseQuery
                .Skip((page - 1) * AdsPerPage)
                .Take(AdsPerPage)
                .Select(p => p.Id)
                .ToArray();

            // Pagination of assorted posts
            var currentAdPage = baseQuery
                .Where(p => adIds.Contains(p.Id))
                .OrderByDescending(c => c.CreatedAt)
                .FetchMany(f => f.Categories)
                .Fetch(f => f.User)
                .ToList();

            return View(new AdsIndex
            {
                Ads = new PagedData<Ad>(currentAdPage, totalAdCount, page, AdsPerPage)
            });
        }

        [Authorize]
        public ActionResult New()
        {
            return View("Form", new AdsForm
            {
                IsNew = true,
                Categories = Database.Session.Query<Category>().Select(category => new CategoryCheckbox
                {
                    Id = category.Id,
                    Name = category.Name,
                    IsChecked = false
                }).ToList()
            });
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var ad = Database.Session.Load<Ad>(id);
            // Returns a not found resource as it shouldn't be possible to 
            // Edit posts that currently exist.
            if (ad == null)
                HttpNotFound();
            // Returns a view with existing db entries with corresponding attributes
            // IsNew boolean property is set to false, indicating the selelction of
            // An existing post.
            return View("Form", new AdsForm
            {
                IsNew = false,
                AdId = id,
                Slug = ad.Slug,
                Title = ad.Title,
                RegistrationYear = ad.RegistrationYear,
                Type = ad.Type,
                Make = ad.Make,
                Model = ad.Model,
                Price = ad.Price,
                Image = ad.Image,
                Location = ad.Location,
                Categories = Database.Session.Query<Category>().Select(category => new CategoryCheckbox
                {
                    Id = category.Id,
                    Name = category.Name,
                    IsChecked = ad.Categories.Contains(category)
                }).ToList()
            });
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false), Authorize]
        public ActionResult Form(AdsForm form)
        {
            form.IsNew = form.AdId == null;

            if (!ModelState.IsValid)
                return View(form);

            var selectedCategories = ReconcileCategories(form.Categories).ToList();

            // Flow control to check if post exists or not. If it exists, instantiate
            // With the creator being the person currently logged in.
            Ad ad;
            if (form.IsNew)
            {
                ad = new Ad
                {
                    CreatedAt = DateTime.UtcNow,
                    User = Auth.User,
                };

                foreach (var tag in selectedCategories)
                    ad.Categories.Add(tag);
            }
            else
            {
                ad = Database.Session.Load<Ad>(form.AdId);

                if (ad == null)
                    return HttpNotFound();

                ad.UpdatedAt = DateTime.UtcNow;

                foreach (var toAdd in selectedCategories.Where(t => !ad.Categories.Contains(t)))
                    ad.Categories.Add(toAdd);

                foreach (var toRemove in selectedCategories.Where(t => !selectedCategories.Contains(t)).ToList())
                    ad.Categories.Remove(toRemove);
            }

            // Model is binded to the form based on the entries on the latter mentioned
            ad.Title = form.Title;
            ad.Slug = form.Slug;
            ad.Colour = form.Colour;
            ad.Model = form.Model;
            ad.Type = form.Type;
            ad.RegistrationYear = form.RegistrationYear;
            ad.Price = form.Price;
            ad.Location = form.Location;
            ad.Image = form.Image;

            // Utilise ORM library for the create or update of a post.
            Database.Session.SaveOrUpdate(ad);

            return RedirectToAction("Index");
        }

        [HttpPost, Authorize]
        public ActionResult Delete(int id)
        {
            var ad = Database.Session.Load<Ad>(id);

            if (ad == null)
                HttpNotFound();

            Database.Session.Delete(ad);
            return RedirectToAction("Index");
        }

        // Will need to create generator on the RoutesConfig cs
        public ActionResult Show(string idAndSlug)
        {
            var parts = SeparateIdAndSlug(idAndSlug);
            if (parts == null)
                return HttpNotFound();

            var ad = Database.Session.Load<Ad>(parts.Item1);
            if (ad == null || ad.IsDeleted)
                return HttpNotFound();

            // Redirect to the correct url if the entered slug or id is incorrect.
            if (!ad.Slug.Equals(parts.Item2, StringComparison.CurrentCultureIgnoreCase))
                return RedirectToRoutePermanent("Ad", new { id = parts.Item1, slug = ad.Slug });

            return View(new AdsShow
            {
                Ad = ad
            });

        }

        private System.Tuple<int, string> SeparateIdAndSlug(string idAndSlug)
        {
            var matches = Regex.Match(idAndSlug, @"^(\d+)\-(.*)?$");
            if (!matches.Success)
                return null;

            // Pulls out first group of regular expression
            var id = int.Parse(matches.Result("$1"));
            // Pulls out second group
            var slug = matches.Result("$2");
            return Tuple.Create(id, slug);
        }

        private IEnumerable<Category> ReconcileCategories(IEnumerable<CategoryCheckbox> categories)
        {
            foreach (var category in categories.Where(t => t.IsChecked))
            {
                if (category.Id != null)
                {
                    yield return Database.Session.Load<Category>(category.Id);
                    continue;
                }

                var existingCategory = Database.Session.Query<Category>().FirstOrDefault(t => t.Name == category.Name);
                // For when a user attempts to add a tag that already exists in the database
                if (existingCategory != null)
                {
                    yield return existingCategory;
                    continue;
                }

                var newCategory = new Category
                {
                    Name = category.Name,
                    Slug = category.Name.Slugify()
                };

                Database.Session.Save(newCategory);
                yield return newCategory;
            }
        }
    }
}