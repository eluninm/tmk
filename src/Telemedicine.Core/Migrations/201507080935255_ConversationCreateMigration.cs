namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConversationCreateMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Doctors", "CurrentPatient_Id", "dbo.Patients");
            DropIndex("dbo.Doctors", new[] { "CurrentPatient_Id" });
            RenameColumn(table: "dbo.Doctors", name: "SiteUserId", newName: "UserId");
            RenameColumn(table: "dbo.Patients", name: "SiteUserId", newName: "UserId");
            RenameIndex(table: "dbo.Doctors", name: "IX_SiteUserId", newName: "IX_UserId");
            RenameIndex(table: "dbo.Patients", name: "IX_SiteUserId", newName: "IX_UserId");
            CreateTable(
                "dbo.Conversations",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Created = c.DateTime(nullable: false),
                        Creator_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Creator_Id)
                .Index(t => t.Creator_Id);
            
            CreateTable(
                "dbo.UserConversations",
                c => new
                    {
                        ConversationId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ConversationId, t.UserId })
                .ForeignKey("dbo.Conversations", t => t.ConversationId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ConversationId)
                .Index(t => t.UserId);
            
            Sql("delete from dbo.ChatMessages");

            AddColumn("dbo.ChatMessages", "ConversationId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.ChatMessages", "ConversationId");
            AddForeignKey("dbo.ChatMessages", "ConversationId", "dbo.Conversations", "Id", cascadeDelete: true);
            DropColumn("dbo.Doctors", "CurrentPatient_Id");
            DropColumn("dbo.Patients", "SelectedDoctorId");
            DropColumn("dbo.ChatMessages", "TargetId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ChatMessages", "TargetId", c => c.String());
            AddColumn("dbo.Patients", "SelectedDoctorId", c => c.Int(nullable: false));
            AddColumn("dbo.Doctors", "CurrentPatient_Id", c => c.Int());
            DropForeignKey("dbo.ChatMessages", "ConversationId", "dbo.Conversations");
            DropForeignKey("dbo.UserConversations", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserConversations", "ConversationId", "dbo.Conversations");
            DropForeignKey("dbo.Conversations", "Creator_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserConversations", new[] { "UserId" });
            DropIndex("dbo.UserConversations", new[] { "ConversationId" });
            DropIndex("dbo.Conversations", new[] { "Creator_Id" });
            DropIndex("dbo.ChatMessages", new[] { "ConversationId" });
            DropColumn("dbo.ChatMessages", "ConversationId");
            DropTable("dbo.UserConversations");
            DropTable("dbo.Conversations");
            RenameIndex(table: "dbo.Patients", name: "IX_UserId", newName: "IX_SiteUserId");
            RenameIndex(table: "dbo.Doctors", name: "IX_UserId", newName: "IX_SiteUserId");
            RenameColumn(table: "dbo.Patients", name: "UserId", newName: "SiteUserId");
            RenameColumn(table: "dbo.Doctors", name: "UserId", newName: "SiteUserId");
            CreateIndex("dbo.Doctors", "CurrentPatient_Id");
            AddForeignKey("dbo.Doctors", "CurrentPatient_Id", "dbo.Patients", "Id");
        }
    }
}
