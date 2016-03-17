angular.module('app').controller('CategoryController', function ($scope, CategoryResource) {

    //need to fixed CR!!!!!!
    function activate() {

        $scope.products = CategoryResource.query();
    }

    activate();


});
