angular.module("petugas.controllers", [])
    .controller('BerkasController', BerkasController)
    .controller('PetugasHomeController', PetugasHomeController)
    ;

function BerkasController($scope, BerkasService, UserServices, LayananServices) {
    $scope.Permohonan = BerkasService.Permohonan;
    $scope.Berkas = [];
    $scope.DataPermohonan;
    $scope.Pemohon = {};
    $scope.UserPetugas = UserServices.getUser();
    $scope.StatusPermohonan = [{ id: 0, status: "Baru" }, { id: 1, status: "Pengerjaan" }, { id: 2, status: "Selesai" }];
    $scope.DataStatus = {};
    $scope.Layanan = {};
    $scope.Tampil = false;
    $scope.IdPendaftaran;
    
    if (UserServices.getUser() == "alpritsrupang@gmail.com") {
        $scope.Tampil = true;
    } else {
        $scope.Tampil = false;
    }
    

    $scope.ShowBerkas = function () {
        angular.forEach($scope.Permohonan[0], function (value, key) {
            if (value.id == $scope.IdPendaftaran) {
                $scope.DataPermohonan = value;
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
        $scope.DataPermohonan.kelengkapans.push(item);
    }
    $scope.update = function () {
        $scope.DataPermohonan.tahapans = $scope.DataPermohonan.nextTahapan;
        BerkasService.put($scope.DataPermohonan).then(function (response) {
            angular.forEach($scope.Permohonan, function (value, key) {
                if (value.id == $scope.DataPermohonan.id) {
                    value.kelengkapans = $scope.DataPermohonan.kelengkapans;
                    value.tahapans = $scope.DataPermohonan.tahapans;
                }
            })
        })
    }
}

function PetugasHomeController($scope) {

}