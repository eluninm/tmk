namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpecializationMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteUserId = c.String(),
                        DoctorStatusId = c.Int(nullable: false),
                        SpecializationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specializations", t => t.SpecializationId, cascadeDelete: true)
                .ForeignKey("dbo.DoctorStatus", t => t.DoctorStatusId, cascadeDelete: true)
                .Index(t => t.DoctorStatusId)
                .Index(t => t.SpecializationId);
            
            CreateTable(
                "dbo.Specializations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(nullable: false),
                        Code = c.Int(nullable: false),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specializations", t => t.ParentId)
                .Index(t => t.ParentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Doctors", "DoctorStatusId", "dbo.DoctorStatus");
            DropForeignKey("dbo.Doctors", "SpecializationId", "dbo.Specializations");
            DropForeignKey("dbo.Specializations", "ParentId", "dbo.Specializations");
            DropIndex("dbo.Specializations", new[] { "ParentId" });
            DropIndex("dbo.Doctors", new[] { "SpecializationId" });
            DropIndex("dbo.Doctors", new[] { "DoctorStatusId" });
            DropTable("dbo.Specializations");
            DropTable("dbo.Doctors");
        }
    }
}
