namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeEvents : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Appointments", newName: "AppointmentsEvent");
            DropForeignKey("dbo.CalendarEvents", "OwnerUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CalendarEvents", new[] { "UserEvent_EventId", "UserEvent_SiteUserId" }, "dbo.UserEvents");
            DropForeignKey("dbo.UserEvents", "SiteUserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserEvents", new[] { "SiteUserId" });
            DropIndex("dbo.CalendarEvents", new[] { "OwnerUserId" });
            DropIndex("dbo.CalendarEvents", new[] { "UserEvent_EventId", "UserEvent_SiteUserId" });
            RenameColumn(table: "dbo.UserEvents", name: "SiteUserId", newName: "Creator_Id");
            DropPrimaryKey("dbo.UserEvents");
            CreateTable(
                "dbo.TimeSpanEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BeginDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
            AddColumn("dbo.UserEvents", "Content", c => c.String());
            AddColumn("dbo.UserEvents", "DateCreation", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AppointmentsEvent", "DateCreation", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.UserEvents", "Creator_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.UserEvents", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.UserEvents", "Id");
            CreateIndex("dbo.UserEvents", "Creator_Id");
            AddForeignKey("dbo.UserEvents", "Creator_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.UserEvents", "EventId");
            DropColumn("dbo.UserEvents", "CalendarEventId");
            DropTable("dbo.CalendarEvents");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CalendarEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OwnerUserId = c.String(maxLength: 128),
                        BeginDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Topic = c.String(),
                        Comments = c.String(),
                        UserEventId = c.String(),
                        EventType = c.Int(nullable: false),
                        UserEvent_EventId = c.String(nullable: false, maxLength: 128),
                        UserEvent_SiteUserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UserEvents", "CalendarEventId", c => c.String());
            AddColumn("dbo.UserEvents", "EventId", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.UserEvents", "Creator_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TimeSpanEvents", "Owner_Id", "dbo.AspNetUsers");
            DropIndex("dbo.TimeSpanEvents", new[] { "Owner_Id" });
            DropIndex("dbo.UserEvents", new[] { "Creator_Id" });
            DropPrimaryKey("dbo.UserEvents");
            AlterColumn("dbo.UserEvents", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.UserEvents", "Creator_Id", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.AppointmentsEvent", "DateCreation");
            DropColumn("dbo.UserEvents", "DateCreation");
            DropColumn("dbo.UserEvents", "Content");
            DropTable("dbo.TimeSpanEvents");
            AddPrimaryKey("dbo.UserEvents", new[] { "EventId", "SiteUserId" });
            RenameColumn(table: "dbo.UserEvents", name: "Creator_Id", newName: "SiteUserId");
            CreateIndex("dbo.CalendarEvents", new[] { "UserEvent_EventId", "UserEvent_SiteUserId" });
            CreateIndex("dbo.CalendarEvents", "OwnerUserId");
            CreateIndex("dbo.UserEvents", "SiteUserId");
            AddForeignKey("dbo.UserEvents", "SiteUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CalendarEvents", new[] { "UserEvent_EventId", "UserEvent_SiteUserId" }, "dbo.UserEvents", new[] { "EventId", "SiteUserId" });
            AddForeignKey("dbo.CalendarEvents", "OwnerUserId", "dbo.AspNetUsers", "Id");
            RenameTable(name: "dbo.AppointmentsEvent", newName: "Appointments");
        }
    }
}
