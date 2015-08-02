namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDoctorTimetable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DoctorTimetable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        DoctorId = c.Int(nullable: false),
                        HourType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors", t => t.DoctorId, cascadeDelete: true)
                .Index(t => t.DoctorId)
                .Index(t=>t.DateTime);
            
            AddColumn("dbo.AppointmentEvents", "DoctorTimetableId", c => c.Int(nullable: false));
            CreateIndex("dbo.AppointmentEvents", "DoctorTimetableId");
            AddForeignKey("dbo.AppointmentEvents", "DoctorTimetableId", "dbo.DoctorTimetable", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DoctorTimetable", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.AppointmentEvents", "DoctorTimetableId", "dbo.DoctorTimetable");
            DropIndex("dbo.DoctorTimetable", new[] { "DoctorId" });
            DropIndex("dbo.AppointmentEvents", new[] { "DoctorTimetableId" });
            DropColumn("dbo.AppointmentEvents", "DoctorTimetableId");
            DropTable("dbo.DoctorTimetable");
        }
    }
}
