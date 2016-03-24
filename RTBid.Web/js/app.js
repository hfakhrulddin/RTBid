/// <reference path="http://localhost:50255/View/Home/Test2.html" />
var myApp = angular.module('app', ['ngResource', 'ui.router', 'LocalStorageModule', 'stripe.checkout', 'ngRoute', 'artistControllers', 'SignalR']);

angular.module('app').value('apiUrl', 'http://localhost:50255/api/');

angular.module('app').config(function ($stateProvider, $urlRouterProvider, $httpProvider) {
    
    $httpProvider.interceptors.push('AuthenticationInterceptor');
    $urlRouterProvider.otherwise('home');
    $stateProvider
        .state('home', { url: '/home', templateUrl: '/templates/home/home.html', controller: 'HomeController' })
        .state('register', { url: '/register', templateUrl: '/templates/register/register.html', controller: 'RegisterController' })
        .state('app', { url: '/app', templateUrl: '/templates/app/app.html', controller: 'AppController' })
                    .state('app.dashboard', { url: '/dashboard', templateUrl: '/templates/app/dashboard/dashboard.html', controller: 'DashboardController' })
                    .state('app.profile', { url: '/profile', templateUrl: '/templates/app/profile/profile.html', controller: 'ProfileController' })
                    .state('app.categories', { url: '/categories', templateUrl: '/templates/app/categories/categories.html', controller: 'CategoriesController' })
                    .state('app.category', { url: '/category', templateUrl: '/templates/app/category/category.html', controller: 'CategoryController' })
                    .state('app.auction', { url: '/auction', templateUrl: '/templates/app/auction/auction.html', controller:'AuctionController' })
                    .state('app.list', { url: '/list', templateUrl: '/templates/app/chatroom/list.html',controller: 'ListController' })
                    .state('app.details', { url: '/details/:itemId', templateUrl: '/templates/app/chatroom/details.html', controller: 'DetailsController' })
                    .state('app.account', { url: '/account', templateUrl: '/templates/app/account/account.html', controller: 'AccountController' })

                        .state('app.selling', { url: '/selling', abstract: true, template: '<ui-view/>' })
                        .state('app.selling.grid', { url: '/grid', templateUrl: '/templates/app/selling/selling.grid.html', controller: 'SellingGridController' })
                        .state('app.selling.detail', { url: '/detail/:id', templateUrl: '/templates/app/selling/selling.detail.html', controller: 'SellingDetailController' })
                        .state('app.selling.add', { url: '/add', templateUrl: '/templates/app/selling/selling.add.html', controller: 'SellingAddController' })

         .state('app.myAuctions', { url: '/myAuctions', abstract: true, template: '<ui-view/>' })
                        .state('app.myAuctions.grid', { url: '/grid', templateUrl: '/templates/app/myAuctions/myAuctions.grid.html', controller: 'MyAuctionsGridController' })
                        .state('app.myAuctions.detail', { url: '/detail/:id', templateUrl: '/templates/app/myAuctions/myAuctions.detail.html', controller: 'MyAuctionsDetailController' })
                        .state('app.myAuctions.add', { url: '/add', templateUrl: '/templates/app/myAuctions/myAuctions.add.html', controller: 'MyAuctionsAddController' })
});

angular.module('app').run(function (AuthenticationService) { AuthenticationService.initialize(); });