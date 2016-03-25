angular.module('app').controller('SellingGridController', function ($scope, SellingResource) {

    ///////////////////////////////Delete selected item form the list.
    $scope.deleteProduct = function (product) {
        product.$remove(function (data) {
            activate();
        })
    };
    ////////////////////////////
    function activate() {
        $scope.products = SellingResource.query();
    }
    ////////////////////////////
    activate();
});