using simpproj.Infrastructure;
using simpproj.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace simpproj.ViewModels
{
    public class CategoryCheckbox
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }
    
    public class AdsIndex
    {
        public PagedData<Ad> Ads { get; set; }
    }

    public class AdsShow
    {
        public Ad Ad { get; set; }
    }

    public class AdsForm
    {
        public bool IsNew { get; set; }

        // int? Determines if we are looking at the creation of new data or the alteration of existing data.
        public int? AdId { get; set; }

        [Required, MaxLength(128)]
        public string Title { get; set; }

        [Required, MaxLength(128)]
        public string Slug { get; set; }

        public  string Type { get; set; }
        public  string Make { get; set; }
        public  string Model { get; set; }
        public  string Colour { get; set; }
        public  string Image { get; set; }
        public  decimal Price { get; set; }
        public  string RegistrationYear { get; set; }
        public  string Location { get; set; }

        public IList<CategoryCheckbox> Categories { get; set; }
    }

    public class AdsCategory
    {
        public Category Category { get; set; }
        public PagedData<Ad> Ads { get; set; }
    }
}