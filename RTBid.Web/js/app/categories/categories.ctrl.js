angular.module('app').controller('CategoriesController', function ($scope, CategoriesResource, $stateParams) {

    function activate() {
        $scope.categories = CategoriesResource.query();
    }

    $scope.getIcon = function (category) {
        switch (category.CategoryId) {
            case 1: return 'glyphicon-camera';
            case 4: return 'glyphicon-book';
        }
    }

    activate();

});