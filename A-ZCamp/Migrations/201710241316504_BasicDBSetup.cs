namespace A_ZCamp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BasicDBSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SurveyMCResponses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SurveyQuestionId = c.Int(nullable: false),
                        SurveyRespondentId = c.Int(nullable: false),
                        Choice = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SurveyQuestions", t => t.SurveyQuestionId, cascadeDelete: true)
                .ForeignKey("dbo.SurveyRespondents", t => t.SurveyRespondentId, cascadeDelete: true)
                .Index(t => t.SurveyQuestionId)
                .Index(t => t.SurveyRespondentId);
            
            CreateTable(
                "dbo.SurveyQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionType = c.Int(nullable: false),
                        Question = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SurveyRespondents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SurveyTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SurveyTypes", t => t.SurveyTypeId, cascadeDelete: true)
                .Index(t => t.SurveyTypeId);
            
            CreateTable(
                "dbo.SurveyTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Survey = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SurveyQuestionOrderings",
                c => new
                    {
                        SurveyTypeId = c.Int(nullable: false),
                        SurveyQuestionId = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SurveyTypeId, t.SurveyQuestionId })
                .ForeignKey("dbo.SurveyQuestions", t => t.SurveyQuestionId, cascadeDelete: true)
                .ForeignKey("dbo.SurveyTypes", t => t.SurveyTypeId, cascadeDelete: true)
                .Index(t => t.SurveyTypeId)
                .Index(t => t.SurveyQuestionId);
            
            CreateTable(
                "dbo.SurveyQuestionSuppliedAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SurveyQuestionId = c.Int(nullable: false),
                        Answer = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SurveyQuestions", t => t.SurveyQuestionId, cascadeDelete: true)
                .Index(t => t.SurveyQuestionId);
            
            CreateTable(
                "dbo.SurveyRankingResponses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SurveyQuestionId = c.Int(nullable: false),
                        SurveyRespondentId = c.Int(nullable: false),
                        Choice = c.String(),
                        Rank = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SurveyQuestions", t => t.SurveyQuestionId, cascadeDelete: true)
                .ForeignKey("dbo.SurveyRespondents", t => t.SurveyRespondentId, cascadeDelete: true)
                .Index(t => t.SurveyQuestionId)
                .Index(t => t.SurveyRespondentId);
            
            CreateTable(
                "dbo.SurveyShortAnswerResponses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SurveyQuestionId = c.Int(nullable: false),
                        SurveyRespondentId = c.Int(nullable: false),
                        Answer = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SurveyQuestions", t => t.SurveyQuestionId, cascadeDelete: true)
                .ForeignKey("dbo.SurveyRespondents", t => t.SurveyRespondentId, cascadeDelete: true)
                .Index(t => t.SurveyQuestionId)
                .Index(t => t.SurveyRespondentId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SurveyShortAnswerResponses", "SurveyRespondentId", "dbo.SurveyRespondents");
            DropForeignKey("dbo.SurveyShortAnswerResponses", "SurveyQuestionId", "dbo.SurveyQuestions");
            DropForeignKey("dbo.SurveyRankingResponses", "SurveyRespondentId", "dbo.SurveyRespondents");
            DropForeignKey("dbo.SurveyRankingResponses", "SurveyQuestionId", "dbo.SurveyQuestions");
            DropForeignKey("dbo.SurveyQuestionSuppliedAnswers", "SurveyQuestionId", "dbo.SurveyQuestions");
            DropForeignKey("dbo.SurveyQuestionOrderings", "SurveyTypeId", "dbo.SurveyTypes");
            DropForeignKey("dbo.SurveyQuestionOrderings", "SurveyQuestionId", "dbo.SurveyQuestions");
            DropForeignKey("dbo.SurveyMCResponses", "SurveyRespondentId", "dbo.SurveyRespondents");
            DropForeignKey("dbo.SurveyRespondents", "SurveyTypeId", "dbo.SurveyTypes");
            DropForeignKey("dbo.SurveyMCResponses", "SurveyQuestionId", "dbo.SurveyQuestions");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.SurveyShortAnswerResponses", new[] { "SurveyRespondentId" });
            DropIndex("dbo.SurveyShortAnswerResponses", new[] { "SurveyQuestionId" });
            DropIndex("dbo.SurveyRankingResponses", new[] { "SurveyRespondentId" });
            DropIndex("dbo.SurveyRankingResponses", new[] { "SurveyQuestionId" });
            DropIndex("dbo.SurveyQuestionSuppliedAnswers", new[] { "SurveyQuestionId" });
            DropIndex("dbo.SurveyQuestionOrderings", new[] { "SurveyQuestionId" });
            DropIndex("dbo.SurveyQuestionOrderings", new[] { "SurveyTypeId" });
            DropIndex("dbo.SurveyRespondents", new[] { "SurveyTypeId" });
            DropIndex("dbo.SurveyMCResponses", new[] { "SurveyRespondentId" });
            DropIndex("dbo.SurveyMCResponses", new[] { "SurveyQuestionId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.SurveyShortAnswerResponses");
            DropTable("dbo.SurveyRankingResponses");
            DropTable("dbo.SurveyQuestionSuppliedAnswers");
            DropTable("dbo.SurveyQuestionOrderings");
            DropTable("dbo.SurveyTypes");
            DropTable("dbo.SurveyRespondents");
            DropTable("dbo.SurveyQuestions");
            DropTable("dbo.SurveyMCResponses");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
