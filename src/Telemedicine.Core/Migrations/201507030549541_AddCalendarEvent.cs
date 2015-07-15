namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCalendarEvent : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UserEvents");
            AddColumn("dbo.CalendarEvents", "UserEventId", c => c.String());
            AddColumn("dbo.CalendarEvents", "UserEvent_EventId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.CalendarEvents", "UserEvent_SiteUserId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.UserEvents", "SiteUserId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.UserEvents", "CalendarEventId", c => c.String());
            AddPrimaryKey("dbo.UserEvents", new[] { "EventId", "SiteUserId" });
            CreateIndex("dbo.UserEvents", "SiteUserId");
            CreateIndex("dbo.CalendarEvents", new[] { "UserEvent_EventId", "UserEvent_SiteUserId" });
            AddForeignKey("dbo.CalendarEvents", new[] { "UserEvent_EventId", "UserEvent_SiteUserId" }, "dbo.UserEvents", new[] { "EventId", "SiteUserId" });
            AddForeignKey("dbo.UserEvents", "SiteUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.UserEvents", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserEvents", "UserId", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.UserEvents", "SiteUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CalendarEvents", new[] { "UserEvent_EventId", "UserEvent_SiteUserId" }, "dbo.UserEvents");
            DropIndex("dbo.CalendarEvents", new[] { "UserEvent_EventId", "UserEvent_SiteUserId" });
            DropIndex("dbo.UserEvents", new[] { "SiteUserId" });
            DropPrimaryKey("dbo.UserEvents");
            DropColumn("dbo.UserEvents", "CalendarEventId");
            DropColumn("dbo.UserEvents", "SiteUserId");
            DropColumn("dbo.CalendarEvents", "UserEvent_SiteUserId");
            DropColumn("dbo.CalendarEvents", "UserEvent_EventId");
            DropColumn("dbo.CalendarEvents", "UserEventId");
            AddPrimaryKey("dbo.UserEvents", new[] { "EventId", "UserId" });
        }
    }
}
