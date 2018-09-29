angular.module("petugas.services", [])
    .factory('BerkasService', BerkasService)
    .factory('fileUploadService', fileUploadService)
    ;

function fileUploadService($http, $q) {

    this.uploadFileToUrl = function (file, uploadUrl) {
        //FormData, object of key/value pair for form fields and values
        var fileFormData = new FormData();
        fileFormData.append('file', file);

        var deffered = $q.defer();
        $http.post(uploadUrl, fileFormData, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }

        }).success(function (response) {
            deffered.resolve(response);

        }).error(function (response) {
            deffered.reject(response);
        });

        return deffered.promise;
    }
}
/*function InboxServices($http, $state, $q, MessageServices, UserServices) {
    let def = $q.defer();
    var service = {
        instance: false,
        Message: [],
        get: get, post: post
    }
    service.get(item);
    return service;

    function get(item) {
        if (!this.instance) {
            NProgress.start();
            $http({
                method: 'Get',
                url: '/api/inbox?id=' + item.id,
                headers: UserService.getHeaders()
            }).then(function (response) {
                service.Message.push(response.data);

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
            def.resolve(this.Message);

        return def.promise;
    }
    function post(data) {
        $http({
            method: 'Post',
            url: '/api/inbox',
            data: data,
            headers: UserServices.getHeaders()
        }).then(function (response) {
            data.id = response.id;
            service.Message.push(data);
            MessageServices.success("Data Berhasil Disimpan");
            def.resolve(data);

        }, function (response) {
            def.reject();
            MessageServices.error(response.data);
        });
        return def.promise;
    }
}*/

function BerkasService($http, $state, $rootScope, $q, MessageServices, UserServices) {
    let def = $q.defer();

    var service = {
        instance: false,
        Message: {},
        Permohonan: [],
        get: get, put: EditItem, post: post, UploadFoto: WithFoto
    }
    service.get();
    return service;

    function get() {

        if (!this.instance) {
            NProgress.start();
            $http({
                method: 'Get',
                url: '/api/Permohonan/admin',
                headers: UserServices.getHeaders()
            }).then(function (response) {
                var dataPermohonan = response.data;
                angular.forEach(dataPermohonan, function (value, key) {
                    $http({
                        method: 'Get',
                        url: '/api/inbox?id=' + value.id,
                        headers: UserServices.getHeaders()
                    }).then(function (response) {
                        value.Message = response.data;
                    }, function (response) {
                        //MessageServices.error(response.data);
                    });
                })
                

                response.data.Message = service.Message;
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

    function WithFoto(item) {
        var def = $q.defer();

        $http({

            method: 'put',

            headers: {

                "cache-control": "no-cache",
                'Authorization': 'Bearer ' + UserServices.getToken(),

            },

            processData: false,

            contentType: false,

            mimeType: "multipart/form-data",

            url: '/api/Permohonan/' + item.id,

            data: item

        }).then(function (response) {
            service.instance = true;
            def.resolve(response.data);
        }, function (response) {
            if (response.status = 401)
                MessageServices.error("Anda Tidak memiliki Hak Akses");
            else
                MessageServices.error(response.data.Message);
            def.reject();
        });
        return def.promise;
    }
    function EditItem(item) {
        var def = $q.defer();
        $http({
            method: 'post',
            url: '/api/Permohonan/UpdatePermohonan',
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

    function post(item) {
        let def = $q.defer();
        $http({
            method: 'post',
            url: '/api/inbox',
            data: item,
            headers: UserServices.getHeaders()
        }).then(function (response) {

            MessageServices.success("Command Terkirim!");
            def.resolve(response.data);
        }, function (response) {
            MessageServices.error("Command gagal dikirim!!!");
            def.reject();
        });
        return def.promise;
    }
}
