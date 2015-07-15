namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TariffCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tariffs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Cost = c.Double(nullable: false),
                        PaymentType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tariffs");
        }
    }
}
