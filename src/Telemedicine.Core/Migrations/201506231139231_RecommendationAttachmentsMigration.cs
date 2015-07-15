namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecommendationAttachmentsMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attachments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        FileName = c.String(),
                        Content = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RecommendationAttachments",
                c => new
                    {
                        RecommendationId = c.Int(nullable: false),
                        AttachmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RecommendationId, t.AttachmentId })
                .ForeignKey("dbo.Recommendations", t => t.RecommendationId, cascadeDelete: true)
                .ForeignKey("dbo.Attachments", t => t.AttachmentId, cascadeDelete: true)
                .Index(t => t.RecommendationId)
                .Index(t => t.AttachmentId);
            
            AddColumn("dbo.Recommendations", "DoctorId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecommendationAttachments", "AttachmentId", "dbo.Attachments");
            DropForeignKey("dbo.RecommendationAttachments", "RecommendationId", "dbo.Recommendations");
            DropIndex("dbo.RecommendationAttachments", new[] { "AttachmentId" });
            DropIndex("dbo.RecommendationAttachments", new[] { "RecommendationId" });
            DropColumn("dbo.Recommendations", "DoctorId");
            DropTable("dbo.RecommendationAttachments");
            DropTable("dbo.Attachments");
        }
    }
}
