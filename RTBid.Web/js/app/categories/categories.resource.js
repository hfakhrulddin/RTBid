angular.module('app').factory('CategoriesResource', function (apiUrl, $resource) {

    return $resource(apiUrl + 'categories/:categoryId', { categoryId: '@CategoriesId' }, {

        'update': {
            method: 'PUT'
        },

        'get': {
            method: 'GET'
        }

    });
});