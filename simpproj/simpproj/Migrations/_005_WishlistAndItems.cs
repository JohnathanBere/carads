using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simpproj.Migrations
{
    [Migration(5)]
    public class _005_WishlistAndItems : Migration
    {
        public override void Up()
        {
            Create.Table("wishlists")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("user_id").AsInt32().ForeignKey("users", "id");

            Create.Table("wishlist_items")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("wishlist_id").AsInt32().ForeignKey("wishlists", "id")
                .WithColumn("ad_id").AsInt32().ForeignKey("ads", "id");
        }

        public override void Down()
        {
            Delete.Table("wishlists");
            Delete.Table("wishlist_items");
        }
    }
}