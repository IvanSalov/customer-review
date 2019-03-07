using CustomerReviews.Core.Model;
using CustomerReviews.Core.Services;
using CustomerReviews.Data.Repositories;
using System;
using System.Data.Entity.Core;
using VirtoCommerce.Platform.Core.Common;

namespace CustomerReviews.Data.Services
{
    public class ProductRatingService : IProductRatingService
    {
        public ProductRatingService()
        {
        }

        public ProductRatingService(Func<IProductRatingRepository> productRatingrepositoryFactory)
        {
            _productRatingRepositoryFactory = productRatingrepositoryFactory;
        }

        private readonly Func<IProductRatingRepository> _productRatingRepositoryFactory;

        public ProductRating GetProductRatingByProductId(string productId)
        {
            using (var repository = _productRatingRepositoryFactory())
            {
                var rating = repository.GetProductRatingByProductId(productId);

                // Actually, ProductRating should be create immediatelly after Product creation, but as long as it is a study project, I won't implement that.
                if (rating == null)
                {
                    return new ProductRating
                    {
                        ProductId = productId,
                        Rating = 0
                    };
                }

                return rating.ToModel(AbstractTypeFactory<ProductRating>.TryCreateInstance());

            }
        }
    }
}
