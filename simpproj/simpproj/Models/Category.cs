using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace simpproj.Models
{
    public class Category
    {
        public virtual int Id { get; set; }
        public virtual string Slug { get; set; }
        public virtual string Name { get; set; }

        public virtual IList<Ad> Ads { get; set; }

        public Category()
        {
            Ads = new List<Ad>();
        }
    }

    public class CategoryMap : ClassMapping<Category>
    {
        public CategoryMap()
        {
            Table("categories");

            Id(x => x.Id, x => x.Generator(Generators.Identity));

            Property(x => x.Name, x => x.NotNullable(true));
            Property(x => x.Slug, x => x.NotNullable(true));

            Bag(x => x.Ads, x => 
            {
                x.Key(y => y.Column("category_id"));
                x.Table("ad_categories");
            }, x => x.ManyToMany(y => y.Column("ad_id")));
        }
    }
}