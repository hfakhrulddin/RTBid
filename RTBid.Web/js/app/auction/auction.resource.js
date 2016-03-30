angular.module('app').factory('AuctionResource', function (apiUrl, $resource) {

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

angular.module('app').factory('BidResource', function (apiUrl, $resource) {
    return $resource(apiUrl + 'bids/:bidId', { bidId: '@BidId' }, {
        'update': {
            method: 'PUT'
        },
                    'get': {
        method: 'GET',
        isArray: true
}
    });
});

angular.module('app').factory('ProductResource', function (apiUrl, $resource) {
    return $resource(apiUrl + 'products/:productId', { productId: '@ProductId' }, {

        'update': {
            method: 'PUT'
        },

        'get': {
            method: 'GET',
            isArray: true
        }
    });
});