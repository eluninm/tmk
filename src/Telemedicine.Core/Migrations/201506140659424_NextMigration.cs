namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NextMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "MiddleName", c => c.String());
            AlterColumn("dbo.Doctors", "SiteUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Doctors", "SiteUserId");
            AddForeignKey("dbo.Doctors", "SiteUserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.AspNetUsers", "DoctorStatusId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "DoctorStatusId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Doctors", "SiteUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Doctors", new[] { "SiteUserId" });
            AlterColumn("dbo.Doctors", "SiteUserId", c => c.String());
            DropColumn("dbo.AspNetUsers", "MiddleName");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
