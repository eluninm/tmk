namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConsultationDoctorPatientFields : DbMigration
    {
        public override void Up()
        {
            Sql("delete from dbo.Conversations");

            RenameColumn(table: "dbo.Conversations", name: "Creator_Id", newName: "CreatorId");
            RenameIndex(table: "dbo.Conversations", name: "IX_Creator_Id", newName: "IX_CreatorId");
            AddColumn("dbo.Conversations", "DoctorId", c => c.Int(nullable: false));
            AddColumn("dbo.Conversations", "PatientId", c => c.Int(nullable: false));
            CreateIndex("dbo.Conversations", "DoctorId");
            CreateIndex("dbo.Conversations", "PatientId");
            AddForeignKey("dbo.Conversations", "DoctorId", "dbo.Doctors", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Conversations", "PatientId", "dbo.Patients", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Conversations", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Conversations", "DoctorId", "dbo.Doctors");
            DropIndex("dbo.Conversations", new[] { "PatientId" });
            DropIndex("dbo.Conversations", new[] { "DoctorId" });
            DropColumn("dbo.Conversations", "PatientId");
            DropColumn("dbo.Conversations", "DoctorId");
            RenameIndex(table: "dbo.Conversations", name: "IX_CreatorId", newName: "IX_Creator_Id");
            RenameColumn(table: "dbo.Conversations", name: "CreatorId", newName: "Creator_Id");
        }
    }
}
