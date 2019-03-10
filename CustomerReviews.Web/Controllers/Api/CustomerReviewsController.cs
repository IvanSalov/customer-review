using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CustomerReviews.Core.Model;
using CustomerReviews.Core.Services;
using CustomerReviews.Web.Security;
using VirtoCommerce.Domain.Commerce.Model.Search;
using VirtoCommerce.Platform.Core.Web.Security;

namespace CustomerReviews.Web.Controllers.Api
{
    [RoutePrefix("api/customerReviews")]
    public class CustomerReviewsController : ApiController
    {
        private readonly ICustomerReviewSearchService _customerReviewSearchService;
        private readonly ICustomerReviewService _customerReviewService;

        public CustomerReviewsController()
        {
        }

        public CustomerReviewsController(ICustomerReviewSearchService customerReviewSearchService, ICustomerReviewService customerReviewService)
        {
            _customerReviewSearchService = customerReviewSearchService;
            _customerReviewService = customerReviewService;
        }

        /// <summary>
        /// Return product Customer review search results
        /// </summary>
        [HttpPost]
        [Route("search")]
        [ResponseType(typeof(GenericSearchResult<CustomerReviewBrief>))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewRead)]
        public IHttpActionResult SearchCustomerReviews(CustomerReviewSearchCriteria criteria)
        {
            var result = _customerReviewSearchService.SearchCustomerReviews(criteria);
            return Ok(result);
        }

        /// <summary>
        ///  Create new or update existing customer review
        /// </summary>
        /// <param name="customerReviews">Customer reviews</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(void))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewUpdate)]
        public IHttpActionResult Update(CustomerReviewRequest[] customerReviews)
        {
            _customerReviewService.SaveCustomerReviews(customerReviews);
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Delete Customer Reviews by IDs
        /// </summary>
        /// <param name="ids">IDs</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        [ResponseType(typeof(void))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewDelete)]
        public IHttpActionResult Delete([FromUri] string[] ids)
        {
            _customerReviewService.DeleteCustomerReviews(ids);
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Get Customer Review by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ResponseType(typeof(CustomerReviewDetailed))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewRead)]
        public IHttpActionResult Get([FromUri]string id)
        {
            var result = _customerReviewService.GetDetailedReviewById(id);
            return Ok(result);
        }

        /// <summary>
        /// Assess Customer's Review with like or dislike
        /// </summary>
        /// <param name="reviewId"></param>
        /// <param name="assessment"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{reviewId}/assessment")]
        [ResponseType(typeof(void))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerRevewAssessmentCreate)]
        public IHttpActionResult AddAssessment([FromUri]string reviewId, CustomerReviewAssessmentRequest assessment)
        {
            _customerReviewService.AddCustomerReviewAssessment(reviewId, assessment);
            return StatusCode(HttpStatusCode.NoContent);
        }


        /// <summary>
        /// Remove your assessment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="assessment"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{reviewId}/assessment/{assessmentId}")]
        [ResponseType(typeof(void))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerRevewAssessmentDelete)]
        public IHttpActionResult DeleteAssessment([FromUri]string reviewId, string assessmentId)
        {
            _customerReviewService.DeleteCustomerReviewAssessment(reviewId, assessmentId);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
