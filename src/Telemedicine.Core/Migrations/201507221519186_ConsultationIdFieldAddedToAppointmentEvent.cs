namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConsultationIdFieldAddedToAppointmentEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppointmentEvents", "ConsultationId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppointmentEvents", "ConsultationId");
        }
    }
}
