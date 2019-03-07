using System;
using System.Collections.Generic;
using System.Linq;
using CustomerReviews.Data.Model;

namespace CustomerReviews.Data.Utils
{
    public static class RateCalculationUtil
    {
        internal static double CalculateProductRating(ICollection<CustomerReviewEntity> remainingReviews)
        {
            var valuesWithWeights = remainingReviews
                .ToDictionary(
                    r => r.Value,
                    r => 
                        Math.Sqrt(r.LikesNumber + 1) /
                        Math.Sqrt(r.DislikesNumber + 1));

            var rate = valuesWithWeights.Sum(vw => vw.Key * vw.Value) / valuesWithWeights.Sum(vw => vw.Value);

            return rate;
        }
    }
}
