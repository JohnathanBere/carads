using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace simpproj.Models
{
    public class Ad
    {
        public virtual int Id { get; set; }
        public virtual User User { get; set; }

        public virtual string Title { get; set; }
        public virtual string Slug { get; set; }

        public virtual string Type { get; set; }
        public virtual string Make { get; set; }
        public virtual string Model { get; set; }
        public virtual string Colour { get; set; }
        public virtual string Image { get; set; }
        public virtual decimal Price { get; set; }
        public virtual string RegistrationYear { get; set; }
        public virtual string Location { get; set; }



        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }
        public virtual DateTime? DeletedAt { get; set; }

        public virtual IList<Category> Categories { get; set; }

        public Ad()
        {
            Categories = new List<Category>();
        }

        public virtual bool IsDeleted { get { return DeletedAt != null; } }
    }

    public class AdMap : ClassMapping<Ad>
    {
        public AdMap()
        {
            Table("ads");

            Id(x => x.Id, x => x.Generator(Generators.Identity));

            ManyToOne(x => x.User, x => 
            {
                x.Column("user_id");
                x.NotNullable(true);
            });

            Property(x => x.Title, x => x.NotNullable(true));
            Property(x => x.Slug, x => x.NotNullable(true));

            Property(x => x.Type, x => x.NotNullable(true));
            Property(x => x.Make, x => x.NotNullable(true));
            Property(x => x.Model, x => x.NotNullable(true));

            Property(x => x.Colour, x => x.NotNullable(true));
            Property(x => x.Image, x => x.NotNullable(true));
            Property(x => x.Price, x => x.NotNullable(true));

            Property(x => x.RegistrationYear, x => x.NotNullable(true));
            Property(x => x.Location, x => x.NotNullable(true));

            Property(x => x.CreatedAt, x => {
                x.Column("created_at");
                x.NotNullable(true);
            });

            Property(x => x.UpdatedAt, x => x.Column("updated_at"));
            Property(x => x.DeletedAt, x => x.Column("deleted_at"));

            Bag(x => x.Categories, x =>
            {
                x.Key(y => y.Column("ad_id"));
                x.Table("ad_categories");
            }, x => x.ManyToMany(y => y.Column("category_id")));
        }
    }
}