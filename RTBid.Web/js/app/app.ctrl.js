angular.module('app').controller('AppController', function ($scope, AuthenticationService, ProfileResource) {


    $scope.logOut = function () {
        AuthenticationService.logout().then(
            function () {
                location.replace('/#/home')
            });
    };


    function activate() {
        ProfileResource.getCurrentUser().then(function (response) {
            $scope.user = response;
        });
    }
        activate();
});