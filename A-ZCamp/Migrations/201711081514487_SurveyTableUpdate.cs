namespace A_ZCamp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SurveyTableUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SurveyTypes", "Name", c => c.String());
        }
        
        public override void Down()
        {
        }
    }
}
