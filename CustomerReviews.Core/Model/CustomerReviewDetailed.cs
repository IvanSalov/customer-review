using System.Collections.Generic;
using VirtoCommerce.Platform.Core.Common;

namespace CustomerReviews.Core.Model
{
    public class CustomerReviewDetailed : AuditableEntity
    {
        public string AuthorNickname { get; set; }
        public string Content { get; set; }
        public bool IsActive { get; set; }
        public string ProductId { get; set; }

        public short Value { get; set; }

        public int LikesNumber { get; set; }

        public int DislikesNumber { get; set; }

        public ICollection<CustomerReviewAssessmentResponse> CustomerReviewAssessments { get; set; }
    }
}
