angular.module('app').controller('SellingDetailController', function ($scope, $stateParams, $state, SellingResource) {

    //get the id from the URL
    $scope.producty = SellingResource.get({ productId: $stateParams.id });

    //Save the new data
    $scope.saveProduct = function () {
        $scope.product.$update(function () {
            toastr.success('Success', 'Your product has been updateded');
            $state.go('app.selling.grid');

        });
    };

});