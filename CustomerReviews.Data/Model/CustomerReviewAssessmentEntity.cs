using CustomerReviews.Core.Model;
using System;
using VirtoCommerce.Platform.Core.Common;

namespace CustomerReviews.Data.Model
{
    public class CustomerReviewAssessmentEntity : AuditableEntity
    {
        public string AuthorNickname { get; set; }
        public string CustomerReviewId { get; set; }
        public bool IsLike { get; set; }

        public virtual CustomerReviewEntity CustomerReview { get; set; }

        public virtual CustomerReviewAssessmentEntity FromModel(CustomerReviewAssessmentRequest customerReviewAssessment)
        {
            if (customerReviewAssessment == null)
            {
                throw new ArgumentException(nameof(customerReviewAssessment));
            }

            AuthorNickname = customerReviewAssessment.AuthorNickname;
            IsLike = customerReviewAssessment.IsLike;
            
            CreatedDate = customerReviewAssessment.CreatedDate;

            return this;
        }

        public virtual CustomerReviewAssessmentResponse ToModel(CustomerReviewAssessmentResponse model)
        {
            if (model == null)
            {
                throw new ArgumentException(nameof(model));
            }

            model.Id = Id;
            model.CreatedDate = CreatedDate;
            model.IsLike = IsLike;
            model.AuthorNickname = AuthorNickname;

            return model;
        }
    }
}
