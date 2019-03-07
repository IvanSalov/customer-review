namespace CustomerReviews.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductRatingEntity : DbMigration
    {
        public override void Up()
        {
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
        }
        
        public override void Down()
        {
            DropTable("dbo.ProductRating");
        }
    }
}
