namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixCustomerBirthdayTypo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Birthday", c => c.DateTime());
            DropColumn("dbo.Customers", "dateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "dateTime", c => c.DateTime());
            DropColumn("dbo.Customers", "Birthday");
        }
    }
}
