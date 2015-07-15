namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldsForTariff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tariffs", "Messages", c => c.Int(nullable: false));
            AddColumn("dbo.Tariffs", "Minutes", c => c.Int(nullable: false));
            DropColumn("dbo.Tariffs", "PaymentType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tariffs", "PaymentType", c => c.Int(nullable: false));
            DropColumn("dbo.Tariffs", "Minutes");
            DropColumn("dbo.Tariffs", "Messages");
        }
    }
}
