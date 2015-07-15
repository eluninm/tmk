namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CalendarOwnerUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CalendarEvents", "OwnerUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.CalendarEvents", "OwnerUserId");
            AddForeignKey("dbo.CalendarEvents", "OwnerUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CalendarEvents", "OwnerUserId", "dbo.AspNetUsers");
            DropIndex("dbo.CalendarEvents", new[] { "OwnerUserId" });
            AlterColumn("dbo.CalendarEvents", "OwnerUserId", c => c.String());
        }
    }
}
