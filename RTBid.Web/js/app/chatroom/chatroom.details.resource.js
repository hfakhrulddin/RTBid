angular.module('app').factory('ChatroomResource', function (apiUrl, $resource) {

    return $resource(apiUrl + '/leases/:leaseId', { leaseId: '@LeaseId' }, {

        'update': {
            method: 'PUT'
        }
    });
});