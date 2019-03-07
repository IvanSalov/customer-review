angular.module('CustomerReviews.Web')
.factory('CustomerReviews.WebApi', ['$resource', function ($resource) {
    return $resource('api/customerReviews', {}, {
        search: { method: 'POST', url: 'api/customerReviews/search' },
        details: { method: 'GET', url: 'api/customerReviews/:customerReviewId' },
        productRating: { method: 'GET', url: 'product/:productId/rating'}
    });
}]);
