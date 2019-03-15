using System;

namespace CustomerReviews.Core.Model
{
    public class CustomerReviewAssessmentResponse
    {
        public string Id { get; set; }

        public string AuthorNickname { get; set; }
        
        public bool IsLike { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }
    }
}
