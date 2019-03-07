using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CustomerReviews.Core.Model;
using VirtoCommerce.Platform.Core.Common;

namespace CustomerReviews.Data.Model
{
    public class CustomerReviewEntity : AuditableEntity
    {
        [StringLength(128)]
        public string AuthorNickname { get; set; }

        [Required]
        [StringLength(1024)]
        public string Content { get; set; }

        public bool IsActive { get; set; }

        [Required]
        [StringLength(128)]
        public string ProductId { get; set; }

        [Required]
        [Range(1, 5)]
        public short Value { get; set; }

        [Required]
        public int LikesNumber { get; set; }

        [Required]
        public int DislikesNumber { get; set; }

        public virtual ICollection<CustomerReviewAssessmentEntity> CustomerReviewAssessments { get; set; }

        public virtual CustomerReviewBrief ToBriefModel(CustomerReviewBrief customerReview)
        {
            if (customerReview == null)
                throw new ArgumentNullException(nameof(customerReview));

            customerReview.Id = Id;
            customerReview.CreatedBy = CreatedBy;
            customerReview.CreatedDate = CreatedDate;
            customerReview.ModifiedBy = ModifiedBy;
            customerReview.ModifiedDate = ModifiedDate;

            customerReview.AuthorNickname = AuthorNickname;
            customerReview.Content = Content;
            customerReview.IsActive = IsActive;
            customerReview.ProductId = ProductId;
            customerReview.Value = Value;
            customerReview.LikesNumber = LikesNumber;
            customerReview.DislikesNumber = DislikesNumber;

            return customerReview;
        }

        public virtual CustomerReviewDetailed ToDetailedModel(CustomerReviewDetailed customerReview)
        {
            if (customerReview == null)
                throw new ArgumentNullException(nameof(customerReview));

            customerReview.Id = Id;
            customerReview.CreatedBy = CreatedBy;
            customerReview.CreatedDate = CreatedDate;
            customerReview.ModifiedBy = ModifiedBy;
            customerReview.ModifiedDate = ModifiedDate;

            customerReview.AuthorNickname = AuthorNickname;
            customerReview.Content = Content;
            customerReview.IsActive = IsActive;
            customerReview.ProductId = ProductId;
            customerReview.Value = Value;
            customerReview.LikesNumber = LikesNumber;
            customerReview.DislikesNumber = DislikesNumber;
            customerReview.CustomerReviewAssessments = CustomerReviewAssessments
                .Select(a => a.ToModel(AbstractTypeFactory<CustomerReviewAssessmentResponse>.TryCreateInstance()))
                .ToArray();

            return customerReview;
        }

        public virtual CustomerReviewEntity FromRequestModel(CustomerReviewRequest customerReview, PrimaryKeyResolvingMap pkMap)
        {
            if (customerReview == null)
                throw new ArgumentNullException(nameof(customerReview));

            pkMap.AddPair(customerReview, this);

            Id = customerReview.Id;
            CreatedBy = customerReview.CreatedBy;
            CreatedDate = customerReview.CreatedDate;
            ModifiedBy = customerReview.ModifiedBy;
            ModifiedDate = customerReview.ModifiedDate;

            AuthorNickname = customerReview.AuthorNickname;
            Content = customerReview.Content;
            IsActive = customerReview.IsActive;
            ProductId = customerReview.ProductId;
            Value = customerReview.Value;

            return this;
        }

        public virtual void Patch(CustomerReviewEntity target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            target.AuthorNickname = AuthorNickname;
            target.Content = Content;
            target.IsActive = IsActive;
            target.ProductId = ProductId;
        }
    }
}
