angular.module("petugas.services", [])
    .factory('BerkasService', BerkasService)
    ;
function BerkasService($http, $state, $rootScope, $q, MessageServices, UserServices) {
    let def = $q.defer();

    var service = {
        instance: false,
        Permohonan: [],
        get: get, put: EditItem
    }
    service.get();
    return service;

    function get() {

        if (!this.instance) {
            NProgress.start();
            $http({
                method: 'Get',
                url: 'api/Permohonan/admin',
                headers: UserServices.getHeaders()
            }).then(function (response) {
                service.Permohonan.push(response.data);
               
                NProgress.done();
                service.instance = true;
                def.resolve(response.data);

            }, function (response) {
                if (response.status = 401)
                    $state.go('login');
                NProgress.done();
                def.reject();
            });
        } else
            def.resolve(this.Permohonan);

        return def.promise;
    }

    function EditItem(item) {
        $http({
            method: 'post',
            url: 'api/Permohonan/UpdatePermohonan',
            data: item,
            headers: UserServices.getHeaders()
        }).then(function (response) {
            MessageServices.success("Data Tersimpan");
            def.resolve(response.data);
        }, function (response) {
            MessageServices.error("Data Tidak Tersimpan");
            def.reject();
        });
        return def.promise;
    }
}
