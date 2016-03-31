angular.module('app').controller('CategoryController', function ($scope, AuctionsByCategoryResource, $stateParams) {

    function activate() {
        AuctionsByCategoryResource.getProductsInCategory($stateParams.id).then(function (response) {
            $scope.auctions = response;
        });
    }

    activate();

});