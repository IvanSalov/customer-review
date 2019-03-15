using CustomerReviews.Core.Model;

namespace CustomerReviews.Core.Services
{
    public interface ICustomerReviewService
    {
        CustomerReviewDetailed[] GetByIds(string[] ids);

        void SaveCustomerReviews(CustomerReviewRequest[] items);

        void DeleteCustomerReviews(string[] ids);

        CustomerReviewDetailed GetDetailedReviewById(string id);

        void AddCustomerReviewAssessment(string customerReviewId, CustomerReviewAssessmentRequest customerReviewAssessment);

        void DeleteCustomerReviewAssessment(string customerReviewId, string customerReviewassessmentId);
    }
}
