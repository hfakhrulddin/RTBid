angular.module('app').controller('SellingDetailController', function ($scope, $stateParams, SellingResource) {

    //get the id from the URL
    $scope.producty = SellingResource.get({ productId: $stateParams.id });

    //Save the new data
    $scope.saveProduct = function () {
        $scope.product.$update(function () {
            alert('save successful');

        });
    };
});