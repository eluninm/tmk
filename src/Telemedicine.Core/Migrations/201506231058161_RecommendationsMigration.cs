namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecommendationsMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Recommendations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(nullable: false),
                        RecommendationText = c.String(),
                        PatientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Recommendations");
        }
    }
}
