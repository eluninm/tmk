namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnableAppointmentStatusInAppointmentEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppointmentEvents", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppointmentEvents", "Status");
        }
    }
}
