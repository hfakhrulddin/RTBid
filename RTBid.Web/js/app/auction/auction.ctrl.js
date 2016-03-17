angular.module('app').controller('AuctionController', function ($scope, AuctionResource, $http, $routeParams, $stateParams) {
    ////////Search and online friends
    $http.get('data/data.json').success(function (data) {
        $scope.artists = data;
        $scope.artistOrder = 'name';
    });
    $(function () {
        $("#addClass").click(function () {
            $('#qnimate').addClass('popup-box-on');
        });

        $("#removeClass").click(function () {
            $('#qnimate').removeClass('popup-box-on');
        });
    })
    ////////Details screen
    $http.get('data/data.json').success(function (data) {
        $scope.artists = data;
        $scope.whichItem = $stateParams.itemId;

        if ($stateParams.itemId > 0) {
            $scope.prevItem = Number($stateParams.itemId) - 1;
        } else {
            $scope.prevItem = $scope.artists.length - 1;
        }

        if ($stateParams.itemId < $scope.artists.length - 1) {
            $scope.nextItem = Number($stateParams.itemId) + 1;
        } else {
            $scope.nextItem = 0;
        }
    });
    //need to fixed CR!!!!!!
    function activate() {

        $scope.products = AuctionResource.query();
    }
    activate();
    ///Chat function Jquery
    $(function () {
        $.connection.hub.url = 'http://localhost:50255/signalr';
        var chat = $.connection.ChatHub;

        chat.client.BroadcastMessage = function (name, message) {
            $('#discussion').append('<li><strong class="pull-right primary-font">' + message + '</strong>&nbsp;&nbsp;'+ name + '</li>',
                '<img src="http://placehold.it/50/FA6F57/fff&amp;text=ME" alt="User Avatar" class="img-circle">')
        };

        $('#displayname').val(prompt('Enter You Name:', ""));
        $('#message').focus();
        $.connection.hub.logging = true;

        $.connection.hub.start().done(function () {
            $('#sendmessage').click(function () {
                chat.server.send($('#displayname').val(), $('#message').val());
                $('#message').val('');
            });
        });
    });
});

//Using Angular below
//angular.module('app').controller('AuctionController', function ($scope) {

//    $(function () {
//        $.connection.hub.url = 'http://localhost:50255/signalr';
//        var chat = $.connection.ChatHub;

//        chat.client.BroadcastMessage = function (name, message) {
//            $scope.UserName = name;
//            $scope.discussion = message;
//        };

//        $scope.displayname = prompt('Enter You Name:', "");
//        //($scope.message).focus();
//        $.connection.hub.logging = true;

//        $.connection.hub.start().done(function () {
//            $scope.sendmessage = function () {

//                chat.server.send($scope.displayname, $scope.message);
//                $scope.message = '';
//            });
//        });
//    });
//});

