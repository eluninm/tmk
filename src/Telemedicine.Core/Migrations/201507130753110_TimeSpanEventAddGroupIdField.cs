namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimeSpanEventAddGroupIdField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeSpanEvents", "IsRepeat", c => c.Boolean(nullable: false));
            AddColumn("dbo.TimeSpanEvents", "Monday", c => c.Boolean(nullable: false));
            AddColumn("dbo.TimeSpanEvents", "Tuesday", c => c.Boolean(nullable: false));
            AddColumn("dbo.TimeSpanEvents", "Wednesday", c => c.Boolean(nullable: false));
            AddColumn("dbo.TimeSpanEvents", "Thursday", c => c.Boolean(nullable: false));
            AddColumn("dbo.TimeSpanEvents", "Friday", c => c.Boolean(nullable: false));
            AddColumn("dbo.TimeSpanEvents", "Saturday", c => c.Boolean(nullable: false));
            AddColumn("dbo.TimeSpanEvents", "Sunday", c => c.Boolean(nullable: false));
            AlterColumn("dbo.TimeSpanEvents", "EndDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TimeSpanEvents", "EndDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            DropColumn("dbo.TimeSpanEvents", "Sunday");
            DropColumn("dbo.TimeSpanEvents", "Saturday");
            DropColumn("dbo.TimeSpanEvents", "Friday");
            DropColumn("dbo.TimeSpanEvents", "Thursday");
            DropColumn("dbo.TimeSpanEvents", "Wednesday");
            DropColumn("dbo.TimeSpanEvents", "Tuesday");
            DropColumn("dbo.TimeSpanEvents", "Monday");
            DropColumn("dbo.TimeSpanEvents", "IsRepeat");
        }
    }
}
