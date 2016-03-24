angular.module('app').controller('MyAuctionsDetailController', function ($scope, $stateParams, MyAuctionsResource) {

    //get the id from the URL
    $scope.auction = MyAuctionsResource.get({ auctionId: $stateParams.id });

    //Save the new data
    $scope.saveAuction = function () {
        $scope.auction.$update(function () {
            alert('save successful');

        });
    };
});