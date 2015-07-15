namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PatientBalanceField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "Balance", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patients", "Balance");
        }
    }
}
