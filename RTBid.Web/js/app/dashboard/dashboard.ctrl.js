angular.module('app').controller('DashboardController', function ($scope, AuthenticationService, AuctionResource, BidResource) {

    $scope.logout = function () {
        AuthenticationService.logout().then(function () { location.replace('/#/app/home') });
    };

    function activate() {
        $scope.auctions = AuctionResource.query();
        $scope.bids = BidResource.query();
    }

    activate();
});