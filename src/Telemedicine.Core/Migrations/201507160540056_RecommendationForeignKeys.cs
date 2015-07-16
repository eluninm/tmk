namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecommendationForeignKeys : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Recommendations", "PatientId");
            CreateIndex("dbo.Recommendations", "DoctorId");
            AddForeignKey("dbo.Recommendations", "DoctorId", "dbo.Doctors", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Recommendations", "PatientId", "dbo.Patients", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recommendations", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Recommendations", "DoctorId", "dbo.Doctors");
            DropIndex("dbo.Recommendations", new[] { "DoctorId" });
            DropIndex("dbo.Recommendations", new[] { "PatientId" });
        }
    }
}
