var artistControllers = angular.module('artistControllers', ['ngAnimate']);

artistControllers.controller('ListController', ['$scope', '$http', function($scope, $http) {
  $http.get('data/data.json').success(function(data) {
    $scope.artists = data;
    $scope.artistOrder = 'name';
  });
}]);



artistControllers.controller('DetailsController', ['$scope', '$http', '$routeParams', '$stateParams', function ($scope, $http, $routeParams, $stateParams, ChatroomResource) {
    $http.get('data/data.json').success(function (data) {
        $scope.artists = data;
        $scope.whichItem = $stateParams.itemId;

        if ($stateParams.itemId > 0) {
            $scope.prevItem = Number($stateParams.itemId) - 1;
        } else {
            $scope.prevItem = $scope.artists.length - 1;
        }

        if ($stateParams.itemId < $scope.artists.length - 1) {
            $scope.nextItem = Number($stateParams.itemId) + 1;
        } else {
            $scope.nextItem = 0;
        }
  });
}]);
