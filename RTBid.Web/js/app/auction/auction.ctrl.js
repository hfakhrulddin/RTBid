angular.module('app').controller('AuctionController', function ($scope) {

    $(function () {
        $.connection.hub.url = 'http://localhost:50255/signalr';
        var chat = $.connection.ChatHub;

        chat.client.BroadcastMessage = function (name, message) {
            $('#discussion').append('<li><strong class="pull-right primary-font">' + message + '</strong>&nbsp;&nbsp;'+ name + '</li>',
                '<img src="http://placehold.it/50/FA6F57/fff&amp;text=ME" alt="User Avatar" class="img-circle">')
        };

        $('#displayname').val(prompt('Enter You Name:', ""));
        //$scope.displayname=prompt('Enter You Name:', "");

        $('#message').focus();
        $.connection.hub.logging = true;

        $.connection.hub.start().done(function () {
            $('#sendmessage').click(function () {
                chat.server.send($('#displayname').val(), $('#message').val());
                $('#message').val('');
                //chat.server.send($scope.displayname, $scope.message2);
                //$scope.message2.val('');
            });
        });
    });
});


