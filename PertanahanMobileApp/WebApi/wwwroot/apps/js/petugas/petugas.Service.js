angular.module("petugas.services", [])
    .factory('BerkasService', BerkasService)
    ;
function BerkasService($http, $state, $rootScope, $q, UserServices, MessageServices) {
    let def = $q.defer();

    var service = {
        instance: false,
        Berkas: [],
        get: get, post: post, delete: deleteItem, put: EditItem
    }
    service.get();
    return service;

    function get() {

        if (!this.instance) {
            $http({
                method: 'Get',
                url: 'api/Petugas',
                headers: UserServices.getHeaders()
            }).then(function (response) {

                angular.forEach(response.data, function (value, index) {
                    service.Petugas.push(value);
                })

                service.instance = true;
                def.resolve(response.data);

            }, function (response) {
                if (response.status = 401)
                    $state.go('login');

                def.reject();
            });
        } else
            def.resolve(this.Petugas);

        return def.promise;
    }

    function post(data) {
        $http({
            method: 'Post',
            url: '/Api/Petugas',
            data: data,
            headers: UserServices.getHeaders()
        }).then(function (response) {
            data.id = response.id;
            service.Petugas.push(data);
            MessageServices.success("Data Berhasil Disimpan");
            def.resolve(data);

        }, function (response) {
            def.reject();
            MessageServices.error(response.data);
        });
        return def.promise;
    }


    function deleteItem(item) {
        $http({
            method: 'delete',
            url: '/Api/Petugas/' + item.id,
            data: item,
            headers: UserServices.getHeaders()
        }).then(function (response) {
            var index = service.Kategories.indexOf(item, 1);
            service.Petugas.splice(index, 1);
            MessageServices.success("Data Berhasil Dihapus");
            def.resolve(response.data);
        }, function (response) {
            MessageServices.error("Data Tidak Terhapus");
            def.reject();
        });
        return def.promise;
    }

    function EditItem(item) {
        $http({
            method: 'put',
            url: '/Api/KategoriLayanan/' + item.id,
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
