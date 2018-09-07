angular.module("petugas.controllers", [])
    .controller('BerkasController', BerkasController)
    .controller('PetugasHomeController', PetugasHomeController)
    ;

function BerkasController($scope, BerkasService, UserServices, LayananServices, MessageServices) {
    window.location.href= "../Admin/index#!/petugas/berkas";
    $scope.Permohonan = BerkasService.Permohonan;
    $scope.Berkas = [];
    $scope.DataPermohonan = {};
    $scope.Pemohon = {};
    $scope.UserPetugas = UserServices.getUser();
    $scope.StatusPermohonan = [{ id: 0, status: "Baru" }, { id: 1, status: "Pengerjaan" }, { id: 2, status: "Selesai" }];
    $scope.DataStatus = {};
    $scope.Layanan = {};
    $scope.Tampil = false;
    $scope.IdPendaftaran;
    $scope.Permohonans = [];
    $scope.DataHapus = {};


    $scope.Init = function () {
        $scope.Permohonan = BerkasService.Permohonan;
    }
    
    if (UserServices.getUser() == "ludvina@gmail.com") {
        $scope.Tampil = true;
    } else {
        $scope.Tampil = false;
    }
    

    $scope.ShowBerkas = function () {
        var a = false;
        angular.forEach($scope.Permohonan[0], function (value, key) {
            if (value.id == $scope.IdPendaftaran) {
                $scope.DataPermohonan = value;
                $scope.DataPermohonan.kelengkapans = [];
                angular.forEach(LayananServices.Layanans, function (value1, key1) {
                    if (value.idLayanan == value1.id) {
                        $scope.Layanan = value1;
                        a = true;
                    }
                })
            }
        })
        if (a == false) {
            MessageServices.error("Data Tidak ditemukan");
        }
    }
    $scope.Detail = function (item) {
        angular.forEach($scope.Permohonan[0], function (value, key) {
            if (value.id == item.id) {
                $scope.DataPermohonan = value;
                $scope.DataPermohonan.kelengkapans = [];
                angular.forEach(LayananServices.Layanans, function (value1, key1) {
                    if (value.idLayanan == value1.id) {
                        $scope.Layanan = value1;
                    }
                })
            }
        })
    }
    $scope.SelectedStatus = function () {
        $scope.DataPermohonan.status = $scope.DataStatus.id;
    }
    $scope.addKelengkapan = function (item) {
        var data = {};
        data.IdPersyaratan = item.id;
        data.idPermohonan = $scope.DataPermohonan.id;
        data.status = "Ada";
        $scope.DataPermohonan.kelengkapans.push(data);
    }
    $scope.update = function () {
        $scope.DataBanding = angular.copy($scope.DataPermohonan);
        $scope.DataPermohonan.tahapans = [];
        var tahap = {};
        tahap.idPermohonan = $scope.DataPermohonan.id;
        tahap.idTahapan = $scope.DataPermohonan.nextTahapan.id;
        $scope.DataPermohonan.tahapans.push(tahap);
        BerkasService.put($scope.DataPermohonan).then(function (response) {
            angular.forEach($scope.Permohonan[0], function (value, key) {
                if (value.id == $scope.DataBanding.id) {
                    var a = $scope.Permohonan[0].indexOf(value, 1);
                    $scope.Permohonan[0].splice(a, 1);
                }
            })
        })
        $scope.DataPermohonan = {};
        $scope.Layanan = {};
        $scope.IdPendaftaran = null;
    }
}

function PetugasHomeController($scope, AuthServices) {
    $scope.Profile = {};
    AuthServices.getProfile().then(
        function (response) {
            $scope.Profile = response;
        }

    );
}