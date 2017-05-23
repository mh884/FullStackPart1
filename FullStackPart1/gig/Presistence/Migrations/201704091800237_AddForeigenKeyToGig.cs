namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeigenKeyToGig : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.gigs", name: "Artist_Id", newName: "ArtistID");
            RenameColumn(table: "dbo.gigs", name: "Genre_id", newName: "GenreID");
            RenameIndex(table: "dbo.gigs", name: "IX_Artist_Id", newName: "IX_ArtistID");
            RenameIndex(table: "dbo.gigs", name: "IX_Genre_id", newName: "IX_GenreID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.gigs", name: "IX_GenreID", newName: "IX_Genre_id");
            RenameIndex(table: "dbo.gigs", name: "IX_ArtistID", newName: "IX_Artist_Id");
            RenameColumn(table: "dbo.gigs", name: "GenreID", newName: "Genre_id");
            RenameColumn(table: "dbo.gigs", name: "ArtistID", newName: "Artist_Id");
        }
    }
}
