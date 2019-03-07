using System;
using System.Data.Entity.Core;
using System.Linq;
using CustomerReviews.Core.Model;
using CustomerReviews.Core.Services;
using CustomerReviews.Data.Model;
using CustomerReviews.Data.Repositories;
using CustomerReviews.Data.Utils;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Data.Infrastructure;

namespace CustomerReviews.Data.Services
{
    public class CustomerReviewService : ServiceBase, ICustomerReviewService
    {
        private readonly Func<ICustomerReviewRepository> _customerReviewRepositoryFactory;
        private readonly Func<IProductRatingRepository> _productRatingRepositoryFactory;

        public CustomerReviewService(Func<ICustomerReviewRepository> customerReviewRepositoryFactory, Func<IProductRatingRepository> productRatingRepositoryFactory)
        {
            _customerReviewRepositoryFactory = customerReviewRepositoryFactory;
            _productRatingRepositoryFactory = productRatingRepositoryFactory;
        }

        public CustomerReviewBrief[] GetByIds(string[] ids)
        {
            using (var repository = _customerReviewRepositoryFactory())
            {
                return repository.GetByIds(ids).Select(x => x.ToBriefModel(AbstractTypeFactory<CustomerReviewBrief>.TryCreateInstance())).ToArray();
            }
        }

        public void SaveCustomerReviews(CustomerReviewRequest[] items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            var pkMap = new PrimaryKeyResolvingMap();
            using (var repository = _customerReviewRepositoryFactory())
            {
                using (var changeTracker = GetChangeTracker(repository))
                {
                    var alreadyExistEntities = repository.GetByIds(items.Where(m => !m.IsTransient()).Select(x => x.Id).ToArray());
                    foreach (var derivativeContract in items)
                    {
                        var sourceEntity = AbstractTypeFactory<CustomerReviewEntity>.TryCreateInstance().FromRequestModel(derivativeContract, pkMap);
                        var targetEntity = alreadyExistEntities.FirstOrDefault(x => x.Id == sourceEntity.Id);
                        if (targetEntity != null)
                        {
                            changeTracker.Attach(targetEntity);
                            sourceEntity.Patch(targetEntity);
                        }
                        else
                        {
                            repository.Add(sourceEntity);
                        }
                    }

                    CommitChanges(repository);
                    pkMap.ResolvePrimaryKeys();
                }
            }
        }

        public void DeleteCustomerReviews(string[] ids)
        {
            using (var repository = _customerReviewRepositoryFactory())
            {
                repository.DeleteCustomerReviews(ids);
                CommitChanges(repository);
            }
        }

        public void AddCustomerReviewAssessment(string customerReviewId, CustomerReviewAssessmentRequest customerReviewAssessment)
        {
            using (var customerReviewRepository = _customerReviewRepositoryFactory())
            using (var changeTracker = GetChangeTracker(customerReviewRepository))
            {
                var review = customerReviewRepository.GetReviewWithAssessmentsById(customerReviewId);
                if (review == null)
                    throw new ObjectNotFoundException($"Customer review with id = {customerReviewId} not found");

                var newAssessment = AbstractTypeFactory<CustomerReviewAssessmentEntity>.TryCreateInstance().FromModel(customerReviewAssessment);
                changeTracker.Attach(review);
                review.CustomerReviewAssessments.Add(newAssessment);
                
                // Let's pretend I've got a validation here.
                if (newAssessment.IsLike)
                {
                    review.LikesNumber++;
                }
                else
                {
                    review.DislikesNumber++;
                }

                CommitChanges(customerReviewRepository);

                // This calculation should be processed in job.
                CalculateRating(customerReviewRepository, review);
            }
        }

        public void DeleteCustomerReviewAssessment(string customerReviewId, string customerReviewassessmentId)
        {
            using (var customerReviewRepository = _customerReviewRepositoryFactory())
            using (var changeTracker = GetChangeTracker(customerReviewRepository))
            {
                var review = customerReviewRepository.GetReviewWithAssessmentsById(customerReviewId);
                if (review == null)
                    throw new ObjectNotFoundException($"Customer review with id = {customerReviewId} not found");

                var targetAssessment = review.CustomerReviewAssessments.FirstOrDefault(a => a.Id == customerReviewassessmentId);

                if (targetAssessment == null)
                    throw new ObjectNotFoundException(
                        $"Customer review assessment with id = {customerReviewassessmentId} does not exist on customer review with id = {customerReviewId}");

                changeTracker.Attach(review);

                changeTracker.RemoveAction(targetAssessment);

                if (targetAssessment.IsLike)
                {
                    review.LikesNumber--;
                }
                else
                {
                    review.DislikesNumber--;
                }

                CommitChanges(customerReviewRepository);

                // This calculation should be processed in job.
                CalculateRating(customerReviewRepository, review);
            }
        }

        public CustomerReviewDetailed GetDetailedReviewById(string id)
        {
            using (var repository = _customerReviewRepositoryFactory())
            {
                return repository
                    .GetReviewWithAssessmentsById(id)
                    .ToDetailedModel(AbstractTypeFactory<CustomerReviewDetailed>.TryCreateInstance());
            }
        }

        private void CalculateRating(ICustomerReviewRepository customerReviewRepository, CustomerReviewEntity review)
        {
            var reviews = customerReviewRepository
                .CustomerReviews
                .Where(r => r.ProductId == review.ProductId && r.Id != review.Id)
                .ToList();

            reviews.Add(review);

            var productRatingValue = RateCalculationUtil.CalculateProductRating(reviews);

            using (var productRatingRepository = _productRatingRepositoryFactory())
            using (var changeTracker = GetChangeTracker(productRatingRepository))
            {
                var productRating = productRatingRepository.GetProductRatingByProductId(review.ProductId);

                if (productRating == null)
                {
                    productRating = AbstractTypeFactory<ProductRatingEntity>.TryCreateInstance();
                    productRating.ProductId = review.ProductId;
                    productRating.Rating = productRatingValue;
                    productRatingRepository.Add(productRating);
                }
                else
                {
                    changeTracker.Attach(productRating);
                    productRating.Rating = productRatingValue;
                }

                CommitChanges(productRatingRepository);
            }
        }
    }
}
