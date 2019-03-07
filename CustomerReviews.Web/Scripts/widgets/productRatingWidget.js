angular.module('CustomerReviews.Web')
    .controller('CustomerReviews.Web.productRatingWidgetController', ['$scope', 'CustomerReviews.WebApi', 'platformWebApp.bladeNavigationService', function ($scope, reviewsApi, bladeNavigationService) {
        // var blade = $scope.blade;
        var parameters = { };

        function refresh() {
            $scope.loading = true;
            reviewsApi.productRating(parameters, function (data) {
                $scope.loading = false;
                $scope.rating = data.rating;
            });
        }

        $scope.$watch("blade.itemId", function (id) {
            parameters.productId = [id];

            if (id) refresh();
        });
    }]);