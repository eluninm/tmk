namespace Telemedicine.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpecializationCollation : DbMigration
    {
        public override void Up()
        {
            Sql("delete from dbo.Specializations");
        }

        public override void Down()
        {

        }
    }
}
