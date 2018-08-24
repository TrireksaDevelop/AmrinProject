
angular.module("admin.services", [])
.factory('UserServices', UserServices)
    .factory('LayananServices', LayananServices)
    .factory('KategoriServices', KategoriServices)
    .factory('PetugasServices', PetugasServices)
    .factory('BidangServices', BidangServices)
    .factory('TahapanServices', TahapanServices)
    .factory('PersyaratanServices',PersyaratanServices)

;




function UserServices($http, $state,$rootScope) {

    this.token = sessionStorage.getItem("AuthToken");
    function logout(){
        $state.go('login');
    }

    function getToken() {
        return sessionStorage.getItem("AuthToken");
    }

    function getHeaders() {
        if (getToken() == null)
            $state.go('login');
        else

        return { 'Authorization': 'Bearer ' + getToken() }
    }

    function getUser() {
        return sessionStorage.getItem("UserName");
    }


    return {
        logout: logout, getToken: getToken, getHeaders: getHeaders,getUser:getUser
    }
}


function PetugasServices($http, $state, $rootScope, $q, UserServices, MessageServices) {
    let def = $q.defer();

    var service = {
        instance: false,
        Petugas: [],
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

function KategoriServices($http, $state, $rootScope, $q, UserServices,MessageServices) {
    var def = $q.defer();

    var service = {
        instance: false,
        Kategories: [],
        get: get, post: post, delete: deleteItem, put: EditItem
    }
    service.get();
    return service;

    function get() {

        if (!this.instance) {
            $http({
                method: 'Get',
                url: 'api/KategoriLayanan',
                headers: UserServices.getHeaders()
            }).then(function (response) {

                angular.forEach(response.data, function (value, index) {
                    service.Kategories.push(value);
                })

                service.instance = true;
                def.resolve(response.data);

            }, function (response) {
                if (response.status = 401)
                    $state.go('login');

                def.reject();
            });
        } else
            def.resolve(this.Kategories);

        return def.promise;
    }

    function post(data) {
        $http({
            method: 'Post',
            url: '/Api/KategoriLayanan',
            data: data,
            headers: UserServices.getHeaders()
        }).then(function (response) {
            service.Kategories.push(response.data);
            MessageServices.success("Data Berhasil Disimpan");
            def.resolve(response.data);
          
        }, function (response) {
            def.reject();
            if (response.status = 401)
                $state.go('login');
        });
        return def.promise;
    }


    function deleteItem(item) {
        $http({
            method: 'delete',
            url: '/Api/KategoriLayanan/' + item.id,
            data: item,
            headers: UserServices.getHeaders()
        }).then(function (response) {
            var index = service.Kategories.indexOf(item, 1);
            service.Kategories.splice(index, 1);
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

function LayananServices($http,$state,$rootScope,$q,UserServices,MessageServices)
{
    var def = $q.defer();

    var service = {
        instance: false,
        Layanans: [],
        get: get, post: post, delete: deleteItem, put: EditItem, puttahapan: EditTahapans
    }
    service.get();
    return service;

    function get() {
       
        if (!this.instance) {
            $http({
                method: 'Get',
                url: '/Api/Layanan',
                headers: UserServices.getHeaders()
            }).then(function (response) {

                angular.forEach(response.data,function (value, index) {
                    service.Layanans.push(value);
                })
                
                service.instance = true;
                def.resolve(response.data);
                
            }, function (response) {
                if (response.status = 401)
                    $state.go('login');

                def.reject();
            });
        } else
            def.resolve(this.Suppliers);

        return def.promise;
    }

    function post(data) {
        $http({
            method: 'Post',
            url: '/api/Layanan',
            data: data,
            headers: UserServices.getHeaders()
        }).then(function (response) {
            service.Layanans.push(response.data);
            def.resolve(response.data);
            MessageServices.success("Data Berhasil Disimpan");
        }, function (response) {
            if (response.status = 401)
                $state.go('login');
            def.reject();
        });
        return def.promise;
    }


    function deleteItem(item) {
        $http({
            method: 'delete',
            url: '/Api/Supplier?Id=' + item.Id,
            data: item,
            headers: UserServices.getHeaders()
        }).then(function (response) {
            var index = service.Layanans.indexOf(item, 1);
            service.Layanans.splice(index, 1);
            def.resolve(response.data);
            MessageServices.success("Berhasil DIhapus");
            }, function (response) {
                MessageServices.error("Data Tidak Terhapus");
            def.reject();
        });
        return def.promise;
    }

    function EditItem(item) {
        $http({
            method: 'put',
            url: '/Api/Supplier?Id=' + item.Id,
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

    function EditTahapans(datas, layanan) {
        let def=$q.defer();
        $http({
            method: 'put',
            url: '/Api/Layanan/tahapans/' + layanan.id,
            data: datas,
            headers: UserServices.getHeaders()
        }).then(function (response) {
            MessageServices.success("Data Tersimpan");
            def.resolve(response.data);
            }, function (response) {
                MessageServices.error(response.data);
            def.reject();
        });
        return def.promise;
    }
}

function BidangServices($http, $q, UserServices, MessageServices, $state) {
    var def = $q.defer();

    var service = {
        instance: false,
        Bidangs: [],
        get: get, post: post, delete: deleteItem, put: EditItem
    }
    service.get();
    return service;

    function get() {

        if (!this.instance) {
            $http({
                method: 'Get',
                url: 'api/Bidang',
                headers: UserServices.getHeaders()
            }).then(function (response) {

                angular.forEach(response.data, function (value, index) {
                    service.Bidangs.push(value);
                })
                service.instance = true;
                def.resolve(response.data);

            }, function (response) {
                if (response.status = 401)
                    $state.go('login');

                def.reject();
            });
        } else
            def.resolve(this.Bidangs);

        return def.promise;
    }

    function post(data) {
        $http({
            method: 'Post',
            url: 'api/Bidang',
            data: data,
            headers: UserServices.getHeaders()
        }).then(function (response) {
            data.id = response.data.id;
            service.Bidangs.push(data);
            MessageServices.success("Data Berhasil Disimpan");
            def.resolve(data);
            }, function (response) {
                MessageServices.error(response.data);
            def.reject();
           
        });
        return def.promise;
    }


    function deleteItem(item) {
        $http({
            method: 'delete',
            url: 'api/Bidang/' + item.id,
            data: item,
            headers: UserServices.getHeaders()
        }).then(function (response) {
            var index = service.Bidangs.indexOf(item, 1);
            service.Bidangs.splice(index, 1);
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
            url: 'api/Bidang/' + item.id,
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

function TahapanServices($http, $q, UserServices, MessageServices, $state) {
    var def = $q.defer();

    var service = {
        instance: false,
        Tahapans: [],
        get: get, post: post, delete: deleteItem, put: EditItem
    }
    service.get();
    return service;

    function get() {

        if (!this.instance) {
            $http({
                method: 'Get',
                url: 'api/tahapan',
                headers: UserServices.getHeaders()
            }).then(function (response) {

                angular.forEach(response.data, function (value, index) {
                    service.Tahapans.push(value);
                })
                service.instance = true;
                def.resolve(response.data);

            }, function (response) {
                if (response.status = 401)
                    $state.go('login');

                def.reject();
            });
        } else
            def.resolve(this.Bidangs);

        return def.promise;
    }

    function post(data) {
        $http({
            method: 'Post',
            url: 'api/tahapan',
            data: data,
            headers: UserServices.getHeaders()
        }).then(function (response) {
            data.id = response.data.id;
            service.Tahapans.push(data);
            MessageServices.success("Data Berhasil Disimpan");
            def.resolve(data);
        }, function (response) {
            MessageServices.error(response.data);
            def.reject();

        });
        return def.promise;
    }


    function deleteItem(item) {
        $http({
            method: 'delete',
            url: 'api/tahapan/' + item.id,
            data: item,
            headers: UserServices.getHeaders()
        }).then(function (response) {
            var index = service.Tahapans.indexOf(item, 1);
            service.Tahapans.splice(index, 1);
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
            url: 'api/Tahapan/' + item.id,
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

function PersyaratanServices($http, $q, UserServices, MessageServices, $state) {
   

    var service = {
        instance: false,
        Persyaratans: [],
        get: get, post: post, delete: deleteItem, put: EditItem
    }
    
    return service;

    function get(layananid) {
        var def = $q.defer();
        if (!this.instance) {
            $http({
                method: 'Get',
                url: '/api/Persyaratan/' + layananid+"/layanan",
                headers: UserServices.getHeaders()
            }).then(function (response) {

                angular.forEach(response.data, function (value, index) {
                    service.Persyaratans.push(value);
                })
                service.instance = true;
                def.resolve(response.data);

            }, function (response) {
                if (response.status = 401)
                    $state.go('login');

                def.reject();
            });
        } else
            def.resolve(this.Persyaratans);

        return def.promise;
    }

    function post(data) {
        let def = $q.defer();
        $http({
            method: 'Post',
            url: 'api/Persyaratan',
            data: data,
            headers: UserServices.getHeaders()
        }).then(function (response) {
            data.id = response.data.id;
            service.Persyaratans.push(data);
            MessageServices.success("Data Berhasil Disimpan");
            def.resolve(data);
        }, function (response) {
            MessageServices.error(response.data);
            def.reject();

        });
        return def.promise;
    }


    function deleteItem(item) {
        let def = $q.defer();
        $http({
            method: 'delete',
            url: 'api/Persyaratan/' + item.id,
            data: item,
            headers: UserServices.getHeaders()
        }).then(function (response) {
            var index = service.Persyaratans.indexOf(item, 1);
            service.Persyaratans.splice(index, 1);
            MessageServices.success("Data Berhasil Dihapus");
            def.resolve(response.data);
        }, function (response) {
            MessageServices.error("Data Tidak Terhapus");
            def.reject();
        });
        return def.promise;
    }

    function EditItem(item) {
        let def = $q.defer();
        $http({
            method: 'put',
            url: 'api/Persyaratan/' + item.id,
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

    ;