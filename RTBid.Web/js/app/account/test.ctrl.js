angular.module('app').controller('TestController', function ($scope) {

    $(function () {
        $.connection.hub.url = 'http://localhost:50255/signalr';
        var chat = $.connection.ChatHub;

        chat.client.BroadcastMessage = function (name, message) {
            $('#discussion').append('<li><strong>' + name + '</strong>:&nbsp;&nbsp;' + message + '</li>');
        };

        $('#displayname').val(prompt('Enter You Name:', ""));
        $('#meesage').focus();
        $.connection.hub.logging = true;

        $.connection.hub.start().done(function () {
            $('#sendmessage').click(function () {
                chat.server.send($('#displayname').val(), $('#message').val());
                $('#message').val('');
            });
        });
    });
});