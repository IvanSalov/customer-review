using CustomerReviews.Core.Model;
using System.ComponentModel.DataAnnotations;
using VirtoCommerce.Platform.Core.Common;

namespace CustomerReviews.Data.Model
{
    public class ProductRatingEntity : Entity
    {
        [Required]
        public string ProductId { get; set; }

        [Required]
        public double Rating { get; set; }

        public ProductRating ToModel(ProductRating model)
        {
            model.ProductId = ProductId;
            model.Rating = Rating;

            return model;
        }
    }
}
