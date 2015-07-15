namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DoctorStatusMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DoctorStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        DisplayName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "DoctorStatusId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DoctorStatusId");
            DropTable("dbo.DoctorStatus");
        }
    }
}
