angular.module('app').controller('SellingGridController', function ($scope, SellingResource) {

    /////////////////////////////
    activate();
    ///////////////////////////////Delete selected property form the list.
    $scope.deleteProduct = function (product) {
        product.$remove(function (data) {
            activate();
        })
    };
    ////////////////////////////
    function activate() {
        $scope.products = SellingResource.query();
    }

});