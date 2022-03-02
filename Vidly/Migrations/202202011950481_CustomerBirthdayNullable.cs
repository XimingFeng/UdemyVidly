namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerBirthdayNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "dateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "dateTime", c => c.DateTime(nullable: false));
        }
    }
}
