angular.module('app').controller('MyAuctionsDetailController', function ($scope, $stateParams, $state, AuctionResource) {

    //get the id from the URL
    $scope.auction = AuctionResource.get({ auctionId: $stateParams.id });

    //Save the new data
    $scope.saveAuction = function () {
        $scope.auction.$update(function () {
            toastr.success('Success', 'Your auction has been updateded');
            $state.go('app.myAuctions.grid');

        });
    };
});