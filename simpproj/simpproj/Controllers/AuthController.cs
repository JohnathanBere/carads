using NHibernate.Linq;
using simpproj.Models;
using simpproj.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace simpproj.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("home");
        }

        public ActionResult Login()
        {
            return View(new AuthLogin {
            });
        }

        // POST: Auth
        [HttpPost]
        public ActionResult Login(AuthLogin form, string returnUrl)
        {
            var user = Database.Session.Query<User>().FirstOrDefault(u => u.Username == form.Username);
            if (user == null)
                simpproj.Models.User.FakeHash();

            if (user == null || !user.CheckPassword(form.Password))
                ModelState.AddModelError("Username", "Username or password is incorrect");

            if (!ModelState.IsValid)
                return View(form);

            FormsAuthentication.SetAuthCookie(user.Username, true);

            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);

            return RedirectToRoute("home");
        }


        public ActionResult Register()
        {
            return View("Register", new UsersForm
            {
                IsNew = true
            });
        }

        public ActionResult Update(int id)
        {
            var user = Database.Session.Load<User>(id);

            if (user == null)
                HttpNotFound();

            return View("Update", new UsersForm
            {
                IsNew = false,
                UserId = id,
                Username = user.Username,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                Address = user.Address,
                Phonenumber = user.Phonenumber
            });
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Form(UsersForm form)
        {
            form.IsNew = form.UserId == null;

            if (!ModelState.IsValid)
                return View(form);

            User user;
            if (form.IsNew)
            {
                user = new User();
            }
            else
            {
                user = Database.Session.Load<User>(form.UserId);

                if (user == null)
                    return HttpNotFound();
            }

            // Model is binded to the form based on the entries on the latter mentioned
            user.Username = form.Username;
            user.Firstname = form.Firstname;
            user.Lastname = form.Lastname;
            user.Address = form.Address;
            user.Phonenumber = form.Phonenumber;
            user.Email = form.Email;

            FormsAuthentication.SetAuthCookie(user.Username, true);

            // Utilise ORM library for the create or update of a user.
            Database.Session.SaveOrUpdate(user);

            return RedirectToAction("Index");
        }


    }
}