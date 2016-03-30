angular.module('app').factory('CategoryResource', function (apiUrl, $resource) {

    return $resource(apiUrl + 'categories/:categoryId', { categoryId: '@categoryId' }, {

        'update': {
            method: 'PUT'
        }

    });
});

angular.module('app').factory('AuctionsByCategoryResource', function ($http, apiUrl, $resource) {

    function getProductsInCategory(id) {
        return $http.get(apiUrl + 'auctionsByCategory/id=' + id)
        .then(function (response) {
            return response.data;
        });
    }

    return {
        getProductsInCategory: getProductsInCategory
    };

});