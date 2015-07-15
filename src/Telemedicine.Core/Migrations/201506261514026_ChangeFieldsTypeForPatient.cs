namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFieldsTypeForPatient : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CalendarEvents", "BeginDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.CalendarEvents", "EndDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CalendarEvents", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CalendarEvents", "BeginDate", c => c.DateTime(nullable: false));
        }
    }
}
