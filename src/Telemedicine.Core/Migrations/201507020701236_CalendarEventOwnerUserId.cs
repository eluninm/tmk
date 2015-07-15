namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CalendarEventOwnerUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CalendarEvents", "OwnerUserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CalendarEvents", "OwnerUserId");
        }
    }
}
