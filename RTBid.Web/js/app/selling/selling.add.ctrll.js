angular.module('app').controller('SellingAddController', function ($scope, $state, $stateParams, SellingResource, CategoryResource) {

    function activate() {
        $scope.product = new SellingResource();
        $scope.categories = CategoryResource.query();
    }
    activate();

    $scope.saveProduct = function () {
        $scope.product.$save(function () {
            toastr.success('Success', 'Your product has been registered');
            $state.go('app.selling.grid');
        });
    };
});