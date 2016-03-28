angular.module('app').factory('CategoryResource', function (apiUrl, $resource) {

//    return $resource(apiUrl + '/categories/:categoryId', { categoryId: '@categoryId' }, {

//        'update': {
//            method: 'PUT'
//        },

//                'query': {
//        method: 'GET',
//        isArray: true
//}


//    });


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