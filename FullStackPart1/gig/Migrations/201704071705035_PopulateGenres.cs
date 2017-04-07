using System.Data.Entity.Migrations;

namespace GigHub.Migrations
{
    public partial class PopulateGenres : DbMigration
    {
        public override void Up()
        {
            Sql("insert into genres (id,name) Values (1,'Jazz')");
            Sql("insert into genres (id,name) Values (2,'Blues')");
            Sql("insert into genres (id,name) Values (3,'Rock')");
            Sql("insert into genres (id,name) Values (4,'Country')");
        }

        public override void Down()
        {
            Sql("Delete from genurs where id in (1,2,3,4)");
        }
    }
}