angular.module('app').factory('MyAuctionsResource', function (apiUrl, $resource) {

    return $resource(apiUrl + '/auctions/:auctionId', { auctionId: '@AuctionId' }, {

        'update': {
            method: 'PUT'
        }
    });
});