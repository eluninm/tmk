namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DoctorUserFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "AvatarUrl", c => c.String());
            AddColumn("dbo.Doctors", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctors", "Description");
            DropColumn("dbo.AspNetUsers", "AvatarUrl");
        }
    }
}
