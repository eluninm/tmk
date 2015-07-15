namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PatientSelectedDoctorIdField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "SelectedDoctorId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patients", "SelectedDoctorId");
        }
    }
}
