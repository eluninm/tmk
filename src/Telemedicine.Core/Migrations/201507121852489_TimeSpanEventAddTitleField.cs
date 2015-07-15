namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimeSpanEventAddTitleField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeSpanEvents", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TimeSpanEvents", "Title");
        }
    }
}
