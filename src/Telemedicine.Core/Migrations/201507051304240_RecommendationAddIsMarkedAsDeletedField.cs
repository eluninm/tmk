namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecommendationAddIsMarkedAsDeletedField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recommendations", "IsMarkedAsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Recommendations", "IsMarkedAsDeleted");
        }
    }
}
