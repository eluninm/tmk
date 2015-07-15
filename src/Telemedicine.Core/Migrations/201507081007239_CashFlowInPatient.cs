namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CashFlowInPatient : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentsHistory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PaymentType = c.Int(nullable: false),
                        Value = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                        ConversationId = c.String(),
                        Patient_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.Patient_Id)
                .Index(t => t.Patient_Id);
            
            AddColumn("dbo.Payments", "AutoPayment", c => c.Boolean(nullable: false));
            DropColumn("dbo.Payments", "PaymentType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payments", "PaymentType", c => c.Int(nullable: false));
            DropForeignKey("dbo.PaymentsHistory", "Patient_Id", "dbo.Patients");
            DropIndex("dbo.PaymentsHistory", new[] { "Patient_Id" });
            DropColumn("dbo.Payments", "AutoPayment");
            DropTable("dbo.PaymentsHistory");
        }
    }
}
