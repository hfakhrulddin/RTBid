angular.module('app').controller('AuctionController', function ($scope){


    $(function () {
        $("#addClass").click(function () {
            $('#qnimate').addClass('popup-box-on');
        });

        $("#removeClass").click(function () {
            $('#qnimate').removeClass('popup-box-on');
        });
    })


});