angular.module('CustomerReviews.Web')
    .controller('CustomerReviews.Web.reviewDetailsController', ['$scope', 'CustomerReviews.WebApi', 'platformWebApp.bladeUtils', 'uiGridConstants', 'platformWebApp.uiGridHelper',
        function ($scope, reviewsApi, bladeUtils, uiGridConstants, uiGridHelper) {
            $scope.uiGridConstants = uiGridConstants;
            var bladeNavigationService = bladeUtils.bladeNavigationService;

            var blade = $scope.blade;

            blade.refresh = function () {
                blade.isLoading = true;
                reviewsApi.details({ customerReviewId: blade.currentEntityId },
                    function (data) {
                        blade.isLoading = false;
                        blade.data = data;
                        blade.loadProduct(data.productId);
                    });
            }

            blade.loadProduct = function (productId) {
                blade.isLoading = true;
                reviewsApi.getProduct({ id: productId },
                    function (data) {
                        blade.isLoading = false;
                        blade.item = data;
                    });
            }

            blade.headIcon = 'fa-comments';

            blade.toolbarCommands = [
                {
                    name: "platform.commands.refresh", icon: 'fa fa-refresh',
                    executeMethod: blade.refresh,
                    canExecuteMethod: function () {
                        return true;
                    }
                }
            ];

            blade.openProduct = function () {
                if ($scope.loading)
                    return;

                var newBlade = {
                    id: "listItemDetail",
                    itemId: blade.item.id,
                    productType: blade.item.productType,
                    title: blade.item.name,
                    controller: 'virtoCommerce.catalogModule.itemDetailController',
                    template: 'Modules/$(VirtoCommerce.Catalog)/Scripts/blades/item-detail.tpl.html'
                };
                bladeNavigationService.showBlade(newBlade, blade);
            }


            $scope.setGridOptions = function (gridOptions) {
                uiGridHelper.initialize($scope, gridOptions, function (gridApi) {
                    uiGridHelper.bindRefreshOnSortChanged($scope);
                });
                bladeUtils.initializePagination($scope);
            };

            blade.refresh();

        }]);
