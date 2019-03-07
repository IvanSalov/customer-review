using CustomerReviews.Core.Model;

namespace CustomerReviews.Core.Services
{
    public interface IProductRatingService
    {
        ProductRating GetProductRatingByProductId(string productId);
    }
}
