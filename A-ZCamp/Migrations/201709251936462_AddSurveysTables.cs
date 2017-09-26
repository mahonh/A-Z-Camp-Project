namespace A_ZCamp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSurveysTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostSurveys",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        WhatDidYouThink = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PreSurveys",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        WhatDoYouExpect = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PreSurveys");
            DropTable("dbo.PostSurveys");
        }
    }
}
