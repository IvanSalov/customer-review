using System;
using System.Collections.Generic;
using System.Linq;
using CustomerReviews.Data.Model;

namespace CustomerReviews.Data.Utils
{
    public static class RateCalculationUtil
    {
        public static double CalculateProductRating(ICollection<CustomerReviewEntity> remainingReviews)
        {
            var valuesWithWeights = remainingReviews
                .Select(r => new Tuple<int, double>(r.Value, Math.Sqrt(r.LikesNumber + 1) / Math.Sqrt(r.DislikesNumber + 1)));

            var rate = valuesWithWeights.Sum(vw => vw.Item1 * vw.Item2) / valuesWithWeights.Sum(vw => vw.Item2);

            return rate;
        }
    }
}
