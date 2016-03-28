angular.module('app').controller('CategoryController', function ($scope, CategoryResource) {

    //need to fixed CR!!!!!!
    function activate() {
        $scope.auctions = CategoryResource.query();
    }

    activate();
});