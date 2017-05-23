using System.Data.Entity.Migrations;

namespace GigHub.Migrations
{
    public partial class overideConventionForGig : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.gigs", "Artist_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.gigs", "Genre_id", "dbo.Genres");
            DropIndex("dbo.gigs", new[] {"Artist_Id"});
            DropIndex("dbo.gigs", new[] {"Genre_id"});
            AlterColumn("dbo.Genres", "Name", c => c.String(false, 255));
            AlterColumn("dbo.gigs", "Venue", c => c.String(false, 255));
            AlterColumn("dbo.gigs", "Artist_Id", c => c.String(false, 128));
            AlterColumn("dbo.gigs", "Genre_id", c => c.Byte(false));
            CreateIndex("dbo.gigs", "Artist_Id");
            CreateIndex("dbo.gigs", "Genre_id");
            AddForeignKey("dbo.gigs", "Artist_Id", "dbo.AspNetUsers", "Id", true);
            AddForeignKey("dbo.gigs", "Genre_id", "dbo.Genres", "id", true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.gigs", "Genre_id", "dbo.Genres");
            DropForeignKey("dbo.gigs", "Artist_Id", "dbo.AspNetUsers");
            DropIndex("dbo.gigs", new[] {"Genre_id"});
            DropIndex("dbo.gigs", new[] {"Artist_Id"});
            AlterColumn("dbo.gigs", "Genre_id", c => c.Byte());
            AlterColumn("dbo.gigs", "Artist_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.gigs", "Venue", c => c.String());
            AlterColumn("dbo.Genres", "Name", c => c.String());
            CreateIndex("dbo.gigs", "Genre_id");
            CreateIndex("dbo.gigs", "Artist_Id");
            AddForeignKey("dbo.gigs", "Genre_id", "dbo.Genres", "id");
            AddForeignKey("dbo.gigs", "Artist_Id", "dbo.AspNetUsers", "Id");
        }
    }
}