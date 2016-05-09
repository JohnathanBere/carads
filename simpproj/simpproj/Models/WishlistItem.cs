using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace simpproj.Models
{
    public class WishlistItem
    {
        public virtual int Id { get; set; }
        public virtual Wishlist Wishlist { get; set; }
        public virtual Ad Ad { get; set; }

        public virtual int WishListId { get; set; }
        public virtual int AdId { get; set; }
    }

    public class WishlistItemMap : ClassMapping<WishlistItem>
    {
        public WishlistItemMap()
        {
            Table("wishlists");

            Id(x => x.Id, x => x.Generator(Generators.Identity));

            ManyToOne(x => x.Wishlist, x => 
            {
                x.Column("wishlist_id");
                x.NotNullable(true);
            });

            OneToOne(x => x.Ad, x => 
            {
                x.ForeignKey("ad_id");
            });
        }
    }
}