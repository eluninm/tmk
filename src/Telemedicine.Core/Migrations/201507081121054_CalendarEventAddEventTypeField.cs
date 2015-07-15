namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CalendarEventAddEventTypeField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CalendarEvents", "EventType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CalendarEvents", "EventType");
        }
    }
}
