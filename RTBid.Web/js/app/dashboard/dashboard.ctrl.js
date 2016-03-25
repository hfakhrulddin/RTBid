angular.module('app').controller('DashboardController', function ($scope, AuthenticationService, AuctionResource) {

    $scope.logout = function () {
        AuthenticationService.logout().then(function () { location.replace('/#/app/home') });
    };

    function activate() {
        $scope.auctions = AuctionResource.query();
    }

    activate();
});