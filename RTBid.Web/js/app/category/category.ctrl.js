angular.module('app').controller('CategoryController', function ($scope, AuctionsByCategoryResource, $stateParams) {

    //need to fixed CR!!!!!!
    function activate() {
        AuctionsByCategoryResource.getProductsInCategory($stateParams.id).then(function (response) {
            $scope.auctions = response;
        });
    }

    activate();

    //////////////////////
    //function activate() {
    //    $scope.categories = CategoriesResource.query();
    //}
    //activate();

    //function AuctionByCategory() {
    //    AuctionsByCategoryResource.getProductsInCategory($stateParams.categoryId).then(function (response) {
    //        $scope.CategoryAuctions = response;
    //    });
    //}


});