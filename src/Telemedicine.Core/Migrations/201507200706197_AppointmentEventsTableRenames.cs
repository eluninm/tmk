namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppointmentEventsTableRenames : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AppointmentsEvent", newName: "AppointmentEvents");
            DropForeignKey("dbo.AppointmentsEvent", "Doctor_Id", "dbo.Doctors");
            DropForeignKey("dbo.AppointmentsEvent", "Patient_Id", "dbo.Patients");
            DropIndex("dbo.AppointmentEvents", new[] { "Doctor_Id" });
            DropIndex("dbo.AppointmentEvents", new[] { "Patient_Id" });
            RenameColumn(table: "dbo.AppointmentEvents", name: "Doctor_Id", newName: "DoctorId");
            RenameColumn(table: "dbo.AppointmentEvents", name: "Patient_Id", newName: "PatientId");
            AddColumn("dbo.AppointmentEvents", "Created", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.AppointmentEvents", "DoctorId", c => c.Int(nullable: false));
            AlterColumn("dbo.AppointmentEvents", "PatientId", c => c.Int(nullable: false));
            CreateIndex("dbo.AppointmentEvents", "PatientId");
            CreateIndex("dbo.AppointmentEvents", "DoctorId");
            AddForeignKey("dbo.AppointmentEvents", "DoctorId", "dbo.Doctors", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AppointmentEvents", "PatientId", "dbo.Patients", "Id", cascadeDelete: true);
            DropColumn("dbo.AppointmentEvents", "DateCreation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AppointmentEvents", "DateCreation", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            DropForeignKey("dbo.AppointmentEvents", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.AppointmentEvents", "DoctorId", "dbo.Doctors");
            DropIndex("dbo.AppointmentEvents", new[] { "DoctorId" });
            DropIndex("dbo.AppointmentEvents", new[] { "PatientId" });
            AlterColumn("dbo.AppointmentEvents", "PatientId", c => c.Int());
            AlterColumn("dbo.AppointmentEvents", "DoctorId", c => c.Int());
            DropColumn("dbo.AppointmentEvents", "Created");
            RenameColumn(table: "dbo.AppointmentEvents", name: "PatientId", newName: "Patient_Id");
            RenameColumn(table: "dbo.AppointmentEvents", name: "DoctorId", newName: "Doctor_Id");
            CreateIndex("dbo.AppointmentEvents", "Patient_Id");
            CreateIndex("dbo.AppointmentEvents", "Doctor_Id");
            AddForeignKey("dbo.AppointmentsEvent", "Patient_Id", "dbo.Patients", "Id");
            AddForeignKey("dbo.AppointmentsEvent", "Doctor_Id", "dbo.Doctors", "Id");
            RenameTable(name: "dbo.AppointmentEvents", newName: "AppointmentsEvent");
        }
    }
}
