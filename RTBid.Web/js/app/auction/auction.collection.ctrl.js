//angular.module('app').controller('AuctionControllerffff', function ($scope, $timeout, AuctionResource, $interval, $http, $routeParams, $stateParams, $rootScope, $location, $anchorScroll, auctionProxy) {

//    setInterval((function () {
//        $location.hash('bottom2');
//        $anchorScroll();
//    }), 10000);

//    ///HB
//    var hbcounter = 0;
//    $interval(function () {
//        hbcounter++;
//        if (hbcounter > 1000) { hbcounter = 0 } else { $scope.heartpulseShow = hbcounter;};
//    }, 5000);

//    ///Scroll
//    $('#sendmessage').click(function () {
//        $location.hash('bottom');
//        $anchorScroll();
//    });

//    ///Timer
//    $scope.TimeDown = function () {
//        var id = window.setTimeout(function () { }, 0);
//        while (id--) {
//            window.clearTimeout(id); // will do nothing if no timeout with id is present
//        }
//        var countDowner, countDown = 20;
//        countDowner = function () {
//            if (countDown < 0) {
//                countDown = 0;
//                return; // quit
//            } else {
//                $scope.countDowntext = countDown; // update scope
//                countDown--; // -1
//                $timeout(countDowner, 1000); // loop it again 
//            }
//        };
//        countDowner()
//    };

//    ////////Search and online friends
//    $http.get('data/data.json').success(function (data) {
//        $scope.artists = data;
//        $scope.artistOrder = 'name';
//    });
//    //show current item for auction - need to fixed CR!!!!!!
//    function activate() {
//        $scope.products = AuctionResource.query();
//    };

//    activate();

//    ///Chat function Jquery
//    //$(function () {
//    //    $.connection.hub.url = 
//    //    var chat = $.connection.ChatHub;

//    //    chat.client.BroadcastMessage = function (name, message) {
//    //        $('#discussion').append('<li><strong class="pull-right primary-font">' + message + '</strong>&nbsp;&nbsp;' + name + '</li><br/>',
//    //            '<img alt="message user image" src="../../../images/profile/robo.jpg" class="direct-chat-img"/><br/>')
//    //    };

//    //    $('#displayname').val(prompt('Enter You Name:', ""));
//    //    $('#message').focus();
//    //    $.connection.hub.logging = true;

//    //    $.connection.hub.start().done(function () {
//    //        $('#sendmessage').click(function () {
//    //            chat.server.send($('#displayname').val(), $('#message').val());
//    //            $('#message').val('');
//    //        });
//    //    });
//    //});

//    ///Chat function Jquery

//    $(function () {
//        $.connection.hub.url = 
//        var chat = $.connection.ChatHub;

//        chat.client.BroadcastMessage = function (name, message) {
//            $('#discussion').append('<li><strong class="pull-right primary-font">' + message + '</strong>&nbsp;&nbsp;' + name + '</li><br/>',
//                '<img alt="message user image" src="../../../images/profile/robo.jpg" class="direct-chat-img"/><br/>')
//        };

//        $('#displayname').val(prompt('Enter You Name:', ""));
//        $('#message').focus();
//        $.connection.hub.logging = true;

//        $.connection.hub.start().done(function () {
//            $('#sendmessage').click(function () {
//                sendChatMessage(('#displayname').val(), $('#message').val());
//                $('#message').val('');
//            });
//        });
//    });

//    ////Heart Beat
//    $(function () {

//        var hearbeat = $.connection.heartBeatHub;

//        //Listen for hearbeats from the server
//        hearbeat.client.Heartbeat = function () {
//            var hbeatName = $('<div />').text('Hearbeat Received').html();
//            $('#hearbeat').append(hbeatName);
//            alert("heartbeat!");
//        };

//        //Call the server and request a hearbeat
//        $.connection.hub.start().done(function () {
//            $('#sendHeartBeat').click(function () {
//                hearbeat.server.heartbeat();
//            });
//        });
//    });
//    //////////////////////
//    //Call the server and request a hearbeat
//    $.connection.hub.start().done(function () {
//        heartpulse = function () {

//            $scope.heartpulseShow = countDown; // update scope
//            countDown--; // -1
//            $timeout(heartpulse, 1000); // loop it again 
//        }

//    });


//    /////dynamic bidding part The Proxy.................................................
//        $scope.messages = [];

//          $rootScope.$on('rtb.newChatMessage', function (message) {
//          $scope.messages.push(message);
//          });

//    //    $rootScope.$on('rtb.newBid', function (bid) {
//    //        $scope.messages.push(bid);
//    //    });

//    //    $rootScope.$on('rtb.auctionFinished', function () {
//    //        window.location.replace('#/');
//    //    });

//    //Using Angular below

//    //angular.module('app').controller('AuctionController', function ($scope) {
//    //    $(function () {
//    //        $.connection.hub.url = 
//    //        var chat = $.connection.ChatHub;

//    //        chat.client.BroadcastMessage = function (name, message) {
//    //            $scope.UserName = name;
//    //            $scope.discussion = message;
//    //        };

//    //        $scope.displayname = prompt('Enter You Name:', "");
//    //        //($scope.message).focus();
//    //        $.connection.hub.logging = true;

//    //        $.connection.hub.start().done(function () {
//    //            $scope.sendmessage = function () {

//    //                chat.server.send($scope.displayname, $scope.message);
//    //                $scope.message = '';
//    //            });
//    //        });
//    //    });
//    //});

//    //////delete later.
//    $(function () {
//        $("#addClass").click(function () {
//            $('#qnimate').addClass('popup-box-on');
//        });

//        $("#removeClass").click(function () {
//            $('#qnimate').removeClass('popup-box-on');
//        });
//    });
//    ////////Details screen
//    $http.get('data/data.json').success(function (data) {
//        $scope.artists = data;
//        $scope.whichItem = $stateParams.itemId;

//        if ($stateParams.itemId > 0) {
//            $scope.prevItem = Number($stateParams.itemId) - 1;
//        } else {
//            $scope.prevItem = $scope.artists.length - 1;
//        }

//        if ($stateParams.itemId < $scope.artists.length - 1) {
//            $scope.nextItem = Number($stateParams.itemId) + 1;
//        } else {
//            $scope.nextItem = 0;
//        }
//    });



//});

//var chat = $.connection.AuctionHub;
//chat.client.newChatMessage = function (message) {
//    $('#discussion').append('<li><strong class="pull-right primary-font">' + message + '</strong>&nbsp;&nbsp;' + "name" + '</li><br/>',
//        '<img alt="message user image" src="../../../images/profile/robo.jpg" class="direct-chat-img"/><br/>')
//};

//$.connection.hub.url =
//$.connection.hub.logging = true;
//$.connection.hub.start().done();

//var chat = $.connection.AuctionHub;

//chat.client.addNewMessageToPage = function (name, message) {
//$scope.discussion.add(message);
//$rootScope.$apply();
//$scope.discussion.push(message);


//var chat = $.connection.AuctionHub;
//chat.server.sendChatMessage(message);

//if (!hub.connectionStarted) {
//$.connection.hub.url = 
//$.connection.hub.logging = true;
//$.connection.hub.start().done();
//hub.promise.done();
//$.connection.AuctionHub;
//hub.connection.start()
//hub.promise.done(
//hub.connectionStarted = true

//C#
//public void Countdown()
//{
//    bool closedBit = false;
//    DateTime total = DateTime.Now.AddSeconds(20);
//    TimeSpan time1 = new TimeSpan();
//   

//    while (time1.Seconds > 0)
//    {
//        time1 = total - DateTime.Now;
//    }

//    closedBit = true;
//    EndAuction(4, closedBit);
//}
