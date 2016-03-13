angular.module('app').controller('HomeController', function ($scope, AuthenticationService) {
    $scope.loginData = {};

    $scope.logout = function () {
        AuthenticationService.logout();
    }

    $scope.login = function () {
        AuthenticationService.logout();
        AuthenticationService.login($scope.loginData).then(
            function (response) {
                location.replace('/#/app/dashboard');
            },
            function (err) {
                alert(err.error_description);
            }
        );
    };


});