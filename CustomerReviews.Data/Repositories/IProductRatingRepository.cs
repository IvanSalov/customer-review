using CustomerReviews.Data.Model;
using System.Linq;
using VirtoCommerce.Platform.Core.Common;

namespace CustomerReviews.Data.Repositories
{
    public interface IProductRatingRepository : IRepository
    {
        IQueryable<ProductRatingEntity> ProductRatings { get; }

        ProductRatingEntity GetProductRatingByProductId(string productId);
    }
}
