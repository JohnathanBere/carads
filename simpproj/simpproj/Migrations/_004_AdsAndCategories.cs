using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simpproj.Migrations
{
    [Migration(4)]
    public class _004_AdsAndCategories : Migration
    {
        public override void Up()
        {
            Create.Table("ads")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("user_id").AsInt32().ForeignKey("users", "id")
                .WithColumn("title").AsString(128)
                .WithColumn("slug").AsString(128)
                .WithColumn("content").AsCustom("Text")
                .WithColumn("type").AsString(128)
                .WithColumn("make").AsString(128)
                .WithColumn("model").AsString(128)
                .WithColumn("colour").AsString(128)
                .WithColumn("image").AsCustom("VARCHAR(256)")
                .WithColumn("price").AsCurrency()
                .WithColumn("registrationyear").AsCustom("VARCHAR(8)")
                .WithColumn("location").AsCustom("VARCHAR(256)")
                .WithColumn("created_at").AsDateTime()
                .WithColumn("updated_at").AsDateTime().Nullable()
                .WithColumn("deleted_at").AsDateTime().Nullable();

            Create.Table("categories")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("slug").AsString(128)
                .WithColumn("name").AsString(128);

            Create.Table("ad_categories")
                .WithColumn("ad_id").AsInt32().ForeignKey("ads", "id").OnDelete(Rule.Cascade)
                .WithColumn("category_id").AsInt32().ForeignKey("categories", "id").OnDelete(Rule.Cascade);
        }

        public override void Down()
        {
            Delete.Table("ad_categories");
            Delete.Table("ads");
            Delete.Table("categories");
        }
    }
}