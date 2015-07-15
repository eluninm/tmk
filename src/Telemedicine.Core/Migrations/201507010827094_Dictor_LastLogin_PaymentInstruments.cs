namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dictor_LastLogin_PaymentInstruments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "LastLoginDate", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "PaymentInstrument", c => c.String());
            AddColumn("dbo.Doctors", "CurrentPatient_Id", c => c.Int());
            CreateIndex("dbo.Doctors", "CurrentPatient_Id");
            AddForeignKey("dbo.Doctors", "CurrentPatient_Id", "dbo.Patients", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Doctors", "CurrentPatient_Id", "dbo.Patients");
            DropIndex("dbo.Doctors", new[] { "CurrentPatient_Id" });
            DropColumn("dbo.Doctors", "CurrentPatient_Id");
            DropColumn("dbo.AspNetUsers", "PaymentInstrument");
            DropColumn("dbo.AspNetUsers", "LastLoginDate");
        }
    }
}
