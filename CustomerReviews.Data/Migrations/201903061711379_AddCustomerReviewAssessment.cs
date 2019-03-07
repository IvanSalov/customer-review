namespace CustomerReviews.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerReviewAssessment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerReviewAssessment",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        AuthorNickname = c.String(),
                        CustomerReviewId = c.String(maxLength: 128),
                        IsLike = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 64),
                        ModifiedBy = c.String(maxLength: 64),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerReview", t => t.CustomerReviewId)
                .Index(t => t.CustomerReviewId);
            
            AddColumn("dbo.CustomerReview", "Value", c => c.Short(nullable: false));
            AddColumn("dbo.CustomerReview", "LikesNumber", c => c.Int(nullable: false));
            AddColumn("dbo.CustomerReview", "DislikesNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerReviewAssessmentEntity", "CustomerReviewId", "dbo.CustomerReview");
            DropIndex("dbo.CustomerReviewAssessmentEntity", new[] { "CustomerReviewId" });
            DropColumn("dbo.CustomerReview", "DislikesNumber");
            DropColumn("dbo.CustomerReview", "LikesNumber");
            DropColumn("dbo.CustomerReview", "Value");
            DropTable("dbo.CustomerReviewAssessmentEntity");
        }
    }
}
