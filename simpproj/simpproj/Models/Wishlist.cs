using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace simpproj.Models
{
    public class Wishlist
    {
        public virtual int Id { get; set; }

        public virtual int UserId { get; set; }

        public virtual IList<WishlistItem> WishlistItems { get; set; }
        public virtual User User { get; set; }

        public Wishlist()
        {
            WishlistItems = new List<WishlistItem>();
        }
    }

    public class WishlistMap : ClassMapping<Wishlist>
    {
        public WishlistMap()
        {
            Table("wishlists");

            Id(x => x.Id, x => x.Generator(Generators.Identity));

            OneToOne(x => x.User, x => 
            {
                x.ForeignKey("user_id");
            });
        }
    }
}