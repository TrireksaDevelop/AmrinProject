angular.module("admin.controllers", [])
    .controller('UserController', UserController)
    .controller('DashboardController', DashboardController)
    .controller('LayananController', LayananController)
    .controller('TahapanController', TahapanController)
    .controller('PersyaratanController', PersyaratanController)
    .controller('BidangController', BidangController)
    .controller('PetugasController', PetugasController)
    .controller('KategoriController', KategoriController)
    ;


function KategoriController($scope, KategoriServices) {
    $scope.Kategories = KategoriServices.Kategories;
   
    $scope.Save = function (item) {
        if (item.id == undefined) {
            KategoriServices.post(item).then(function (response) { })
        } else
            KategoriServices.put(item).then(function (response) { })
    }

    $scope.EditItem = function (item) {
        $scope.ModalTitle = "Edit";
        $scope.model = item;
    }

    $scope.Delete = function (item) {
        KategoriServices.delete(item).then(function (response) { })
    }
}
function BidangController($scope, BidangServices, PetugasServices) {
    $scope.Petugas = PetugasServices.Petugas;
    $scope.Bidangs = BidangServices.Bidangs;
    $scope.Save = function (item) {
        if (item.id == undefined) {
            item.petugasid = item.petugas.id;
            BidangServices.post(item).then(function (response) { })
        } else
            BidangServices.put(item).then(function (response) { })
    }

    $scope.EditItem = function (item) {
        $scope.ModalTitle = "Edit";
        $scope.model = item;
    }

    $scope.Delete = function (item) {
        BidangServices.delete(item).then(function (response) { })
    }
}
function PetugasController($scope, PetugasServices) {
    $scope.Petugas = PetugasServices.Petugas;

    $scope.Save = function (item) {
        if (item.id == undefined) {
            PetugasServices.post(item).then(function (response) { })
        } else
            PetugasServices.put(item).then(function (response) { })
    }

    $scope.EditItem = function (item) {
        $scope.ModalTitle = "Edit";
        $scope.model = item;
    }

    $scope.Delete = function (item) {
        PetugasServices.delete(item).then(function (response) { })
    }
}
function UserController(UserServices,$state) {
    var res = UserServices.getToken();
    var res1 = UserServices.getUser();
    if (res == null)
        $state.go("login");
}

function DashboardController(UserServices) {

    var res = UserServices.getToken();
    var res1 = UserServices.getUser();
    if (res == "")
        $state.go("login");
}

function LayananController($scope, LayananServices, TahapanServices, PersyaratanServices, MessageServices, KategoriServices) {
    $scope.Kategories = KategoriServices.Kategories;
    $scope.tahapans = TahapanController.Tahapans;
    $scope.modalTitlePersayaratan = "Tambah Persyaratan";
   
    $scope.Layanans = LayananServices.Layanans;
    $scope.SelectedLayananItem = {};
    $scope.Save = function (item) {
        if (item.id == undefined) {
            item.idKategoriLayanan = item.kategori.id;
            LayananServices.post(item).then(function (response) { })
        } else
            LayananServices.put(item).then(function (response) { })
    }

    $scope.SelectedLayanan = function (item,item1) {
        $scope.SelectedLayananItem = item;
        if (item1 != null) {
            $scope.Tahapans = [];
            angular.forEach(TahapanServices.Tahapans, function (value, key) {
                let isfound = false;
                angular.forEach(item.tahapans, function (value1, key1) {
                    if (value.id == value1.id) {
                        value1.IsNew = false;
                        $scope.Tahapans.push(value1);
                        isfound = true;
                    }
                })
                if (!isfound) {
                    var result = angular.copy(value);
                    result.IsNew = true;
                    result.layananid = item.id;
                    $scope.Tahapans.push(result);
                }
            })
        }
    }

    $scope.EditPersyaratan = function (item) {
        $scope.modelPersyaratan = item;
        $scope.modalTitlePersayaratan = "Edit Persyaratan";
    }

    $scope.SavePersyaratan = function (model) {
        model.idlayanan = $scope.SelectedLayananItem.id;
        if (model.id == undefined) {
            PersyaratanServices.post(model).then(function (response) {
                $scope.SelectedLayananItem.persyaratans.push(response);
                $scope.modelPersyaratan = {};
            }, function (error) {
                MessageServices.error(error.Message);
            });
        } else {
            PersyaratanServices.put(model).then(function (response) {
               // $scope.SelectedLayananItem.Persyaratans.put(response);
            }, function (error) {
                MessageServices.error(error.Message);
            });
        }


    }

    $scope.SaveTahapan = function (model) {
        LayananServices.puttahapan(model, $scope.SelectedLayananItem).then(function (response) {
            $scope.SelectedLayananItem.tahapans = [];
            angular.forEach(response, function (value, key) {
                $scope.SelectedLayananItem.tahapans.push(value);
            })

        });

    }




    $scope.DeletePersyaratan = function (list ,item) {
        PersyaratanServices.delete(item).then(function (response) {
            var index = list.indexOf(response, 1);
            list.splice(index, 1);
        });
    }



}



function TahapanController($scope, BidangServices, TahapanServices) {
    $scope.Tahapans = TahapanServices.Tahapans;
    $scope.Bidangs = BidangServices.Bidangs;
    $scope.Save = function (item) {
        if (item.id == undefined) {
            item.bidangid = item.bidang.id;
            TahapanServices.post(item).then(function (response) { })
        } else
            TahapanServices.put(item).then(function (response) { })
    }

    $scope.EditItem = function (item) {
        $scope.ModalTitle = "Edit";
        $scope.model = item;
    }

    $scope.Delete = function (item) {
        TahapanServices.delete(item).then(function (response) { })
    }
}


function PersyaratanController($scope, PersyaratanServices) {
    $scope.Persyaratans = PersyaratanServices.Persyaratans;
    $scope.Save = function (item) {
        if (item.id == undefined) {
            item.petugasid = item.petugas.id;
            PersyaratanServices.post(item).then(function (response) { })
        } else
            PersyaratanServices.put(item).then(function (response) { })
    }

    $scope.EditItem = function (item) {
        $scope.ModalTitle = "Edit Persyaratan";
        $scope.model = item;
    }

    $scope.Delete = function (item) {
        PersyaratanServices.delete(item).then(function (response) { })
    }
}