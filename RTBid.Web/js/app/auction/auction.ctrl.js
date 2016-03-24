angular.module('app').controller('AuctionController',
function (auctionProxy, $scope, $rootScope, $timeout, $interval, $http, $routeParams, ProfileResource, $stateParams, $location, $anchorScroll) {
    var CurrentAmount;

     function activate() {
        ProfileResource.getCurrentUser().then(function (response) {
            $scope.user = response;
        });
    }
        activate();

    $('#message').val('').focus();
    $scope.chats = auctionProxy;

    $scope.sendChat = function () {
        $scope.chats.sendChatMessage(4, $scope.message);
        $('#message').val('');
        $scope.message = [];
        //$scope.message.clear();
    }

    $scope.TimeDown = function () {
        toastr.success('Good Luck', 'Good Luck');
        toastr.clear();
        var bidding = true;
        $scope.chats.bidOnItem(CurrentAmount, bidding);
      
    }

    $rootScope.$on('rtb.newChatMessage', function (event, message) {
        toastr.info('New Message');
        $scope.discussions = [];

            //$scope.sendChat = function () {
            //$scope.discussions.push({ message: message });

        $('#discussion').append('<li><strong class="pull-right primary-font">' + message + '</strong>&nbsp;&nbsp;' + " <h4>Hussen Fakhrulddin</h4>" + '</li><br/>'
                +'<img alt="message user image" src="../../../images/profile/robo.jpg" class="direct-chat-img"/><br/>')
        });

///Timer
        $rootScope.$on('rtb.newBid', function (event, CurrentAmount, restBit) {
            //toastr.info('New Bid');
           
            $scope.CurrentAmount = CurrentAmount;

        var id = window.setTimeout(function () { }, 0);
        while (id--) {
            window.clearTimeout(id); // will do nothing if no timeout with id is present
        }
        var countDowner, countDown = 20;
        countDowner = function () {
            if (countDown < 0) {
                countDown = 0;
                return; // quit
            } else {
                $scope.countDowntext = countDown; // update scope
                countDown--; // -1
                $timeout(countDowner, 1000); // loop it again 
            }
        };
        countDowner()
       
        });

        $(function () {
            setInterval(function () {
                var divUtc = $('#divUTC');
                var divLocal = $('#divLocal');
                //put UTC time into divUTC  
                divUtc.text(moment.utc().format('YYYY-MM-DD HH:mm:ss'));

                //get text from divUTC and conver to local timezone  
                var localTime = moment.utc(divUtc.text()).toDate();
                localTime = moment(localTime).format('YYYY-MM-DD HH:mm:ss');
                divLocal.text(localTime);
            }, 1000);
        });

        $rootScope.$on('rtb.auctionStarted', function (event, auctionId, openedBit) {
            console.log('Started!!!');
        });

        $rootScope.$on('rtb.auctionFinished', function (event, auctionId, closedBit) {
            toastr.info("The Auction Has been Finished");
            //alert("The Auction Has been Finished");
            console.log('Finished!!!');
        });

        $rootScope.$on('rtb.heartbeat', function (event) {
            console.log('HEARTBEAT!!!');
        });

    /////Scroll
        $('#sendChat').click(function () {
            $location.hash('bottom');
            $anchorScroll();
        });

});







