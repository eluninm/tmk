namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTargetIdColumnToMessage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChatMessages", "TargetId", c => c.String());
            Sql("delete from dbo.ChatMessages");
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChatMessages", "TargetId");
        }
    }
}
