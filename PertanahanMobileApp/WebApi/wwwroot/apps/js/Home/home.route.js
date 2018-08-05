'use strict';
angular.module('home.routes', [])
    .config(function ($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.when('/', '/login');
        $stateProvider
            .state('login', {
                url: '/login',
                templateUrl: '/apps/templates/home/login.html',
                controller: 'LoginController'
            })
            .state('register', {
                url: '/register',
                templateUrl: '/apps/templates/home/register.html',
                controller: 'RegisterController'
            })
    })
   ;