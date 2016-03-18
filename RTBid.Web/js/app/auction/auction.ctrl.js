angular.module('app').controller('AuctionController', function ($scope, $timeout, AuctionResource, $interval, $http, $routeParams, $stateParams, $rootScope, $location, $anchorScroll) {

    ///////dynamic bidding part (The Proxy).................................................
    ////        $scope.messages = [];

    ////          $rootScope.$on('rtb.newChatMessage', function (message) {
    ////          $scope.messages.push(message);
    ////          });


              $(function () {
                          $.connection.hub.url = 'http://localhost:50255/signalr';
                          var chat = $.connection.ChatHub;

                          chat.client.sTOclients = function (name, message) {
                              $('#discussion').append('<li><strong class="pull-right primary-font">' + message + '</strong>&nbsp;&nbsp;' + name + '</li><br/>',
                                  '<img alt="message user image" src="../../../images/profile/robo.jpg" class="direct-chat-img"/><br/>')
                          };

                          $('#displayname').val(prompt('Enter You Name:', ""));
                          $('#message').focus();
                          $.connection.hub.logging = true;

                          $.connection.hub.start().done(function () {
                              $('#sendmessage').click(function () {
                                  chat.server.clientTOs($('#displayname').val(), $('#message').val());
                                  $('#message').val('');
                              });
                          });
                      });

});