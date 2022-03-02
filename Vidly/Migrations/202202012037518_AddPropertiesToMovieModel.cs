namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropertiesToMovieModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "GenreID", c => c.Int(nullable: false));
            AddColumn("dbo.Movies", "ReleaseDate", c => c.DateTime());
            AddColumn("dbo.Movies", "DateAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.Movies", "NumberInStock", c => c.Int(nullable: false));
            CreateIndex("dbo.Movies", "GenreID");
            AddForeignKey("dbo.Movies", "GenreID", "dbo.Genres", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "GenreID", "dbo.Genres");
            DropIndex("dbo.Movies", new[] { "GenreID" });
            DropColumn("dbo.Movies", "NumberInStock");
            DropColumn("dbo.Movies", "DateAdded");
            DropColumn("dbo.Movies", "ReleaseDate");
            DropColumn("dbo.Movies", "GenreID");
        }
    }
}
