namespace CustomerReviews.Web.Security
{
    public static class PredefinedPermissions
    {
        public const string CustomerReviewRead = "customerReview:read",
                    CustomerReviewUpdate = "customerReview:update",
                    CustomerReviewDelete = "customerReview:delete",
                    CustomerRevewAssessmentCreate = "customerReviewAssessment:create",
                    CustomerRevewAssessmentDelete = "customerReviewAssessment:delete";
    }
}
