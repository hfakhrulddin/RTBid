angular.module('app').controller('PaymentController', function ($scope, ProductResource, ProfileResource, PaymentResource, $stateParams, AuctionResource) {

    function activate() {

        $scope.auction = AuctionResource.get({ auctionId: $stateParams.id });
        $scope.product = ProductResource.get({ productId: $stateParams.productId });

        ProfileResource.getCurrentUser().then(function (response) {
            $scope.user = response;
        })
        }

    activate();
});