namespace CustomerReviews.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerReviewAssessmentAndProductRating : DbMigration
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
                .ForeignKey("dbo.CustomerReview", t => t.CustomerReviewId, cascadeDelete: true)
                .Index(t => t.CustomerReviewId);

            CreateTable(
                "dbo.ProductRating",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    ProductId = c.String(nullable: false, maxLength: 128),
                    Rating = c.Double(nullable: false)
                }
                )
                .PrimaryKey(t => t.Id)
                .Index(t => t.ProductId);

            AddColumn("dbo.CustomerReview", "Value", c => c.Short(nullable: false));
            AddColumn("dbo.CustomerReview", "LikesNumber", c => c.Int(nullable: false));
            AddColumn("dbo.CustomerReview", "DislikesNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerReviewAssessment", "CustomerReviewId", "dbo.CustomerReview");
            DropIndex("dbo.CustomerReviewAssessment", new[] { "CustomerReviewId" });
            DropColumn("dbo.CustomerReview", "DislikesNumber");
            DropColumn("dbo.CustomerReview", "LikesNumber");
            DropColumn("dbo.CustomerReview", "Value");
            DropTable("dbo.CustomerReviewAssessment");
            DropTable("dbo.ProductRating");
        }
    }
}
