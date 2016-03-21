angular.module('app').factory('auctionProxy', ["Hub", "$timeout", "$http", "$rootScope", "$location",

    function ($rootScope, Hub, $timeout, $http, $location) {

        var hub = new Hub("AuctionHub", {
        listeners: {
            'newChatMessage': function (message) {
                $rootScope.$apply();
                var x = 1;
                $rootScope.$broadcast('rtb.newChatMessage', message);
            },
            'newBid': function (bid) {
                $rootScope.$broadcast('rtb.newBid', bid);
            },
            'auctionStarted': function (auctionId) {
                $rootScope.$broadcast('rtb.auctionStarted', auctionId);
            },
            'auctionFinished': function (auctionId) {
                $rootScope.$broadcast('rtb.auctionFinished', auctionId);
            },
            'heartbeat': function (heartbeat) {
                // implement
                $rootScope.$broadcast('rtb.heartbeat', auctionId);
            }
        },

        methods: ['sendChatMessage', 'bidOnItem'],

        errorHandler: function (error) {
            console.error(error);
        },

    stateChanged: function (state) {
        switch (state.newState) {
            case $.signalR.connectionState.connecting:
                toastr["success"]("Connection Status", "Connecting..");
                break;
            case $.signalR.connectionState.connected:
                toastr["success"]("Connection Status", "Connected!");
                break;
            case $.signalR.connectionState.reconnecting:
                toastr["warning"]("Connection Status", "Lost connection, reconnecting..");
                //your code here
                break;
            case $.signalR.connectionState.disconnected:
                toastr["error"]("Connection Status", "Connection Lost!");
                //your code here
                break;
        }
    },
        hubDisconnected: function () {
            if (hub.connection.lastError) {
                hub.connection.start();
            }
        },
        transport: 'webSockets',
        logging: true
    });

        sendChatMessage = function (message) {
            var chat = $.connection.AuctionHub;
            chat.server.sendChatMessage(message);

            hub.sendChatMessage(message);
        }

        bidOnItem = function (auctionId, bidAmount) {
            var chat = $.connection.AuctionHub;
            chat.server.bidOnItem(auctionId, bidAmount);
        }

    return {

        sendChatMessage: sendChatMessage,
        bidOnItem: bidOnItem
    }

}]);