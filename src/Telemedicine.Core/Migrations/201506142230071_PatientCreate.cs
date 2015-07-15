namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PatientCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.SiteUserId)
                .Index(t => t.SiteUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patients", "SiteUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Patients", new[] { "SiteUserId" });
            DropTable("dbo.Patients");
        }
    }
}
