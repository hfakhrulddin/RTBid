angular.module('app').controller('MyAuctionsGridController', function ($scope, AuctionResource) {

    $scope.deleteAuction = function (auction) {
        auction.$remove(function (data) {
            activate();
        })
    };

    function activate() {
        $scope.auctions = AuctionResource.query();
    }

    activate();

});