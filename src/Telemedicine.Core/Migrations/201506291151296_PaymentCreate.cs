namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PaymentType = c.Int(nullable: false),
                        Doctor_Id = c.Int(),
                        Patient_Id = c.Int(),
                        PaymentMethod_Id = c.Int(),
                        Tariff_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors", t => t.Doctor_Id)
                .ForeignKey("dbo.Patients", t => t.Patient_Id)
                .ForeignKey("dbo.PaymentMethods", t => t.PaymentMethod_Id)
                .ForeignKey("dbo.Tariffs", t => t.Tariff_Id)
                .Index(t => t.Doctor_Id)
                .Index(t => t.Patient_Id)
                .Index(t => t.PaymentMethod_Id)
                .Index(t => t.Tariff_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "Tariff_Id", "dbo.Tariffs");
            DropForeignKey("dbo.Payments", "PaymentMethod_Id", "dbo.PaymentMethods");
            DropForeignKey("dbo.Payments", "Patient_Id", "dbo.Patients");
            DropForeignKey("dbo.Payments", "Doctor_Id", "dbo.Doctors");
            DropIndex("dbo.Payments", new[] { "Tariff_Id" });
            DropIndex("dbo.Payments", new[] { "PaymentMethod_Id" });
            DropIndex("dbo.Payments", new[] { "Patient_Id" });
            DropIndex("dbo.Payments", new[] { "Doctor_Id" });
            DropTable("dbo.Payments");
        }
    }
}
