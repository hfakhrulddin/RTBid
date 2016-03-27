angular.module('app').factory('SellingResource', function (apiUrl, $resource) {

    return $resource(apiUrl + 'products/:productId', { productId: '@ProductId' }, {

        'update': {
            method: 'PUT'
        },

        'query': {
            method: 'GET',
            isArray: true
        }
    });
});