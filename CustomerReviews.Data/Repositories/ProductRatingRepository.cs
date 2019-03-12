using CustomerReviews.Data.Model;
using System.Data.Entity;
using System.Linq;
using VirtoCommerce.Platform.Data.Infrastructure;
using VirtoCommerce.Platform.Data.Infrastructure.Interceptors;

namespace CustomerReviews.Data.Repositories
{
    public class ProductRatingRepository : EFRepositoryBase, IProductRatingRepository
    {
        public ProductRatingRepository()
        {
        }

        public ProductRatingRepository(string nameOrConnectionString, params IInterceptor[] interceptors)
            : base(nameOrConnectionString, null, interceptors)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public IQueryable<ProductRatingEntity> ProductRatings => GetAsQueryable<ProductRatingEntity>();

        public ProductRatingEntity GetProductRatingByProductId(string productId)
        {
            return ProductRatings.FirstOrDefault(p => p.ProductId == productId);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductRatingEntity>().ToTable("ProductRating").HasKey(x => x.Id).Property(x => x.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
