namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsCanceled : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.gigs", "Iscanceled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.gigs", "Iscanceled");
        }
    }
}
