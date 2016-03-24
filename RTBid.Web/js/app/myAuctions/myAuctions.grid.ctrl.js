angular.module('app').controller('MyAuctionsGridController', function ($scope, MyAuctionsResource) {

    /////////////////////////////
    activate();
    ///////////////////////////////Delete selected property form the list.
    $scope.deleteAuction = function (auction) {
        auction.$remove(function (data) {
            activate();
        })
    };
    ////////////////////////////
    function activate() {
        $scope.auctions = MyAuctionsResource.query();
    }

});