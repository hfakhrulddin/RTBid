angular.module('app').factory('CategoryResource', function (apiUrl, $resource) {

    return $resource(apiUrl + '/categories/:categoryId', { categoryId: '@categoryId' }, {

        'update': {
            method: 'PUT'
        }
    });
});