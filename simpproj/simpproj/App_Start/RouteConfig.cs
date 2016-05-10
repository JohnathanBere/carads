using simpproj.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace simpproj
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var namespaces = new[] { typeof(PostsController).Namespace};
            var anamespaces = new[] { typeof(AdsController).Namespace };
            var authnamespaces = new[] { typeof(AuthController).Namespace };
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("ActualCategory", "category/{idAndSlug}", new { controller = "Ads", action = "Category"}, anamespaces);
            routes.MapRoute("Category", "category/{id}-{slug}", new { controller = "Ads", action = "Category"}, anamespaces);

            routes.MapRoute("ActualAd", "ad/{idAndSlug}", new { controller = "Ads", action = "Show" }, anamespaces);
            routes.MapRoute("Ad", "ad/{id}-{slug}", new { controller = "Ads", action = "Show"}, anamespaces);

            routes.MapRoute("ActualTag", "tag/{idAndSlug}", new { controller = "Posts", action = "Tag" }, namespaces);
            routes.MapRoute("Tag", "tag/{id}-{slug}", new { controller = "Posts", action = "Tag" }, namespaces);

            routes.MapRoute("ActualPost", "post/{idAndSlug}", new { controller = "Posts", action = "Show" }, namespaces);
            routes.MapRoute("Post", "post/{id}-{slug}", new { controller = "Posts", action = "Show" }, namespaces);

            routes.MapRoute("Login", "login", new { controller = "Auth", action = "Login" }, namespaces);

            routes.MapRoute("Register", "register", new { controller = "Auth", action = "Register" }, authnamespaces);
            routes.MapRoute("Update", "update/{id}", new { controller = "Auth", action = "Update" }, authnamespaces);

            routes.MapRoute("Home", "", new { controller = "Posts", action = "Index" }, namespaces);
            routes.MapRoute("Ads", "ads", new { controller = "Ads", action = "Index" }, anamespaces);
            routes.MapRoute("New Ad", "ads/new", new { controller = "Ads", action = "New" }, anamespaces);
            // routes.MapRoute("Site_Default", "ad/{controller}/{action}/{id}", new { action = "Index", id = UrlParameter.Optional });

            routes.MapRoute("Logout", "logout", new { controller = "Auth", action = "Logout" }, namespaces);
        }
    }
}
