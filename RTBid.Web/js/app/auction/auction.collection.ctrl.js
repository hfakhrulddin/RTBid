﻿//angular.module('app').controller('AuctionControllerffff', function ($scope, $timeout, AuctionResource, $interval, $http, $routeParams, $stateParams, $rootScope, $location, $anchorScroll, auctionProxy) {

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


//Moment.JS CurrentTime
//$(function () {
//    setInterval(function () {
//        var divUtc = $('#divUTC');
//        var divLocal = $('#divLocal');
//        //put UTC time into divUTC  
//        divUtc.text(moment.utc().format('YYYY-MM-DD HH:mm:ss'));

//        //get text from divUTC and conver to local timezone  
//        var localTime = moment.utc(divUtc.text()).toDate();
//        localTime = moment(localTime).format('YYYY-MM-DD HH:mm:ss');
//        divLocal.text(localTime);
//    }, 1000);
//});



//////Search for users Online
//<!--<div class="panel panel-primary" id="searchPanel">
//    <div class="panel-heading">
//        <h4><i class="fa fa-search"></i> Search</h4>
//    </div>
//    <div class="panel-body">
//        <div class="form-group">
//            <label>Search:</label>
//            <input type="text" class="form-control" ng-model="query" placeholder="Enter a Username" />
//        </div>

//        <ul ng-show="query">
//            <li ng-animate="'animate'" class="artist cf" ng-repeat="item in artists | filter: query | orderBy: artistOrder:direction">
//                <a href="/#/app/details/{{artists.indexOf(item)}}">
//                    <img ng-src="images/{{item.shortname}}_tn.jpg" alt="Photo of {{item.name}}">
//                    <div class="info">
//                        <h2>{{item.name}}</h2>
//                        <h3>{{item.reknown}}</h3>
//                    </div>
//                </a>
//            </li>
//        </ul>
//    </div>
//</div>-->
//<!---Heart Beat Test-->
//<!--<div>
//    <div id="hearbeat">hihihihihih </div>
//    <input type="button" id="sendHeartBeat" value="Send HB" />
//</div>-->


////Dashboard controller old code

//<!--The Original Code-->
////$(function () {
////    var toggleFunction;
////    $('.toggle-handle').click(toggleFunction = function () {
////        var area = $('#' + $(this).attr('data-area'));
////        if (area.hasClass('expanded')) {
////            area.removeClass('expanded');
////        } else {
////            area.addClass('expanded');
////        }
////        $(this).blur();
////        return false;
////    });

////    $('#supportedCauses').append(
////        $(document.createElement('div')).attr('id', 'pane4').addClass('cause-info').append(
////            $(document.createElement('div')).append(
////                $(document.createElement('img')).attr('src', 'http://lorempixel.com/420/420/people'),
////                $(document.createElement('div')).append(
////                    $(document.createElement('h4')).text('[Name]'),
////                    $(document.createElement('h4')).text('[Cause]')
////                ),
////                $(document.createElement('div')).append(
////                    $(document.createElement('h4')).text('[X] Votes')
////                )
////            ),
////            $(document.createElement('div')).append(
////                $(document.createElement('h4')).text('About:'),
////                $(document.createElement('div')).append(
////                    $(document.createElement('p')).text(
////                        'Nam ex ullum assum apeirian, facilisi splendide quo ex. Sea et nonumy accusata, in utinam vocent facilis vix. \
////                        Cu vix eripuit temporibus mediocritatem, denique theophrastus ne mel, et graecis maiorum mediocritatem per. \
////                        Magna tacimates sed eu, sit no graeco latine referrentur. Id sed assum quaerendum, apeirian erroribus ut his. Ex mei mazim minimum.'
////                    ),
////                    $(document.createElement('h5')).html('More at <a>[Website]</a>')
////                ),
////                $(document.createElement('button')).addClass('btn btn-primary pull-right').text('Give')
////            ),
////            $(document.createElement('div')).append(
////                $(document.createElement('h2')).append(
////                    $(document.createElement('a')).append(
////                        $(document.createElement('span')).addClass('glyphicon glyphicon-chevron-down')
////                    ).attr('href', '#pane4').attr('data-area', 'pane4').click(toggleFunction)
////                )
////            )
////        )
////    );
////});





//$(document).ready(function () {
//    var panels = $('.user-infos');
//    var panelsButton = $('.dropdown-user');
//    panels.hide();

//    //Click dropdown
//    panelsButton.click(function () {
//        //get data-for attribute
//        var dataFor = $(this).attr('data-for');
//        var idFor = $(dataFor);

//        //current button
//        var currentButton = $(this);
//        idFor.slideToggle(400, function () {
//            //Completed slidetoggle
//            if (idFor.is(':visible')) {
//                currentButton.html('<i class="glyphicon glyphicon-chevron-up text-muted"></i>');
//            }
//            else {
//                currentButton.html('<i class="glyphicon glyphicon-chevron-down text-muted"></i>');
//            }
//        })
//    });


//    $('[data-toggle="tooltip"]').tooltip();

//    $('button').click(function (e) {
//        e.preventDefault();
//        alert("This is a demo.\n :-)");
//    });
//});