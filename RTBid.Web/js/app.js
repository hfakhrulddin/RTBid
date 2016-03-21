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
                    .state('app.selling', { url: '/selling', templateUrl: '/templates/app/selling/selling.html', controller: 'SellingController' })
                    .state('app.account', { url: '/account', templateUrl: '/templates/app/account/account.html', controller: 'AccountController' });       
});

angular.module('app').run(function (AuthenticationService) { AuthenticationService.initialize(); });