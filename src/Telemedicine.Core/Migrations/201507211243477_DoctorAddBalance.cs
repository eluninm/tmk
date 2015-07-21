namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DoctorAddBalance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "Balance", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctors", "Balance");
        }
    }
}
