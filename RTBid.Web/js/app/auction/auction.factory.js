angular.module('app').factory('auctionProxy',
    function ($rootScope, Hub, $timeout, $http, $location) {

        var hub = new Hub("AuctionHub", {

            listeners: {
                'newChatMessage': function (auctionId, message, userName) {
                    $rootScope.$broadcast('rtb.newChatMessage', message, userName);
                },
                'newBid': function (auctionId, currentAmount) {
                    $rootScope.$broadcast('rtb.newBid', auctionId, currentAmount);
                },
                'auctionStarted': function (auctionId) {
                    $rootScope.$broadcast('rtb.auctionStarted', auctionId);
                },
                'auctionEnded': function (auctionId) {
                    $rootScope.$broadcast('rtb.auctionFinished', auctionId);
                },
                'startUp': function (auctionId, colseTime, currentAmount) {
                    $rootScope.$broadcast('rtb.startUp', auctionId, colseTime, currentAmount);
                },
                //'heartbeat': function () {
                //    $rootScope.$broadcast('rtb.heartbeat');
                //},
                //'onlineUsers': function (usersList) {
                //    $rootScope.$broadcast('rtb.onlineUsers', usersList);
                //},
            },

            methods: ['sendChatMessage', 'bidOnItem', 'join', 'pageStartUp'],

            errorHandler: function (error) {
                console.error(error);
            },
            hubDisconnected: function () {
                if (hub.connection.lastError) {
                    hub.connection.start();
                }
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
            transport: 'webSockets',
            logging: true,
       
        });

        hub.connection.url = 'http://localhost:50255/signalr/hubs';
        hub.connection.logging = true;
        hub.connection.start();
        var chat = $.connection.AuctionHub;
        

        hub.connection.disconnected(function () {
            if (hub.connection.lastError) {

                setTimeout(function () {
                    hub.connection.url = 'http://localhost:50255/signalr/hubs';
                    hub.connection.logging = true;
                    hub.connection.start();
                    var chat = $.connection.AuctionHub;

                }, 50);
            }
        });

        var sendChatMessage = function (auctionId, message, userName) {

            hub.connection.url = 'http://localhost:50255/signalr/hubs';
            hub.connection.logging = true;

            hub.connection.start().done(function () {
                hub.sendChatMessage(auctionId, message, userName)

            }
            )};
         
        var bidOnItem = function (auctionId) {

            hub.connection.url = 'http://localhost:50255/signalr/hubs';
            hub.connection.logging = true;

            hub.connection.start().done(function () {
                hub.bidOnItem(auctionId)
                    
                });
            
            };

        var pageStartUp = function (userName) {

            hub.connection.url = 'http://localhost:50255/signalr/hubs';
            hub.connection.logging = true;

            hub.connection.start().done(function () {
                hub.pageStartUp()

            }
            )
        };
        pageStartUp();
     
        return {

            sendChatMessage: sendChatMessage,
            bidOnItem: bidOnItem,
            //join: join,
            pageStartUp: pageStartUp
        }

    });

