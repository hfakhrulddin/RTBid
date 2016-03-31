angular.module('app').factory('PaymentResource', function (apiUrl, $resource) {

    return $resource(apiUrl + 'auctions/:auctionId', { auctionId: '@AuctionId' }, {

        'update': {
            method: 'PUT'
        },

        'query': {
            method: 'GET',
            isArray: true
        }
    });
});