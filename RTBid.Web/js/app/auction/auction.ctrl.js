angular.module('app').controller('AuctionController',

function (auctionProxy, $scope, $rootScope, $timeout, AuctionResource, $interval, $http, $routeParams, $stateParams, $location, $anchorScroll) {

        $('#message').val('').focus();
        $.connection.hub.url = 'http://localhost:50255/signalr';
        $.connection.hub.logging = true;
        $.connection.hub.start().done();

        $rootScope.$on('rtb.newChatMessage', function (message) {
            var x = 1;
            $scope.discussion.push(message);
        });

        $scope.chats = auctionProxy;
        $scope.sendChat = function () {
            $scope.chats.sendChatMessage($scope.message);
            $('#message').val('');

        }
    });

            //var chat = $.connection.AuctionHub;
            //chat.client.newChatMessage = function (message) {
            //    $('#discussion').append('<li><strong class="pull-right primary-font">' + message + '</strong>&nbsp;&nbsp;' + "name" + '</li><br/>',
            //        '<img alt="message user image" src="../../../images/profile/robo.jpg" class="direct-chat-img"/><br/>')
            //};

     


