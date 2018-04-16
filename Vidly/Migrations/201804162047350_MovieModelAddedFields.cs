namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieModelAddedFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "Genre", c => c.String());
            AddColumn("dbo.Movies", "DateReleased", c => c.DateTime(nullable: false));
            AddColumn("dbo.Movies", "DateAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.Movies", "NumberInStock", c => c.Int(nullable: false));

            Sql("INSERT INTO Movies VALUES ('Die Hard 1', 'Action', '2018-01-01', '2018-01-01', 5)");
            Sql("INSERT INTO Movies VALUES ('Die Hard 2', 'Action', '2018-01-01', '2018-01-01', 5)");
            Sql("INSERT INTO Movies VALUES ('Die Hard 3', 'Action', '2018-01-01', '2018-01-01', 5)");
            Sql("INSERT INTO Movies VALUES ('Die Hard 4', 'Action', '2018-01-01', '2018-01-01', 5)");
            Sql("INSERT INTO Movies VALUES ('Die Hard 5', 'Action', '2018-01-01', '2018-01-01', 5)");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "NumberInStock");
            DropColumn("dbo.Movies", "DateAdded");
            DropColumn("dbo.Movies", "DateReleased");
            DropColumn("dbo.Movies", "Genre");
        }
    }
}
