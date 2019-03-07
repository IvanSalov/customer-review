using CustomerReviews.Core.Model;
using VirtoCommerce.Domain.Commerce.Model.Search;

namespace CustomerReviews.Core.Services
{
    public interface ICustomerReviewSearchService
    {
        GenericSearchResult<CustomerReviewBrief> SearchCustomerReviews(CustomerReviewSearchCriteria criteria);
    }
}
