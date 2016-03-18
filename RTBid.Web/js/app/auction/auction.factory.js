// JUST AN ANGULAR SERVICE
angular.module('app').factory('auctionProxy', function ($rootScope, Hub, $timeout) {
    var hub = new Hub('AuctionHub', {
        listeners: {
            'newChatMessage': function (message) {
                $rootScope.$broadcast('rtb.newChatMessage', message);
            },
            'newBid': function (bid) {
                $rootScope.$broadcast('rtb.newBid', bid);
            },
            'auctionStarted': function(auctionId) {
                $rootScope.$broadcast('rtb.auctionStarted', auctionId);
            },
            'auctionFinished': function (auctionId) {
                $rootScope.$broadcast('rtb.auctionFinished', auctionId);
            },
            'heartbeat': function (heartbeat) {
                // implement
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
        }
    });

    var sendChatMessage = function (auctionId, message) {
        hub.sendChatMessage(auctionId, message);
        };




    var sendChatMessage = function (auctionId, message) {
        chat.server.send(auctionId, message);
      
    };


    var bidOnItem = function (auctionId, message) {
        // implement
    };

    return {
        sendChatMessage: sendChatMessage,
        bidOnItem: bidOnItem
    };
});