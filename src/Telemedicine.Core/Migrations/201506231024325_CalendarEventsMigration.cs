namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CalendarEventsMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CalendarEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BeginDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Topic = c.String(),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserEvents",
                c => new
                    {
                        EventId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EventId, t.UserId });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserEvents");
            DropTable("dbo.CalendarEvents");
        }
    }
}
