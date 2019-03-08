using CustomerReviews.Core.Model;
using CustomerReviews.Core.Services;
using System.Web.Http;
using System.Web.Http.Description;

namespace CustomerReviews.Web.Controllers.Api
{
    public class ProductRatingController : ApiController
    {
        private readonly IProductRatingService _productRatingService;

        public ProductRatingController()
        {
        }

        public ProductRatingController(IProductRatingService productRatingService)
        {
            _productRatingService = productRatingService;
        }

        /// <summary>
        /// Returns product rating derived from customers' reviews.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(ProductRating))]
        [Route("api/product/{productId}/rating")]
        public IHttpActionResult GetProductRating(string productId)
        {
            var rating = _productRatingService.GetProductRatingByProductId(productId);
            return Ok(rating);
        }
    }
}
