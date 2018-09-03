angular.module("petugas.services", [])
    .factory('BerkasService', BerkasService)
    ;
function BerkasService($http, $state, $rootScope, $q, MessageServices) {
    let def = $q.defer();

    var service = {
        instance: false,
        Berkas: [],
        get: get
    }
    service.get();
    return service;

    function get() {

        if (!this.instance) {
            $http({
                method: 'Get',
                url: 'api/Permohonan/admin'
            }).then(function (response) {

                angular.forEach(response.data, function (value, index) {
                    service.Berkas.push(value);
                })

                service.instance = true;
                def.resolve(response.data);

            }, function (response) {
                if (response.status = 401)
                    $state.go('login');

                def.reject();
            });
        } else
            def.resolve(this.Berkas);

        return def.promise;
    }
}
