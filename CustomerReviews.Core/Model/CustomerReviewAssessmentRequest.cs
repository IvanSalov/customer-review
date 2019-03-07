using System;

namespace CustomerReviews.Core.Model
{
    public class CustomerReviewAssessmentRequest
    {
        public string AuthorNickname { get; set; }

        // Yes, it would be much better to use enum instead
        public bool IsLike { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
