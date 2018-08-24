'use strict';
angular.module('admin.routes', [])
    .config(function ($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.when('/', '/admin');
        $stateProvider
            .state('admin', {
                url: '/admin',
                templateUrl: 'apps/templates/admin/index.html',
                controller: 'UserController'
            })
            .state('dashboard', {
                url: '/dashboard',
                parent: 'admin',
                templateUrl: 'apps/templates/admin/dashboard.html',
                controller: 'DashboardController'
            })

            .state('tahapan', {
                url: '/tahapan',
                parent: 'admin',
                templateUrl: 'apps/templates/admin/tahapan.html',
                controller: 'TahapanController'
            })

            .state('persyaratan', {
                url: '/persyaratan',
                parent: 'admin',
                templateUrl: 'apps/templates/admin/persyaratan.html',
                controller: 'PersyaratanController'
            })
            .state('bidang', {
                url: '/bidang',
                parent: 'admin',
                templateUrl: 'apps/templates/admin/bidang.html',
                controller: 'BidangController'
            })

            .state('kategori', {
                url: '/kategori',
                parent: 'admin',
                templateUrl: 'apps/templates/admin/kategori.html',
                controller: 'KategoriController'
            })


            .state('admin-petugas', {
                url: '/petugas',
                parent: 'admin',
                templateUrl: 'apps/templates/admin/petugas.html',
                controller: 'PetugasController'
            })


            .state('layanan', {
                url: '/layanan',
                parent: 'admin',
                templateUrl: 'apps/templates/admin/layanan.html',
                controller: 'LayananController'
            })

    })
   ;