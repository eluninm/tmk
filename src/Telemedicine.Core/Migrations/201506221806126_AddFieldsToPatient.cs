namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldsToPatient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.Patients", "Sex", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patients", "Sex");
            DropColumn("dbo.Patients", "Age");
        }
    }
}
