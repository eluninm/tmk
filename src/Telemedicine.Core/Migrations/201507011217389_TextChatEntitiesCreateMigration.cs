namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TextChatEntitiesCreateMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false),
                        Message = c.String(),
                        CreatorId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatorId, cascadeDelete: true)
                .Index(t => t.CreatorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChatMessages", "CreatorId", "dbo.AspNetUsers");
            DropIndex("dbo.ChatMessages", new[] { "CreatorId" });
            DropTable("dbo.ChatMessages");
        }
    }
}
