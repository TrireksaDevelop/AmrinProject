angular.module("petugas.controllers", [])
    .controller('BerkasController', BerkasController)
    .controller('PetugasHomeController', PetugasHomeController)
    ;

function BerkasController($scope, BerkasService) {
    $scope.Berkas = BerkasService.Berkas;
}

function PetugasHomeController($scope) {

}