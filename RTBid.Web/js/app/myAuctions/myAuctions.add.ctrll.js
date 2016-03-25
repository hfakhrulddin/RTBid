angular.module('app').controller('MyAuctionsAddController', function ($scope, $state, $stateParams, AuctionResource, SellingResource) {

    function activate() {
        $scope.auction = new AuctionResource();
        $scope.products = SellingResource.query();
    }

    activate();

    $scope.saveAuction = function () {
        $scope.auction.$save(function () {
            toastr.success('Success', 'Your auction has been created, make sure you start it!');
            $state.go('app.myAuctions.grid');
        });
    };

});