angular.module('app').factory('AuctionResource', function (apiUrl, $resource) {

    return $resource(apiUrl + 'auctions/:auctionId', { auctionId: '@AuctionId' }, {

        'update': {
            method: 'PUT'
        }
    });
});

angular.module('app').factory('BidResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/bids/:bidId', { bidId: '@BidId' }, {
        'update': {
            method: 'PUT'
        }
    });
});