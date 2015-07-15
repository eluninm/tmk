namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimeSpanEventAddFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeSpanEvents", "RepeatType", c => c.Int(nullable: false));
            AddColumn("dbo.TimeSpanEvents", "Interval", c => c.Int(nullable: false));
            AddColumn("dbo.TimeSpanEvents", "RepeatCount", c => c.Int(nullable: false));
            AddColumn("dbo.TimeSpanEvents", "RepeatInterval", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TimeSpanEvents", "RepeatInterval");
            DropColumn("dbo.TimeSpanEvents", "RepeatCount");
            DropColumn("dbo.TimeSpanEvents", "Interval");
            DropColumn("dbo.TimeSpanEvents", "RepeatType");
        }
    }
}
