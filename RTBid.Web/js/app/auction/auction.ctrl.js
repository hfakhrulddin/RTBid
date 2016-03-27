angular.module('app').controller('AuctionController',
function (auctionProxy, $scope, $rootScope, $timeout, $interval, $http, $routeParams, BidResource, AuctionResource, ProfileResource,
    $stateParams, $location, $anchorScroll, ProductResource) {

    function activate() {
        ProfileResource.getCurrentUser().then(function (response) {
            $scope.user = response;
        });
    }

    activate();

    $scope.auction = AuctionResource.get({ auctionId: $stateParams.auctionId });
    $scope.product = ProductResource.get({ productId: $stateParams.productId });

    $('#message').val('').focus();
    $scope.sendChat = function () {
        auctionProxy.sendChatMessage($stateParams.auctionId, $scope.message, $scope.user.UserName);
        $scope.message = '';
    }

    $scope.sendBid = function () {
        // send bid to BidResource
        var bid = new BidResource();
        bid.AuctionId = $stateParams.auctionId;
        bid.$save(function (bid) {
        // tell people about this new bid (after the bid has been saved to db) 
        //auctionProxy.bidOnItem($scope.auction.AuctionId);
        });
    }

    $rootScope.$on('rtb.newChatMessage', function (event, message, userName) {
        toastr.info('New Message');
        $scope.discussions = [];
        //$scope.sendChat = function () {
        //$scope.discussions.push({ message: message });
        $('#discussion').append('<li><strong class="pull-right primary-font">'+message+'</strong>&nbsp;&nbsp;' +
        '<h4>'+userName+'</h4>' +'</li><br/>'+ '<img alt="message user image" src="../../../images/profile/robo.jpg" class="direct-chat-img"/><br/>')
   
        });
    
    /////Scroll
    $('#sendChat').click(function () {
        $location.hash('bottom');
        $anchorScroll();
    });

    ///HB
    //var hbcounter = 0;
    //$interval(function () {
    //    //auctionProxy.join($scope.user.UserName);

    //    hbcounter++;
    //    if (hbcounter > 1000) { hbcounter = 0 } else { $scope.heartpulseShow = hbcounter; };
    //}, 90000000);

    $rootScope.$on('rtb.auctionStarted', function (event, auctionId) {
        $scope.checked = false;
        $scope.soldImg = false;
            toastr.success("The Auction Has been Started");
            console.log('Started!!!');
        });
    
    $rootScope.$on('rtb.auctionFinished', function (event, auctionId) {
        $scope.checked = true;
        $scope.soldImg = true;
            toastr.info("The Auction Has been Finished");
            console.log('Finished!!!');
    });

    $rootScope.$on('rtb.startUp', function (auctionId, colseTime, currentAmount) {
        $scope.auction.CurrentAmount =  currentAmount;
        $scope.countDowntext = colseTime;
        toastr.info("Updated !!");
        console.log('Updated !!!');
    });
    $scope.auction.CurrentAmount = $scope.auction.StartBid;
    $scope.countDowntext = $scope.auction.ClosedTime;

    //$rootScope.$on('rtb.heartbeat', function (event) {
    //    console.log('HEARTBEAT!!!');
    //});

    //$rootScope.$on('rtb.onlineUsers', function (event, usersList) {
    //    $scope.usersList = usersList;
    //});

    /////CountDown Timer Not Angular
    $rootScope.$on('rtb.newBid', function (event, auctionId, currentAmount) {
        toastr.info('New Bid');

        $scope.auction.CurrentAmount = currentAmount;

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

});