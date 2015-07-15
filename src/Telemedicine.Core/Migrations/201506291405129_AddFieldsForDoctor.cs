namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldsForDoctor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "PaymentMethod_Id", c => c.Int());
            CreateIndex("dbo.Doctors", "PaymentMethod_Id");
            AddForeignKey("dbo.Doctors", "PaymentMethod_Id", "dbo.PaymentMethods", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Doctors", "PaymentMethod_Id", "dbo.PaymentMethods");
            DropIndex("dbo.Doctors", new[] { "PaymentMethod_Id" });
            DropColumn("dbo.Doctors", "PaymentMethod_Id");
        }
    }
}
