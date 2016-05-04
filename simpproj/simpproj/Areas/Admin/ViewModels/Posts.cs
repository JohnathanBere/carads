using simpproj.Infrastructure;
using simpproj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace simpproj.Areas.Admin.ViewModels
{
    public class PostsIndex
    {
        public PagedData<Post> Posts { get; set; }
    }
}