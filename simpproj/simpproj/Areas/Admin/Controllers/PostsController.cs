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
    public class PostsController : Controller
    {
        public ActionResult Index()
        {
            return Content("Admini Post");
        }
    }
}