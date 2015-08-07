namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DoctorPaymentHistoryAddPaymentHistory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DoctorPaymentsHistory", "PatientPayment_Id", c => c.Int());
            CreateIndex("dbo.DoctorPaymentsHistory", "PatientPayment_Id");
            AddForeignKey("dbo.DoctorPaymentsHistory", "PatientPayment_Id", "dbo.PaymentsHistory", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DoctorPaymentsHistory", "PatientPayment_Id", "dbo.PaymentsHistory");
            DropIndex("dbo.DoctorPaymentsHistory", new[] { "PatientPayment_Id" });
            DropColumn("dbo.DoctorPaymentsHistory", "PatientPayment_Id");
        }
    }
}
