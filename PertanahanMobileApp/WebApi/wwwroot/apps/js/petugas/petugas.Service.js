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
            $http({
                method: 'Get',
                url: 'api/Permohonan/admin',
                headers: UserServices.getHeaders()
            }).then(function (response) {

                angular.forEach(response.data, function (value, index) {
                    var urlBerkas = 'api/Persyaratan/' + value.idLayanan + '/layanan';
                    $http({
                        method: 'Get',
                        url: urlBerkas,
                        headers: UserServices.getHeaders()
                    }).then(function (response) {
                        value.Berkas = response.data;
                        service.Permohonan.push(value);
                    }, function (error) {

                    })
                    
                })

                service.instance = true;
                def.resolve(response.data);

            }, function (response) {
                if (response.status = 401)
                    $state.go('login');

                def.reject();
            });
        } else
            def.resolve(this.Permohonan);

        return def.promise;
    }

    function EditItem(item) {
        $http({
            method: 'put',
            url: 'api/Permohonan/CompleteStep',
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
