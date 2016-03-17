angular.module('app').factory('DashboardResource', function (apiUrl, $resource) {

    return $resource(apiUrl + '/products/:productId', { productId: '@productId' }, {

        'update': {
            method: 'PUT'
        }
    });
});