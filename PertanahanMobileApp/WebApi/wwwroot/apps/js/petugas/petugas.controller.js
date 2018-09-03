angular.module("petugas.controllers", [])
    .controller('BerkasController', BerkasController)
    .controller('PetugasHomeController', PetugasHomeController)
    ;

function BerkasController($scope, BerkasService) {
    $scope.Permohonan = BerkasService.Permohonan;
    $scope.Berkas = [];
    $scope.DataPermohonan;
    
    $scope.ShowBerkas = function (item) {
        $scope.Berkas = item.Berkas;
        $scope.DataPermohonan = angular.copy(item);
        $scope.DataPermohonan.kelengkapans = [];
        $scope.ModalTitle = "Verifikasi Berkas";
    }
    $scope.addKelengkapan = function (item) {
        $scope.DataPermohonan.kelengkapans.push(item);
    }
    $scope.update = function () {
        $scope.DataPermohonan.tahapans = $scope.DataPermohonan.nextTahapan;
        BerkasService.put($scope.DataPermohonan).then(function (response) { })
    }
}

function PetugasHomeController($scope) {

}