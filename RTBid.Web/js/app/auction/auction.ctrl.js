angular.module('app').controller('AuctionController',
function (auctionProxy, $scope, $rootScope, $timeout, $interval, $http, $routeParams, BidResource, AuctionResource, ProfileResource,
    $stateParams, $location, $anchorScroll) {

    function activate() {
        ProfileResource.getCurrentUser().then(function (response) {
            $scope.user = response;
        });
        $scope.auction = AuctionResource.get({ auctionId: $stateParams.auctionId });
    }

    activate();

    $('#message').val('').focus();
    $scope.sendChat = function () {
        auctionProxy.sendChatMessage($scope.auction.AuctionId, $scope.message);
        $scope.message = '';
    }

    $scope.sendBid = function () {
        toastr.success('Good Luck', 'Good Luck');
        
        // send bid to BidResource
        var bid = new BidResource();
        bid.AuctionId = $scope.auction.AuctionId;
        bid.$save(function(newBid) {
            // tell people about this new bid (after the bid has been saved to db)
            //auctionProxy.bidOnItem($scope.auction.AuctionId, newBid.CurrentAmount);   
            auctionProxy.bidOnItem($scope.auction.AuctionId);
        });
    }

    $rootScope.$on('rtb.newChatMessage', function (event, message) {
        toastr.info('New Message');
        $scope.discussions = [];
        //$scope.sendChat = function () {
        //$scope.discussions.push({ message: message });
        $('#discussion').append('<li><strong class="pull-right primary-font">'+message+'</strong>&nbsp;&nbsp;' +
        "<h4>Hussen Fakhrulddin</h4>" +'</li><br/>'+ '<img alt="message user image" src="../../../images/profile/robo.jpg" class="direct-chat-img"/><br/>')
    });

    $rootScope.$on('rtb.auctionStarted', function (event, auctionId, openedBit) {
        toastr.info("The Auction Has been Started");
        console.log('Started!!!');
    });
    
    $rootScope.$on('rtb.auctionFinished', function (event, auctionId, closedBit) {
        toastr.info("The Auction Has been Finished");
        console.log('Finished!!!');
    });

    $rootScope.$on('rtb.heartbeat', function (event) {
        console.log('HEARTBEAT!!!');
    });

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

    /////Scroll
    $('#sendChat').click(function () {
        $location.hash('bottom');
        $anchorScroll();
    });

});