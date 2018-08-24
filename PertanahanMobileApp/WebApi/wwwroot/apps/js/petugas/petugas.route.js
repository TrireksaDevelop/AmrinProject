//'use strict';
angular.module('petugas.routes', [])
    .config(function ($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.when('/petugas', '/petugas');
        $stateProvider
            .state('petugas', {
                url: '/petugas',
                templateUrl: '/apps/templates/petugas/index.html',
                controller: 'PetugasHomeController'
            })
            .state('berkas', {
                url: '/berkas',
                parent: 'petugas',
                templateUrl: 'apps/templates/petugas/berkas.html',
                controller: 'BerkasController'
            })
        
    })
   ;